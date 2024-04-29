using NarlonLib.Control;

namespace TaleofMonsters.Forms.MagicBook
{
    sealed partial class MonsterSkillViewForm
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
            this.bitmapButtonNext = new NarlonLib.Control.BitmapButton();
            this.bitmapButtonPre = new NarlonLib.Control.BitmapButton();
            this.labelName = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.nlClickLabel1 = new ControlPlus.NLClickLabel();
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
            this.bitmapButtonClose.Location = new System.Drawing.Point(741, 4);
            this.bitmapButtonClose.Name = "bitmapButtonClose";
            this.bitmapButtonClose.Size = new System.Drawing.Size(24, 24);
            this.bitmapButtonClose.StretchImage = true;
            this.bitmapButtonClose.TabIndex = 28;
            this.bitmapButtonClose.Text = "关闭窗口";
            this.bitmapButtonClose.UseVisualStyleBackColor = true;
            this.bitmapButtonClose.Click += new System.EventHandler(this.buttonClose_Click);
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
            this.bitmapButtonNext.Location = new System.Drawing.Point(18, 85);
            this.bitmapButtonNext.Name = "bitmapButtonNext";
            this.bitmapButtonNext.Size = new System.Drawing.Size(40, 40);
            this.bitmapButtonNext.StretchImage = true;
            this.bitmapButtonNext.TabIndex = 43;
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
            this.bitmapButtonPre.Location = new System.Drawing.Point(18, 39);
            this.bitmapButtonPre.Name = "bitmapButtonPre";
            this.bitmapButtonPre.Size = new System.Drawing.Size(40, 40);
            this.bitmapButtonPre.StretchImage = true;
            this.bitmapButtonPre.TabIndex = 42;
            this.bitmapButtonPre.Text = "上一页";
            this.bitmapButtonPre.UseVisualStyleBackColor = true;
            this.bitmapButtonPre.Click += new System.EventHandler(this.buttonPre_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this.labelName.Location = new System.Drawing.Point(73, 510);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 12);
            this.labelName.TabIndex = 44;
            // 
            // comboBoxType
            // 
            this.comboBoxType.BackColor = System.Drawing.Color.Black;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxType.Font = new System.Drawing.Font("宋体", 9F);
            this.comboBoxType.ForeColor = System.Drawing.SystemColors.Window;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "全部种类",
            "攻击",
            "防御",
            "生命",
            "克制",
            "魔法",
            "状态",
            "光环",
            "必杀",
            "地形",
            "变形",
            "成长",
            "范围",
            "特效",
            "特殊"});
            this.comboBoxType.Location = new System.Drawing.Point(128, 47);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(75, 20);
            this.comboBoxType.TabIndex = 46;
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.DarkBlue;
            this.buttonOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.buttonOk.ForeColor = System.Drawing.Color.White;
            this.buttonOk.Location = new System.Drawing.Point(476, 46);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(76, 24);
            this.buttonOk.TabIndex = 49;
            this.buttonOk.Text = "查询";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // nlClickLabel1
            // 
            this.nlClickLabel1.BackColor = System.Drawing.Color.Transparent;
            this.nlClickLabel1.Location = new System.Drawing.Point(73, 548);
            this.nlClickLabel1.Margin = new System.Windows.Forms.Padding(1);
            this.nlClickLabel1.Name = "nlClickLabel1";
            this.nlClickLabel1.Size = new System.Drawing.Size(580, 25);
            this.nlClickLabel1.TabIndex = 50;
            this.nlClickLabel1.SelectionChange += new ControlPlus.NLClickLabel.ClickEventHandler(this.nlClickLabel1_SelectionChange);
            // 
            // MonsterSkillViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.nlClickLabel1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.bitmapButtonNext);
            this.Controls.Add(this.bitmapButtonPre);
            this.Controls.Add(this.bitmapButtonClose);
            this.DoubleBuffered = true;
            this.Name = "MonsterSkillViewForm";
            this.Size = new System.Drawing.Size(783, 585);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MonsterSkillViewForm_Paint);
            this.Click += new System.EventHandler(this.MonsterSkillViewForm_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MonsterSkillViewForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BitmapButton bitmapButtonClose;
        private BitmapButton bitmapButtonNext;
        private BitmapButton bitmapButtonPre;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonOk;
        private ControlPlus.NLClickLabel nlClickLabel1;
    }
}