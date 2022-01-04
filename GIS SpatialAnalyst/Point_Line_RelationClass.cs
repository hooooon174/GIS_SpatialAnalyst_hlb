using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Surveying
{
    /// <summary>
    /// 点和线段关系判断：右手法则
    /// </summary>
    public class Point_Line_RelationClass
    {
        private Surveying.Point m_VPoint;
        private Surveying.Point m_LinePoint_1;
        private Surveying.Point m_LinePoint_2;

        public Surveying.Point VPoint
        {
            set { m_VPoint = value; }
        }
        public Surveying.Point LinePoint_1
        {
            set { m_LinePoint_1 = value; }
        }
        public Surveying.Point LinePoint_2
        {
            set { m_LinePoint_2 = value; }
        }

        public string RelationCal()
        {
            double H = m_VPoint.x * (m_LinePoint_1.y - m_LinePoint_2.y) - m_VPoint.y * (m_LinePoint_1.x - m_LinePoint_2.x) + m_LinePoint_1.x * m_LinePoint_2.y - m_LinePoint_2.x * m_LinePoint_1.y;
            string ret = "";
            if (H > 0)
            {
                if (m_LinePoint_1.y < m_LinePoint_2.y)
                {
                    ret = "三个点成逆时针排列！参考点在线段左侧！";
                    if (m_VPoint.y <= m_LinePoint_2.y && m_VPoint.y >= m_LinePoint_1.y)
                    {
                        ret = ret + "在参考点正X射线方向上存在1个交点！";
                    }
                    else
                    {
                        ret = ret + "在参考点正X射线方向上没有交点！";
                    }
                }
                else
                {
                    ret = "三个点成逆时针排列！参考点在线段左侧！在参考点正X射线方向上没有交点！";
                }
            }
            else if (H < 0)
            {

                if (m_LinePoint_1.y > m_LinePoint_2.y)
                {
                    ret = "三个点成顺时针排列！参考点在线段右侧！";
                    if (m_VPoint.y >= m_LinePoint_2.y && m_VPoint.y <= m_LinePoint_1.y)
                    {
                        ret = ret + "在参考点正X射线方向上存在1个交点！";
                    }
                    else
                    {
                        ret = ret + "在参考点正X射线方向上没有交点！";
                    }
                }
                else
                {
                    ret = "三个点成顺时针排列！参考点在线段右侧！在参考点正X射线方向上没有交点！";
                }
            }

            return ret;
        }

        public int get_Relation_Point_Line(Surveying.Point vp, Surveying.Point lp1, Surveying.Point lp2)
        {
            double p = vp.x * (lp1.y - lp2.y) - vp.y * (lp1.x - lp2.x) + lp1.x * lp2.y - lp2.x * lp1.y;
            if (p > 0)
            {
                return 1;
            }
            else if (p < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// 点和面关系判断：射线算法
    /// </summary>
    public class Point_Polygon_RelationClass
    {
        private Surveying.Point m_VPoint;
        private Surveying.Polygon m_Polygon;
        private Surveying.Point[] m_Points;


        public Surveying.Point VPoint
        {
            set { m_VPoint = value; }
        }

        public Surveying.Polygon VPolygon
        {
            set
            {
                m_Polygon = value;
                m_Points = m_Polygon.point;
                m_Points[0] = m_Points[m_Points.Length - 1];
            }
        }
        public string get_Relation_Point_Polygon()
        {
            int point_count = this.m_Points.Length - 1;
            int flag = -1;
            string ret = "";

            for (int i = 0; i <= point_count - 1; i++)
            {   

                if ((this.m_Points[i].x == this.m_VPoint.x)
                    || ((this.m_Points[i].x < this.m_VPoint.x) && (this.m_Points[i + 1].x > this.m_VPoint.x))
                    || ((this.m_Points[i].x > this.m_VPoint.x) && (this.m_Points[i + 1].x < this.m_VPoint.x)))
                {
                    double p = this.m_VPoint.x * (this.m_Points[i].y - this.m_Points[i + 1].y) - this.m_VPoint.y * 
                        (this.m_Points[i].x - this.m_Points[i + 1].x) + this.m_Points[i].x *
                        this.m_Points[i + 1].y - this.m_Points[i + 1].x * this.m_Points[i].y;

                    if (this.m_Points[i].x < this.m_Points[i + 1].x)
                    {
                        if (p == 0) { ret = ret + "该点在多边形边界"; return ret; }
                        else if (p > 0) { flag *= -1; }
                    }
                    else
                    {
                        if (p == 0) { ret = ret + "该点在多边形边界"; return ret; }
                        else if (p < 0) { flag *= -1; }
                    }
                }
            }
            if (flag == -1)
                ret = ret + "该点不在多边形内部";
            else
                ret = ret + "该点在多边形内部";
            return ret;
        }
    }
    /// <summary>
    /// 线和线关系判断：矢量外积算法
    /// </summary>
    public class Line_Line_RelationClass
    {
        private Surveying.Line first_Line;
        private Surveying.Line last_Line;

        public Line_Line_RelationClass() { }

        public Line_Line_RelationClass(Line First_Line, Line Last_Line)
        {
            this.first_Line = First_Line;
            this.last_Line = Last_Line;
        }

        public Surveying.Line FLine
        {
            set { first_Line = value; }
        }

        public Surveying.Line LLine
        {
            set { last_Line = value; }
        }

        public int get_Relation_Line_Line()
        {
            int flag = 0;
            Surveying.Point A = first_Line.point_one;
            Surveying.Point B = first_Line.point_two;
            Surveying.Point C = last_Line.point_one;
            Surveying.Point D = last_Line.point_two;

            Surveying.Point vAB = new Surveying.Point(B.x - A.x, B.y - A.y);
            Surveying.Point vAD = new Surveying.Point(D.x - A.x, D.y - A.y);
            Surveying.Point vAC = new Surveying.Point(C.x - A.x, C.y - A.y);
            Surveying.Point vCB = new Surveying.Point(B.x - C.x, B.y - C.y);
            Surveying.Point vCD = new Surveying.Point(D.x - C.x, D.y - C.y);

            //((𝐴𝐵) ⃗×(𝐴𝐷) ⃗ )∗((𝐴𝐵) ⃗×(𝐴𝐶) ⃗ )≤0
            double AB_x_AD = vAB.x * vAD.y - vAD.x * vAB.y;
            double AB_x_AC = vAB.x * vAC.y - vAC.x * vAB.y;
            //((𝐶𝐷) ⃗×(𝐶𝐴) ⃗ )∗((𝐶𝐷) ⃗×(𝐶𝐵) ⃗ )≤0
            double CD_x_CA = -vCD.x * vAC.y + vAC.x * vCD.y;
            double CD_x_CB = vCD.x * vCB.y - vCB.x * vCD.y;

            if (AB_x_AD * AB_x_AC <= 0 && CD_x_CA * CD_x_CB <= 0)
                flag = 1;
            else flag = 0;
            return flag;
        }


        public Point Line_Calculate_intersection()
        {
            Point intersection = new Point();
            //ab  first_Line 
            //cd  last_Line
            double area_cda = (last_Line.point_one.x - first_Line.point_one.x) * (last_Line.point_two.y - first_Line.point_one.y) - (last_Line.point_one.y - first_Line.point_one.y) * (last_Line.point_two.x - first_Line.point_one.x);
            // 三角形cdb 面积可用其他三个三角形的面积求得，这里考虑了各个三角形面积的正负号
            double area_abc = (first_Line.point_one.x - last_Line.point_one.x) * (first_Line.point_two.y - last_Line.point_one.y) - (first_Line.point_one.y - last_Line.point_one.y) * (first_Line.point_two.x - last_Line.point_one.x);
            double area_abd = (first_Line.point_one.x - last_Line.point_two.x) * (first_Line.point_two.y - last_Line.point_two.y) - (first_Line.point_one.y - last_Line.point_two.y) * (first_Line.point_two.x - last_Line.point_two.x);
            double area_cdb = area_cda + area_abc - area_abd;
            double t = area_cda / (area_abd - area_abc);
            double dx = t * (first_Line.point_two.x - first_Line.point_one.x);
            double dy = t * (first_Line.point_two.y - first_Line.point_one.y);
            intersection.x = first_Line.point_one.x + dx;
            intersection.y = first_Line.point_one.y + dy;
            return intersection;
        }
    }


    /// <summary>
    /// 面裁剪线
    /// </summary>
    public class Polygon_clip_Line
    {
        private PolyLine m_Polyline;
        private Polygon m_Polygon;



        public Polygon_clip_Line() { }

        public Polygon_clip_Line(PolyLine Polyline, Polygon Polygon)
        {

            this.m_Polygon = Polygon;
            this.m_Polyline = Polyline;
        }


        public Surveying.PolyLine Polylin
        {
            set { m_Polyline = value; }
        }

        public Surveying.Polygon Polygon
        {
            set { m_Polygon = value; }
        }

        /// <summary>
        /// 计算多变形与复合线的交点，返回交点，参数all_points为所有需要保留的点
        /// </summary>
        /// <param name="all_points"></param>
        /// <returns></returns>
        private List<Point> Calculate_intersection(ref List<Point> all_points)
        {
            List<Point> inter_points = new List<Point>();
            //计算polyline与polygon的交点
            // 1.判断两条线是否相交  2.若相交则计算交点
            Line polygon_line = new Line();
            Line polyline_line = new Line();
            Point t_point;
            m_Polygon.point[0] = m_Polygon.point[m_Polygon.point.Length - 1];  //连接多边形最后一个点和第一个点
            int k;
            for (k=1; k < m_Polyline.point_count; k++)
            {
                polyline_line.point_one = m_Polyline.point[k];
                polyline_line.point_two = m_Polyline.point[k + 1];
                int one_in_or_not = -1;//判断线段起点是否在多边形内部
                one_in_or_not = this.Point_Polygon_Re(polyline_line.point_one, m_Polygon);
                if (one_in_or_not != 0)  //起点在内部则加入要保留的点数组
                    all_points.Add(polyline_line.point_one);
                for (int i = 0; i <= m_Polygon.point.Length - 2; i++)
                {
                    polygon_line.point_one = m_Polygon.point[i];
                    polygon_line.point_two = m_Polygon.point[i + 1];
                    //判断是否存在交点
                    Line_Line_RelationClass LLR = new Line_Line_RelationClass(polygon_line, polyline_line);
                    int flag = LLR.get_Relation_Line_Line();
                    if (flag == 1)
                    {
                        //有交点，计算交点。
                        t_point = LLR.Line_Calculate_intersection();
                        inter_points.Add(t_point);
                        all_points.Add(t_point);
                    }
                    else continue;
                }
            }
            int two_in_or_not = -1;//判断复合线最后一个点是否在多边形内部
            two_in_or_not = this.Point_Polygon_Re(polyline_line.point_two, m_Polygon);
            if (two_in_or_not == 1 && k == m_Polyline.point_count )
            {
                all_points.Add(polyline_line.point_two);
            }
            return inter_points;
        }

        /// <summary>
        /// 点与多边形关系
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private int Point_Polygon_Re(Point point, Polygon polygon)
        {
            int flag = -1;
            int ret;
            polygon.point[0] = polygon.point[polygon.point_count];
            for (int i = 0; i <= polygon.point_count - 1; i++)
            {
                if ((polygon.point[i].x == point.x)
                    || ((polygon.point[i].x < point.x) && (polygon.point[i + 1].x > point.x))
                    || ((polygon.point[i].x > point.x) && (polygon.point[i + 1].x < point.x)))
                {
                    double p = point.x * (polygon.point[i].y - polygon.point[i + 1].y) - point.y * (polygon.point[i].x - polygon.point[i + 1].x) + polygon.point[i].x * polygon.point[i + 1].y - polygon.point[i + 1].x * polygon.point[i].y;

                    if (polygon.point[i].x < polygon.point[i + 1].x)
                    {
                        if (p == 0)
                        { //ret = ret + "该点在多边形边界"; 
                            ret = 2;
                            return ret;
                        }
                        else if (p > 0) { flag *= -1; }
                    }
                    else
                    {
                        if (p == 0)
                        {
                            ret = 2;
                            //ret = ret + "该点在多边形边界"; 
                            return ret;
                        }
                        else if (p < 0) { flag *= -1; }
                    }
                }
            }
            if (flag == -1)
                ret = 0;
            //ret = ret + "该点不在多边形内部";
            else
                ret = 1;
            //ret = ret + "该点在多边形内部";
            return ret;
        }

        public List<PolyLine> Clip()
        {
            List<Point> all_points = new List<Point>();
            List<Point> inter_points = new List<Point>();
            inter_points = this.Calculate_intersection(ref all_points);
            List<PolyLine> polyLines = new List<PolyLine>();
            List<Point> temp_point = new List<Point>();
            Point[] temp_points;
            temp_point.Add(null);
            PolyLine temp_polyLine;
            for (int i = 0; i < all_points.Count - 1; i++)
            {
                temp_point.Add(all_points[i]); //添加要保留的第一个点到当前复合线的点集中
                int flag = 0;  //flag表示下一个点是否为原复合线的点
                for (int k = 1; k < m_Polyline.point_count; k++)
                {
                    if (all_points[i+1] == m_Polyline.point[k])
                    {
                        flag = 1; 
                    }
                }
                //如果下一个点为为计算出的交点，则当前复合线创建完毕。
                if(flag != 1)
                {
                    temp_point.Add(all_points[i+1]);//添加当前线的终点
                    i++;      //结束当前复合线线,并生成复合线添加到list中
                    temp_points = temp_point.ToArray();
                    temp_polyLine = new PolyLine(temp_points);
                    temp_point.Clear();
                    temp_point.Add(null);
                    polyLines.Add(temp_polyLine);
                }
            }
            return polyLines;
        }
    }
    public class Delaunay_Tin
    {
        private List<Surveying.Point> m_Points;
        //private List<Surveying.Point> left_Points;
        //private Surveying.Line m_Line;
        private List<Surveying.Triangle> m_Triangles = new List<Surveying.Triangle>();

        public List<Surveying.Point> Points
        {
            set
            {
                m_Points = value;
                //left_Points = value;
            }
        }

        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public double Distance(Point first, Point second)
        {
            double dis;
            dis = Math.Sqrt((second.y - first.y) * (second.y - first.y) + (second.x - first.x) * (second.x - first.x));
            return dis;
        }
        /// <summary>
        /// 右手法则，返回值为-1表示点在直线右侧
        /// </summary>
        /// <param name="vp"></param>
        /// <param name="lp1"></param>
        /// <param name="lp2"></param>
        /// <returns></returns>
        private int get_Relation_Point_Line(Surveying.Point vp, Surveying.Point lp1, Surveying.Point lp2)
        {
            double p = vp.x * (lp1.y - lp2.y) - vp.y * (lp1.x - lp2.x) + lp1.x * lp2.y - lp2.x * lp1.y;
            if (p > 0)
            {
                return 1;
            }
            else if (p < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// //三个点形成的三角形，vp为顶点对应的夹角，返回其夹角
        /// </summary>
        /// <param name="vp"></param>
        /// <param name="lp1"></param>
        /// <param name="lp2"></param>
        /// <returns></returns>
        private double Angle(Surveying.Point vp, Surveying.Point lp1, Surveying.Point lp2)
        {

            double dx1, dx2, dy1, dy2;
            double angle;
            dx1 = lp1.x - vp.x;//x1-x3
            dy1 = lp1.y - vp.y;//y1-y3
            dx2 = lp2.x - vp.x;//x2-x3
            dy2 = lp2.y - vp.y;//y2-y3
            double c = (double)Math.Sqrt(dx1 * dx1 + dy1 * dy1) * (double)Math.Sqrt(dx2 * dx2 + dy2 * dy2);
            if (c == 0) return -1;
            angle = (double)Math.Acos((dx1 * dx2 + dy1 * dy2) / c);
            return angle;
        }
        /// <summary>
        /// 找到直线右边且角度最大的点，返回值为-1时表示未找到
        /// </summary>
        /// <param name="vp"></param>
        /// <param name="m_Points"></param>
        /// <returns></returns>
        private int Find_right_Point(List<Surveying.Point> m_Points, Surveying.Point lp1, Surveying.Point lp2)
        {

            double angle1;
            double max_angle = -1;
            int max_angle_index = -1;

            for (int element = 0; element < m_Points.Count; element++)  //T的类型与mList声明时一样
            {
                int direction = get_Relation_Point_Line(m_Points[element], lp1, lp2);
                if (direction == -1)
                {
                    angle1 = Angle(m_Points[element], lp1, lp2);
                    if (angle1 > max_angle)
                    {
                        max_angle = angle1;
                        max_angle_index = element;
                    }
                }
            }
            return max_angle_index;
        }


        public List<Surveying.Triangle> Generate_Tin()
        {
            double dis = 99999;
            int min_index = 0;
            int start_index = 0;
            double temp;
            //不能选择第0.1个作为起点，要算与起点距离最近的点
            //起点也不能为0，存在起点与最近点右边没有点的情况
            //存储一个平均x，y，找到与平均x，y最近的点
            Point average_Point = new Surveying.Point();
            double average_x = 0;
            double average_y = 0;

            for (int i = 0; i < m_Points.Count; i++)
            {
                average_x = average_x + m_Points[i].x;  //懒得写变量了，就用average把
                average_y = average_y + m_Points[i].y;
            }
            average_x = average_x / m_Points.Count;
            average_y = average_y / m_Points.Count;
            average_Point.x = average_x;
            average_Point.y = average_y;
            //懒得写成函数了
            for (int i = 0; i < m_Points.Count; i++)
            {
                temp = Distance(average_Point, m_Points[i]);
                if (temp < dis)
                {
                    dis = temp;
                    start_index = i;
                }
            }
            dis = 99999; //给dis恢复一下最大值
            for (int i = 0; i < m_Points.Count; i++)
            {
                //这里写个if判断，防止起点终点重叠
                if (i != start_index)
                {
                    temp = Distance(m_Points[start_index], m_Points[i]);
                    if (temp < dis)
                    {
                        dis = temp;
                        min_index = i;
                    }
                }
            }
            this.Recursive_growth(start_index, min_index, m_Points);
            return m_Triangles;

        }

        //递归，输入为两个点，一个POINT列表，一次执行生成一个三角形
        //根据新生成的三角形进入两次递归，递归结束条件为线的右边没有点
        private int Recursive_growth(int p_1_index, int p_2_index, List<Surveying.Point> m_Points)
        {
            for (int index = 0; index < m_Triangles.Count; index++)
            {
                if (m_Triangles[index].Point_1 == p_1_index && m_Triangles[index].Point_2 == p_2_index)
                {
                    return 0;
                }
            }
            int max_angle_index = -1;
            Surveying.Triangle temp_triangle = new Surveying.Triangle();
            temp_triangle.Point_1 = p_1_index;
            temp_triangle.Point_2 = p_2_index;
            max_angle_index = Find_right_Point(m_Points, m_Points[p_1_index], m_Points[p_2_index]);



            if (max_angle_index != -1)
            {

                temp_triangle.Point_3 = max_angle_index;
                this.m_Triangles.Add(temp_triangle);
                Recursive_growth(temp_triangle.Point_1, temp_triangle.Point_3, m_Points);
                Recursive_growth(temp_triangle.Point_3, temp_triangle.Point_2, m_Points);
            }
            return 0;
        }



        /// <summary>
        /// 判断点 Ｐ 是否在圆内,Vrtx为验证点
        /// </summary>
        /// <param name="Vrtx0"></param>
        /// <param name="Vrtx1"></param>
        /// <param name="Vrtx2"></param>
        /// <param name="Vrtx"></param>
        /// <returns></returns>
        public bool InnerOROut1(Point Vrtx0, Point Vrtx1, Point Vrtx2, Point Vrtx)
        {
            double Radius_2;  // 半径的平方
            double Cntrx, Cntry; //圆心坐标

            // 求圆心和半径
            double a1 = (Vrtx1.x - Vrtx0.x), b1 = (Vrtx1.y - Vrtx0.y);
            double c1 = (a1 * a1 + b1 * b1) / 2.0;
            double a2 = (Vrtx2.x - Vrtx0.x), b2 = (Vrtx2.y - Vrtx0.y);
            double c2 = (a2 * a2 + b2 * b2) / 2.0;
            double d = (a1 * b2 - a2 * b1);
            Cntrx = Vrtx0.x + (c1 * b2 - c2 * b1) / d;
            Cntry = Vrtx0.y + (a1 * c2 - a2 * c1) / d;
            Radius_2 = Math.Pow(Vrtx0.x - Cntrx, 2.0) + Math.Pow(Vrtx0.y - Cntry, 2.0);

            // 判断 Vrtx0 是否在外接圆内或其上，若是，返回 true,否则，返回 false
            double Rad_V0Cntr;
            Rad_V0Cntr = Math.Pow(Vrtx.x - Cntrx, 2.0) + Math.Pow(Vrtx.y - Cntry, 2.0);
            if (Rad_V0Cntr <= Radius_2)
            {
                return true;
                
            }
            else
            {
                return false;
            }
        }

    }
}


