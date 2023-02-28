using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Creator
{
    /// <summary>A static class which holds static properties used in ArcFile creation</summary>
    public static class ArcFileSettings
    {
        /// <summary>Gets and sets the size (in centimeters) of the actual canvas that any ArcFiles created will be printed on</summary>
        public static SizeF CanvasSize { get; set; }
        /// <summary>Gets and sets the number of "snapshots" taken per degree when HiddenLine mode is enabled. Higher resolutions will prolong ArcFile generation, but ArcSections will start and stop at more accurate angles.</summary>
        public static double AngularResolution { get; set; }

        /// <summary>Gets the number of degrees apart each "snapshot" is when HiddenLine mode is enabled. This value is (1 / AngularResolution).</summary>
        public static double AngularStepIncrement { get { return 1 / AngularResolution; } }

        /// <summary>If false, each individual point will be compared to every IndexedFaceSet to determine visibility at each angle. If false, a much more efficient, yet slightly more error-prone hidden line algorithm will be used.</summary>
        public static bool QuickMode { get; set; }

        /// <summary>
        /// Initializes the <see cref="ArcFileSettings"/> class with a 40x30cm canvas, angular resolution of 1, not in QuickMOde.
        /// </summary>
        static ArcFileSettings()
        {
            CanvasSize = new SizeF(40, 30);
            AngularResolution = 1;
            QuickMode = false;
        }
    }
}