using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Utility
{
    /// <summary>
    /// Handles ArcFileChanged events.
    /// </summary>
    public delegate void ArcFileChangedHandler();

    /// <summary>
    /// Represents an ArcFile that defines the ArcSections to be printed.
    /// </summary>
    public class ArcFile
    {
        /// <summary>
        /// Gets or sets the size of the canvas in centimeters. This should match the actual size of the canvas upon which this ArcFile will be printed.
        /// </summary>
        /// <value>The size of the canvas.</value>
        public SizeF CanvasSize { get; set; }
        /// <summary>
        /// Gets or sets the arc sections making up the ArcFile.
        /// </summary>
        /// <value>The arc sections.</value>
        public List<ArcSection> ArcSections { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcFile"/> class with no ArcSections and a 40x30 canvas.
        /// </summary>
        public ArcFile()
        {
            CanvasSize = new SizeF(40, 30);
            ArcSections = new List<ArcSection>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ArcFile"/> class representing the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public ArcFile(string fileName)
        {
            ArcSections = new List<ArcSection>();

            StreamReader r = new StreamReader(fileName);
            while(!r.EndOfStream)
            {
                string line = r.ReadLine();
                if (line.StartsWith("#canvasSize:"))
                {
                    string[] vals = line.Split(' ');
                    CanvasSize = new SizeF(float.Parse(vals[1]), float.Parse(vals[2]));
                }
                else if (line.StartsWith("#arc"))
                {
                    ArcSections.Add(new ArcSection(line));
                }
            }
        }

        /// <summary>
        /// Saves this ArcFile with the specified name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveAs(string fileName)
        {
            using (StreamWriter w = new StreamWriter(fileName, false))
            {
                //Write the canvas size.
                w.WriteLine("#canvasSize: " + CanvasSize.Width + " " + CanvasSize.Height);

                //Write all the arcs
                for(int i = 0; i < ArcSections.Count; i++)
                {
                    w.WriteLine(ArcSections[i].GetCommand(i));
                }
            }
        }
    }
}
