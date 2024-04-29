using System.Drawing;
using ConfigDatas;
using TaleofMonsters.Controler.Battle.Data.MemMap;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Controler.Battle.Data.Players;
using TaleofMonsters.Controler.Battle.Tool;

namespace TaleofMonsters.Controler.Battle.Data
{
    internal class MagicRegion
    {
        private static MagicRegion instance;

        internal static MagicRegion Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MagicRegion();
                }
                return instance;
            }
        }

        internal static void Init()
        {
            instance = new MagicRegion();
        }

        private RegionTypes type;
        private int range;
        private Color color;

        internal RegionTypes Type
        {
            get { return type; }
            set { type = value; }
        }

        internal void Update(SpellConfig spellConfig)
        {
            type = BattleTargetManager.GetRegionType(spellConfig.Target[2]);
            range = spellConfig.Range;
            switch (spellConfig.Target[1])
            {
                case 'E':
                    color = Color.Red;
                    break;
                case 'F':
                    color = Color.Green;
                    break;
                case 'A':
                    color = Color.Yellow;
                    break;
                default:
                    color = Color.White;
                    break;
            }
        }

        internal Color GetColor(LiveMonster lm, int mouseX, int mouseY)
        {
            if (color == Color.Red && lm.PPos == PlayerIndex.Player1)
                return Color.White;

            if (color == Color.Green && lm.PPos == PlayerIndex.Player2)
                return Color.White;

            if (!BattleLocationManager.IsPointInRegionType(type, mouseX, mouseY, lm.Position, range))
            {
                return Color.White;
            }

            return color;
        }

        internal void Draw(Graphics g, int round, int mouseX, int mouseY)
        {
            int size = BattleInfo.Instance.MemMap.CardSize;
            SolidBrush fillBrush = new SolidBrush(Color.FromArgb(60, color));
            Pen borderPen = new Pen(color, 3);

            int roundoff = ((round/5)%3)*3 + 8;

            foreach (MemMapPoint memMapPoint in BattleInfo.Instance.MemMap.Cells)
            {
                if (BattleLocationManager.IsPointInRegionType(type, mouseX, mouseY, memMapPoint.ToPoint(), range))
                {
                    g.FillRectangle(fillBrush, memMapPoint.X + roundoff, memMapPoint.Y + roundoff, size - roundoff * 2, size - roundoff * 2);
                    g.DrawRectangle(borderPen, memMapPoint.X + roundoff, memMapPoint.Y + roundoff, size - roundoff * 2, size - roundoff * 2);
                }
            }
            borderPen.Dispose();
            fillBrush.Dispose();
        }

    }
}