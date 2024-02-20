using RimWorld;
using Verse;

namespace meng_pungi_sticks;

[DefOf]
public static class MengRulePackDefOf
{
    public static RulePackDef DamageEvent_TrapMengPunjiSticks;

    static MengRulePackDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(RulePackDefOf));
    }
}