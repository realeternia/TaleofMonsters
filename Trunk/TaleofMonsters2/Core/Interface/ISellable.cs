using TaleofMonsters.DataType.Shops;

namespace TaleofMonsters.Core.Interface
{
    interface ISellable
    {
        int GetSellRate();
        CardProductMarkTypes GetSellMark();
    }
}
