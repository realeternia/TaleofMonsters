using NarlonLib.Control;

namespace TaleofMonsters.Controler.Battle
{
    sealed partial class BattleForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.bitmapButtonClose = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonAuto = new NarlonLib.Control.BitmapButton();
            this.panelState = new System.Windows.Forms.Panel();
            this.panelBattle = new NarlonLib.Control.DoubleBuffedPanel();
            this.miniItemView1 = new TaleofMonsters.Forms.Items.MiniItemView();
            this.cardList2 = new TaleofMonsters.Controler.Battle.Components.CardList();
            this.timeViewer1 = new TaleofMonsters.Controler.Battle.Components.TimeViewer();
            this.cardsArray1 = new TaleofMonsters.Controler.Battle.Components.CardsArray();
            this.lifeClock2 = new TaleofMonsters.Controler.Battle.Components.LifeClock();
            this.lifeClock1 = new TaleofMonsters.Controler.Battle.Components.LifeClock();
            this.bitmapButtonThrow = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonCode = new NarlonLib.Control.BitmapButton();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.White;
            this.buttonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("Matura MT Script Capitals", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonStart.Location = new System.Drawing.Point(350, 233);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(230, 110);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // bitmapButtonClose
            // 
            this.bitmapButtonClose.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonClose.Image = null;
            this.bitmapButtonClose.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonClose.ImageInactive = null;
            this.bitmapButtonClose.ImageMouseOver = null;
            this.bitmapButtonClose.ImageNormal = null;
            this.bitmapButtonClose.ImagePressed = null;
            this.bitmapButtonClose.Location = new System.Drawing.Point(891, 4);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 25;
            this.bitmapButtonClose.Text = "关闭窗口";
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // bitmapButtonAuto
            // 
            this.bitmapButtonAuto.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonAuto.Image = null;
            this.bitmapButtonAuto.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonAuto.ImageInactive = null;
            this.bitmapButtonAuto.ImageMouseOver = null;
            this.bitmapButtonAuto.ImageNormal = null;
            this.bitmapButtonAuto.ImagePressed = null;
            this.bitmapButtonAuto.Location = new System.Drawing.Point(18, 519);
            this.bitmapButtonAuto.Name = "bitmapButtonAuto";
            this.bitmapButtonAuto.Size = new System.Drawing.Size(50, 30);
            this.bitmapButtonAuto.StretchImage = true;
            this.bitmapButtonAuto.TabIndex = 41;
            this.bitmapButtonAuto.Text = "开启自动战斗";
            this.bitmapButtonAuto.UseVisualStyleBackColor = true;
            this.bitmapButtonAuto.Click += new System.EventHandler(this.bitmapButtonAuto_Click);
            // 
            // panelState
            // 
            this.panelState.BackColor = System.Drawing.Color.Black;
            this.panelState.Location = new System.Drawing.Point(72, 656);
            this.panelState.Name = "panelState";
            this.panelState.Size = new System.Drawing.Size(832, 20);
            this.panelState.TabIndex = 37;
            this.panelState.Paint += new System.Windows.Forms.PaintEventHandler(this.panelState_Paint);
            // 
            // panelBattle
            // 
            this.panelBattle.BackColor = System.Drawing.Color.Black;
            this.panelBattle.Location = new System.Drawing.Point(15, 110);
            this.panelBattle.Name = "panelBattle";
            this.panelBattle.Size = new System.Drawing.Size(900, 400);
            this.panelBattle.TabIndex = 34;
            this.panelBattle.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBattle_Paint);
            this.panelBattle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelBattle_MouseClick);
            this.panelBattle.MouseEnter += new System.EventHandler(this.panelBattle_MouseEnter);
            this.panelBattle.MouseLeave += new System.EventHandler(this.panelBattle_MouseLeave);
            this.panelBattle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelBattle_MouseMove);
            // 
            // miniItemView1
            // 
            this.miniItemView1.BackColor = System.Drawing.Color.Black;
            this.miniItemView1.Enabled = false;
            this.miniItemView1.ItemType = 11;
            this.miniItemView1.Location = new System.Drawing.Point(832, 519);
            this.miniItemView1.Name = "miniItemView1";
            this.miniItemView1.Size = new System.Drawing.Size(72, 135);
            this.miniItemView1.TabIndex = 38;
            // 
            // cardList2
            // 
            this.cardList2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cardList2.Location = new System.Drawing.Point(578, 74);
            this.cardList2.Marginp = 3;
            this.cardList2.MaxCards = 6;
            this.cardList2.Name = "cardList2";
            this.cardList2.Size = new System.Drawing.Size(225, 35);
            this.cardList2.TabIndex = 36;
            this.cardList2.Visible = false;
            // 
            // timeViewer1
            // 
            this.timeViewer1.BackColor = System.Drawing.Color.Black;
            this.timeViewer1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.timeViewer1.Location = new System.Drawing.Point(407, 38);
            this.timeViewer1.Name = "timeViewer1";
            this.timeViewer1.Size = new System.Drawing.Size(117, 66);
            this.timeViewer1.TabIndex = 35;
            this.timeViewer1.Click += new System.EventHandler(this.timeViewer1_Click);
            // 
            // cardsArray1
            // 
            this.cardsArray1.BackColor = System.Drawing.Color.DimGray;
            this.cardsArray1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardsArray1.Location = new System.Drawing.Point(72, 518);
            this.cardsArray1.Name = "cardsArray1";
            this.cardsArray1.Size = new System.Drawing.Size(754, 137);
            this.cardsArray1.TabIndex = 33;
            this.cardsArray1.SelectionChange += new TaleofMonsters.Controler.Battle.Components.CardsArray.CardArrayEventHandler(this.cardsArray1_SelectionChange);
            // 
            // lifeClock2
            // 
            this.lifeClock2.Location = new System.Drawing.Point(534, 35);
            this.lifeClock2.Name = "lifeClock2";
            this.lifeClock2.Size = new System.Drawing.Size(380, 70);
            this.lifeClock2.TabIndex = 32;
            this.lifeClock2.MouseLeave += new System.EventHandler(this.lifeClock1_MouseLeave);
            this.lifeClock2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lifeClock2_MouseMove);
            // 
            // lifeClock1
            // 
            this.lifeClock1.Location = new System.Drawing.Point(15, 35);
            this.lifeClock1.Name = "lifeClock1";
            this.lifeClock1.Size = new System.Drawing.Size(380, 70);
            this.lifeClock1.TabIndex = 31;
            this.lifeClock1.MouseLeave += new System.EventHandler(this.lifeClock1_MouseLeave);
            this.lifeClock1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lifeClock1_MouseMove);
            // 
            // bitmapButtonThrow
            // 
            this.bitmapButtonThrow.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonThrow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonThrow.Image = null;
            this.bitmapButtonThrow.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonThrow.ImageInactive = null;
            this.bitmapButtonThrow.ImageMouseOver = null;
            this.bitmapButtonThrow.ImageNormal = null;
            this.bitmapButtonThrow.ImagePressed = null;
            this.bitmapButtonThrow.Location = new System.Drawing.Point(18, 555);
            this.bitmapButtonThrow.Name = "bitmapButtonThrow";
            this.bitmapButtonThrow.Size = new System.Drawing.Size(50, 30);
            this.bitmapButtonThrow.StretchImage = true;
            this.bitmapButtonThrow.TabIndex = 42;
            this.bitmapButtonThrow.Text = "丢弃卡片";
            this.bitmapButtonThrow.UseVisualStyleBackColor = true;
            this.bitmapButtonThrow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.bitmapButtonThrow_MouseClick);
            // 
            // bitmapButtonCode
            // 
            this.bitmapButtonCode.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonCode.Image = null;
            this.bitmapButtonCode.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonCode.ImageInactive = null;
            this.bitmapButtonCode.ImageMouseOver = null;
            this.bitmapButtonCode.ImageNormal = null;
            this.bitmapButtonCode.ImagePressed = null;
            this.bitmapButtonCode.Location = new System.Drawing.Point(18, 591);
            this.bitmapButtonCode.Name = "bitmapButtonCode";
            this.bitmapButtonCode.Size = new System.Drawing.Size(50, 30);
            this.bitmapButtonCode.StretchImage = true;
            this.bitmapButtonCode.TabIndex = 43;
            this.bitmapButtonCode.Text = "指令";
            this.bitmapButtonCode.UseVisualStyleBackColor = true;
            this.bitmapButtonCode.Click += new System.EventHandler(this.bitmapButtonCode_Click);
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bitmapButtonCode);
            this.Controls.Add(this.bitmapButtonThrow);
            this.Controls.Add(this.bitmapButtonAuto);
            this.Controls.Add(this.miniItemView1);
            this.Controls.Add(this.panelState);
            this.Controls.Add(this.cardList2);
            this.Controls.Add(this.timeViewer1);
            this.Controls.Add(this.cardsArray1);
            this.Controls.Add(this.lifeClock2);
            this.Controls.Add(this.lifeClock1);
            this.Controls.Add(this.bitmapButtonClose);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.panelBattle);
            this.Name = "BattleForm";
            this.Size = new System.Drawing.Size(933, 705);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BattleForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private BitmapButton bitmapButtonClose;
        private BitmapButton bitmapButtonAuto;
        private TaleofMonsters.Forms.Items.MiniItemView miniItemView1;
        private System.Windows.Forms.Panel panelState;
        private TaleofMonsters.Controler.Battle.Components.CardList cardList2;
        private TaleofMonsters.Controler.Battle.Components.TimeViewer timeViewer1;
        private DoubleBuffedPanel panelBattle;
        private TaleofMonsters.Controler.Battle.Components.CardsArray cardsArray1;
        private TaleofMonsters.Controler.Battle.Components.LifeClock lifeClock2;
        private TaleofMonsters.Controler.Battle.Components.LifeClock lifeClock1;
        private BitmapButton bitmapButtonThrow;
        private BitmapButton bitmapButtonCode;
    }
}