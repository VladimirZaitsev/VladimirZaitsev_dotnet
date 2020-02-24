using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Lab1.Logger
{
    public static class Log4Net
    {
        private static ILog log => LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
    }
}