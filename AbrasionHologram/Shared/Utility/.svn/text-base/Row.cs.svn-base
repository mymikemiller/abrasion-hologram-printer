using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// Represents a Row of a Matrix3D by storing the values (doubles) in a 4-item array.
    /// </summary>
    public class Row
    {
        /// <summary>
        /// An ordered array of the values of the Row.
        /// </summary>
        double[] values;

        /// <summary>
        /// Initializes the Row with no values.
        /// </summary>
        public Row()
        {
            values = new double[0];
        }

        /// <summary>
        /// Initializes the Row with the specified number of values and sets each value to 0.
        /// </summary>
        /// <param name="numValues">The number of values in the Row.</param>
        public Row(int numValues)
        {
            values = new double[numValues];
            Fill(0.0);
        }

        /// <summary>
        /// Initializes the Row with the specified number of values and sets each value to the specified initial value.
        /// </summary>
        /// <param name="numValues">The number of values in the Row.</param>
        /// <param name="initialValue">The initial value to which to set all the values.</param>
        public Row(int numValues, double initialValue)
        {
            values = new double[numValues];
            Fill(initialValue);
        }

        /// <summary>
        /// Initializes the row with the values from the specified row, essentially making a copy.
        /// </summary>
        /// <param name="rowToCopy">The Row to duplicate.</param>
        public Row(Row rowToCopy)
        {
            values = new double[rowToCopy.NumValues];
            for (int i = 0; i < NumValues; i++)
            {
                values[i] = rowToCopy[i];
            }
        }

        /// <summary>
        /// Sets every value in the Row with the specified value.
        /// </summary>
        /// <param name="value">The value to which to set all the values in the row.</param>
        /// <returns>This Row.</param>
        public Row Fill(double value)
        {
            for (int i = 0; i < NumValues; i++)
            {
                values[i] = value;
            }
            return this;
        }
        /// <summary>
        /// Sets all the values of this Row to match that of the passed in Row.
        /// </summary>
        /// <param name="valuesToFillWith">The Row containing the values to fill this Row with. Must have the same NumValues.</param>
        /// <returns>This Row.</param>
        public Row Fill(Row valuesToFillWith)
        {
            if (this.NumValues != valuesToFillWith.NumValues)
                throw new Exception("The passed in Row must have the same NumValues as this Row.");

            for (int i = 0; i < NumValues; i++)
            {
                values[i] = valuesToFillWith[i];
            }
            return this;
        }

        /// <summary>
        /// Value accessor. Identical to GetValue(colIndex) and SetValue(colIndex, value).
        /// </summary>
        /// <param name="colIndex">The 0-based Column index of the value to return/set.</param>
        /// <returns>The value in the Row at the specified Column index.</returns>
        public double this[int colIndex]
        {
            get
            {
                return GetValue(colIndex);
            }
            set
            {
                SetValue(colIndex, value);
            }
        }

        /// <summary>
        /// Value accessor. Identical to this[colIndex].
        /// </summary>
        /// <param name="colIndex">The 0-based Column index of the value to return.</param>
        /// <returns>The value in the Row at the specified Column index.</returns>
        public double GetValue(int colIndex)
        {
            if (colIndex < NumValues)
            {
                return values[colIndex];
            }
            else
            {
                throw new IndexOutOfRangeException("An attempt was made to get value " + colIndex + ". The greatest value index of this matrix is " + (NumValues - 1) + ".");
            }
        }

        /// <summary>
        /// Sets the value at the specified index to the specified value.
        /// </summary>
        /// <param name="colIndex">The 0-based Column index of the value to set.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(int colIndex, double value)
        {
            if (colIndex < NumValues)
            {
                values[colIndex] = value;
            }
            else
            {
                throw new IndexOutOfRangeException("An attempt was made to set value " + colIndex + ". The greatest value index of this matrix is " + (NumValues - 1) + ".");
            }
        }

        /// <summary>
        /// Tests if each value in a Row is numerically equivalent to that of this Row.
        /// </summary>
        /// <param name="obj">The Row to compare.</param>
        /// <returns>True if the two Rows are identical.</returns>
        public override bool Equals(object obj)
        {
            Row r = (Row)obj;
            if (NumValues != r.NumValues) return false;
            for (int i = 0; i < NumValues; i++)
            {
                if (this.GetValue(i) != r.GetValue(i)) return false;
            }
            return true;
        }

        /// <summary>
        /// Returns the Row's hash-code.
        /// </summary>
        /// <returns>The Row's hash-code.</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (double val in values)
            {
                hash ^= val.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Addition operator for two Rows.
        /// </summary>
        /// <param name="r1">The Row to be added to.</param>
        /// <param name="r2">The Row to add.</param>
        /// <returns>A Row containing the values of the two Rows added together.</returns>
        public static Row operator +(Row r1, Row r2)
        {
            if (r1.NumValues != r2.NumValues)
            {
                throw new Exception("Added rows must have the same number of values. Left hand row: " + r1.NumValues + " values. Right hand row: " + r2.NumValues + " values.");
            }
            else
            {
                Row r3 = new Row(r1.NumValues);
                for (int i = 0; i < r1.NumValues; i++)
                {
                    r3[i] = r1[i] + r2[i];
                }
                return r3;
            }
        }

        /// <summary>
        /// Subtraction operator for two Rows.
        /// </summary>
        /// <param name="r1">The Row to be subtracted from.</param>
        /// <param name="r2">The Row to subtract.</param>
        /// <returns>A Row containing the values of the second Row subtracted from the first Row.</returns>
        public static Row operator -(Row r1, Row r2)
        {
            if (r1.NumValues != r2.NumValues)
            {
                throw new Exception("Subtracted rows must have the same number of values. Left hand row: " + r1.NumValues + " values. Right hand row: " + r2.NumValues + " values.");
            }
            else
            {
                return r1 + (-1 * r2);
            }
        }

        /// <summary>
        /// Scalar multiplication operator
        /// </summary>
        /// <param name="r">The Row to be multiplied by the scalar value.</param>
        /// <param name="scalar">The scalar value by which to multiply the Row.</param>
        /// <returns>A Row containing each value from the Row multiplied by the scalar.</returns>
        public static Row operator *(Row r, double scalar)
        {
            Row r2 = new Row(r.NumValues);
            for (int i = 0; i < r.NumValues; i++)
            {
                r2[i] = r[i] * scalar;
            }
            return r2;
        }

        /// <summary>
        /// Scalar multiplication operator
        /// </summary>
        /// <param name="scalar">The scalar value by which to multiply the Row.</param>
        /// <param name="r">The Row to be multiplied by the scalar value.</param>
        /// <returns>A Row containing each value from the Row multiplied by the scalar.</returns>
        public static Row operator *(double scalar, Row r1)
        {
            return r1 * scalar;
        }

        /// <summary>
        /// Scalar division operator
        /// </summary>
        /// <param name="r">The Row to be divided by the scalar value.</param>
        /// <param name="scalar">The scalar value by which to divide the Row.</param>
        /// <returns>A Row containing each value from the Row divided by the scalar.</returns>
        public static Row operator /(Row r, double scalar)
        {
            Row r2 = new Row(r.NumValues);
            for (int i = 0; i < r.NumValues; i++)
            {
                r2[i] = r[i] / scalar;
            }
            return r2;
        }

        /// <summary>
        /// Returns the Row in the form of a 1xn Matrix.
        /// </summary>
        /// <returns>The Row in the form of a 1xn Matrix.</returns>
        public Matrix ToMatrix()
        {
            return new Matrix(this);
        }

        /// <summary>
        /// The number of values in the Row.
        /// </summary>
        public int NumValues
        {
            get { return values.Length; }
        }

        /// <summary>
        /// Returns a textual-representation of the Row, defaulting to 2 decimal places for each value.
        /// </summary>
        /// <returns>A textual-representation of the Row.</returns>
        public override string ToString()
        {
            return ToString(2);
        }

        /// <summary>
        /// Returns a textual-representation of the Row with values rounded to the specified number of decimal places.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places to which to round the Row values.</param>
        /// <returns>A textual-representation of the Row.</returns>
        public string ToString(int decimalPlaces)
        {
            StringBuilder b = new StringBuilder();
            b.Append("[");
            foreach (double val in values)
            {
                b.Append("  ");
                b.Append(val.ToString("N" + decimalPlaces.ToString()));
            }
            b.Append("  ]");
            return b.ToString();
        }

        /// <summary>
        /// Returns the length of the "longest" value in the Row. "Longest" means the number of characters in the textual representation of the value rounded to the specified number of decimal places.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places to which to round the Row values.</param>
        /// <returns>The length of the longest value in the Row.</returns>
        public int LengthOfLongestValue(int decimalPlaces)
        {
            int length = 0;
            foreach (double val in values)
            {
                length = Math.Max(length, val.ToString("N" + decimalPlaces.ToString()).Length);
            }
            return length;
        }
    }
}
