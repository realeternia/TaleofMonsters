﻿using NarlonLib.Control;

namespace TaleofMonsters.Forms.MiniGame
{
    partial class MGIconsCatch
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MGIconsCatch));
            this.customScrollbar1 = new ControlPlus.CustomScrollbar();
            this.colorLabel1 = new NarlonLib.Control.ColorLabel();
            this.bitmapButtonC9 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC8 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC7 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC6 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC5 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC4 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC3 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC2 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonC1 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonClose = new NarlonLib.Control.BitmapButton();
            this.SuspendLayout();
            // 
            // customScrollbar1
            // 
            this.customScrollbar1.ChannelColor = System.Drawing.Color.Black;
            this.customScrollbar1.DownArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.DownArrowImage")));
            this.customScrollbar1.LargeChange = 10;
            this.customScrollbar1.Location = new System.Drawing.Point(321, 129);
            this.customScrollbar1.Maximum = 100;
            this.customScrollbar1.Minimum = 0;
            this.customScrollbar1.MinimumSize = new System.Drawing.Size(15, 86);
            this.customScrollbar1.Name = "customScrollbar1";
            this.customScrollbar1.Size = new System.Drawing.Size(15, 200);
            this.customScrollbar1.SmallChange = 1;
            this.customScrollbar1.TabIndex = 3;
            this.customScrollbar1.ThumbBottomImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbBottomImage")));
            this.customScrollbar1.ThumbBottomSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbBottomSpanImage")));
            this.customScrollbar1.ThumbMiddleImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbMiddleImage")));
            this.customScrollbar1.ThumbTopImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbTopImage")));
            this.customScrollbar1.ThumbTopSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.ThumbTopSpanImage")));
            this.customScrollbar1.UpArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar1.UpArrowImage")));
            this.customScrollbar1.Value = 0;
            this.customScrollbar1.Scroll += new System.EventHandler(this.customScrollbar1_Scroll);
            // 
            // colorLabel1
            // 
            this.colorLabel1.ForeColor = System.Drawing.Color.White;
            this.colorLabel1.Location = new System.Drawing.Point(13, 42);
            this.colorLabel1.Name = "colorLabel1";
            this.colorLabel1.Size = new System.Drawing.Size(326, 86);
            this.colorLabel1.TabIndex = 37;
            this.colorLabel1.Text = "|游戏中隐藏着由|#ff9900|某4种元素||按一定顺序拼接的串，你要做的就\r\n|是猜出|#44cc00|这个顺序。\r\n|每次猜测后，下方面板会给予一定的提示，" +
                "|#ffcc00|黄色问号表示位\r\n#ffcc00|置和符号都正确，|#66ccff|蓝色问号表示该符号存在，但位置错误。\r\n|在15回合内完全猜对四个元素及位" +
                "置|#ff0000|游戏胜利||；否则|#0033cc|失败||。";
            // 
            // bitmapButtonC9
            // 
            this.bitmapButtonC9.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC9.Image = null;
            this.bitmapButtonC9.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC9.ImageInactive = null;
            this.bitmapButtonC9.ImageMouseOver = null;
            this.bitmapButtonC9.ImageNormal = null;
            this.bitmapButtonC9.ImagePressed = null;
            this.bitmapButtonC9.Location = new System.Drawing.Point(306, 355);
            this.bitmapButtonC9.Name = "bitmapButtonC9";
            this.bitmapButtonC9.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC9.StretchImage = true;
            this.bitmapButtonC9.TabIndex = 35;
            this.bitmapButtonC9.Tag = "9";
            this.bitmapButtonC9.UseVisualStyleBackColor = true;
            this.bitmapButtonC9.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC8
            // 
            this.bitmapButtonC8.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC8.Image = null;
            this.bitmapButtonC8.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC8.ImageInactive = null;
            this.bitmapButtonC8.ImageMouseOver = null;
            this.bitmapButtonC8.ImageNormal = null;
            this.bitmapButtonC8.ImagePressed = null;
            this.bitmapButtonC8.Location = new System.Drawing.Point(269, 379);
            this.bitmapButtonC8.Name = "bitmapButtonC8";
            this.bitmapButtonC8.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC8.StretchImage = true;
            this.bitmapButtonC8.TabIndex = 34;
            this.bitmapButtonC8.Tag = "8";
            this.bitmapButtonC8.UseVisualStyleBackColor = true;
            this.bitmapButtonC8.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC7
            // 
            this.bitmapButtonC7.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC7.Image = null;
            this.bitmapButtonC7.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC7.ImageInactive = null;
            this.bitmapButtonC7.ImageMouseOver = null;
            this.bitmapButtonC7.ImageNormal = null;
            this.bitmapButtonC7.ImagePressed = null;
            this.bitmapButtonC7.Location = new System.Drawing.Point(232, 355);
            this.bitmapButtonC7.Name = "bitmapButtonC7";
            this.bitmapButtonC7.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC7.StretchImage = true;
            this.bitmapButtonC7.TabIndex = 33;
            this.bitmapButtonC7.Tag = "7";
            this.bitmapButtonC7.UseVisualStyleBackColor = true;
            this.bitmapButtonC7.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC6
            // 
            this.bitmapButtonC6.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC6.Image = null;
            this.bitmapButtonC6.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC6.ImageInactive = null;
            this.bitmapButtonC6.ImageMouseOver = null;
            this.bitmapButtonC6.ImageNormal = null;
            this.bitmapButtonC6.ImagePressed = null;
            this.bitmapButtonC6.Location = new System.Drawing.Point(195, 379);
            this.bitmapButtonC6.Name = "bitmapButtonC6";
            this.bitmapButtonC6.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC6.StretchImage = true;
            this.bitmapButtonC6.TabIndex = 32;
            this.bitmapButtonC6.Tag = "6";
            this.bitmapButtonC6.UseVisualStyleBackColor = true;
            this.bitmapButtonC6.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC5
            // 
            this.bitmapButtonC5.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC5.Image = null;
            this.bitmapButtonC5.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC5.ImageInactive = null;
            this.bitmapButtonC5.ImageMouseOver = null;
            this.bitmapButtonC5.ImageNormal = null;
            this.bitmapButtonC5.ImagePressed = null;
            this.bitmapButtonC5.Location = new System.Drawing.Point(158, 355);
            this.bitmapButtonC5.Name = "bitmapButtonC5";
            this.bitmapButtonC5.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC5.StretchImage = true;
            this.bitmapButtonC5.TabIndex = 31;
            this.bitmapButtonC5.Tag = "5";
            this.bitmapButtonC5.UseVisualStyleBackColor = true;
            this.bitmapButtonC5.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC4
            // 
            this.bitmapButtonC4.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC4.Image = null;
            this.bitmapButtonC4.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC4.ImageInactive = null;
            this.bitmapButtonC4.ImageMouseOver = null;
            this.bitmapButtonC4.ImageNormal = null;
            this.bitmapButtonC4.ImagePressed = null;
            this.bitmapButtonC4.Location = new System.Drawing.Point(121, 379);
            this.bitmapButtonC4.Name = "bitmapButtonC4";
            this.bitmapButtonC4.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC4.StretchImage = true;
            this.bitmapButtonC4.TabIndex = 30;
            this.bitmapButtonC4.Tag = "4";
            this.bitmapButtonC4.UseVisualStyleBackColor = true;
            this.bitmapButtonC4.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC3
            // 
            this.bitmapButtonC3.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC3.Image = null;
            this.bitmapButtonC3.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC3.ImageInactive = null;
            this.bitmapButtonC3.ImageMouseOver = null;
            this.bitmapButtonC3.ImageNormal = null;
            this.bitmapButtonC3.ImagePressed = null;
            this.bitmapButtonC3.Location = new System.Drawing.Point(84, 355);
            this.bitmapButtonC3.Name = "bitmapButtonC3";
            this.bitmapButtonC3.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC3.StretchImage = true;
            this.bitmapButtonC3.TabIndex = 29;
            this.bitmapButtonC3.Tag = "3";
            this.bitmapButtonC3.UseVisualStyleBackColor = true;
            this.bitmapButtonC3.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC2
            // 
            this.bitmapButtonC2.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC2.Image = null;
            this.bitmapButtonC2.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC2.ImageInactive = null;
            this.bitmapButtonC2.ImageMouseOver = null;
            this.bitmapButtonC2.ImageNormal = null;
            this.bitmapButtonC2.ImagePressed = null;
            this.bitmapButtonC2.Location = new System.Drawing.Point(47, 379);
            this.bitmapButtonC2.Name = "bitmapButtonC2";
            this.bitmapButtonC2.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC2.StretchImage = true;
            this.bitmapButtonC2.TabIndex = 28;
            this.bitmapButtonC2.Tag = "2";
            this.bitmapButtonC2.UseVisualStyleBackColor = true;
            this.bitmapButtonC2.Click += new System.EventHandler(this.bitmapButtonC1_Click);
            // 
            // bitmapButtonC1
            // 
            this.bitmapButtonC1.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonC1.Image = null;
            this.bitmapButtonC1.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonC1.ImageInactive = null;
            this.bitmapButtonC1.ImageMouseOver = null;
            this.bitmapButtonC1.ImageNormal = null;
            this.bitmapButtonC1.ImagePressed = null;
            this.bitmapButtonC1.Location = new System.Drawing.Point(10, 355);
            this.bitmapButtonC1.Name = "bitmapButtonC1";
            this.bitmapButtonC1.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonC1.StretchImage = true;
            this.bitmapButtonC1.TabIndex = 27;
            this.bitmapButtonC1.Tag = "1";
            this.bitmapButtonC1.UseVisualStyleBackColor = true;
            this.bitmapButtonC1.Click += new System.EventHandler(this.bitmapButtonC1_Click);
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(312, 11);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 41;
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.bitmapButtonClose_Click);
            // 
            // MGIconsCatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonClose);
            this.Controls.Add(this.colorLabel1);
            this.Controls.Add(this.bitmapButtonC9);
            this.Controls.Add(this.bitmapButtonC8);
            this.Controls.Add(this.bitmapButtonC7);
            this.Controls.Add(this.bitmapButtonC6);
            this.Controls.Add(this.bitmapButtonC5);
            this.Controls.Add(this.bitmapButtonC4);
            this.Controls.Add(this.bitmapButtonC3);
            this.Controls.Add(this.bitmapButtonC2);
            this.Controls.Add(this.bitmapButtonC1);
            this.Controls.Add(this.customScrollbar1);
            this.DoubleBuffered = true;
            this.Name = "MGIconsCatch";
            this.Size = new System.Drawing.Size(345, 416);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MGIconsCatch_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private ControlPlus.CustomScrollbar customScrollbar1;
        private BitmapButton bitmapButtonC1;
        private BitmapButton bitmapButtonC2;
        private BitmapButton bitmapButtonC3;
        private BitmapButton bitmapButtonC4;
        private BitmapButton bitmapButtonC5;
        private BitmapButton bitmapButtonC6;
        private BitmapButton bitmapButtonC7;
        private BitmapButton bitmapButtonC8;
        private BitmapButton bitmapButtonC9;
        private ColorLabel colorLabel1;
        private BitmapButton bitmapButtonClose;
    }
}
