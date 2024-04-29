using System;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.HeroSkills;
using TaleofMonsters.Forms.Items.Regions;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.Forms.Items
{
    internal class HeroSkillAttrItem
    {
        private int index;
        private int sid;
        private bool show;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        private int x, y, width, height;
        private BasePanel parent;
        private BitmapButton bitmapButtonBuy;

        public HeroSkillAttrItem(BasePanel prt, int x, int y, int width, int height)
        {
            parent = prt;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.bitmapButtonBuy = new BitmapButton();
            bitmapButtonBuy.Location = new Point(x + 125, y + 53);
            bitmapButtonBuy.Size = new Size(50, 24);
            this.bitmapButtonBuy.Click += new System.EventHandler(this.pictureBoxBuy_Click);
            this.bitmapButtonBuy.ImageNormal = PicLoader.Read("Button", "LevelButton.JPG");
            this.bitmapButtonBuy.ImageMouseOver = PicLoader.Read("Button", "LevelButtonOn.JPG");
            this.bitmapButtonBuy.ImagePressed = PicLoader.Read("Button", "LevelButtonOn.JPG");
            this.bitmapButtonBuy.FlatStyle = FlatStyle.Flat;
            this.bitmapButtonBuy.StretchImage = true;
            this.bitmapButtonBuy.Text = "升级技能";
            parent.Controls.Add(bitmapButtonBuy);
        }

        public void Init(int idx)
        {
            index = idx;

            virtualRegion = new VirtualRegion(parent);
            virtualRegion.AddRegion(new PictureRegion(1, x + 3, y + 3, 76, 75, 1, VirtualRegionCellType.SkillAttr, 0));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        public void RefreshData(int skid)
        {
            sid = skid;
            if (sid > 0)
            {
                bitmapButtonBuy.Visible = true;
                virtualRegion.SetRegionInfo(1, sid);
                show = true;
            }
            else
            {
                virtualRegion.SetRegionInfo(1, 0);
                bitmapButtonBuy.Visible = false;
                show = false;
            }

            parent.Invalidate(new Rectangle(x, y, width, height));
        }

        private void virtualRegion_RegionEntered(int info, int mx, int my, int key)
        {
            if (sid > 0)
            {
                int level = UserProfile.InfoSkill.GetSkillAttrLevel(sid);
                Image image = HeroSkillAttrBook.GetPreview(sid,level);
                tooltip.Show(image, parent, mx, my, sid);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(parent, sid);
        }

        private void pictureBoxBuy_Click(object sender, EventArgs e)
        {
            int level = UserProfile.InfoSkill.GetSkillAttrLevel(sid);
            int cost = HeroSkillAttrBook.GetCost(sid,level + 1);

            if (level >= UserProfile.InfoBasic.level)
            {
                parent.AddFlowCenter("等级限制", "Red");
                return;
            }

            if (UserProfile.InfoBasic.attrPoint >= cost)
            {
                UserProfile.InfoBasic.attrPoint -= cost;
                UserProfile.InfoSkill.AddSkillAttrLevel(sid);

                if (level + 1 >= UserProfile.InfoBasic.level)
                {
                    bitmapButtonBuy.Visible = false;
                }
                parent.Invalidate();
            }
            else
            {
                parent.AddFlowCenter("阅历不足", "Red");
            }
        }

        public void Draw(Graphics g)
        {
            SolidBrush sb = new SolidBrush(Color.FromArgb(20, 20, 20));
            g.FillRectangle(sb, x + 2, y + 2, width - 4, height - 4);
            sb.Dispose();
            g.DrawRectangle(Pens.Teal, x + 2, y + 2, width - 4, height - 4);

            if (show)
            {
                HeroSkillAttrConfig heroSkillAttrConfig = ConfigData.GetHeroSkillAttrConfig(sid);

                virtualRegion.Draw(g);

                g.DrawRectangle(Pens.White, x + 3, y + 3, 76, 75);
                Font ft = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                g.DrawString(heroSkillAttrConfig.Name, ft, Brushes.Gold, x + 90, y + 10);
                int slevel = UserProfile.InfoSkill.GetSkillAttrLevel(sid);
                g.DrawString(string.Format("等级{0}级", slevel), ft, Brushes.White, x + 90, y + 32);
                ft.Dispose();

                if (slevel == 0)
                {
                    Image marker = PicLoader.Read("System", "Mark1.PNG");
                    g.DrawImage(marker, x + 30, y + 2, 50, 51);
                    marker.Dispose();
                }
            }
        }
    }
}
