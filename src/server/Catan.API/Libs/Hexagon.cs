// Generated code -- CC0 -- No Rights Reserved -- http://www.redblobgames.com/grids/hexagons/

using System;
using System.Collections.Generic;

namespace Catan.API.Libs
{
    public struct Point
    {
        public readonly double X;
        public readonly double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public struct Hex
    {
        public readonly int Q;
        public readonly int R;
        public readonly int S;

        public Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;

            if (q + r + s != 0)
                throw new ArgumentException("q + r + s must be 0");
        }

        public Hex Add(Hex b)
        {
            return new Hex(Q + b.Q, R + b.R, S + b.S);
        }


        public Hex Subtract(Hex b)
        {
            return new Hex(Q - b.Q, R - b.R, S - b.S);
        }


        public Hex Scale(int k)
        {
            return new Hex(Q * k, R * k, S * k);
        }


        public Hex RotateLeft()
        {
            return new Hex(-S, -Q, -R);
        }


        public Hex RotateRight()
        {
            return new Hex(-R, -S, -Q);
        }

        public static List<Hex> Directions = new List<Hex>
        {
            new Hex(1, 0, -1),
            new Hex(1, -1, 0),
            new Hex(0, -1, 1),
            new Hex(-1, 0, 1),
            new Hex(-1, 1, 0),
            new Hex(0, 1, -1)
        };

        public static Hex Direction(int direction)
        {
            return Directions[direction];
        }


        public Hex Neighbor(int direction)
        {
            return Add(Direction(direction));
        }

        public static List<Hex> Diagonals = new List<Hex>
        {
            new Hex(2, -1, -1),
            new Hex(1, -2, 1),
            new Hex(-1, -1, 2),
            new Hex(-2, 1, 1),
            new Hex(-1, 2, -1),
            new Hex(1, 1, -2)
        };

        public Hex DiagonalNeighbor(int direction)
        {
            return Add(Diagonals[direction]);
        }


        public int Length()
        {
            return (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;
        }


        public int Distance(Hex b)
        {
            return Subtract(b).Length();
        }

    }

    public struct FractionalHex
    {
        public readonly double Q;
        public readonly double R;
        public readonly double S;

        public FractionalHex(double q, double r, double s)
        {
            if (Math.Round(q + r + s) != 0) throw new ArgumentException("q + r + s must be 0");

            Q = q;
            R = r;
            S = s;
        }

        public Hex HexRound()
        {
            var qi = (int)Math.Round(Q);
            var ri = (int)Math.Round(R);
            var si = (int)Math.Round(S);
            var qDiff = Math.Abs(qi - Q);
            var rDiff = Math.Abs(ri - R);
            var sDiff = Math.Abs(si - S);
            if (qDiff > rDiff && qDiff > sDiff)
            {
                qi = -ri - si;
            }
            else if (rDiff > sDiff)
            {
                ri = -qi - si;
            }
            else
            {
                si = -qi - ri;
            }

            return new Hex(qi, ri, si);
        }

        public FractionalHex HexLerp(FractionalHex b, double t)
        {
            return new FractionalHex(Q * (1.0 - t) + b.Q * t, R * (1.0 - t) + b.R * t, S * (1.0 - t) + b.S * t);
        }

        public static List<Hex> HexLinedraw(Hex a, Hex b)
        {
            var n = a.Distance(b);
            var aNudge = new FractionalHex(a.Q + 0.000001, a.R + 0.000001, a.S - 0.000002);
            var bNudge = new FractionalHex(b.Q + 0.000001, b.R + 0.000001, b.S - 0.000002);
            var results = new List<Hex>();
            var step = 1.0 / Math.Max(n, 1);
            for (var i = 0; i <= n; i++)
            {
                results.Add(aNudge.HexLerp(bNudge, step * i).HexRound());
            }

            return results;
        }
    }

    public struct OffsetCoord
    {
        public readonly int Col;
        public readonly int Row;
        public static int EVEN = 1;
        public static int ODD = -1;

