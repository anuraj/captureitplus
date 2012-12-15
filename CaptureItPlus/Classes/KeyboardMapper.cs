namespace CaptureItPlus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using CaptureItPlus.Libs;

    public class KeyboardMapper
    {
        private static Dictionary<int, string> _keys = new Dictionary<int, string>();

        [Category("Capture Modes"), Action("CaptureItPlus.Capturemodes.FullScreenCapture")]
        [Description("Keyboard shortcut for Fullscreen capture.")]
        public Keys FullScreen
        {
            get;
            set;
        }

        [Category("Capture Modes"), Action("CaptureItPlus.Capturemodes.FreeFormCapture")]
        [Description("Keyboard shortcut for FreeForm capture.")]
        public Keys FreeForm
        {
            get;
            set;
        }

        [Category("Capture Modes"), Action("CaptureItPlus.Capturemodes.RectangleCapture")]
        [Description("Keyboard shortcut for Rectangle capture.")]
        public Keys Rectangle
        {
            get;
            set;
        }

        [Category("Capture Modes"), Action("CaptureItPlus.Capturemodes.CircleCapture")]
        [Description("Keyboard shortcut for Circle Capture.")]
        public Keys Circle
        {
            get;
            set;
        }

        [Category("Capture Modes"), Action("CaptureItPlus.Capturemodes.FixedRegionCapture")]
        [Description("Keyboard shortcut for FixedRegion.")]
        public Keys FixedRegion
        {
            get;
            set;
        }

        [Category("Capture Modes"), Action("CaptureItPlus.Capturemodes.WindowCapture")]
        [Description("Keyboard shortcut for Window Capture.")]
        public Keys WindowCapture
        {
            get;
            set;
        }

        [Category("Capture Modes"), Action("CaptureItPlus.Capturemodes.ActiveWindowCapture")]
        [Description("Keyboard shortcut for ActiveWindow.")]
        public Keys ActiveWindow
        {
            get;
            set;
        }

        [Category("Send To"), Action("CaptureItPlus.Plugins.SendToClipboard", typeof(ISendTo))]
        [Description("Keyboard shortcut for Send To Clipboard.")]
        public Keys SendToClipboard
        {
            get;
            set;
        }

        [Category("Send To"), Action("CaptureItPlus.Plugins.SendToPrinter", typeof(ISendTo))]
        [Description("Keyboard shortcut for Send To Printer.")]
        public Keys SendToPrinter
        {
            get;
            set;
        }

        [Category("Send To"), Action("CaptureItPlus.Plugins.SendToMailRecipient", typeof(ISendTo))]
        [Description("Keyboard shortcut for Send To Mail client.")]
        public Keys SendToMailRecipient
        {
            get;
            set;
        }

        public void Save()
        {
            Properties.Settings.Default.Shortcuts = Utilities.Serialize<KeyboardMapper>(this);
            Properties.Settings.Default.Save();
        }

        public static KeyboardMapper Load()
        {
            KeyboardMapper keyboardMapper = new KeyboardMapper();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Shortcuts))
            {
                keyboardMapper = Utilities.Deserialize<KeyboardMapper>(Properties.Settings.Default.Shortcuts);
            }
            return keyboardMapper;
        }

        #region fields

        public const int MOD_ALT = 0x1;
        public const int MOD_CONTROL = 0x2;
        public const int MOD_SHIFT = 0x4;
        public const int MOD_WIN = 0x8;
        public const int WM_HOTKEY = 0x312;

        #endregion

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static void RegisterHotKeys(Form form, Keys key, int keyId, string property)
        {
            int modifiers = 0;

            if ((key & Keys.Alt) == Keys.Alt)
                modifiers = modifiers | MOD_ALT;

            if ((key & Keys.Control) == Keys.Control)
                modifiers = modifiers | MOD_CONTROL;

            if ((key & Keys.Shift) == Keys.Shift)
                modifiers = modifiers | MOD_SHIFT;

            Keys k = key & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;
            //keyId = form.GetHashCode(); // this should be a key unique ID, modify this if you want more than one hotkey
            _keys.Add(keyId, property);
            RegisterHotKey((IntPtr)form.Handle, keyId, (uint)modifiers, (uint)k);
        }

        private delegate void Func();

        public static void UnregisterHotKeys(Form form)
        {
            try
            {
                foreach (var key in _keys)
                {
                    UnregisterHotKey(form.Handle, key.Key);                     // modify this if you want more than one hotkey
                }
                _keys.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static string GetProperty(int keyId)
        {
            return _keys[keyId];
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ActionAttribute : Attribute
    {
        public string ActionMethod
        {
            private set;
            get;
        }

        public Type PluginType
        {
            private set;
            get;
        }

        public ActionAttribute(string actionMethod)
        {
            ActionMethod = actionMethod;
        }

        public ActionAttribute(string actionMethod, Type pluginType)
        {
            ActionMethod = actionMethod;
            PluginType = pluginType;
        }
    }
}

