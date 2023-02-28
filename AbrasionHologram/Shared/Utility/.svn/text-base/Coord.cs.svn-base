using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Utility
{
    public struct Coord
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Coord(double x, double y, double z)
            :this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Coord(double x, double y)
            :this()
        {
            X = x;
            Y = y;
            Z = 0;
        }

        public static Coord operator *(Coord c, double rhs)
        {
            return new Coord(c.X * rhs, c.Y * rhs, c.Z * rhs);
        }
        public static Coord operator *(double lhs, Coord c)
        {
            return c * lhs;
        }
        public static Coord operator /(Coord c, double rhs)
        {
            return new Coord(c.X / rhs, c.Y / rhs, c.Z / rhs);
        }
        public static Coord operator -(Coord c, Coord rhs)
        {
            return new Coord(c.X - rhs.X, c.Y - rhs.Y, c.Z - rhs.Z);
        }
        public static Coord operator +(Coord c, Coord rhs)
        {
            return new Coord(c.X + rhs.X, c.Y + rhs.Y, c.Z + rhs.Z);
        }
        public static bool operator ==(Coord a, Coord b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Coord a, Coord b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Coord))
            {
                return this == (Coord)obj;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
        public string ToString(int decimalPlaces)
        {
            return "(" + X.ToString("N" + decimalPlaces) + ", " + Y.ToString("N" + decimalPlaces) + ", " + Z.ToString("N" + decimalPlaces) + ")";
        }


        public Matrix ToVectorCol(bool includeBottom1)
        {
            if (includeBottom1)
            {
                Matrix v = new Matrix(4, 1);
                v[0, 0] = X;
                v[1, 0] = Y;
                v[2, 0] = Z;
                v[3, 0] = 1.0;
                return v;
            }
            else
            {
                Matrix v = new Matrix(3, 1);
                v[0, 0] = X;
                v[1, 0] = Y;
                v[2, 0] = Z;
                return v;
            }

        }

        public double Length
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }
        /// <summary>Gets a unit vector in the direction of this Coord.</summary>
        public Coord UnitVector
        {
            get { return this / Length; }
        }

        /// <summary>Returns the Cross Product of this and rhs.</summary>
        /// <param name="rhs">The right-hand-side Coord.</param>
        /// <returns>The Cross Product of this and rhs.</returns>
        public Coord CrossProduct(Coord rhs)
        {
            Coord retVal = new Coord
                (
                this.Y * rhs.Z - this.Z * rhs.Y,
                this.Z * rhs.X - this.X * rhs.Z,
                this.X * rhs.Y - this.Y * rhs.X
                );
            return retVal;
        }
        /// <summary>Returns the Dot Product of this and rhs.</summary>
        /// <param name="rhs">The right-hand-side Coord.</param>
        /// <returns>The Dot Product of this and rhs.</returns>
        public double DotProduct(Coord rhs)
        {
            return this.X * rhs.X + this.Y * rhs.Y + this.Z * rhs.Z;
        }

        /// <summary>
        /// Returns a new PointD object with this Coord's X and Y value. The Z value is eliminated.
        /// </summary>
        public PointD ToPointD()
        {
            return new PointD(X, Y);
        }
        /// <summary>
        /// Returns a new PointF object with this Coord's X and Y value. The Z value is eliminated.
        /// </summary>
        public PointF ToPointF()
        {
            return new PointF((float)X, (float)Y);
        }
        /// <summary>
        /// Returns a new Point object with this Coord's X and Y value. The Z value is eliminated.
        /// </summary>
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
        public bool IsValid()
        {
            return !(double.IsNaN(X) || double.IsNaN(Y) || double.IsNaN(Z));
        }
    }
}
