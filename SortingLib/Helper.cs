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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Defines the <see cref="Helper" />.
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Defines the list.
        /// </summary>
        private readonly List<int> list;

        /// <summary>
        /// Defines the max.
        /// </summary>
        private readonly int max;

        /// <summary>
        /// Defines the min.
        /// </summary>
        private readonly int min;

        /// <summary>
        /// Defines the seed.
        /// </summary>
        private readonly int seed;

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
        public Helper(List<int> list, int seed, int min, int max)
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
        public void GenerateIntArray()
        {
            if (seed != -1)
            {
                var r = new Random(seed);
                list.Clear();
                for (int i = 0; i < list.Capacity; i++)
                {
                    list.Add(r.Next(min, max));
                }
            }
        }

        /// <summary>
        /// Sorts it.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="text">The text.</param>
        /// <param name="bw">Worker thread.</param>
        /// <returns>The elapsed time.</returns>
        public double SortIt(Action<int[], BackgroundWorker> methodName, string text, BackgroundWorker bw)
        {
            Console.Write($"Processing {text} => ");
            double elapsedTime;  // time in second, accurate to about milliseconds
            GenerateIntArray();

            if (methodName != null)
            {
                // create working copy
                var listArray = new int[list.Count];
                list.CopyTo(listArray, 0);

                watch.Restart();
                methodName?.Invoke(listArray, bw);
                watch.Stop();

                // Copy sorted array back over the 'list'
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = listArray[i];
                }
            }
            else
            {
                watch.Restart();
                list.Sort();
                watch.Stop();
            }

            elapsedTime = watch.ElapsedMilliseconds / 1000.0;
            Console.WriteLine($"{elapsedTime:F3}");

            return elapsedTime;
        }
    }
}