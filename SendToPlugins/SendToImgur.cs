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

        public string Description
        {
            get { return "This plugin will help to upload the image to Imgur.com"; }
        }

        public string Help
        {
            get { return string.Format("Send To Imgur.com Plugin.{0}Copyright (C) 2011 captureitplus developers. All rights reserved.", Environment.NewLine); }
        }

        public bool IsFinished
        {
            get { return false; }
        }

        public Keys ShortcutKey
        {
            get { return Keys.Control & Keys.F; }
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
