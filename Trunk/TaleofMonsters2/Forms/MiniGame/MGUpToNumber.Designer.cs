﻿using NarlonLib.Control;

namespace TaleofMonsters.Forms.MiniGame
{
    partial class MGUpToNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MGUpToNumber));
            this.colorLabel1 = new NarlonLib.Control.ColorLabel();
            this.bitmapButtonC1 = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonClose = new NarlonLib.Control.BitmapButton();
            this.SuspendLayout();
            // 
            // colorLabel1
            // 
            this.colorLabel1.ForeColor = System.Drawing.Color.White;
            this.colorLabel1.Location = new System.Drawing.Point(13, 42);
            this.colorLabel1.Name = "colorLabel1";
            this.colorLabel1.Size = new System.Drawing.Size(326, 86);
            this.colorLabel1.TabIndex = 37;
            this.colorLabel1.Text = resources.GetString("colorLabel1.Text");
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
            this.bitmapButtonC1.Location = new System.Drawing.Point(148, 376);
            this.bitmapButtonC1.Name = "bitmapButtonC1";
            this.bitmapButtonC1.Size = new System.Drawing.Size(50, 30);
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(306, 15);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 39;
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.bitmapButtonClose_Click);
            // 
            // MGUpToNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonClose);
            this.Controls.Add(this.colorLabel1);
            this.Controls.Add(this.bitmapButtonC1);
            this.DoubleBuffered = true;
            this.Name = "MGUpToNumber";
            this.Size = new System.Drawing.Size(345, 416);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MGUpToNumber_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapButton bitmapButtonC1;
        private ColorLabel colorLabel1;
        private BitmapButton bitmapButtonClose;
    }
}
