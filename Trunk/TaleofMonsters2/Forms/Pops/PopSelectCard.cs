using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Cards;

namespace TaleofMonsters.Forms.Pops
{
    internal partial class PopSelectCard : Form
    {
        private int[] cids;
        private int[] activeCids;
        private int sel = -1;

        private int fil_type;
        private int fil_order;

        public PopSelectCard()
        {
            InitializeComponent();
            BackgroundImage = PicLoader.Read("System", "DeckChoose.PNG");
            FormBorderStyle = FormBorderStyle.None;

            Init();
        }

        private void Init()
        {
            cids = new int[50];
            for (int i = 0; i < 50; i++)
            {
                cids[i] = UserProfile.InfoCard.SelectedDeck.GetCardAt(i);
            }
            sel = 0;
            radioButton1.Checked = true;
            comboBoxType.SelectedIndex = 0;

            RegetCards();
        }

        private void RegetCards()
        {
            List<DeckCard> cards = new List<DeckCard>();
            foreach (int cid in cids)
            {
                DeckCard card = UserProfile.InfoCard.GetDeckCardById(cid);
                if ((int)card.GetCardType() == fil_type)
                {
                    cards.Add(card);
                }
            }

            if (cards.Count <= 0)
            {
                sel = -1;
            }
            else
            {
                switch (fil_order)
                {
                    case 0: cards.Sort(new CompareDeckCardByLevel()); break;
                    case 1: cards.Sort(new CompareDeckCardByLevel()); cards.Reverse(); break;
                    case 2: cards.Sort(new CompareDeckCardByExp()); cards.Reverse(); break;
                    case 3: cards.Sort(new CompareDeckCardByExp()); break;
                }

                activeCids = new int[cards.Count];
                for (int i = 0; i < activeCids.Length; i++)
                {
                    activeCids[i] = cards[i].Id;
                }
                sel = 0;
            }

            Invalidate();
        }

        private void MessageBoxEx_Paint(object sender, PaintEventArgs e)
        {
            if (sel >= 0)
            {
                DeckCard card = UserProfile.InfoCard.GetDeckCardById(activeCids[sel]);
                e.Graphics.DrawImage(CardAssistant.GetCard(card.BaseId).GetCardImage(100, 100), 38, 62, 100, 100);

                Font font = new Font("Arial", 7, FontStyle.Regular);
                const string stars = "★★★★★★★★★★";
                e.Graphics.DrawString(stars.Substring(10 - CardAssistant.GetCard(card.BaseId).Star), font, Brushes.Yellow, 38, 62);

                e.Graphics.DrawString(string.Format("{0:00}", card.Level), font, Brushes.Gold, 38, 150);
                e.Graphics.FillRectangle(Brushes.Wheat, 53, 153, card.Exp * 80 / ExpTree.GetNextRequiredCard(card.Level), 5);
                e.Graphics.DrawRectangle(Pens.WhiteSmoke, 53, 153, 80, 5);
                font.Dispose();
            }
        }

        public new static int Show()
        {
            PopSelectCard mb = new PopSelectCard();
            mb.ShowDialog();
            return mb.sel == -1 ? -1 : mb.activeCids[mb.sel];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sel = -1;
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fil_type = int.Parse((sender as Control).Tag.ToString());
            RegetCards();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fil_order = comboBoxType.SelectedIndex;
            RegetCards();
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (sel>=0)
            {
                sel--;
                sel = (sel + activeCids.Length) % activeCids.Length;
                Invalidate();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                sel++;
                sel = (sel + activeCids.Length)%activeCids.Length;
                Invalidate();
            }
        }

        private void buttonRand_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                sel = NarlonLib.Math.MathTool.GetRandom(activeCids.Length);
                Invalidate();
            }
        }
    }
}