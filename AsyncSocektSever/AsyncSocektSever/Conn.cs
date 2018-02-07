using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocektSever
{
    class Conn
    {
        public const int BUFFER_SIZE = 1024;
        public Socket socket;
        public bool isUse = false;
        //缓冲区
        public byte[] readbuffer = null;
        public int buffCount = 0;
        public Conn()
        {
            readbuffer = new byte[BUFFER_SIZE];
        }
        public void Init(Socket socket)
        {
            this.socket = socket;
            isUse = true;
            buffCount = 0;
        }
        //缓冲区剩余的字节数
        public int Remain()
        {
            return BUFFER_SIZE - buffCount;
        }
        public string GetAddress()
        {
            if (!isUse)
            {
                return "无法获取地址";
            }
            return socket.RemoteEndPoint.ToString();
        }
        public void Close()
        {
            if (!isUse)
            {
                return;
            }
            Console.WriteLine("断开连接" + GetAddress());
            socket.Close();
            isUse = false;
        }
    }
}
