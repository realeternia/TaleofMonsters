using TaleofMonsters.DataType.Cards;

namespace TaleofMonsters.Controler.Battle.Components
{
    partial class CardsArray
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
            this.cardSlot6 = new CardSlot();
            this.cardSlot5 = new CardSlot();
            this.cardSlot4 = new CardSlot();
            this.cardSlot3 = new CardSlot();
            this.cardSlot2 = new CardSlot();
            this.cardSlot1 = new CardSlot();
            this.SuspendLayout();
            // 
            // cardSlot6
            // 
            this.cardSlot6.BackColor = System.Drawing.Color.DimGray;
            this.cardSlot6.IsSelected = false;
            this.cardSlot6.Location = new System.Drawing.Point(625, 0);
            this.cardSlot6.Name = "cardSlot6";
            this.cardSlot6.Size = new System.Drawing.Size(120, 150);
            this.cardSlot6.TabIndex = 10;
            this.cardSlot6.SelectionChange += new CardSlot.CardSlotEventHandler(this.cardSlot1_SelectionChange);
            // 
            // cardSlot5
            // 
            this.cardSlot5.BackColor = System.Drawing.Color.DimGray;
            this.cardSlot5.IsSelected = false;
            this.cardSlot5.Location = new System.Drawing.Point(500, 0);
            this.cardSlot5.Name = "cardSlot5";
            this.cardSlot5.Size = new System.Drawing.Size(120, 150);
            this.cardSlot5.TabIndex = 9;
            this.cardSlot5.SelectionChange += new CardSlot.CardSlotEventHandler(this.cardSlot1_SelectionChange);
            // 
            // cardSlot4
            // 
            this.cardSlot4.BackColor = System.Drawing.Color.DimGray;
            this.cardSlot4.IsSelected = false;
            this.cardSlot4.Location = new System.Drawing.Point(375, 0);
            this.cardSlot4.Name = "cardSlot4";
            this.cardSlot4.Size = new System.Drawing.Size(120, 150);
            this.cardSlot4.TabIndex = 8;
            this.cardSlot4.SelectionChange += new CardSlot.CardSlotEventHandler(this.cardSlot1_SelectionChange);
            // 
            // cardSlot3
            // 
            this.cardSlot3.BackColor = System.Drawing.Color.DimGray;
            this.cardSlot3.IsSelected = false;
            this.cardSlot3.Location = new System.Drawing.Point(250, 0);
            this.cardSlot3.Name = "cardSlot3";
            this.cardSlot3.Size = new System.Drawing.Size(120, 150);
            this.cardSlot3.TabIndex = 7;
            this.cardSlot3.SelectionChange += new CardSlot.CardSlotEventHandler(this.cardSlot1_SelectionChange);
            // 
            // cardSlot2
            // 
            this.cardSlot2.BackColor = System.Drawing.Color.DimGray;
            this.cardSlot2.IsSelected = false;
            this.cardSlot2.Location = new System.Drawing.Point(125, 0);
            this.cardSlot2.Name = "cardSlot2";
            this.cardSlot2.Size = new System.Drawing.Size(120, 150);
            this.cardSlot2.TabIndex = 6;
            this.cardSlot2.SelectionChange += new CardSlot.CardSlotEventHandler(this.cardSlot1_SelectionChange);
            // 
            // cardSlot1
            // 
            this.cardSlot1.BackColor = System.Drawing.Color.DimGray;
            this.cardSlot1.IsSelected = false;
            this.cardSlot1.Location = new System.Drawing.Point(0, 0);
            this.cardSlot1.Name = "cardSlot1";
            this.cardSlot1.Size = new System.Drawing.Size(120, 150);
            this.cardSlot1.TabIndex = 5;
            this.cardSlot1.SelectionChange += new CardSlot.CardSlotEventHandler(this.cardSlot1_SelectionChange);
            // 
            // CardsArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cardSlot6);
            this.Controls.Add(this.cardSlot5);
            this.Controls.Add(this.cardSlot4);
            this.Controls.Add(this.cardSlot3);
            this.Controls.Add(this.cardSlot2);
            this.Controls.Add(this.cardSlot1);
            this.Name = "CardsArray";
            this.Size = new System.Drawing.Size(754, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private CardSlot cardSlot3;
        private CardSlot cardSlot2;
        private CardSlot cardSlot1;
        private CardSlot cardSlot4;
        private CardSlot cardSlot5;
        private CardSlot cardSlot6;
    }
}