        public OffsetCoord(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public static OffsetCoord QoffsetFromCube(int offset, Hex h)
        {
            var col = h.Q;
            var row = h.R + (h.Q + offset * (h.Q & 1)) / 2;
            return new OffsetCoord(col, row);
        }

        public static Hex QoffsetToCube(int offset, OffsetCoord h)
        {
            var q = h.Col;
            var r = h.Row - (h.Col + offset * (h.Col & 1)) / 2;
            var s = -q - r;
            return new Hex(q, r, s);
        }

        public static OffsetCoord RoffsetFromCube(int offset, Hex h)
        {
            var col = h.Q + (h.R + offset * (h.R & 1)) / 2;
            var row = h.R;
            return new OffsetCoord(col, row);
        }

        public static Hex RoffsetToCube(int offset, OffsetCoord h)
        {
            var q = h.Col - (h.Row + offset * (h.Row & 1)) / 2;
            var r = h.Row;
            var s = -q - r;
            return new Hex(q, r, s);
        }
    }

    public struct DoubledCoord
    {
        public readonly int Col;
        public readonly int Row;

        public DoubledCoord(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public static DoubledCoord QdoubledFromCube(Hex h)
        {
            var col = h.Q;
            var row = 2 * h.R + h.Q;

            return new DoubledCoord(col, row);
        }
        
        public Hex QdoubledToCube()
        {
            var q = Col;
            var r = (Row - Col) / 2;
            var s = -q - r;

            return new Hex(q, r, s);
        }

        public static DoubledCoord RdoubledFromCube(Hex h)
        {
            var col = 2 * h.Q + h.R;
            var row = h.R;

            return new DoubledCoord(col, row);
        }

        public Hex RdoubledToCube()
        {
            var q = (Col - Row) / 2;
            var r = Row;
            var s = -q - r;

            return new Hex(q, r, s);
        }
    }

    public struct Orientation
    {
        public readonly double F0;
        public readonly double F1;
        public readonly double F2;
        public readonly double F3;
        public readonly double B0;
        public readonly double B1;
        public readonly double B2;
        public readonly double B3;
        public readonly double StartAngle;

        public Orientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3,
            double startAngle)
        {
            F0 = f0;
            F1 = f1;
            F2 = f2;
            F3 = f3;
            B0 = b0;
            B1 = b1;
            B2 = b2;
            B3 = b3;
            StartAngle = startAngle;
        }
    }

    public struct Layout
    {
        public readonly Orientation Orientation;
        public readonly Point Size;
        public readonly Point Origin;

        public Layout(Orientation orientation, Point size, Point origin)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
        }

        public static Orientation Pointy = new Orientation(Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0,
            Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);

        public static Orientation Flat = new Orientation(3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0, Math.Sqrt(3.0),
            2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);

        public Point HexToPixel(Hex h)
        {
            var m = Orientation;
            var x = (m.F0 * h.Q + m.F1 * h.R) * Size.X;
            var y = (m.F2 * h.Q + m.F3 * h.R) * Size.Y;

            return new Point(x + Origin.X, y + Origin.Y);
        }

        public FractionalHex PixelToHex(Point p)
        {
            var m = Orientation;
            var pt = new Point((p.X - Origin.X) / Size.X, (p.Y - Origin.Y) / Size.Y);
            var q = m.B0 * pt.X + m.B1 * pt.Y;
            var r = m.B2 * pt.X + m.B3 * pt.Y;

            return new FractionalHex(q, r, -q - r);
        }
        
        public Point HexCornerOffset(int corner)
        {
            var m = Orientation;
            var angle = 2.0 * Math.PI * (m.StartAngle - corner) / 6.0;

            return new Point(Size.X * Math.Cos(angle), Size.Y * Math.Sin(angle));
        }
        
        public List<Point> PolygonCorners(Hex h)
        {
            var corners = new List<Point>();
            var center = HexToPixel(h);
            for (var i = 0; i < 6; i++)
            {
                var offset = HexCornerOffset(i);
                corners.Add(new Point(center.X + offset.X, center.Y + offset.Y));
            }

            return corners;
        }
    }
}