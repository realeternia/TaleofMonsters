namespace ConfigDatas
{
	public class SpellConfig
	{
		public int Id;
		public string Name;
		public string Ename;
		public string EnameShort;
		public int Star;
		public int Type;
		public int Attr;
		public int Cost;
		public int Special;
		public int Range;
		public string Target;
		public SpellEffectDelegate Effect;
		public FormatStringDelegate GetDescript;
		public string UnitEffect;
		public string AreaEffect;
		public int Res;
		public string Icon;
		public SpellConfig(){}
		public SpellConfig(int Id,string Name,string Ename,string EnameShort,int Star,int Type,int Attr,int Cost,int Special,int Range,string Target,SpellEffectDelegate Effect,FormatStringDelegate GetDescript,string UnitEffect,string AreaEffect,int Res,string Icon)
		{
			this.Id= Id;
			this.Name= Name;
			this.Ename= Ename;
			this.EnameShort= EnameShort;
			this.Star= Star;
			this.Type= Type;
			this.Attr= Attr;
			this.Cost= Cost;
			this.Special= Special;
			this.Range= Range;
			this.Target= Target;
			this.Effect= Effect;
			this.GetDescript= GetDescript;
			this.UnitEffect= UnitEffect;
			this.AreaEffect= AreaEffect;
			this.Res= Res;
			this.Icon= Icon;
		}
	}
}
