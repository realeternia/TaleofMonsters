namespace ConfigDatas
{
	public class PeopleConfig
	{
		public int Id;
		public string Name;
		public int Type;
		public string World;
		public string[] Deck;
		public int[] Cards;
		public int Sex;
		public int Job;
		public int KingCard;
		public int Rule;
		public int Level;
		public int[] Reward;
		public string Method;
		public string Emethod;
		public string Figue;
		public PeopleConfig(){}
		public PeopleConfig(int Id,string Name,int Type,string World,string[] Deck,int[] Cards,int Sex,int Job,int KingCard,int Rule,int Level,int[] Reward,string Method,string Emethod,string Figue)
		{
			this.Id= Id;
			this.Name= Name;
			this.Type= Type;
			this.World= World;
			this.Deck= Deck;
			this.Cards= Cards;
			this.Sex= Sex;
			this.Job= Job;
			this.KingCard= KingCard;
			this.Rule= Rule;
			this.Level= Level;
			this.Reward= Reward;
			this.Method= Method;
			this.Emethod= Emethod;
			this.Figue= Figue;
		}
	}
}
