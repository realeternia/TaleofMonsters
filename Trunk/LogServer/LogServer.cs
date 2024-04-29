using System.Text;
using System.Net.Sockets;
using System.Net;
using NarlonLib.Log;

namespace LogServer
{
    public class LogServer
    {
        private UdpClient _server;  //server ,used for recieve log
        private IPEndPoint _client; //client ,send data
        private byte[] _buffer;     //recieve data buffer

        /// <summary>
        /// 初始化服务器dup环境
        /// </summary>
        public void Run()
        {
            _server = new UdpClient(5501);
            _client = new IPEndPoint(IPAddress.Any, 0);
            NLog.Info("Server Start Ok");

            while (true)
            {
                _buffer = _server.Receive(ref _client);
                string log = Encoding.Default.GetString(_buffer);
                AppendLog(log);
            }
        }

        private void AppendLog(string log)
        {
            if (log.Contains("[ERROR]"))
            {
                NLog.Error(log);
            }
            else if (log.Contains("[FATAL]"))
            {
                NLog.Fatal(log);
            }
            else if (log.Contains("[WARN ]"))
            {
                NLog.Warn(log);
            }
            else if (log.Contains("[DEBUG]"))
            {
                NLog.Debug(log);
            }
            else
            {
                NLog.Info(log);
            }
        }
    }
}
