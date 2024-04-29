using System.Drawing;
using System.Windows.Forms;
using NarlonLib.Control;
using NarlonLib.Drawing;
using NarlonLib.Math;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.HeroSkills;
using TaleofMonsters.DataType.Others;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Core;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms.Items.Regions;
using ConfigDatas;

namespace TaleofMonsters.Forms
{
    internal sealed partial class LevelUpForm : BasePanel
    {
        private int[] point;
        private int[] skillcommon;
        private int stindex;
        private int sindex;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;
        private VirtualRegion virtualRegion;

        public LevelUpForm()
        {
            InitializeComponent();
            this.bitmapButtonC1.ImageNormal = PicLoader.Read("Button", "LevelButton.JPG");
            this.bitmapButtonC1.ImageMouseOver = PicLoader.Read("Button", "LevelButtonOn.JPG");
            this.bitmapButtonC1.ImagePressed = PicLoader.Read("Button", "LevelButtonOn.JPG");
            virtualRegion = new VirtualRegion(this);
            virtualRegion.AddRegion(new SubVirtualRegion(1, 5, 5, 40, 40, 1));
            virtualRegion.AddRegion(new SubVirtualRegion(2, 5, 55, 40, 40, 2));
            virtualRegion.AddRegion(new SubVirtualRegion(3, 155, 5, 40, 40, 3));
            virtualRegion.AddRegion(new SubVirtualRegion(4, 155, 55, 40, 40, 4));
            virtualRegion.RegionEntered += new VirtualRegion.VRegionEnteredEventHandler(virtualRegion_RegionEntered);
            virtualRegion.RegionLeft += new VirtualRegion.VRegionLeftEventHandler(virtualRegion_RegionLeft);
        }

        internal override void Init()
        {
            base.Init();

            CheckLevelInfo();
        }

        private void CheckLevelInfo()
        {
            SoundManager.Play("System", "LevelUp.wav");

            int nowlevel = UserProfile.InfoBasic.level + 1;
            Text = string.Format("恭喜你提升到{0}级", nowlevel);

            JobConfig jobConfig = ConfigData.GetJobConfig(UserProfile.InfoBasic.job);

            point = new int[8];
            for (int i = 0; i < 8; i++)
            {
                point[i] = 0;
            }
            for (int i = 0; i < 10; i++) //一级10个点
            {
                RandomMaker maker2 = new RandomMaker();
                maker2.Add(1, jobConfig.Atkp);
                maker2.Add(2, jobConfig.Defp);
                maker2.Add(3, jobConfig.Sklp);
                maker2.Add(4, jobConfig.Magp);
                maker2.Add(5, jobConfig.Spdp);
                maker2.Add(6, jobConfig.Lukp);
                maker2.Add(7, jobConfig.Vitp);
                maker2.Add(8, jobConfig.Adpp);
                point[maker2.Process(1)[0] - 1]++;
            }

            RandomMaker maker = new RandomMaker();
            foreach (HeroSkillCommonConfig skill in ConfigData.HeroSkillCommonDict.Values)
            {
                if (skill.HeroLevel > nowlevel)
                    continue;

                if (skill.Level == 1 && UserProfile.InfoSkill.skillCommons.Count >= 8)
                    continue;

                if (UserProfile.InfoSkill.HasSkillCommon(skill.Id))
                    continue;

                if (skill.ForeSkill != 0 && !UserProfile.InfoSkill.HasSkillCommon(skill.ForeSkill)) 
                    continue;

                int skillRate = JobBook.GetSkillCommonRate(jobConfig.Id, skill.Group - 1);
                if (skillRate > 0)
                {
                    maker.Add(skill.Id, skillRate);
                }
            }
            skillcommon = maker.Process(3);

            sindex = -1;
            stindex = 0;
        }

        private void LevelUpForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(Text, font, Brushes.White, Width / 2 - 70, 8);
            font.Dispose();

            Font font2 = new Font("宋体", 9, FontStyle.Regular);
            for (int i = 0; i < 8; i++)
            {
                e.Graphics.DrawImage(HSIcons.GetIconsByEName(string.Format("hatt{0}", i + 1)), 29 + (i % 2) * 150, 66 + (i / 2) * 24, 24, 24);
                e.Graphics.DrawString(string.Format(ConfigData.GetEquipAddonConfig(i + 1).Format, point[i]), font2, Brushes.White, 29 + (i % 2) * 150 + 32, 72 + (i / 2) * 24);
            }
            font2.Dispose();
        }

