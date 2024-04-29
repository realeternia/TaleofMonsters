using TaleofMonsters.Controler.Loader;
using System.Drawing;
using TaleofMonsters.DataType.Decks;

namespace TaleofMonsters.DataType.Cards
{
    sealed class SpecialCard : Card
    {
        private string cardName;
        private Image cardImg;

        internal SpecialCard(string name)
        {
            cardName = name;
            cardImg = PicLoader.Read("System", cardName);
        }

        internal override int CardId
        {
            get { return 0; }
        }

        internal override int Star
        {
            get { return 0; }
        }

        internal override int Type
        {
            get { return 99; }
        }

        internal override int Cost
        {
            get { return 0; }
        }

        internal override string Name
        {
            get { return ""; }
        }

        internal override byte Quality
        {
            get { return 0; }
        }

        internal override string ENameShort
        {
            get { return ""; }
        }

        internal override int Res
        {
            get { return 0; }
        }

        internal override Image GetCardImage(int width, int height)
        {
            return cardImg;
        }

        internal override void DrawOnDeck(Graphics g, int offX, int offY, bool isShowPicture)
        {
        }

        internal override CardTypes GetCardType()
        {
            return CardTypes.Null;
        }

        internal override void UpdateToLevel(DeckCard dc)
        {            
        }

        internal override void DrawOnStateBar(Graphics g)
        {
        }

        internal override Image GetPreview(CardPreviewType type, int[] parms)
        {
            return null;
        }
    }

    static class SpecialCards 
	{
        static readonly SpecialCard nullCard = new SpecialCard("CardBack.JPG");

        internal static SpecialCard NullCard
        {
            get { return nullCard; }
        }
    }
}
