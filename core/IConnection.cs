namespace splatoon3Tester.core;

public abstract class IConnection
{
    public abstract void connect();
    public abstract bool connected();

    public void writeCommand(int cmd)
    {
        writeByte(cmd);
    }

    public void write(byte[] b)
    {
        write(b, 0, b.Length);
    }

    public abstract void write(byte[] data, int off, int len);

    public void writeLong(long l)
    {
        write(BitConverter.GetBytes(l));
    }

    public void writeInt(int i)
    {
        write(BitConverter.GetBytes(i));
    }

    public void writeShort(int s)
    {
        byte[] data = { (byte)(s & 0xFF), (byte)((s >> 8) & 0xFF) };
        write(data);
    }

    public abstract void writeByte(int i);

    public Result readResult()
    {
        return Result.valueOf(readInt());
    }

    public abstract int readByte();

    public long readLong()
    {
        byte[] result = new byte[8];
        read(result);
        return BitConverter.ToInt64(result, 0);
    }

    public uint readUInt()
    {
        byte[] result = new byte[4];
        read(result);
        return BitConverter.ToUInt32(result, 0);
    }

    public int readInt()
    {
        byte[] result = new byte[4];
        read(result);
        return BitConverter.ToInt32(result, 0);
    }

    public int readUShort()
    {
        byte[] result = new byte[2];
        read(result);
        return BitConverter.ToInt16(result, 0);
    }

    public short readShort()
    {
        byte[] data = new byte[2];
        int len = readFully(data);
        if (len != 2)
        {
            throw new ConnectionException("Unable to fully read data. Expected 2 bytes, but we only read:" + len);
        }
        return (short)((data[0] & 0xFF) | (data[1] & 0xFF) << 8);
    }

    public int read(byte[] b)
    {
        return read(b, 0, b.Length);
    }

    public abstract int read(byte[] b, int off, int len);

    public int readFully(byte[] b)
    {
        return readFully(b, 0, b.Length);
    }

    public int readFully(byte[] b, int off, int len)
    {
        int tmp = len;
        while (tmp > 0)
        {
            int i = read(b, off, tmp);
            if (i == -1)
            {
                break;
            }
            off += i;
            tmp -= i;
        }
        return len - tmp;
    }
    public abstract void flush();

    public abstract void close();
}
