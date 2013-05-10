using CaptureItPlus.Libs;

namespace ImageEditor
{
    class SendToScreenShotEditor : ISendTo
    {
        public string Name
        {
            get { return "ScreenShot Editor"; }
        }

        public string Text
        {
            get { return "ScreenShot Editor"; }
        }

        public void Execute(string filename)
        {
            using (FrmMainUI frmMainUI = new FrmMainUI(filename))
            {
                frmMainUI.ShowDialog();
            }
        }

        public ISendToHost Host
        {
            get;
            set;
        }

        public void Dispose()
        {
            //nothing
        }
    }
}
