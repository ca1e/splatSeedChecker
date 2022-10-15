namespace splatoon3Tester.data;

public class AbilityDex
{
    private static readonly string[] ability = {
        "提升墨汁效率(主)",
        "提升墨汁效率(次)",
        "提升墨汁回复力",
        "提升人类移动速度",
        "提升鱿鱼冲刺速度",
        "提升特殊武器增加量",
        "降低特殊武器减少量",
        "提升特殊武器性能",
        "缩短复活时间",
        "缩短超级跳跃时间",
        "提升次要武器性能",
        "减轻对手墨汁影响",
        "减轻次要武器影响",
        "行动强化",
    };
    private static readonly string[] Mability =
    {
        "最初冲刺",
        "最后冲刺",
        "逆境强化",
        "回归",
        "鱿鱼忍者",
        "复仇",
        "热力墨汁",
        "复活惩罚",
        "能力增倍",
        "隐身跳跃",
        "提升对物体攻击力",
        "受身术",
    };
    private static readonly AbilityDex _instance = new();

    public static string[] Abilities() => ability;
    public static string[] MAbilities() => Mability;

    public string this[int index]
    {
        get
        {
            if(index > 0xD)
            {
                if(index > 0x19)
                {
                    return $"Unknown {index}";
                }
                return Mability[index];
            }
            return ability[index];
        }
    }

    public string AmountName(int index)
    {
        return _instance[index];
    }
}