using System;
using System.Collections.Generic;
using NarlonLib.Math;
namespace ConfigDatas
{
	public class ConfigData
	{
		public static Dictionary<int, buffConfig> buffDict= new Dictionary<int, buffConfig>();
		public static buffConfig Nonebuff= new buffConfig();
		public static buffConfig GetbuffConfig(int id)
		{
			if (buffDict.ContainsKey(id))
				return buffDict[id];
			else
				return Nonebuff;
		}
		private static void Loadbuff()
		{
			buffDict.Add(5, new buffConfig(5,"贯通","ns",null,null,0,0,0,new int[]{0},new int[]{3,4},false,new int[]{0,0},delegate(int lv){ return string.Format("取消地形和光环支援");},0,"tranfix"));
			buffDict.Add(9, new buffConfig(9,"冰冻","ns",null,null,0,0,0,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("无法移动和攻击");},0,"frost"));
			buffDict.Add(12, new buffConfig(12,"晕眩","ns",null,null,0,0,0,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("无法移动和攻击");},0,"stun"));
			buffDict.Add(19, new buffConfig(19,"睡眠","ns",null,null,0,0,0,new int[]{0},new int[]{1,2},true,new int[]{0,0},delegate(int lv){ return string.Format("无法移动和攻击，但受到攻击后状态解除");},0,"sleep"));
			buffDict.Add(20, new buffConfig(20,"遗忘","ns",null,null,0,0,0,new int[]{0},new int[]{2},false,new int[]{0,0},delegate(int lv){ return string.Format("无法攻击");},0,"forgot"));
			buffDict.Add(22, new buffConfig(22,"背叛","ns",null,null,0,0,0,new int[]{0},new int[]{102},false,new int[]{0,0},delegate(int lv){ return string.Format("目标会攻击己方");},0,"rebel"));
			buffDict.Add(10025, new buffConfig(10025,"圣印","na",null,null,0,0,0,new int[]{0},new int[]{203},false,new int[]{0,0},delegate(int lv){ return string.Format("召唤师无法使用生物技能");},0,"saintmark"));
			buffDict.Add(10026, new buffConfig(10026,"刚印","pa",null,null,0,0,0,new int[]{0},new int[]{307},false,new int[]{0,0},delegate(int lv){ return string.Format("召唤师使用魔法和武器只消耗魔法或是怒气");},0,"justmark"));
			buffDict.Add(10029, new buffConfig(10029,"集结","pa",null,null,0,0,0,new int[]{0},new int[]{301},false,new int[]{0,0},delegate(int lv){ return string.Format("每回合抽取2张卡片");},0,"gather"));
			buffDict.Add(10030, new buffConfig(10030,"虹吸","ns",null,null,0,0,0,new int[]{0},new int[]{304},false,new int[]{0,0},delegate(int lv){ return string.Format("下次使用的装备自动破坏");},0,"siphon"));
			buffDict.Add(10031, new buffConfig(10031,"沉默","ns",null,null,0,0,0,new int[]{0},new int[]{202},false,new int[]{0,0},delegate(int lv){ return string.Format("无法使用魔法");},0,"silent"));
			buffDict.Add(10032, new buffConfig(10032,"束缚","ns",null,null,0,0,0,new int[]{0},new int[]{201},false,new int[]{0,0},delegate(int lv){ return string.Format("无法使用任何卡片");},0,"bind"));
			buffDict.Add(10033, new buffConfig(10033,"透视","ns",null,null,0,0,0,new int[]{0},new int[]{306},false,new int[]{0,0},delegate(int lv){ return string.Format("手牌可以被看到");},0,"eye"));
			buffDict.Add(10036, new buffConfig(10036,"吸魂","pa",null,null,0,0,0,new int[]{0},new int[]{303},false,new int[]{0,0},delegate(int lv){ return string.Format("杀死单位回复3%魔法");},0,"soulsteal"));
			buffDict.Add(10037, new buffConfig(10037,"孤单","ns",null,null,0,0,0,new int[]{0},new int[]{4},false,new int[]{0,0},delegate(int lv){ return string.Format("无法获得支援效果");},0,"longly"));
			buffDict.Add(18, new buffConfig(18,"石化","ns",delegate(IMonster o,int lv){o.AddDef(0.6f*lv);},delegate(IMonster o,int lv){o.AddDef(-50);},1,0,50,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("无法移动和攻击，但会提高{0:0}点防御,{1:0}点",3*lv,3*lv);},0,"stone"));
			buffDict.Add(10001, new buffConfig(10001,"昏暗","pa",null,null,1,15,15,new int[]{0},new int[]{0},false,new int[]{7,7},delegate(int lv){ return string.Format("亡灵提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"dark"));
			buffDict.Add(10002, new buffConfig(10002,"巨龙","pa",null,null,1,22,8,new int[]{0},new int[]{0},false,new int[]{11,4},delegate(int lv){ return string.Format("龙提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"dragon"));
			buffDict.Add(10003, new buffConfig(10003,"寒气","pa",null,null,1,12,12,new int[]{0},new int[]{0},false,new int[]{6,6},delegate(int lv){ return string.Format("冰系单位提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"chili"));
			buffDict.Add(10004, new buffConfig(10004,"学识","pa",null,null,1,0,20,new int[]{0},new int[]{0},false,new int[]{0,10},delegate(int lv){ return string.Format("无属性单位提高{0:0}点防御",val2);},0,"learned"));
			buffDict.Add(10005, new buffConfig(10005,"焰刃","pa",null,null,1,30,0,new int[]{0},new int[]{0},false,new int[]{15,0},delegate(int lv){ return string.Format("火系单位提高{0:0}点攻击",val1);},0,"firesword"));
			buffDict.Add(10006, new buffConfig(10006,"水刃","pa",null,null,1,15,15,new int[]{0},new int[]{0},false,new int[]{7,7},delegate(int lv){ return string.Format("水系单位提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"watersword"));
			buffDict.Add(10007, new buffConfig(10007,"暗爪","pa",null,null,1,25,6,new int[]{0},new int[]{0},false,new int[]{12,3},delegate(int lv){ return string.Format("暗系单位提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"darkclaw"));
			buffDict.Add(10008, new buffConfig(10008,"雷刃","pa",null,null,1,30,0,new int[]{0},new int[]{0},false,new int[]{15,0},delegate(int lv){ return string.Format("雷系单位提高{0:0}点攻击",val1);},0,"thundersword"));
			buffDict.Add(10009, new buffConfig(10009,"风斧","pa",null,null,1,20,12,new int[]{0},new int[]{0},false,new int[]{10,6},delegate(int lv){ return string.Format("风系单位提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"windaxe"));
			buffDict.Add(10010, new buffConfig(10010,"冰锤","pa",null,null,1,15,18,new int[]{0},new int[]{0},false,new int[]{7,9},delegate(int lv){ return string.Format("冰系单位提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"icesinker"));
			buffDict.Add(10011, new buffConfig(10011,"光剑","pa",null,null,1,10,21,new int[]{0},new int[]{0},false,new int[]{5,10},delegate(int lv){ return string.Format("光系单位提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"lightsword"));
			buffDict.Add(10012, new buffConfig(10012,"地刃","pa",null,null,1,0,30,new int[]{0},new int[]{0},false,new int[]{0,15},delegate(int lv){ return string.Format("地系单位提高{0:0}点防御",val2);},0,"earthsword"));
			buffDict.Add(10014, new buffConfig(10014,"微光","pa",null,null,1,16,0,new int[]{0},new int[]{0},false,new int[]{8,0},delegate(int lv){ return string.Format("植物提高{0:0}点攻击",val1);},0,"twilight"));
			buffDict.Add(10015, new buffConfig(10015,"控制","pa",null,null,1,12,12,new int[]{0},new int[]{0},false,new int[]{6,6},delegate(int lv){ return string.Format("野兽提高{0:0}点攻击和防御",val1);},0,"control"));
			buffDict.Add(10054, new buffConfig(10054,"武装","ps",null,null,1,10,10,new int[]{0},new int[]{0},false,new int[]{5,5},delegate(int lv){ return string.Format("提高{0:0}点攻击和{1:0}点防御",val1,val2);},0,"fight"));
			buffDict.Add(10056, new buffConfig(10056,"火焰盾","pa",null,null,1,0,4,new int[]{0},new int[]{0},false,new int[]{15,15},delegate(int lv){ return string.Format("提高{0:0}点防御",val2);},0,"fireshield"));
			buffDict.Add(10043, new buffConfig(10043,"巨大","ps",null,null,2,50,50,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("提高{0:0}%攻击和防御",val1);},0,"huge"));
			buffDict.Add(10046, new buffConfig(10046,"猛砍","ps",null,null,2,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("提高{0:0}%攻击",val1);},0,"crashhit"));
			buffDict.Add(10013, new buffConfig(10013,"水护","pa",null,null,3,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("水系单位提高{0:0}点回避",val2);},0,"waterprotect"));
			buffDict.Add(10034, new buffConfig(10034,"暗色","pa",null,null,3,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("暗系单位提高{0:0}点回避",val2);},0,"dim"));
			buffDict.Add(10035, new buffConfig(10035,"福音","pa",null,null,3,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("光系单位提高{0:0}点回避",val2);},0,"matthew"));
			buffDict.Add(10057, new buffConfig(10057,"潜行","ps",null,null,3,0,8,new int[]{0},new int[]{0},false,new int[]{0,30},delegate(int lv){ return string.Format("提高{0:0}点回避",val2);},0,"slink"));
			buffDict.Add(10042, new buffConfig(10042,"极光","ps",null,null,4,15,15,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("提高{0:0}%命中和回避",val1);},0,"aurora"));
			buffDict.Add(10045, new buffConfig(10045,"神射","ps",null,null,4,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("提高{0:0}%命中",val1);},0,"shooter"));
			buffDict.Add(16, new buffConfig(16,"加速","ps",null,null,6,50,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("提高{0:0}%速度",val1);},0,"speedup"));
			buffDict.Add(10020, new buffConfig(10020,"奏乐","pa",null,null,6,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("提高{0:0}点速度",val2);},0,"music"));
			buffDict.Add(10044, new buffConfig(10044,"速行","ps",null,null,6,0,6,new int[]{0},new int[]{0},false,new int[]{0,3},delegate(int lv){ return string.Format("提高{0:0}点速度",val2);},0,"rider"));
			buffDict.Add(10053, new buffConfig(10053,"疾走","ps",null,null,6,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("提高{0:0}点速度",val2);},0,"rush"));
			buffDict.Add(23, new buffConfig(23,"地形","ta",null,null,7,20,20,new int[]{0},new int[]{101},false,new int[]{0,0},delegate(int lv){ return string.Format("提高{0:0}%攻击和{1:0}%回避",val1,val2);},0,"tile"));
			buffDict.Add(24, new buffConfig(24,"地主","ta",null,null,7,40,40,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("提高{0:0}%攻击和{1:0}%回避",val1,val2);},0,"lord"));
			buffDict.Add(10018, new buffConfig(10018,"领袖","pa",null,null,8,35,25,new int[]{0},new int[]{0},false,new int[]{17,12},delegate(int lv){ return string.Format("海盗提高{0:0}点攻击和{1:0}点速度",val1,val2);},0,"leader"));
			buffDict.Add(10051, new buffConfig(10051,"月蚀","ps",null,null,8,30,20,new int[]{0},new int[]{0},false,new int[]{15,10},delegate(int lv){ return string.Format("提高{0:0}点攻击和{1:0}点速度",val1,val2);},8196,"eclipse"));
			buffDict.Add(10055, new buffConfig(10055,"领导","pa",null,null,8,5,5,new int[]{0},new int[]{0},false,new int[]{15,15},delegate(int lv){ return string.Format("提高{0:0}点攻击和{1:0}点速度",val1,val2);},0,"royal"));
			buffDict.Add(10017, new buffConfig(10017,"鼠王","pa",null,null,9,40,40,new int[]{0},new int[]{0},false,new int[]{20,20},delegate(int lv){ return string.Format("巨鼠提高{0:0}点攻击和{1:0}点回避",val1,val2);},0,"mouseking"));
			buffDict.Add(10021, new buffConfig(10021,"庇佑","pa",null,null,10,10,10,new int[]{0},new int[]{0},false,new int[]{5,5},delegate(int lv){ return string.Format("提高{0:0}点攻击和{1:0}点命中",val1,val2);},0,"pray"));
			buffDict.Add(10022, new buffConfig(10022,"镇守","na",null,null,101,30,0,new int[]{0},new int[]{0},false,new int[]{15,0},delegate(int lv){ return string.Format("降低{0:0}点攻击",val1);},0,"defence"));
			buffDict.Add(3, new buffConfig(3,"恐惧","ns",null,null,102,0,50,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%防御",val2);},0,"fear"));
			buffDict.Add(4, new buffConfig(4,"削弱","ns",null,null,102,50,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%攻击",val1);},0,"weak"));
			buffDict.Add(25, new buffConfig(25,"弱地","ns",null,null,102,20,20,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%攻击和{1:0}%防御",val1,val2);},0,"dtile"));
			buffDict.Add(10048, new buffConfig(10048,"邪气","na",null,null,103,12,0,new int[]{0},new int[]{0},false,new int[]{6,0},delegate(int lv){ return string.Format("降低{0:0}点命中",val1);},0,"demon"));
			buffDict.Add(1, new buffConfig(1,"静止","ns",null,null,104,0,15,new int[]{0},new int[]{1},false,new int[]{0,0},delegate(int lv){ return string.Format("无法移动并降低{0:0}%回避",val2);},0,"stop"));
			buffDict.Add(11, new buffConfig(11,"致盲","ns",null,null,104,40,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%命中",val1);},0,"blind"));
			buffDict.Add(14, new buffConfig(14,"诅咒","ns",null,null,104,25,25,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%命中和回避",val1);},0,"curse"));
			buffDict.Add(8, new buffConfig(8,"冻伤","ns",null,null,105,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%速度",val1);},0,"snow"));
			buffDict.Add(17, new buffConfig(17,"迟缓","ns",null,null,105,50,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%速度",val1);},0,"speeddown"));
			buffDict.Add(10016, new buffConfig(10016,"大风","na",null,null,105,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("降低{0:0}点速度",val2);},0,"wind"));
			buffDict.Add(10040, new buffConfig(10040,"停留","na",null,null,105,80,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("英雄速度降低{0:0}%",val1);},0,"stay"));
			buffDict.Add(10052, new buffConfig(10052,"重力","ns",null,null,105,70,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%速度",val1);},0,"gravity"));
			buffDict.Add(2, new buffConfig(2,"麻痹","ns",null,null,106,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%速度",val1);},0,"als"));
			buffDict.Add(10050, new buffConfig(10050,"醉意","ns",null,null,106,30,30,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("降低{0:0}%速度和{1:0}%命中",val1,val2);},25604,"beer"));
			buffDict.Add(10, new buffConfig(10,"灼伤","ns",null,null,201,25,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("每回合减少{0:0}点生命",val1);},0,"burn"));
			buffDict.Add(10024, new buffConfig(10024,"圣冰","ns",null,null,201,15,0,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("无法移动和攻击，每回合受到{0:0}点伤害",val1);},0,"saintfrost"));
			buffDict.Add(7, new buffConfig(7,"中毒","ns",null,null,202,10,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("每回合减少{0:0}%生命",val1);},0,"poison"));
			buffDict.Add(15, new buffConfig(15,"感染","na",null,null,202,3,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("每回合减少{0:0}%生命",val1);},0,"infect"));
			buffDict.Add(13, new buffConfig(13,"流血","ns",null,null,203,6,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("每回合减少自身星级x{0:0}点生命",val1);},0,"bleed"));
			buffDict.Add(21, new buffConfig(21,"衰老","ns",null,null,204,10,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("每回合降低{0:0}%攻击和防御",val1);},0,"aging"));
			buffDict.Add(10049, new buffConfig(10049,"圣杖","pa",null,null,205,10,0,new int[]{0},new int[]{0},false,new int[]{5,0},delegate(int lv){ return string.Format("提高每回合{0:0}点生命回复",val1);},1028,"stick"));
			buffDict.Add(6, new buffConfig(6,"混乱","ns",null,null,301,30,100,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("攻击时有{0:0}%几率击中自身",val1);},0,"confuse"));
			buffDict.Add(10019, new buffConfig(10019,"神谕","ps",null,null,401,100,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("所有伤害降为0");},0,"godhint"));
			buffDict.Add(10023, new buffConfig(10023,"防护","ps",null,null,401,100,50,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("受到物理伤害降低50%(无法与降低百分比伤害类技能共存)");},0,"protect"));
			buffDict.Add(10047, new buffConfig(10047,"腐烂","ns",null,null,401,100,130,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("受到攻击时承受30%额外伤害");},0,"decay"));
			buffDict.Add(10027, new buffConfig(10027,"咒印","na",null,null,501,0,0,new int[]{100,100,150},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("使用魔法消耗150%魔法");},0,"magicmark"));
			buffDict.Add(10028, new buffConfig(10028,"魔印","pa",null,null,501,0,0,new int[]{100,100,50},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("使用魔法消耗50%魔法");},0,"manamark"));
			buffDict.Add(10038, new buffConfig(10038,"秘力","pa",null,null,501,0,0,new int[]{100,50,100},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("使用武器消耗50%怒气");},0,"secret"));
			buffDict.Add(10039, new buffConfig(10039,"炫光","pa",null,null,501,0,0,new int[]{100,150,100},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("使用武器消耗150%怒气");},0,"curlight"));
			buffDict.Add(10041, new buffConfig(10041,"曙光","ps",null,null,501,0,0,new int[]{100,50,120},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("使用武器消耗50%怒气，使用魔法消耗120%魔法");},0,"sunlight"));
		}
		public static void LoadData()
		{
			Loadbuff();

		}
	}
}
