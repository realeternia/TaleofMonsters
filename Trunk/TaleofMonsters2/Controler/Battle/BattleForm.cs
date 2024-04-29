using System;
using System.Drawing;
using System.Windows.Forms;
using ConfigDatas;
using NarlonLib.Core;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.MemFlow;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Buffs;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Spells;
using TaleofMonsters.DataType.User;
using TaleofMonsters.Controler.Battle.Data.MemCard;
using TaleofMonsters.Forms.Items.Core;
using TaleofMonsters.Forms;

namespace TaleofMonsters.Controler.Battle
{
    internal sealed partial class BattleForm : BasePanel
    {
        #region 委托
        delegate void CloseFormCallback();
        private void CloseForm()
        {
            if (InvokeRequired)
            {
                CloseFormCallback d = CloseForm;
                Invoke(d, new object[] { });
            }
            else
            {
                ComClose();
            }
        }
        #endregion

        internal HsActionCallback BattleWin;
        internal HsActionCallback BattleLose;

        private bool showGround;
        private bool isGamePaused;
        private bool gameEnd;
        private bool isMouseIn;
        private int roundMark;
        private int mouseX, mouseY;
        private int showPlayerState;
        private bool onTurn;
        private bool autoFight;

        private HSCursor myCursor;
        private ActiveCard leftSelectCard;
        private bool isHuman;

        private int leftId;
        private int rightId;
        private int rightLevel;
        private string mapName;
        private int defaultTile;

        private long lastMouseMoveTime;
        private int hitRound;
        private int hitMonsterId;

        private int itemCount;

        public bool IsWin
        {
            get { return BattleInfo.Instance.PlayerWin; }
        }

        public bool IsGamePaused
        {
            get { return isGamePaused; }
            set { isGamePaused = value; CheckEnable(); }
        }

        private int rule;

        internal BattleForm()
        {
            InitializeComponent();
            NeedBlackForm = true;
            this.bitmapButtonClose.ImageNormal = PicLoader.Read("System", "CloseButton1.JPG");
            this.bitmapButtonClose.ImageMouseOver = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonClose.ImagePressed = PicLoader.Read("System", "CloseButton2.JPG");
            this.bitmapButtonAuto.ImageNormal = PicLoader.Read("Button", "AutoFightButton.JPG");
            this.bitmapButtonAuto.ImageMouseOver = PicLoader.Read("Button", "AutoFightButtonOn.JPG");
            this.bitmapButtonAuto.ImagePressed = PicLoader.Read("Button", "AutoFightButton.JPG");
            this.bitmapButtonThrow.ImageNormal = PicLoader.Read("Button", "DropButton.JPG");
            this.bitmapButtonThrow.ImageMouseOver = PicLoader.Read("Button", "DropButtonOn.JPG");
            this.bitmapButtonThrow.ImagePressed = PicLoader.Read("Button", "DropButtonOn.JPG");
          //  this.FormBorderStyle = FormBorderStyle.None; 
#if DEBUG
            cardList2.Visible = true;
#endif
            myCursor = new HSCursor(this);
            lifeClock1.IsLeft = true;
            lifeClock2.IsLeft = false;
        }

        internal void Init(int lid, int rid, string map, int tile, int rlevel)
        {
            isHuman = lid == 0;
            rightId = rid;
            leftId = lid;
            rightLevel = rlevel;
            rule = ConfigData.GetPeopleConfig(rightId).Rule;
            if (leftId > 0)
            {
                lifeClock1.SetPlayer(leftId);
                autoFight = true;
                bitmapButtonAuto.Visible = false;
                miniItemView1.Visible = false;
            }
            else
            {
                lifeClock1.SetPlayer(UserProfile.ProfileName, UserProfile.InfoBasic.face);
            }
            lifeClock2.SetPlayer(rightId);
            mapName = map;
            defaultTile = tile;
            timeViewer1.Init();
            cardsArray1.Init();
            cardList2.Init();
            miniItemView1.Init();
        }

        internal void InitGame()//初始化游戏
        {
            IsGamePaused = false;
            showGround = true;
            MonsterQueue.Init();
            EffectQueue.Init();
            FlowWordQueue.Init();
            MagicRegion.Init();
            PlayerManager.Init(leftId, rightId, rightLevel);
            BattleInfo.Init(mapName, defaultTile);

            PlayerManager.LeftPlayer.CardsDesk = cardsArray1;
            PlayerManager.LeftPlayer.InitialCards();
            PlayerManager.RightPlayer.CardsDesk = cardList2;
            PlayerManager.RightPlayer.InitialCards();
            MonsterQueue.Instance.AddInitialAction();
            lifeClock1.Player = PlayerManager.LeftPlayer;
            lifeClock2.Player = PlayerManager.RightPlayer;
            roundMark = 0;
            BattleInfo.Instance.StartTime = DateTime.Now;
            BattleInfo.Instance.EndTime = DateTime.Now;
        }

