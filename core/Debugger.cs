using static splatoon3Tester.core.Commands;

namespace splatoon3Tester.core;

public class Debugger
{
    public static readonly int CURRENT_PROTOCOL_VERSION = (VersionConstants.VERSION_MAJOR << 16) | (VersionConstants.VERSION_MINOR) << 8;
    private IConnection conn;
    private MemoryInfo prev;
    private Semaphore semaphore = new Semaphore(1, 1);
    private int protocolVersion;
    private byte[] peekBuffer = new byte[8];
    private byte[] compressedBuffer = new byte[2048 * 4 * 6];

    public Debugger(IConnection conn)
    {
        this.conn = conn;
    }

    public void connect()
    {
        this.conn.connect();
    }

    public IConnection raw()
    {
        return conn;
    }

    private void acquire()
    {
        try
        {
            semaphore.WaitOne();
        }
        catch (Exception e)
        {
            throw new ConnectionException(e.Message);
        }
    }
    private void release()
    {
        semaphore.Release();
    }

    public void poke8(long addr, int value)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_POKE8);
            conn.writeLong(addr);
            conn.writeByte(value);
            conn.flush();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                throw new ConnectionException(rc);
            }
        }
        finally
        {
            release();
        }
    }

    public int peek8(long addr)
    {
        return BitConverter.ToInt32(readmem(addr, 1, peekBuffer)) & 0xFF;
    }

    public void poke16(long addr, int value)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_POKE16);
            conn.writeLong(addr);
            conn.writeShort(value);
            conn.flush();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                throw new ConnectionException(rc);
            }
        }
        finally
        {
            release();
        }
    }

    public int peek16(long addr)
    {
        return BitConverter.ToInt32(readmem(addr, 2, peekBuffer)) & 0xFFFF;
    }

    public void poke32(long addr, uint value)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_POKE32);
            conn.writeLong(addr);
            conn.writeInt((int)value);
            conn.flush();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                throw new ConnectionException(rc);
            }
        }
        finally
        {
            release();
        }
    }

    public int peek32(long addr)
    {
        return BitConverter.ToInt32(readmem(addr, 4, peekBuffer));
    }

    public void poke64(long addr, long value)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_POKE32);
            conn.writeLong(addr);
            conn.writeLong(value);
            conn.flush();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                throw new ConnectionException(rc);
            }
        }
        finally
        {
            release();
        }
    }

    public long peek64(long addr)
    {
        return BitConverter.ToInt64(readmem(addr, 8, peekBuffer));
    }



    public Result writemem(byte[] data, long addr)
    {
        return writemem(data, 0, data.Length, addr);
    }

    public Result writemem(byte[] data, int off, int len, long addr)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_WRITE);
            conn.writeLong(addr);
            conn.writeInt(len);
            conn.flush();
            Result r = conn.readResult();
            if (r.succeeded())
            {
                conn.write(data, off, len);
                conn.flush();
            }
            else
            {
                conn.readResult();
                return r;
            }
            return conn.readResult();
        }
        finally
        {
            release();
        }
    }

    public byte[] readmem(long addr, int size, byte[] bytes)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_READ);
            conn.writeLong(addr);
            conn.writeInt(size);
            conn.flush();
            Result rc = conn.readResult();

            if (rc.failed())
            {
                conn.readResult(); // ignored
                throw new ConnectionException(rc);
            }

            if (bytes == null)
            {
                bytes = new byte[size];
            }

            int pos = 0;
            byte[] buffer = new byte[2048 * 4];
            while (pos < size)
            {
                rc = conn.readResult();
                if (rc.failed())
                {
                    conn.readResult();
                    throw new ConnectionException(rc);
                }
                int len = readCompressed(buffer);
                Array.Copy(buffer, 0, bytes, pos, len);
                pos += len;
            }
            conn.readResult(); // ignored
            return bytes;
        }
        finally
        {
            release();
        }
    }

    public Result resume()
    {
        return getResult((int)COMMAND_CONTINUE);
    }

    public Result pause()
    {
        return getResult((int)COMMAND_PAUSE);
    }

    public Result attach(long pid)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_ATTACH);
            conn.writeLong(pid);
            conn.flush();
            return conn.readResult();
        }
        finally
        {
            release();
        }
    }

    public Result detach()
    {
        return getResult((int)COMMAND_DETATCH);
    }

    public MemoryInfo query(long address)
    {
        acquire();
        try
        {
            if (prev != null && prev.getAddress() != 0 && address >= prev.getAddress() && address < prev.getNextAddress())
            {
                return prev;
            }

            conn.writeCommand((int)COMMAND_QUERY_MEMORY);
            conn.writeLong(address);
            conn.flush();
            return prev = readInfo();
        }
        finally
        {
            release();
        }
    }

    public IReadOnlyCollection<MemoryInfo> query(long start, int max)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_QUERY_MEMORY_MULTI);
            conn.writeLong(start);
            conn.writeInt(max);
            conn.flush();
            var res = new MemoryInfo[max];
            int count;
            for (count = 0; count < max; count++)
            {
                MemoryInfo info = readInfo();
                
                res[count] = info;
                if (info.getType() == MemoryType.RESERVED)
                {
                    break;
                }
            }
            conn.readResult(); // ignored here, it gets checked in readInfo()
            return new ArraySegment<MemoryInfo>(res, 0, count);
        }
        finally
        {
            release();
        }
    }

    public long getCurrentPid()
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_CURRENT_PID);
            conn.flush();
            long pid = conn.readLong();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                pid = 0;
            }
            return pid;
        }
        finally
        {
            release();
        }
    }

    public long getAttachedPid()
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_GET_ATTACHED_PID);
            conn.flush();
            long pid = conn.readLong();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                throw new ConnectionException("This is impossible, so you've done something terribly wrong", rc);
            }
            return pid;
        }
        finally
        {
            release();
        }
    }

    public long[] getPids()
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_GET_PIDS);
            conn.flush();
            int count = conn.readInt();
            long[] pids = new long[count];
            for (int i = 0; i < count; i++)
            {
                pids[i] = conn.readLong();
            }
            Result rc = conn.readResult();
            if (rc.failed())
            {
                throw new ConnectionException(rc);
            }
            return pids;
        }
        finally
        {
            release();
        }
    }

    public long getTitleId(long pid)
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_GET_TITLEID);
            conn.writeLong(pid);
            conn.flush();
            long tid = conn.readLong();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                //TODO throw? idk
            }
            return tid;
        }
        finally
        {
            release();
        }
    }

    private void disconnect()
    {
        acquire();
        try
        {
            conn.writeCommand((int)COMMAND_DISCONNECT);
            conn.flush();
            Result rc = conn.readResult();
            if (rc.failed())
            {
                throw new ConnectionException("This is impossible, so you've done something terribly wrong", rc);
            }
        }
        finally
        {
            release();
        }
    }

    public long getCurrentTitleId()
    {
        long pid = getCurrentPid();
        if (pid == 0)
        {
            return 0;
        }
        return getTitleId(pid);
    }

    public bool attached()
    {
        return getAttachedPid() != 0;
    }

    public bool connected()
    {
        return conn.connected();
    }


    public void close()
    {
        if (connected()) {
            detach();
            disconnect();
        }
        conn.close();
    }

    private int readCompressed(byte[] buffer)
    {
        int compressedFlag = conn.readByte();
        int decompressedLen = conn.readInt();

        if (compressedFlag == 0)
        {
            conn.readFully(buffer, 0, decompressedLen);
        }
        else
        {
            int compressedLen = conn.readInt();
            conn.readFully(compressedBuffer, 0, compressedLen);
            int pos = 0;
            for (int i = 0; i < compressedLen; i += 2)
            {
                byte value = compressedBuffer[i];
                int count = compressedBuffer[i + 1] & 0xFF;
                for (int j = pos; j < pos + count; j++)
                {
                    buffer[j] = value;
                }
                pos += count;
            }
        }
        return decompressedLen;
    }

    private MemoryInfo readInfo()
    {
        long addr = conn.readLong();
        long size = conn.readLong();
        int type = conn.readInt();
        int perm = conn.readInt();
        Result rc = conn.readResult();
        if (rc.failed())
        {
            throw new ConnectionException(rc);
        }
        return new MemoryInfo(addr, size, type, perm);
    }

    private Result getResult(int cmd)
    {
        acquire();
        try
        {
            conn.writeCommand(cmd);
            conn.flush();
            return conn.readResult();
        }
        finally
        {
            release();
        }
    }
}
