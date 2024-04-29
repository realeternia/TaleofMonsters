using System;
using NarlonLib.Math;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.DataTent;
using System.Drawing;
using TaleofMonsters.Controler.Battle.Data.MemMap;
using TaleofMonsters.Controler.Battle.Data.Players;

namespace TaleofMonsters.Controler.Battle.Tool
{
    class BattleLocationManager
    {
        public static Point GetMonsterPoint(PlayerIndex pPos)
        {
            int size = BattleInfo.Instance.MemMap.CardSize;
            while (true)
            {
                int x = pPos == PlayerIndex.Player1 ? MathTool.GetRandom(1, 4) : MathTool.GetRandom(5, 8);
                int y = MathTool.GetRandom(0, 4);
                if (BattleInfo.Instance.MemMap.Cells[x, y].Owner == 0)
                {
                    return new Point(x * size, y * size);
                }
            }
        }

        public static Point GetHeroPoint(PlayerIndex pPos, int id) //初始化使用
        {
            int size = BattleInfo.Instance.MemMap.CardSize;
            while (true)
            {
                int x = pPos == PlayerIndex.Player1 ? 0 : 8;
                int y = 0;
                if (BattleInfo.Instance.MemMap.Cells[x, y].Owner == 0)
                {
                    return new Point(x * size, y * size);
                }
            }
        }

        public static LiveMonster GetPlaceMonster(int x, int y)
        {
            if (x < 0 || y < 0 || x >= BattleInfo.Instance.MemMap.StageWidth || y >= BattleInfo.Instance.MemMap.StageHeight)
                return null;

            MemMapPoint mp = BattleInfo.Instance.MemMap.GetMouseCell(x, y);

            return MonsterQueue.Instance.GetMonsterByUniqueId(mp.Owner);
        }

        public static void SetToPosition(LiveMonster lm, Point dest)
        {
            MemMapPoint point = BattleInfo.Instance.MemMap.GetMouseCell(dest.X, dest.Y);
            if (point.Owner <= 0 && !lm.HasBuff(DataType.Buffs.BuffEffectTypes.NoMove))
            {
                UpdateCellOwner(lm.Position.X, lm.Position.Y, 0);
                lm.Position = new Point(point.X, point.Y);
                UpdateCellOwner(point.X, point.Y, lm.Id);
            }
        }

        public static bool IsPlaceBlank(int tx, int ty)
        {
            MemMapPoint point = BattleInfo.Instance.MemMap.GetMouseCell(tx, ty);
            return point.Owner <= 0 && point.SideIndex > 0 && point.SideIndex < 4;
        }

        public static bool IsPlaceTombSide(int tx, int ty, PlayerIndex index)
        {
            MemMapPoint point = BattleInfo.Instance.MemMap.GetMouseCell(tx, ty);
            return point.Owner < 0 && point.SideIndex > 0 && point.SideIndex < 4;
        }

        public static Point GetMonsterNearPoint(Point pos, string type, bool goLeft)
        {
            int size = BattleInfo.Instance.MemMap.CardSize;
            if (type == "side")
            {
                Point pa = new Point(pos.X, pos.Y - size);
                Point pb = new Point(pos.X, pos.Y + size);
                bool paavail = BattleInfo.Instance.MemMap.IsMousePositionCanSummon(pa.X, pa.Y);
                bool pbavail = BattleInfo.Instance.MemMap.IsMousePositionCanSummon(pb.X, pb.Y);
                if (paavail && !pbavail)
                {
                    return pa;
                }
                if (!paavail && pbavail)
                {
                    return pb;
                }
                if (paavail)
                {
                    return MathTool.GetRandom(2) == 0 ? pa : pb;
                }
                return new Point(-1, -1);
            }
            if (type == "back") //击退
            {
                if (goLeft)
                {
                    Point pa = new Point(pos.X + size, pos.Y);
                    bool paavail = BattleInfo.Instance.MemMap.IsMousePositionCanSummon(pa.X, pa.Y);
                    if (paavail)
                    {
                        return pa;
                    }
                }
                else
                {
                    Point pa = new Point(pos.X - size, pos.Y);
                    bool paavail = BattleInfo.Instance.MemMap.IsMousePositionCanSummon(pa.X, pa.Y);
                    if (paavail)
                    {
                        return pa;
                    }
                }

                return new Point(-1, -1);
            }
            if (type == "come") //拉过来
            {
                if (goLeft)
                {
                    Point pa = new Point(pos.X - size, pos.Y);
                    bool paavail = BattleInfo.Instance.MemMap.IsMousePositionCanSummon(pa.X, pa.Y);
                    if (paavail)
                    {
                        return pa;
                    }
                }
                else
                {
                    Point pa = new Point(pos.X + size, pos.Y);
                    bool paavail = BattleInfo.Instance.MemMap.IsMousePositionCanSummon(pa.X, pa.Y);
                    if (paavail)
                    {
                        return pa;
                    }
                }

                return new Point(-1, -1);
            }
            if (type == "around") //随机
            {
                RandomMaker rm = new RandomMaker();
                int count = 0;
                for (int i = 0; i < 4; i++)
                {
                    int xoff = i > 1 ? 0 : i*2 - 1;
                    int yoff = i <2 ? 0 : i * 2 - 5;
                    Point pa = new Point(pos.X + xoff * size, pos.Y + yoff * size);
                    bool paavail = BattleInfo.Instance.MemMap.IsMousePositionCanSummon(pa.X, pa.Y);
                    if (paavail)
                    {
                        rm.Add(i,1);
                        count++;
                    }
                }
                if (count > 0)
                {
                    int sel = rm.Process(1)[0];
                    return new Point(pos.X + (sel > 1 ? 0 : sel * 2 - 1) * size, pos.Y + (sel < 2 ? 0 : sel * 2 - 5) * size);
                }
            }
            return new Point(-1, -1);
        }

