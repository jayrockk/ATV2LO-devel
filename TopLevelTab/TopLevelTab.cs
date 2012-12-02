using System;
using System.Drawing;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace FTR2LO_Vail
{
    [ContainsCustomControl]
    public class TopLevelTabPageProvider : PageProvider
    {
        public TopLevelTabPageProvider()
            : base(new Guid("CF68B457-C20E-4311-BF1D-8875B51ED2A4"),
                   "ATV2LO",
                   "Argus TV to Lights-Out")
        {
            TopLevelTabHelpers.CheckForNewVersion();
        }

        protected override Icon CreateImage()
        {
            return FTR2LO_Vail.Properties.Resources.icon_ATV2LO;
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