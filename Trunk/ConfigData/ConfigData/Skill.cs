namespace ConfigDatas
{
	public class SkillConfig
	{
		public int Id;
		public string Name;
		public string Type;
		public SkillInitialEffectDelegate OnAdd;
		public SkillBurstCheckDelegate CanBurst;
		public SkillHitEffectDelegate CheckHit;
		public SkillDamageEffectDelegate CheckDamage;
		public SkillAfterHitEffectDelegate AfterHit;
		public SkillTimelyEffectDelegate OnRoundUpdate;
		public SkillTimelyEffectDelegate CheckSpecial;
		public int Active;
		public bool IsRandom;
		public FormatStringDelegate GetDescript;
		public string Effect;
		public int Mark;
		public string Icon;
		public SkillConfig(){}
		public SkillConfig(int Id,string Name,string Type,SkillInitialEffectDelegate OnAdd,SkillBurstCheckDelegate CanBurst,SkillHitEffectDelegate CheckHit,SkillDamageEffectDelegate CheckDamage,SkillAfterHitEffectDelegate AfterHit,SkillTimelyEffectDelegate OnRoundUpdate,SkillTimelyEffectDelegate CheckSpecial,int Active,bool IsRandom,FormatStringDelegate GetDescript,string Effect,int Mark,string Icon)
		{
			this.Id= Id;
			this.Name= Name;
			this.Type= Type;
			this.OnAdd= OnAdd;
			this.CanBurst= CanBurst;
			this.CheckHit= CheckHit;
			this.CheckDamage= CheckDamage;
			this.AfterHit= AfterHit;
			this.OnRoundUpdate= OnRoundUpdate;
			this.CheckSpecial= CheckSpecial;
			this.Active= Active;
			this.IsRandom= IsRandom;
			this.GetDescript= GetDescript;
			this.Effect= Effect;
			this.Mark= Mark;
			this.Icon= Icon;
		}
	}
}
