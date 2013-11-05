using System;
using System.Linq;
using System.Windows;
using DanilosBackUp.Utils;

namespace DanilosBackUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

            protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
            {
                // Always call method in base class, so that the event gets raised.
                base.OnSessionEnding(e);

                // Place your own SessionEnding logic here
                BackUpApi.BackUpProcessVerify();
            }

    }
}
