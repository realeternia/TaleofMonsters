using System.Windows.Forms;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.DataType.Decks;

namespace TaleofMonsters.Forms
{
    internal partial class CommandBattleForm : Form
    {
        public CommandBattleForm()
        {
            InitializeComponent();
        }

        private void textBoxTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                PlayerManager.LeftPlayer.AddCard(new ActiveCard(new DeckCard(Controler.World.WorldInfoManager.GetCardFakeId(), int.Parse(textBoxTitle.Text), 1, 0)));
                Close();
            }
        }
    }
}