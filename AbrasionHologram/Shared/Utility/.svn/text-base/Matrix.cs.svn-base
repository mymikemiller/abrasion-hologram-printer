using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Utility
{
    /// <summary>
    /// Represents a Matrix as an ordered collection of Row objects.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// The collection of Rows making up the Matrix.
        /// </summary>
        Row[] rows;

        /// <summary>
        /// Initializes a 4x4 identity Matrix.
        /// </summary>
        public Matrix():
            this(4){}

        /// <summary>
        /// Initializes a square identity Matrix with the specified number of rows and columns.
        /// </summary>
        /// <param name="numRowsAndCols"></param>
        public Matrix(int numRowsAndCols)
        {
            rows = new Row[numRowsAndCols];

            for (int i = 0; i < NumRows; i++)
            {
                rows[i] = new Row(numRowsAndCols);
            }

            Identity();
        }

        /// <summary>
        /// Initializes an identity Matrix with the specified number of Rows and Columns.
        /// </summary>
        /// <param name="numRows">The number of Rows for the Matrix to have.</param>
        /// <param name="numCols">The number of Columns for the Matrix to have.</param>
        public Matrix(int numRows, int numCols)
        {
            rows = new Row[numRows];
            for (int i = 0; i < NumRows; i++)
            {
                rows[i] = new Row(numCols, 0);
            }
            Identity();
        }

        /// <summary>
        /// Initializes the matrix with the specified number of Rows and Columns with all values set to initialValue.
        /// </summary>
        /// <param name="numRows">The number of Rows for the Matrix to have.</param>
        /// <param name="numCols">The number of Columns for the Matrix to have.</param>
        /// <param name="initialValue">The initial value for all the cells in the Matrix.</param>
        public Matrix(int numRows, int numCols, double initialValue)
        {
            rows = new Row[numRows];
            for (int i = 0; i < NumRows; i++)
            {
                rows[i] = new Row(numCols, initialValue);
            }
        }

        /// <summary>
        /// Initializes the Matrix with the values from the passed-in Matrix.
        /// </summary>
        /// <param name="matrixToCopy">The Matrix whose values are to be copied.</param>
        public Matrix(Matrix matrixToCopy)
        {
            rows = new Row[matrixToCopy.NumRows];
            for (int i = 0; i < NumRows; i++)
            {
                rows[i] = new Row(matrixToCopy[i]);
            }
        }

        /// <summary>
        /// Initializes a 1xn Matrix with the values from the passed-in Row.
        /// </summary>
        /// <param name="singleRow">The Row whose values are to be copied.</param>
        public Matrix(Row singleRow)
        {
            rows = new Row[1];
            rows[0] = new Row(singleRow);
        }
        /// <summary>
        /// Sets the values in this Matrix to be an identity Matrix.
        /// </summary>
        /// <returns>This Matrix.</returns>
        public Matrix Identity()
        {
            Fill(0);
            for (int i = 0; i < NumRows; i++)
            {
                if(i < NumCols)
                    rows[i][i] = 1;
            }
            return this;
        }

        /// <summary>
        /// Row accessor. Identical to GetRow(rowIndex) and SetRow(rowIndex, row).
        /// </summary>
        /// <param name="rowIndex">The 0-based index of the row to retrieve.</param>
        /// <returns></returns>
        public Row this[int rowIndex]
        {
            get
            {
                return GetRow(rowIndex);
            }
            set
            {
                SetRow(rowIndex, value);
            }
        }

        /// <summary>
        /// Value accessor. Returns the value at the specified Row index and Column index.
        /// </summary>
        /// <param name="rowIndex">The 0-based Row index for the returned value.</param>
        /// <param name="colIndex">The 0-based Column index for the returned value.</param>
        /// <returns>The value at the specified Row index and Column index.</returns>
        public double this[int rowIndex, int colIndex]
        {
            get
            {
                return GetRow(rowIndex).GetValue(colIndex);
            }
            set
            {
                GetRow(rowIndex).SetValue(colIndex, value);
            }
        }

        /// <summary>
        /// Gets the specified Row.
        /// </summary>
        /// <param name="rowIndex">The 0-based index of the Row to return.</param>
        /// <returns>The Row at the specified index.</returns>
        public Row GetRow(int rowIndex)
        {
            if (rowIndex < NumRows)
            {
                return rows[rowIndex];
            }
            else
            {
                throw new IndexOutOfRangeException("An attempt was made to get row " + rowIndex + ". The greatest row index of this matrix is " + (NumRows - 1) + ".");
            }
        }

        /// <summary>
        /// Sets the Row at the specified 0-based index.
        /// </summary>
        /// <param name="rowIndex">The 0-based index to set the Row.</param>
        /// <param name="row">The Row to set in the Matrix.</param>
        public void SetRow(int rowIndex, Row row)
        {
            if (rowIndex < NumRows)
            {
                rows[rowIndex] = row;
            }
            else
            {
                throw new IndexOutOfRangeException("An attempt was made to set row " + rowIndex + ". The greatest row index of this matrix is " + (NumRows - 1) + ".");
            }
        }

        /// <summary>
        /// Sets all the values of this Matrix to the specified value.
        /// </summary>
        /// <param name="value">The value to fill the Matrix with.</param>
        /// <returns>This Matrix.</param>
        public Matrix Fill(double value)
        {
            foreach (Row r in rows)
            {
                r.Fill(value);
            }
            return this;
        }
        /// <summary>
        /// Sets all the values of this Matrix to match that of the passed in Matrix.
        /// </summary>
        /// <param name="valuesToFillWith">The Matrix containing the values to fill this Matrix with. Must be the same dimensions.</param>
        /// <returns>This Matrix.</param>
        public Matrix Fill(Matrix valuesToFillWith)
        {
            if (this.NumRows != valuesToFillWith.NumRows || this.NumCols != valuesToFillWith.NumCols)
                throw new Exception("The passed in Matrix must have the same dimensions as this Matrix.");

            for(int i = 0; i < rows.Length; i++)
            {
                rows[i].Fill(valuesToFillWith[i]);
            }
            return this;
        }

        /// <summary>
        /// Tests for numeric equality betwen two Matrices
        /// </summary>
        /// <param name="obj">The Matrix to compare.</param>
        /// <returns>True if the two matricies are identical.</returns>
        public override bool Equals(object obj)
        {
            Matrix m = (Matrix)obj;
            if (this.NumRows != m.NumRows) return false;
            for (int i = 0; i < this.NumRows; i++)
            {
                if (!this.GetRow(i).Equals(m.GetRow(i))) return false;
            }
            return true;
        }


        /// <summary>
        /// Returns the Matrix's hash-code.
        /// </summary>
        /// <returns>The Matrix's hash-code.</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (Row r in rows)
            {
                hash ^= r.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Addition operator for two Matrices.
        /// </summary>
        /// <param name="m1">The Matrix to be added to.</param>
        /// <param name="m2">The Matrix to add.</param>
        /// <returns>A Matrix containing the values of the two Matrices added together.</returns>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.NumRows != m2.NumRows || m1.NumCols != m2.NumCols)
            {
                throw new Exception("Added matrices must have the same number of rows and columns. Left hand matrix: " + m1.NumRows + " x " + m1.NumCols + ". Right hand matrix: " + m2.NumRows + " x " + m2.NumCols + ".");
            }
            else
            {
                Matrix m3 = new Matrix(m1.NumRows, m1.NumCols).Fill(0);
                for (int i = 0; i < m1.NumRows; i++)
                {
                    m3[i] = m1[i] + m2[i];
                }
                return m3;
            }
        }

        /// <summary>
        /// Subtraction operator for two Matrices.
        /// </summary>
        /// <param name="m1">The Matrix to be subtracted from.</param>
        /// <param name="m2">The Matrix to subtract.</param>
        /// <returns>A Matrix containing the values of the second Matrrix subtracted from the first Matrix.</returns>
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.NumRows != m2.NumRows || m1.NumCols != m2.NumCols)
            {
                throw new Exception("Subtracted matrices must have the same number of rows and columns. Left hand matrix: " + m1.NumRows + " x " + m1.NumCols + ". Right hand matrix: " + m2.NumRows + " x " + m2.NumCols + ".");
            }
            else
            {
                return m1 + (-1 * m2);
            }
        }

        /// <summary>
        /// Scalar division operator.
        /// </summary>
        /// <param name="m">The Matrix whose values are to be divided.</param>
        /// <param name="scalar">The scalar value by which to divide the values in the Matrix.</param>
        /// <returns>A Matrix containing the values of the Matrix divided by the scalar value.</returns>
        public static Matrix operator /(Matrix m, double scalar)
        {
            return m * (1 / scalar);
        }

        /// <summary>
        /// Scalar multiplication operator.
        /// </summary>
        /// <param name="m">The Matrix to be multiplied by the scalar value.</param>
        /// <param name="scalar">The scalar value by which to multiply the Matrix.</param>
        /// <returns>A Matrix containing each value from the Matrix multiplied by the scalar.</returns>
        public static Matrix operator *(Matrix m, double scalar)
        {
            Matrix m2 = new Matrix(m.NumRows, m.NumCols).Fill(0);
            for (int i = 0; i < m.NumRows; i++)
            {
                m2[i] = m[i] * scalar;
            }
            return m2;
        }

        /// <summary>
        /// Scalar multiplication operator.
        /// </summary>
        /// <param name="scalar">The scalar value by which to multiply the Matrix.</param>
        /// <param name="m">The Matrix to be multiplied by the scalar value.</param>
        /// <returns>A Matrix containing each value from the Matrix multiplied by the scalar.</returns>
        public static Matrix operator *(double scalar, Matrix m)
        {
            return m * scalar;
        }

        /// <summary>
        /// Matrix multiplication operator. Performs "standard" Matrix multiplication. Number of columns of the left hand matrix must be the same as the number of rows of the right matrix.
        /// </summary>
        /// <param name="m1">The left-hand Matrix.</param>
        /// <param name="m2">The right-hand Matrix.</param>
        /// <returns>The Matrix of the result of the multiplication.</returns>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.NumCols != m2.NumRows)
            {
                string message = "Number of columns of the left hand matrix must be the same as the number of rows of the right matrix. Left hand matrix: " + m1.NumRows + " x " + m1.NumCols + ". Right hand matrix: " + m2.NumRows + " x " + m2.NumCols + ".";

                if (m1.NumRows == m2.NumCols)
                {
                    throw new Exception(message + " Try switching the order of multiplication.");
                }
                else
                {
                    throw new Exception(message);
                }
            }
            else
            {
                Matrix m3 = new Matrix(m1.NumRows, m2.NumCols).Fill(0);

                if (m3.NumRows == 1)
                {
                    //because we know that m1.NumCols = m2.NumRows, we know that a row of m1 * a column of m2 is safe and will return a 1x1 matrix.
                    //to stop the recursion... to multiply a 1x? matrix by a ?x1 matrix, add the products of each cell as though the second matrix were pivotted onto the first.
                    m3[0, 0] = 0;
                    for (int z = 0; z < m1.NumCols; z++)
                    {
                        m3[0, 0] += m1[0, z] * m2[z, 0];
                    }
                }
                else
                {
                    for (int i = 0; i < m3.NumRows; i++)
                    {
                        for (int j = 0; j < m3.NumCols; j++)
                        {
                            Matrix single = (m1.ExtractRow(i) * m2.ExtractColumn(j));
                            m3[i, j] = single[0, 0];
                        }
                    }
                }
                return m3;
            }
        }

        /// <summary>Returns the Cross Product of this and rhs. Only defined for 3x1 Matrices.</summary>
        /// <param name="rhs">The right-hand-side Matrix.</param>
        /// <returns>The Cross Product of this and rhs.</returns>
        public Matrix CrossProduct(Matrix rhs)
        {
            if(this.NumCols != 1 || this.NumRows != 3 || rhs.NumCols != 1 || rhs.NumRows != 3)
                throw new Exception("CrossProduct is only defined for 3x1 Matrices");

            Matrix retVal = new Matrix(3, 1);
            retVal[0, 0] = this[1, 0] * rhs[2, 0] - this[2, 0] * rhs[1, 0];
            retVal[1, 0] = this[2, 0] * rhs[0, 0] - this[0, 0] * rhs[2, 0];
            retVal[2, 0] = this[0, 0] * rhs[1, 0] - this[1, 0] * rhs[0, 0];
            return retVal;
        }

        /// <summary>Gets the Norm of this Matrix: the square root of the sum of each of the values squared. Basically, the Distance Formula for the entire Matrix.</summary>
        public double Norm
        {
            get
            {
                double norm = 0; 
                for (int i = 0; i < NumRows; i++)
                {
                    for (int j = 0; j < NumCols; j++)
                    {
                        //square the value
                        norm += this[i, j] * this[i, j]; 
                    }
                }
                return Math.Sqrt(norm);
            }
        }

        /// <summary>
        /// Gets the number of Rows in the Matrix.
        /// </summary>
        public int NumRows
        {
            get { return rows.Length; }
        }

        /// <summary>
        /// Gets the number of Columns in the Matrrix.
        /// </summary>
        public int NumCols
        {
            get { return rows[0].NumValues; }
        }

        /// <summary>
        /// Returns a 1xn Matrix with the values from the specified Row.
        /// </summary>
        /// <param name="rowIndex">The Row whose values will be returned in a 1xn Matrix.</param>
        /// <returns>A 1xn Matrix with the values from the specified Row.</returns>
        public Matrix ExtractRow(int rowIndex)
        {
            return rows[rowIndex].ToMatrix();
        }

        /// <summary>
        /// Returns an nx1 Matrix with the values from the specified Column.
        /// </summary>
        /// <param name="colIndex">The Column whose values will be returned in an nx1 Matrix.</param>
        /// <returns>An nx1 Matrix with the values from the specified Column.</returns>
        public Matrix ExtractColumn(int colIndex)
        {
            Matrix m = new Matrix(NumRows, 1).Fill(0);
            for (int i = 0; i < this.NumRows; i++)
            {
                m[i, 0] = this[i, colIndex];
            }
            return m;
        }

        /// <summary>
        /// Transposes the Matrix.
        /// </summary>
        /// <returns>A Matrix with the values of this Matrix flipped about the diagonal.</returns>
        public Matrix Transpose()
        {
            Matrix m = new Matrix(NumCols, NumRows).Fill(0);
            for (int i = 0; i < m.NumRows; i++)
            {
                for (int j = 0; j < m.NumCols; j++)
                {
                    m[i, j] = this[j, i];
                }
            }
            return m;
        }

        /// <summary>
        /// Get the Matrix's inverse.
        /// </summary>
        /// <returns>This Matrix's inverse Matrix.</returns>
        public Matrix Inverse()
        {
            if (NumCols != NumRows)
                throw new Exception("This implementation of Matrix inversion can only handle square Matrices");

            Matrix m = new Matrix(this);
            //use Gaussian Elimination to find the inverse. When the lhs is eliminated, the rhs will contain the inverse.
            m = m.Augment(new Matrix(NumRows, NumCols));

            m = GaussianEliminator.Solve(m);

            Matrix inverse = new Matrix(NumRows, NumCols);

            for (int i = 0; i < NumRows; i++)
            {
                for (int j = 0; j < NumCols; j++)
                {
                    inverse[i, j] = m[i, j + NumCols];
                }
            }

            return inverse;
        }

        public double Determinant()
        {
            if (NumRows != NumCols)
                throw new Exception("Can not find the Determinants of a non-square Matrix");

            if (NumRows == 1)
                return this[0, 0]; //the determinant of a 1x1 matrix is its only value.

            double det = 0;
            for (int i = 0; i < NumCols; i++)
            {
                //add (sign = 1) determinants of odd-numbered (1-based indexed) Columns
                //subtract (sign = -1) determinants of even-numbered (1-based indexed) Columns
                int sign = ((i + 1) % 2) * 2 - 1;
                det += this[0,i] * this.SubMatrix(i).Determinant() * sign;
            }
            return det;
        }

        /// <summary>Returns a sub-matrix of this Matrix with the top row and specified column excluded.</summary>
        /// <param name="colToExclude">The 0-based index of the Column to exclude.</param>
        /// <returns>The sub-matrix.</returns>
        private Matrix SubMatrix(int colToExclude)
        {
            if (NumRows < 2 || NumCols < 2)
                throw new Exception("SubMatrix is not defined for Matrices with less than 2 Rows or Columns");

            Matrix m = new Matrix(NumRows - 1, NumCols - 1);

            int skipNumCols = 0; //don't skip any columns initially

            for (int c = 0; c < m.NumCols; c++)
            {
                if (c == colToExclude) skipNumCols = 1; //start skipping columns if we're at the column to skip
                for (int r = 0; r < m.NumRows; r++)
                {
                    m[r, c] = this[r + 1, c + skipNumCols];
                }
            }
            return m;
        }


        /// <summary>
        /// Augments a Matrix on the right side of this Matrix.
        /// </summary>
        /// <param name="rhs">The Matrix to Augment.</param>
        /// <returns>A Matrix with the values from rhs concatonated to the right of this.</returns>
        public Matrix Augment(Matrix rhs)
        {
            if (this.NumRows != rhs.NumRows)
            {
                throw new Exception("The matrices being augmented must have the same number of rows. Left hand matrix: " + this.NumRows + " rows. Right hand matrix: " + rhs.NumRows + " rows.");
            }
            else
            {
                Matrix m = new Matrix(this.NumRows, this.NumCols + rhs.NumCols).Fill(0);
                for (int i = 0; i < m.NumRows; i++)
                {
                    //fill the left side
                    for (int j = 0; j < this.NumCols; j++)
                    {
                        m[i, j] = this[i, j];
                    }
                    //fill the right side
                    for (int j = 0; j < rhs.NumCols; j++)
                    {
                        m[i, j + this.NumCols] = rhs[i, j];
                    }
                }
                return m;
            }
        }

        /// <summary>
        /// Uses scalar multiplication and row addition to "eliminate" the specified value to 0 while retaining an equivalent Matrix.
        /// </summary>
        /// <param name="rowIndex">The 0-based Row index of the value to eliminate.</param>
        /// <param name="colIndex">The 0-based Column index of the value to eliminate.</param>
        public void Eliminate(int rowIndex, int colIndex)
        {
            //refuse to eliminate anything in the top row
            if (rowIndex == 0) throw new Exception("Can not eliminate any cell in the top row");

            //our goal here is to make the value at (rowIndex, colIndex) zero by scalar multiplication and row addition

            //scalar multiple = - (passed-in cell / cell above on diagonal (cell with rowindex = colindex))
            double scalarMultiple = -1 * this[rowIndex, colIndex] / this[colIndex, colIndex]; //rowIndex - 1 is safe because we already checked that we're not on row 0

            //multiply the modifier row (the row used in the scalar multiple denominator - it has the same index as the column index) by the 
            Row temp = this[colIndex] * scalarMultiple;

            //now add the original row to make the passed-in cell 0
            temp = temp + this[rowIndex];

            this[rowIndex] = temp;

            double finalVal = this[rowIndex, colIndex];
        }

        /// <summary>
        /// Returns a textual-representation of the Matrix, defaulting to 2 decimal places for each value.
        /// </summary>
        /// <returns>A textual-representation of the Matrix.</returns>
        public override string ToString()
        {
            return ToString(2);
        }

        /// <summary>
        /// Returns a textual-representation of the Matrix with values rounded to the specified number of decimal places.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places to which to round the Matrix values.</param>
        /// <returns>A textual-representation of the Matrix.</returns>
        public string ToString(int decimalPlaces)
        {
            StringBuilder b = new StringBuilder();

            //int fixedNumberWidth = LengthOfLongestValue(decimalPlaces);

            if (NumRows > 0)
            {
                for (int i = 0; i < NumRows; i++)
                {

                    b.Append("[");

                    for (int j = 0; j < NumCols; j++)
                    {
                        b.Append("  ");
                        int longestLengthInCol = this.ExtractColumn(j).LengthOfLongestValue(decimalPlaces);
                        string valStr = this[i, j].ToString("N" + decimalPlaces.ToString());
                        b.Append(valStr.PadLeft(longestLengthInCol));
                    }
                    b.Append("  ]");

                    if (i < (NumRows - 1)) b.AppendLine();
                }
            }
            else
            {
                b.Append("[  ]");
            }

            return b.ToString();
        }

        /// <summary>
        /// Returns the length of the "longest" value in the Matrix. "Longest" means the number of characters in the textual representation of the value rounded to the specified number of decimal places.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places to which to round the Matrix values.</param>
        /// <returns>The length of the longest value in the Matrix.</returns>
        private int LengthOfLongestValue(int decimalPlaces)
        {
            int length = 0;
            foreach (Row r in rows)
            {
                length = Math.Max(length, r.LengthOfLongestValue(decimalPlaces));
            }
            return length;
        }


        public Matrix Translate(double offsetX, double offsetY, double offsetZ)
        {
            if (NumRows != 4 || NumCols != 4)
                throw new Exception("Translate operation only applies to 4x4 Matrices.");

            Matrix translateMatrix = new Matrix(4);
            translateMatrix[0, 3] = offsetX;
            translateMatrix[1, 3] = offsetY;
            translateMatrix[2, 3] = offsetZ;

            Matrix result = translateMatrix * this;
            Fill(result);
            return this;
        }

        public Matrix Scale(double scaleX, double scaleY, double scaleZ)
        {
            if (NumRows != 4 || NumCols != 4)
                throw new Exception("Scale operation only applies to 4x4 Matrices.");

            Matrix translateMatrix = new Matrix(4);
            translateMatrix[0, 0] = scaleX;
            translateMatrix[1, 1] = scaleY;
            translateMatrix[2, 2] = scaleZ;

            Matrix result = translateMatrix * this;
            Fill(result);
            return this;
        }


    }
}
