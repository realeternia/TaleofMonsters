using System;
using System.Drawing;
using System.Windows.Forms;
using NarlonLib.Control;
using NarlonLib.Core;
using NarlonLib.Drawing;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.DataType.HeroSkills;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms.Items.Regions;
using ConfigDatas;
using TaleofMonsters.MainItem;

namespace TaleofMonsters.Forms
{
    internal sealed partial class EquipmentForm : BasePanel
    {
        private bool show;
        private bool isDirty = true;
        private int tar;
        private int selectTar;
        private Bitmap tempImage;
        private ImageToolTip tooltip = SystemToolTip.Instance;
        private HSCursor myCursor;
        private VirtualRegion virtualRegion;

        private PopMenuEquip popMenuEquip;
        private PoperContainer popContainer;

        public EquipmentForm()
        {
            InitializeComponent();
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            virtualRegion = new VirtualRegion(this);
            virtualRegion.AddRegion(new SubVirtualRegion(1, 429, 62, 32, 32, 1));
            virtualRegion.AddRegion(new SubVirtualRegion(2, 429, 114, 32, 32, 2));
            virtualRegion.AddRegion(new SubVirtualRegion(3, 383, 114, 32, 32, 3));
            virtualRegion.AddRegion(new SubVirtualRegion(4, 476, 114, 32, 32, 4));
            virtualRegion.AddRegion(new SubVirtualRegion(5, 476, 62, 32, 32, 5));
            virtualRegion.AddRegion(new SubVirtualRegion(6, 383, 166, 32, 32, 6));
            virtualRegion.AddRegion(new SubVirtualRegion(7, 429, 166, 32, 32, 7));
            virtualRegion.AddRegion(new SubVirtualRegion(8, 476, 166, 32, 32, 8));
            virtualRegion.AddRegion(new SubVirtualRegion(9, 383, 62, 32, 32, 9));

            virtualRegion.AddRegion(new SubVirtualRegion(10, 147, 107, 46, 44, 10));
            virtualRegion.AddRegion(new SubVirtualRegion(11, 200, 107, 46, 44, 11));
            virtualRegion.AddRegion(new SubVirtualRegion(12, 253, 107, 46, 44, 12));
            virtualRegion.AddRegion(new SubVirtualRegion(13, 306, 107, 46, 44, 13));
            virtualRegion.AddRegion(new SubVirtualRegion(14, 147, 170, 46, 44, 14));
            virtualRegion.AddRegion(new SubVirtualRegion(15, 200, 170, 46, 44, 15));
            virtualRegion.AddRegion(new SubVirtualRegion(16, 253, 170, 46, 44, 16));
            virtualRegion.AddRegion(new SubVirtualRegion(17, 306, 170, 46, 44, 17));

            virtualRegion.AddRegion(new SubVirtualRegion(20, 149, 222, 40, 40, 20));
            virtualRegion.AddRegion(new SubVirtualRegion(21, 253, 222, 40, 40, 21));
            virtualRegion.AddRegion(new SubVirtualRegion(22, 149, 270, 40, 40, 22));
            virtualRegion.AddRegion(new SubVirtualRegion(23, 253, 270, 40, 40, 23));
            virtualRegion.AddRegion(new SubVirtualRegion(24, 149, 318, 40, 40, 24));
            virtualRegion.AddRegion(new SubVirtualRegion(25, 253, 318, 40, 40, 25));
            virtualRegion.AddRegion(new SubVirtualRegion(26, 149, 366, 40, 40, 26));
            virtualRegion.AddRegion(new SubVirtualRegion(27, 253, 366, 40, 40, 27));

            virtualRegion.AddRegion(new SubVirtualRegion(30, 33, 194, 38, 38, 30));
            virtualRegion.AddRegion(new SubVirtualRegion(31, 33, 236, 38, 38, 31));
            virtualRegion.AddRegion(new SubVirtualRegion(32, 33, 278, 38, 38, 32));
            virtualRegion.AddRegion(new SubVirtualRegion(33, 33, 320, 38, 38, 33));
            virtualRegion.AddRegion(new SubVirtualRegion(34, 33, 362, 38, 38, 34));

            for (int i = 0; i < 25; i++)
            {
                virtualRegion.AddRegion(new SubVirtualRegion(40 + i, 361 + (i % 5) * 32, 222 + (i / 5) * 32, 32, 32, 40 + i));
            }

            virtualRegion.RegionClicked += new VirtualRegion.VRegionClickEventHandler(virtualRegion_RegionClicked);
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
            tempImage = new Bitmap(160, 160);
            tar = selectTar = -1;
            myCursor = new HSCursor(this);

            popMenuEquip = new PopMenuEquip();
            popContainer = new PoperContainer(popMenuEquip);
            popMenuEquip.PoperContainer = popContainer;
            popMenuEquip.Form = this;
        }

