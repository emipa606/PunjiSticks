using RimWorld;
using Verse;
using Verse.Sound;

namespace meng_pungi_sticks
{
    // Token: 0x02000003 RID: 3
    internal class Meng_pungi_sticks_anesthetic_Building_TrapDamager : Building_TrapDamager
    {
        // Token: 0x04000001 RID: 1
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.2f, 0.5f);

        // Token: 0x04000002 RID: 2
        private static readonly float DamageCount = 5f;

        // Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
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

        // Token: 0x06000003 RID: 3 RVA: 0x00002108 File Offset: 0x00000308
        private void Randomanesthetic(Pawn hitPawn)
        {
            if (Rand.Value > 0.25f)
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

        // Token: 0x06000004 RID: 4 RVA: 0x000021BC File Offset: 0x000003BC
        protected override void SpringSub(Pawn hitThing)
        {
            if (hitThing == null)
            {
                return;
            }

            SpringSubImpl(hitThing);
            Randomanesthetic(hitThing);
        }

        // Token: 0x02000004 RID: 4
        [DefOf]
        private static class MengRulePackDefOf
        {
            // Token: 0x04000003 RID: 3
            public static RulePackDef DamageEvent_TrapMengPunjiSticks;

            // Token: 0x06000007 RID: 7 RVA: 0x000021F7 File Offset: 0x000003F7
            static MengRulePackDefOf()
            {
                DefOfHelper.EnsureInitializedInCtor(typeof(RulePackDefOf));
            }
        }
    }
}