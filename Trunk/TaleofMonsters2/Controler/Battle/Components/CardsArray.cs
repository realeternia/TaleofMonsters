using System;
using System.Windows.Forms;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Cards;

namespace TaleofMonsters.Controler.Battle.Components
{
    internal partial class CardsArray : UserControl, ICardList
    {
        public delegate void CardArrayEventHandler(Object sender, EventArgs e);
        public event CardArrayEventHandler SelectionChange;

        int selectId;
        CardSlot[] cards = new CardSlot[6];
        private Player owner;

        public CardsArray()
        {
            InitializeComponent();
        }

        public void Init()
        {
            for (int i = 1; i <= 6; i++)
            {
                cards[i - 1] = (Controls["cardSlot" + i] as CardSlot);
                cards[i - 1].SetSlotCard(ActiveCards.NoneCard);
            }
        }

        private void cardSlot1_SelectionChange(object sender, EventArgs e)
        {
            if (!Enabled)
            {
                return;
            }

            for (int i = 0; i <= 5; i++)
            {
                if (cards[i] == sender)
                {
                    cards[i].IsSelected = true;
                    selectId = i + 1;
                }
                else
                    cards[i].IsSelected = false;
            }
            SelectionChange(this, e);
        }

        public Card GetTargetCard()
        {
            if (selectId > 0)
                return cards[selectId - 1].Card;
            return SpecialCards.NullCard;
        }

        #region ICardList接口
        public void DisSelectCard()
        {
            if (selectId != 0)
                cards[selectId - 1].IsSelected = false;
            selectId = 0;
            SelectionChange(null, null);
        }

        public void UpdateSlot(ActiveCard[] pCards)
        {
            for (int i = 0; i < 6; i++)
            {
                if (cards[i].ACard.Id != pCards[i].Id)
                    cards[i].SetSlotCard(pCards[i]);
            }
        }

        public int GetSelectId()
        {
            return selectId;
        }

        public ActiveCard GetSelectCard()
        {
            if (selectId > 0)
                return cards[selectId - 1].ACard;
            return null;
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
            for (int i = 0; i < 6; i++)
            {
                cards[i].ACard.UpdateCardMana(owner.Hero);
                cards[i].Invalidate();
            }
        }

        public int GetCapacity()
        {
            return cards.Length;
        }
        #endregion
    }
}
