using System;
using System.Drawing;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace ATV2LO_TopLevelTab
{
    [ContainsCustomControl]
    public class TopLevelTabPageProvider : PageProvider
    {
        public TopLevelTabPageProvider()
            : base(new Guid("d2c5f7c6-395f-419f-8c77-0c73f37b354c"),
                   "ATV2LO",
                   "Argus TV to Lights-Out")
        {
            TopLevelTabHelpers.CheckForNewVersion();
        }

        protected override Icon CreateImage()
        {
            return ATV2LO_TopLevelTab.Properties.Resources.icon_ATV2LO;
        }

        protected override object CreatePages()
        {
            return (new object[] {
                new StatusSubTabPage(), 
                new LogSubTabPage(),
                new AboutSubTabPage()
            });
        }
    }
}