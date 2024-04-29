using System.Collections.Generic;
using System.Drawing;
using NarlonLib.Core;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.DataTent;
using TaleofMonsters.DataType.Maps;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.Tool;
using TaleofMonsters.DataType.Others;
using ConfigDatas;

namespace TaleofMonsters.Controler.Battle.Data.MemMap
{
    internal class MemRowColumnMap:IMap
    {
        private const int stageWidth = 900;
        private const int stageHeight = 400;
        private const int cardSize = 100;
        private const int rowCount = stageHeight/cardSize;
        private const int columnCount = stageWidth / cardSize;
        private MemMapPoint[,] cells;
        private AutoDictionary<int, int> tiles;

        public int CardSize
        {
            get { return cardSize; }
        }

        public int RowCount
        {
            get { return rowCount; }
        }

        public int StageWidth
        {
            get { return stageWidth; }
        }

        public int StageHeight
        {
            get { return stageHeight; }
        }

        public MemMapPoint[,] Cells
        {
            get { return cells; }
            set { cells = value; }
        }

        private bool isDirty;
        private Image cachImage;

        public MemRowColumnMap(string map, int tile)
        {
            BattleMap bMap = BattleMapBook.GetMap(map);
            cells = new MemMapPoint[columnCount, rowCount];
            tiles = new AutoDictionary<int, int>();
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    int tarTile = bMap.cells[i, j];
                    if (tarTile == 0)
                    {
                        tarTile = tile == 0 ? 9 : tile;
                    }
                    cells[i, j] = new MemMapPoint(i, i * cardSize, j * cardSize, columnCount, tarTile);
                    tiles[tarTile == 9 ? 0 : tarTile]++;
                }
            }
            isDirty = true;
        }

        public MemMapPoint GetCell(int x, int y)
        {
            return cells[x, y];
        }

        public MemMapPoint GetMouseCell(int x, int y)
        {
            return cells[x / cardSize, y / cardSize];
        }

        public bool IsMousePositionCanSummon(int x, int y)
        {
            if (x < cardSize || (x >= cardSize*4 && x < cardSize*5) || x >= cardSize*8)
            {
                return false;
            }
            if (y < 0 || y >= cardSize*4)
            {
                return false;
            }
            return GetMouseCell(x, y).Owner <= 0;
        }

        public int GetEmemyId(int mid, PlayerIndex pos, int y) //pos方位
        {
            int rowid = y/cardSize;
            if (pos == PlayerIndex.Player1)
            {
                for (int j = rowid; j >= 0; j--)
                {
                    for (int i = 5; i < columnCount; i++)
                    {
                        if (cells[i, j].Owner > 0 && cells[i, j].Owner != mid)
                        {
                            return cells[i, j].Owner;
                        }
                    }
                }
                for (int j = rowid + 1; j < rowCount; j++)
                {
                    for (int i = 5; i < columnCount; i++)
                    {
                        if (cells[i, j].Owner > 0 && cells[i, j].Owner != mid)
                        {
                            return cells[i, j].Owner;
                        }
                    }
                }
            }
            else
            {
                for (int j = rowid; j >= 0; j--)
                {
                    for (int i = 3; i >= 0; i--)
                    {
                        if (cells[i, j].Owner > 0 && cells[i, j].Owner != mid)
                        {
                            return cells[i, j].Owner;
                        }
                    }
                }
                for (int j = rowid + 1; j < rowCount; j++)
                {
                    for (int i = 3; i >= 0; i--)
                    {
                        if (cells[i, j].Owner > 0 && cells[i, j].Owner != mid)
                        {
                            return cells[i, j].Owner;
                        }
                    }
                }
            }
            return 0;
        }

        public void SetTile(int itype, Point point, int dis, int tile)
        {
            RegionTypes type = (RegionTypes)itype;
            foreach (MemMapPoint memMapPoint in cells)
            {
                if (BattleLocationManager.IsPointInRegionType(type, point.X, point.Y, memMapPoint.ToPoint(), dis))
                {
                    memMapPoint.Tile = tile;
                }
            }
            tiles.Clear();
            foreach (MemMapPoint memMapPoint in cells)
            {
                tiles[memMapPoint.Tile == 9 ? 0 : memMapPoint.Tile]++;
            }
            isDirty = true;
        }

        public void ChangePositionWithRandom(IMonster target)
        {
            if (target.IsHero)
                return;

            RandomMaker rm = new RandomMaker();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                int xoff = i > 1 ? 0 : i * 2 - 1;
                int yoff = i < 2 ? 0 : i * 2 - 5;
                Point pa = new Point(target.Position.X + xoff * cardSize, target.Position.Y + yoff * cardSize);
                LiveMonster lm = BattleLocationManager.GetPlaceMonster(pa.X, pa.Y);
                if (lm != null && !lm.IsHero)
                {
                    rm.Add(lm.Id, 1);
                    count++;
                }
            }
            if (count >= 1)
            {
                int id = rm.Process(1)[0];
                LiveMonster lm = MonsterQueue.Instance.GetMonsterByUniqueId(id);
                BattleLocationManager.UpdateCellOwner(lm.Position.X, lm.Position.Y, target.Id);
                BattleLocationManager.UpdateCellOwner(target.Position.X, target.Position.Y, lm.Id);
                Point temp = lm.Position;
                lm.Position = target.Position;
                target.Position = temp;
            }
        }

        public void DragRandomUnitNear(Point mouse)
        {
            MemMapPoint point = BattleInfo.Instance.MemMap.GetMouseCell(mouse.X, mouse.Y);
            if (point.SideIndex < 1 || point.SideIndex > 3)//不能传
                return;

            RandomMaker rm = new RandomMaker();
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                int xoff = i > 1 ? 0 : i * 2 - 1;
                int yoff = i < 2 ? 0 : i * 2 - 5;
                Point pa = new Point(mouse.X + xoff * cardSize, mouse.Y + yoff * cardSize);
                LiveMonster lm = BattleLocationManager.GetPlaceMonster(pa.X, pa.Y);
                if (lm != null && !lm.IsHero)
                {
                    rm.Add(lm.Id, 1);
                    count++;
                }
            }
            if (count >= 1)
            {
                int id = rm.Process(1)[0];
                LiveMonster lm = MonsterQueue.Instance.GetMonsterByUniqueId(id);

                BattleLocationManager.SetToPosition(lm, point.ToPoint());
            }
        }

        public void SetRowUnitPosition(int y, bool isLeft, string type)
        {
            for (int i = 1; i < 8; i++)
            {
                LiveMonster lm = BattleLocationManager.GetPlaceMonster(i * 100, y);
                if (lm == null)
                    continue;

                if (lm.IsGhost || isLeft && lm.PPos == PlayerIndex.Player1)
                    continue;

                if (lm.IsHero)
                    continue;

                lm.SetToPosition(type);
            }
        }

        public IMonster[] GetAllMonster()
        {
            List<IMonster> monsters = new List<IMonster>();
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost)
                    continue;

                monsters.Add(mon);
            }
            return monsters.ToArray();
        }

        public IMonster[] GetAllMonsterType(int type)
        {
            List<IMonster> monsters = new List<IMonster>();
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost)
                    continue;

                if (type != 0 && type != mon.Avatar.MonsterConfig.Type)
                    continue;

                monsters.Add(mon);
            }
            return monsters.ToArray();
        }

        public IMonster[] GetAllMonsterLevel(int minLv, int maxLv)
        {
            List<IMonster> monsters = new List<IMonster>();
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost)
                    continue;

                if (mon.Star < minLv || mon.Star > maxLv)
                    continue;

                monsters.Add(mon);
            }
            return monsters.ToArray();
        }

        public IMonster[] GetRangeMonster(bool isLeft, string target, int range, Point mouse)
        {
            List<IMonster> monsters = new List<IMonster>();
            foreach (LiveMonster mon in MonsterQueue.Instance.Enumerator)
            {
                if (mon.IsGhost)
                    continue;

                if ((BattleTargetManager.IsSpellEnemyMonster(target) && isLeft != mon.Owner.IsLeft) || (BattleTargetManager.IsSpellFriendMonster(target) && isLeft == mon.Owner.IsLeft))
                {
                    RegionTypes rt = BattleTargetManager.GetRegionType(target[2]);
                    if (!BattleLocationManager.IsPointInRegionType(rt, mouse.X, mouse.Y, mon.Position, range))
                        continue;

                    monsters.Add(mon);
                }
            }
            return monsters.ToArray();
        }

        public void ReviveUnit(Point mouse, int addHp)
        {
            int oid = BattleInfo.Instance.MemMap.GetMouseCell(mouse.X, mouse.Y).Owner;
            if (oid < 0)
            {
                LiveMonster lm = MonsterQueue.Instance.GetMonsterByUniqueId(-oid);
                lm.Revive();
                lm.DeleteWeapon();
                lm.Life += addHp;
            }
        }

        public void UpdateCellOwner(Point mouse, int ownerId)
        {
            BattleLocationManager.UpdateCellOwner(mouse.X, mouse.Y, ownerId);
        }

        public int GetTileCount(int tid)
        {
            return tiles[tid];
        }

        public void Draw(Graphics g)
        {
            if (isDirty)
            {
                isDirty = false;
                if (cachImage!=null)
                {
                    cachImage.Dispose();
                }
                cachImage = new Bitmap(stageWidth, stageHeight);
                Graphics cg = Graphics.FromImage(cachImage);

                foreach (var memMapPoint in cells)
                {
                    if (memMapPoint.SideIndex > 0 || memMapPoint.Y == 0)
                    {
                        cg.DrawImage(TileBook.GetTileImage(memMapPoint.Tile, cardSize, cardSize), memMapPoint.X, memMapPoint.Y, cardSize, cardSize);
                    }
                    Pen pen = new Pen(Brushes.DarkRed, 3);
                    if (memMapPoint.SideIndex == 0 || memMapPoint.SideIndex == 4)
                    {
                        cg.DrawLine(pen, memMapPoint.X, memMapPoint.Y, memMapPoint.X, memMapPoint.Y + cardSize);
                        cg.DrawLine(pen, memMapPoint.X + cardSize, memMapPoint.Y, memMapPoint.X + cardSize, memMapPoint.Y + cardSize);
                    }
                    else
                    {
                        cg.DrawRectangle(pen, memMapPoint.X, memMapPoint.Y, cardSize - 1, cardSize);
                    }
#if DEBUG
                    Font font = new Font("Arial", 7, FontStyle.Regular);
                    g.DrawString(memMapPoint.Owner.ToString(), font, Brushes.White, memMapPoint.X, memMapPoint.Y+10);
                    font.Dispose();
#endif
                    pen.Dispose();
                }
                cg.Dispose();
            }
            g.DrawImageUnscaled(cachImage, 0, 0, stageWidth ,stageHeight);
        }

    }
}
