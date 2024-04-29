using System.Drawing;
using System.Windows.Forms;

namespace TaleofMonsters.Forms
{
    internal sealed partial class BlackWallForm : BasePanel
    {
        private static BlackWallForm instance;
        public static BlackWallForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BlackWallForm();
                    instance.Init();
                }
                return instance;
            }
        }

        public BlackWallForm()
        {
            InitializeComponent();
            Width = 1000;
            Height = 716;
        }

        private void CardShopViewForm_Paint(object sender, PaintEventArgs e)
        {
            Color col = Color.FromArgb(180, Color.Black);
            SolidBrush brush = new SolidBrush(col);
            e.Graphics.FillRectangle(brush, 0, 0, Width, Height);
            brush.Dispose();
        }
    }
}