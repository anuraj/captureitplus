namespace CaptureItPlus
{
    public static class Constants
    {
        public const string APP_TITLE = "CaptureIt Plus";
        public const string APP_ALREADY_RUNNING = "Another instance of CaptureItPlus is already running";
        public const string APP_EXCEPTION = "An application exception occured in CaptureIt Plus.{0}Please report the issue to http://captureitplus.codeplex.com.{0}Please attach the captureitplus.log file, which helps us to identify and fix the problem.";
        public const string APP_URL = "http://captureitplus.codeplex.com";
        public const string ABOUT_MESSAGE = "CaptureIt Plus v1.0{0}{0}Developed by Anuraj P <anuraj.p@dotnetthoughts.net>{0}Licensed under GNU GPL v2.{0}{0}Homepage : http://captureitplus.codeplex.com";
        public const string APPINFOMESG = "{0} is running. Right click for settings";
        public const string SELECTFOLDER = "Select output folder";
        public const string IMAGEFILTER = "PNG File|*.png|JPEG File|*.jpeg|BMP File|*.bmp|TIFF File|*.tiff|GIF File|*.gif|WMF File|*.wmf";
        public const string SOUNDFILTER = "Sound files(*.wav)|*.wav";
        public const string SELECTSOUND = "Select a wav file";
        public const string IMAGESELECTDLGTITLE = "Select an Image File";
        public const string INITMETHOD = "Initialize";
        public const string EXECUTEMETHOD = "Execute";
        public const string EXIT_CONFIRM = "An scheduled image capture process is running in background.{0}If you exit from CaptureItPlus will" +
            " cancel this process.{0}{0}Are you sure want to exit?";
        public const string SETTINGSFILE = "Settings.ini";
        public const string APP_NAME = "CaptureItPlus";
        public const string REGISTRY_LOC = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string IMAGE_SELECT_CONFIRM = "This action requires an image, and no captured image found in this session. Would you like to browse for an Image?";
        public const string HELPFILE = "captureitplushelp.chm";
        public const string TEMPLATE_HELP = "CaptureItPlus supports following place holders.{0}{0}1.%TICKS% - Date time ticks.(default){0}" +
            "2.%AUTO% - Auto increment.{0}3.%DATE% - Date(current system format){0}4.%TIME% - Time(current system format){0}5.%USER% - Logged in username.{0}" +
            "6.%SYSTEM% - Machine name.{0}{0}These place holders will be replaced{0}by actual values while capturing completes.";
        public const string DEFAULT_TEMPLATE = "CaptureItPlus%TICKS%";
        public const string ACTIVEWINDOWCAPTURE_PLUGIN = "CaptureItPlus.Capturemodes.WindowCaptureHelper";
    }
}
