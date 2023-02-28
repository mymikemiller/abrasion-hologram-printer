using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using Utility;

namespace CreatorSupport
{

    public class RedrawRequiredEventArgs : EventArgs
    {
        public RedrawTypeRequired RedrawTypeRequired { get; private set; }
        public RedrawRequiredEventArgs(RedrawTypeRequired redrawType)
        {
            RedrawTypeRequired = redrawType;
        }
    }

    public enum StereoscopicMode { NonStereoscopic, Left, Right }
    public enum RedrawTypeRequired { None, Redraw, RecalculateViewPrimitives, RecalculateAllArcs, RecalculateArcPositionsOnly }
    
    public delegate void ViewChangedHandler(RedrawRequiredEventArgs e);
    

    public static class ViewContext
    {
        /// <summary>Gets and sets a value indicating tne closest the user can get to the point of reference when zooming in. After getting this close, Pr moves away from the user, causing the user to "fly around" instead of just zooming.</summary>
        private static double MinNLength { get; set; }

        private static Size mCanvasSize;
        private static Coord mLookUpVector = new Coord(0, 1, 0);
        private static Coord mPo = new Coord(1,0,0);
        private static Coord mPr = new Coord();
        private static double mCurrentScale;
        private static double mZf;
        private static double mN_ViewCoordinates;
        private static Coord mPreviousN;

        /// <summary>ViewAngle is the angle currently being used for drawing. In stereoscopic modes, this will be offset from mBaseViewAngle by mStereoscopicDisparityAngly.</summary>
        private static double mViewAngle = 0;

        //Cache the trig values used to find the ArcPoint so we don't calculate the same values for each ViewPoint being drawn.
        public static double SinViewAngle { get; private set; }
        public static double CosViewAngle { get; private set; }

        public static bool SlowNavigation { get; set; }

        public static bool HaltViewChangedEvent { get; set; }
        public static event ViewChangedHandler ViewChanged;
        
        static ViewContext()
        {
            HaltViewChangedEvent = false;
            mPreviousN = N;
            SlowNavigation = false;
            CanvasSize = new Size(1, 1);
            MinNLength = 1.5;
            RecalculateViewAngleTrig();
            ResetCamera();
        }

        /// <summary>Fires the even that lets listening classes know that the view has changed.</summary>
        private static void FireViewChangedEvent(RedrawTypeRequired type)
        {
            if (!HaltViewChangedEvent)
            {
                if (ViewChanged != null)
                    ViewChanged(new RedrawRequiredEventArgs(type));
            }
        }

        public static void ResetCamera()
        {
            Po = new Coord(6,6,6);
            Pr = new Coord(-.5,.5,0);
            LookUpVector = new Coord(0, 1, 0);
            CurrentScale = 2.5;
            Zf = 25;
        }


        internal static void RecalculateMatrix()
        {
            if (!Helper.DesignMode)
            {
                if (CanvasSize.Width > 0 && CanvasSize.Height > 0)
                {
                    Matrix n = N.UnitVector.ToVectorCol(false);

                    Matrix V = LookUpVector.ToVectorCol(false);
                    //if (n.Equals(V / V.Norm)) // we're going to cross-product n and V, so they can't be pointing in the same direction. Move V just a bit
                    //    V[0, 0] += Double.MinValue;
                    Matrix u = V.CrossProduct(n);
                    u = u / u.Norm;

                    Matrix v = n.CrossProduct(u);


                    Matrix modelToView = new Matrix();
                    modelToView[0, 0] = u[0, 0];
                    modelToView[0, 1] = u[1, 0];
                    modelToView[0, 2] = u[2, 0];
                    modelToView[1, 0] = v[0, 0];
                    modelToView[1, 1] = v[1, 0];
                    modelToView[1, 2] = v[2, 0];
                    modelToView[2, 0] = n[0, 0];
                    modelToView[2, 1] = n[1, 0];
                    modelToView[2, 2] = n[2, 0];

                    //set initial offset (translate Po to origin) in modelToView
                    modelToView[0, 3] = -(u[0, 0] * Po.X + u[1, 0] * Po.Y + u[2, 0] * Po.Z);
                    modelToView[1, 3] = -(v[0, 0] * Po.X + v[1, 0] * Po.Y + v[2, 0] * Po.Z);
                    modelToView[2, 3] = -(n[0, 0] * Po.X + n[1, 0] * Po.Y + n[2, 0] * Po.Z);


                    //set perspective
                    Matrix perspective = new Matrix();
                    double zf = (Zf == 0) ? .00001 : Zf;
                    perspective[3, 2] = -1 / zf;

                    //we want 15 modeling units to fit on the height of the screen.
                    double baseScaleValue = CanvasSize.Height / 15;
                    Matrix scale = new Matrix();
                    scale[0, 0] = baseScaleValue * CurrentScale;
                    scale[1, 1] = baseScaleValue * CurrentScale;
                    scale[2, 2] = baseScaleValue * CurrentScale;

                    //flip the y and center (shift origin down and left half the height/width)
                    Matrix viewToWindow = new Matrix();
                    viewToWindow[1, 1] = -1; //to flip the y's
                    viewToWindow[0, 3] = CanvasSize.Width / 2.0; //to shift right
                    viewToWindow[1, 3] = CanvasSize.Height / 2.0; //to shift down

                    Transformer.ModelToWindowMatrix = viewToWindow * scale * perspective * modelToView;


                    //mZf_ModelingCoordinates = WindowToModel(mPo_ViewCoordinates - new Coord(0, 0, Zf));
                    mN_ViewCoordinates = Transformer.ModelToWindow(Pr).Z;


                    FireViewChangedEvent(RedrawTypeRequired.RecalculateViewPrimitives);
                    mPreviousN = N;
                }
            }
        }

