using System;
using System.Drawing;
using System.Windows.Forms;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.Forms.Items.Regions;
using TaleofMonsters.Forms.Pops;
using ConfigDatas;

namespace TaleofMonsters.Forms.Items
{
    internal class GameShopItem
    {
        private bool show;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        private int productId;
        private int x, y, width, height;
        private Control parent;
        private BitmapButton bitmapButtonBuy;

        public void Init()
        {
            virtualRegion = new VirtualRegion(parent);
            virtualRegion.AddRegion(new PictureAnimRegion(1, x + 11, y + 19, 56, 56, 1, VirtualRegionCellType.Item, 0));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        public void RefreshData(int id)//商品id
        {
            productId = id;
            GameShopConfig gameShopConfig = ConfigData.GetGameShopConfig(id);

            bitmapButtonBuy.Visible = id != 0;
            show = id != 0;

            if (id != 0)
            {
                virtualRegion.SetRegionInfo(1, gameShopConfig.ItemId);
                virtualRegion.SetRegionType(1, gameShopConfig.Type == 1 ? VirtualRegionCellType.Item : VirtualRegionCellType.Equip);
            }

            parent.Invalidate(new Rectangle(x, y, width, height));
        }

        public GameShopItem(UserControl prt, int x, int y, int width, int height)
        {
            parent = prt;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.bitmapButtonBuy = new BitmapButton();
            bitmapButtonBuy.Location = new Point(x + 125, y + 70);
            bitmapButtonBuy.Size = new Size(35, 20);
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
            if (info == 1 && productId > 0)
            {
                GameShopConfig gameShopConfig = ConfigData.GetGameShopConfig(productId);
                Image image = gameShopConfig.Type == 1 ? HItemBook.GetPreview(gameShopConfig.ItemId) : EquipBook.GetPreview(gameShopConfig.ItemId);
                tooltip.Show(image, parent, mx, my, gameShopConfig.ItemId);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            GameShopConfig gameShopConfig = ConfigData.GetGameShopConfig(productId);
            tooltip.Hide(parent, gameShopConfig.ItemId);
        }

        private void pictureBoxBuy_Click(object sender, EventArgs e)
        {
            GameShopConfig gameShopConfig = ConfigData.GetGameShopConfig(productId);
            PopBuyProduct.Show(gameShopConfig.ItemId, gameShopConfig.Type, gameShopConfig.Price);
        }

        public void Draw(Graphics g)
        {
            Image back = PicLoader.Read("System", "ShopItemBack.JPG");
            g.DrawImage(back, x, y, width - 1, height - 1);
            back.Dispose();

            if (show)
            {
                GameShopConfig gameShopConfig = ConfigData.GetGameShopConfig(productId);
                string name;
                string fontcolor;
                if (gameShopConfig.Type == 1)
                {
                    HItemConfig itemConfig = ConfigData.GetHItemConfig(gameShopConfig.ItemId);
                    name = itemConfig.Name;
                    fontcolor = HSTypes.I2RareColor(itemConfig.Rare);
                }
                else
                {
                    EquipConfig equipConfig = ConfigData.GetEquipConfig(gameShopConfig.ItemId);
                    name = equipConfig.Name;
                    fontcolor = HSTypes.I2QualityColor(equipConfig.Quality);
                }
                Font fontsong = new Font("宋体", 9, FontStyle.Regular);
                Brush brush = new SolidBrush(Color.FromName(fontcolor));
                g.DrawString(name, fontsong, brush, x + 76, y + 9);
                brush.Dispose();
                g.DrawString(string.Format("{0,3:D}", gameShopConfig.Price), fontsong, Brushes.PaleTurquoise, x + 80, y + 37);
                fontsong.Dispose();
                g.DrawImage(HSIcons.GetIconsByEName("res8"), x + 110, y + 35, 16, 16);

                virtualRegion.Draw(g);
            }
        }
    }
}