        public static bool IsPlaceCanCast(int tx, int ty, string target) //玩家专用函数
        {
            if (BattleTargetManager.IsSpellNullTarget(target))
                return true;

            if (BattleTargetManager.IsSpellGridTarget(target))
                return IsPlaceBlank(tx, ty);

            if (BattleTargetManager.IsSpellTombTarget(target))
                return IsPlaceTombSide(tx, ty, BattleTargetManager.IsPlaceFriendMonster(target) ? PlayerIndex.Player1 : PlayerIndex.Player2);

            LiveMonster deskMon = GetPlaceMonster(tx, ty);
            if (deskMon != null && !deskMon.IsGhost)
            {
                if (deskMon.PPos == PlayerIndex.Player1 && BattleTargetManager.IsPlaceFriendMonster(target))
                    return true;

                if (deskMon.PPos == PlayerIndex.Player2 && BattleTargetManager.IsPlaceEnemyMonster(target))
                    return true;
            }

            return false;
        }

        public static bool IsPointInRegionType(RegionTypes type, int mouseX, int mouseY, Point pos, int range)
        {
            if (type == RegionTypes.None)
                return false;

            int size = BattleInfo.Instance.MemMap.CardSize;
            int targetX = pos.X + size / 2;
            int targetY = pos.Y + size / 2;
            MemMapPoint mousePoint = BattleInfo.Instance.MemMap.GetMouseCell(mouseX, mouseY);
            if (type == RegionTypes.Circle)
            {
                if (MathTool.GetDistance(mousePoint.X + size / 2, mousePoint.Y + size / 2, targetX, targetY) > range * size / 10)
                    return false;
            }
            else if (type == RegionTypes.Cross)
            {
                if (!((mousePoint.X == pos.X && Math.Abs(mousePoint.Y - pos.Y) <= range * size / 10) || (mousePoint.Y == pos.Y && Math.Abs(mousePoint.X - pos.X) <= range * size / 10)))
                    return false;
            }
            else if (type == RegionTypes.XCross)
            {
                if (MathTool.GetDistance(mousePoint.X + size / 2, mousePoint.Y + size / 2, targetX, targetY) > range * size / 10)
                    return false;

                if (Math.Abs(mousePoint.X - pos.X) != Math.Abs(mousePoint.Y - pos.Y))
                    return false;
            }
            else if (type == RegionTypes.Grid)
            {
                if (mousePoint.Y != pos.Y || mousePoint.X != pos.X)
                    return false;
            }
            else if (type == RegionTypes.Row)
            {
                if (mousePoint.Y != pos.Y)
                    return false;
            }
            else if (type == RegionTypes.RowHalf)
            {
                if (mousePoint.Y != pos.Y || (!mousePoint.IsLeft && pos.X <= size * 4) || (mousePoint.IsLeft && pos.X >= size * 4))
                    return false;
            }
            else if (type == RegionTypes.Column)
            {
                if (mousePoint.X != pos.X)
                    return false;
            }
            else if (type == RegionTypes.Net)
            {
                if (MathTool.GetDistance(mousePoint.X + size / 2, mousePoint.Y + size / 2, targetX, targetY) > range * size / 10)
                    return false;

                if ((Math.Abs(mousePoint.X + mousePoint.Y - pos.X - pos.Y) / size % 2) == 1)
                    return false;
            }
            else if (type == RegionTypes.AllHalf)
            {
                if ((!mousePoint.IsLeft && pos.X <= size * 4) || (mousePoint.IsLeft && pos.X >= size * 4))
                    return false;
            }
            else if (type == RegionTypes.AllHalfOther)
            {
                if ((!mousePoint.IsLeft && pos.X >= size * 4) || (mousePoint.IsLeft && pos.X <= size * 4))
                    return false;
            }
            return true;
        }

        public static void UpdateCellOwner(int x, int y, int owner)
        {
            int cardSize = BattleInfo.Instance.MemMap.CardSize;
            int mid = BattleInfo.Instance.MemMap.Cells[x / cardSize, y / cardSize].Owner;
            if (mid < 0)
            {
                LiveMonster lm = MonsterQueue.Instance.GetMonsterByUniqueId(Math.Abs(mid));
                lm.GhostTime += 10000;//直接回收吧
            }
            BattleInfo.Instance.MemMap.Cells[x / cardSize, y / cardSize].UpdateOwner(owner);
        }
    }
}
