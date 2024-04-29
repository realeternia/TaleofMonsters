using System;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using TaleofMonsters.Controler.Battle;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Peoples;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.Forms
{
    internal partial class CommandForm : Form
    {
        private string cmd;
        private string hint;
        public CommandForm()
        {
            InitializeComponent();
        }

        private void textBoxTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                GMCommand.ParseCommand(textBoxTitle.Text);
                Close();
            }
        }

        private void textBoxTitle_TextChanged(object sender, System.EventArgs e)
        {
            cmd = "";
            if (textBoxTitle.Text.Contains("exp"))
            {
                cmd = "exp";
                hint = "提升角色[p1]点经验值";
            }
            if (textBoxTitle.Text.Contains("atp"))
            {
                cmd = "atp";
                hint = "提升角色[p1]点阅历";
            }
            if (textBoxTitle.Text.Contains("skp"))
            {
                cmd = "skp";
                hint = "提升角色[p1]点灵气";
            }
            if (textBoxTitle.Text.Contains("emys"))
            {
                cmd = "emys";
                hint = "使所有角色进入对战列表";
            }
            if (textBoxTitle.Text.Contains("mov"))
            {
                cmd = "mov";
                hint = "直接移动到场景[p1]";
            }
            if (textBoxTitle.Text.Contains("eqp"))
            {
                cmd = "eqp";
                hint = "获得装备[p1]";
            }
            if (textBoxTitle.Text.Contains("itm"))
            {
                cmd = "itm";
                hint = "获得物品[p1]x[p2]";
            }
            if (textBoxTitle.Text.Contains("gold"))
            {
                cmd = "gold";
                hint = "获得黄金[p1]";
            }
            if (textBoxTitle.Text.Contains("dmd"))
            {
                cmd = "dmd";
                hint = "获得钻石[p1]";
            }
            if (textBoxTitle.Text.Contains("tsk"))
            {
                cmd = "tsk";
                hint = "接受任务[p1]";
            }
            if (textBoxTitle.Text.Contains("acv"))
            {
                cmd = "acv";
                hint = "实现成就[p1]";
            }
            if (textBoxTitle.Text.Contains("view"))
            {
                cmd = "view";
                hint = "观看比赛[p1]Vs[p2]";
            }
            if (textBoxTitle.Text.Contains("fbat"))
            {
                cmd = "fbat";
                hint = "快速战斗[p1]Vs[p2]";
            }
            Invalidate();
        }

        private void CommandForm_Paint(object sender, PaintEventArgs e)
        {
            if (cmd == "")
            {
                return;
            }

            Font ft = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            e.Graphics.DrawString(cmd, ft, Brushes.Lime, 0, 23);
            e.Graphics.DrawString(hint, ft, Brushes.White, 30, 23);
        }
    }

    internal class GMCommand
    {
        public static void ParseCommand(string cmd)
        {
            string[] data = cmd.Split(' ');
            if (data.Length == 0) return;
            try
            {
                switch (data[0])
                {
                    case "exp": if (data.Length == 2) UserProfile.InfoBasic.AddExp(int.Parse(data[1])); break;
                    case "skp": if (data.Length == 2) UserProfile.InfoBasic.skillPoint += int.Parse(data[1]); break;
                    case "atp": if (data.Length == 2) UserProfile.InfoBasic.attrPoint += int.Parse(data[1]); break;
                    case "mov": if (data.Length == 2) MainForm.Instance.ChangeMap(int.Parse(data[1])); break;
                    case "eqp": if (data.Length == 2) UserProfile.InfoBag.AddEquip(int.Parse(data[1])); break;
                    case "itm": if (data.Length == 3) UserProfile.InfoBag.AddItem(int.Parse(data[1]), int.Parse(data[2])); break;
                    case "emys": foreach (int peopleId in ConfigData.PeopleDict.Keys)
                        {
                            RivalState rival = new RivalState(peopleId);
                            rival.avail = true;
                            UserProfile.InfoRival.Rivals[peopleId] = rival;
                        }
                        break;
                    case "gold": if (data.Length == 2)
                        {
                            GameResource res = new GameResource();
                            res.Gold = int.Parse(data[1]);
                            UserProfile.InfoBag.AddResource(res.ToArray());
                        } break;
                    case "dmd": if (data.Length == 2) UserProfile.InfoBag.AddDiamond(int.Parse(data[1])); break;
                    case "tsk": if (data.Length == 2) UserProfile.InfoTask.BeginTask(int.Parse(data[1])); break;
                    case "acv": if (data.Length == 2) UserProfile.Profile.InfoAchieve.SetAchieve(int.Parse(data[1])); break;
                    case "view": if (data.Length == 3) PeopleBook.ViewMatch(int.Parse(data[1]), int.Parse(data[2]), "default", 9); break;
                    case "fbat": if (data.Length == 3)
                        {
                            FastBattle.Instance.StartGame(int.Parse(data[1]), int.Parse(data[2]), "default", 9);
                            MainForm.Instance.AddTip(string.Format("{0} {1}合", FastBattle.Instance.LeftWin ? "左胜" : "右胜", BattleInfo.Instance.Round), "White");
                        } break;
                }
            }
            catch (FormatException) { }
        }
    }
}