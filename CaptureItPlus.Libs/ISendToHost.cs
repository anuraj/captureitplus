using System;
using System.Collections.Generic;
using System.Text;

namespace CaptureItPlus.Libs
{
    public interface ISendToHost
    {
        void SaveConfiguration(string pluginName, PluginConfiguration configuration);
        PluginConfiguration LoadConfiguration(string pluginName);
    }
}
