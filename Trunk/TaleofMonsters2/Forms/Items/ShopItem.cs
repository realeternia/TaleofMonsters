using System;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms.Items.Regions;

namespace TaleofMonsters.Forms.Items
{
    internal class ShopItem
    {
        private int itemId;
        private int itemType;
        private int price;
        private bool show;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        private int x, y, width, height;
        private BasePanel parent;
        private BitmapButton bitmapButtonBuy;

        public void Init()
        {
            virtualRegion = new VirtualRegion(parent);
            virtualRegion.AddRegion(new PictureAnimRegion(1, x + 5, y + 8, 40, 40, 1, VirtualRegionCellType.Card, 0));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        public void RefreshData(int id, int type)
        {
            bitmapButtonBuy.Visible = id != 0;
            show = id != 0;
            itemId = id;
            itemType = type;
            if (id != 0)
            {
                virtualRegion.SetRegionInfo(1, id);
                virtualRegion.SetRegionType(1, type == 1 ? VirtualRegionCellType.Item : VirtualRegionCellType.Equip);
            }
            if (itemType == 1)
            {
                price = ConfigData.GetHItemConfig(itemId).Value;
            }
            else
            {                
                price = ConfigData.GetEquipConfig(itemId).Value;
            }

            parent.Invalidate(new Rectangle(x, y, width, height));
        }

        public ShopItem(BasePanel prt, int x, int y, int width, int height)
        {
            parent = prt;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.bitmapButtonBuy = new BitmapButton();
            bitmapButtonBuy.Location = new Point(x + 102, y + 30);
            bitmapButtonBuy.Size = new Size(35, 20);
            this.bitmapButtonBuy.Click += new System.EventHandler(this.pictureBoxBuy_Click);
            this.bitmapButtonBuy.ImageNormal = PicLoader.Read("Button", "BuyButton.JPG");
            this.bitmapButtonBuy.ImageMouseOver = PicLoader.Read("Button", "BuyButtonOn.JPG");
            this.bitmapButtonBuy.ImagePressed = PicLoader.Read("Button", "BuyButtonOn.JPG");
            this.bitmapButtonBuy.FlatStyle = FlatStyle.Flat;
            this.bitmapButtonBuy.StretchImage = true;
            this.bitmapButtonBuy.Text = "购买物品";
            parent.Controls.Add(bitmapButtonBuy);
        }

        private void virtualRegion_RegionEntered(int info, int mx, int my, int key)
        {
            if (info == 1 && itemId > 0)
            {
                Image image = itemType == 1 ? HItemBook.GetPreview(itemId) : EquipBook.GetPreview(itemId);
                tooltip.Show(image, parent, mx, my, itemId);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(parent, itemId);
        }

        private void pictureBoxBuy_Click(object sender, EventArgs e)
        {
            if (UserProfile.InfoBag.resource.Gold < price)
            {
                parent.AddFlowCenter("金钱不足", "Red");
                return;
            }

            UserProfile.InfoBag.resource.Gold -= price;
            if (itemType == 1)
            {
                UserProfile.InfoBag.AddItem(itemId, 1);
            }
            else if (itemType == 2)
            {
                UserProfile.InfoBag.AddEquip(itemId);
            }
        }

        public void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.White, x, y, width - 1, height - 1);

            if (show)
            {
                Font font = new Font("微软雅黑", 10, FontStyle.Regular);
                if (itemType == 1)
                {
                    HItemConfig itemConfig = ConfigData.GetHItemConfig(itemId);

                    Brush brush = new SolidBrush(Color.FromName(HSTypes.I2RareColor(itemConfig.Rare)));
                    g.DrawString(itemConfig.Name, font, brush, x+50, y+7);
                    brush.Dispose();
                }
                else
                {
                    EquipConfig equipConfig = ConfigData.GetEquipConfig(itemId);

                    Brush brush = new SolidBrush(Color.FromName(HSTypes.I2RareColor(equipConfig.Quality)));
                    g.DrawString(equipConfig.Name, font, brush, x + 50, y + 7);
                    brush.Dispose();
                }
                g.DrawString(price.ToString(), font, Brushes.Gold,x+ 50,y+ 30);
                g.DrawImage(HSIcons.GetIconsByEName("res1"), g.MeasureString(price.ToString(), font).Width + 50+x, 32+y, 16, 16);
                font.Dispose();

                virtualRegion.Draw(g);
            }
        }
    }
}
