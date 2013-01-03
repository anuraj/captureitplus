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
            _sendToImgurUI = new SendToImgurUI(filename, "Send to Imagur.com");
            if (_sendToImgurUI.ShowDialog() == DialogResult.OK)
            {
                string uploadedImageUrl = _sendToImgurUI.OutputFile;
                var result = MessageBox.Show(string.Format("Image uploaded successfully. Here is the URL : {0} Would you like to copy the url to clipboard?"
                    , uploadedImageUrl), "Send To Imgur", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(uploadedImageUrl);
                }
            }
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
