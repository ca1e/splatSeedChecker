using System.Net.Sockets;

namespace splatoon3Tester.core;

public class SocketConnection : IConnection
{
    private string _server {get; init;}
    private int _port {get; init;}
    private NetworkStream socket;

    public SocketConnection(string server, int port = 7331)
    {
        _server = server;
        _port = port;
    }

    private static NetworkStream ConnectSocket(string server, int port)
    {
        Socket Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var result = Connection.BeginConnect(server, port, null, null);

        bool success = result.AsyncWaitHandle.WaitOne(3000, true);
        if (success)
        {
            Connection.EndConnect(result);
        }
        else
        {
            Connection.Close();
            throw new SocketException(10060); // Connection timed out.
        }
        return new NetworkStream(Connection, true);
    }

    public override void connect()
    {
        this.socket = ConnectSocket(_server, _port);
    }

    public override void close()
    {
        flush();
        socket.Close();
    }

    public override bool connected()
    {
        if(socket == null) return false;
        return socket.Socket.Connected;
    }

    public override void flush()
    {
        socket.Flush();
    }

    public override int read(byte[] b, int off, int len)
    {
        int count = socket.Read(b, off, len);
        while (count != len)
            count += socket.Read(b, count, len - count);
        return count;
    }

    public override int readByte()
    {
        return socket.ReadByte();
    }

    public override void write(byte[] data, int off, int len)
    {
        socket.Write(data, off, len);
    }

    public override void writeByte(int i)
    {
        socket.WriteByte((byte)i);
    }
}
