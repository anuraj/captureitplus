using System;
using System.Collections.Generic;
using System.Text;
using CaptureItPlus.Libs;
using System.Windows.Forms;

namespace SendToPlugins
{
    public class ApplyWaterMark : ISendTo
    {
        public string Name
        {
            get { return this.GetType().Name; }
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

        public string Text
        {
            get { return "&Watermarker"; }
        }

        public void Execute(string filename)
        {
            WaterMarkerUI waterMarker = new WaterMarkerUI(filename);
            waterMarker.Show();
        }

        public void Dispose()
        {
        }
    }
}
