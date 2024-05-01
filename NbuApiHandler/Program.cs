using System.ServiceProcess;

namespace NbuApiHandler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new CurrencyService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
