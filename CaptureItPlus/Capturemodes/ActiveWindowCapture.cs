namespace CaptureItPlus.Capturemodes
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;
    using System.Threading;
    using dotnetthoughts.CaptureItPlus;
    

    
    public class ActiveWindowCapture : ICaptureMode
    {
        private string _folder;
        private object _owner;
        private IntPtr _windowHandle = IntPtr.Zero;

        public string Name
        {
            get { return "ActiveWindow"; }
        }

        public string Text
        {
            get { return "&Active Window"; }
        }

        //public Image Icon
        //{
        //    get { return Properties.Resources.activewin; }
        //}

        public string Description
        {
            get { return "Helps to capture Active Window from the running windows, if not Active window found, it will take desktop screenshot. It will capture the screen after 3 seconds."; }
        }

        public Keys ShortcutKey
        {
            get { return Keys.Control & Keys.F11; }
        }

        public void Initialize(params object[] arguments)
        {
            _folder = arguments[0].ToString();
            _owner = arguments[1].ToString();
        }

        public string Execute()
        {
            Thread.Sleep(3000);
            string fileName = string.Empty;
            var rect = default(NativeMethods.RECT);
            _windowHandle = NativeMethods.GetForegroundWindow();
            IntPtr handle = _windowHandle == IntPtr.Zero ? NativeMethods.GetDesktopWindow() : _windowHandle;
            if (NativeMethods.GetWindowRect(handle, ref rect))
            {
                var region = NativeMethods.ConvertToRectangle(rect);
                using (var bitmap = new Bitmap(region.Width, region.Height, PixelFormat.Format32bppArgb))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(region.Left, region.Top, 0, 0, region.Size);
                        if (Properties.Settings.Default.IncludeCursor)
                        {
                            Rectangle cursorBounds = new Rectangle(Cursor.Position, Cursor.Current.Size);
                            graphics.DrawIcon(CursorHelper.GetIcon(), cursorBounds);
                        }
                        fileName = Common.SaveImage(bitmap);
                    }
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
