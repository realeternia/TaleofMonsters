using NarlonLib.Control;

namespace TaleofMonsters.Forms
{
    sealed partial class DeckViewForm
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
            this.bitmapButtonClose = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonDel = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonAdd = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonNextD = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonPreD = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonNext = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonPre = new NarlonLib.Control.BitmapButton();
            this.SuspendLayout();
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(700, 3);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 26;
            this.bitmapButtonClose.Text = "关闭窗口";
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // bitmapButtonDel
            // 
            this.bitmapButtonDel.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonDel.Image = null;
            this.bitmapButtonDel.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonDel.ImageInactive = null;
            this.bitmapButtonDel.ImageMouseOver = null;
            this.bitmapButtonDel.ImageNormal = null;
            this.bitmapButtonDel.ImagePressed = null;
            this.bitmapButtonDel.Location = new System.Drawing.Point(210, 466);
            this.bitmapButtonDel.Name = "bitmapButtonDel";
            this.bitmapButtonDel.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonDel.StretchImage = true;
            this.bitmapButtonDel.TabIndex = 40;
            this.bitmapButtonDel.Text = "清空卡组";
            this.bitmapButtonDel.UseVisualStyleBackColor = true;
            this.bitmapButtonDel.Click += new System.EventHandler(this.buttonDelD_Click);
            // 
            // bitmapButtonAdd
            // 
            this.bitmapButtonAdd.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonAdd.Image = null;
            this.bitmapButtonAdd.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonAdd.ImageInactive = null;
            this.bitmapButtonAdd.ImageMouseOver = null;
            this.bitmapButtonAdd.ImageNormal = null;
            this.bitmapButtonAdd.ImagePressed = null;
            this.bitmapButtonAdd.Location = new System.Drawing.Point(185, 466);
            this.bitmapButtonAdd.Name = "bitmapButtonAdd";
            this.bitmapButtonAdd.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonAdd.StretchImage = true;
            this.bitmapButtonAdd.TabIndex = 39;
            this.bitmapButtonAdd.Text = "重命名卡组";
            this.bitmapButtonAdd.UseVisualStyleBackColor = true;
            this.bitmapButtonAdd.Click += new System.EventHandler(this.buttonAddD_Click);
            // 
            // bitmapButtonNextD
            // 
            this.bitmapButtonNextD.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonNextD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonNextD.Image = null;
            this.bitmapButtonNextD.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonNextD.ImageInactive = null;
            this.bitmapButtonNextD.ImageMouseOver = null;
            this.bitmapButtonNextD.ImageNormal = null;
            this.bitmapButtonNextD.ImagePressed = null;
            this.bitmapButtonNextD.Location = new System.Drawing.Point(159, 466);
            this.bitmapButtonNextD.Name = "bitmapButtonNextD";
            this.bitmapButtonNextD.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonNextD.StretchImage = true;
            this.bitmapButtonNextD.TabIndex = 38;
            this.bitmapButtonNextD.Text = "后一卡组";
            this.bitmapButtonNextD.UseVisualStyleBackColor = true;
            this.bitmapButtonNextD.Click += new System.EventHandler(this.buttonNextD_Click);
            // 
            // bitmapButtonPreD
            // 
            this.bitmapButtonPreD.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonPreD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonPreD.Image = null;
            this.bitmapButtonPreD.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonPreD.ImageInactive = null;
            this.bitmapButtonPreD.ImageMouseOver = null;
            this.bitmapButtonPreD.ImageNormal = null;
            this.bitmapButtonPreD.ImagePressed = null;
            this.bitmapButtonPreD.Location = new System.Drawing.Point(134, 466);
            this.bitmapButtonPreD.Name = "bitmapButtonPreD";
            this.bitmapButtonPreD.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonPreD.StretchImage = true;
            this.bitmapButtonPreD.TabIndex = 37;
            this.bitmapButtonPreD.Text = "前一卡组";
            this.bitmapButtonPreD.UseVisualStyleBackColor = true;
            this.bitmapButtonPreD.Click += new System.EventHandler(this.buttonPreD_Click);
            // 
            // bitmapButtonNext
            // 
            this.bitmapButtonNext.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonNext.Image = null;
            this.bitmapButtonNext.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonNext.ImageInactive = null;
            this.bitmapButtonNext.ImageMouseOver = null;
            this.bitmapButtonNext.ImageNormal = null;
            this.bitmapButtonNext.ImagePressed = null;
            this.bitmapButtonNext.Location = new System.Drawing.Point(57, 450);
            this.bitmapButtonNext.Name = "bitmapButtonNext";
            this.bitmapButtonNext.Size = new System.Drawing.Size(40, 40);
            this.bitmapButtonNext.StretchImage = true;
            this.bitmapButtonNext.TabIndex = 36;
            this.bitmapButtonNext.Text = "下一页";
            this.bitmapButtonNext.UseVisualStyleBackColor = true;
            this.bitmapButtonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // bitmapButtonPre
            // 
            this.bitmapButtonPre.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonPre.Image = null;
            this.bitmapButtonPre.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonPre.ImageInactive = null;
            this.bitmapButtonPre.ImageMouseOver = null;
            this.bitmapButtonPre.ImageNormal = null;
            this.bitmapButtonPre.ImagePressed = null;
            this.bitmapButtonPre.Location = new System.Drawing.Point(11, 450);
            this.bitmapButtonPre.Name = "bitmapButtonPre";
            this.bitmapButtonPre.Size = new System.Drawing.Size(40, 40);
            this.bitmapButtonPre.StretchImage = true;
            this.bitmapButtonPre.TabIndex = 35;
            this.bitmapButtonPre.Text = "上一页";
            this.bitmapButtonPre.UseVisualStyleBackColor = true;
            this.bitmapButtonPre.Click += new System.EventHandler(this.buttonPre_Click);
            // 
            // DeckViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonDel);
            this.Controls.Add(this.bitmapButtonNextD);
            this.Controls.Add(this.bitmapButtonAdd);
            this.Controls.Add(this.bitmapButtonPreD);
            this.Controls.Add(this.bitmapButtonNext);
            this.Controls.Add(this.bitmapButtonClose);
            this.Controls.Add(this.bitmapButtonPre);
            this.DoubleBuffered = true;
            this.Name = "DeckViewForm";
            this.Size = new System.Drawing.Size(810, 520);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DeckViewForm_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DeckViewForm_MouseMove);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DeckViewForm_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapButton bitmapButtonClose;
        private BitmapButton bitmapButtonDel;
        private BitmapButton bitmapButtonAdd;
        private BitmapButton bitmapButtonNextD;
        private BitmapButton bitmapButtonPreD;
        private BitmapButton bitmapButtonNext;
        private BitmapButton bitmapButtonPre;
    }
}