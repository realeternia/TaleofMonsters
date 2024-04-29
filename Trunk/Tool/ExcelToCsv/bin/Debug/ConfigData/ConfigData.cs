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
			buffDict.Add(5, new buffConfig(5,"��ͨ","ns",null,null,0,0,0,new int[]{0},new int[]{3,4},false,new int[]{0,0},delegate(int lv){ return string.Format("ȡ�����κ͹⻷֧Ԯ");},0,"tranfix"));
			buffDict.Add(9, new buffConfig(9,"����","ns",null,null,0,0,0,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷��ƶ��͹���");},0,"frost"));
			buffDict.Add(12, new buffConfig(12,"��ѣ","ns",null,null,0,0,0,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷��ƶ��͹���");},0,"stun"));
			buffDict.Add(19, new buffConfig(19,"˯��","ns",null,null,0,0,0,new int[]{0},new int[]{1,2},true,new int[]{0,0},delegate(int lv){ return string.Format("�޷��ƶ��͹��������ܵ�������״̬���");},0,"sleep"));
			buffDict.Add(20, new buffConfig(20,"����","ns",null,null,0,0,0,new int[]{0},new int[]{2},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷�����");},0,"forgot"));
			buffDict.Add(22, new buffConfig(22,"����","ns",null,null,0,0,0,new int[]{0},new int[]{102},false,new int[]{0,0},delegate(int lv){ return string.Format("Ŀ��ṥ������");},0,"rebel"));
			buffDict.Add(10025, new buffConfig(10025,"ʥӡ","na",null,null,0,0,0,new int[]{0},new int[]{203},false,new int[]{0,0},delegate(int lv){ return string.Format("�ٻ�ʦ�޷�ʹ�����＼��");},0,"saintmark"));
			buffDict.Add(10026, new buffConfig(10026,"��ӡ","pa",null,null,0,0,0,new int[]{0},new int[]{307},false,new int[]{0,0},delegate(int lv){ return string.Format("�ٻ�ʦʹ��ħ��������ֻ����ħ������ŭ��");},0,"justmark"));
			buffDict.Add(10029, new buffConfig(10029,"����","pa",null,null,0,0,0,new int[]{0},new int[]{301},false,new int[]{0,0},delegate(int lv){ return string.Format("ÿ�غϳ�ȡ2�ſ�Ƭ");},0,"gather"));
			buffDict.Add(10030, new buffConfig(10030,"����","ns",null,null,0,0,0,new int[]{0},new int[]{304},false,new int[]{0,0},delegate(int lv){ return string.Format("�´�ʹ�õ�װ���Զ��ƻ�");},0,"siphon"));
			buffDict.Add(10031, new buffConfig(10031,"��Ĭ","ns",null,null,0,0,0,new int[]{0},new int[]{202},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷�ʹ��ħ��");},0,"silent"));
			buffDict.Add(10032, new buffConfig(10032,"����","ns",null,null,0,0,0,new int[]{0},new int[]{201},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷�ʹ���κο�Ƭ");},0,"bind"));
			buffDict.Add(10033, new buffConfig(10033,"͸��","ns",null,null,0,0,0,new int[]{0},new int[]{306},false,new int[]{0,0},delegate(int lv){ return string.Format("���ƿ��Ա�����");},0,"eye"));
			buffDict.Add(10036, new buffConfig(10036,"����","pa",null,null,0,0,0,new int[]{0},new int[]{303},false,new int[]{0,0},delegate(int lv){ return string.Format("ɱ����λ�ظ�3%ħ��");},0,"soulsteal"));
			buffDict.Add(10037, new buffConfig(10037,"�µ�","ns",null,null,0,0,0,new int[]{0},new int[]{4},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷����֧ԮЧ��");},0,"longly"));
			buffDict.Add(18, new buffConfig(18,"ʯ��","ns",delegate(IMonster o,int lv){o.AddDef(0.6f*lv);},delegate(IMonster o,int lv){o.AddDef(-50);},1,0,50,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷��ƶ��͹������������{0:0}�����,{1:0}��",3*lv,3*lv);},0,"stone"));
			buffDict.Add(10001, new buffConfig(10001,"�谵","pa",null,null,1,15,15,new int[]{0},new int[]{0},false,new int[]{7,7},delegate(int lv){ return string.Format("�������{0:0}�㹥����{1:0}�����",val1,val2);},0,"dark"));
			buffDict.Add(10002, new buffConfig(10002,"����","pa",null,null,1,22,8,new int[]{0},new int[]{0},false,new int[]{11,4},delegate(int lv){ return string.Format("�����{0:0}�㹥����{1:0}�����",val1,val2);},0,"dragon"));
			buffDict.Add(10003, new buffConfig(10003,"����","pa",null,null,1,12,12,new int[]{0},new int[]{0},false,new int[]{6,6},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�㹥����{1:0}�����",val1,val2);},0,"chili"));
			buffDict.Add(10004, new buffConfig(10004,"ѧʶ","pa",null,null,1,0,20,new int[]{0},new int[]{0},false,new int[]{0,10},delegate(int lv){ return string.Format("�����Ե�λ���{0:0}�����",val2);},0,"learned"));
			buffDict.Add(10005, new buffConfig(10005,"����","pa",null,null,1,30,0,new int[]{0},new int[]{0},false,new int[]{15,0},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�㹥��",val1);},0,"firesword"));
			buffDict.Add(10006, new buffConfig(10006,"ˮ��","pa",null,null,1,15,15,new int[]{0},new int[]{0},false,new int[]{7,7},delegate(int lv){ return string.Format("ˮϵ��λ���{0:0}�㹥����{1:0}�����",val1,val2);},0,"watersword"));
			buffDict.Add(10007, new buffConfig(10007,"��צ","pa",null,null,1,25,6,new int[]{0},new int[]{0},false,new int[]{12,3},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�㹥����{1:0}�����",val1,val2);},0,"darkclaw"));
			buffDict.Add(10008, new buffConfig(10008,"����","pa",null,null,1,30,0,new int[]{0},new int[]{0},false,new int[]{15,0},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�㹥��",val1);},0,"thundersword"));
			buffDict.Add(10009, new buffConfig(10009,"�縫","pa",null,null,1,20,12,new int[]{0},new int[]{0},false,new int[]{10,6},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�㹥����{1:0}�����",val1,val2);},0,"windaxe"));
			buffDict.Add(10010, new buffConfig(10010,"����","pa",null,null,1,15,18,new int[]{0},new int[]{0},false,new int[]{7,9},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�㹥����{1:0}�����",val1,val2);},0,"icesinker"));
			buffDict.Add(10011, new buffConfig(10011,"�⽣","pa",null,null,1,10,21,new int[]{0},new int[]{0},false,new int[]{5,10},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�㹥����{1:0}�����",val1,val2);},0,"lightsword"));
			buffDict.Add(10012, new buffConfig(10012,"����","pa",null,null,1,0,30,new int[]{0},new int[]{0},false,new int[]{0,15},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}�����",val2);},0,"earthsword"));
			buffDict.Add(10014, new buffConfig(10014,"΢��","pa",null,null,1,16,0,new int[]{0},new int[]{0},false,new int[]{8,0},delegate(int lv){ return string.Format("ֲ�����{0:0}�㹥��",val1);},0,"twilight"));
			buffDict.Add(10015, new buffConfig(10015,"����","pa",null,null,1,12,12,new int[]{0},new int[]{0},false,new int[]{6,6},delegate(int lv){ return string.Format("Ұ�����{0:0}�㹥���ͷ���",val1);},0,"control"));
			buffDict.Add(10054, new buffConfig(10054,"��װ","ps",null,null,1,10,10,new int[]{0},new int[]{0},false,new int[]{5,5},delegate(int lv){ return string.Format("���{0:0}�㹥����{1:0}�����",val1,val2);},0,"fight"));
			buffDict.Add(10056, new buffConfig(10056,"�����","pa",null,null,1,0,4,new int[]{0},new int[]{0},false,new int[]{15,15},delegate(int lv){ return string.Format("���{0:0}�����",val2);},0,"fireshield"));
			buffDict.Add(10043, new buffConfig(10043,"�޴�","ps",null,null,2,50,50,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("���{0:0}%�����ͷ���",val1);},0,"huge"));
			buffDict.Add(10046, new buffConfig(10046,"�Ϳ�","ps",null,null,2,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("���{0:0}%����",val1);},0,"crashhit"));
			buffDict.Add(10013, new buffConfig(10013,"ˮ��","pa",null,null,3,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("ˮϵ��λ���{0:0}��ر�",val2);},0,"waterprotect"));
			buffDict.Add(10034, new buffConfig(10034,"��ɫ","pa",null,null,3,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}��ر�",val2);},0,"dim"));
			buffDict.Add(10035, new buffConfig(10035,"����","pa",null,null,3,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("��ϵ��λ���{0:0}��ر�",val2);},0,"matthew"));
			buffDict.Add(10057, new buffConfig(10057,"Ǳ��","ps",null,null,3,0,8,new int[]{0},new int[]{0},false,new int[]{0,30},delegate(int lv){ return string.Format("���{0:0}��ر�",val2);},0,"slink"));
			buffDict.Add(10042, new buffConfig(10042,"����","ps",null,null,4,15,15,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("���{0:0}%���кͻر�",val1);},0,"aurora"));
			buffDict.Add(10045, new buffConfig(10045,"����","ps",null,null,4,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("���{0:0}%����",val1);},0,"shooter"));
			buffDict.Add(16, new buffConfig(16,"����","ps",null,null,6,50,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("���{0:0}%�ٶ�",val1);},0,"speedup"));
			buffDict.Add(10020, new buffConfig(10020,"����","pa",null,null,6,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("���{0:0}���ٶ�",val2);},0,"music"));
			buffDict.Add(10044, new buffConfig(10044,"����","ps",null,null,6,0,6,new int[]{0},new int[]{0},false,new int[]{0,3},delegate(int lv){ return string.Format("���{0:0}���ٶ�",val2);},0,"rider"));
			buffDict.Add(10053, new buffConfig(10053,"����","ps",null,null,6,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("���{0:0}���ٶ�",val2);},0,"rush"));
			buffDict.Add(23, new buffConfig(23,"����","ta",null,null,7,20,20,new int[]{0},new int[]{101},false,new int[]{0,0},delegate(int lv){ return string.Format("���{0:0}%������{1:0}%�ر�",val1,val2);},0,"tile"));
			buffDict.Add(24, new buffConfig(24,"����","ta",null,null,7,40,40,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("���{0:0}%������{1:0}%�ر�",val1,val2);},0,"lord"));
			buffDict.Add(10018, new buffConfig(10018,"����","pa",null,null,8,35,25,new int[]{0},new int[]{0},false,new int[]{17,12},delegate(int lv){ return string.Format("�������{0:0}�㹥����{1:0}���ٶ�",val1,val2);},0,"leader"));
			buffDict.Add(10051, new buffConfig(10051,"��ʴ","ps",null,null,8,30,20,new int[]{0},new int[]{0},false,new int[]{15,10},delegate(int lv){ return string.Format("���{0:0}�㹥����{1:0}���ٶ�",val1,val2);},8196,"eclipse"));
			buffDict.Add(10055, new buffConfig(10055,"�쵼","pa",null,null,8,5,5,new int[]{0},new int[]{0},false,new int[]{15,15},delegate(int lv){ return string.Format("���{0:0}�㹥����{1:0}���ٶ�",val1,val2);},0,"royal"));
			buffDict.Add(10017, new buffConfig(10017,"����","pa",null,null,9,40,40,new int[]{0},new int[]{0},false,new int[]{20,20},delegate(int lv){ return string.Format("�������{0:0}�㹥����{1:0}��ر�",val1,val2);},0,"mouseking"));
			buffDict.Add(10021, new buffConfig(10021,"����","pa",null,null,10,10,10,new int[]{0},new int[]{0},false,new int[]{5,5},delegate(int lv){ return string.Format("���{0:0}�㹥����{1:0}������",val1,val2);},0,"pray"));
			buffDict.Add(10022, new buffConfig(10022,"����","na",null,null,101,30,0,new int[]{0},new int[]{0},false,new int[]{15,0},delegate(int lv){ return string.Format("����{0:0}�㹥��",val1);},0,"defence"));
			buffDict.Add(3, new buffConfig(3,"�־�","ns",null,null,102,0,50,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%����",val2);},0,"fear"));
			buffDict.Add(4, new buffConfig(4,"����","ns",null,null,102,50,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%����",val1);},0,"weak"));
			buffDict.Add(25, new buffConfig(25,"����","ns",null,null,102,20,20,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%������{1:0}%����",val1,val2);},0,"dtile"));
			buffDict.Add(10048, new buffConfig(10048,"а��","na",null,null,103,12,0,new int[]{0},new int[]{0},false,new int[]{6,0},delegate(int lv){ return string.Format("����{0:0}������",val1);},0,"demon"));
			buffDict.Add(1, new buffConfig(1,"��ֹ","ns",null,null,104,0,15,new int[]{0},new int[]{1},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷��ƶ�������{0:0}%�ر�",val2);},0,"stop"));
			buffDict.Add(11, new buffConfig(11,"��ä","ns",null,null,104,40,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%����",val1);},0,"blind"));
			buffDict.Add(14, new buffConfig(14,"����","ns",null,null,104,25,25,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%���кͻر�",val1);},0,"curse"));
			buffDict.Add(8, new buffConfig(8,"����","ns",null,null,105,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%�ٶ�",val1);},0,"snow"));
			buffDict.Add(17, new buffConfig(17,"�ٻ�","ns",null,null,105,50,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%�ٶ�",val1);},0,"speeddown"));
			buffDict.Add(10016, new buffConfig(10016,"���","na",null,null,105,0,10,new int[]{0},new int[]{0},false,new int[]{0,5},delegate(int lv){ return string.Format("����{0:0}���ٶ�",val2);},0,"wind"));
			buffDict.Add(10040, new buffConfig(10040,"ͣ��","na",null,null,105,80,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("Ӣ���ٶȽ���{0:0}%",val1);},0,"stay"));
			buffDict.Add(10052, new buffConfig(10052,"����","ns",null,null,105,70,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%�ٶ�",val1);},0,"gravity"));
			buffDict.Add(2, new buffConfig(2,"���","ns",null,null,106,30,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%�ٶ�",val1);},0,"als"));
			buffDict.Add(10050, new buffConfig(10050,"����","ns",null,null,106,30,30,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����{0:0}%�ٶȺ�{1:0}%����",val1,val2);},25604,"beer"));
			buffDict.Add(10, new buffConfig(10,"����","ns",null,null,201,25,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ÿ�غϼ���{0:0}������",val1);},0,"burn"));
			buffDict.Add(10024, new buffConfig(10024,"ʥ��","ns",null,null,201,15,0,new int[]{0},new int[]{1,2},false,new int[]{0,0},delegate(int lv){ return string.Format("�޷��ƶ��͹�����ÿ�غ��ܵ�{0:0}���˺�",val1);},0,"saintfrost"));
			buffDict.Add(7, new buffConfig(7,"�ж�","ns",null,null,202,10,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ÿ�غϼ���{0:0}%����",val1);},0,"poison"));
			buffDict.Add(15, new buffConfig(15,"��Ⱦ","na",null,null,202,3,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ÿ�غϼ���{0:0}%����",val1);},0,"infect"));
			buffDict.Add(13, new buffConfig(13,"��Ѫ","ns",null,null,203,6,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ÿ�غϼ��������Ǽ�x{0:0}������",val1);},0,"bleed"));
			buffDict.Add(21, new buffConfig(21,"˥��","ns",null,null,204,10,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ÿ�غϽ���{0:0}%�����ͷ���",val1);},0,"aging"));
			buffDict.Add(10049, new buffConfig(10049,"ʥ��","pa",null,null,205,10,0,new int[]{0},new int[]{0},false,new int[]{5,0},delegate(int lv){ return string.Format("���ÿ�غ�{0:0}�������ظ�",val1);},1028,"stick"));
			buffDict.Add(6, new buffConfig(6,"����","ns",null,null,301,30,100,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("����ʱ��{0:0}%���ʻ�������",val1);},0,"confuse"));
			buffDict.Add(10019, new buffConfig(10019,"����","ps",null,null,401,100,0,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("�����˺���Ϊ0");},0,"godhint"));
			buffDict.Add(10023, new buffConfig(10023,"����","ps",null,null,401,100,50,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("�ܵ������˺�����50%(�޷��뽵�Ͱٷֱ��˺��༼�ܹ���)");},0,"protect"));
			buffDict.Add(10047, new buffConfig(10047,"����","ns",null,null,401,100,130,new int[]{0},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("�ܵ�����ʱ����30%�����˺�");},0,"decay"));
			buffDict.Add(10027, new buffConfig(10027,"��ӡ","na",null,null,501,0,0,new int[]{100,100,150},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ʹ��ħ������150%ħ��");},0,"magicmark"));
			buffDict.Add(10028, new buffConfig(10028,"ħӡ","pa",null,null,501,0,0,new int[]{100,100,50},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ʹ��ħ������50%ħ��");},0,"manamark"));
			buffDict.Add(10038, new buffConfig(10038,"����","pa",null,null,501,0,0,new int[]{100,50,100},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ʹ����������50%ŭ��");},0,"secret"));
			buffDict.Add(10039, new buffConfig(10039,"�Ź�","pa",null,null,501,0,0,new int[]{100,150,100},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ʹ����������150%ŭ��");},0,"curlight"));
			buffDict.Add(10041, new buffConfig(10041,"���","ps",null,null,501,0,0,new int[]{100,50,120},new int[]{0},false,new int[]{0,0},delegate(int lv){ return string.Format("ʹ����������50%ŭ����ʹ��ħ������120%ħ��");},0,"sunlight"));
		}
		public static void LoadData()
		{
			Loadbuff();

		}
	}
}
