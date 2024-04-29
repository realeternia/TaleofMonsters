﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Peoples;
using TaleofMonsters.Forms.Items.Core;

namespace TaleofMonsters.Forms.MagicBook
{
    internal sealed partial class PeopleDropViewForm : BasePanel
    {
        private const int cardWidth = 60;
        private const int cardHeight = 60;
        private const int xCount = 8;
        private const int yCount = 6;
        private const int cardCount = xCount*yCount;
        private int totalCount;
        private int page;
        private bool show;
        private int tar = -1;
        private int sel = -1;
        private List<int> people;
        private Bitmap tempImage;
        private bool isDirty = true;
        private CardDetail cardDetail;

        public PeopleDropViewForm()
        {
            InitializeComponent();
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonNext.ImageNormal = PicLoader.Read("System", "NextButton.JPG");
            this.bitmapButtonNext.ImageMouseOver = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonNext.ImagePressed = PicLoader.Read("System", "NextButtonOn.JPG");
            this.bitmapButtonPre.ImageNormal = PicLoader.Read("System", "PreButton.JPG");
            this.bitmapButtonPre.ImageMouseOver = PicLoader.Read("System", "PreButtonOn.JPG");
            this.bitmapButtonPre.ImagePressed = PicLoader.Read("System", "PreButtonOn.JPG"); 
            tempImage = new Bitmap(cardWidth * xCount, cardHeight * yCount);
            cardDetail = new CardDetail(this, cardWidth * xCount + 65, 35, cardHeight * 7 + 105);
            nlClickLabel1.Location = new Point(70, cardHeight * yCount + 65);
            nlClickLabel1.Size = new Size(cardWidth * xCount - 10, 70 + (7 - yCount) * cardHeight);
        }

        internal override void Init()
        {
            base.Init();
            show = true;
            page = 0;
            people = new List<int>();
            foreach (PeopleConfig peopleConfig in ConfigData.PeopleDict.Values)
            {
                if (PeopleBook.IsPeople(peopleConfig.Id) && peopleConfig.Emethod != "")
                {
                    people.Add(peopleConfig.Id);
                }
            }
            totalCount = people.Count;
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            bitmapButtonPre.Enabled = page > 0;
            bitmapButtonNext.Enabled = (page + 1) * cardCount < totalCount;
        }

        private void buttonPre_Click(object sender, EventArgs e)
        {
            page--;
            if (page < 0)
            {
                page++;
                return;
            }
            tar = -1;
            sel = -1;
            cardDetail.SetInfo(-1);
            UpdateButtonState();
            isDirty = true;
            Invalidate(new Rectangle(65, 35, cardWidth * xCount+200, 630));
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            page++;
            if (page > (totalCount - 1) / cardCount)
            {
                page--;
                return;
            }
            tar = -1;
            sel = -1;
            cardDetail.SetInfo(-1);
            UpdateButtonState();
            isDirty = true;
            Invalidate(new Rectangle(65, 35, cardWidth * xCount+200, 630));
        }

        private void PeopleDropViewForm_Click(object sender, EventArgs e)
        {
            if (tar != -1)
            {
                sel = tar;
                cardDetail.SetInfo(-1);

                nlClickLabel1.ClearLabel();
                foreach (DropCardConfig dropCardConfig in ConfigData.DropCardDict.Values)
                {
                    if (dropCardConfig.Pid == people[tar])
                    {
                        if (dropCardConfig.Pid < 0)
                            continue;
                        Card card = CardAssistant.GetCard(dropCardConfig.CardId);
                        string cardstr = string.Format("{0}{1}%", card.Name, dropCardConfig.Rate);
                        nlClickLabel1.AddLabel(cardstr, new DeckCard(1, dropCardConfig.CardId, 1, 0));
                    }
                }
                Invalidate(new Rectangle(65, 35, cardWidth * xCount + 200, 630));
            }
        }

