using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFTSpectrumAnalyzer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        #region attributes
        WaveIn m_waveInStream;
        int m_fftlen = 2 << 11;
        int m_sampFreq = 44100;
        int m_bitRate = 16;
        bool m_startup = true;
        // tag used to identify the object in the scene
        private static readonly string DefaultLinePlotTag = "LinePlot";
        private static readonly string DefaultMarkerPlotTag = "MarkerPlot";
        // flag indicating the form is closing
        private bool m_shutdown; 
        #endregion

        #region properties
        // helper property for easy access to the line plot in the scene
        public ILLinePlot Line { get { return ilPanel1.Scene.First<ILLinePlot>(DefaultLinePlotTag); } }
        // helper property for easy access to the line plot used for markers in the scene       
        public ILLinePlot Marker { get { return ilPanel1.Scene.First<ILLinePlot>(DefaultMarkerPlotTag); } }

        #endregion

        // this gets called when the panel is loaded into the form
        private void ilPanel1_Load(object sender, EventArgs e) {
            m_shutdown = false; 
            // setup the scene
            ilPanel1.Scene.Add(new ILPlotCube(twoDMode: false) {
                Children = {
                // create two line plots: the first is used to display the data itself ... 
                    new ILLinePlot(0, DefaultLinePlotTag, Color.Magenta, lineWidth: 1),
                    // .. the second is used for marking magnitude peaks, it gets the line hidden
                    new ILLinePlot(0, DefaultMarkerPlotTag, markerStyle:MarkerStyle.Square) { Line = { Visible = false }}
                },
                // we want both axes in logarithmic scale
                ScaleModes = { XAxisScale = AxisScale.Logarithmic, YAxisScale = AxisScale.Logarithmic },
                // configure axis labels
                Axes = { XAxis = { Label = { Text = "Frequency [1/\\omega]" }, LabelPosition = new Vector3(1, 1, 0) },
                         YAxis = { Label = { Text = "Magnitude [dB]" }, LabelPosition = new Vector3(1, 1, 0), LabelAnchor = new PointF(1,0) }
                }
            }); 

            // setup audio stream (this is not related to ILNumerics but to the NAudio helper lib)
            m_waveInStream = new WaveIn();
            m_waveInStream.WaveFormat = new WaveFormat(m_sampFreq, m_bitRate, 1); // 1: mono
            m_waveInStream.DeviceNumber = 0;
            m_waveInStream.BufferMilliseconds = (int)(m_fftlen / (float)m_sampFreq * 1010);  // roughly one buffersize
            m_waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
            try {
                m_waveInStream.StartRecording();
            } catch (NAudio.MmException exc) {
                // when no device exists or no microphone is plugged in, an exception will be thrown here
                MessageBox.Show("Error initializing audio device. Make sure that a default recording device is available!" + Environment.NewLine + "Error details:" + exc.Message); 
            }
        }

        // the callback from naudio 
        void waveInStream_DataAvailable(object sender, WaveInEventArgs e) {
            if (m_shutdown) return;  
            using (ILScope.Enter()) {
                // prepare variables for requesting X values and the index of the maximum value
                ILArray<int> maxID = 1;
                // convert the recorded samples in computation module: 
                ILArray<float> Y = Computation.GetMagnitudes(e.Buffer, e.BytesRecorded, m_fftlen, maxID);
                // update the line shape
                Line.Update(Y);
                // update the marker point
                ILArray<float> markerPoints = ILMath.zeros<float>(2, maxID.S[0]);
                markerPoints[0, ":"] = ILMath.tosingle(maxID);
                markerPoints[1, ":"] = Y[maxID]; 
                Marker.Update(markerPoints);

                // on the first only run we zoom to content
                if (m_startup) {
                    m_startup = false;
                    ilPanel1.Scene.First<ILPlotCube>().Reset();
                }
                // redraw the scene 
                ilPanel1.Refresh();
            }
        }
        // cleaning up the naudio
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            m_shutdown = true; 
            CloseDevice();
        }
        private void CloseDevice() {
            if (m_waveInStream != null) {
                m_waveInStream.StopRecording();
                m_waveInStream.Dispose();
                m_waveInStream = null;
            }
        }


        // private computation module 
        private class Computation : ILMath {

            /// <summary>
            /// computes normalized magnitudes out of raw samples 
            /// </summary>
            /// <param name="buffer">sample buffer from naudio</param>
            /// <param name="buffLen">number of samples</param>
            /// <param name="fftLen">number of samples for fft </param>
            /// <param name="MaxValue">[output] index of maximum magnitude value</param>
            /// <returns>normalized magnitudes (for Y axis)</returns>
            public static ILRetArray<float> GetMagnitudes(byte[] buffer, int buffLen, int fftLen, ILOutArray<int> MaxValue) {
                using (ILScope.Enter()) {
                    // how many samples returned from naudio? 
                    int newSampleLen = Math.Min(buffLen / 2, fftLen);
                    // create a temporary array for the samples
                    ILArray<float> tmp = zeros<float>(fftLen, 1);

                    // transfer byte[] buffer to temp array
                    for (int s = 0; s < newSampleLen; s++) { 
                        tmp.SetValue((short)(buffer[s * 2 + 1] << 8 | buffer[s * 2]), s);
                    }

                    // transform into frequency domain, we use a simple cosine window here 
                    ILArray<float> cosWin = sin(pif * counter<float>(0f, 1f, tmp.Length, 1) / (tmp.Length - 1));
                    //ILArray<float> hamm = (0.54f - 0.46f * cos(2f * pif * counter<float>(0f,1f,ret.Length,1)/ (ret.Length - 1))); 

                    // compute the magnitudes, keep relevant part only
                    tmp.a = abs(fft(tmp * cosWin)[r(0, end / 2 + 1)]);
                    // some poor mans high pass filter 
                    if (tmp.Length > 20) 
                        tmp["0:20"] = tmp[20]; 

                    // compute max values 
                    ILArray<int> maxTmpId = 0;  // -> we do want the indices
                    ILArray<float> maxTmp = sort(tmp, Indices: maxTmpId, descending: true); 
                    // assign to output parameter
                    MaxValue.a = maxTmpId["0:4"];
                    // return magnitudes Y values 
                    return tmp.T;
                }
            }
        }
    }
}
