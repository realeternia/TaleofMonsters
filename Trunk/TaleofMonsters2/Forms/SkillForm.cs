using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ControlPlus;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms.Items.Core;
using ConfigDatas;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.Forms.Items.Regions;
using TaleofMonsters.Forms.Items.Regions.Decorators;
using TaleofMonsters.DataType.HeroSkills;

namespace TaleofMonsters.Forms
{
    internal sealed partial class SkillForm : BasePanel
    {
        private NLSelectPanel selectPanel;
        private List<int> skillIds;
        private NLPageSelector nlPageSelector1;
        private int pageid;
        private int baseid;
        private int skillid;
        private ColorWordRegion nowLevelDes;
        private ColorWordRegion nextLevelDes;
        private VirtualRegion virtualRegion;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;

        public SkillForm()
        {
            InitializeComponent();
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonActive.ImageNormal = PicLoader.Read("Button", "ActiveButton.JPG");
            this.bitmapButtonActive.ImageMouseOver = PicLoader.Read("Button", "ActiveButtonOn.JPG");
            this.bitmapButtonActive.ImagePressed = PicLoader.Read("Button", "ActiveButtonOn.JPG");
            this.bitmapButtonUpgrade.ImageNormal = PicLoader.Read("Button", "LevelButton.JPG");
            this.bitmapButtonUpgrade.ImageMouseOver = PicLoader.Read("Button", "LevelButtonOn.JPG");
            this.bitmapButtonUpgrade.ImagePressed = PicLoader.Read("Button", "LevelButtonOn.JPG");

            selectPanel = new NLSelectPanel(8, 60, 154, 400, this);
            selectPanel.ItemHeight = 50;
            selectPanel.SelectIndexChanged += selectPanel_SelectedIndexChanged;
            selectPanel.DrawCell += new NLSelectPanel.SelectPanelCellDrawHandler(selectPanel_DrawCell);

            this.nlPageSelector1 = new NLPageSelector(this, 10, 311, 150);
            nlPageSelector1.PageChange += nlPageSelector1_PageChange;

            nowLevelDes = new ColorWordRegion(250, 112, 250, "宋体", 10, Color.White);
            nextLevelDes = new ColorWordRegion(250, 156, 250, "宋体", 10, Color.White);

            virtualRegion = new VirtualRegion(this);
            for (int i = 0; i < 5; i++)
            {
                PictureRegion region = new PictureRegion(i + 1, 68 + 80 * i, 348, 60, 60, i + 1, VirtualRegionCellType.SkillSpecial, 0);
                region.AddDecorator(new RegionBorderDecorator(region, Color.DodgerBlue));
                virtualRegion.AddRegion(region);
            }
            for (int i = 0; i < 2; i++)
            {
                SubVirtualRegion subRegion = new ButtonRegion(i + 11, 16 + 45 * i, 39, 42, 23, i + 11, "ShopTag.JPG", "ShopTagOn.JPG");
                subRegion.AddDecorator(new RegionTextDecorator(subRegion, 8, 7, 9, Color.White, false));
                virtualRegion.AddRegion(subRegion);
            }
            virtualRegion.SetRegionDecorator(11, 0, "职业");
            virtualRegion.SetRegionDecorator(12, 0, "通用");
            virtualRegion.RegionEntered += virtualRegion_RegionEntered;
            virtualRegion.RegionLeft += virtualRegion_RegionLeft;
            virtualRegion.RegionClicked += virtualRegion_RegionClicked;
        }

        internal override void Init()
        {
            base.Init();

            virtualRegion_RegionClicked(11, MouseButtons.Left);
            refreshUseSkill();
        }

        private void changePage(int page)
        {
            pageid = page;
            baseid = 0;
            skillIds = new List<int>();
            foreach (int skillId in HeroSkillBook.GetSkillSpecialJob(UserProfile.Profile.InfoBasic.job))
            {
                HeroSkillSpecialConfig skill = ConfigData.GetHeroSkillSpecialConfig(skillId);
                if (skill.Catalog == page + 1 && skill.Level == UserProfile.InfoSkill.GetSkillSpecialLevelByGroup(skill.Group) + 1)
                {
                    skillIds.Add(skill.Id);
                }
            }
            nlPageSelector1.TotalPage = (skillIds.Count+4)/5;
            refreshInfo();
        }

        private void refreshInfo()
        {
            selectPanel.ClearContent();
            for (int i = baseid; i < Math.Min(baseid+5, skillIds.Count); i++)
            {
                selectPanel.AddContent(skillIds[i]);
            }

            selectPanel.SelectIndex = 0;
        }

        private void refreshUseSkill()
        {
            for (int i = 0; i < 5; i++)
            {
                virtualRegion.SetRegionInfo(i+1, UserProfile.InfoSkill.skillUse[i]);
            }
        }

        private void selectPanel_SelectedIndexChanged()
        {
            skillid = skillIds[baseid + selectPanel.SelectIndex];
            refreshDesText();
            Invalidate();
        }

