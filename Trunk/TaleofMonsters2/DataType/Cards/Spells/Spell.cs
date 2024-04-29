using NarlonLib.Math;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Shops;
using ConfigDatas;

namespace TaleofMonsters.DataType.Cards.Spells
{
    internal class Spell : ISellable
    {
        public SpellConfig SpellConfig;

        private int lv = 1;

        public int Id
        {
            get { return SpellConfig.Id; }
        }

        public Spell(int sid)
        {
            SpellConfig = ConfigData.GetSpellConfig(sid);
        }

        public string Descript
        {
            get
            {
                return SpellConfig.GetDescript(lv);
            }
        }

        public int GetSellRate()
        {
            if (SpellConfig.Star >= 7)
            {
                return 0;
            }
            return 18 - SpellConfig.Star * 2;
        }

        public CardProductMarkTypes GetSellMark()
        {
            CardProductMarkTypes mark = CardProductMarkTypes.Null;
            if (SpellConfig.Star > 4)
            {
                mark = CardProductMarkTypes.Only;
            }
            else if (SpellConfig.Star < 2 && MathTool.GetRandom(10) > 7)
            {
                mark = CardProductMarkTypes.Sale;
            }
            else
            {
                int roll = MathTool.GetRandom(10);
                if (roll == 0)
                    mark = CardProductMarkTypes.Hot;
                else if (roll == 1)
                    mark = CardProductMarkTypes.Gold;
            }
            return mark;
        }

        public void UpgradeToLevel(int level, byte quality)
        {
            lv = level;
        }
    }
}
