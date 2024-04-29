using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ControlPlus;
using NarlonLib.Control;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.DataType.HeroSkills;
using TaleofMonsters.Forms.Items.Core;
using ConfigDatas;
using TaleofMonsters.Forms.Items.Regions;
using TaleofMonsters.Core;
using TaleofMonsters.Forms.Items.Regions.Decorators;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.Forms
{
    internal sealed partial class SelectJobForm : BasePanel
    {
        private NLSelectPanel selectPanel;
        private NLPageSelector nlPageSelector1;
        private int pageid;
        private int baseid;
        private List<int> jobIds;
        private int selectJob;
        private VirtualRegion virtualRegion;
        private ColorWordRegion jobDes;
        private ImageToolTip tooltip = MainItem.SystemToolTip.Instance;

        public SelectJobForm()
        {
            InitializeComponent();
            NeedBlackForm = true;
            this.bitmapButtonSelect.ImageNormal = PicLoader.Read("Button", "ExpButton.JPG");
            this.bitmapButtonSelect.ImageMouseOver = PicLoader.Read("Button", "ExpButtonOn.JPG");
            this.bitmapButtonSelect.ImagePressed = PicLoader.Read("Button", "ExpButtonOn.JPG");

            selectPanel = new NLSelectPanel(8, 38, 154, 400, this);
            selectPanel.ItemHeight = 35;
            selectPanel.SelectIndexChanged += selectPanel_SelectedIndexChanged;
            selectPanel.DrawCell += selectPanel_DrawCell;

            this.nlPageSelector1 = new NLPageSelector(this, 10, 291, 150);
            nlPageSelector1.PageChange += nlPageSelector1_PageChange;

            jobDes = new ColorWordRegion(180, 45, 320, "宋体", 10, Color.White);

            virtualRegion = new VirtualRegion(this);
            for (int i = 0; i < 4; i++)
            {
                PictureRegion region = new PictureRegion(i + 1, 178 + 56 * i, 266, 48, 48, i + 1, VirtualRegionCellType.SkillSpecial, 0);
                region.AddDecorator(new RegionBorderDecorator(region, Color.DodgerBlue));
                virtualRegion.AddRegion(region);
            }
            virtualRegion.RegionEntered += virtualRegion_RegionEntered;
            virtualRegion.RegionLeft += virtualRegion_RegionLeft;
     }

        internal override void Init()
        {
            base.Init();
            jobIds = new List<int>();
            foreach (JobConfig jobConfig in ConfigData.JobDict.Values)
            {
                if (jobConfig.Id>0)
                {
                    jobIds.Add(jobConfig.Id);
                }
            }
            nlPageSelector1.TotalPage = (jobIds.Count+7)/8;
            refreshInfo();
        }

        private void refreshInfo()
        {
            selectPanel.ClearContent();
            for (int i = baseid; i < Math.Min(baseid + 8, jobIds.Count); i++)
            {
                selectPanel.AddContent(jobIds[i]);
            }
           
            selectPanel.SelectIndex = 0;
        }

        private void selectPanel_SelectedIndexChanged()
        {
            selectJob = jobIds[baseid + selectPanel.SelectIndex];
            jobDes.Text = ConfigData.GetJobConfig(selectJob).Des;
            for (int i = 0; i < 4; i++)
            {
                virtualRegion.SetRegionInfo(i + 1, ConfigData.GetJobConfig(selectJob).SpecialSkills[i]);
            }
            Invalidate();
        }

        private void selectPanel_DrawCell(Graphics g, int info, int xOff, int yOff)
        {
            JobConfig jobConfig = ConfigData.GetJobConfig(info);
            g.DrawImage(HSIcons.GetIconsByEName(string.Format("job{0}",jobConfig.Id)),14+xOff,4+yOff,28,28);
            Font font = new Font("微软雅黑", 11.25F, FontStyle.Bold);
            g.DrawString(jobConfig.Name, font, Brushes.White , 75 + xOff, 6 + yOff);
            font.Dispose();
        }

        private void virtualRegion_RegionEntered(int info, int x, int y, int key)
        {
            Image image = HeroSkillBook.GetSkillSpecialPreview(key);
            tooltip.Show(image, this, x, y);
        }

        private void virtualRegion_RegionLeft()
        {
            tooltip.Hide(this);
        }

        private string GetStarText(int count)
        {
            return "★★★★★★★★★★".Substring(0, Math.Min(count, 9));
        }

        private void SkillForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString("选择职业", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            jobDes.Draw(e.Graphics);
            virtualRegion.Draw(e.Graphics);

            if (selectJob > 0)
            {
                Font fontDes = new Font("宋体", 10, FontStyle.Regular);
                JobConfig job = ConfigData.GetJobConfig(selectJob);
                e.Graphics.DrawString("攻击", fontDes, Brushes.White, 180, 175);
                e.Graphics.DrawString(GetStarText(job.Atk/3 + 1), fontDes, Brushes.Yellow, 210, 175);
                e.Graphics.DrawString("魔力", fontDes, Brushes.White, 180, 195);
                e.Graphics.DrawString(GetStarText(job.Mag / 3 + 1), fontDes, Brushes.Yellow, 210, 195);
                e.Graphics.DrawString("速度", fontDes, Brushes.White, 180, 215);
                e.Graphics.DrawString(GetStarText(job.Spd / 3 + 1), fontDes, Brushes.Yellow, 210, 215);
                e.Graphics.DrawString("体能", fontDes, Brushes.White, 180, 235);
                e.Graphics.DrawString(GetStarText(job.Vit / 3 + 1), fontDes, Brushes.Yellow, 210, 235);
                e.Graphics.DrawString("防御", fontDes, Brushes.White, 335, 175);
                e.Graphics.DrawString(GetStarText(job.Def / 3 + 1), fontDes, Brushes.Yellow, 365, 175);
                e.Graphics.DrawString("特技", fontDes, Brushes.White, 335, 195);
                e.Graphics.DrawString(GetStarText(job.Skl / 3 + 1), fontDes, Brushes.Yellow, 365, 195);
                e.Graphics.DrawString("幸运", fontDes, Brushes.White, 335, 215);
                e.Graphics.DrawString(GetStarText(job.Luk / 3 + 1), fontDes, Brushes.Yellow, 365, 215);
                e.Graphics.DrawString("适应", fontDes, Brushes.White, 335, 235);
                e.Graphics.DrawString(GetStarText(job.Adp / 3 + 1), fontDes, Brushes.Yellow, 365, 235);
                fontDes.Dispose();
            }


            e.Graphics.DrawRectangle(Pens.DodgerBlue, 8, 39, 153, 279);
            e.Graphics.DrawRectangle(Pens.DodgerBlue, 170, 38, 330, 125);
            e.Graphics.DrawRectangle(Pens.DodgerBlue, 170, 168, 330, 86);
            e.Graphics.DrawRectangle(Pens.DodgerBlue, 170, 259, 330, 59);
        }

        private void nlPageSelector1_PageChange(int pg)
        {
            pageid = pg;
            baseid = pageid * 8;
            refreshInfo();
        }

        private void bitmapButtonSelect_Click(object sender, EventArgs e)
        {
            if (UserProfile.Profile.InfoBasic.job == 0)
            {
                UserProfile.Profile.InfoBasic.job = selectJob;
            }
       
            Close();
        }

    }
}