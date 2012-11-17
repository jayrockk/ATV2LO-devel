using System;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace FTR2LO_Vail
{
    public class AboutSubTabPage : ControlRendererPage
    {
        public AboutSubTabPage()
            : base(new Guid("3db7d5a0-68b0-4bd2-afba-3f0266cff9e0"),
                "About",
                "About FTR2LO") { }

        protected override ControlRendererPageContent CreateContent()
        {
            return ControlRendererPageContent.Create(new AboutControl());
        }
    }
}