        internal void StopGame()
        {
            isGamePaused = true;
        }

        internal override void OnFrame(int tick)
        {
            base.OnFrame(tick);

            if (onTurn)
                return;
            onTurn = true;
            NLCoroutine.DoTimer();
            try
            {
                panelBattle.Invalidate();
                if (BattleInfo.Instance.Items != null && itemCount != BattleInfo.Instance.Items.Count)//获得新物品
                {
                    itemCount = BattleInfo.Instance.Items.Count;
                }

                if (PlayerManager.LeftPlayer == null || PlayerManager.RightPlayer == null)
                    return;
                CheckCursor();
                FlowWordQueue.Instance.Next();
                EffectQueue.Instance.Next();
                if (IsGamePaused)
                    return;
                miniItemView1.NewTick();
                roundMark++;
                if (MonsterQueue.Instance.LeftCount == 0 || MonsterQueue.Instance.RightCount == 0)
                {
                    EndGame();
                    return;
                }

                if (hitRound >= 0)//战斗动画播放中
                {
                    hitRound--;
                    return;
                }

                if (roundMark % 2 == 0)
                {
                    hitMonsterId = MonsterQueue.Instance.NextAction();//1回合
                    if (hitMonsterId > 0)
                    {
                        hitRound = SysConstants.BattleAttackRoundWait;
                    }
                }
                if (roundMark % 20 == 0)//0.1回合
                {
                    PlayerManager.RoundRecover(rule == BattleRuleType.FastRecover);
                    if (timeViewer1.TimeGo())
                        PlayerManager.CheckRoundCard(); //3回合
                }
                BattleInfo.Instance.Round = roundMark / 200 + 1;
                if (roundMark % 10 == 0)
                {
                    AIStrategy.AIProc(PlayerManager.RightPlayer, isGamePaused);
                    if (autoFight)
                    {
                        AIStrategy.AIProc(PlayerManager.LeftPlayer, isGamePaused);
                    }
                }
            }
            finally
            {
                onTurn = false;
            }
        }

        private void EndGame()
        {
            if (!gameEnd)
            {
                gameEnd = true;
                StopGame();
                lifeClock1.Invalidate();
                lifeClock2.Invalidate();
                BattleInfo.Instance.PlayerWin = MonsterQueue.Instance.LeftCount > 0;
                BattleInfo.Instance.EndTime = DateTime.Now;
                BattleInfo.Instance.LeftHPRate = PlayerManager.LeftPlayer.Anger * 100 / PlayerManager.LeftPlayer.MaxAnger;
                CloseForm();
            }
        }

        private void panelBattle_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(panelBattle.Width, panelBattle.Height);
            Graphics g = Graphics.FromImage(bmp);

            if (showGround)
            {
                BattleInfo.Instance.MemMap.Draw(g);

                if (MagicRegion.Instance.Type != RegionTypes.None && isMouseIn)
                    MagicRegion.Instance.Draw(g, roundMark , mouseX, mouseY);
                for (int i = 0; i < MonsterQueue.Instance.Count; i++)
                {
                    LiveMonster monster = MonsterQueue.Instance[i];
                    if (monster.Id == hitMonsterId)
                    {
                        continue;
                    }
                    Color color = Color.White;
                    if (isMouseIn)
                        color = MagicRegion.Instance.GetColor(monster, mouseX, mouseY);
                    monster.Draw(g, color);
                }
                if (hitMonsterId>0)
                {
                    LiveMonster monster = MonsterQueue.Instance.GetMonsterByUniqueId(hitMonsterId);
                    Color color = Color.White;
                    if (isMouseIn)
                        color = MagicRegion.Instance.GetColor(monster, mouseX, mouseY);
                    monster.Draw(g, color);
                }
                for (int i = 0; i < EffectQueue.Instance.Count; i++)
                    EffectQueue.Instance[i].Draw(g);
                for (int i = 0; i < FlowWordQueue.Instance.Count; i++)
                    FlowWordQueue.Instance[i].Draw(g);

                LiveMonster target = BattleLocationManager.GetPlaceMonster(mouseX, mouseY);
                if (target != null && isMouseIn && MagicRegion.Instance.Type == RegionTypes.None)
                {
                    target.DrawCardToolTips(g);
                }
                if (showPlayerState == 1)
                    PlayerManager.LeftPlayer.DrawToolTips(g);
                else if (showPlayerState == 2)
                    PlayerManager.RightPlayer.DrawToolTips(g);

#if !DEBUG
                if (IsGamePaused)
                {
                    Font font = new Font("微软雅黑", 30, FontStyle.Bold);
                    g.DrawString("游戏暂停", font, Brushes.OrangeRed, 370, 170);
                    font.Dispose();
                }
#endif
            }

