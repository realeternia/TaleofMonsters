﻿using NarlonLib.Control;

namespace TaleofMonsters.Forms
{
    sealed partial class LevelInfoForm
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(374, 3);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 23;
            this.bitmapButtonClose.Text = "关闭窗口";
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.pictureBoxCancel_Click);
            // 
            // LevelInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonClose);
            this.DoubleBuffered = true;
            this.Name = "LevelInfoForm";
            this.Size = new System.Drawing.Size(415, 283);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LevelInfoForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapButton bitmapButtonClose;




    }
}