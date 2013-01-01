using CaptureItPlus.Libs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SendToPlugins
{
    class SendToFTPServer : ISendTo
    {
        private ISendToHost _host;

        public string Name
        {
            get { return "FTP Server"; }
        }

        public string Text
        {
            get { return "FTP Server"; }
        }

        public void Execute(string filename)
        {
            using (SendToFTPServerUI sendToFtpServerUI = new SendToFTPServerUI(filename))
            {
                if (sendToFtpServerUI.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("File uploaded to server successfully.", "Send To FTP Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        public ISendToHost Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        public void Dispose()
        {

        }
    }
}
