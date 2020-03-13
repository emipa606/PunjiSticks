using RimWorld;
using Verse;
using Verse.Sound;

namespace meng_pungi_sticks
{
    // Token: 0x02000003 RID: 3
    internal class meng_pungi_sticks_Building_TrapDamager : Building_TrapDamager
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		private void SpringSubImpl(Pawn hitPawn)
		{
			SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(base.Position, base.Map, false));
			float num = this.GetStatValue(StatDefOf.TrapMeleeDamage, true) * meng_pungi_sticks_Building_TrapDamager.DamageRandomFactorRange.RandomInRange / meng_pungi_sticks_Building_TrapDamager.DamageCount;
			float armorPenetration = num * 0.015f;
			int num2 = 0;
			while ((float)num2 < meng_pungi_sticks_Building_TrapDamager.DamageCount)
			{
				DamageInfo dinfo = new DamageInfo(DamageDefOf.Stab, num, armorPenetration, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
				DamageWorker.DamageResult damageResult = hitPawn.TakeDamage(dinfo);
				if (num2 == 0)
				{
					BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(hitPawn, meng_pungi_sticks_Building_TrapDamager.MengRulePackDefOf.DamageEvent_TrapMengPunjiSticks, null);
					Find.BattleLog.Add(battleLogEntry_DamageTaken);
					damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
				}
				num2++;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002108 File Offset: 0x00000308
		private void RandomPlague(Pawn hitPawn)
		{
			if (Rand.Value <= 0.25f)
			{
				Messages.Message(TranslatorFormattedStringExtensions.Translate("meng_punji_sticks_PlagueSuccess", hitPawn.Label), MessageTypeDefOf.PositiveEvent, false);
				Hediff hediff;
				if (hitPawn == null)
				{
					hediff = null;
				}
				else
				{
					Pawn_HealthTracker health = hitPawn.health;
					if (health == null)
					{
						hediff = null;
					}
					else
					{
						HediffSet hediffSet = health.hediffSet;
						hediff = ((hediffSet != null) ? hediffSet.GetFirstHediffOfDef(HediffDefOf.Plague, false) : null);
					}
				}
				Hediff hediff2 = hediff;
				float num = Rand.Range(0.1f, 0.2f);
				if (hediff2 != null)
				{
					hediff2.Severity += num;
					return;
				}
				Hediff hediff3 = HediffMaker.MakeHediff(HediffDefOf.Plague, hitPawn, null);
				hediff3.Severity = num;
				hitPawn.health.AddHediff(hediff3, null, null, null);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021BC File Offset: 0x000003BC
		protected override void SpringSub(Pawn hitThing)
		{
			if (hitThing != null)
			{
				this.SpringSubImpl(hitThing);
				this.RandomPlague(hitThing);
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

		// Token: 0x04000002 RID: 2
		private static readonly float DamageCount = 5f;

		// Token: 0x02000004 RID: 4
		[DefOf]
		private static class MengRulePackDefOf
		{
			// Token: 0x06000007 RID: 7 RVA: 0x000021F7 File Offset: 0x000003F7
			static MengRulePackDefOf()
			{
				DefOfHelper.EnsureInitializedInCtor(typeof(RulePackDef));
			}

			// Token: 0x04000003 RID: 3
			public static RulePackDef DamageEvent_TrapMengPunjiSticks;
		}
	}
}
