﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.CardPieces;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.Forms.Items.Core;
using ConfigDatas;

namespace TaleofMonsters.Forms.MagicBook
{
    internal sealed partial class DropItemViewerForm : BasePanel
    {
        private const int cardWidth = 50;
        private const int cardHeight = 50;
        private const int xCount = 10;
        private const int yCount = 9;
        private const int cardCount = xCount * yCount;
        private int totalCount;
        private int page;
        private bool show;
        private int tar = -1;
        private int sel = -1;
        private List<int> items;
        private Bitmap tempImage;
        private bool isDirty = true;
        private ItemDetail itemDetail;

        public DropItemViewerForm()
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
            tempImage = new Bitmap(cardWidth*xCount, cardHeight*yCount);

            itemDetail = new ItemDetail(cardWidth * xCount + 65, 35, cardHeight * yCount+93);
            nlClickLabel1.Location = new Point(75, cardHeight * yCount + 60);
            nlClickLabel1.Size = new Size(cardWidth * xCount - 20, 63);
        }

        internal override void Init()
        {
            base.Init();
            show = true;
            page = 0;

            items = new List<int>();
            foreach (HItemConfig itemConfig in ConfigData.HItemDict.Values)
            {
                if (itemConfig.SubType == HItemTypes.Material)
                    items.Add(itemConfig.Id);
            }
            totalCount = items.Count;

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
            itemDetail.ItemId = -1;
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
            itemDetail.ItemId = -1;
            UpdateButtonState();
            isDirty = true;
            Invalidate(new Rectangle(65, 35, cardWidth * xCount+200, 630));
        }

        private void DropItemViewerForm_Click(object sender, EventArgs e)
        {
            if (tar != -1)
            {
                sel = tar;

                itemDetail.ItemId = items[tar];
                itemDetail.Type = 1;
                Invalidate(new Rectangle(65 + cardWidth * xCount, 35, 200, 630));

                nlClickLabel1.ClearLabel();
                int[] cardIds = CardPieceBook.GetCardIdsByItemId(items[tar]);
                foreach (int cid in cardIds)
                {
                    nlClickLabel1.AddLabel(CardAssistant.GetCard(cid).Name, cid);
                }
                nlClickLabel1.Invalidate();
            }
        }

        private void DropItemViewerForm_MouseMove(object sender, MouseEventArgs e)
        {
            int truex = e.X - 65;
            int truey = e.Y - 35;
            if (truex > 0 && truex < xCount * cardWidth && truey > 0 && truey < yCount * cardHeight)
            {
                int temp = truex / cardWidth + truey / cardHeight * xCount + cardCount * page;
                if (temp != tar)
                {
                    tar = temp < totalCount ? temp : -1;
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
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MonsterSkillViewForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(" 全材料 ", font, Brushes.White, 320, 8);
            font.Dispose();

            Font fontblack = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(245, 244, 242)), 65, cardHeight * yCount+35, cardWidth * xCount, 93);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(190, 175, 160)), 65, cardHeight * yCount+35, cardWidth * xCount, 20);
            e.Graphics.DrawString("掉落怪物", fontblack, Brushes.White, 65, cardHeight * yCount + 37);
            fontblack.Dispose();

            itemDetail.Draw(e.Graphics);

            if (show)
            {
                int pages = totalCount / cardCount + 1;
                int cardLimit = (page < pages - 1) ? cardCount : (totalCount % cardCount);
                int former = cardCount * page + 1;
                if (isDirty)
                {
                    tempImage.Dispose();
                    tempImage = new Bitmap(cardWidth * xCount, cardHeight * yCount);
                    Graphics g = Graphics.FromImage(tempImage);
                    for (int i = former - 1; i < former + cardLimit - 1; i++)
                    {
                        g.DrawImage(HItemBook.GetHItemImage(items[i]), (i % xCount) * cardWidth, ((i / xCount) % yCount) * cardHeight, cardWidth, cardHeight);
                    }
                    g.Dispose();
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