using System.Collections.Generic;
using System.Drawing;
using NarlonLib.Core;
using NarlonLib.Drawing;

namespace TaleofMonsters.MainItem
{
    internal class MainTipManager
    {
        private static List<MainTipData> tips;

        static MainTipManager()
        {
            tips = new List<MainTipData>();
        }

        public static void Refresh()
        {
            tips.Clear();
        }

        public static void DrawAll(Graphics g)
        {
            if(tips.Count <= 0)
                return;

            Color alphaBlack = Color.FromArgb(100, Color.Black);
            Brush alphaBBrush = new SolidBrush(alphaBlack);
            g.FillRectangle(alphaBBrush, 5, 560, 200, 90);
            alphaBBrush.Dispose();

            Font font = new Font("ËÎÌו", 9, FontStyle.Regular);

            MainTipData[] datas = tips.ToArray();
            for (int i = 0; i < datas.Length; i++)
            {
                if (datas[i].Word == "") continue;

                int xoff = 0;
                if (datas[i].Word.IndexOf('|') >= 0)
                {
                    string[] text = datas[i].Word.Split('|');
                    for (int j = 0; j < text.Length; j += 2)
                    {
                        Color color = Color.FromName(datas[i].Color);
                        if (text[j] != "")
                        {
                            color = DrawTool.GetColorFromHtml(text[j]);
                        }
                        Brush brush = new SolidBrush(color);
                        g.DrawString(text[j + 1], font, brush, 11 + xoff, 565 + i * 18, StringFormat.GenericTypographic);
                        brush.Dispose();
                        xoff += (int)g.MeasureString(text[j + 1], font).Width - 5;
                    }
                }
                else
                {
                    Brush brush = new SolidBrush(Color.FromName(datas[i].Color));
                    g.DrawString(datas[i].Word, font, brush, 11, 565 + i * 18, StringFormat.GenericTypographic);
                    brush.Dispose();
                }
            }
            font.Dispose();
        }

        public static void AddTip(string newtip, string color)
        {
            MainTipData sp = new MainTipData();
            sp.Color = color;
            sp.Word = newtip;
            sp.CreateTime = TimeTool.GetNowMiliSecond();
            lock (tips)
            {
                tips.Add(sp);
                if (tips.Count > 5)
                {
                    tips.RemoveAt(0);
                }
            }
        }

        public static bool OnFrame()
        {
            long nowTick = TimeTool.GetNowMiliSecond();
            lock (tips)
            {
                foreach (MainTipData pair in tips)
                {
                    if (pair.CreateTime < nowTick - 10 * 1000)
                    {
                        tips.Remove(pair);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
