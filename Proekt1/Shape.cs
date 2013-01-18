using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Proekt1
{
    public abstract class Shape
         {
            public abstract void DrawWith(Graphics g, Pen p);
            public abstract void SaveTo(StreamWriter sw);
            public abstract string ConfString { get; }
 	     }

    public class Cross : Shape
        {
            int X, Y;
            Pen p = new Pen(Color.Red);
            public Cross(int _X, int _Y)
            {
                X = _X; Y = _Y;
            }
            public override void DrawWith(Graphics g, Pen p)
            {
                g.DrawLine(p, X - 4, Y - 4, X + 4, Y + 4);
                g.DrawLine(p, X + 4, Y - 4, X - 4, Y + 4);
            }
            public override void SaveTo(StreamWriter sw)
            {
                sw.WriteLine("Cross");
                sw.Write(Convert.ToString(X));
                sw.Write(' ');
                sw.WriteLine(Convert.ToString(Y));
            }
            public Cross(StreamReader _sr)
            {
                string line = _sr.ReadLine();
                string[] str = line.Split(' ');
                X = Convert.ToInt32(str[0]);
                Y = Convert.ToInt32(str[1]);
            }
            public override string ConfString
            {
                get
                {
                    return "Cross " + Convert.ToString(X) + " " + Convert.ToString(Y);
                }
            }
        }
    public class Line : Shape
        {
            Point C, F;  
            Pen p = new Pen(Color.Blue);
            public Line(Point _C, Point _F)
                {
                    this.C = _C; this.F = _F;
                }
            public override void DrawWith(Graphics g, Pen p)

            {
                 g.DrawLine(p, C, F);
            }
            public override void SaveTo(StreamWriter sw)
            {
                sw.WriteLine("Line");
                sw.Write(Convert.ToString(C.X));
                sw.Write(' ');
                sw.Write(Convert.ToString(C.Y));
                sw.Write(' ');
                sw.Write(Convert.ToString(F.X));
                sw.Write(' ');
                sw.WriteLine(Convert.ToString(F.Y));
            }
            public Line(StreamReader _sr)
            {
                string line = _sr.ReadLine();
                string[] str = line.Split(' ');
                C.X = Convert.ToInt32(str[0]);
                C.Y = Convert.ToInt32(str[1]);
                F.X = Convert.ToInt32(str[2]);
                F.Y = Convert.ToInt32(str[3]);
            }
            public override string ConfString
            {
                get
                {
                    return "Line " + Convert.ToString(C) + " : " + Convert.ToString(F);
                }
            }
 
        }
   
    public class Circle : Shape
    {
        Point C, P;
        Pen p = new Pen(Color.Blue);
        int r;
        public Circle(Point _C, Point _P)
        {
            C = _C;
            P = _P;
            r = Convert.ToInt32(Math.Sqrt(Math.Pow(C.X - P.X,2) + Math.Pow(C.Y - P.Y,2)));
        }
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawEllipse(p,C.X - r, C.Y - r, 2 * r, 2 * r);
            g.DrawLine(p, C, P);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.Write(Convert.ToString(C.X));
            sw.Write(' ');
            sw.Write(Convert.ToString(C.Y));
            sw.Write(' ');
            sw.WriteLine(Convert.ToString(r));
        }
        public Circle(StreamReader _sr)
        {
            string line = _sr.ReadLine();
            string[] str = line.Split(' ');
            C.X = Convert.ToInt32(str[0]);
            C.Y = Convert.ToInt32(str[1]);
            r = Convert.ToInt32(str[2]);
        }
        public override string ConfString
        {
            get
            {
                return "Circle " + Convert.ToString(C) + " : " + Convert.ToString(P);
            }
        }
    }
}