            e.Graphics.DrawImageUnscaled(bmp, 0, 0);
            g.Dispose();
            bmp.Dispose();
        }

        private void panelBattle_MouseClick(object sender, MouseEventArgs e)
        {
            if (PlayerManager.LeftPlayer == null)
                return;

            if (autoFight || isGamePaused)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (leftSelectCard != null && (myCursor.Name == "summon" || myCursor.Name == "equip" || myCursor.Name == "cast"))
                {
                    int result;
                    if ((result = PlayerManager.LeftPlayer.CardUsePreCheck(leftSelectCard)) != HSErrorTypes.OK)
                    {
                        FlowWordQueue.Instance.Add(new FlowErrInfo(result, new Point(mouseX, mouseY), 0, 0), false);
                        return;
                    }
                    
                    LiveMonster lm = BattleLocationManager.GetPlaceMonster(mouseX, mouseY);
                    if (myCursor.Name == "summon" && lm == null)
                    {
                        LiveMonster newMon = new LiveMonster(leftSelectCard.Card, new Monster(leftSelectCard.CardId), new Point(mouseX / 100*100, mouseY / 100*100), PlayerIndex.Player1);
                        MonsterQueue.Instance.Add(newMon);
                        BattleLocationManager.UpdateCellOwner(newMon.Position.X, newMon.Position.Y, newMon.Id);
                    }
                    else if (myCursor.Name == "equip" && lm != null)
                    {
                        if (PlayerManager.LeftPlayer.HasBuff(BuffEffectTypes.BreakWeapon))
                        {
                            PlayerManager.LeftPlayer.DelBuff(BuffEffectTypes.BreakWeapon);
                        }
                        else
                        {
                            lm.AddWeapon(leftSelectCard.Card);
                            WeaponConfig weaponConfig = ConfigData.GetWeaponConfig(leftSelectCard.CardId);
                            UserProfile.Profile.OnUseCard(weaponConfig.Star, 0, weaponConfig.Type);
                            BattleInfo.Instance.LeftWeaponAdd++;
                        }
                    }
                    else if (myCursor.Name == "cast")
                    {
                        SpellConfig spellConfig = ConfigData.GetSpellConfig(leftSelectCard.CardId);
                        SpellAssistant.CheckSpellEffect(PlayerIndex.Player1, lm, leftSelectCard.Card, e.Location);
                        UserProfile.Profile.OnUseCard(spellConfig.Star, 0, spellConfig.Type);
                        BattleInfo.Instance.LeftSpellAdd++;
                    }
                    PlayerManager.LeftPlayer.OnUseCard(leftSelectCard);
                    PlayerManager.LeftPlayer.DeleteCardAt(PlayerManager.LeftPlayer.SelectId);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                cardsArray1.DisSelectCard();
            }
        }

