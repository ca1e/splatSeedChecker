using splatoon3Tester.core;
using splatoon3Tester.data;
using splatoon3Tester.type;

namespace splatoon3Tester.editor;

public partial class SplatEditor
{
    private Debugger debugger;
    private bool connected = false;

    private long baseHeap1Addr = -1;
    private long pid = -1;
    private static readonly long TID = 0x0100C2500FC20000L;

    public ulong BASEHEAP_ADDR => (ulong)baseHeap1Addr;

    public SplatEditor() { }

    public void connect(string ip, int port = 7331)
    {
        debugger = debugger = new Debugger(new SocketConnection(ip, port));
        debugger.connect();
        connected = true;
    }

    public void init()
    {
        if (!connected)
        {
            throw new Exception("not connected");
        }
        pid = debugger.getPids().Reverse().Take(5).Where(pid => TID == debugger.getTitleId(pid)).FirstOrDefault(-1);
        if (pid == -1)
        {
            debugger.close();
            throw new Exception("not splatoon3");
        }
        debugger.attach(pid);
        debugger.resume();
        baseHeap1Addr = getbase();
        debugger.detach();
    }

    private long getbase()
    {
    }

    public void ATTACH()
    {
        if (!connected)
        {
            throw new Exception("not connected");
        }
        if (baseHeap1Addr == -1 || pid == -1)
        {
            debugger?.close();
            throw new Exception("reconnect please");
        }
        debugger?.attach(pid);
    }

    public void DETTACH()
    {
        debugger?.detach();
    }

    public void close()
    {
        if (debugger != null && debugger.connected())
        {
            debugger?.close();
        }
    }

    public ICollection<GearItem> getGears(int type)
    {
        ATTACH();
        if (type < 0 || type > 2)
        {
            throw new Exception($"invalid gear type: {type}, (0,1,2)");
        }

        var baseAddr = baseHeap1Addr + Offsets.gearHeadBaseAddr;
        baseAddr += type * Offsets.gearTypeSize;
        byte[] candidateBytes = new byte[Offsets.gearBlockSize * Offsets.maxGearAmount];
        byte[]? searchData = debugger.readmem(baseAddr, Offsets.gearBlockSize * Offsets.maxGearAmount, candidateBytes);

        DETTACH();
        if (searchData == null)
        {
            throw new Exception("error: failed to search");
        }
        int pointer = 0;
        GearType gtype = (GearType)type;
        List<GearItem> results = new();
        for (int i = 0; i < Offsets.maxGearAmount; i++)
        {
            var item = new GearItem(gtype, searchData.AsSpan()[pointer..(pointer + 0xB0)]);
            if (item.id == 0) continue;
            item.offset = pointer;
            results.Add(item);

            pointer += Offsets.gearBlockSize;
        }
        return results;
    }

    public void setGear(GearItem gear)
    {
        ATTACH();
#if DEBUG
        var baseAddr = baseHeap1Addr + Offsets.gearHeadBaseAddr;
        baseAddr += (int)gear.type * Offsets.gearTypeSize;
        debugger.poke32(baseAddr, gear.gearSeed);
#endif
        DETTACH();
    }
}