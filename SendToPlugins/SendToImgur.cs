using System;
using System.Windows.Forms;
using CaptureItPlus.Libs;

namespace CaptureItPlus.Plugins
{
    public class SendToImgur : ISendTo
    {
        private SendToImgurUI _sendToImgurUI;

        public string Name
        {
            get { return "SendToImgur"; }
        }

        public string Text
        {
            get { return "Imgur.com"; }
        }

        public void Execute(string filename)
        {
            _sendToImgurUI = new SendToImgurUI(filename);
            _sendToImgurUI.Show();
            _sendToImgurUI.UploadFile();
        }

                private ISendToHost _host;
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
            if (null != _sendToImgurUI)
            {
                _sendToImgurUI.Dispose();
            }
        }
    }
}
