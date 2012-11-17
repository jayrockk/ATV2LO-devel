using System;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace FTR2LO_Vail
{
    public class LogSubTabPage : ControlRendererPage
    {
        public LogSubTabPage()
            : base(new Guid("DA565AC2-D4F8-401A-B19E-DCF06DD441DC"),
                "Log",
                "Log info") { }

        protected override ControlRendererPageContent CreateContent()
        {
            return ControlRendererPageContent.Create(new LogControl());
        }
    }
}
