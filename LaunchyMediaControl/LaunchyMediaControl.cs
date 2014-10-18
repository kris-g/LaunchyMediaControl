using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LaunchySharp;

namespace LaunchyMediaControl
{
    public class LaunchyMediaControl : IPlugin
    {
        private ICatItemFactory catItemFactory;
        private uint id;
        private const string Name = "MediaControl";
        private ILaunchyPaths launchyPaths;
        private Dictionary<string, MediaControlItem> mediaControlItems;

        public void init(IPluginHost pluginHost)
        {
            if (pluginHost != null)
            {
                catItemFactory = pluginHost.catItemFactory();
                launchyPaths = pluginHost.launchyPaths();
                id = pluginHost.hash(Name);
            }

            mediaControlItems = new[]
            {
                new MediaControlItem("PLAY", "Play.media", "media_play.ico", Keys.MediaPlayPause),
                new MediaControlItem("PAUSE", "Pause.media", "media_pause.ico", Keys.MediaPlayPause),
                new MediaControlItem("STOP", "Stop.media", "media_stop.ico", Keys.MediaStop),
                new MediaControlItem("REWIND", "Rewind.media", "media_rewind.ico", Keys.MediaPreviousTrack),
                new MediaControlItem("FORWARD", "Forward.media", "media_forward.ico", Keys.MediaNextTrack)
            }.ToDictionary(x => x.FullPath);
        }

        public uint getID()
        {
            return id;
        }

        public string getName()
        {
            return Name;
        }

        public void getLabels(List<IInputData> inputDataList)
        {
        }

        public void getResults(List<IInputData> inputDataList, List<ICatItem> resultsList)
        {
        }

        public void getCatalog(List<ICatItem> catalogItems)
        {
            var items = mediaControlItems
                .Values
                .Select(x => catItemFactory.createCatItem(x.FullPath, x.Label, getID(), GetIconPath(x.IconFileName)))
                .ToArray();

            catalogItems.AddRange(items);
        }

        public void launchItem(List<IInputData> inputDataList, ICatItem item)
        {
            ICatItem catItem = inputDataList[inputDataList.Count - 1].getTopResult();

            MediaControlItem mediaItem;
            if (mediaControlItems.TryGetValue(catItem.getFullPath(), out mediaItem))
            {
                SIClass.SendKeyAsInput(mediaItem.KeyPress);
            }
        }

        public bool hasDialog()
        {
            return false;
        }

        public IntPtr doDialog()
        {
            return IntPtr.Zero;
        }

        public void endDialog(bool acceptedByUser)
        {
        }

        public void launchyShow()
        {
        }

        public void launchyHide()
        {
        }

        private string GetIconPath(string iconFileName)
        {
            return Path.Combine(launchyPaths.getIconsPath(), iconFileName);
        }
    }
}
