using NarlonLib.Control;

namespace TaleofMonsters.Forms
{
    sealed partial class WorldMapViewForm
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
            this.bitmapButtonRight = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonDown = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonUp = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonLeft = new NarlonLib.Control.BitmapButton();
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(741, 5);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 27;
            this.bitmapButtonClose.Text = "关闭窗口";
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // bitmapButtonRight
            // 
            this.bitmapButtonRight.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonRight.Image = null;
            this.bitmapButtonRight.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonRight.ImageInactive = null;
            this.bitmapButtonRight.ImageMouseOver = null;
            this.bitmapButtonRight.ImageNormal = null;
            this.bitmapButtonRight.ImagePressed = null;
            this.bitmapButtonRight.Location = new System.Drawing.Point(736, 250);
            this.bitmapButtonRight.Name = "bitmapButtonRight";
            this.bitmapButtonRight.Size = new System.Drawing.Size(20, 40);
            this.bitmapButtonRight.StretchImage = true;
            this.bitmapButtonRight.TabIndex = 38;
            this.bitmapButtonRight.Text = "向东移动";
            this.bitmapButtonRight.UseVisualStyleBackColor = true;
            this.bitmapButtonRight.Click += new System.EventHandler(this.bitmapButtonRight_Click);
            // 
            // bitmapButtonDown
            // 
            this.bitmapButtonDown.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonDown.Image = null;
            this.bitmapButtonDown.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonDown.ImageInactive = null;
            this.bitmapButtonDown.ImageMouseOver = null;
            this.bitmapButtonDown.ImageNormal = null;
            this.bitmapButtonDown.ImagePressed = null;
            this.bitmapButtonDown.Location = new System.Drawing.Point(370, 505);
            this.bitmapButtonDown.Name = "bitmapButtonDown";
            this.bitmapButtonDown.Size = new System.Drawing.Size(40, 20);
            this.bitmapButtonDown.StretchImage = true;
            this.bitmapButtonDown.TabIndex = 37;
            this.bitmapButtonDown.Text = "向南移动";
            this.bitmapButtonDown.UseVisualStyleBackColor = true;
            this.bitmapButtonDown.Click += new System.EventHandler(this.bitmapButtonDown_Click);
            // 
            // bitmapButtonUp
            // 
            this.bitmapButtonUp.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonUp.Image = null;
            this.bitmapButtonUp.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonUp.ImageInactive = null;
            this.bitmapButtonUp.ImageMouseOver = null;
            this.bitmapButtonUp.ImageNormal = null;
            this.bitmapButtonUp.ImagePressed = null;
            this.bitmapButtonUp.Location = new System.Drawing.Point(370, 45);
            this.bitmapButtonUp.Name = "bitmapButtonUp";
            this.bitmapButtonUp.Size = new System.Drawing.Size(40, 20);
            this.bitmapButtonUp.StretchImage = true;
            this.bitmapButtonUp.TabIndex = 36;
            this.bitmapButtonUp.Text = "向北移动";
            this.bitmapButtonUp.UseVisualStyleBackColor = true;
            this.bitmapButtonUp.Click += new System.EventHandler(this.bitmapButtonUp_Click);
            // 
            // bitmapButtonLeft
            // 
            this.bitmapButtonLeft.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonLeft.Image = null;
            this.bitmapButtonLeft.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonLeft.ImageInactive = null;
            this.bitmapButtonLeft.ImageMouseOver = null;
            this.bitmapButtonLeft.ImageNormal = null;
            this.bitmapButtonLeft.ImagePressed = null;
            this.bitmapButtonLeft.Location = new System.Drawing.Point(19, 250);
            this.bitmapButtonLeft.Name = "bitmapButtonLeft";
            this.bitmapButtonLeft.Size = new System.Drawing.Size(20, 40);
            this.bitmapButtonLeft.StretchImage = true;
            this.bitmapButtonLeft.TabIndex = 35;
            this.bitmapButtonLeft.Text = "向西移动";
            this.bitmapButtonLeft.UseVisualStyleBackColor = true;
            this.bitmapButtonLeft.Click += new System.EventHandler(this.bitmapButtonLeft_Click);
            // 
            // WorldMapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonRight);
            this.Controls.Add(this.bitmapButtonDown);
            this.Controls.Add(this.bitmapButtonUp);
            this.Controls.Add(this.bitmapButtonLeft);
            this.Controls.Add(this.bitmapButtonClose);
            this.DoubleBuffered = true;
            this.Name = "WorldMapViewForm";
            this.Size = new System.Drawing.Size(783, 545);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WorldMapViewForm_Paint);
            this.Click += new System.EventHandler(this.WorldMapViewForm_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WorldMapViewForm_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapButton bitmapButtonClose;
        private BitmapButton bitmapButtonRight;
        private BitmapButton bitmapButtonDown;
        private BitmapButton bitmapButtonUp;
        private BitmapButton bitmapButtonLeft;
    }
}