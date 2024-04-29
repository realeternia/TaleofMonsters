namespace ConfigDatas
{
	public class WeaponConfig
	{
		public int Id;
		public string Name;
		public string Ename;
		public string EnameShort;
		public int Star;
		public int Type;
		public int Attr;
		public int Cost;
		public int Atk;
		public int Def;
		public int MAtk;
		public int MDef;
		public int Hit;
		public int Dhit;
		public int Skl;
		public int Spd;
		public int SkillMark;
		public int Sum;
		public int Modify;
		public int SkillId;
		public int Percent;
		public string Arrow;
		public int Res;
		public string Icon;
		public WeaponConfig(){}
		public WeaponConfig(int Id,string Name,string Ename,string EnameShort,int Star,int Type,int Attr,int Cost,int Atk,int Def,int MAtk,int MDef,int Hit,int Dhit,int Skl,int Spd,int SkillMark,int Sum,int Modify,int SkillId,int Percent,string Arrow,int Res,string Icon)
		{
			this.Id= Id;
			this.Name= Name;
			this.Ename= Ename;
			this.EnameShort= EnameShort;
			this.Star= Star;
			this.Type= Type;
			this.Attr= Attr;
			this.Cost= Cost;
			this.Atk= Atk;
			this.Def= Def;
			this.MAtk= MAtk;
			this.MDef= MDef;
			this.Hit= Hit;
			this.Dhit= Dhit;
			this.Skl= Skl;
			this.Spd= Spd;
			this.SkillMark= SkillMark;
			this.Sum= Sum;
			this.Modify= Modify;
			this.SkillId= SkillId;
			this.Percent= Percent;
			this.Arrow= Arrow;
			this.Res= Res;
			this.Icon= Icon;
		}
	}
}
