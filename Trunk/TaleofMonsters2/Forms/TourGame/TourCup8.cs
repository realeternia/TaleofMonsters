﻿using System;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.User.Mem;

namespace TaleofMonsters.Forms.TourGame
{
    internal partial class TourCup8 : UserControl
    {
        private bool show;
        private MemTournamentData tourData;
        private Button[] buttons;
        private int tid;

        public TourCup8()
        {
            InitializeComponent();            
        }

        public void Init(int id)
        {
            show = true;

            tid = id;
            tourData = UserProfile.InfoWorld.GetTournamentData(id);

            if (buttons != null)
            {
                foreach (Button button in buttons)
                {
                    Controls.Remove(button);
                }
            }

            buttons = new Button[7];
            for (int i = 0; i < 4; i++)
            {
                Button btn = MatchManager.GetButton(i, 152, 47 + 100 * i);
                btn.Click += new EventHandler(button1_Click);
                Controls.Add(btn);
                buttons[i] = btn;
            }
            for (int i = 0; i < 2; i++)
            {
                Button btn = MatchManager.GetButton(i + 4, 290, 97 + 200*i);
                btn.Click += new EventHandler(button1_Click);
                Controls.Add(btn);
                buttons[i+4] = btn;
            }
            Button btn2 = MatchManager.GetButton(6, 426, 197);
            btn2.Click += new EventHandler(button1_Click);
            Controls.Add(btn2);
            buttons[6] = btn2;

            RefreshButtons();

            Invalidate();
        }

        private void RefreshButtons()
        {
        //    Tournament tour = TournamentBook.GetTournament(tid);
         //   int date = UserProfile.Profile.time.Date;

            //for (int i = 0; i < 7; i++)
            //{
            //    buttons[i].Visible = tour.matchs[i].date == date && tourData.results[i].winner == 0;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int mid = int.Parse(((Button)sender).Tag.ToString());

            tourData.CheckMatch(mid, false);

            Invalidate();
            RefreshButtons();
        }

        private bool IsPlayerLost(int pid)
        {
            foreach (MatchResult matchResult in tourData.results)
            {
                if (matchResult.loser == pid)
                {
                    return true;
                }
            }
            return false;
        }

        private void TourCup8_Paint(object sender, PaintEventArgs e)
        {
            if (show)
            {
                Font font = new Font("宋体", 11, FontStyle.Regular);

                for (int i = 0; i < 8; i++)
                {
                    Image head = MatchManager.GetHeadImage(tourData.pids[i]);
                    if (head != null)
                    {
                        e.Graphics.DrawImage(head, 10, 50 * i + 10, 36, 36);
                    }
                    e.Graphics.DrawString(MatchManager.GetPlayerName(tourData.pids[i]), font, IsPlayerLost(tourData.pids[i]) ? Brushes.DimGray : Brushes.White, 50, 50 * i + 25);
                }
                for (int i = 0; i < 4; i++)
                {
                    MatchManager.DrawCrossing(e.Graphics, 100, 100 * i + 33, 140, 100 * i + 83);
                    if (tourData.results[i].winner != 0)
                    {
                        Image head = MatchManager.GetHeadImage(tourData.results[i].winner);
                        if (head != null)
                        {
                            e.Graphics.DrawImage(head, 148, 100 * i + 35, 36, 36);
                        }
                        e.Graphics.DrawString(MatchManager.GetPlayerName(tourData.results[i].winner), font, IsPlayerLost(tourData.results[i].winner) ? Brushes.DimGray : Brushes.White, 188, 100 * i + 50);
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    MatchManager.DrawCrossing(e.Graphics, 238, 200 * i + 58, 278, 200 * i + 158);
                    if (tourData.results[i + 4].winner != 0)
                    {
                        Image head = MatchManager.GetHeadImage(tourData.results[i + 4].winner);
                        if (head != null)
                        {
                            e.Graphics.DrawImage(head, 286, 200 * i + 85, 36, 36);
                        }
                        e.Graphics.DrawString(MatchManager.GetPlayerName(tourData.results[i + 4].winner), font, IsPlayerLost(tourData.results[i + 4].winner) ? Brushes.DimGray : Brushes.White, 326, 200 * i + 100);
                    }
                }
                MatchManager.DrawCrossing(e.Graphics, 376, 108, 416, 308);
                if (tourData.results[6].winner != 0)
                {
                    Image head = MatchManager.GetHeadImage(tourData.results[6].winner);
                    if (head != null)
                    {
                        e.Graphics.DrawImage(head, 424, 185, 36, 36);
                    }
                    e.Graphics.DrawString(MatchManager.GetPlayerName(tourData.results[6].winner), font, Brushes.White, 464, 200);
                }
            }
        }
    }
}