        internal override void Init()
        {
            base.Init();
            show = true;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            int id = info;
            Image image = null;
            if (id < 10)
            {
                if (UserProfile.InfoBag.equipon[id - 1] != 0)
                {
                    image = EquipBook.GetPreview(UserProfile.InfoBag.equipon[id - 1]);
                }
            }
            else if (id < 20)
            {
                image = DrawTool.GetImageByString(HSTypes.I2HeroAttrTip(id - 10), 200);
            }
            else if (id < 30)
            {
                if (UserProfile.InfoSkill.skillCommons.Count > id - 20)
                {
                    int sid = UserProfile.InfoSkill.skillCommons[id - 20];
                    image = DrawTool.GetImageByString(ConfigData.GetHeroSkillCommonConfig(sid).Des, 200);
                }
            }
            else if (id < 40)
            {
                if (UserProfile.InfoSkill.skillSpecials.Count > id - 30)
                {
                    int sid = UserProfile.InfoSkill.skillSpecials[id - 30];
                    image = DrawTool.GetImageByString("待实现", 200);
                }
            }
            else if(id>=40)
            {
                if (UserProfile.InfoBag.equipoff[id - 40] != 0)
                {
                    image = EquipBook.GetPreview(UserProfile.InfoBag.equipoff[id - 40]);
                }
            }

            if (image != null)
            {
                tooltip.Show(image, this, x, y);
            }
            else
            {
                tooltip.Hide(this);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(this);
        }

        private void virtualRegion_RegionClicked(int info, MouseButtons button)
        {
            int id = info;
            if (button == MouseButtons.Left && id < 10)
            {
                if (selectTar != -1)
                {
                    EquipConfig equipConfig = ConfigData.GetEquipConfig(UserProfile.InfoBag.equipoff[selectTar]);
                    if (!EquipBook.CanEquip(equipConfig.Id))
                    {
                        AddFlowCenter("等级不足或职业不匹配", "Red");
                    }
                    else
                    {
                        if (equipConfig.Position == id - 1 || (equipConfig.Position == 5 && id == 8))
                        {
                            int oldid = UserProfile.InfoBag.equipon[id - 1];
                            UserProfile.InfoBag.equipon[id - 1] = UserProfile.InfoBag.equipoff[selectTar];
                            UserProfile.InfoBag.equipoff[selectTar] = oldid;
                            selectTar = -1;
                            myCursor.ChangeCursor("default");
                        }
                    }
                }
                else
                {
                    int i;
                    for (i = 0; i < 25; i++)
                    {
                        if (UserProfile.InfoBag.equipoff[i] == 0)
                            break;
                    }
                    if (i == 25)
                        return;
                    UserProfile.InfoBag.equipoff[i] = UserProfile.InfoBag.equipon[id - 1];
                    UserProfile.InfoBag.equipon[id - 1] = 0;
                }
                isDirty = true;
                Invalidate();
            }
        }

        public void MenuRefresh()
        {
            isDirty = true;
            Invalidate();
        }

        private int GetX(Graphics g, string s, Font font, int x, int width)
        {
            return x + (width - (int)g.MeasureString(s, font).Width) / 2;
        }

        private void EquipmentView_MouseClick(object sender, MouseEventArgs e)
        {
            tooltip.Hide(this);

            if (tar == -1)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (selectTar == -1)
                {
                    if (UserProfile.InfoBag.equipoff[tar] != 0)
                    {
                        myCursor.ChangeCursor("Equip", string.Format("{0}.JPG", ConfigData.GetEquipConfig(UserProfile.InfoBag.equipoff[tar]).Url), 40, 40);
                        selectTar = tar;
                        tooltip.Hide(this);
                    }
                }
                else
                {
                    myCursor.ChangeCursor("default");
                    if (UserProfile.InfoBag.equipoff[tar] == 0)
                    {
                        UserProfile.InfoBag.equipoff[tar] = UserProfile.InfoBag.equipoff[selectTar];
                        UserProfile.InfoBag.equipoff[selectTar] = 0;
                    }
                    else
                    {
                        int oldid = UserProfile.InfoBag.equipoff[tar];
                        UserProfile.InfoBag.equipoff[tar] = UserProfile.InfoBag.equipoff[selectTar];
                        UserProfile.InfoBag.equipoff[selectTar] = oldid;
                    }
                    selectTar = -1;
                }
                isDirty = true;
                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                selectTar = tar;
                tooltip.Hide(this);
 
                popMenuEquip.Clear();
                #region 构建菜单
                if (UserProfile.InfoBag.equipoff[selectTar] != 0)
                {
                    popMenuEquip.AddItem("throw", "丢弃","Red");
                }
                popMenuEquip.AddItem("exit", "退出");
                #endregion
                popMenuEquip.AutoResize();
                popMenuEquip.EquipIndex = selectTar;
                popContainer.Show(this, e.Location.X, e.Location.Y);      
            }
        }

