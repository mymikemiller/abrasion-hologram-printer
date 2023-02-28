using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    /// <summary>
    /// Performs Gaussian Elimination.
    /// </summary>
    static class GaussianEliminator
    {
        /// <summary>
        /// Solves the given Matrix using Gaussian Elimination.
        /// </summary>
        /// <param name="toSolve">The Matrix to solve. Must have more Columns than Rows.</param>
        /// <returns>The solved Matrix.</returns>
        public static Matrix Solve(Matrix toSolve)
        {
            if (toSolve.NumCols < toSolve.NumRows)
            {
                throw new Exception("To use Gaussian Elimination, the supplied matrix must have at least as many columns as rows. Use the matrix Augment function to attach the solution matrix.");
            }
            else
            {
                Matrix m = new Matrix(toSolve);

                //put in upper-triangular form
                m = UpperTriangularize(m);

                Matrix solved = BackSubstitute(m);

                return solved;
            }
        }

        /// <summary>
        /// Converts a Matrix to upper-triangular form. Used in the Solve() function. It is not necessary to call this method - it is made available for convenience.
        /// </summary>
        /// <param name="toTriangularize">The Matrix to convert.</param>
        /// <returns>An upper-triangular version of the Matrix.</returns>
        public static Matrix UpperTriangularize(Matrix toTriangularize)
        {
            Matrix m = new Matrix(toTriangularize);

            //our goal here is to make sure all the numbers along the diagonal are non-zero, but all numbers below that in each col are 0
            for (int i = 1; i < m.NumRows; i++) //loop through each row below the first
            {
                for (int j = 0; j < i; j++) //for each row, loop through to the cell before the diagonal (at diagonal, j = i, so do while j < i)
                {
                    //may need to do a pivot to make sure we don't divide by zero
                    m.Eliminate(i, j);
                }
            }

            return m;
        }

        /// <summary>
        /// Back-substitutes the Matrix to form the solution Matrix. Used in the Solve() function.
        /// </summary>
        /// <param name="toBackSubstitute">The matrix to back-substitute. Must be in upper-triangular form.</param>
        /// <returns>The solved Matrix.</returns>
        private static Matrix BackSubstitute(Matrix toBackSubstitute)
        {
            Matrix m = new Matrix(toBackSubstitute);

            for (int i = m.NumRows - 1; i >= 0; i--) //loop through the rows backwards
            {
                double diagonalValue = m[i, i];

                //we want the diagonal values to be 1, so divide the entire row by its diagonal value
                m[i] /= diagonalValue;

                if (i < m.NumRows - 1) //...if we're not on the last row
                {
                    //there might be numbers to the right of the diagonal. we want them to be 0's. 
                    //we know that there is a row below this that has already been solved and has all 0's and only a 1 on the diagonal
                    //so, for each row below this, subtract (that row * this row's value that we want to get rid of)
                    for (int j = m.NumRows - 1; j > i; j--) //for each row below, starting at the bottom
                    {
                        m[i] -= m[j] * m[i, j];
                    }
                }
            }

            return m;
        }
    }
}
