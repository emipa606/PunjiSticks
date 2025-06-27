using RimWorld;
using Verse;

namespace meng_pungi_sticks;

internal class Meng_pungi_sticks_Building_TrapDamager : Meng_pungi_sticks_BaseBuilding_TrapDamager
{
    private static readonly FloatRange damageRandomFactorRange = new(0.8f, 1.2f);

    private static void randomPlague(Pawn hitPawn)
    {
        if (hitPawn.RaceProps?.IsFlesh != true || Rand.Value > 0.25f)
        {
            return;
        }

        Messages.Message("meng_punji_sticks_PlagueSuccess".Translate(hitPawn.Label), new LookTargets(hitPawn),
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
            hediff = hediffSet?.GetFirstHediffOfDef(HediffDefOf.Plague);
        }

        var hediff2 = hediff;
        var num = Rand.Range(0.1f, 0.2f);
        if (hediff2 != null)
        {
            hediff2.Severity += num;
            return;
        }

        var hediff3 = HediffMaker.MakeHediff(HediffDefOf.Plague, hitPawn);
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
        randomPlague(hitThing);
    }
}