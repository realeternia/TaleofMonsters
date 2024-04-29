using System.Drawing;
using System.Windows.Forms;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Shops;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms.Items.Regions;

namespace TaleofMonsters.Forms.Items
{
    internal class CardShopItem
    {
        private CardProduct product;
        private bool show;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        private int x, y, width, height;
        private BasePanel parent;

        public void Init()
        {
            virtualRegion = new VirtualRegion(parent);
            virtualRegion.AddRegion(new PictureAnimRegion(1, x + 12, y + 14, 64, 84, 1, VirtualRegionCellType.Card, 0));
            virtualRegion.AddRegion(new ButtonRegion(2, x + 55, y + 102, 17, 17, 2, "BuyIcon.PNG", "BuyIconOn.PNG"));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
            virtualRegion.RegionClicked += new VirtualRegion.VRegionClickEventHandler(virtualRegion_RegionClicked);
        }

        public void RefreshData(CardProduct pro)
        {
            show = pro.id != 0;
            product = pro;
            if (product.id != 0)
            {
                virtualRegion.SetRegionInfo(1, product.cid);
            }

            parent.Invalidate(new Rectangle(x, y, width, height));
        }

        public CardShopItem(BasePanel prt, int x, int y, int width, int height)
        {
            parent = prt;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        private void virtualRegion_RegionEntered(int info, int mx, int my, int key)
        {
            if (info == 1 && product.id > 0)
            {
                Image image = CardAssistant.GetCard(product.cid).GetPreview(CardPreviewType.Shop, product.Price.ToArray());
                tooltip.Show(image, parent, mx, my, product.id);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(parent, product.id);
        }

        private void virtualRegion_RegionClicked(int info, MouseButtons button)
        {
            if (info == 2 && product.id > 0)
            {
                GameResource res = product.Price;
                if (UserProfile.InfoBag.CheckResource(res.ToArray()))
                {
                    UserProfile.InfoCard.AddCard(product.cid);
                    UserProfile.InfoBag.SubResource(res.ToArray());
                    UserProfile.InfoWorld.RemoveCardProduct(product.cid);
                    CardShopViewForm cardShopViewForm = parent as CardShopViewForm;
                    if (cardShopViewForm != null) cardShopViewForm.ChangeShop();
                }
                else
                {
                    parent.AddFlowCenter("没有足够的资源!", "Red");
                }
            }
        }

        public void Draw(Graphics g)
        {
            Image back = PicLoader.Read("System", "CardBack2.JPG");
            g.DrawImage(back, x, y, width - 1, height - 1);
            back.Dispose();

            if (show)
            {
                Card card = CardAssistant.GetCard(product.cid);
                g.FillRectangle(PaintTool.GetBrushByAttribute(card.Type), x + 10, y + 12, 70 - 2, 90 - 2);

                virtualRegion.Draw(g);

                if (product.mark != CardProductMarkTypes.Null)
                {
                    Image marker = PicLoader.Read("System", string.Format("Mark{0}.PNG", (int)product.mark));
                    g.DrawImage(marker, x + 28, y+12, 50, 51);
                    marker.Dispose();
                }
            }
        }
    }
}
