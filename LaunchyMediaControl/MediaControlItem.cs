using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaunchyMediaControl
{
    internal class MediaControlItem
    {
        public string Label { get; private set; }
        public string FullPath { get; private set; }
        public string IconFileName { get; private set; }
        public Keys KeyPress { get; private set; }

        public MediaControlItem(string label, string fullPath, string iconFileName, Keys keyPress)
        {
            Label = label;
            FullPath = fullPath;
            IconFileName = iconFileName;
            KeyPress = keyPress;
        }
    }
}
