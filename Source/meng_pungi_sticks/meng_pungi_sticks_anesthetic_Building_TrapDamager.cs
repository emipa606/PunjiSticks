using RimWorld;
using Verse;

namespace meng_pungi_sticks;

internal class Meng_pungi_sticks_anesthetic_Building_TrapDamager : Meng_pungi_sticks_BaseBuilding_TrapDamager
{
    private static readonly FloatRange damageRandomFactorRange = new(0.2f, 0.5f);

    private static void randomAnesthetic(Pawn hitPawn)
    {
        if (hitPawn.RaceProps?.IsFlesh != true || Rand.Value > 0.25f)
        {
            return;
        }

        var anestheticHediffDef = DefDatabase<HediffDef>.GetNamedSilentFail("Hediff_AnestheticBullet");
        if (anestheticHediffDef == null)
        {
            return;
        }

        Messages.Message("meng_punji_sticks_anestheticSuccess".Translate(hitPawn.Label), new LookTargets(hitPawn),
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
            hediff = hediffSet?.GetFirstHediffOfDef(anestheticHediffDef);
        }

        var hediff2 = hediff;
        var num = Rand.Range(0.5f, 0.8f);
        if (hediff2 != null)
        {
            hediff2.Severity += num;
            return;
        }

        var hediff3 = HediffMaker.MakeHediff(anestheticHediffDef, hitPawn);
        hediff3.Severity = num;
        hitPawn.health.AddHediff(hediff3);
    }

    protected override void SpringSub(Pawn hitThing)
    {
        if (hitThing == null)
        {
            return;
        }

        SpringSubImpl(hitThing, damageRandomFactorRange);
        randomAnesthetic(hitThing);
    }
}