        public static PointD GetArcPoint(PointD locationAtZeroAngle, PointD centerPoint)
        {
            PointD withOriginAtZero = locationAtZeroAngle - centerPoint;
            //it doesn't matter whether we're doing an upside-down or rightside-up arc - because we're rotating about the center point - and it will be above or below us depending - we'll end up at the right place.
            return new PointD(withOriginAtZero.X * CosViewAngle - withOriginAtZero.Y * SinViewAngle + centerPoint.X, withOriginAtZero.X * SinViewAngle + withOriginAtZero.Y * CosViewAngle + centerPoint.Y);
        }


        #region Properties
        /// <summary>Gets and sets the angle at which the lightsource is illuminating the arcs. Range: -90 to 90.</summary>
        public static double ViewAngle
        {
            get { return mViewAngle; }
            set
            {
                if (mViewAngle == value)
                    return;
                mViewAngle = value;

                RecalculateViewAngleTrig();

                FireViewChangedEvent(RedrawTypeRequired.RecalculateArcPositionsOnly);
            }
        }

        private static void RecalculateViewAngleTrig()
        {
            SinViewAngle = Math.Sin(mViewAngle * Math.PI / 180.0);
            CosViewAngle = Math.Cos(mViewAngle * Math.PI / 180.0);
        }
        
        public static Size CanvasSize
        {
            get { return mCanvasSize; }
            set
            {
                if (mCanvasSize == value)
                    return;
                mCanvasSize = value;
                RecalculateMatrix();
            }
        }
        public static Coord LookUpVector
        {
            get { return mLookUpVector; }
            set
            {
                if (mLookUpVector == value.UnitVector)
                    return;
                mLookUpVector = value.UnitVector;
                RecalculateMatrix();
            }
        }
        /// <summary>
        /// Gets and sets the Point of Origin (in Modeling coordinates) for this ViewContext (the point from which the user is looking at the scene)
        /// </summary>
        public static Coord Po
        {
            get { return mPo; }
            set
            {
                if (mPo == value)
                    return;
                mPo = value;
                RecalculateMatrix();
            }
        }        
        ///// <summary>
        ///// Gets the Point of Origin in View coordinates for this ViewContext (Always at the center of the screen with Z = 0. Cached here for easy access)
        ///// </summary>
        //public static Coord Po_ViewCoordinates
        //{
        //    get { return mPo_ViewCoordinates; }
        //}
        /// <summary>
        /// Gets and sets the Point of Reference (in Modeling coordinates) for this ViewContext (the point at which the user is looking)
        /// </summary>
        public static Coord Pr
        {
            get { return mPr; }
            set
            {
                if (mPr == value) 
                    return;
                mPr = value;
                RecalculateMatrix();
            }
        }
        /// <summary>
        /// Gets and sets the current scale value for this ViewContext. A Scale of 1 is default.
        /// </summary>
        public static double CurrentScale
        {
            get { return mCurrentScale; }
            set
            {
                if (mCurrentScale == value)
                    return;
                mCurrentScale = value;
                RecalculateMatrix();
            }
        }
        /// <summary>
        /// Gets and sets the Z coordinate (in View coordinates) for the point of perspective.
        /// This point always lies along the N line away from the canvas toward the user, 
        /// therefore the coordinates (in View coordinates) of the point of perspective is always (0, 0, Zf)
        /// </summary>
        public static double Zf
        {
            get { return mZf; }
            set
            {
                if (mZf == value)
                    return;
                mZf = value;
                RecalculateMatrix();
            }
        }

