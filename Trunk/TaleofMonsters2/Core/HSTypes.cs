namespace TaleofMonsters.Core
{
    public delegate void HsActionCallback();
    class HSTypes
    {
        public static string I2Attr(int aid)
        {
            string[] rt = { "��", "ˮ", "��", "��", "��", "��", "��", "��", "��", "����", "����", "����", "��Ʒ", "���巨��", "Ⱥ�巨��", "��������", "���α仯"};
            return rt[aid];
        }

        public static int Attr2I(string name)
        {
            string[] rt = { "��", "ˮ", "��", "��", "��", "��", "��", "��", "��", "����", "����", "����", "��Ʒ", "���巨��", "Ⱥ�巨��", "��������", "���α仯" };
            for (int i = 0; i < rt.Length; i++)
            {
                if (rt[i] == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string I2Race(int rid)
        {
            string[] rt = { "�ؾ�", "��ħ", "��е", "����", "����", "��", "��", "����", "����", "����", "����", "Ұ��", "��", "Ԫ��", "ֲ��", "Ӣ��" };
            return rt[rid];
        }

        public static string I2Equip(int eid)
        {
            string[] rt = { "ͷ��", "����", "����", "����", "����", "��ָ", "Ь��", "��ָ", "����" };
            return rt[eid];
        }

        public static string I2HItemType(int id)
        {
            string[] rt = { "", "��ͨ", "����", "����" };
            return rt[id];
        }

        public static string I2EPosition(int id)
        {
            string[] rt = { "ͷ��", "����", "����", "����", "����", "��ָ", "Ь��", "", "����" };
            return rt[id];
        }

        public static string I2Quality(int id)
        {
            string[] rt = {"��ͨ", "����", "����", "ʷʫ", "��˵", "", "", "", "", "��װ"};
            return rt[id];
        }

        public static string I2QualityColor(int id)
        {
            string[] rt = { "White", "Blue", "Yellow", "Purple", "Orange", "", "", "", "", "Green" };
            return rt[id];
        }

        public static string I2EaddonColor(int id)
        {
            string[] rt = { "Gray", "White", "Blue", "Yellow", "Purple", "Orange", "Red" };
            return "Cyan";
        }

        public static string I2RareColor(int value)
        {
            string[] rares = { "White", "Cyan", "SkyBlue", "Yellow", "Pink", "Purple", "Orange", "Red" };
            return rares[value];
        }

        public static string I2Resource(int id)
        {
            string[] rt = { "�ƽ�", "ľ��", "��ʯ", "ˮ��", "�챦ʯ", "���", "ˮ��" };
            return rt[id];
        }

        public static string I2ResourceColor(int id)
        {
            string[] rt = { "Gold", "DarkGoldenrod", "DarkKhaki", "White", "Red", "Yellow", "Blue" };
            return rt[id];
        }

        public static string I2CardLevelColor(int id)
        {
            string[] rt = { "", "White", "White", "SkyBlue", "SkyBlue", "Yellow", "Orange", "Red", "Red" };
            return rt[id];
        }

        public static string I2TaskStateColor(int id)
        {
            string[] rt = { "", "White", "Green", "Orange", "Red" };
            return rt[id];
        }

        public static string I2Season(int id)
        {
            string[] rt = { "����", "�ļ�", "�＾", "����" };
            return rt[id];
        }

        public static string I2LevelInfoColor(int id)
        {
            string[] rt = { "White", "Lime", "Purple" };
            return rt[id];
        }

        public static string I2LevelInfoType(int id)
        {
            string[] rt = { "", "�¹���", "�»" };
            return rt[id];
        }

        public static string I2CardQuality(int quality)
        {
            string[] rt = { "��", "����", "����", "����" };
            return rt[quality];
        }

        public static string I2HeroAttrTip(int id)
        {
            string[] rt = { "-ÿ�����Ӣ��1.8�㹥��$-ÿ�����Ӣ��0.3����", 
                "-ÿ�����Ӣ��1�����$-ÿ�����Ӣ��1������", 
                "-ÿ�����Ӣ��1��ħ��$-ÿ�����3��ħ������",
                "-ÿ�����Ӣ��0.6������$-ÿ�����3��ŭ������", 
                "-ÿ�����Ӣ��0.6���ٶ�$-ÿ�����Ӣ��0.3��ر�",
                "-ÿ�����Ӣ��0.6��ر�$-ÿ�����0.6��ŭ�����޺�ħ������", 
                "-ÿ�����Ӣ��3������$-ÿ�����Ӣ��0.2���ٶ�", 
                "-ÿ�����Ӣ��0.6�㹥��/0.3�����$-ǿ��Ӣ��ƥ�����Ч��" };
            return rt[id];
        }

        public static string I2InitialAttrTip(int aid)
        {
            string[] rt = {
                              "������$-��������ƽ��",
                              "ˮ����$-����ǿ���ظ�",
                              "������$-����ǿ���ٶ�",
                              "������$-����ǿ������",
                              "������$-����ǿ������",
                              "������$-����ǿ������",
                              "������$-����ǿ������"
                          };
            return rt[aid];
        }

        public static string I2ConstellationTip(int aid)
        {
            string[] rt = {
                              "������$3/22-4/20$��Ȼ���������ɣ�������Ҫ���ŵ�",
                              "��ţ��$4/21-5/20$ż��������أ������Ե�ð��",
                              "˫����$5/21-6/21$���桢�ö������棬��������þ���",
                              "��з��$6/22-7/22$���������籩�����Ĵ������������������",
                              "ʨ����$7/23-8/22$С�ı�ǿ�ҵ������ķ�ҧһ��",
                              "��Ů��$8/23-9/22$ע��Сϸ�ڣ���Ҫץ������",
                              "�����$9/23-10/23$����ӹ֮������ƽ�ȵ�����",
                              "��Ы��$10/24-11/22$���Թ¶���������",
                              "������$11/23-12/21$׷�����ɣ��ӽ�����",
                              "Ħ����$12/22-1/20$̤ʵ������������Ŀ��",
                              "ˮƿ��$1/21-2/19$�Ȱ�֪ʶ����������ḻ",
                              "˫����$2/20-3/21$Ҫ��ǿ������־��"
                          };
            return rt[aid];
        }
    }

    internal class BattleRuleType
    {
        public const int None = 0;
        public const int FastRecover = 1;
    }
}
