﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CaptureItPlus.Libs
{
    public interface ITool
    {
        void Execute(params object[] args);
        string Name { get; }
        string Description { get; }
        string MenuCaption { get; }
        bool IsVisible { get; }
    }
}
