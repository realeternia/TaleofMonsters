using System.Collections.Generic;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.Core;

namespace TaleofMonsters.DataType.Decks
{
    internal class DeckCard : INlSerlizable
    {
        private int id;
        private int baseId;
        private int level;
        private int exp;
        private string name;
        private byte quality;

        public DeckCard()
        {
            name = "";
        }

        public DeckCard(int id, int baseId, int level, int exp)
        {
            this.id = id;
            this.baseId = baseId;
            this.level = level;
            this.exp = exp;
            name = "";
        }

        public int Id
        {
            get { return id; }
        }

        public int BaseId
        {
            get { return baseId; }
        }

        public int Level
        {
            get { return level; }
        }

        public int Exp
        {
            get { return exp; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public byte Quality
        {
            get { return quality; }
            set { quality = value; }
        }

        public bool AddExp(int addon)
        {
            if (level>= ExpTree.MaxLevel)
            {
                return false;
            }

            exp += addon;
            bool levelUp = false;
            while (true)
            {
                int expNeed = ExpTree.GetNextRequiredCard(level);
                if (exp < expNeed)
                {
                    break;
                }
                exp -= expNeed;
                level++;
                levelUp = true;
            }

            return levelUp;
        }

        public CardTypes GetCardType()
        {
            switch (baseId / 10000)
            {
                case 1: return CardTypes.Monster;
                case 2: return CardTypes.Weapon;
                case 3: return CardTypes.Spell;
            }
            return CardTypes.Null;
        }

        internal int Mana
        {
            get
            {
                if (GetCardType() == CardTypes.Monster)
                {
                    return 0;
                }
                return CardAssistant.GetCard(baseId).Cost;
            }
        }

        internal int Anger
        {
            get
            {
                if (GetCardType() != CardTypes.Monster)
                {
                    return 0;
                }
                return CardAssistant.GetCard(baseId).Cost;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", id, baseId);
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(id);
            bw.Write(baseId);
            bw.Write(level);
            bw.Write(exp);
            bw.Write(name);
            bw.Write(quality);
        }

        public void Read(System.IO.BinaryReader br)
        {
            id = br.ReadInt32();
            baseId = br.ReadInt32();
            level = br.ReadInt32();
            exp = br.ReadInt32();
            name = br.ReadString();
            quality = br.ReadByte();
        }

        #endregion
    }

    internal class CompareDeckCardByType : IComparer<DeckCard>
    {
        #region IComparer<CardDescript> 成员

        public int Compare(DeckCard cx, DeckCard cy)
        {
            if (cx.BaseId == cy.BaseId && cy.BaseId == 0)
            {
                return 0;
            }
            if (cy.BaseId == 0)
            {
                return -1;
            }
            if (cx.BaseId == 0)
            {
                return 1;
            }
            int typex = CardAssistant.GetCard(cx.BaseId).Type;
            int typey = CardAssistant.GetCard(cy.BaseId).Type;
            if (typex != typey)
            {
                return typex.CompareTo(typey);
            }

            if (cx.BaseId != cy.BaseId)
            {
                return cx.BaseId.CompareTo(cy.BaseId);
            }

            return (cx.Id.CompareTo(cy.Id));
        }

        #endregion
    }

    internal class CompareDeckCardByLevel : IComparer<DeckCard>
    {
        #region IComparer<CardDescript> 成员

        public int Compare(DeckCard cx, DeckCard cy)
        {
            if (cx.Level != cy.Level)
            {
                return cx.Level.CompareTo(cy.Level);
            }

            return (cx.Id.CompareTo(cy.Id));
        }

        #endregion
    }

    internal class CompareDeckCardByExp : IComparer<DeckCard>
    {
        #region IComparer<CardDescript> 成员

        public int Compare(DeckCard cx, DeckCard cy)
        {
            int expx = (ExpTree.GetNextRequiredCard(cx.Level) - cx.Exp) * 100 / ExpTree.GetNextRequiredCard(cx.Level);
            int expy = (ExpTree.GetNextRequiredCard(cy.Level) - cy.Exp) * 100 / ExpTree.GetNextRequiredCard(cy.Level);

            if (expx != expy)
            {
                return expx.CompareTo(expy);
            }

            return (cx.Id.CompareTo(cy.Id));
        }

        #endregion
    }
}
