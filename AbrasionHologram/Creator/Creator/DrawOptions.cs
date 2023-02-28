using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using CreatorSupport;

namespace Creator
{
    public enum VisibilityMode { Transparent, HiddenLine }

    /// <summary>Handles the DrawOptionChanged event.</summary>
    public delegate void DrawOptionChangedHandler(RedrawRequiredEventArgs e);

    public class DrawOptions
    {
        //Options requiring no recalculation, just redraw
        private static VisibilityMode mVisibilityMode = VisibilityMode.HiddenLine;

        //Options requiring recalculation of viewpoints
        private static double mCoordsPerUnitLength = 0;

        /// <summary>
        /// Occurs when a DrawOption is changed.
        /// </summary>
        public static event DrawOptionChangedHandler DrawOptionChanged;

        /// <summary>Fires the event that lets listening classes know that an option has changed.</summary>
        private static void FireOptionChangedEvent(RedrawTypeRequired type)
        {
            if (DrawOptionChanged != null)
                DrawOptionChanged(new RedrawRequiredEventArgs(type));
        }
        /// <summary>
        /// Fires the option changed event.
        /// </summary>
        private static void FireOptionChangedEvent()
        {
            FireOptionChangedEvent(RedrawTypeRequired.Redraw);
        }

        #region Properties
        /// <summary>
        /// Gets or sets the visibility mode.
        /// </summary>
        /// <value>The visibility mode.</value>
        public static VisibilityMode VisibilityMode
        {
            get
            {
                return mVisibilityMode;
            }
            set
            {
                if (mVisibilityMode == value)
                    return;
                mVisibilityMode = value;
                FireOptionChangedEvent();
            }
        }

        /// <summary>
        /// Gets or sets the number of Coords per unit length of modeling-coordinate line.
        /// </summary>
        /// <value>The number of Coords per unit length of modeling-coordinate line.</value>
        public static double CoordsPerUnitLength
        {
            get { return mCoordsPerUnitLength; }
            set
            {
                if (mCoordsPerUnitLength == value)
                    return;
                mCoordsPerUnitLength = value;
                FireOptionChangedEvent(RedrawTypeRequired.RecalculateViewPrimitives);
            }
        }

        #endregion


    }
}