        private void refreshDesText()
        {
            HeroSkillSpecialConfig skillSpecial = ConfigData.GetHeroSkillSpecialConfig(skillid);
            bitmapButtonActive.Visible = skillSpecial.Level > 1 && !UserProfile.InfoSkill.HasSkillSpecialUse(skillSpecial.Former);
            Skill nowSkill = new Skill(skillSpecial.SkillId).UpgradeToLevel(skillSpecial.Level - 1);
            if (skillSpecial.Level == 1)
            {
                nowLevelDes.Text = "无";
            }
            else
            {
                HeroSkillSpecialConfig skillSpecialFormer = ConfigData.GetHeroSkillSpecialConfig(skillSpecial.Former);
                string des = nowSkill.Descript;
                if (skillSpecialFormer.Rate < 100)
                {
                    des += string.Format("({0}%)", skillSpecialFormer.Rate);
                }
               // des += nowSkill.GetBuffDescript(skillSpecial.Level - 1);todo 技能buff说明
                nowLevelDes.Text = des;
            }


            Skill nextSkill = new Skill(skillSpecial.SkillId).UpgradeToLevel(skillSpecial.Level);
            string des2 = nextSkill.Descript;
            if (skillSpecial.Rate < 100)
            {
                des2 += string.Format("({0}%)", skillSpecial.Rate);
            }
            //  des2 += nowSkill.GetBuffDescript(skillSpecial.Level);todo 技能buff说明
            nextLevelDes.Text = des2;
        }

