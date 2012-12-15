using System;
using CaptureItPlus.Libs;

namespace ColorPickerTool
{
    public class ColorPicker : ITool
    {
        public void Execute(params object[] args)
        {
            ColorPickerUI colorPickerUI = new ColorPickerUI();
            colorPickerUI.ShowDialog();
        }

        public string Name
        {
            get { return "Color Picker"; }
        }

        public string Description
        {
            get { return "Color Picker"; }
        }

        public string MenuCaption
        {
            get { return "Color Picker"; }
        }

        public bool IsVisible
        {
            get { return true; }
        }
    }
}
