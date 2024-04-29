using System;
using System.Drawing;

namespace TaleofMonsters.DataType.Cards
{
    internal abstract class Card
    {
        internal abstract int CardId { get; }
        internal abstract int Star { get; }
        internal abstract int Cost { get; }
        internal abstract int Type { get; }
        internal abstract string Name { get; }
        internal abstract string ENameShort { get; }
        internal abstract int Res { get; }
        internal abstract byte Quality { get; }

        internal abstract Image GetCardImage(int width ,int height);
        internal abstract void DrawOnDeck(Graphics g, int offX, int offY, bool isShowPicture);
        internal abstract void DrawOnStateBar(Graphics g);
        internal abstract Image GetPreview(CardPreviewType type, int[] parms);
        internal abstract CardTypes GetCardType();
        internal abstract void UpdateToLevel(Decks.DeckCard dc);
    }

    internal enum CardPreviewType
    {
        Normal, Shop
    }
}
