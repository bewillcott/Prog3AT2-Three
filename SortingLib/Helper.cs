/*
 *  File Name:   Helper.cs
 *
 *  Copyright (c) 2021 Bradley Willcott
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * ****************************************************************
 * Name: Bradley Willcott
 * ID:   M198449
 * Date: 21/08/2021
 * ****************************************************************
 */

namespace SortingLib
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Defines the <see cref="Helper" />.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Defines the list.
        /// </summary>
        private readonly int[] list;

        /// <summary>
        /// Defines the seed.
        /// </summary>
        private readonly int seed;

        /// <summary>
        /// Defines the min.
        /// </summary>
        private readonly int min;

        /// <summary>
        /// Defines the max.
        /// </summary>
        private readonly int max;

        /// <summary>
        /// Defines the watch.
        /// </summary>
        private readonly Stopwatch watch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Helper"/> class.
        /// </summary>
        /// <param name="list">The list<see cref="int[]"/>.</param>
        /// <param name="seed">The seed<see cref="int"/>.</param>
        /// <param name="min">The min<see cref="int"/>.</param>
        /// <param name="max">The max<see cref="int"/>.</param>
        public Helper(int[] list, int seed, int min, int max)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
            this.seed = seed;
            this.min = min;
            this.max = max;
            watch = new();
        }

        /// <summary>
        /// Generate an int array of random numbers.
        /// </summary>
        private void IntArrayGenerate()
        {
            var r = new Random(seed);

            for (int i = 0; i < list.Length; i++)
                list[i] = r.Next(min, max);
        }

        /// <summary>
        /// Sorts it.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="text">The text.</param>
        /// <returns>The elapsed time.</returns>
        public double SortIt(Action<int[]> methodName, string text)
        {
            Console.Write($"Processing {text} => ");
            double elapsedTime;  // time in second, accurate to about milliseconds
            IntArrayGenerate();

            watch.Restart();
            methodName?.Invoke(list);
            watch.Stop();

            elapsedTime = watch.ElapsedMilliseconds / 1000.0;
            Console.WriteLine("{0:F3}", elapsedTime);

            return elapsedTime;
        }
    }
}