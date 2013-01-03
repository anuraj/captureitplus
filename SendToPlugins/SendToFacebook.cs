using CaptureItPlus.Libs;
using CaptureItPlus.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace SendToPlugins
{
    class SendToFacebook : ISendTo
    {
        private ISendToHost _sendToHost;
        public string Name
        {
            get { return "SendToFacebook"; }
        }

        public string Text
        {
            get { return "Facebook"; }
        }

        public void Execute(string filename)
        {
            SendToImgurUI _sendToImgurUI = new SendToImgurUI(filename, "Send to Facebook.com");
            if (_sendToImgurUI.ShowDialog() == DialogResult.OK)
            {
                string uploadedImageUrl = _sendToImgurUI.OutputFile;
                Process.Start("http://www.facebook.com/share.php?u=" + uploadedImageUrl);
            }
        }

        public ISendToHost Host
        {
            get
            {
                return _sendToHost;
            }
            set
            {
                _sendToHost = value;
            }
        }

        public void Dispose()
        {

        }
    }
}
