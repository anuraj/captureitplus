namespace CaptureItPlus.Capturemodes
{
    using System.Drawing;
    using System.Windows.Forms;
    

    
    public class FixedRegionCapture : ICaptureMode
    {
        private string _folder;
        #region ICaptureMode Members

        public string Name
        {
            get { return "FixedRegionCapture"; }
        }

        public string Text
        {
            get { return "Fixed Region"; }
        }

        //public Image Icon
        //{
        //    get { return Properties.Resources.fixedselection; }
        //}

        public string Description
        {
            get { return "Helps to capture fixed region of the screen. Double click on the area to Save. And ESC to cancel."; }
        }

        public Keys ShortcutKey
        {
            get { return Keys.Control & Keys.F; }
        }

        public bool IsEnabled
        {
            get { return true; }
        }

        public void Initialize(params object[] arguments)
        {
            _folder = arguments[0].ToString();
        }

        public string Execute()
        {
            string fileName = string.Empty;
            using (frmOverlay frmOverlay = new frmOverlay(fileName, Common.CaptureModes.FixedRectangle))
            {
                if (frmOverlay.ShowDialog() == DialogResult.OK)
                {
                    fileName = frmOverlay.FileName;
                }
            }
            return fileName;
        }

        public bool IsFinished
        {
            get { return false; }
        }

        #endregion
    }
}
