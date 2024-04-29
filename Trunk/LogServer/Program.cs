using NarlonLib.Log;

namespace LogServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NLog.StartServer(LogTargets.ServerConsole | LogTargets.File);
            LogServer logServer = new LogServer();
            logServer.Run();
        }
    }
}
