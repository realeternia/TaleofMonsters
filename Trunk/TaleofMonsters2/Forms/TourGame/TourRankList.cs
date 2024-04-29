using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.User.Mem;

namespace TaleofMonsters.Forms.TourGame
{
    internal partial class TourRankList : UserControl
    {
        private bool show;
        private MemRankData[] ranks;

        public TourRankList()
        {
            InitializeComponent();
        }

        public void Init()
        {
            show = true;

            ranks = UserProfile.InfoWorld.GetAllPeopleRank();
            Array.Sort(ranks, new CompareByMark());

            Height = Math.Max(410, ranks.Length * 32 + 10);
            Invalidate();
        }

        private void TourRankList_Paint(object sender, PaintEventArgs e)
        {
            if (show)
            {
                Font font = new Font("宋体", 11, FontStyle.Regular);
                for (int i = 0; i < ranks.Length; i++)
                {
                    Image head = MatchManager.GetHeadImage(ranks[i].id);
                    PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(ranks[i].id);
                    e.Graphics.DrawImage(head, 85, 32*i, 24, 24);
                    e.Graphics.DrawString(string.Format("{0}", i + 1), font, Brushes.White, 35, 32*i + 5);
                    e.Graphics.DrawString(string.Format("{0}", ranks[i].id == -1 ? UserProfile.ProfileName : peopleConfig.Name), font, Brushes.White, 120, 32*i + 5);
                    e.Graphics.DrawString(string.Format("{0}", ranks[i].id == -1 ? UserProfile.InfoBasic.level : peopleConfig.Level), font, Brushes.White, 220, 32 * i + 5);
                    e.Graphics.DrawString(string.Format("{0}", ConfigData.GetJobConfig(ranks[i].id == -1 ? UserProfile.InfoBasic.job : peopleConfig.Job).Name), font, Brushes.White, 260, 32 * i + 5);
                    e.Graphics.DrawString(string.Format("{0}", ranks[i].mark).PadLeft(5, ' '), font, Brushes.Gold, 360, 32*i + 5);
                }

                font.Dispose();
            }
        }
    }

    internal class CompareByMark : IComparer<MemRankData>
    {
        #region IComparer<MemRankData> 成员

        public int Compare(MemRankData x, MemRankData y)
        {
            return y.mark - x.mark;
        }

        #endregion
    }
}
