using System;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace ATV2LO_TopLevelTab
{
    public class AboutSubTabPage : ControlRendererPage
    {
        public AboutSubTabPage()
            : base(new Guid("a6c586b2-7f60-49f8-aa78-67e5bea26e25"),
                "About",
                "About Argus TV to Lights-Out") { }

        protected override ControlRendererPageContent CreateContent()
        {
            return ControlRendererPageContent.Create(new AboutControl());
        }
    }
}
