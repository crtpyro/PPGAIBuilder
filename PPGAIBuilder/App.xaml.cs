using System;
using System.Windows;

namespace PPGAIBuilder
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // DI will be set up in MainWindow
        }
    }
}
