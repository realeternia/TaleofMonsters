﻿using NarlonLib.Control;

namespace TaleofMonsters.Forms.Items
{
    partial class MiniItemView
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
            this.bitmapButtonRight = new BitmapButton();
            this.bitmapButtonLeft = new BitmapButton();
            this.SuspendLayout();
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
            this.bitmapButtonRight.Location = new System.Drawing.Point(36, 112);
            this.bitmapButtonRight.Name = "bitmapButtonRight";
            this.bitmapButtonRight.Size = new System.Drawing.Size(35, 20);
            this.bitmapButtonRight.StretchImage = true;
            this.bitmapButtonRight.TabIndex = 26;
            this.bitmapButtonRight.Text = "下一页";
            this.bitmapButtonRight.UseVisualStyleBackColor = true;
            this.bitmapButtonRight.Click += new System.EventHandler(this.bitmapButtonRight_Click);
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
            this.bitmapButtonLeft.Location = new System.Drawing.Point(0, 112);
            this.bitmapButtonLeft.Name = "bitmapButtonLeft";
            this.bitmapButtonLeft.Size = new System.Drawing.Size(35, 20);
            this.bitmapButtonLeft.StretchImage = true;
            this.bitmapButtonLeft.TabIndex = 25;
            this.bitmapButtonLeft.Text = "上一页";
            this.bitmapButtonLeft.UseVisualStyleBackColor = true;
            this.bitmapButtonLeft.Click += new System.EventHandler(this.bitmapButtonLeft_Click);
            // 
            // MiniItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonRight);
            this.Controls.Add(this.bitmapButtonLeft);
            this.DoubleBuffered = true;
            this.Name = "MiniItemView";
            this.Size = new System.Drawing.Size(72, 142);
            this.DoubleClick += new System.EventHandler(this.ItemView_DoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ItemView_MouseMove);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ItemView_Paint);
            this.MouseLeave += new System.EventHandler(this.ItemView_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapButton bitmapButtonLeft;
        private BitmapButton bitmapButtonRight;


    }
}
