using System;
using System.Collections.Generic;
using System.Text;
using CaptureItPlus.Libs;
using System.Windows.Forms;
using System.Drawing;


namespace CaptureItPlus.Plugins
{
    
    public class GrayScaleConverter : ISendTo
    {
        #region ISendTo Members

        public string Name
        {
            get { return this.GetType().Name; }
        }

        public string Text
        {
            get { return "Gray Scale Converter"; }
        }

        public void Execute(string filename)
        {
            if (MessageBox.Show(string.Format("This will create a copy of the image and convert the Image to Grayscale.{0}{0}Continue?", Environment.NewLine),
                Constants.APP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (Bitmap bitmap = Bitmap.FromFile(filename) as Bitmap)
                {
                    using (Bitmap bmp = ConvertToGrayscale(bitmap))
                    {
                        Common.SaveImage(bmp, filename);
                        MessageBox.Show("Image converted to Grayscale", Constants.APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private Bitmap ConvertToGrayscale(Bitmap source)
        {
            Bitmap bm = new Bitmap(source.Width, source.Height);
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = source.GetPixel(x, y);
                    int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    bm.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
                }
            }
            return bm;
        }

        public string Description
        {
            get { return "This plugin helps to convert an image to Gray scale"; }
        }

        public string Help
        {
            get { return string.Format("Send To GrayScale converter Plugin.{0}Copyright (C) 2011 captureitplus developers. All rights reserved.", Environment.NewLine); }
        }

        public bool IsFinished
        {
            get { return true; }
        }

        public Keys ShortcutKey
        {
            get { return Keys.Control & Keys.F; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
