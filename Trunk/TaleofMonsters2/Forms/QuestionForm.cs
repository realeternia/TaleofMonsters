using System;
using System.Drawing;
using System.Windows.Forms;
using ControlPlus;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Others;
using TaleofMonsters.DataType.User;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.Forms.Items.Core;

namespace TaleofMonsters.Forms
{
    internal partial class QuestionForm : BasePanel
    {
        private ColorWordRegion colorWord;
        private string result;
        private string guess;

        public QuestionForm()
        {
            InitializeComponent();
            colorWord = new ColorWordRegion(21, 44, 274, "Î¢ÈíÑÅºÚ", 11, Color.White);
            colorWord.Bold = true;
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonC1.ImageNormal = PicLoader.Read("Button", "AnswerButton.JPG");
            this.bitmapButtonC1.ImageMouseOver = PicLoader.Read("Button", "AnswerButtonOn.JPG");
            this.bitmapButtonC1.ImagePressed = PicLoader.Read("Button", "AnswerButtonOn.JPG");
        }

        internal override void Init()
        {
            base.Init();

            Question qes = QuestionBook.GetQuestion();
            colorWord.Text = qes.info;
            radioButton1.Text = qes.GetAns(0);
            radioButton2.Text = qes.GetAns(1);
            radioButton3.Text = qes.GetAns(2);
            radioButton4.Text = qes.GetAns(3);
            result = qes.GetResult();
            guess = qes.GetAns(0);
        }

        private void ConnectForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("ºÚÌå", 12, FontStyle.Bold);
            e.Graphics.DrawString("»ÃÊÞÎÊ´ð", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();

            colorWord.Draw(e.Graphics);
        }

        private void bitmapButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            UserProfile.InfoRecord.SetRecordById((int)MemPlayerRecordTypes.LastQuestionTime, NarlonLib.Core.TimeTool.DateTimeToUnixTime(DateTime.Now) + SysConstants.QuestionCooldownDura);
            if (result == guess)
            {
                UserProfile.InfoBag.AddResource(GameResourceType.Gold, 20);
                MessageBoxEx.Show("»Ø´ðÕýÈ·£¬½±Àø20½ð±Ò");
            }
            else
            {
                MessageBoxEx.Show("»Ø´ð´íÎó");
            }
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null) 
                guess = rb.Text;
        }
    }
}