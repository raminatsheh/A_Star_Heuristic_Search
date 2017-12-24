using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Robot
{
    class Program
    {
        
        static void Main(string[] args)
        {

            int StartingPointX = System.Convert.ToInt32(args[0]);
            int StartingPointY = System.Convert.ToInt32(args[1]);
            int GoalX = System.Convert.ToInt32(args[2]);
            int GoalY = System.Convert.ToInt32(args[3]);



            Polygon[] Polygons = new Polygon[12];
            Point StartingPoint = new Point(StartingPointX, StartingPointY);
            Point GoalPoint = new Point(GoalX, GoalY);
            Point EndingPoint = new Point(400, 700);
            Line CurrentLine = new Line(StartingPoint, EndingPoint); 
           
            
            int [] PointValue1 = new  int[8] {220, 616, 220, 666, 251, 670, 272, 647};
            int [] PointValue2 = new  int[8] {341, 655, 359, 667, 374, 651, 366, 577};
            int [] PointValue3 = new  int[12] {311, 530, 311, 559, 339, 578, 361, 560, 361, 528, 336, 516};
            int [] PointValue4 = new  int[10] {105, 628, 151, 670, 180, 629, 156, 577, 113, 587};
            int [] PointValue5 = new  int[8] {118, 517, 245, 517, 245, 557, 118, 557};
            int [] PointValue6 = new  int[8] {280, 583, 333, 583, 333, 665, 280, 665};
            int [] PointValue7 = new  int[6] {252, 594, 290, 562, 264, 538};
            int [] PointValue8 = new  int[6] {198, 635, 217, 574, 182, 574};
            int [] PointValue9 = new  int[8] {190, 675, 210, 675, 210, 650, 190, 645};
            int [] PointValue10 = new  int[8] {280, 540, 305, 550, 300, 510, 280, 510};
            int [] PointValue11 = new  int[6]  {230, 600, 250, 620, 240, 580};
            int [] PointValue12 = new  int[8]  {270, 680, 360, 695, 340, 675, 260, 666};



            Polygon Polygon1 = new Polygon(8, PointValue1);
            Polygon Polygon2 = new Polygon(8, PointValue2);
            Polygon Polygon3 = new Polygon(12, PointValue3);
            Polygon Polygon4 = new Polygon(10, PointValue4);
            Polygon Polygon5 = new Polygon(8, PointValue5);
            Polygon Polygon6 = new Polygon(8, PointValue6);
            Polygon Polygon7 = new Polygon(6, PointValue7);
            Polygon Polygon8 = new Polygon(6, PointValue8);
            Polygon Polygon9 = new Polygon(8, PointValue9);
            Polygon Polygon10 = new Polygon(8, PointValue10);
            Polygon Polygon11 = new Polygon(6, PointValue11);
            Polygon Polygon12 = new Polygon(8, PointValue12);

            Line testline = new Line(Polygon6.Points[2], Polygon1.Points[2]);

            Console.WriteLine(Lines_Not_Intersect(testline, Polygon12.Lines[3]));

            Polygons[0]= Polygon1;
            Polygons[1]= Polygon2;
            Polygons[2]= Polygon3;
            Polygons[3]= Polygon4;
            Polygons[4]= Polygon5;
            Polygons[5]= Polygon6;
            Polygons[6]= Polygon7;
            Polygons[7]= Polygon8;
            Polygons[8]= Polygon9;
            Polygons[9]= Polygon10;
            Polygons[10]= Polygon11;
            Polygons[11]= Polygon12;

            ArrayList Children = new ArrayList();

            //FindChildren(StartingPoint, Polygons, ref Children, GoalPoint);

            //Start the main Program

            //Define OpenSet and ClosedSet (Arraylists)
            ArrayList OpenSet = new ArrayList();
            ArrayList ClosedSet = new ArrayList();

            //Populate the OpenSet with the existing points from each Polygon

            OpenSet.Add(StartingPoint);
            // change the cost for the starting point to 0
            StartingPoint.Costg = 0;
            //Define Points x and y
            Point x;


            while (OpenSet.Count > 0)
            {
                //remove the leftmost node from open, call it X; Or actually remove the point with lowest cost
                x = (Point)OpenSet[0];

                foreach (Point P in OpenSet)
                {
                    if (x.Cost > P.Cost)
                    {
                        x = P;
                    }
                }


                OpenSet.Remove(x);
	            // if X is a goal then return(success);
                if (x == GoalPoint)
                {
                    Console.Write("Total Cost is :  ");
                    Console.WriteLine(x.Costg);
                    ReconstructPath(x, StartingPoint);
                    Console.Write(StartingPoint.x);
                    Console.Write("  ");
                    Console.WriteLine(StartingPoint.y);
                    break;
                }
	            // generate all children of X;
                FindChildren(x, Polygons, ref Children, GoalPoint);
                FindChildrenOnSamePolyogn(x, Polygons, ref Children);

	            // add X to closed;
                ClosedSet.Add(x);

                // Create a new Array list to manipulaet the children
                ArrayList Children2 = new ArrayList();
                Children2.Clear();

                for (int temp = 0; temp < Children.Count; temp++)
                {
                    for (int temp2 = 0; temp2 < ClosedSet.Count; temp2++)
                    {
                            Point P2 = (Point)Children[temp];
                            if (P2 == (Point)ClosedSet[temp2])
                            {
                                //Children2.Remove(P2);
                                ((Point)Children[temp]).Discard = true;
                            }
                    }
                }

                // discard any children of X that is already in open or closed;
                for (int temp = 0; temp < Children.Count; temp++ )
                {
                    for (int temp2 = 0; temp2 < OpenSet.Count; temp2++ )
                    {
                            Point P1 = (Point)Children[temp];
                            if (P1 == (Point)OpenSet[temp2])
                            {
                                ((Point)Children[temp]).Discard = true;
                                double temp3 = x.Costg + GetDistanceG(x, P1) + GetDistanceG(P1, GoalPoint);
                                if (P1.Cost > temp3)
                                {
                                    P1.Costh = GetDistanceG(P1, GoalPoint);
                                    P1.Parent = x;
                                    P1.Costg = x.Costg+GetDistanceG(x,P1);
                                    P1.Cost = P1.Costg + P1.Costh;
                                }

                            }
                    }

                }


 
                for (int temp = 0; temp < Children.Count; temp++)
                {
                    if (!((Point)Children[temp]).Discard)
                    {
                        Children2.Add((Point)Children[temp]);
                    }

                }

                Children.Clear();
                Children = Children2;
                


                // Calculate the Cost of each child and assign Parent
                foreach (Point P in Children)
                {
                    P.Costh = GetDistanceG(P, GoalPoint);
                    P.Parent = x;
                    P.Costg = x.Costg+GetDistanceG(x,P);
                    P.Cost = P.Costg + P.Costh;
                }

	            /* add remaining children of X, in order of discovery (According to Cost), 
	             to the right end of open, then clear Children */

                foreach (Point P in Children)
                {
                    //if (!P.Discard)
                    //{
                        OpenSet.Add(P);
                    //}
                }

                Children.Clear();
                Children2.Clear();

            }


            


        }

        static bool IsPointOnPolygon(Point Po, Polygon P1)
        {
            for (int temp = 0; temp < P1.Points.Length; temp++)
            {
                if ((Po.x == P1.Points[temp].x) && (Po.y == P1.Points[temp].y))
                {
                    return true;
                }
            }
            return false;
        }

        static void ReconstructPath(Point P, Point StartingPoint)
        {
            int temp = 0;
            while (P != StartingPoint)
            {
                if (temp == 0) 
                { 
                    Console.WriteLine("Path is: ");
                    temp = 1;
                }
                Console.Write(P.x);
                Console.Write("  ");
                Console.WriteLine(P.y);
                P = P.Parent;
            }
        }

        static bool IsPointANeighbor(Point P1, Point P2, Polygon [] Polygons)
        {
            foreach (Polygon P in Polygons)
            {
                if (IsPointOnPolygon(P1, P))
                {
                    if (IsPointOnPolygon(P2, P))
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        static bool Lines_Not_Intersect(Line Line1, Line Line2)
        {
            int p1x = Line1.StartPoint.x;
            int p1y = Line1.StartPoint.y;
            int p2x = Line1.EndPoint.x;
            int p2y = Line1.EndPoint.y;
            int q1x = Line2.StartPoint.x;
            int q1y = Line2.StartPoint.y;
            int q2x = Line2.EndPoint.x;
            int q2y = Line2.EndPoint.y;

            int k1 = p1x - p2x;
            int k2 = q2y - q1y;
            int k3 = p1y - p2y;
            int k4 = q2x - q1x;
            int k5 = p1x - q1x;
            int k6 = p1y - q1y;

            double d  = (k1 * k2) - (k3 * k4);

            double a  = ((k2 * k5) - (k4 * k6)) / d;
            double b = ((k1 * k6) - (k3 * k5)) / d;

            //if (d == 0) return false;
            if ((a < 1) && (a > 0))
            {
                if ((b < 1) && (b > 0))
                {
                    return false;
                }
            }
            return true;
            
        }

        static double GetDistanceG(Point P1, Point P2)
        {
            double Distance = System.Math.Sqrt(Math.Pow((P1.x - P2.x), 2) + Math.Pow((P1.y - P2.y), 2));
            return Distance;
        }

        static void FindChildren(Point Point1, Polygon [] Polygons_Under_Test, ref ArrayList x, Point GoalPoint)
        {
            Line CurrentLine = new Line(Point1, Point1);
            Polygon[] TempPolygons = Polygons_Under_Test;

            foreach (Polygon P in Polygons_Under_Test)
            {
                for (int tempPoint = 0; tempPoint < P.Points.Length; tempPoint++)
                {
                    CurrentLine.EndPoint = P.Points[tempPoint];
                    bool interset_Status = false;
                    if (!IsPointANeighbor(CurrentLine.EndPoint, CurrentLine.StartPoint, Polygons_Under_Test))
                    {

                        foreach (Polygon P2 in TempPolygons)
                        {


                            for (int tempLine = 0; tempLine < P2.Lines.Length; tempLine++)
                            {

                                if (Lines_Not_Intersect(P2.Lines[tempLine], CurrentLine))
                                {
                                    if (!interset_Status)
                                    {
                                        interset_Status = false;
                                    }
                                }
                                else
                                {
                                    interset_Status = true;
                                }


                            }
                        }



                    }
                    else
                    {
                        interset_Status = true;
                    }
                    if (!interset_Status)
                    {
                        x.Add(CurrentLine.EndPoint);
  

                    }


                }

            }            
            if (!FindIfGoalIsVisibleFromPoint(Point1, GoalPoint, Polygons_Under_Test))
            {
                x.Add(GoalPoint);
            }
            
        }
        static void FindChildrenOnSamePolyogn(Point Point1, Polygon[] Polygons_Under_Test, ref ArrayList x)
        {
            int index = 0;
            foreach (Polygon P in Polygons_Under_Test)
            {
                if (IsPointOnPolygon(Point1, P))
                {
                    for (int temp = 0; temp < P.Points.Length; temp++)
                    {
                        if ((Point1.x == P.Points[temp].x) && (Point1.y == P.Points[temp].y))
                        {
                            index = temp;
                        }
                    }
                    if (index == 0)
                    {
                        //Console.Write(P.Points[index+1].x);
                        //Console.Write("   ");
                        //Console.WriteLine(P.Points[index+1].y);
                        //Console.Write(P.Points[P.Points.Length-1].x);
                        //Console.Write("   ");
                        //Console.WriteLine(P.Points[P.Points.Length - 1].y);
                        x.Add(P.Points[index+1]);
                        x.Add(P.Points[P.Points.Length-1]);
                    }
                    else if (index == P.Points.Length - 1)
                    {
                        //Console.Write(P.Points[index-1].x);
                        //Console.Write("   ");
                        //Console.WriteLine(P.Points[index-1].y);
                        //Console.Write(P.Points[0].x);
                        //Console.Write("   ");
                        //Console.WriteLine(P.Points[0].y);
                        x.Add(P.Points[index-1]);
                        x.Add(P.Points[0]);
                    }
                    else
                    {
                        x.Add(P.Points[index - 1]);
                        x.Add(P.Points[index + 1]);
                    }
                }
            }

        }
        static bool FindIfGoalIsVisibleFromPoint(Point Point1, Point GoalPoint, Polygon[] Polygons_Under_Test)
        {
            Line CurrentLine = new Line(Point1, GoalPoint);
            bool intersect = false;
            foreach (Polygon x in Polygons_Under_Test)
            {
                for (int templine = 0; templine < x.Lines.Length; templine++)
                {
                    if (Lines_Not_Intersect(x.Lines[templine], CurrentLine))
                    {
                        if (!intersect)
                        {
                            intersect = false;
                        }
                    }
                    else
                    {
                        intersect = true;
                    }
                }
            }
            return intersect;
        }
    }

    public class Polygon 
    {
        public int My_Number_of_Points;
        public Point[] Points;
        public Line[] Lines;

        public Polygon(int Numberofpoints, int[] Points_Of_This_Polygon )
        {
            this.My_Number_of_Points = Numberofpoints;
            this.Points = new Point[Numberofpoints/2];
            this.Lines = new Line[Numberofpoints/2];
            int temp1 = 0;
            for (int temp = 0; temp < Numberofpoints/2; temp++)
            {
                this.Points[temp] = new Point(Points_Of_This_Polygon[temp1], Points_Of_This_Polygon[temp1 + 1]);
                temp1 = temp1+2;
            }

            this.Lines[0] = new Line(this.Points[Numberofpoints/2-1], this.Points[0]);
          
            for (int temp = 1; temp < Numberofpoints / 2; temp++)
            {
                this.Lines[temp] = new Line(this.Points[temp-1], this.Points[temp]);
            }

        }

    }

    public class Point
    {
        public int x;
        public int y;
        
        public double Cost = 9999999999999999999;
        public double Costh = 9999999999999999999;
        public double Costg = 9999999999999999999;
        public Point Parent;
        public bool Discard = false;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }



    }

    public class Line
    {
        public Point StartPoint;
        public Point EndPoint;

        public Line(Point StartPoint, Point EndPoint)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
        }
    }


}
