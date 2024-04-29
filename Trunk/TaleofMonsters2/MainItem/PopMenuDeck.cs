using System.Windows.Forms;
using ConfigDatas;
using ControlPlus;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms;

namespace TaleofMonsters.MainItem
{
    internal partial class PopMenuDeck : PopMenuBase
    {
        public DeckViewForm Form;
        public DeckCard TargetCard;
        public int Floor;

        public PopMenuDeck()
        {
            InitializeComponent();
        }

        protected override void OnClick(MenuItemData target)
        {
            bool needUpdate = false;
            if (target.Type == "activate")
            {
                int result = UserProfile.InfoCard.SelectedDeck.AddCardAuto(TargetCard);
                if (result != HSErrorTypes.OK)
                {
                    Form.AddFlowCenter(HSErrorTypes.GetDescript(result), "Red");
                }
            }
            else if (target.Type == "remove")
            {
                UserProfile.InfoCard.SelectedDeck.RemoveCardById(TargetCard.Id);
                if (Floor == 1)
                {
                   Form.ClearCard();
                }
            }
            else if (target.Type == "delete")
            {
                if (MessageBoxEx2.Show("确定要丢弃这张卡片？") == DialogResult.OK)
                {
                    UserProfile.InfoCard.RemoveCard(TargetCard.Id);
                    Form.ClearCard();
                }
            }
            else if (target.Type.StartsWith("expbean"))
            {
                int itemId = int.Parse(target.Type.Substring(7));
                HItemConfig itemConfig = ConfigData.GetHItemConfig(itemId);
                if (UserProfile.Profile.InfoBag.GetItemCount(itemId) > 0)
                {
                    UserProfile.Profile.InfoBag.DeleteItem(itemId, 1);
                    UserProfile.InfoCard.AddCardExp(TargetCard.Id, itemConfig.UseEffect[0]);
                    needUpdate = true;
                }
            }
            else
            {
                return;
            }
            Form.MenuRefresh(needUpdate);
        }
    }
}

