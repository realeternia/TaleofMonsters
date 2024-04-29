namespace ConfigDatas
{
	public class buffConfig
	{
		public int Id;
		public string Name;
		public string Type;
		public BuffEffectDelegate OnAdd;
		public BuffEffectDelegate OnRemove;
		public int SubType;
		public int Val1;
		public int Val2;
		public int[] Other;
		public int[] Effect;
		public bool EndOnHit;
		public int[] Pot;
		public FormatStringDelegate Des;
		public int Immune;
		public string Icon;
		public buffConfig(){}
		public buffConfig(int Id,string Name,string Type,BuffEffectDelegate OnAdd,BuffEffectDelegate OnRemove,int SubType,int Val1,int Val2,int[] Other,int[] Effect,bool EndOnHit,int[] Pot,FormatStringDelegate Des,int Immune,string Icon)
		{
			this.Id= Id;
			this.Name= Name;
			this.Type= Type;
			this.OnAdd= OnAdd;
			this.OnRemove= OnRemove;
			this.SubType= SubType;
			this.Val1= Val1;
			this.Val2= Val2;
			this.Other= Other;
			this.Effect= Effect;
			this.EndOnHit= EndOnHit;
			this.Pot= Pot;
			this.Des= Des;
			this.Immune= Immune;
			this.Icon= Icon;
		}
	}
}
