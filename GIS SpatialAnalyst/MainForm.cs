using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS_SpatialAnalyst
{

    public partial class MainForm : Form
    {

        private int line_count=0;
        private double xmin;
        private double xmax;
        private double ymin;
        private double ymax;
        private double xrate;
        private double yrate;
        private Graphics grp;
        private Bitmap bmp;
        private Pen pen;
        private Font sfont;
        private string geoStyle;
        
        //Surveying.Point[] dpoint;
        //private List<Surveying.Point> VPoints;
        Queue queueTemp;
        private Surveying.Polygon m_Polygon;
        private Surveying.PolyLine m_Polyline;
        private List<Surveying.Point> m_Points;
        private Surveying.Line m_Line;
        private Surveying.Line test_m_Line;
        private List<Surveying.Triangle> m_Triangles;
        bool rightbutton = false;
     
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            btnClear.Click += new EventHandler(btnClear_Click);
            btnPolyline.Click += new EventHandler(btnPolyline_Click);
            btnPoint.Click += new EventHandler(btnPoint_Click);
            btnPolygon.Click += new EventHandler(btnPolygon_Click);
            btnLine.Click += new EventHandler(btnLine_Click);
            btnSX.Click += new EventHandler(btnSX_Click);
            btnYS.Click += new EventHandler(btnYS_Click);
            btnSLWJ.Click += new EventHandler(btnSLWJ_Click);
            btnCutLine.Click += new EventHandler(btnCutLine_Click);
            btnTIN.Click += new System.EventHandler(btnTIN_Click);
            picDP.MouseMove += new MouseEventHandler(picDP_MouseMove);
            picDP.MouseDown += new MouseEventHandler(picDP_MouseDown);
            this.Load += new EventHandler(MainForm_Load);
            this.Resize += new EventHandler(MainForm_Resize);
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            xrate = (xmax - xmin) / picDP.Height;
            yrate = (ymax - ymin) / picDP.Width;           
        }

        void picDP_MouseDown(object sender, MouseEventArgs e)
        {
            if (geoStyle == "Polygon")
            {
                Polygon_MouseDown(sender, e);
            }
            else if (geoStyle == "Line")
            {
                Line_MouseDown(sender, e);
            }
            else if (geoStyle == "Polyline")
            {
                Polyline_MouseDown(sender, e);
            }
            else if (geoStyle == "Point")
            {
                Point_MouseDown(sender, e);
            }
        }

        void picDP_MouseMove(object sender, MouseEventArgs e)
        {
            if (geoStyle == "Polygon")
            {
                Polygon_MouseMove(sender, e);
            }           
            else if (geoStyle == "Polyline")
            {
                Polyline_MouseMove(sender, e);
            }
            else if (geoStyle == "Point")
            {
                Point_MouseMove(sender, e);
            }
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            xmin = 0;
            ymin = 0;
            xmax = 100;
            ymax = 150;
            xrate = (xmax - xmin) / picDP.Height;
            yrate = (ymax - ymin) / picDP.Width;
            bmp = new Bitmap(picDP.Width, picDP.Height);
            grp = Graphics.FromImage(bmp);
            grp.Clear(picDP.BackColor);
            pen = new Pen(Color.Black, 1);
            sfont = new Font("宋体", 12);
            queueTemp = new Queue();

            m_Polygon = new Surveying.Polygon();
            m_Polyline = new Surveying.PolyLine();
            rightbutton = false;      
            m_Points = new List<Surveying.Point>();
            m_Polygon = new Surveying.Polygon();
            m_Polyline = new Surveying.PolyLine();
            m_Line = new Surveying.Line();
        }

        void btnCutLine_Click(object sender, EventArgs e)
        {
            //string ret;
            //string pInfo = "";
            Surveying.Polygon_clip_Line LCP = new Surveying.Polygon_clip_Line(m_Polyline,m_Polygon);
            List<Surveying.PolyLine> polyLines = LCP.Clip();
            foreach(Surveying.PolyLine polyLine in polyLines)
            {
                DrawPolyLine(polyLine);
                picDP.Image = bmp;                
            }
            picDP.Image = bmp;
            
            //pInfo = pInfo + ret;
            //Surveying.frmMessage newForm = new Surveying.frmMessage();
            //newForm.Info = pInfo;
            //newForm.Show();
            //newForm.Info = pInfo;

        }

        void btnSLWJ_Click(object sender, EventArgs e)
        {
            Surveying.Line_Line_RelationClass LLR = new Surveying.Line_Line_RelationClass();
            string pInfo = "";
            LLR.FLine = m_Line;
            LLR.LLine = test_m_Line;
            int flag = LLR.get_Relation_Line_Line();
            string ret= " ";
            if (flag == 0) ret = ret + "两条线不相交";
            else ret = ret + "两条线相交";

            pInfo = pInfo + ret + "\r\n" + "\r\n";
            Surveying.frmMessage newForm = new Surveying.frmMessage();
            newForm.Info = pInfo;
            newForm.Text = "矢量外积算法";
            newForm.Show();
            newForm.Info = pInfo;

        }

        void btnYS_Click(object sender, EventArgs e)
        {
            Surveying.Point_Line_RelationClass pPL = new Surveying.Point_Line_RelationClass();
            string pInfo = "";
            for (int i = 0; i < m_Points.Count; i++)
            {
                pPL.VPoint = m_Points[i];
                pPL.LinePoint_1 = m_Line.point_one;
                pPL.LinePoint_2 = m_Line.point_two;
                string ret = pPL.RelationCal();
                pInfo = pInfo + "点号" + i.ToString() + ": " + ret + "\r\n" + "\r\n";
            }
            Surveying.frmMessage newForm = new Surveying.frmMessage();
            newForm.Text = "右手法则";
            newForm.Info = pInfo;
            newForm.Show();
        }

        void btnSX_Click(object sender, EventArgs e)
        {
            Surveying.Point_Polygon_RelationClass pPR = new Surveying.Point_Polygon_RelationClass();
            string pInfo = "";
            pPR.VPolygon = m_Polygon;
            for (int i = 0; i < m_Points.Count; i++)
            {
                pPR.VPoint = m_Points[i];
                string ret = pPR.get_Relation_Point_Polygon();
                pInfo = pInfo + "点号" + i.ToString() + ": " + ret + "\r\n" + "\r\n";
            }
            Surveying.frmMessage newForm = new Surveying.frmMessage();
            newForm.Text = "射线算法";
            newForm.Info = pInfo;
            newForm.Show();
            newForm.Info = pInfo;
        }

        void btnTIN_Click(object sender, EventArgs e)
        {
            Surveying.Delaunay_Tin DT = new Surveying.Delaunay_Tin();

            DT.Points = m_Points;
            m_Triangles = DT.Generate_Tin();
            string pInfo = "";
            DrawTriangles();
            foreach(Surveying.Triangle triangle in m_Triangles)
            {
                bool flag = false;
                
                for (int i = 0; i < m_Points.Count; i++)
                {
                    if(i!=triangle.Point_1&& i != triangle.Point_2 && i != triangle.Point_3)
                    {
                        flag = DT.InnerOROut1(m_Points[triangle.Point_1], m_Points[triangle.Point_2], m_Points[triangle.Point_3], m_Points[i]);

                        if (flag == true)
                        {
                            
                            pInfo = pInfo + "点"+triangle.Point_1+ "点"+triangle.Point_2 + "点"+triangle.Point_2;
                            pInfo = pInfo + "/n";
                        }
                    }
                }
            }
            Surveying.frmMessage newForm = new Surveying.frmMessage();
            newForm.Text = "不符合空外接圆法则的点";
            newForm.Info = pInfo;
            newForm.Show();

        }

        void btnLine_Click(object sender, EventArgs e)
        {
            line_count = 0;
            queueTemp.Clear();
            geoStyle = "Line";
        }

        void btnPolygon_Click(object sender, EventArgs e)
        {
            queueTemp.Clear();
            geoStyle = "Polygon";
        }

        void btnPoint_Click(object sender, EventArgs e)
        {
            queueTemp.Clear();
            geoStyle = "Point";
        }

        void btnPolyline_Click(object sender, EventArgs e)
        {
            queueTemp.Clear();
            geoStyle = "Polyline";
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            m_Polygon = new Surveying.Polygon();
            m_Polyline = new Surveying.PolyLine();
            rightbutton = false;
            queueTemp.Clear();
            m_Points = new List<Surveying.Point>();
            m_Polygon = new Surveying.Polygon();
            m_Polyline = new Surveying.PolyLine();
            m_Line = new Surveying.Line();
            grp.Clear(picDP.BackColor);
            picDP.Refresh();
        }

        void DrawPolygon()
        {
            if (m_Polygon.point_count > 0)
            {
                int i;
                System.Drawing.Point[] points = new Point[m_Polygon.point.Length - 1];
                for (i = 0; i < m_Polygon.point.Length - 1; i++)
                {
                    points[i] = new Point();
                    points[i].X = Convert.ToInt32(m_Polygon.point[i + 1].x);
                    points[i].Y = Convert.ToInt32(picDP.Height - m_Polygon.point[i + 1].y);
                }

                grp.FillPolygon(new SolidBrush(Color.Gray), points);
                for (i = 0; i < m_Polygon.point.Length - 1; i++)
                {
                    grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polygon.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polygon.point[i + 1].y));
                }
            }
        }

        void DrawPolyLine()
        {
            if (m_Polyline.point_count > 0)
            {
                int i;
                for (i = 1; i <= m_Polyline.point_count - 1; i++)
                {
                    grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
                    grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y), Convert.ToSingle(m_Polyline.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i + 1].y));
                }
                grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
            }
        }
        void DrawPolyLine(Surveying.PolyLine polyLine)
        {
            if (polyLine.point_count > 0)
            {
                int i;
                for (i = 1; i <= polyLine.point_count - 1; i++)
                {
                grp.DrawLine(new Pen(Color.Red, 4), Convert.ToSingle(polyLine.point[i].x), Convert.ToSingle(picDP.Height - polyLine.point[i].y), Convert.ToSingle(polyLine.point[i + 1].x), Convert.ToSingle(picDP.Height - polyLine.point[i + 1].y));
                }
                
               
            }
        }


        void DrawPoints()
        {
            for (int i = 0; i < m_Points.Count; i++)
            {
                grp.DrawRectangle(new Pen(Color.Blue, 3), Convert.ToSingle(m_Points[i].x), Convert.ToSingle(picDP.Height - m_Points[i].y), 3, 3);
                grp.DrawString(Convert.ToString(i), sfont, Brushes.Blue, Convert.ToSingle(m_Points[i].x + 5), Convert.ToSingle(picDP.Height - m_Points[i].y + 5));
            }
        }

        void DrawLine()
        {
            if (m_Line!=null && m_Line.point_one != null && m_Line.point_two != null)
            {
                grp.DrawString("1", sfont, Brushes.Red, Convert.ToSingle(m_Line.point_one.x), Convert.ToSingle(picDP.Height - m_Line.point_one.y));
                grp.DrawString("2", sfont, Brushes.Red, Convert.ToSingle(m_Line.point_two.x), Convert.ToSingle(picDP.Height - m_Line.point_two.y));
                grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(m_Line.point_one.x), Convert.ToSingle(picDP.Height - m_Line.point_one.y), Convert.ToSingle(m_Line.point_two.x), Convert.ToSingle(picDP.Height - m_Line.point_two.y));
            }
        }
        
        void DrawTriangles()
        {
            if (m_Triangles != null)
            {
                double x1, x2, y1, y2,x3,y3;
                for (int i = 0; i < m_Triangles.Count; i++)
                {
                    x1 = m_Points[m_Triangles[i].Point_1].x;
                    y1 = m_Points[m_Triangles[i].Point_1].y;
                    x2 = m_Points[m_Triangles[i].Point_2].x;
                    y2 = m_Points[m_Triangles[i].Point_2].y;
                    x3 = m_Points[m_Triangles[i].Point_3].x;
                    y3 = m_Points[m_Triangles[i].Point_3].y;
                    grp.DrawLine(new Pen(Color.Red, 5), Convert.ToSingle(x1), Convert.ToSingle(picDP.Height - y1), Convert.ToSingle(x2), Convert.ToSingle(picDP.Height - y2));
                    grp.DrawLine(new Pen(Color.Red, 5), Convert.ToSingle(x1), Convert.ToSingle(picDP.Height - y1), Convert.ToSingle(x3), Convert.ToSingle(picDP.Height - y3));
                    grp.DrawLine(new Pen(Color.Red, 5), Convert.ToSingle(x3), Convert.ToSingle(picDP.Height - y3), Convert.ToSingle(x2), Convert.ToSingle(picDP.Height - y2));
                }
            }
            picDP.Image = bmp;
        }

        private void CalCoorXY(int picCoorX, int picCoorY, ref double X, ref double Y)
        {
            X = (-picCoorY + picDP.Height) * xrate;
            Y = picCoorX * yrate;
        }
        private void CalPicCoorXY(double X, double Y, ref Single picCoorX, ref Single picCoorY)
        {
            picCoorX = Convert.ToSingle(Y / yrate);
            picCoorY = Convert.ToSingle(picDP.Height - X / xrate);
        }

        /// <summary>
        /// 画多边形的MouseDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Polygon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                grp.Clear(picDP.BackColor);             

                rightbutton = false;
                Surveying.Point pointemp = new Surveying.Point();
                Surveying.Point[] p;

                //pointemp.x = -e.Y + picDP.Height;
                //pointemp.y = e.X;
                pointemp.x = e.X;
                pointemp.y = -e.Y + picDP.Height;

                queueTemp.Enqueue(pointemp.x);
                queueTemp.Enqueue(pointemp.y);
                if (queueTemp.Count >= 4)
                {
                    int i = 0;
                    p = new Surveying.Point[queueTemp.Count / 2 + 1];
                    while (queueTemp.Count > 0)
                    {
                        i = i + 1;
                        p[i] = new Surveying.Point();
                        p[i].x = Convert.ToDouble(queueTemp.Dequeue());
                        p[i].y = Convert.ToDouble(queueTemp.Dequeue());
                    }
                    for (i = 1; i <= p.Length - 1; i++)
                    {
                        queueTemp.Enqueue(p[i].x);
                        queueTemp.Enqueue(p[i].y);
                    }
                    m_Polygon = new Surveying.Polygon(p);

                    System.Drawing.Point[] points = new Point[m_Polygon.point.Length - 1];

                    for (i = 0; i < m_Polygon.point.Length - 1; i++)
                    {
                        points[i] = new Point();
                        points[i].X = Convert.ToInt32(m_Polygon.point[i + 1].x);
                        points[i].Y = Convert.ToInt32(picDP.Height - m_Polygon.point[i + 1].y);
                    }

                    grp.FillPolygon(new SolidBrush(Color.Gray), points);
                    DrawPolyLine();
                    DrawLine();
                    DrawPoints();
                    for (i = 0; i < m_Polygon.point.Length - 1; i++)
                    {
                        grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polygon.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polygon.point[i + 1].y));
                    }
                    picDP.Image = bmp;

                }
            }
            //右键结束绘图
            else
            {
                rightbutton = true;
                grp.Clear(picDP.BackColor);
                
                int i;

                System.Drawing.Point[] points = new Point[m_Polygon.point.Length - 1];
                for (i = 0; i < m_Polygon.point.Length - 1; i++)
                {
                    points[i] = new Point();
                    points[i].X = Convert.ToInt32(m_Polygon.point[i + 1].x);
                    points[i].Y = Convert.ToInt32(picDP.Height - m_Polygon.point[i + 1].y);
                }

                grp.FillPolygon(new SolidBrush(Color.Gray), points);
                DrawPolyLine();
                DrawLine();
                DrawPoints();
                for (i = 0; i < m_Polygon.point.Length - 1; i++)
                {
                    grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polygon.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polygon.point[i + 1].y));
                }

                picDP.Image = bmp;

            }
        }

        /// <summary>
        /// 画复合线的MouseDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Polyline_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                grp.Clear(picDP.BackColor);
                int i = 0;

                DrawPolygon();
                DrawLine();
                DrawPoints();

                rightbutton = false;
                Surveying.Point pointemp = new Surveying.Point();
                Surveying.Point[] p;
                pointemp.x = e.X;
                pointemp.y = -e.Y + picDP.Height;
                queueTemp.Enqueue(pointemp.x);
                queueTemp.Enqueue(pointemp.y);
                if (queueTemp.Count >= 4)
                {
                    i = 0;
                    p = new Surveying.Point[queueTemp.Count / 2 + 1];
                    while (queueTemp.Count > 0)
                    {
                        i = i + 1;
                        p[i] = new Surveying.Point();
                        p[i].x = Convert.ToDouble(queueTemp.Dequeue());
                        p[i].y = Convert.ToDouble(queueTemp.Dequeue());
                    }
                    for (i = 1; i <= p.Length - 1; i++)
                    {
                        queueTemp.Enqueue(p[i].x);
                        queueTemp.Enqueue(p[i].y);
                    }
                    m_Polyline = new Surveying.PolyLine(p);


                    for (i = 1; i <= m_Polyline.point_count - 1; i++)
                    {
                        grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
                        grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y), Convert.ToSingle(m_Polyline.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i + 1].y));
                    }
                    grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
                    picDP.Image = bmp;

                }
            }
            else
            {
                rightbutton = true;
                grp.Clear(picDP.BackColor);
                DrawPolygon();               
                DrawLine();
                DrawPoints();
                int i;
                for (i = 1; i <= m_Polyline.point_count - 1; i++)
                {
                    grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
                    grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y), Convert.ToSingle(m_Polyline.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i + 1].y));
                }
                grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
                picDP.Image = bmp;
            }
        }

        /// <summary>
        /// 画点的MouseDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Point_MouseDown(object sender, MouseEventArgs e)
        {
            Surveying.Point pointemp = new Surveying.Point();
            pointemp.x = e.X;
            pointemp.y = -e.Y + picDP.Height;
            m_Points.Add(pointemp);
            grp.Clear(picDP.BackColor);
            DrawPolygon();
            DrawPolyLine();
            DrawLine();
            DrawPoints();
            picDP.Image = bmp;
        }

        /// <summary>
        /// 画线段的MouseDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Line_MouseDown(object sender, MouseEventArgs e)
        {
            Surveying.Line temp_line;
            if (e.Button == MouseButtons.Left)
            {
                if (queueTemp.Count > 4) return;

                grp.Clear(picDP.BackColor);
                int i = 0;
                DrawPolygon();
                DrawPolyLine();
                DrawPoints();

                rightbutton = false;
                Surveying.Point pointemp = new Surveying.Point();
                Surveying.Point[] p;
                pointemp.x = e.X;
                pointemp.y = -e.Y + picDP.Height;
                queueTemp.Enqueue(pointemp.x);
                queueTemp.Enqueue(pointemp.y);
                if (queueTemp.Count >= 4)
                {
                    i = 0;
                    p = new Surveying.Point[queueTemp.Count / 2 + 1];
                    while (queueTemp.Count > 0)
                    {
                        i = i + 1;
                        p[i] = new Surveying.Point();
                        p[i].x = Convert.ToDouble(queueTemp.Dequeue());
                        p[i].y = Convert.ToDouble(queueTemp.Dequeue());
                    }
/*                    for (i = 1; i <= p.Length - 1; i++)
                    {
                        queueTemp.Enqueue(p[i].x);
                        queueTemp.Enqueue(p[i].y);
                    }*/
                    temp_line = new Surveying.Line();
                    temp_line.point_one = p[1];
                    temp_line.point_two = p[2];
                    if (line_count == 0)
                        m_Line = temp_line;
                    else
                    {
                        test_m_Line = temp_line;
                        grp.DrawString("temp_1", sfont, Brushes.Red, Convert.ToSingle(test_m_Line.point_one.x), Convert.ToSingle(picDP.Height - test_m_Line.point_one.y));
                        grp.DrawString("temp_2", sfont, Brushes.Red, Convert.ToSingle(test_m_Line.point_two.x), Convert.ToSingle(picDP.Height - test_m_Line.point_two.y));
                        grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(test_m_Line.point_one.x), Convert.ToSingle(picDP.Height - test_m_Line.point_one.y), Convert.ToSingle(test_m_Line.point_two.x), Convert.ToSingle(picDP.Height - test_m_Line.point_two.y));
                    }
                    grp.DrawString("1", sfont, Brushes.Red, Convert.ToSingle(m_Line.point_one.x), Convert.ToSingle(picDP.Height - m_Line.point_one.y));
                    grp.DrawString("2", sfont, Brushes.Red, Convert.ToSingle(m_Line.point_two.x), Convert.ToSingle(picDP.Height - m_Line.point_two.y));
                    grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(m_Line.point_one.x), Convert.ToSingle(picDP.Height - m_Line.point_one.y), Convert.ToSingle(m_Line.point_two.x), Convert.ToSingle(picDP.Height - m_Line.point_two.y));

                    line_count += 1;
                    picDP.Image = bmp;
                }
            }
        }

        /// <summary>
        /// 画多边形的MouseMove事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Polygon_MouseMove(object sender, MouseEventArgs e)
        {
            double X, Y;
            X = 0;
            Y = 0;

            labCoor.Left = e.X + 15;
            labCoor.Top = e.Y - 10;

            this.CalCoorXY(e.X, e.Y, ref X, ref Y);
            labCoor.Text = e.X.ToString("0.000") + "," + (-e.Y + picDP.Height).ToString("0.000");
            if (rightbutton == false)
            {
                if (m_Polygon.point_count >= 2)
                {
                    grp.Clear(picDP.BackColor);                  
                    int i = 0;

                    System.Drawing.Point[] points = new Point[m_Polygon.point.Length];
                    for (i = 0; i < m_Polygon.point.Length - 1; i++)
                    {
                        points[i] = new Point();
                        points[i].X = Convert.ToInt32(m_Polygon.point[i + 1].x);
                        points[i].Y = Convert.ToInt32(picDP.Height - m_Polygon.point[i + 1].y);
                    }
                    points[i] = new Point();
                    points[i].X = e.X;
                    points[i].Y = e.Y;
                    grp.FillPolygon(new SolidBrush(Color.Gray), points);
                    DrawPolyLine();
                    DrawLine();
                    DrawPoints();

                    for (i = 0; i < m_Polygon.point.Length - 1; i++)
                    {
                        grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polygon.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polygon.point[i + 1].y));
                    }

                    picDP.Image = bmp;
                }
            }
        }

        /// <summary>
        /// 画复合线的MouseMove事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Polyline_MouseMove(object sender, MouseEventArgs e)
        {
            double X, Y;
            X = 0;
            Y = 0;

            labCoor.Left = e.X + 15;
            labCoor.Top = e.Y - 10;

            this.CalCoorXY(e.X, e.Y, ref X, ref Y);
            labCoor.Text = e.X.ToString("0.000") + "," + (-e.Y + picDP.Height).ToString("0.000");
            if (rightbutton == false)
            {
                if (m_Polyline.point_count >= 2)
                {

                    grp.Clear(picDP.BackColor);
                    DrawPolygon();
                    DrawLine();
                    DrawPoints();

                    int i = 0;
                    for (i = 1; i <= m_Polyline.point_count - 1; i++)
                    {
                        grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
                        grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y), Convert.ToSingle(m_Polyline.point[i + 1].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i + 1].y));
                    }
                    grp.DrawString(Convert.ToString(i), sfont, Brushes.Red, Convert.ToSingle(m_Polyline.point[i].x), Convert.ToSingle(picDP.Height - m_Polyline.point[i].y));
                    grp.DrawLine(new Pen(Color.Blue, 2), Convert.ToSingle(m_Polyline.point[m_Polyline.point_count].x), Convert.ToSingle(picDP.Height - m_Polyline.point[m_Polyline.point_count].y), Convert.ToSingle(e.X), Convert.ToSingle(e.Y));
                    picDP.Image = bmp;
                }
            }
        }


        void Point_MouseMove(object sender, MouseEventArgs e)
        {
            double X, Y;
            X = 0;
            Y = 0;

            labCoor.Left = e.X + 15;
            labCoor.Top = e.Y - 10;

            this.CalCoorXY(e.X, e.Y, ref X, ref Y);
            labCoor.Text = e.X.ToString("0.000") + "," + (-e.Y + picDP.Height).ToString("0.000");
        }

    }
}
