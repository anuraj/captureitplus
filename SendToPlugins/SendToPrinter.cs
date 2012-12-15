namespace CaptureItPlus.Plugins
{
    using CaptureItPlus.Libs;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class SendToPrinter : ISendTo
    {
        private bool _isFinished;
        private Image _previewImage;
        public string Text
        {
            get { return "&Printer"; }
        }

        public void Execute(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return;
            }

            _previewImage = Image.FromFile(filename);
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += printDocument_PrintPage;

            using (PrintDialog printDialog = new PrintDialog())
            {
                printDialog.ShowHelp = false;
                printDialog.ShowNetwork = false;
                printDialog.Document = printDocument;
                printDialog.AllowPrintToFile = true;
                printDialog.AllowCurrentPage = false;
                printDialog.AllowSelection = false;
                printDialog.AllowSomePages = false;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
                _isFinished = true;
            }

        }

        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float newWidth = _previewImage.Width * 100 / _previewImage.HorizontalResolution;
            float newHeight = _previewImage.Height * 100 / _previewImage.VerticalResolution;

            float widthFactor = newWidth / e.MarginBounds.Width;
            float heightFactor = newHeight / e.MarginBounds.Height;

            if (widthFactor > 1 | heightFactor > 1)
            {
                if (widthFactor > heightFactor)
                {
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }
            e.Graphics.DrawImage(_previewImage, 0, 0, (int)newWidth, (int)newHeight);
        }

        public string Description
        {
            get
            {
                return "This plugin is used to print the captured image.";
            }
        }

        public string Help
        {
            get
            {
                return string.Format("Send To Printer Plugin.{0}Copyright (C) 2011 captureitplus developers. All rights reserved.", Environment.NewLine);
            }
        }

        public bool IsFinished
        {
            get { return _isFinished; }
        }

        public Keys ShortcutKey
        {
            get { return Keys.Control & Keys.P; }
        }
        public string Name
        {
            get { return this.GetType().Name; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_previewImage != null)
            {
                _previewImage.Dispose();
                _previewImage = null;
            }
        }

        #endregion
    }
}