        private void EquipmentView_MouseLeave(object sender, EventArgs e)
        {
            tar = -1;
        }

        private void EquipmentView_MouseMove(object sender, MouseEventArgs e)
        {
            int truex = e.X - 361;
            int truey = e.Y - 222;
            if (truex > 0 && truey > 0 && truex < 160 && truey < 160)
            {
                int temp = truex / 32 + truey / 32 * 5;
                if (temp != tar)
                {
                    tar = temp;
                    Invalidate(new Rectangle(360, 221, 162, 162));
                }
            }
            else if (tar != -1)
            {
                tar = -1;
                Invalidate(new Rectangle(360, 221, 162, 162));
            }
        }

        private void EquipmentForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString("角色", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            Image back = PicLoader.Read("System", "HeroBackNew1.JPG");
            e.Graphics.DrawImage(back, 20, 36, 512, 378);
            back.Dispose();

            if (!show)
                return;

            Image body = PicLoader.Read("Hero", string.Format("{0}{1}.JPG", UserProfile.InfoBasic.job, UserProfile.InfoBasic.sex == 1 ? "m" : "f"));
            e.Graphics.DrawImage(body, 25, 47, 111, 111);
            body.Dispose();

            e.Graphics.FillRectangle(Brushes.LightSlateGray, 92, 113, 42, 42);
            Image head = PicLoader.Read("Player", string.Format("{0}.PNG", UserProfile.InfoBasic.face));
            e.Graphics.DrawImage(head, 93, 114, 40, 40);
            head.Dispose();

            font = new Font("宋体", 11, FontStyle.Regular);
            Font font2 = new Font("宋体", 10, FontStyle.Regular);
            Font font3 = new Font("宋体", 9, FontStyle.Regular);
            string namestr = string.Format("{0}(Lv{1}{2})", UserProfile.ProfileName, UserProfile.InfoBasic.level, ConfigData.GetJobConfig(UserProfile.InfoBasic.job).Name);
            e.Graphics.DrawString(namestr, font, Brushes.White, GetX(e.Graphics, namestr, font, 182, 130), 61);

            Brush brush = new SolidBrush(Color.FromArgb(180, Color.DimGray));
            for (int i = 0; i < 4; i++)
            {
                e.Graphics.FillRectangle(brush, 147 + 53 * i, 136, 45, 15);
                e.Graphics.FillRectangle(brush, 147 + 53 * i, 199, 45, 15);
            }
            brush.Dispose();

            string expstr = string.Format("{0}/{1}", UserProfile.InfoBasic.exp, ExpTree.GetNextRequired(UserProfile.InfoBasic.level));
            e.Graphics.DrawString(expstr, font2, Brushes.White, GetX(e.Graphics, expstr, font2, 38, 68), 161);
            e.Graphics.FillRectangle(Brushes.DimGray, 31, 178, 80, 2);
            e.Graphics.FillRectangle(Brushes.DodgerBlue, 31, 178, Math.Min(UserProfile.InfoBasic.exp * 79 / ExpTree.GetNextRequired(UserProfile.InfoBasic.level) + 1, 80), 2);

            AutoDictionary<int, int> addons = EquipBook.GetEquipsAddons(UserProfile.InfoBag.equipon);
            if (addons[1] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.atk.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.atk.ToString(), font, 147, 45), 136);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.atk.ToString().PadLeft(2), font, Brushes.White, 147, 136);
                e.Graphics.DrawString(string.Format("+{0}", addons[1]), font2, Brushes.Lime, 165, 136);
            }
            if (addons[2] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.def.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.def.ToString(), font, 147 + 53, 45), 136);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.def.ToString().PadLeft(2), font, Brushes.White, 147 + 53, 136);
                e.Graphics.DrawString(string.Format("+{0}", addons[2]), font2, Brushes.Lime, 165 + 53, 136);
            }
            if (addons[3] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.mag.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.mag.ToString(), font, 147 + 53 * 2, 45), 136);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.mag.ToString().PadLeft(2), font, Brushes.White, 147 + 53 * 2, 136);
                e.Graphics.DrawString(string.Format("+{0}", addons[3]), font2, Brushes.Lime, 165 + 53 * 2, 136);
            }
            if (addons[4] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.skl.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.skl.ToString(), font, 147 + 53 * 3, 45), 136);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.skl.ToString().PadLeft(2), font, Brushes.White, 147 + 53 * 3, 136);
                e.Graphics.DrawString(string.Format("+{0}", addons[4]), font2, Brushes.Lime, 165 + 53 * 3, 136);
            }

            if (addons[5] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.spd.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.spd.ToString(), font, 147, 45), 199);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.spd.ToString().PadLeft(2), font, Brushes.White, 147, 199);
                e.Graphics.DrawString(string.Format("+{0}", addons[5]), font2, Brushes.Lime, 165, 199);
            }
            if (addons[6] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.luk.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.luk.ToString(), font, 147 + 53, 45), 199);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.luk.ToString().PadLeft(2), font, Brushes.White, 147 + 53, 199);
                e.Graphics.DrawString(string.Format("+{0}", addons[6]), font2, Brushes.Lime, 165 + 53, 199);
            }
            if (addons[7] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.vit.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.vit.ToString(), font, 147 + 53 * 2, 45), 199);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.vit.ToString().PadLeft(2), font, Brushes.White, 147 + 53 * 2, 199);
                e.Graphics.DrawString(string.Format("+{0}", addons[7]), font2, Brushes.Lime, 165 + 53 * 2, 199);
            }
            if (addons[8] == 0)
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.adp.ToString(), font, Brushes.White, GetX(e.Graphics, UserProfile.InfoBasic.adp.ToString(), font, 147 + 53 * 3, 45), 199);
            }
            else
            {
                e.Graphics.DrawString(UserProfile.InfoBasic.adp.ToString().PadLeft(2), font, Brushes.White, 147 + 53 * 3, 199);
                e.Graphics.DrawString(string.Format("+{0}", addons[8]), font2, Brushes.Lime, 165 + 53 * 3, 199);
            }

            for (int i = 0; i < UserProfile.InfoSkill.skillCommons.Count; i++)
            {
                int sid = UserProfile.InfoSkill.skillCommons[i];
                e.Graphics.DrawImage(HeroSkillBook.GetHeroSkillCommonImage(sid), 149 + 103 * (i % 2), 222 + 47 * (i / 2), 40, 40);

                string skillName = ConfigData.GetHeroSkillCommonConfig(sid).Name;
                int idex = skillName.IndexOf('L');
                string foreStr = skillName.Substring(0, idex);
                string behindStr = skillName.Substring(idex);
                e.Graphics.DrawString(foreStr, font3, Brushes.Gold, GetX(e.Graphics, foreStr, font3, 192 + 103 * (i % 2), 60), 227 + 47 * (i / 2));
                e.Graphics.DrawString(behindStr, font3, Brushes.White, GetX(e.Graphics, behindStr, font3, 192 + 103 * (i % 2), 60), 245 + 47 * (i / 2));
            }
            for (int i = 0; i < UserProfile.InfoSkill.skillSpecials.Count; i++)
            {
                int sid = UserProfile.InfoSkill.skillSpecials[i];
                e.Graphics.DrawImage(HeroSkillBook.GetHeroSkillSpecialImage(sid), 33, 194 + 42 * i, 38, 38);

                string skillName = ConfigData.GetSkillConfig(ConfigData.GetHeroSkillSpecialConfig(sid).SkillId).Name;
                e.Graphics.DrawString(skillName, font3, Brushes.Gold, GetX(e.Graphics, skillName, font3, 72, 55), 208 + 42 * i);
            }

            font.Dispose();
            font2.Dispose();
            font3.Dispose();

            int[] equipPos = { 9, 1, 5, 3, 2, 4, 6, 7, 8 };
            for (int i = 0; i < 9; ++i)
            {
                int eid = UserProfile.InfoBag.equipon[equipPos[i] - 1];
                if (eid == 0)
                {
                    Image img = PicLoader.Read("System", string.Format("EquipBack{0}.JPG", equipPos[i]));
                    e.Graphics.DrawImage(img, 380 + (i % 3) * 46, 62 + (i / 3) * 52, 32, 32);
                    img.Dispose();
                }
                else
                {
                    e.Graphics.DrawImage(EquipBook.GetEquipImage(eid), 380 + (i % 3) * 46, 62 + (i / 3) * 52, 32, 32);
                }
            }

            if (isDirty)
            {
                tempImage.Dispose();
                tempImage = new Bitmap(160, 160);
                Graphics g = Graphics.FromImage(tempImage);
                for (int i = 0; i < 25; i++)
                {
                    if (UserProfile.InfoBag.equipoff[i] != 0)
                        g.DrawImage(EquipBook.GetEquipImage(UserProfile.InfoBag.equipoff[i]), (i % 5) * 32F, (i / 5) * 32F, 32F, 32F);
                }
                g.Dispose();
                isDirty = false;
            }
            e.Graphics.DrawImage(tempImage, 361, 222);
            int rect = tar;
            if (tar == -1 && selectTar >= 0)
                rect = selectTar;
            if (rect >= 0)
            {
                SolidBrush yellowbrush = new SolidBrush(Color.FromArgb(80, Color.Yellow));
                e.Graphics.FillRectangle(yellowbrush, (rect % 5) * 32F + 361, (rect / 5) * 32F + 222, 32F, 32F);
                yellowbrush.Dispose();

                Pen yellowpen = new Pen(Brushes.Yellow, 2);
                e.Graphics.DrawRectangle(yellowpen, (rect % 5) * 32F + 361, (rect / 5) * 32F + 222, 32F, 32F);
                yellowpen.Dispose();
            }
        }
    }
}