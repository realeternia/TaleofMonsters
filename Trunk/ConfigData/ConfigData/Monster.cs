namespace ConfigDatas
{
	public class MonsterConfig
	{
		public int Id;
		public string Name;
		public string Ename;
		public string EnameShort;
		public int Star;
		public int Race;
		public int Type;
		public int Cost;
		public int AtkP;
		public int DefP;
		public int MAtkP;
		public int MDef;
		public int HitP;
		public int DhitP;
		public int SklP;
		public int SpdP;
		public int VitP;
		public int Sum;
		public int Modify;
		public int Tile;
		public string Arrow;
		public string Cover;
		public RLVector3List Skills;
		public int Smark;
		public int Res;
		public int Icon;
		public MonsterConfig(){}
		public MonsterConfig(int Id,string Name,string Ename,string EnameShort,int Star,int Race,int Type,int Cost,int AtkP,int DefP,int MAtkP,int MDef,int HitP,int DhitP,int SklP,int SpdP,int VitP,int Sum,int Modify,int Tile,string Arrow,string Cover,RLVector3List Skills,int Smark,int Res,int Icon)
		{
			this.Id= Id;
			this.Name= Name;
			this.Ename= Ename;
			this.EnameShort= EnameShort;
			this.Star= Star;
			this.Race= Race;
			this.Type= Type;
			this.Cost= Cost;
			this.AtkP= AtkP;
			this.DefP= DefP;
			this.MAtkP= MAtkP;
			this.MDef= MDef;
			this.HitP= HitP;
			this.DhitP= DhitP;
			this.SklP= SklP;
			this.SpdP= SpdP;
			this.VitP= VitP;
			this.Sum= Sum;
			this.Modify= Modify;
			this.Tile= Tile;
			this.Arrow= Arrow;
			this.Cover= Cover;
			this.Skills= Skills;
			this.Smark= Smark;
			this.Res= Res;
			this.Icon= Icon;
		}
	}
}
