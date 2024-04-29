﻿using System;
using System.Drawing;
using System.Windows.Forms;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.Forms.Items.Regions;

namespace TaleofMonsters.Forms.Items
{
    internal class ChangeCardItem
    {
        private int index;
        private int card1;
        private int card2;
        private bool show;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        private int x, y, width, height;
        private Control parent;
        private BitmapButton bitmapButtonBuy;
        private Color backColor;

        public void Init(int idx)
        {
            index = idx;

            switch (index)
            {
                case 0:
                case 1: backColor = Color.Black; break;
                case 2:
                case 3: backColor = Color.FromArgb(0, 0, 60); break;
                case 4:
                case 5: backColor = Color.FromArgb(0, 60, 0); break;
                case 6:
                case 7: backColor = Color.FromArgb(60, 60, 0); break;
            }

            virtualRegion = new VirtualRegion(parent);
            virtualRegion.AddRegion(new PictureAnimRegion(1, x + 5, y + 8, 40, 40, 1, VirtualRegionCellType.Card, 0));
            virtualRegion.AddRegion(new PictureAnimRegion(2, x + 97, y + 8, 40, 40, 2, VirtualRegionCellType.Card, 0));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        public void RefreshData()
        {
            MemChangeCardData change = UserProfile.InfoWorld.GetChangeCardData(index);
            if (!change.IsEmpty())
            {
                card1 = change.id1;
                card2 = change.id2;
                bitmapButtonBuy.Visible = !change.used;
                bitmapButtonBuy.Enabled = UserProfile.InfoCard.GetCardCount(card1) > 0;

                virtualRegion.SetRegionInfo(1, card1);
                virtualRegion.SetRegionInfo(2, card2);
                show = true;
            }
            else
            {
                card1 = -1;
                card2 = -1;
                virtualRegion.SetRegionInfo(1, 0);
                virtualRegion.SetRegionInfo(2, 0);
                bitmapButtonBuy.Visible = false;
                show = false;
            }

            parent.Invalidate(new Rectangle(x, y, width, height));
        }

        public ChangeCardItem(UserControl prt, int x, int y, int width, int height)
        {
            parent = prt;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.bitmapButtonBuy=new BitmapButton();
            bitmapButtonBuy.Location= new Point(x+152,y+30);
            bitmapButtonBuy.Size=new Size(35,20);
            this.bitmapButtonBuy.Click += new System.EventHandler(this.pictureBoxBuy_Click);
            this.bitmapButtonBuy.ImageNormal = PicLoader.Read("Button", "BuyButton.JPG");
            this.bitmapButtonBuy.ImageMouseOver = PicLoader.Read("Button", "BuyButtonOn.JPG");
            this.bitmapButtonBuy.ImagePressed = PicLoader.Read("Button", "BuyButtonOn.JPG");
            this.bitmapButtonBuy.FlatStyle = FlatStyle.Flat;
            this.bitmapButtonBuy.StretchImage = true;
            this.bitmapButtonBuy.Text = "交换物品";
            parent.Controls.Add(bitmapButtonBuy);
        }

        private void virtualRegion_RegionEntered(int info, int mx, int my, int key)
        {
            if (info == 1 && card1 != -1)
            {
                Image image = CardAssistant.GetCard(card1).GetPreview(CardPreviewType.Normal, new int[] { });
                tooltip.Show(image, parent, mx, my, card1);
            }
            else if (info == 2 && card2 != -1)
            {
                Image image = CardAssistant.GetCard(card2).GetPreview(CardPreviewType.Normal, new int[] { });
                tooltip.Show(image, parent, mx, my, card2);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(parent);
        }

        private void pictureBoxBuy_Click(object sender, EventArgs e)
        {
            if (UserProfile.InfoCard.GetCardCount(card1) <= 0)
            {
                return;
            }

            UserProfile.InfoCard.RemoveCard(card1);//error
            UserProfile.InfoCard.AddCard(card2);
            UserProfile.InfoWorld.RemoveChangeCardData(index);

            RefreshData();
        }

        public void Draw(Graphics g)
        {
            SolidBrush sb= new SolidBrush(backColor);
            g.FillRectangle(sb, x, y, width, height);
            sb.Dispose();
            g.DrawRectangle(Pens.White, x, y, width - 1,height - 1);

            if (show)
            {
                Image img = PicLoader.Read("System", "ArrowS.PNG");
                g.DrawImage(img, x + 54,y+19, 35, 20);
                img.Dispose();

                if (!bitmapButtonBuy.Visible)
                {
                    Font font = new Font("微软雅黑", 10, FontStyle.Regular);
                    g.DrawString("完成", font, Brushes.LightGreen, x + 152, y + 30);
                    font.Dispose();
                }

                virtualRegion.Draw(g);
            }
        }
    }
}
