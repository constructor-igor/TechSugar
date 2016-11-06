using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

/*
 * References:
 *  - https://habrahabr.ru/post/282231/
 *  - key: https://www.microsoft.com/cognitive-services/en-us/face-api
 *  - https://github.com/Microsoft/Cognitive-Face-Windows/blob/master/README.md
 *  - https://www.nuget.org/packages/Microsoft.ProjectOxford.Face/
 * 
 * */

namespace Microsoft_Face_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFaceServiceClient m_faceServiceClient = new FaceServiceClient("<key from https://www.microsoft.com/cognitive-services/en-us/face-api>");

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {

            var openDlg = new Microsoft.Win32.OpenFileDialog();

            openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";
            bool? result = openDlg.ShowDialog(this);

            if (!(bool)result)
            {
                return;
            }

            string filePath = openDlg.FileName;

            Uri fileUri = new Uri(filePath);
            BitmapImage bitmapSource = new BitmapImage();

            bitmapSource.BeginInit();
            bitmapSource.CacheOption = BitmapCacheOption.None;
            bitmapSource.UriSource = fileUri;
            bitmapSource.EndInit();

            FacePhoto.Source = bitmapSource;
            Title = "Analysis...";

            FaceRectangle[] faceRects = await UploadAndDetectFaces(filePath);
            Title = String.Format("Found {0} faces", faceRects.Length);
            if (faceRects.Length > 0)
            {
                DrawingVisual visual = new DrawingVisual();
                DrawingContext drawingContext = visual.RenderOpen();
                drawingContext.DrawImage(bitmapSource,
                    new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));
                double dpi = bitmapSource.DpiX;
                double resizeFactor = 96 / dpi;

                foreach (var faceRect in faceRects)
                {
                    drawingContext.DrawRectangle(
                        Brushes.Transparent,
                        new Pen(Brushes.Red, 2),
                        new Rect(
                            faceRect.Left * resizeFactor,
                            faceRect.Top * resizeFactor,
                            faceRect.Width * resizeFactor,
                            faceRect.Height * resizeFactor
                            )
                    );
                }

                drawingContext.Close();
                RenderTargetBitmap faceWithRectBitmap = new RenderTargetBitmap(
                    (int)(bitmapSource.PixelWidth * resizeFactor),
                    (int)(bitmapSource.PixelHeight * resizeFactor),
                    96,
                    96,
                    PixelFormats.Pbgra32);

                faceWithRectBitmap.Render(visual);
                FacePhoto.Source = faceWithRectBitmap;
            }
        }

        private async Task<FaceRectangle[]> UploadAndDetectFaces(string imageFilePath)
        {
            List<FaceAttributeType> FaceAG = new List<FaceAttributeType>();
            FaceAG.Add(FaceAttributeType.Age);
            FaceAG.Add(FaceAttributeType.Gender);// параметры которые хотим узнать

            try
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    var faces = await m_faceServiceClient.DetectAsync(imageFileStream, true, false, FaceAG); // отправляем фото на анализ
                    var faceRects = faces.Select(face => face.FaceRectangle); // получаем список лиц
                    var faceA = faces.Select(face => face.FaceAttributes);// получаем атрибуты - пол и возраст
                    FaceAttributes[] faceAtr = faceA.ToArray();
                    foreach (var fecea in faceAtr)
                    {
                        Console.WriteLine(@"Age: {0}", fecea.Age);
                        Console.WriteLine(@"Gender: {0}", fecea.Gender);
                        Console.WriteLine();
                     }

                    return faceRects.ToArray();
                }
            }
            catch (Exception)
            {
                return new FaceRectangle[0];
            }
        }
    }
}
