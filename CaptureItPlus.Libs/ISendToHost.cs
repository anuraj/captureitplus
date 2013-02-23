using System;
using System.Collections.Generic;
using System.Text;

namespace CaptureItPlus.Libs
{
    public interface ISendToHost
    {
        void SaveConfiguration<T>(string pluginName, T configuration);
        T LoadConfiguration<T>(string pluginName);
    }
}
