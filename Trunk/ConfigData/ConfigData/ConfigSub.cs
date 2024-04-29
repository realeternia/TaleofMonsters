using System;
using System.Collections.Generic;

namespace ConfigDatas
{
    public struct RLIdValue
    {
        public int Id;
        public int Value;

        public static RLIdValue Parse(string str)
        {
            RLIdValue data = new RLIdValue();
            string[] datas = str.Split(';');

            data.Id = int.Parse(datas[0]);
            data.Value = int.Parse(datas[1]);
            return data;
        }
    }

    public struct RLIdValueList
    {
        private RLIdValue[] list;
        public RLIdValue this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }
        
        public RLIdValueList(RLIdValue[] data)
        {
        	list=data;
        }

        public int Count
        {
            get { return list.Length; }
        }

        public static RLIdValueList Parse(string str)
        {
            RLIdValueList data = new RLIdValueList();
            string[] datas = str.Split('|');
            
            if(str!=""&& datas.Length>0)
            {
            	data.list = new RLIdValue[datas.Length];
	            for (int i = 0; i < datas.Length; i++)
	            {
	                data.list[i] = RLIdValue.Parse(datas[i]);
	            }
            }
			else
			{
				data.list = new RLIdValue[]{};
			}

            return data;
        }
    }

    public struct RLVector3
    {
        public int X;
        public int Y;
	public int Z;

        public static RLVector3 Parse(string str)
        {
            RLVector3 data = new RLVector3();
            string[] datas = str.Split(';');

            data.X = int.Parse(datas[0]);
            data.Y = int.Parse(datas[1]);
            if (datas.Length == 3)
                data.Z = int.Parse(datas[2]);
            return data;
        }
    }

    public struct RLVector3List
    {
        private RLVector3[] list;
        public RLVector3 this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public RLVector3List(RLVector3[] data)
        {
        	list=data;
        }

        public int Count
        {
            get { return list.Length; }
        }

        public static RLVector3List Parse(string str)
        {
            RLVector3List data = new RLVector3List();
            string[] datas = str.Split('|');
            
            if(str!=""&& datas.Length>0)
            {
                data.list = new RLVector3[datas.Length];
	            for (int i = 0; i < datas.Length; i++)
	            {
                    data.list[i] = RLVector3.Parse(datas[i]);
	            }
            }
			else
			{
                data.list = new RLVector3[] { };
			}

            return data;
        }
    }
    
    public struct RLIItemRateCount
    {
    	public int Type;
        public int Id;
        public int RollMin;
        public int RollMax;
        public int Count;        

        public static RLIItemRateCount Parse(string str)
        {
            RLIItemRateCount data = new RLIItemRateCount();
            string[] datas = str.Split(';');

            data.Type = int.Parse(datas[0]);
            data.Id = int.Parse(datas[1]);
            data.RollMin = int.Parse(datas[2]);
            data.RollMax = int.Parse(datas[3]);
            data.Count = int.Parse(datas[4]);
            return data;
        }
    }

    public struct RLIItemRateCountList
    {
        private RLIItemRateCount[] list;
        public RLIItemRateCount this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public int Count
        {
            get { return list.Length; }
        }

        public static RLIItemRateCountList Parse(string str)
        {
            RLIItemRateCountList data = new RLIItemRateCountList();
            string[] datas = str.Split('|');
            data.list = new RLIItemRateCount[datas.Length];
            for (int i = 0; i < datas.Length; i++)
            {
                data.list[i] = RLIItemRateCount.Parse(datas[i]);
            }

            return data;
        }
    }    
    
    public struct RLXY
    {
        public int X;
        public int Y;

        public static RLXY Parse(string str)
        {
            RLXY data = new RLXY();
            string[] datas = str.Split(';');

            data.X = int.Parse(datas[0]);
            data.Y = int.Parse(datas[1]);
            return data;
        }
    }
     public class AttrModifyData
    {
        private float source;

        public float Source
        {
            get { return source; }
            set { source = value; }
        }

        public float Adder
        {
            get { return adder; }
            set { adder = value; }
        }

        public float Multiter
        {
            get { return multiter; }
            set { multiter = value; }
        }

        private float adder;

        private float multiter;

        public AttrModifyData(float sourceValue)
        {
            source = sourceValue;
        }

        public static AttrModifyData operator +(AttrModifyData data, double value)
        {
            data.adder += (float)value;
            return data;
        }

        public static AttrModifyData operator -(AttrModifyData data, double value)
        {
            data.adder -= (float)value;
            return data;
        }

        public static AttrModifyData operator *(AttrModifyData data, double value)
        {
            data.multiter += (float)value;
            return data;
        }

        public static AttrModifyData operator /(AttrModifyData data, double value)
        {
            data.multiter -= (float)value;
            return data;
        }

        public static bool operator <(AttrModifyData x, AttrModifyData y)
        {
            return (x.source * (1 + x.multiter) + x.adder) < (y.source * (1 + y.multiter) + y.adder);
        }

        public static bool operator >(AttrModifyData x, AttrModifyData y)
        {
            return (x.source * (1 + x.multiter) + x.adder) > (y.source * (1 + y.multiter) + y.adder);
        }

        public static bool operator ==(AttrModifyData x, AttrModifyData y)
        {
            return System.Math.Abs((x.source * (1 + x.multiter) + x.adder) - (y.source * (1 + y.multiter) + y.adder)) < 0.01;
        }

        public static bool operator !=(AttrModifyData x, AttrModifyData y)
        {
            return !(x == y);
        }
        
        public static implicit  operator double(AttrModifyData data)
        {
            return data.source * (1 + data.multiter) + data.adder;
        }
        
        public static implicit  operator int(AttrModifyData data)
        {
            return (int)(data.source * (1 + data.multiter) + data.adder);
        }
        
        public bool Equals(AttrModifyData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other==this;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AttrModifyData)) return false;
            return Equals((AttrModifyData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = source.GetHashCode();
                result = (result*397) ^ adder.GetHashCode();
                result = (result*397) ^ multiter.GetHashCode();
                return result;
            }
        }
    }
    
    public enum DamageTypes
    {
        Magic,
        Physical,
        All
    };
    
    public class HitDamage
    {
        private int value;
        private int element;
        private DamageTypes dtype;

        public int Value
        {
            get { return value; }
        }

        public int Element
        {
            get { return element; }
        }

        public DamageTypes Dtype
        {
            get { return dtype; }
        }

        public HitDamage(int damage, int element, DamageTypes type)
        {
            value = damage;
            this.element = element;
            dtype = type;
        }
        
        public bool AddPDamage(double damage)
        {
         return	AddDamage(DamageTypes.Physical,(int)damage);
        }
                
        public bool AddMDamage(double damage)
        {
         return	AddDamage(DamageTypes.Magic,(int)damage);
        }

        public bool AddDamage(DamageTypes type, int damage)
        {
            if (dtype == type || (type == DamageTypes.Physical && dtype != DamageTypes.Magic) || type == DamageTypes.All)
            {
                value += damage;
                return true;
            }
            return false;
        }
                
        public bool SetPDamageRate(double rate)
        {
         return	SetDamage(DamageTypes.Physical,(int)(Value*rate));
        }    
                    
        public bool SetMDamageRate(double rate)
        {
         return	SetDamage(DamageTypes.Magic,(int)(Value*rate));
        }

        public bool SetDamage(DamageTypes type, int damage)
        {
            if (dtype == type || (type == DamageTypes.Physical && dtype != DamageTypes.Magic) || type == DamageTypes.All)
            {
                value = damage;
                return true;
            }
            return false;
        }
    }
        
    public class WeaponAttrRate
    {
    	public int Type;
    	
    	public double Atk;
    	public double Def;    	
    	public double Hit;    	
    	public double Dhit;
    	public double Magic;    	
    }
    
    public class SkillActiveType
    {
        public static int Active = 0;
        public static int Passive = 1;
        public static int Both = 2;

        public static int Parse(string data)
        {
            if (data=="Active")
            {
                return Active;
            }
            if (data == "Passive")
            {
                return Passive;
            }
            return Both;
        }
    }    
    
     public interface IPlayer
    {
    	bool IsLeft{get;}
    	int Mana{get;}
    	int Anger{get;}  
    	int MaxMana{get;}
    	int MaxAnger{get;}      	
    	void AddMana(double addon);
		void AddAnger(double addon);
  	
    	void DeleteRandomCardFor(IPlayer p);
		void GetNextNCard(int n);
		void ConvertCard(int count, int cardId);
		void ThrowCard(int type);
		void DeleteAllCard();
		int GetCardNumber();
		void CopyRandomNCard(int n, int spellid);
		
		void AddResource(int type, int number);
		int Round{get;}
		void AddBuff(int buffId, int blevel, int time);
    }
    
    public interface IMap
    {
    	void SetTile(int itype, System.Drawing.Point point, int dis, int tile);
    	void ChangePositionWithRandom(IMonster target);
    	void SetRowUnitPosition(int y, bool isLeft, string type);
    	void DragRandomUnitNear(System.Drawing.Point point);
    	void UpdateCellOwner(System.Drawing.Point mouse, int ownerId);
    	void ReviveUnit(System.Drawing.Point mouse, int addHp);
    	
    	IMonster[] GetAllMonster();
    	IMonster[] GetAllMonsterType(int type);
    	IMonster[] GetAllMonsterLevel(int minLv,int maxLv);    
    	IMonster[] GetRangeMonster(bool isLeft, string target, int range, System.Drawing.Point mouse);	
    }
    
    public interface IMonster
    {
    	int Id{get;}
    	int CardId{get;}
		void AddHp(double addon);
		void AddHpRate(double value);
		double HpRate{get;}
		int MaxHp{get;set;}
		int Hp{get;}
		int WeaponId{get;}
		IPlayer Owner{get;}
		bool IsHero{get;}		
		System.Drawing.Point Position{get;set;}
		
		void AddAuro(int buff, int lv, int ele, int rac, int ran, int tar);
		void AddAuro(int buff, int lv, int mid);
		void AddAntiMagic(string type, int value);
		void AddBuff(int buffId, int blevel, int dura);
		void AddItem(int itemId);//Õ½¶·ÖÐ
		void Transform(int monId);
		void AddCardRate(int monId, int rate);
		void DeleteRandomCard();
		void AddResource(int type,int count);
		void AddActionRate(double value);
		void StealWeapon(IMonster target);
		void BreakWeapon();
		void AddCardExp(int addon);
		void WeaponReturn(int type);
		void AddTile(int tile);
		void AddRandSkill();
		void AddWeaponAttr(int type, double atkRate, double defRate, double hitRate, double dhitRate, double magicRate);
		void AddImmune(int buffId);
		void AddSpecialMark(int mark);
		IMonster[] GetRangeUnit(int type, int range, string filter, IMonster d, string effect);
		IMonster[] GetTypeUnit(IMonster d, string effect);
		IMonster[] GetBehindUnit(IMonster d, string effect);
		int GetTileCount(int type);
		int GetMonsterCount(int mid);
		int GetMonsterCountByType(int type);		
		
		AttrModifyData Atk{get;set;}
		AttrModifyData Def{get;set;}	
		AttrModifyData Hit{get;set;}	
		AttrModifyData DHit{get;set;}	
		AttrModifyData Spd{get;set;}
		AttrModifyData MAtk{get;set;}
		AttrModifyData MDef{get;set;}
		AttrModifyData Skl{get;set;}		
		int Ats{get;set;}		
        int PDamageAbsorb { get;set;}
        int CardManaRateW { get;set;}
        int CardManaRateS { get;set;}
        int TileBuff { get;set;}
		int Star{get;}	
 		bool IsTileMatching { get;}
 		bool IsElement(string ele);
 		bool IsRace(string rac); 
 		bool HasSkill(int sid);
 		int AttackType{get;set;}
		int HeroAtk{get;}
		int HeroDef{get;}	
 		
 		int SkillParm{get;set;}
 		bool DropAdd{get;set;}
 		
 		bool IsNight{get;}
 		int CardNumber{get;} 	
 		
 		void ClearDebuff();
 		void ExtendDebuff(int count);
 		bool HasBuff(int id);
 		void SetToPosition(string type);
 		void OnMagicDamage(int damage, int element);
 		void SuddenDeath();
    }
    public delegate void BuffEffectDelegate(IMonster owner,int level);
    
     public delegate void SkillInitialEffectDelegate(IMonster src,int level);
     
      public delegate bool SkillBurstCheckDelegate(IMonster src,IMonster dest,bool isActive);
    public delegate void SkillHitEffectDelegate(IMonster src,IMonster dest,ref int hit,int level);
     public delegate void SkillDamageEffectDelegate(IMonster src,IMonster dest,bool isActive,HitDamage damage, ref int minDamage, ref bool deathHit,int level);    
     public delegate void SkillAfterHitEffectDelegate(IMonster src,IMonster dest,HitDamage damage, bool deadHit,int level);    
     public delegate void SkillTimelyEffectDelegate(IMonster src,int level);
     
      public delegate void SpellEffectDelegate(IMap map, IPlayer player, IPlayer rival, IMonster target,System.Drawing.Point mouse,int level);

    public delegate string FormatStringDelegate(int level);
}
