using System;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace ATV2LO_TopLevelTab
{
    public class StatusSubTabPage : ControlRendererPage
    {
        public StatusSubTabPage()
            : base(new Guid("820ee168-ade4-470d-a3ba-a8a7f568a6e8"),
                "Overview",
                "Status summary and configuration of ATV2LO") { }

        protected override ControlRendererPageContent CreateContent()
        {
            return ControlRendererPageContent.Create(new MyControl());
        }
    }
}