        private void buttonLevelUp_Click(object sender, System.EventArgs e)
        {
            if (UserProfile.InfoBasic.CheckNewLevel())
            {
                UserProfile.InfoBasic.skillPoint++;
                for (int i = 0; i < 8; i++)
                {
                    UserProfile.InfoBasic.AddSkillValueById(i + 1, point[i]);
                }
                if (stindex >= 0 && stindex < 3)
                {
                    UserProfile.InfoSkill.AddSkillCommon(skillcommon[stindex]);
                }
                else
                {
                    UserProfile.InfoBasic.skillPoint++;                    
                }
            }

            if (UserProfile.InfoBasic.exp >= ExpTree.GetNextRequired(UserProfile.InfoBasic.level))
            {
                CheckLevelInfo();
                Invalidate();
                panelSkill.Invalidate();
            }
            else
            {
                MainItem.SystemMenuManager.ResetIconState();
                MainForm.Instance.RefreshPanel();
                Close();

                MainItem.PanelManager.ShowLevelInfo(UserProfile.InfoBasic.level);
            }
        }

        private void panelSkill_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.DarkBlue, 150 * (stindex / 2), 50 * (stindex % 2), 150, 50);

            Font font2 = new Font("宋体", 9, FontStyle.Regular);
            if (skillcommon[0] > 0)
            {
                e.Graphics.DrawImage(HeroSkillBook.GetHeroSkillCommonImage(skillcommon[0]), 5, 5, 40, 40);
                e.Graphics.DrawString(ConfigData.GetHeroSkillCommonConfig(skillcommon[0]).Name, font2, Brushes.White, 59, 17);
            }
            if (skillcommon[1] > 0)
            {
                e.Graphics.DrawImage(HeroSkillBook.GetHeroSkillCommonImage(skillcommon[1]), 5, 55, 40, 40);
                e.Graphics.DrawString(ConfigData.GetHeroSkillCommonConfig(skillcommon[1]).Name, font2, Brushes.White, 59, 67);
            }
            if (skillcommon[2] > 0)
            {
                e.Graphics.DrawImage(HeroSkillBook.GetHeroSkillCommonImage(skillcommon[2]), 155, 5, 40, 40);
                e.Graphics.DrawString(ConfigData.GetHeroSkillCommonConfig(skillcommon[2]).Name, font2, Brushes.White, 209, 17);
            }
            e.Graphics.DrawImage(HeroSkillBook.GetHeroSkillCommonDirectImage("skill99"), 155, 55, 40, 40);
            e.Graphics.DrawString("灵气+1", font2, Brushes.White, 209, 67);

            font2.Dispose();
            if (sindex >= 0)
            {
                Pen pen = new Pen(Color.Goldenrod, 3);
                e.Graphics.DrawRectangle(pen, 150 * (sindex / 2), 50 * (sindex % 2), 147, 47);
                pen.Dispose();
            }
        }

        private void panelSkill_MouseMove(object sender, MouseEventArgs e)
        {
            sindex = (e.X/150)*2 + e.Y/50;
            if (sindex < 3 && sindex >= 0 && skillcommon[sindex] == 0)
            {
                sindex = -1;
            }
            panelSkill.Invalidate();
        }

        private void panelSkill_MouseLeave(object sender, System.EventArgs e)
        {
            sindex = -1;
            panelSkill.Invalidate();
        }

        private void panelSkill_MouseClick(object sender, MouseEventArgs e)
        {
            if (sindex != -1 && sindex != stindex)
            {
                stindex = sindex;
                panelSkill.Invalidate();
            }
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            int id = info;
            if (id < 4 && skillcommon[id - 1] != 0)
            {
                int sid = skillcommon[id - 1];
                Image image = DrawTool.GetImageByString(ConfigData.GetHeroSkillCommonConfig(sid).Des, 200);
                tooltip.Show(image, this, panelSkill.Location.X + x, panelSkill.Location.Y + y);
            }
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(this);
        }
    }
}