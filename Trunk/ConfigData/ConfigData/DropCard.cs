namespace ConfigDatas
{
	public class DropCardConfig
	{
		public int Id;
		public int Pid;
		public int CardId;
		public int Rate;
		public DropCardConfig(){}
		public DropCardConfig(int Id,int Pid,int CardId,int Rate)
		{
			this.Id= Id;
			this.Pid= Pid;
			this.CardId= CardId;
			this.Rate= Rate;
		}
	}
}
