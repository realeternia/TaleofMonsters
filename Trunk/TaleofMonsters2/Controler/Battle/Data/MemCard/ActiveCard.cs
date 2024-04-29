using System;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Decks;

namespace TaleofMonsters.Controler.Battle.Data.MemCard
{
    internal class ActiveCard
    {
        private int costRate;
        private readonly DeckCard card;

        internal ActiveCard(DeckCard card)
        {
            this.card = card;
            costRate = 100;
        }

        internal ActiveCard(int id, int baseid, int level, int exp)
        {
            card = new DeckCard(id, baseid, level, exp);
            costRate = 100;
        }

        internal int Id //Î¨Ò»µÄid
        {
            get { return Card.Id; }
        }

        internal int CardId //¿¨Æ¬ÅäÖÃµÄid
        {
            get { return Card.BaseId; }
        }

        internal CardTypes CardType
        {
            get
            {
                return Card.GetCardType();
            }
        }

        internal int Level
        {
            get { return Card.Level; }
        }

        internal int CostRate
        {
            get { return costRate; }
        }

        internal DeckCard Card
        {
            get { return card; }
        }

        internal int GetRequiredMana()
        {
            return (int)Math.Ceiling((double)card.Mana * costRate / 100);
        }

        internal int GetRequiredAnger()
        {
            return (int)Math.Ceiling((double)card.Anger * costRate / 100);
        }

        internal void UpdateCardMana(LiveMonster hero)
        {
            costRate = 100;
            if (hero != null && hero.IsAlive)
            {
                CardAssistant.CheckRate(hero, card, ref costRate);
            }
        }

        internal ActiveCard GetCopy()
        {
            DeckCard dc = new DeckCard(World.WorldInfoManager.GetCardFakeId(), Card.BaseId, Card.Level, Card.Exp);
            return new ActiveCard(dc);
        }

        public static bool operator ==(ActiveCard rec1, ActiveCard rec2)
        {
            return Equals(rec1, rec2);
        }

        public static bool operator !=(ActiveCard rec1, ActiveCard rec2)
        {
            return !Equals(rec1, rec2);
        }

        public override int GetHashCode()
        {
            return Card.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            ActiveCard rec = (ActiveCard) obj;
            if (rec.Card == Card)
            {
                return true;
            }
            return false;
        }    
    }   
}
