namespace CaptureItPlus.Capturemodes
{
    using System.Drawing;
    using System.Windows.Forms;
    

    
    public class RectangleCapture : ICaptureMode
    {
        private string _folder;

        public string Name
        {
            get { return "Rectangle"; }
        }

        public string Text
        {
            get { return "&Rectangle"; }
        }

        //public Image Icon
        //{
        //    get { return Properties.Resources.rectangle; }
        //}

        public string Description
        {
            get { return "Helps to capture the screen on rectangle shape. Press ESC to cancel"; }
        }

        public Keys ShortcutKey
        {
            get { return Keys.Control & Keys.F11; }
        }

        public void Initialize(params object[] arguments)
        {
            _folder = arguments[0].ToString();
        }

        public string Execute()
        {
            string fileName = string.Empty;
            using (frmOverlay frmOverlay = new frmOverlay(fileName, Common.CaptureModes.Rectangle))
            {
                if (frmOverlay.ShowDialog() == DialogResult.OK)
                {
                    fileName = frmOverlay.FileName;
                }
            }
            return fileName;
        }

        public bool IsEnabled
        {
            get { return true; }
        }

        public bool IsFinished
        {
            get { return true; }
        }
    }
}
