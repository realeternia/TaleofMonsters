namespace TaleofMonsters.Core
{
    public delegate void HsActionCallback();
    class HSTypes
    {
        public static string I2Attr(int aid)
        {
            string[] rt = { "无", "水", "风", "火", "地", "冰", "雷", "光", "暗", "武器", "卷轴", "防具", "饰品", "单体法术", "群体法术", "基本法术", "地形变化"};
            return rt[aid];
        }

        public static int Attr2I(string name)
        {
            string[] rt = { "无", "水", "风", "火", "地", "冰", "雷", "光", "暗", "武器", "卷轴", "防具", "饰品", "单体法术", "群体法术", "基本法术", "地形变化" };
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
            string[] rt = { "地精", "恶魔", "机械", "精灵", "昆虫", "龙", "鸟", "爬行", "人类", "兽人", "亡灵", "野兽", "鱼", "元素", "植物", "英雄" };
            return rt[rid];
        }

        public static string I2Equip(int eid)
        {
            string[] rt = { "头盔", "护甲", "武器", "护盾", "项链", "戒指", "鞋子", "戒指", "神器" };
            return rt[eid];
        }

        public static string I2HItemType(int id)
        {
            string[] rt = { "", "普通", "材料", "任务" };
            return rt[id];
        }

        public static string I2EPosition(int id)
        {
            string[] rt = { "头盔", "护甲", "武器", "护盾", "项链", "戒指", "鞋子", "", "神器" };
            return rt[id];
        }

        public static string I2Quality(int id)
        {
            string[] rt = {"普通", "良好", "优质", "史诗", "传说", "", "", "", "", "套装"};
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
            string[] rt = { "黄金", "木材", "矿石", "水银", "红宝石", "硫磺", "水晶" };
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
            string[] rt = { "春季", "夏季", "秋季", "冬季" };
            return rt[id];
        }

        public static string I2LevelInfoColor(int id)
        {
            string[] rt = { "White", "Lime", "Purple" };
            return rt[id];
        }

        public static string I2LevelInfoType(int id)
        {
            string[] rt = { "", "新功能", "新活动" };
            return rt[id];
        }

        public static string I2CardQuality(int quality)
        {
            string[] rt = { "无", "蓝钻", "紫钻", "黄钻" };
            return rt[quality];
        }

        public static string I2HeroAttrTip(int id)
        {
            string[] rt = { "-每点提高英雄1.8点攻击$-每点提高英雄0.3命中", 
                "-每点提高英雄1点防御$-每点提高英雄1点生命", 
                "-每点提高英雄1点魔力$-每点提高3点魔法上限",
                "-每点提高英雄0.6点命中$-每点提高3点怒气上限", 
                "-每点提高英雄0.6点速度$-每点提高英雄0.3点回避",
                "-每点提高英雄0.6点回避$-每点提高0.6点怒气上限和魔法上限", 
                "-每点提高英雄3点生命$-每点提高英雄0.2点速度", 
                "-每点提高英雄0.6点攻击/0.3点防御$-强化英雄匹配地形效果" };
            return rt[id];
        }

        public static string I2InitialAttrTip(int aid)
        {
            string[] rt = {
                              "无属性$-怪物属性平衡",
                              "水属性$-怪物强化回复",
                              "风属性$-怪物强化速度",
                              "火属性$-怪物强化攻击",
                              "地属性$-怪物强化防御",
                              "冰属性$-怪物强化生命",
                              "雷属性$-怪物强化命中"
                          };
            return rt[aid];
        }

        public static string I2ConstellationTip(int aid)
        {
            string[] rt = {
                              "白羊座$3/22-4/20$虽然你是乐天派，但凡事要稳着点",
                              "金牛座$4/21-5/20$偶尔跨出保守，是良性的冒险",
                              "双子座$5/21-6/21$好奇、好动、好玩，你的人生好精彩",
                              "巨蟹座$6/22-7/22$减少情绪风暴发生的次数，你的人生更美好",
                              "狮子座$7/23-8/22$小心被强烈的自尊心反咬一口",
                              "处女座$8/23-9/22$注意小细节，更要抓紧大方向",
                              "天秤座$9/23-10/23$持中庸之道享受平稳的人生",
                              "天蝎座$10/24-11/22$可以孤独，但勿封闭",
                              "射手座$11/23-12/21$追求自由，接近阳光",
                              "摩羯座$12/22-1/20$踏实的作风助你达成目标",
                              "水瓶座$1/21-2/19$热爱知识，精神生活丰富",
                              "双鱼座$2/20-3/21$要加强锻炼意志力"
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
