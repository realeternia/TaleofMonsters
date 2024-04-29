using System;
using System.Collections.Generic;
using ConfigDatas;
using NarlonLib.Core;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.DataType.HeroSkills;
using TaleofMonsters.DataType.Skills;
using TaleofMonsters.Core;
using System.Drawing;
using TaleofMonsters.Controler.Battle.Tool;

namespace TaleofMonsters.Controler.Battle.Data.Players
{
    internal class Player : IPlayer
    {
        public delegate void PlayerPointEventHandler();
        public event PlayerPointEventHandler AngerChanged;
        public event PlayerPointEventHandler ManaChanged;
        public event PlayerPointEventHandler ExtraChanged;

        protected int peopleId;
        protected int level;        
        protected int anger;
        protected int maxAnger;
        protected int mana;
        protected int maxMana;
        protected int angerAdd;
        protected int manaAdd;
        protected PlayerIndex pPos;
        protected ActiveCard[] cards = new ActiveCard[6];
        protected MonsterSkill[] skills = new MonsterSkill[5];
        protected ICardList cardsDesk;
        protected ActiveCards activeCards;
        protected Monster heroData;
        protected LiveMonster hero;
        protected Image heroImage;
        protected PlayerState playerState;

        protected int[] extraInfo; //魂/时/命
		protected int angerRecover;
        protected int manaRecover;

        private int directDamage; // 主要是来自物品的伤害

        #region 属性

        public int Mana
        {
            get { return mana; }
            set
            {
                mana = value;
                if (mana < 0) mana = 0;
                else if (mana > maxMana) mana = maxMana;
                if (ManaChanged != null)
                {
                    ManaChanged();
                }
            }
        }

        public int Anger
        {
            get { return anger; }
            set
            {
                anger = value;
                if (anger < 0) anger = 0;
                else if (anger > maxAnger) anger = maxAnger;
                if (AngerChanged != null)
                {
                    AngerChanged();
                }
            }
        }

        public int Level
        {
            get { return level; }
        }

        public int MaxAnger
        {
            get { return maxAnger; }
        }

        public int MaxMana
        {
            get { return maxMana; }
        }

        public ICardList CardsDesk
        {
            get { return cardsDesk; }
            set { cardsDesk = value; }
        }

        public PlayerIndex PPos
        {
            get { return pPos; }
            set { pPos = value; }
        }

        public int SelectCardId
        {
            get { return cardsDesk.GetSelectCard().CardId; }
        }

        public int SelectId
        {
            get { return cardsDesk.GetSelectId(); }
        }

        public Monster HeroData
        {
            get { return heroData; }
        }

        public LiveMonster Hero
        {
            get { return hero; }
            set { hero = value; }
        }

        public Image HeroImage
        {
            get { return heroImage; }
        }

        public PlayerState State
        {
            get { return playerState; }
        }

        public int PeopleId
        {
            get { return peopleId; }
            set { peopleId = value; }
        }

        public ActiveCards Cards
        {
            get { return activeCards; }
        }

        public int DirectDamage
        {
            get { return directDamage; }
            set { directDamage = value; }
        }

        #endregion

        protected void InitBase()
        {
            maxAnger = SysConstants.MaxAp;
            maxMana = SysConstants.MaxMp;
            angerAdd = 0;
            manaAdd = 0;
            angerRecover = 0;
            manaRecover = 0;
           

            anger = 1;
            mana = 0;

            extraInfo = new int[3];
        }

        public void AddMana(double addon)
        {
            Mana += (int)addon;
        }

        public void AddAnger(double addon)
        {
            Anger += (int)addon;
        }

        public virtual void AddResource(GameResourceType type, int number)
        {
        }

        public void AddResource(int type, int number)
        {
            AddResource((GameResourceType)type, number);
        }

        protected void CalculateEquipAndSkill(int[] equipids, int[] cskills)
        {
            playerState.Init();

            AutoDictionary<int, int> addons = EquipBook.GetEquipsAddons(equipids);
            for (int i = 0; i < 8; i++)
            {
                playerState.AddAttrs((PlayerAttrs) i, addons[i + 1]);
            }
            playerState.UpdateSkills(addons.Keys(), addons.Values());

            addons = HeroSkillBook.GetSkillCommonAddons(cskills);
            for (int i = 0; i < 8; i++)
            {
                playerState.AddAttrs((PlayerAttrs)i, addons[i + 1]);
            }
            playerState.UpdateSkills(addons.Keys(), addons.Values());
        }

        public void GetNextCard()
        {
            if (GetCardNumber() < 6)
            {
                ActiveCard next = activeCards.GetNextCard();
                if (next!=ActiveCards.NoneCard)
                {
                    AddCard(next);
                }               
            }
        }

        public void DeleteRandomCardFor(IPlayer p)
        {
            ActiveCard card = ActiveCards.NoneCard;
            if (GetCardNumber() > 0)
            {
                int id = MathTool.GetRandom(GetCardNumber());
                card = cards[id];
                DeleteCardAt(id + 1);
            }
            Player player = p as Player;
            if (player != null)
            {
                player.AddCard(card);
            }
        }