        /// <summary>
        /// Gets the N vector, which points from Pr to Po in model coordinates
        /// </summary>
        public static Coord N
        {
            get
            {
                return Po - Pr;
            }
        }        
        /// <summary>
        /// Gets the distance from the screen (Po plane) to Pr in view coordinates.
        /// </summary>
        public static Double N_ViewCoordinates
        {
            get
            {
                return mN_ViewCoordinates;
            }
        }
        /// <summary>Gets the location of Po in view coordinates. Always the center of the canvas with Z = 0.</summary>
        public static Coord Po_ViewCoordinates
        {
            get { return new Coord(mCanvasSize.Width / 2, mCanvasSize.Height / 2); }
        }
        /// <summary>Gets the location of Pr in view coordinates. Always the center of the canvas with Z = N_ViewCoordinates.</summary>
        public static Coord Pr_ViewCoordinates
        {
            get { return new Coord(mCanvasSize.Width / 2, mCanvasSize.Height / 2, N_ViewCoordinates); }
        }

        public static void FlipLookUp()
        {
            LookUpVector = LookUpVector - LookUpVector * 2;
        }

        #endregion

 



        #region Maniuplation Methods

        public static void Pan(Coord lastMouseClick_ViewCoordinates, Coord currentMouseClick_ViewCoordinates) //send in without converting WindowToModel();
        {
            Coord lastClick = Transformer.WindowToModel(new Coord(lastMouseClick_ViewCoordinates.X, lastMouseClick_ViewCoordinates.Y, 0));
            Coord currentClick = Transformer.WindowToModel(new Coord(currentMouseClick_ViewCoordinates.X, currentMouseClick_ViewCoordinates.Y, 0));
            Coord diff = lastClick - currentClick;
            Po += diff;
            Pr += diff;
        }
        /// <summary>
        /// Zooms in by moving Po closer to Pr. Zooming in too much will begin to cut objects off at the Po plane. Use Scale() to enlarge the image without moving the plane.
        /// </summary>
        /// <param name="zoomAmount">The number of modeling units closer (positive) or farther away (negative) to Pr to move Po. 1.5 modeling units is as close as Po can get to Pr.</param>
        public static void Zoom(double zoomAmount)
        {
            //move Po toward Pr (in along N)
            //if (zoomAmount > N.Length)
            //    zoomAmount = 1.5;

            //multiply N's unit vector by zoomAmount to get the length along N that the new Po will be at
            Coord n = N.UnitVector;
            n *= zoomAmount;

            if ((N - n).Length < 1.5) //allow Po to be a minimum of 1.5 modeling units from Pr.
                Po = (Pr + N.UnitVector);
            else
                Po = (Po - n);
        }
        /// <summary>
        /// Flies toward Pr by moving Po and Pr the same amount in the direction of the line from Po to Pr.
        /// </summary>
        /// <param name="zoomAmount"></param>
        public static void Fly(double flyAmount)
        {
            //move Po toward Pr (in along N)

            //multiply N's unit vector by zoomAmount to get the length along N that the new Po will be at
            Coord n = N.UnitVector;
            n *= flyAmount;

            //n is now of the right magnitude. we need to get it back onto the N line, so add Pr's coordinates
            Po = (Po - n);// +Pr;

            //we need to move Pr the same distance along N to simulate flying around
            Pr = Pr - n;
        }
        /// <summary>
        /// Scale the image without moving Po or Pr. Multiplies CurrentScale by the specified scale amount.
        /// </summary>
        public static void Scale(double scaleAmount)
        {
            CurrentScale *= scaleAmount;
        }
        public static void Orbit(Coord newPoLocation_ViewCoordinates) //send in without converting WindowToModel();. Z value will be ignored because Z value is always 0.
        {
            newPoLocation_ViewCoordinates.Z = 0;
            // Move Po - leave Pr fixed. Keep distance between Po and Pr fixed too.

            Coord newPo = Transformer.WindowToModel(newPoLocation_ViewCoordinates);

            //newPo is in the right spot, but its N vector is now too long. Make the N vector the same size as the old N vector.
            Coord newN = (newPo - Pr).UnitVector * N.Length;

            //now put Po on that new vector.
            Po = Pr + newN;
        }
        public static void LookAround(Coord newPrLocation_ViewCoordinates) //send in without converting WindowToModel();. Z value will be ignored because Z value is always -N_ViewCoordinates.
        {
            newPrLocation_ViewCoordinates.Z = -N_ViewCoordinates;
            // Move Pr - leave Po fixed. Keep distance between Po and Pr fixed too.

            Coord newPr = Transformer.WindowToModel(newPrLocation_ViewCoordinates);

            //newPr is in the right spot, but its N vector is a different size. Make the N vector the same size as the old N vector.
            Coord newN = (Po - newPr).UnitVector * N.Length;

            //now put Po on that new vector.
            Pr = newN + Po;
        }

        #endregion

    }
}
