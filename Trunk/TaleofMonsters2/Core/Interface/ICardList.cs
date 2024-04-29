using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.Players;

namespace TaleofMonsters.Core.Interface
{
    internal interface ICardList
    {
        void DisSelectCard();
        void UpdateSlot(ActiveCard[] pCards);
        void SetOwner(Player player);
        void UpdateCardMana();
        ActiveCard GetSelectCard();
        void SetSelectId(int value);
        int GetSelectId();
        int GetCapacity();
    }
}