        public void SetCard(int id, ActiveCard card)
        {
            int count = GetCardNumber();
            if (id < count)
            {
                cards[id] = card;
            }
            if (cardsDesk != null)
                cardsDesk.UpdateSlot(cards);
        }

        public void AddCard(ActiveCard card)
        {
            int count = GetCardNumber();
            card.UpdateCardMana(hero);
            if (count < 6)
            {
                cards[count] = card;
            }
            if (cardsDesk != null)
                cardsDesk.UpdateSlot(cards);
        }

        public virtual void AddBottleExp(int addon)
        {
        }

        public void GetNextNCard(int n)
        {
            for (int i = 0; i < n; i++)
                GetNextCard();
        }

        public void CopyRandomNCard(int n, int spellid)
        {
            List<int> indexs = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                if (cards[i].CardId != 0 && (cards[i].CardId != spellid || cards[i].CardType != CardTypes.Spell))
                    indexs.Add(i);
            }
            indexs = RandomShuffle.Process(indexs.ToArray());
            for (int i = 0; i < Math.Min(n, indexs.Count); i++)
            {
                AddCard(cards[indexs[i]].GetCopy());
            }
        }

        public void DeleteCardAt(int index)
        {
            cards[index - 1] = ActiveCards.NoneCard;
            for (int i = 0; i <= 4; i++)
            {
                if (cards[i].Id == 0 && cards[i + 1].Id > 0)
                {
                    ActiveCard tempCard = cards[i];
                    cards[i] = cards[i + 1];
                    cards[i + 1] = tempCard;
                }
            }
            if (cardsDesk != null)
            {
                cardsDesk.DisSelectCard();
                cardsDesk.UpdateSlot(cards);
            }
        }

        public void DeleteAllCard()
        {
            for (int i = 0; i <= 5; i++)
            {
                cards[i] = ActiveCards.NoneCard;
            }
            if (cardsDesk != null)
            {
                cardsDesk.UpdateSlot(cards);
            }
        }

