using System;
using System.IO;
using System.Timers;

/*
 * copied from https://gist.github.com/ant-fx/989dd86a1ace38a9ac58
 *
 *
 */

namespace LogFileMonitoring
{
    public class LogFileMonitorLineMsg
    {
        public string[] Lines;
    }

    public class LogFileMonitor: IDisposable
    {
        public Action<LogFileMonitorLineMsg> OnLine;

        // file path + delimiter internals
        readonly string m_path;
        readonly string m_delimiter;

        // timer object
        private Timer m_t;

        // buffer for storing data at the end of the file that does not yet have a delimiter
        string _buffer = string.Empty;

        // get the current size
        long _currentSize;

        // are we currently checking the log (stops the timer going in more than once)
        bool _isCheckingLog;

        protected bool StartCheckingLog()
        {
            lock (m_t)
            {
                if (_isCheckingLog)
                    return true;

                _isCheckingLog = true;
                return false;
            }
        }

        protected void DoneCheckingLog()
        {
            lock (m_t)
                _isCheckingLog = false;
        }

        public LogFileMonitor(string path, Action<LogFileMonitorLineMsg> online, string delimiter = "\r\n")
        {
            m_path = path;
            m_delimiter = delimiter;
            OnLine = online;
            Start();
        }

        public void Start()
        {
            // get the current size
            _currentSize = new FileInfo(m_path).Length;

            // start the timer
            m_t = new Timer();
            m_t.Elapsed += CheckLog;
            m_t.AutoReset = true;
            m_t.Start();
        }

        private void CheckLog(object s, ElapsedEventArgs e)
        {
            if (StartCheckingLog())
            {
                try
                {
                    // get the new size
                    var newSize = new FileInfo(m_path).Length;

                    // if they are the same then continue.. if the current size is bigger than the new size continue
                    if (_currentSize >= newSize)
                        return;

                    // read the contents of the file
                    using (var stream = File.Open(m_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        // seek to the current file position
                        sr.BaseStream.Seek(_currentSize, SeekOrigin.Begin);

                        // read from current position to the end of the file
                        var newData = _buffer + sr.ReadToEnd();

                        // if we don't end with a delimiter we need to store some data in the buffer for next time
                        if (!newData.EndsWith(m_delimiter))
                        {
                            // we don't have any lines to process so save in the buffer for next time
                            if (newData.IndexOf(m_delimiter, StringComparison.Ordinal) == -1)
                            {
                                _buffer += newData;
                                newData = string.Empty;
                            }
                            else
                            {
                                // we have at least one line so store the last section (without lines) in the buffer
                                var pos = newData.LastIndexOf(m_delimiter, StringComparison.Ordinal) + m_delimiter.Length;
                                _buffer = newData.Substring(pos);
                                newData = newData.Substring(0, pos);
                            }
                        }

                        // split the data into lines
                        var lines = newData.Split(new[] { m_delimiter }, StringSplitOptions.RemoveEmptyEntries);

                        // send back to caller, NOTE: this is done from a different thread!
                        OnLine(new LogFileMonitorLineMsg {Lines = lines });
                    }

                    // set the new current position
                    _currentSize = newSize;
                }
                catch (Exception)
                {
                }

                // we done..
                DoneCheckingLog();
            }
        }

        public void Stop()
        {
            m_t?.Stop();
            m_t = null;
        }

        #region IDisposable
        public void Dispose()
        {
            Stop();
        }
        #endregion
    }
}