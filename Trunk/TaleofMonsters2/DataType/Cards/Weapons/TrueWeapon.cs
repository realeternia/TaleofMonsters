using System.Drawing;
using ConfigDatas;
using TaleofMonsters.DataType.Decks;

namespace TaleofMonsters.DataType.Cards.Weapons
{
    internal class TrueWeapon
    {
        private Weapon avatar;
        private DeckCard card;

        public Weapon Avatar
        {
            get { return avatar; }
        }

        public int CardId
        {
            get { return avatar.Id; }
        }

        public int Level
        {
            get { return card.Level; }
        }

        public DeckCard Card
        {
            get { return card; }
        }

        public TrueWeapon()
        {
            avatar = new Weapon(0);
        }

        public TrueWeapon(DeckCard card, Weapon wpn)
        {
            avatar = wpn;
            this.card = card;
        }        

        public void CheckWeaponRate(WeaponAttrRate rate)
        {
            if (rate!=null && rate.Type == avatar.WeaponConfig.Type)
            {
                avatar.Atk = (int) (avatar.Atk*rate.Atk);
                avatar.Def = (int)(avatar.Def * rate.Def);
                avatar.Hit = (int)(avatar.Hit * rate.Hit);
                avatar.Dhit = (int)(avatar.Dhit * rate.Dhit);
              //  avatar.Magic = (int)(avatar.Magic * rate.Magic);
            }
        }

        public Image GetImage(int width, int height)
        {
            return WeaponBook.GetWeaponImage(avatar.Id, width, height);  
        }
    }
}