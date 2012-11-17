using System;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace FTR2LO_Vail
{
    public class StatusSubTabPage : ControlRendererPage
    {
        public StatusSubTabPage()
            : base(new Guid("3db7d5a0-68b0-4bd2-afba-3f0266cff9e9"),
                "Overview",
                "Status summary and configuration of FTR2LO") { }

        protected override ControlRendererPageContent CreateContent()
        {
            return ControlRendererPageContent.Create(new MyControl());
        }
    }
}