        public int GetCardNumber()
        {
            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (cards[i].Id != 0)
                    count++;
            }
            return count;
        }

        public ActiveCard GetDeckCardAt(int index)
        {
            if (index > 6 || index <= 0)
                return ActiveCards.NoneCard;

            return cards[index - 1];
        }

        public int CardUsePreCheck(ActiveCard selectCard)
        {
            if (HasBuff(BuffEffectTypes.NoUseCard))
            {
                return HSErrorTypes.BattleNoUseCard;
            }
            if (selectCard.CardType == CardTypes.Spell && HasBuff(BuffEffectTypes.NoUseCardSpell))
            {
                return HSErrorTypes.BattleNoUseSpellCard;
            }

            if (mana < selectCard.GetRequiredMana())
            {
                return HSErrorTypes.BattleLackMana;
            }

            if (anger < selectCard.GetRequiredAnger())
            {
                return HSErrorTypes.BattleLackAnger;
            }

            return HSErrorTypes.OK;
        }

        public void OnUseCard(ActiveCard selectCard)
        {
            Mana -= selectCard.GetRequiredMana();
            Anger -= selectCard.GetRequiredAnger();
        }

		public void RoundRecover(bool isFast)
		{
		    angerAdd += (isFast ? 3 : 1)*maxAnger + angerRecover; //10回合完全回复
			if(angerAdd >= 100)
			{
				Anger += angerAdd / 100;
				angerAdd = angerAdd % 100;
			}

            manaAdd += (isFast ? 3 : 1) * maxMana + manaRecover;
			if(manaAdd >= 100)
			{
				Mana += manaAdd / 100;
				manaAdd = manaAdd % 100;
			}
            AddExtraInfo(PlayerExtraInfo.Time, 1); //10回合完全回复
		}

		public virtual void InitialCards()
		{
		    for (int i = 0; i < 4; i++)
		    {
                GetNextCard();
		    }

		    DeckCard heroDeckCard = new DeckCard(World.WorldInfoManager.GetCardFakeId(), 99, level, 0);
            if (heroData != null)
		    {
                LiveMonster lm = new LiveMonster(heroDeckCard, heroData, BattleLocationManager.GetHeroPoint(pPos, heroDeckCard.Id), pPos);
                MonsterQueue.Instance.Add(lm);
                BattleLocationManager.UpdateCellOwner(lm.Position.X, lm.Position.Y, lm.Id);
		        hero = lm;
		    }
		}

        public void AddExtraInfo(PlayerExtraInfo index, int addon)
        {
            int id = (int) index;
            extraInfo[id] = Math.Min(extraInfo[id] + addon, 99);
            if (ExtraChanged != null)
                ExtraChanged();
        }

        public int GetExtraInfo(PlayerExtraInfo index)
        {
            return extraInfo[(int) index];
        }

        public void ConvertCard(int count, int cardId)
        {
            int num = GetCardNumber();
            int id = MathTool.GetRandom(num);
            for (int i = 0; i < count; i++)
            {
                if (num > i)
                    SetCard((id + i) % num, new ActiveCard(World.WorldInfoManager.GetCardFakeId(), cardId, 1, 0));
            }
        }

        public void ThrowCard(int type)
        {
            for (int i = GetCardNumber(); i > 0; i--)
            {
                CardTypes ctype = GetDeckCardAt(i).CardType;
                if ((int)ctype == type)
                {
                    DeleteCardAt(i);
                }
            }
        }

        public int Round
        {
            get { return BattleInfo.Instance.Round; }
        }

        public void AddBuff(int buffId, int blevel, int time)
        {
            if (hero == null || !hero.IsAlive)
            {
                return;
            }
            hero.AddBuff(buffId, blevel, time);
        }

        public bool HasBuff(BuffEffectTypes type)
        {
            if (hero == null || !hero.IsAlive)
            {
                return false;
            }
            return hero.HasBuff(type);
        }

        public void DelBuff(BuffEffectTypes type)
        {
            if (hero == null || !hero.IsAlive)
            {
                return;
            }
            foreach (MemBaseBuff buff in hero.Buffs.Values)
            {
                if (BuffBook.HasEffect(buff.Id,type))
                {
                    buff.TimeLeft = 0;
                }
            }
        }

        public void UpdateCardMana()
        {
            cardsDesk.UpdateCardMana();
        }

        public bool IsLeft
        {
            get { return pPos == PlayerIndex.Player1; }
        }

        public void DrawToolTips(Graphics g)
        {
            int x = 0, y = 0;
            int wid = 165, heg = 83;
            List<string> datas = new List<string>();
            Font fontsong = new Font("宋体", 9, FontStyle.Regular);
            Font fontsong2 = new Font("宋体", 10, FontStyle.Bold);
            if (pPos == PlayerIndex.Player2) //右边那人
                x = 899 - wid;

            g.FillRectangle(Brushes.Black, x, y, wid, heg);
            g.DrawRectangle(Pens.White, x, y, wid, heg);
            g.DrawString(string.Format("{0} Lv{1}", ConfigData.GetJobConfig(playerState.job).Name, level), fontsong2, Brushes.Gold, x + 3, y + 3);
            g.DrawString(string.Format("战斗 {0,3:D}", playerState.GetAttr(PlayerAttrs.Atk, false)), fontsong, Brushes.White, x + 3, y + 19);
            if (playerState.GetAttr(PlayerAttrs.Atk, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Atk, true)), fontsong, Brushes.Lime, x + 53, y + 19);
            g.DrawString(string.Format("守护 {0,3:D}", playerState.GetAttr(PlayerAttrs.Def, false)), fontsong, Brushes.White, x + 83, y + 19);
            if (playerState.GetAttr(PlayerAttrs.Def, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Def, true)), fontsong, Brushes.Lime, x + 133, y + 19);
            g.DrawString(string.Format("法术 {0,3:D}", playerState.GetAttr(PlayerAttrs.Mag, false)), fontsong, Brushes.White, x + 3, y + 35);
            if (playerState.GetAttr(PlayerAttrs.Mag, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Mag, true)), fontsong, Brushes.Lime, x + 53, y + 35);
            g.DrawString(string.Format("技巧 {0,3:D}", playerState.GetAttr(PlayerAttrs.Skl, false)), fontsong, Brushes.White, x + 83, y + 35);
            if (playerState.GetAttr(PlayerAttrs.Skl, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Skl, true)), fontsong, Brushes.Lime, x + 133, y + 35);
            g.DrawString(string.Format("速度 {0,3:D}", playerState.GetAttr(PlayerAttrs.Spd, false)), fontsong, Brushes.White, x + 3, y + 51);
            if (playerState.GetAttr(PlayerAttrs.Spd, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Spd, true)), fontsong, Brushes.Lime, x + 53, y + 51);
            g.DrawString(string.Format("幸运 {0,3:D}", playerState.GetAttr(PlayerAttrs.Luk, false)), fontsong, Brushes.White, x + 83, y + 51);
            if (playerState.GetAttr(PlayerAttrs.Luk, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Luk, true)), fontsong, Brushes.Lime, x + 133, y + 51);
            g.DrawString(string.Format("体质 {0,3:D}", playerState.GetAttr(PlayerAttrs.Vit, false)), fontsong, Brushes.White, x + 3, y + 67);
            if (playerState.GetAttr(PlayerAttrs.Vit, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Vit, true)), fontsong, Brushes.Lime, x + 53, y + 67);
            g.DrawString(string.Format("生存 {0,3:D}", playerState.GetAttr(PlayerAttrs.Adp, false)), fontsong, Brushes.White, x + 83, y + 67);
            if (playerState.GetAttr(PlayerAttrs.Adp, true) > 0) g.DrawString(string.Format("+{0,3:D}", playerState.GetAttr(PlayerAttrs.Adp, true)), fontsong, Brushes.Lime, x + 133, y + 67);
            fontsong.Dispose();
            fontsong2.Dispose();
            datas.Clear();
        }
    }
}
