using System;
using System.Windows.Forms;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.Controler.Battle.Data.Players;

namespace TaleofMonsters.Controler.Battle.Components
{
    internal sealed partial class CardList : UserControl, ICardList
    {
        private bool show;
        private int margin = 5;
        private int maxCards;
        private int selectId;
        private ActiveCard[] cards;
        private Player owner;

        public int Marginp
        {
            get { return margin; }
            set { margin = value; }
        }

        public int MaxCards
        {
            get { return maxCards; }
            set
            {
                maxCards = value;
                cards = new ActiveCard[maxCards];
                for (int i = 0; i < maxCards; i++)
                    cards[i] = ActiveCards.NoneCard;
            }
        }

        public CardList()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public void Init()
        {
            show = true;
            Invalidate();
        }

        public void AddCard(ActiveCard card)
        {
            for (int i = 0; i < maxCards - 1; i++)
                cards[i] = cards[i + 1];
            cards[maxCards - 1] = card;
            Invalidate();
        }

        private void CardList_Paint(object sender, PaintEventArgs e)
        {
            if (!show)
                return;

            int heg = Height;
            int wid = heg;
            for (int i = 0; i < maxCards; i++)
            {
                e.Graphics.DrawImage(CardAssistant.GetCard(cards[i].CardId).GetCardImage(60, 60), (wid + margin)*i, 0, wid, heg);
            }
        }

        #region ICardList接口

        public void DisSelectCard()
        {
        }

        public void UpdateSlot(ActiveCard[] pCards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = ActiveCards.NoneCard;
            }
            for (int i = 0; i < Math.Min(pCards.Length, cards.Length); i++)
            {
                if (cards[i].Id != pCards[i].Id)
                    cards[i] = pCards[i];
            }
            Invalidate();
        }

        public int GetSelectId()
        {
            return selectId;
        }

        public ActiveCard GetSelectCard()
        {
            return cards[selectId - 1];
        }

        public void SetSelectId(int value)
        {
            selectId = value;
        }

        public void SetOwner(Player player)
        {
            owner = player;
        }

        public void UpdateCardMana()
        {
            for (int i = 0; i < maxCards; i++)
            {
                cards[i].UpdateCardMana(owner.Hero);
            }
        }

        public int GetCapacity()
        {
            return cards.Length;
        }
        #endregion
    }
}
