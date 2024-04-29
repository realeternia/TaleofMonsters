using TaleofMonsters.DataType.User;
using TaleofMonsters.Forms;

namespace TaleofMonsters.MainItem
{
    internal partial class PopMenuEquip : PopMenuBase
    {
        public EquipmentForm Form;
        public int EquipIndex;

        public PopMenuEquip()
        {
            InitializeComponent();
        }

        protected override void OnClick(MenuItemData target)
        {
            if (target.Type == "throw")
            {
                UserProfile.InfoBag.equipoff[EquipIndex] = 0;
            }
            else
            {
                return;
            }
            Form.MenuRefresh();
        }
    }
}