        private void panelBattle_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastMouseMoveTime + 50 > TimeTool.GetNowMiliSecond())
            {
                return;
            }
            lastMouseMoveTime = TimeTool.GetNowMiliSecond();

            mouseX = e.X;
            mouseY = e.Y;
            //if (PlayerManager.LeftPlayer != null)
            //    CheckCursor();
        }

        private void panelBattle_MouseEnter(object sender, EventArgs e)
        {
            isMouseIn = true;
        }

        private void panelBattle_MouseLeave(object sender, EventArgs e)
        {
            MagicRegion.Instance.Type = RegionTypes.None;
            mouseX = -1;
            isMouseIn = false;
        }

        private void CheckCursor()
        {
            string cursorname = "default";
            MagicRegion.Instance.Type = RegionTypes.None;
            LiveMonster lm = BattleLocationManager.GetPlaceMonster(mouseX, mouseY);

            if (leftSelectCard != null)
            {
                if (leftSelectCard.CardType == CardTypes.Monster)
                {
                    if (lm == null && mouseX>100&&mouseX<400)
                    {
                        cursorname = "summon";
                    }
                }
                else if (leftSelectCard.CardType == CardTypes.Weapon)
                {
                    if (lm != null && !lm.IsGhost && lm.PPos == PlayerIndex.Player1)
                    {
                        cursorname = "equip";
                    }
                }
                else if (leftSelectCard.CardType == CardTypes.Spell)
                {
                    if (mouseX > 0)
                    {
                        SpellConfig spellConfig = ConfigData.GetSpellConfig(leftSelectCard.CardId);
                        if (BattleLocationManager.IsPlaceCanCast(mouseX, mouseY, spellConfig.Target))
                        {
                            cursorname = "cast";
                            MagicRegion.Instance.Update(spellConfig);
                        }
                        else
                        {
                            cursorname = "nocast";
                        }
                    }
                }
            }
            myCursor.ChangeCursor(cursorname);
        }

        private void timeViewer1_Click(object sender, EventArgs e)
        {
            IsGamePaused = !IsGamePaused;
        }

        private void lifeClock1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > 2 && e.X < 60 && e.Y > 2 && e.Y < 66)
                showPlayerState = 1;
            else
                showPlayerState = 0;
        }

        private void lifeClock1_MouseLeave(object sender, EventArgs e)
        {
            showPlayerState = 0;
        }

        private void lifeClock2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > 320 && e.X < 378 && e.Y > 2 && e.Y < 66)
                showPlayerState = 2;
            else
                showPlayerState = 0;
        }

        private void cardsArray1_SelectionChange(object sender, EventArgs e)
        {
            leftSelectCard = cardsArray1.GetSelectCard();
            panelState.Invalidate();
        }

        private void panelState_Paint(object sender, PaintEventArgs e)
        {
            if (showGround)
            {
                if (leftSelectCard != null)
                {
                    cardsArray1.GetTargetCard().DrawOnStateBar(e.Graphics);
                }
            }
        }

        private void CheckEnable()
        {
            bitmapButtonAuto.Enabled = !isGamePaused;
            bitmapButtonThrow.Enabled = !autoFight && !isGamePaused;
            cardsArray1.Enabled = !autoFight && !isGamePaused;
            miniItemView1.Enabled = !autoFight && !isGamePaused;
        }

        private void bitmapButtonAuto_Click(object sender, EventArgs e)
        {
            autoFight = !autoFight;
            CheckEnable();
            if (bitmapButtonAuto.ImageNormal != null)
            {
                bitmapButtonAuto.ImageNormal.Dispose();
                bitmapButtonAuto.ImageMouseOver.Dispose();
                bitmapButtonAuto.ImagePressed.Dispose();
            }
            if (autoFight)
            {
                bitmapButtonAuto.ImageNormal = PicLoader.Read("Button", "AutoFightButton2.JPG");
                bitmapButtonAuto.ImageMouseOver = PicLoader.Read("Button", "AutoFightButton2On.JPG");
                bitmapButtonAuto.ImagePressed = PicLoader.Read("Button", "AutoFightButton2.JPG");
                bitmapButtonAuto.Text = @"关闭自动战斗";
            }
            else
            {
                bitmapButtonAuto.ImageNormal = PicLoader.Read("Button", "AutoFightButton.JPG");
                bitmapButtonAuto.ImageMouseOver = PicLoader.Read("Button", "AutoFightButtonOn.JPG");
                bitmapButtonAuto.ImagePressed = PicLoader.Read("Button", "AutoFightButton.JPG");
                bitmapButtonAuto.Text = @"开启自动战斗";
            }
        }

        private void bitmapButtonThrow_MouseClick(object sender, MouseEventArgs e)
        {
            if (PlayerManager.LeftPlayer.SelectId > 0)
            {
                PlayerManager.LeftPlayer.DeleteCardAt(PlayerManager.LeftPlayer.SelectId);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            InitGame();
            buttonStart.Hide();
            miniItemView1.Enabled = true;
        }

        private bool CloseCheck()
        {
            if (!isHuman)
            {
                return false;
            }

            IsGamePaused = true;
            if (ControlPlus.MessageBoxEx2.Show("现在退出战斗，将会被判输哦！") == DialogResult.Cancel)
            {
                IsGamePaused = false;
                return false;
            }
            StopGame();
            return true;
        }

        internal void ComClose()
        {
            OnGameOver();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (CloseCheck())
            {
                OnGameOver();
            }
        }

        internal void OnGameOver()
        {
            ImageManager.Compress();
            Close();
            if (PlayerManager.RightPlayer != null)
            {
                MainForm.Instance.DealPanel(new BattleResultForm());
            }
            if (BattleInfo.Instance.PlayerWin)
            {
                if (BattleWin != null)
                {
                    BattleWin();
                }
            }
            else
            {
                if (BattleLose != null)
                {
                    BattleLose();
                }
            }
        }

        private void BattleForm_Paint(object sender, PaintEventArgs e)
        {
            BorderPainter.Draw(e.Graphics, "", Width, Height);

            Font font = new Font("黑体", 12, FontStyle.Bold);
            e.Graphics.DrawString(" 模拟战斗 ", font, Brushes.White, Width / 2 - 40, 8);
            font.Dispose();
        }

        private void bitmapButtonCode_Click(object sender, EventArgs e)
        {
            (new CommandBattleForm()).ShowDialog();
        }

    }
}