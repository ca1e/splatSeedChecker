using splatoon3Tester.data;

namespace splatoon3Tester.type;

public class GearItem
{
    public int offset { get; set; }
    public GearType type { get; set; }
    public int id { get; set; }
    public int gearExp { get; set; }
    public int star { get; set; }
    public int mainAbility { get; set; }
    public int subAmount { get; set; }
    public int sub1Ability { get; set; }
    public int sub2Ability { get; set; }
    public int sub3Ability { get; set; }
    public uint gearSeed { get; set; }

    public GearItem(GearType gtype, Span<byte> data)
    {
        type = gtype;
        id = BitConverter.ToInt32(data[8..12]);
        gearExp = BitConverter.ToInt32(data[80..84]);
        star = BitConverter.ToInt32(data[88..92]);
        mainAbility = BitConverter.ToInt32(data[92..96]) - 1;
        subAmount = BitConverter.ToInt32(data[112..116]);
        sub1Ability = BitConverter.ToInt32(data[116..120]);
        sub2Ability = BitConverter.ToInt32(data[120..124]);
        sub3Ability = BitConverter.ToInt32(data[124..128]);
        gearSeed = BitConverter.ToUInt32(data[136..140]);
    }

    public string GearName => Gear.getName(type, id);

    public string TypeName => type switch
    {
        GearType.HEAD => "头",
        GearType.CLOTHES => "衣",
        GearType.SHOES => "鞋",
        _ => "Unknown",
    };

    public override string ToString() => GearName;
}
