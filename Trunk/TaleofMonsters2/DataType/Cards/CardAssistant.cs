using System.Drawing;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Spells;
using TaleofMonsters.DataType.Cards.Weapons;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.Controler.Battle.Data.MemMonster;

namespace TaleofMonsters.DataType.Cards
{
    internal class CardAssistant
    {
        public static void DrawBase(Graphics g, int cid, int x, int y, int width, int height, bool isSelected)
        {
            Image img = GetCard(cid).GetCardImage(60, 60);
            int subtype = GetCard(cid).Type;
            if (isSelected)
            {
                g.DrawImage(img, x, y, width, height);
            }
            else
            {
                g.FillRectangle(PaintTool.GetBrushByAttribute(subtype), x + 1, y + 1, width - 2, height - 2);
                g.DrawImage(img, x + 3, y + 3, width - 6, height - 6);
            }
        }

        public static Card GetCard(int cid)
        {
            switch (cid / 10000)
            {
                case 1: return new MonsterCard(new Monster(cid));
                case 2: return new WeaponCard(new Weapon(cid));
                case 3: return new SpellCard(new Spell(cid));
            }
            return SpecialCards.NullCard;
        }

        public static void CheckRate(LiveMonster lm, DeckCard card, ref int rate)
        {
            if (lm.CardManaRateW!=0&&card.GetCardType()== CardTypes.Weapon)
            {
                rate += lm.CardManaRateW;
            }
            if (lm.CardManaRateS!= 0 && card.GetCardType() == CardTypes.Spell)
            {
                rate += lm.CardManaRateS;
            }
        }
    }
}
