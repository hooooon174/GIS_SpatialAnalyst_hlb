using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows;
namespace Surveying
{
    public class Point              //点，该结构有三个构造函数
    {
        public Double x;
        public Double y;
        public Double z;

        public Point() { }

        public Point(Double vx, Double vy, Double vz)
        {
            this.x = vx;
            this.y = vy;
            this.z = vz;
        }

        public Point(Double vx, Double vy)
        {
            this.x = vx;
            this.y = vy;
            this.z = 0;
        }

        public Double Distance(Point point2)
        {
            Double s;
            s = System.Math.Sqrt((this.x - point2.x) * (this.x - point2.x) + (this.y - point2.y) * (this.y - point2.y) + (this.z - point2.z) * (this.z - point2.z));
            return s;
        }
    }

    public class Line               //线段
    {
        public Point point_one;
        public Point point_two;
        public Double length;
        //private Point vector;  没用,之前设置的

        public Line() { }

        public Line(Point vpoint1, Point vpoint2)
        {
            this.point_one = vpoint1;
            this.point_two = vpoint2;
            this.length = 0;
            //this.vector = new Point(point_two.x - point_one.x, point_two.y - point_one.y);
            this.getLength_Line();
        }

        public void getLength_Line()
        {
            this.length = this.point_one.Distance(this.point_two);
        }
    }

    public class PolyLine                            //复合线
    {
        public Int32 point_count;                     //点数目
        public Point[] point;                         //点数组
        public Double length;                         //PolyLine的长度

        public PolyLine() { }

        public PolyLine(Point[] vpoint)
        {
            this.point_count = vpoint.Length - 1;
            this.point = vpoint;
            this.length = 0;

            this.getLength_PolyLine();
        }

        //计算PolyLine的长度
        public void getLength_PolyLine()
        {
            Double vlength = 0;
            Int32 i;
            for (i = 1; i <= this.point_count - 1; i++)
            {
                vlength = vlength + this.point[i].Distance(this.point[i + 1]);
            }
            this.length = vlength;
        }
    }

    public class Polygon               //面
    {
        public Int32 point_count;                  //顶点的个数
        public Point[] point;                      //顶点数组
        public Double area;                        //面积
        public Double perimeter;                   //周长

        public Polygon() { }

        public Polygon(Point[] vpoint)
        {
            this.point_count = vpoint.Length - 1;
            this.point = vpoint;
            this.perimeter = 0;
            this.area = 0;
        }

        //计算多边形的面积
        public void getArea_Of_Polygon()
        {
            Point[] pPoint;
            pPoint = this.point;

            Queue queueLine1 = new Queue();
            Queue queueLine2 = new Queue();
            this.Line_Chain_Polygon(ref queueLine1, ref queueLine2);
            Int32 former;
            Int32 nexter;
            Double Arealine1 = 0;
            Double Arealine2 = 0;
            former = Convert.ToInt32(queueLine1.Dequeue());
            nexter = Convert.ToInt32(queueLine1.Dequeue());
            if (pPoint[former].y < pPoint[nexter].y)
                Arealine1 = Arealine1 + System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[nexter].y - pPoint[former].y) / 2);
            else
                Arealine1 = Arealine1 - System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[former].y - pPoint[nexter].y) / 2);



            while (queueLine1.Count > 0)
            {
                former = nexter;
                nexter = Convert.ToInt32(queueLine1.Dequeue());
                if (pPoint[former].y < pPoint[nexter].y)
                    Arealine1 = Arealine1 + System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[nexter].y - pPoint[former].y) / 2);
                else
                    Arealine1 = Arealine1 - System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[former].y - pPoint[nexter].y) / 2);
            }


            former = Convert.ToInt32(queueLine2.Dequeue());
            nexter = Convert.ToInt32(queueLine2.Dequeue());
            if (pPoint[former].y < pPoint[nexter].y)
                Arealine2 = Arealine2 + System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[nexter].y - pPoint[former].y) / 2);
            else
                Arealine2 = Arealine2 - System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[former].y - pPoint[nexter].y) / 2);

            while (queueLine2.Count > 0)
            {
                former = nexter;
                nexter = Convert.ToInt32(queueLine2.Dequeue());
                if (pPoint[former].y < pPoint[nexter].y)
                    Arealine2 = Arealine2 + System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[nexter].y - pPoint[former].y) / 2);
                else
                    Arealine2 = Arealine2 - System.Math.Abs((pPoint[former].x + pPoint[nexter].x) * (pPoint[former].y - pPoint[nexter].y) / 2);
            }

            Double polygon_area;
            polygon_area = System.Math.Abs(Arealine1 - Arealine2);
            this.area = polygon_area;
        }
        //获得Polygon上下两条线段链，分别存储在队列里面
        private void Line_Chain_Polygon(ref Queue queueLine1, ref Queue queueLine2)
        {
            Queue queue = new Queue();
            Point[] pPoint;
            pPoint = this.point;

            Double xmin = pPoint[1].x;
            Double xmax = pPoint[1].x;
            Int32 xminIndex;
            xminIndex = 1;
            Int32 xmaxIndex;
            xmaxIndex = 1;
            Int32 i;
            for (i = 1; i <= this.point_count; i++)
            {
                queue.Enqueue(i);

            }
            for (i = 2; i <= this.point_count; i++)
            {
                if (pPoint[i].x < xmin)
                {
                    xmin = pPoint[i].x;
                    xminIndex = i;
                }

                if (pPoint[i].x > xmax)
                {
                    xmax = pPoint[i].x;
                    xmaxIndex = i;
                }
            }
            object temp;

            while (Convert.ToInt32(queue.Peek()) != xminIndex)
            {

                temp = queue.Dequeue();
                queue.Enqueue(temp);
            }


            while (Convert.ToInt32(queue.Peek()) != xmaxIndex)
            {
                queueLine1.Enqueue(queue.Dequeue());
            }

            queueLine1.Enqueue(queue.Dequeue());
            queueLine2.Enqueue(xminIndex);


            Stack stackTemp = new Stack();
            while (queue.Count > 0)
            {
                stackTemp.Push(queue.Dequeue());
            }
            while (stackTemp.Count > 0)
            {
                queueLine2.Enqueue(stackTemp.Pop());
            }


            queueLine2.Enqueue(xmaxIndex);

        }

        //计算多边形的周长
        public void getPerimeter_Polygon()
        {
            Double vperimeter = 0;
            Int32 i;
            for (i = 1; i <= this.point_count - 1; i++)
            {
                vperimeter = vperimeter + this.point[i].Distance(this.point[i + 1]);
            }
            vperimeter = vperimeter + this.point[1].Distance(this.point[this.point_count]);
            this.perimeter = vperimeter;
        }

        public void get_MER(int minIndex, int maxIndex)
        {
        }
    }



    public class Triangle
    {
        public Int32 Point_1;
        public Int32 Point_2;
        public Int32 Point_3;

    }
}

