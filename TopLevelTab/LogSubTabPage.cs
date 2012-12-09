using System;
using Microsoft.WindowsServerSolutions.Administration.ObjectModel;

namespace ATV2LO_TopLevelTab
{
    public class LogSubTabPage : ControlRendererPage
    {
        public LogSubTabPage()
            : base(new Guid("c38255dd-2c01-4d22-b4f7-d496f4d5036a"),
                "Log",
                "Log info") { }

        protected override ControlRendererPageContent CreateContent()
        {
            return ControlRendererPageContent.Create(new LogControl());
        }
    }
}
