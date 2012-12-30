using System;
using System.Collections.Generic;
using System.Text;

namespace CaptureItPlus.Libs
{
    public class PluginConfiguration
    {
        private string _name;
        private IList<PluginData> _configuration;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public IList<PluginData> Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }
    }
}
