/*
 *  File Name:   Program.cs
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
 * Date: 20/08/2021
 * ****************************************************************
 */

namespace ConsoleApp
{
    using SortingLib;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The array size
        /// </summary>
        private const int ARRAY_SIZE = 10000000;

        /// <summary>
        /// The maximum salary
        /// </summary>
        private const int MAX_SALARY = 10000000;

        /// <summary>
        /// The minimum salary
        /// </summary>
        private const int MIN_SALARY = 10000;

        /// <summary>
        /// The random seed
        /// </summary>
        private const int RANDOM_SEED = 1234;

        /// <summary>
        /// The list
        /// </summary>
        private static readonly List<int> list = new(ARRAY_SIZE);

        /// <summary>
        /// The number of test runs
        /// </summary>
        private const int NUM_OF_TEST_RUNS = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        protected Program()
        {
        }

        /// <summary>
        /// The Main.
        /// </summary>
        public static void Main()
        {
            var helper = new Helper(list, RANDOM_SEED, MIN_SALARY, MAX_SALARY);
            var results = new double[4];
            var canceled = new Ref<bool>(false);

            for (int i = 0; i < NUM_OF_TEST_RUNS; i++)
            {
                results[0] += helper.SortIt(null, $"list.Sort [{i + 1}]", canceled);
                results[1] += helper.SortIt(Sorting.HeapSort, $"Heap Sort [{i + 1}]", canceled);
                results[2] += helper.SortIt(Sorting.MergeSort, $"Merge Sort [{i + 1}]", canceled);
                results[3] += helper.SortIt(Sorting.QuickSort, $"Quick Sort [{i + 1}]", canceled);
            }

            Console.WriteLine($"Average results over {NUM_OF_TEST_RUNS} test runs:");
            Console.WriteLine($" - list.Sort  : {results[0] / NUM_OF_TEST_RUNS:F3}");
            Console.WriteLine($" - HeapSort   : {results[1] / NUM_OF_TEST_RUNS:F3}");
            Console.WriteLine($" - MergeSort  : {results[2] / NUM_OF_TEST_RUNS:F3}");
            Console.WriteLine($" - QuickSort  : {results[3] / NUM_OF_TEST_RUNS:F3}");
        }
    }
}