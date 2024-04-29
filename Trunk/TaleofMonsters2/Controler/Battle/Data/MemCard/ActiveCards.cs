using System.Collections.Generic;
using NarlonLib.Math;
using TaleofMonsters.DataType.Decks;

namespace TaleofMonsters.Controler.Battle.Data.MemCard
{
    internal class ActiveCards
    {
        private int index;
        private List<ActiveCard> cards;
        internal static ActiveCard NoneCard = new ActiveCard(new DeckCard(0, 0, 0, 0));

        internal ActiveCards()
        {
            index = 0;
        }

        internal ActiveCards(DeckCard[] itsCards)
        {
            ActiveCard[] tcards = new ActiveCard[50];
            for (int i = 0; i < 50; i++)
            {
                tcards[i] = new ActiveCard(itsCards[i]);
            }
            for (int i = 0; i < 100; i++)
            {
                int x = MathTool.GetRandom(50);
                int y = MathTool.GetRandom(50);
                ActiveCard temp = tcards[x];
                tcards[x] = tcards[y];
                tcards[y] = temp;
            }
            cards = new List<ActiveCard>(tcards);
            index = 0;
        }

        internal ActiveCards GetCopy()
        {
            ActiveCards tcards = new ActiveCards();
            tcards.cards = new List<ActiveCard>();
            foreach (ActiveCard activeCard in cards)
            {
                tcards.cards.Add(new ActiveCard(World.WorldInfoManager.GetCardFakeId(), activeCard.CardId, activeCard.Level, 0));
            }
            return tcards;
        }

        internal ActiveCard GetNextCard()
        {
            if (cards.Count == 0)
            {
                return NoneCard;
            }

            int rt = index;
            if (++index >= cards.Count)
                index = 0;
            return cards[rt];
        }
    }
}
