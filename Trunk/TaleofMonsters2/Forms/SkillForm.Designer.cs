using NarlonLib.Control;

namespace TaleofMonsters.Forms
{
    sealed partial class SkillForm
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
            this.bitmapButtonActive = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonUpgrade = new NarlonLib.Control.BitmapButton();
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(468, 13);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 26;
            this.bitmapButtonClose.Text = "关闭窗口";
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // bitmapButtonActive
            // 
            this.bitmapButtonActive.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonActive.Image = null;
            this.bitmapButtonActive.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonActive.ImageInactive = null;
            this.bitmapButtonActive.ImageMouseOver = null;
            this.bitmapButtonActive.ImageNormal = null;
            this.bitmapButtonActive.ImagePressed = null;
            this.bitmapButtonActive.Location = new System.Drawing.Point(338, 295);
            this.bitmapButtonActive.Name = "bitmapButtonActive";
            this.bitmapButtonActive.Size = new System.Drawing.Size(50, 30);
            this.bitmapButtonActive.StretchImage = true;
            this.bitmapButtonActive.TabIndex = 28;
            this.bitmapButtonActive.Tag = "1";
            this.bitmapButtonActive.UseVisualStyleBackColor = true;
            this.bitmapButtonActive.Click += new System.EventHandler(this.bitmapButtonActive_Click);
            // 
            // bitmapButtonUpgrade
            // 
            this.bitmapButtonUpgrade.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonUpgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonUpgrade.Image = null;
            this.bitmapButtonUpgrade.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonUpgrade.ImageInactive = null;
            this.bitmapButtonUpgrade.ImageMouseOver = null;
            this.bitmapButtonUpgrade.ImageNormal = null;
            this.bitmapButtonUpgrade.ImagePressed = null;
            this.bitmapButtonUpgrade.Location = new System.Drawing.Point(419, 295);
            this.bitmapButtonUpgrade.Name = "bitmapButtonUpgrade";
            this.bitmapButtonUpgrade.Size = new System.Drawing.Size(50, 30);
            this.bitmapButtonUpgrade.StretchImage = true;
            this.bitmapButtonUpgrade.TabIndex = 29;
            this.bitmapButtonUpgrade.Tag = "1";
            this.bitmapButtonUpgrade.UseVisualStyleBackColor = true;
            this.bitmapButtonUpgrade.Click += new System.EventHandler(this.bitmapButtonUpgrade_Click);
            // 
            // SkillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonUpgrade);
            this.Controls.Add(this.bitmapButtonActive);
            this.Controls.Add(this.bitmapButtonClose);
            this.DoubleBuffered = true;
            this.Name = "SkillForm";
            this.Size = new System.Drawing.Size(513, 421);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SkillForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapButton bitmapButtonClose;
        private BitmapButton bitmapButtonActive;
        private BitmapButton bitmapButtonUpgrade;
    }
}