using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Proekt1
{
    public partial class Form1 : Form
    {
         List<Shape> Shapes = new List<Shape>();
         Point ShapeStart;
         bool IsShapeStart = false;
         string curFile;
         Pen p;
         Pen p1 = new Pen(Color.Black);
         Pen p2 = new Pen(Color.Green);
         Pen p3 = new Pen(Color.Red, 5);
         Shape TempShape;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (rb_cross.Checked) TempShape = new Cross(e.X, e.Y);
            else if (rb_line.Checked)
            {
                if (!IsShapeStart)
                {
                    TempShape = new Line(ShapeStart, e.Location);
                }
            }
            else if (rb_circle.Checked)
            {
                if (!IsShapeStart)
                {
                    TempShape = new Circle(ShapeStart, e.Location);
                }
            }
            this.Refresh();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (rb_cross.Checked)
            {
                AddShape(TempShape);
            }
            if (rb_line.Checked)
            {
                if (IsShapeStart) ShapeStart = e.Location;
                else AddShape(TempShape);
                
            }
            if (rb_circle.Checked)
            {
                if (IsShapeStart) ShapeStart = e.Location;
                else AddShape(TempShape);
                
            }
            IsShapeStart = !IsShapeStart;
            this.Refresh();
        }
       
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape f in Shapes)
            {
                f.DrawWith(e.Graphics, p1);
            }
            if (TempShape != null)
            {
                TempShape.DrawWith(e.Graphics, p2);
            }
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            TempShape = null;
            IsShapeStart = true;
        }

        private void AddShape(Shape s)
        {
            Shapes.Add(s);
            Shapes_List.Items.Add(s.ConfString);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                curFile = saveFileDialog1.FileName;
                StreamWriter sw = new StreamWriter(curFile);
                foreach (Shape p in this.Shapes)
                {
                    p.SaveTo(sw);
                }
                sw.Close();
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                curFile = openFileDialog1.FileName;
                Shapes.Clear();
                StreamReader sr = new StreamReader(curFile);
                while (!sr.EndOfStream)
                {
                    string type = sr.ReadLine();
                    switch (type)
                    {
                        case "Cross":
                            {
                                AddShape(new Cross(sr));
                                break;
                            }
                        case "Line":
                            {
                                AddShape(new Line(sr));
                                break;
                            }
                        case "Circle":
                            {
                                AddShape(new Circle(sr));
                                break;
                            }
                    }
                }
                sr.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (Shapes_List.SelectedIndices.Count > 0)
            {
                Shapes.RemoveAt(Shapes_List.SelectedIndices[0]);
                Shapes_List.Items.RemoveAt(Shapes_List.SelectedIndices[0]);
            }
            //button1.Enabled = false;
            this.Refresh();
        }
        private void Shapes_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }

}