        private void selectPanel_DrawCell(Graphics g, int info, int xOff, int yOff)
        {
            HeroSkillSpecialConfig skillSpecialConfig = ConfigData.GetHeroSkillSpecialConfig(info);
            SkillConfig skillConfig = ConfigData.GetSkillConfig(skillSpecialConfig.SkillId);
            Image skillImage = SkillBook.GetSkillImage(skillSpecialConfig.SkillId);
            Font fontDes = new Font("宋体", 10F);
            if (skillSpecialConfig.Level == 1)
            {
                g.DrawImage(skillImage, new Rectangle(5 + xOff, 5 + yOff, 40, 40), 0, 0, skillImage.Width, skillImage.Height, GraphicsUnit.Pixel, HSImageAttributes.ToGray);
                g.DrawString("未学习", fontDes, Brushes.Gray, 100 + xOff, 31 + yOff);
            }
            else
            {
                g.DrawImage(skillImage, 5 + xOff, 5 + yOff, 40, 40);
                g.DrawString(string.Format("等级 {0}", skillSpecialConfig.Level-1), fontDes, Brushes.LightSalmon, 100 + xOff, 31 + yOff);
            }            
            Font font = new Font("微软雅黑", 11.25F, FontStyle.Bold);
            g.DrawString(skillConfig.Name, font, skillSpecialConfig.Level == 1 ? Brushes.White : Brushes.YellowGreen, 50 + xOff, 5 + yOff);
            font.Dispose();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bitmapButtonUpgrade_Click(object sender, EventArgs e)
        {
            if (skillid > 0)
            {
                HeroSkillSpecialConfig skillSpecial = ConfigData.GetHeroSkillSpecialConfig(skillid);

                if (skillSpecial.HeroLevel > UserProfile.InfoBasic.level)
                {
                    AddFlowCenter("等级限制", "Red");
                    return;
                }

                if (skillSpecial.SkillPointCost > UserProfile.InfoBasic.skillPoint)
                {
                    AddFlowCenter("灵气不足", "Red");
                    return;
                }

                if (!UserProfile.InfoBag.HasResource(GameResourceType.Gold, skillSpecial.GoldCost))
                {
                    AddFlowCenter("金钱不足", "Red");
                    return;
                }

                if (skillSpecial.SkillNeed.Length > 1 || skillSpecial.SkillNeed[0]>0)
                {
                    foreach (int skill in skillSpecial.SkillNeed)
                    {
                        if (!hasSkillSpecial(skill))
                        {
                            AddFlowCenter("前置技能未达成", "Red");
                            return;
                        }
                    }
                }

                UserProfile.InfoBag.AddResource(GameResourceType.Gold, -skillSpecial.GoldCost);
                UserProfile.InfoBasic.skillPoint -= skillSpecial.SkillPointCost;

                UserProfile.InfoSkill.AddSkillSpecial(skillid);
                for (int i = 0; i < skillIds.Count; i++)
                {
                    if (skillIds[i] == skillid)
                    {
                        skillIds[i]++;
                        break;
                    }
                }
                skillid++;
                selectPanel.UpdateContent(skillid);
                refreshDesText(); 
                Invalidate();
            }
        }

        private bool hasSkillSpecial(int sid)
        {
            HeroSkillSpecialConfig srcSkill = ConfigData.GetHeroSkillSpecialConfig(sid);
            foreach (int skillId in UserProfile.InfoSkill.skillSpecials)
            {
                if (skillId == sid)
                {
                    return true;
                }
                HeroSkillSpecialConfig destSkill = ConfigData.GetHeroSkillSpecialConfig(skillId);
                if (destSkill.Group == srcSkill.Group&&destSkill.Level>srcSkill.Level)
                {
                    return true;
                }
            }
            return false;
        }

        private void bitmapButtonActive_Click(object sender, EventArgs e)
        {
            if (skillid > 0)
            {
                HeroSkillSpecialConfig skillSpecial = ConfigData.GetHeroSkillSpecialConfig(skillid);
                if (skillSpecial.Level<=1)
                {
                    return;
                }

                UserProfile.InfoSkill.skillUse[skillSpecial.Slot - 1] = skillSpecial.Former;
                virtualRegion.SetRegionInfo(skillSpecial.Slot, skillSpecial.Former);
                bitmapButtonActive.Visible = false;
                Invalidate();
            }
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            int id = info;
            if (id > 0 && id < 10 && UserProfile.InfoSkill.skillUse[id - 1] > 0)
            {
                Image image = HeroSkillBook.GetSkillSpecialPreview(UserProfile.InfoSkill.skillUse[id - 1]);
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

        void virtualRegion_RegionClicked(int info, MouseButtons button)
        {
            if (info>10&&button == MouseButtons.Left)
            {
                for (int i = 0; i < 4; i++)
                {
                    virtualRegion.SetRegionState(i + 11, RegionState.Free);
                }

                virtualRegion.SetRegionState(info, RegionState.Blacken);

                changePage(info - 11);
            }
        }

        private void SkillForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(" 技能 ", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            virtualRegion.Draw(e.Graphics);

            e.Graphics.DrawRectangle(Pens.DodgerBlue, 8, 61, 153, 279);
            e.Graphics.DrawRectangle(Pens.DodgerBlue, 170, 60, 330, 210);
            e.Graphics.DrawRectangle(Pens.DodgerBlue, 170, 278, 330, 62);

            if (skillid>0)
            {
                HeroSkillSpecialConfig skillSpecial = ConfigData.GetHeroSkillSpecialConfig(skillid);
                SkillConfig skillConfig = ConfigData.GetSkillConfig(skillSpecial.SkillId);
                Font fontsong = new Font("宋体", 10, FontStyle.Regular);
                e.Graphics.DrawString("技能名称:", fontsong, Brushes.SkyBlue, 180, 70);
                e.Graphics.DrawString(skillConfig.Name, fontsong, Brushes.White, 250,70);
                e.Graphics.DrawString("当前等级:", fontsong, Brushes.SkyBlue, 350, 70);
                e.Graphics.DrawString((skillSpecial.Level-1).ToString(), fontsong, Brushes.Yellow, 420, 70);
                e.Graphics.DrawString("技能槽序:", fontsong, Brushes.SkyBlue, 180, 92);
                e.Graphics.DrawString(string.Format("第{0}技能", skillSpecial.Slot), fontsong, Brushes.White, 250, 92);
                e.Graphics.DrawString("当前效果:", fontsong, Brushes.SkyBlue, 180, 114);
                e.Graphics.DrawString("下级效果:", fontsong, Brushes.Plum, 180, 158);
                string needSkillStr = "";
                if (skillSpecial.SkillNeed.Length == 1 && skillSpecial.SkillNeed[0] == 0)
                {
                    needSkillStr = "无";
                }
                else
                {
                    foreach (int skillNed in skillSpecial.SkillNeed)
                    {
                        HeroSkillSpecialConfig skillCommonFormer = ConfigData.GetHeroSkillSpecialConfig(skillNed);
                        needSkillStr += string.Format("{0}Lv{1} ", ConfigData.GetSkillConfig(skillCommonFormer.SkillId).Name, skillCommonFormer.Level);
                    }
                }
                e.Graphics.DrawString("前置技能:", fontsong, Brushes.Plum, 180, 202);
                e.Graphics.DrawString(needSkillStr, fontsong, Brushes.Yellow, 250, 202);
                e.Graphics.DrawString("需要等级:", fontsong, Brushes.Plum, 180, 224);
                e.Graphics.DrawString(skillSpecial.HeroLevel.ToString(), fontsong, Brushes.Yellow, 250, 224);
                e.Graphics.DrawString("消耗金钱:", fontsong, Brushes.Plum, 180, 246);
                e.Graphics.DrawString(skillSpecial.GoldCost.ToString(), fontsong, Brushes.Yellow, 250, 246);
                e.Graphics.DrawString("消耗灵气:", fontsong, Brushes.Plum, 350, 246);
                e.Graphics.DrawString(skillSpecial.SkillPointCost.ToString(), fontsong, Brushes.Yellow, 420, 246);
                fontsong.Dispose();

                nowLevelDes.Draw(e.Graphics);
                nextLevelDes.Draw(e.Graphics);

                font = new Font("宋体", 9);
                string str = string.Format("我的灵气:  {0} ", UserProfile.InfoBasic.skillPoint);
                int strWid = (int)e.Graphics.MeasureString(str, font).Width;
                e.Graphics.DrawString(str, font, Brushes.White, 180, 303);
                font.Dispose();
                e.Graphics.DrawImage(HSIcons.GetIconsByEName("oth7"), 180 + strWid, 302, 14, 14);
            }
        }

        private void nlPageSelector1_PageChange(int pg)
        {
            pageid = pg;
            baseid = pageid * 5;
            refreshInfo();
        }

    }
}