using RimWorld;
using Verse;
using Verse.Sound;

namespace meng_pungi_sticks;

internal class Meng_pungi_sticks_poison_Building_TrapDamager : Building_TrapDamager
{
    private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

    private static readonly float DamageCount = 5f;

    private void SpringSubImpl(Pawn hitPawn)
    {
        SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(Position, Map));
        var num = this.GetStatValue(StatDefOf.TrapMeleeDamage) * DamageRandomFactorRange.RandomInRange /
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

    private void RandomPoison(Pawn hitPawn)
    {
        if (Rand.Value > 0.25f)
        {
            return;
        }

        Messages.Message("meng_punji_sticks_PoisonSuccess".Translate(hitPawn.Label), new LookTargets(hitPawn),
            MessageTypeDefOf.PositiveEvent, false);
        Hediff hediff;
        var health = hitPawn.health;
        if (health == null)
        {
            hediff = null;
        }
        else
        {
            var hediffSet = health.hediffSet;
            hediff = hediffSet?.GetFirstHediffOfDef(HediffDefOf.ToxicBuildup);
        }

        var hediff2 = hediff;
        var num = Rand.Range(0.1f, 0.2f);
        if (hediff2 != null)
        {
            hediff2.Severity += num;
            return;
        }

        var hediff3 = HediffMaker.MakeHediff(HediffDefOf.ToxicBuildup, hitPawn);
        hediff3.Severity = num;
        hitPawn.health.AddHediff(hediff3);
    }

    protected override void SpringSub(Pawn hitThing)
    {
        if (hitThing == null)
        {
            return;
        }

        SpringSubImpl(hitThing);
        RandomPoison(hitThing);
    }

    [DefOf]
    private static class MengRulePackDefOf
    {
        public static RulePackDef DamageEvent_TrapMengPunjiSticks;

        static MengRulePackDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RulePackDefOf));
        }
    }
}