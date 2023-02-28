using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Utility;

namespace CreatorSupport
{
    public static class Transformer
    {
        private static Matrix mModelToWindowMatrix;
        private static Matrix mWindowToModelMatrix { get; set; }



        static Transformer()
        {
            mModelToWindowMatrix = new Matrix();
            mWindowToModelMatrix = new Matrix();
        }

        internal static Matrix ModelToWindowMatrix
        {
        		get
        		{
        		    return mModelToWindowMatrix;
        		}
        		set
        		{
                    mModelToWindowMatrix = value;
                    mWindowToModelMatrix = mModelToWindowMatrix.Inverse();
        		}
        }

        //unpredictable results if windowCoord.Z != 0
        public static Coord WindowToModel(Coord windowCoord)
        {
            Matrix toTransform = windowCoord.ToVectorCol(true);
            Matrix result = mWindowToModelMatrix * toTransform;

            //normalize for perspective
            result /= result[3, 0];

            return new Coord(result[0, 0], result[1, 0], result[2, 0]);
        }
        public static Coord ModelToWindow(Coord modelCoord)
        {
            Matrix toTransform = modelCoord.ToVectorCol(true);
            Matrix result = ModelToWindowMatrix * toTransform;

            double perspectiveNormalization = result[3, 0];


            //normalize for perspective
            result /= result[3, 0];

            Coord retVal = new Coord(result[0, 0], result[1, 0], result[2, 0]);
            return retVal;
        }

        public static PointF ViewFromAxis(Coord pointToTransform, Coord viewAxisUnitVector, Coord perpendicularAxisUnitVector)
        {
            Coord thirdUnitVector = viewAxisUnitVector.CrossProduct(perpendicularAxisUnitVector);

            Matrix m = new Matrix(3);
            m[0, 0] = viewAxisUnitVector.X;
            m[0, 1] = viewAxisUnitVector.Y;
            m[0, 2] = viewAxisUnitVector.Z;
            m[1, 0] = perpendicularAxisUnitVector.X;
            m[1, 1] = perpendicularAxisUnitVector.Y;
            m[1, 2] = perpendicularAxisUnitVector.Z;
            m[2, 0] = thirdUnitVector.X;
            m[2, 1] = thirdUnitVector.Y;
            m[2, 2] = thirdUnitVector.Z;

            Matrix toTransform = pointToTransform.ToVectorCol(false);
            Matrix result = m * toTransform;
            PointF retVal = new PointF((float)result[1, 0], (float)result[2, 0]);
            return retVal;
        }


        public static Coord GetArcCoord(Coord locationAtZeroAngle)
        {
            //Find the Center Point of the arc:
            //X value is at this ViewPoint's X value because when the ViewPoint is drawn at 0 angle, it appears at the apex of the arc.
            //Y value is either shifted up or down from that point depending on whether or not the point is in front of or behind the canvas. The amount shifted is directly proportional to the distance to the canvas.
            //if in front of the canvas, we want the arc u-shaped (with Location.Y at the bottom of the arc), so the center is Location.Y - Distance (Distance will be positive if in front of canvas)
            //if behind the canvas, we want the arc n-shaped (with Location.Y at the top of the arc), so the center is Location.Y + Math.Abs(Distance), or Location.Y - Distance (because distance is negative if behind canvas)
            // either way, the Y value of the arc center is at Location.Y - DistanceFromCanvas.
            double distanceFromCanvas = locationAtZeroAngle.Z - ViewContext.N_ViewCoordinates;
            PointD center = new PointD(locationAtZeroAngle.X, locationAtZeroAngle.Y - distanceFromCanvas / 2);

            PointD withOriginAtZero = locationAtZeroAngle.ToPointD() - center;
            //it doesn't matter whether we're doing an upside-down or rightside-up arc - because we're rotating about the center point - and it will be above or below us depending - we'll end up at the right place.
            Coord c = new Coord(withOriginAtZero.X * ViewContext.CosViewAngle - withOriginAtZero.Y * ViewContext.SinViewAngle + center.X, withOriginAtZero.X * ViewContext.SinViewAngle + withOriginAtZero.Y * ViewContext.CosViewAngle + center.Y, locationAtZeroAngle.Z);
            return c;
        }

        public static RectangleF GetArcSquare(Coord locationAtZeroAngle)
        {
            PointD center = GetArcCenter(locationAtZeroAngle);

            double halfwidth = Math.Abs(center.Y - locationAtZeroAngle.Y);
            float length = Math.Max((float)(halfwidth * 2 + .5), 1);
            RectangleF r = new RectangleF((float)(center.X - halfwidth + .5), (float)(center.Y - halfwidth + .5), length, length);
            return r;
        }

        public static PointD GetArcCenter(Coord locationAtZeroAngle)
        {
            return new PointD(locationAtZeroAngle.X, locationAtZeroAngle.Y - GetArcRadius(locationAtZeroAngle));
        }

        /// <summary>Gets the radius of the arc to be drawn. Negative values imply upside-down arcs</summary>
        public static double GetArcRadius(Coord locationAtZeroAngle)
        {
            double distanceFromCanvas = locationAtZeroAngle.Z - ViewContext.N_ViewCoordinates;
            return distanceFromCanvas / 2.0;
        }
    }
}
