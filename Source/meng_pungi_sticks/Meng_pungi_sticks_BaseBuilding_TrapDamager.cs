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
            var damageResult = hitPawn.TakeDamage(new DamageInfo(DamageDefOf.Stab, num, armorPenetration, -1f, this));
            if (num2 == 0)
            {
                var battleLogEntryDamageTaken =
                    new BattleLogEntry_DamageTaken(hitPawn, MengRulePackDefOf.DamageEvent_TrapMengPunjiSticks);
                Find.BattleLog.Add(battleLogEntryDamageTaken);
                damageResult.AssociateWithLog(battleLogEntryDamageTaken);
            }

            num2++;
        }
    }
}