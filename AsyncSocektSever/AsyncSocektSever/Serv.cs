using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace AsyncSocektSever
{
    class Serv
    {
        public Socket listenfd;
        public Conn[] conns;
        public int maxConn = 50;
        //获取连接池空余位置的下标
        public int NewIndex()
        {
            if (conns == null)
            {
                return -1;
            }
            for(int i = 0; i < conns.Length; i++)
            {
                if (conns[i] == null)
                {
                    conns[i] = new Conn();
                    return i;
                }
                else if (conns[i].isUse == false)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Start(string host,int port)
        {
            //初始化
            conns = new Conn[maxConn];
            for(int i = 0; i < conns.Length; i++)
            {
                conns[i] = new Conn();
            }
            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAdr = IPAddress.Parse(host);
            IPEndPoint ipEp = new IPEndPoint(ipAdr, port);
            listenfd.Bind(ipEp);
            listenfd.Listen(maxConn);
            listenfd.BeginAccept(AcceptCb,null);
            Console.WriteLine("服务器已启动...");
        }
        private void AcceptCb(IAsyncResult ar)
        {
            try
            {
                //程序停在这里直到有client连接
                Socket socket=listenfd.EndAccept(ar);
                int index = NewIndex();
                if (index <0)
                {
                    socket.Close();
                    Console.WriteLine("连接池已满...");
                }
                else
                {
                    Conn conn = conns[index];
                    conn.Init(socket);
                    string adr = conn.GetAddress();
                    Console.WriteLine("客户端连接...["+adr+"],连接池Id:"+index);
                    conn.socket.BeginReceive(conn.readbuffer, conn.buffCount, conn.Remain(), SocketFlags.None, ReceiveCb, conn);
                    listenfd.BeginAccept(AcceptCb, null);//实现接收循环
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Accepte失败");
            }
        }
        private void ReceiveCb(IAsyncResult result)
        {

        }
    }
}
