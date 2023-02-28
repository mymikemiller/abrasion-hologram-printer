using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Drawing;
using System.IO;

namespace Utility
{
    /// <summary>Represents a portion of an arc that will be scratched onto the canvas. Center and Radius values are specified in centimeters, and angles are specified in degrees counterclockwise from the positive x axis.</summary>
    public struct ArcSection
    {
        /// <summary>The center-point (in centimeters x and y) of the circle upon which this ArcSection lies</summary>
        public PointD Center { get; set; }
        /// <summary>The radius (in centimeters) of the circle upon which this ArcSection lies</summary>
        public double Radius { get; set; }
        /// <summary>The angle (in degrees counterclockwise from positive x) at which this ArcSection begins</summary>
        public double StartAngle { get; set; }
        /// <summary>The angle (in degrees counterclockwise from positive x) at which this ArcSection ends</summary>
        public double EndAngle { get; set; }

        /// <summary>
        /// Creates a new ArcSection with the specified values
        /// </summary>
        /// <param name="center">The center-point (in centimeters x and y) of the circle upon which this ArcSection lies</param>
        /// <param name="radius">The radius (in centimeters) of the circle upon which this ArcSection lies</param>
        /// <param name="startAngle">The angle (in degrees counterclockwise from positive x) at which this ArcSection begins</param>
        /// <param name="endAngle">The angle (in degrees counterclockwise from positive x) at which this ArcSection ends</param>
        public ArcSection(PointD center, double radius, double startAngle, double endAngle)
            : this()
        {
            Center = center;
            Radius = radius;
            StartAngle = startAngle;
            EndAngle = endAngle;
        }
        /// <summary>Creates an ArcSection based on a command identical in form to the command that would be returned by GetCommand(). The ArcNum value is ignored.</summary>
        /// <param name="command">An arc creation command in the following format: #arc ArcNum: CenterX CenterY Radius StartAngle EndAngle</param>
        public ArcSection(string command)
            : this()
        {
            string[] vals = command.Split(' ');
            Center = new PointD(double.Parse(vals[2]), double.Parse(vals[3]));
            Radius = double.Parse(vals[4]);
            StartAngle = double.Parse(vals[5]);
            EndAngle = double.Parse(vals[6]);
        }

        /// <summary>
        /// Creates a duplicate of this ArcSection
        /// </summary>
        /// <returns>A new ArcSection with the values from this ArcSection</returns>
        public ArcSection Clone()
        {
            return new ArcSection(Center, Radius, StartAngle, EndAngle);
        }

        #region Calculated Properties

        /// <summary>Gets a value indicating whether or not this ArcSection represents a U-shaped arc. True if StartAngle > 180.</summary>
        public bool IsUpsideDown { get { return StartAngle > 180; } }

        #endregion

        public static bool operator ==(ArcSection a, ArcSection b)
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
            return a.Center == b.Center && a.StartAngle == b.StartAngle && a.EndAngle == b.EndAngle;
        }

        public static bool operator !=(ArcSection a, ArcSection b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(ArcSection))
            {
                return this == (ArcSection)obj;
            }
            else
                return false;
        }

        /// <summary>Gets a command that can be used to recreate this ArcSection.</summary>
        /// <param name="ArcNum">The ID of this arc that will be appended before the colon.</param>
        /// <returns>An arc creation command in the following format: #arc ArcNum: CenterX CenterY Radius StartAngle EndAngle</returns>
        public string GetCommand(int ArcNum)
        {
            StringBuilder b = new StringBuilder();
            b.AppendFormat("#arc {0}: {1} {2} {3} {4} {5}", ArcNum, Center.X, Center.Y, Radius, StartAngle, EndAngle);
            return b.ToString();
        }
    }
}