        private void PeopleDropViewForm_MouseMove(object sender, MouseEventArgs e)
        {
            cardDetail.CheckMouseMove(e.X, e.Y);

            int truex = e.X - 65;
            int truey = e.Y - 35;
            if (truex > 0 && truex < xCount * cardWidth && truey > 0 && truey < yCount * cardHeight)
            {
                int temp = truex / cardWidth + truey / cardHeight * xCount + cardCount * page;
                if (temp != tar)
                {
                    if (temp < totalCount)
                    {
                        tar = temp;
                    }
                    else
                    {
                        tar = -1;
                    }
                    Invalidate(new Rectangle(65, 35, cardWidth * xCount, cardHeight * yCount));
                }
            }
            else
            {
                if (tar != -1)
                {
                    tar = -1;
                    Invalidate(new Rectangle(65, 35, cardWidth * xCount, cardHeight * yCount));
                }
            }
        }

        private void nlClickLabel1_SelectionChange(Object value)
        {
            cardDetail.SetInfo(value as DeckCard);
            Invalidate(new Rectangle(65, 35, cardWidth * xCount + 200, 630));
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MonsterSkillViewForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(" 对战掉落 ", font, Brushes.White, 320, 8);
            font.Dispose();

            Font fontblack = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(245, 244, 242)), 65, cardHeight * yCount + 40, cardWidth * xCount, 100 + (7 - yCount) * cardHeight);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(190, 175, 160)), 65, cardHeight * yCount + 40, cardWidth * xCount, 20);
            e.Graphics.DrawString("所有卡组", fontblack, Brushes.White, 65, cardHeight * yCount + 42);
            fontblack.Dispose();

            cardDetail.Draw(e.Graphics);

            if (show)
            {
                int pages = totalCount / cardCount + 1;
                int cardLimit = (page < pages - 1) ? cardCount : (totalCount % cardCount);
                int former = cardCount * page + 1;
                if (isDirty)
                {
                    tempImage.Dispose();
                    tempImage = new Bitmap(cardWidth * xCount, cardHeight * yCount);
                    Graphics g2 = Graphics.FromImage(tempImage);
                    for (int i = former - 1; i < former + cardLimit - 1; i++)
                    {
                        g2.DrawImage(PeopleBook.GetPersonImage(people[i]), (i % xCount) * cardWidth, ((i / xCount) % yCount) * cardHeight, cardWidth, cardHeight);
                    }
                    g2.Dispose();
                    isDirty = false;
                }
                e.Graphics.DrawImage(tempImage, 65, 35);

                if (sel != -1 && sel < totalCount)
                {
                    SolidBrush yellowbrush = new SolidBrush(Color.FromArgb(80, Color.Lime));
                    int x = (sel%xCount)*cardWidth + 65;
                    int y = ((sel/xCount)%yCount)*cardHeight + 35;
                    e.Graphics.FillRectangle(yellowbrush, x, y, cardWidth, cardHeight);
                    yellowbrush.Dispose();

                    Pen yellowpen = new Pen(Brushes.Lime, 3);
                    e.Graphics.DrawRectangle(yellowpen, x, y, cardWidth, cardHeight);
                    yellowpen.Dispose();
                }
                if (tar != -1 && tar < totalCount)
                {
                    int x = (tar % xCount) * cardWidth + 65;
                    int y = ((tar / xCount) % yCount) * cardHeight + 35;
                    SolidBrush yellowbrush = new SolidBrush(Color.FromArgb(80, Color.Yellow));
                    e.Graphics.FillRectangle(yellowbrush, x, y, cardWidth, cardHeight);
                    yellowbrush.Dispose();

                    Pen yellowpen = new Pen(Brushes.Yellow, 3);
                    e.Graphics.DrawRectangle(yellowpen, x, y, cardWidth, cardHeight);
                    yellowpen.Dispose();
                }
            }
        }
    }
}