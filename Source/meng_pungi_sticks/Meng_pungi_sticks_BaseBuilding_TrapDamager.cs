using RimWorld;
using Verse;
using Verse.Sound;

namespace meng_pungi_sticks;

internal class Meng_pungi_sticks_BaseBuilding_TrapDamager : Building_TrapDamager
{
    private static readonly float DamageCount = 5f;

    protected void SpringSubImpl(Pawn hitPawn, FloatRange damageRange)
    {
        SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(Position, Map));
        var num = this.GetStatValue(StatDefOf.TrapMeleeDamage) * damageRange.RandomInRange /
                  DamageCount;
        var armorPenetration = num * 0.015f;
        var num2 = 0;
        while (num2 < DamageCount)
        {
            var dinfo = new DamageInfo(DamageDefOf.Stab, num, armorPenetration, -1f, this);
            var damageResult = hitPawn.TakeDamage(dinfo);
            if (num2 == 0)
            {
                var battleLogEntry_DamageTaken =
                    new BattleLogEntry_DamageTaken(hitPawn, MengRulePackDefOf.DamageEvent_TrapMengPunjiSticks);
                Find.BattleLog.Add(battleLogEntry_DamageTaken);
                damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
            }

            num2++;
        }
    }
}