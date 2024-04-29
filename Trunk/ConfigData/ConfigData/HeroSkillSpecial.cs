namespace ConfigDatas
{
	public class HeroSkillSpecialConfig
	{
		public int Id;
		public int Former;
		public int Catalog;
		public int Job;
		public int Group;
		public int Level;
		public int HeroLevel;
		public int GoldCost;
		public int[] SkillNeed;
		public int SkillPointCost;
		public int Slot;
		public int SkillId;
		public int Rate;
		public HeroSkillSpecialConfig(){}
		public HeroSkillSpecialConfig(int Id,int Former,int Catalog,int Job,int Group,int Level,int HeroLevel,int GoldCost,int[] SkillNeed,int SkillPointCost,int Slot,int SkillId,int Rate)
		{
			this.Id= Id;
			this.Former= Former;
			this.Catalog= Catalog;
			this.Job= Job;
			this.Group= Group;
			this.Level= Level;
			this.HeroLevel= HeroLevel;
			this.GoldCost= GoldCost;
			this.SkillNeed= SkillNeed;
			this.SkillPointCost= SkillPointCost;
			this.Slot= Slot;
			this.SkillId= SkillId;
			this.Rate= Rate;
		}
	}
}
