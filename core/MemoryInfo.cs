namespace splatoon3Tester.core;

public class MemoryInfo
{
    private long addr; //64
    private long size; //64
    private MemoryType type; //32
    private int perm; //32

    public MemoryInfo(long addr, long size, int type, int perm)
    {
        this.addr = addr;
        this.size = size;
        this.type = (MemoryType)(type);
        this.perm = perm;
    }

    public long getAddress()
    {
        return addr;
    }

    public long getSize()
    {
        return size;
    }

    public long getNextAddress()
    {
        return addr + size;
    }

    public MemoryType getType()
    {
        return type;
    }
}
