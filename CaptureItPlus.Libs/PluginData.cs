namespace CaptureItPlus.Libs
{
    public class PluginData
    {
        private string _configurationKey;
        private string _configurationValue;

        public string ConfigurationKey
        {
            get
            {
                return _configurationKey;
            }
            set
            {
                _configurationKey = value;
            }
        }

        public string ConfigurationValue
        {
            get
            {
                return _configurationValue;
            }
            set
            {
                _configurationValue = value;
            }
        }
    }
}
