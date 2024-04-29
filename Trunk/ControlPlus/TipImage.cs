using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ControlPlus
{
    public class TipImage
    {
        List<LineInfo> datas = new List<LineInfo>();
        List<ImageInfo> imgs = new List<ImageInfo>();

        public void AddTextNewLine(string data, string color, int height)
        {
            LineInfo info = new LineInfo(datas.Count, height);
            info.objects.Add(new LineText(data, color));
            datas.Add(info);
        }

        public void AddTextNewLine(string data, string color)
        {
            AddTextNewLine(data, color, 16);
        }

        public void AddText(string data, string color)
        {
            datas[datas.Count-1].objects.Add(new LineText(data, color));
        }

        public void AddImage(Image img, int wid)
        {
            datas[datas.Count - 1].objects.Add(new LineImage(img, wid));
        }

        public void AddImage(Image img)
        {
            AddImage(img, datas[datas.Count - 1].height);
        }

        public void AddImageXY(Image img,int sx, int sy, int swidth, int sheight, int x,int y, int width, int height)
        {
            ImageInfo info = new ImageInfo();
            info.img = img;
            info.sx = sx;
            info.sy = sy;
            info.swidth = swidth;
            info.sheight = sheight;
            info.x = x;
            info.y = y;
            info.width = width;
            info.height = height;
            imgs.Add(info);
        }

        public void AddBar(int wid, int per, Color start, Color end)
        {
            datas[datas.Count - 1].objects.Add(new LineBar(wid, per, start, end));
        }

        public void AddLine()
        {
            AddLine(5);
        }

        public void AddLine(int height)
        {
            LineInfo info = new LineInfo(datas.Count, height);
            info.objects.Add(new LineLine());
            datas.Add(info);
        }

        public Image Image
        {
            get
            {
                int wid = 120, heg = 0;
                Bitmap bmp = new Bitmap(300, 300);
                Graphics g = Graphics.FromImage(bmp);
                Font fontInfo = new Font("宋体", 9, FontStyle.Regular);
                for (int i = 0; i < datas.Count;i++ )
                {
                    foreach (ILineObject obj in datas[i].objects)
                    {
                        if (obj is LineText)
                        {
                            LineText text = (obj as LineText);
                            text.UpdateWid((int) g.MeasureString(text.text, fontInfo).Width);
                        }
                    }
                    wid = Math.Max(wid, datas[i].width);
                    heg += datas[i].height;
                }
                wid += 5;
                heg += 5;
                g.Dispose();
                bmp.Dispose();
                bmp = new Bitmap(wid, heg);
                g = Graphics.FromImage(bmp);
                g.FillRectangle(Brushes.Black, 0, 0, wid, heg);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), 0, 0, wid, datas[0].height);
                int y = 2;
                datas[0].Draw(g, 5, y, wid);
                for (int i = 1; i < datas.Count; i++)
                {
                    y += datas[i-1].height;
                    datas[i].Draw(g, 5, y, wid);
                }
                fontInfo.Dispose();
                foreach (ImageInfo imageInfo in imgs)
                {
                    Rectangle dest = new Rectangle(imageInfo.x, imageInfo.y, imageInfo.width, imageInfo.height);
                    g.DrawImage(imageInfo.img, dest, imageInfo.sx, imageInfo.sy, imageInfo.swidth, imageInfo.sheight, GraphicsUnit.Pixel);
                }
                Pen pen = new Pen(Brushes.Gray, 2);
                g.DrawRectangle(pen, 1, 1, wid - 3, heg - 3);
                pen.Dispose();
                g.Dispose();
                return bmp;
            }
        }
    }

    struct LineInfo
    {
        private int id;
        public List<ILineObject> objects;
        public int height;

        public LineInfo(int id, int height)
        {
            this.id = id;
            this.height = height;
            objects = new List<ILineObject>();
        }
        public int width
        {
            get { int sum = 0;
                foreach (ILineObject obj in objects)
                {
                    sum += obj.width();
                }
                return sum;
            }
        }
        public void Draw(Graphics g, int x, int y, int twid)
        {
            int xoff = x;
            foreach (ILineObject obj in objects)
            {
                obj.Draw(g, id, xoff, y, twid, height);
                xoff += obj.width();
            }
        }
    }

    struct ImageInfo
    {
        public Image img;
        public int sx;
        public int sy;
        public int swidth;
        public int sheight;
        public int x;
        public int y;
        public int width;
        public int height;
    }

    interface ILineObject
    {
        int width();
        void Draw(Graphics g, int id, int x, int y, int twid, int height);
    }

    class LineText : ILineObject
    {
        public string text;
        public string color;
        private int wid;

        public LineText(string txt, string cor)
        {
            text = txt;
            color = cor;
        }

        public void UpdateWid(int wd)
        {
            wid = wd;
        }

        #region ILineObject 成员

        public int width()
        {
            return wid;
        }

        public void Draw(Graphics g, int id, int x, int y, int twid, int height)
        {
            Font fontInfo = new Font("宋体", id == 0 ? 10 : 9, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.FromName(color));
            g.DrawString(text, fontInfo, brush, x, y+(height-14)/2, StringFormat.GenericTypographic);
            fontInfo.Dispose();
            brush.Dispose();
        }

        #endregion
    }

    class LineImage : ILineObject
    {
        private Image img;
        private int wid;

        public LineImage(Image img, int wid)
        {
            this.img = img;
            this.wid = wid;
        }

        #region ILineObject 成员

        public int width()
        {
            return wid;
        }

        public void Draw(Graphics g, int id, int x, int y, int twid, int height)
        {
            g.DrawImage(img, x, y-1, wid, height);
        }

        #endregion
    }

    class LineBar : ILineObject
    {
        private int wid;
        private int per;
        private Color start;
        private Color end;

        public LineBar(int wid, int per, Color start, Color end)
        {
            this.wid = wid;
            this.per = per;
            this.start = start;
            this.end = end;
        }

        #region ILineObject 成员

        public int width()
        {
            return wid;
        }

        public void Draw(Graphics g, int id, int x, int y, int twid, int height)
        {
            int rwid = wid - 10;
            LinearGradientBrush b1 = new LinearGradientBrush(new Rectangle(x, y + 2, rwid, height - 8), start, end, LinearGradientMode.Horizontal);
            g.FillRectangle(b1, x, y + 2, rwid * per / 100, height - 8);
            g.DrawRectangle(Pens.Gray, x, y + 2, rwid, height - 8);      
            b1.Dispose();
        }

        #endregion
    }

    class LineLine : ILineObject
    {
        #region ILineObject 成员

        public int width()
        {
            return 0;
        }

        public void Draw(Graphics g, int id, int x, int y, int twid, int height)
        {
            g.DrawLine(Pens.Gray, x, y + height / 2-1, twid - 10, y + height / 2-1);
        }

        #endregion
    }
}
