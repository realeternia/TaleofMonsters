using System;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards;

namespace TaleofMonsters.Controler.Battle.Components
{
    internal sealed partial class CardSlot : UserControl
    {
        private bool mouseOn;
        private bool isSelected;
        private ActiveCard acard;
        private Card card;

        internal delegate void CardSlotEventHandler(Object sender, EventArgs e);
        internal event CardSlotEventHandler SelectionChange;

        internal ActiveCard ACard
        {
            get { return acard; }
        }

        internal Card Card
        {
            get { return card; }
        }

        internal bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; Invalidate(); }
        }

        internal CardSlot()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        internal void SetSlotCard(ActiveCard tcard)
        {
            acard = tcard;
            card = CardAssistant.GetCard(tcard.CardId);
            card.UpdateToLevel(acard.Card);
            Invalidate();
        }

        private void CardSlot_MouseEnter(object sender, EventArgs e)
        {
            mouseOn = true;
            Invalidate();
        }

        private void CardSlot_MouseLeave(object sender, EventArgs e)
        {
            mouseOn = false;
            Invalidate();
        }

        private void CardSlot_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isSelected = true;
                SelectionChange(this, e);
            }
        }

        private void CardSlot_Paint(object sender, PaintEventArgs e)
        {
            if (card != null)
            {
                CardMouseState state = !Enabled ? CardMouseState.Disable : (mouseOn ? CardMouseState.MouseOn : CardMouseState.Normal);
                Draw(e.Graphics, state);


#if DEBUG
                Font font = new Font("Arial", 7, FontStyle.Regular);
                e.Graphics.DrawString(acard.Id.ToString(), font, Brushes.White, 0, 20);
                font.Dispose();
#endif
            }
        }

        internal void Draw(Graphics g, CardMouseState mouse)
        {
            if (card is SpecialCard)
            {
                Image img2 = Card.GetCardImage(120, 120);
                g.DrawImage(img2, new Rectangle(0, 10, 120, 120), 0, 0, img2.Width, img2.Height, GraphicsUnit.Pixel);
                return;
            }

            int y = 0;
            g.FillRectangle(isSelected ? Brushes.LightGreen : Brushes.DimGray, new Rectangle(0, 0, 120, 150));
            if (mouse != CardMouseState.MouseOn)
                y = 10;
            Rectangle destBack = new Rectangle(0, y, 120, 120);
            Image img = Card.GetCardImage(120, 120);
            if (mouse == CardMouseState.Disable)
                g.DrawImage(img, destBack, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, HSImageAttributes.ToGray);
            else
                g.DrawImage(img, destBack, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            Font font = new Font("Arial", 7, FontStyle.Regular);
            const string stars = "★★★★★★★★★★";
            g.DrawString(stars.Substring(10 - Card.Star), font, Brushes.Black, 1, y + 2);
            g.DrawString(stars.Substring(10 - Card.Star), font, Brushes.Yellow, 0, y);
            font.Dispose();

            font = new Font("宋体", 9, FontStyle.Regular);
            g.DrawString(Card.Name, font, Brushes.Gray, 1, mouse != CardMouseState.MouseOn ? 117 : 122);
            g.DrawString(Card.Name, font, Brushes.White, 0, mouse != CardMouseState.MouseOn ? 116 : 121);
            font.Dispose();
            font = new Font("宋体", 10, FontStyle.Bold);

            if (card.GetCardType() == CardTypes.Monster)
            {
                string angerString;
                int anger = acard.Card.Anger;
                int manaNeed = (int)Math.Ceiling((double)anger * acard.CostRate / 100);
                if (acard.CostRate == 100)
                    angerString = string.Format("{0}AP", anger);
                else if (acard.CostRate > 100)
                    angerString = string.Format("{0}+{1}AP", anger, manaNeed - anger);
                else
                    angerString = string.Format("{0}-{1}AP", anger, anger - manaNeed);
                int manaLen = (int)g.MeasureString(angerString, font).Width;
                g.DrawString(angerString, font, Brushes.Gray, 120 - manaLen, mouse != CardMouseState.MouseOn ? 117 : 122);
                g.DrawString(angerString, font, Brushes.Red, 119 - manaLen, mouse != CardMouseState.MouseOn ? 116 : 121);
            }
            else
            {
                string manaString;
                int mana = acard.Card.Mana;
                int manaNeed = (int)Math.Ceiling((double)mana * acard.CostRate / 100);
                if (acard.CostRate == 100)
                    manaString = string.Format("{0}MP", mana);
                else if (acard.CostRate > 100)
                    manaString = string.Format("{0}+{1}MP", mana, manaNeed - mana);
                else
                    manaString = string.Format("{0}-{1}MP", mana, mana - manaNeed);
                int manaLen = (int)g.MeasureString(manaString, font).Width;
                g.DrawString(manaString, font, Brushes.Gray, 120 - manaLen, mouse != CardMouseState.MouseOn ? 117 : 122);
                g.DrawString(manaString, font, Brushes.Blue, 119 - manaLen, mouse != CardMouseState.MouseOn ? 116 : 121);
            }

            if (Card.GetCardType() == CardTypes.Weapon)
            {
                g.DrawImage(HSIcons.GetIconsByEName("wep" + (Card.Type-8)), 100, y + 2, 16, 16);
            }
            else if (Card.GetCardType() == CardTypes.Spell)
            {
                g.DrawImage(HSIcons.GetIconsByEName("spl" + (Card.Type - 12)), 100, y + 2, 16, 16);
            }
        }

    }
}
