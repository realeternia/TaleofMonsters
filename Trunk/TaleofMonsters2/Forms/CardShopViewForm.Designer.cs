using NarlonLib.Control;

namespace TaleofMonsters.Forms
{
    sealed partial class CardShopViewForm
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
            this.bitmapButtonRefresh = new NarlonLib.Control.BitmapButton();
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(493, 5);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 27;
            this.bitmapButtonClose.Text = "关闭窗口";
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // bitmapButtonRefresh
            // 
            this.bitmapButtonRefresh.BorderColor = System.Drawing.Color.DarkBlue;
            this.bitmapButtonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bitmapButtonRefresh.Image = null;
            this.bitmapButtonRefresh.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.bitmapButtonRefresh.ImageInactive = null;
            this.bitmapButtonRefresh.ImageMouseOver = null;
            this.bitmapButtonRefresh.ImageNormal = null;
            this.bitmapButtonRefresh.ImagePressed = null;
            this.bitmapButtonRefresh.Location = new System.Drawing.Point(141, 441);
            this.bitmapButtonRefresh.Name = "bitmapButtonRefresh";
            this.bitmapButtonRefresh.Size = new System.Drawing.Size(28, 28);
            this.bitmapButtonRefresh.StretchImage = true;
            this.bitmapButtonRefresh.TabIndex = 51;
            this.bitmapButtonRefresh.Text = "立刻刷新";
            this.bitmapButtonRefresh.UseVisualStyleBackColor = true;
            this.bitmapButtonRefresh.Click += new System.EventHandler(this.bitmapButtonRefresh_Click);
            // 
            // CardShopViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.bitmapButtonRefresh);
            this.Controls.Add(this.bitmapButtonClose);
            this.DoubleBuffered = true;
            this.Name = "CardShopViewForm";
            this.Size = new System.Drawing.Size(533, 475);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CardShopViewForm_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapButton bitmapButtonClose;
        private BitmapButton bitmapButtonRefresh;
    }
}