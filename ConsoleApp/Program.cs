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
    using NUnit.Framework;

    using SortingLib;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
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
        /// The filename
        /// </summary>
        private const string FILENAME = @"sort_duration.csv";

        /// <summary>
        /// The maximum salary
        /// </summary>
        private const int MAX_SALARY = 1000000;

        /// <summary>
        /// The minimum salary
        /// </summary>
        private const int MIN_SALARY = 10000;

        /// <summary>
        /// The number of test runs
        /// </summary>
        private const int NUM_OF_TEST_RUNS = 10;

        /// <summary>
        /// The random seed
        /// </summary>
        private const int RANDOM_SEED = 1234;

        /// <summary>
        /// The list
        /// </summary>
        private static readonly List<int> list = new(ARRAY_SIZE);

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
            var rows = new CsvRow[] {
                new(@"Array.Sort"),
                new(@"Heap Sort"),
                new(@"Merge Sort"),
                new(@"Top-down Merge Sort"),
                new(@"Quick Sort")
            };

            var ls = Array.Empty<int>();
            var tdms = Array.Empty<int>();

            for (int i = 0; i < NUM_OF_TEST_RUNS; i++)
            {
                rows[0].Add(helper.SortIt(Sorting.ArraySort, $"Array.Sort [{i + 1}]", null));
                ls = list.ToArray();
                rows[1].Add(helper.SortIt(Sorting.HeapSort, $"Heap Sort [{i + 1}]", null));
                rows[2].Add(helper.SortIt(Sorting.MergeSort, $"Merge Sort [{i + 1}]", null));
                rows[3].Add(helper.SortIt(Sorting.TopDownMergeSort, $"Top-down Merge Sort [{i + 1}]", null));
                tdms = list.ToArray();
                rows[4].Add(helper.SortIt(Sorting.QuickSort, $"Quick Sort [{i + 1}]", null));
                Console.WriteLine(@"--------------------------------------------------------------");
            }

            Console.WriteLine($"Writing data to CSV filename: {FILENAME}");

            if (WriteCSVData(FILENAME, rows))
            {
                Console.WriteLine(@"Data written.");
            }
            else
            {
                Console.WriteLine(@"Failed to write data.");
            }

            Console.WriteLine($"\nAverage results over {NUM_OF_TEST_RUNS} test runs:");
            Console.WriteLine($" - Array.Sort           : {rows[0].Avg:F3}");
            Console.WriteLine($" - HeapSort            : {rows[1].Avg:F3}");
            Console.WriteLine($" - MergeSort           : {rows[2].Avg:F3}");
            Console.WriteLine($" - Top-down MergeSort  : {rows[3].Avg:F3}");
            Console.WriteLine($" - QuickSort           : {rows[4].Avg:F3}");

            for (int i = 0; i < ls.Length; i++)
            {
                Assert.IsTrue(ls[i] == tdms[i], $"Top-down Merge Sort failed at [{i}]");
            }
        }

        /// <summary>
        /// Writes the CSV data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="rows">The rows.</param>
        /// <returns></returns>
        private static bool WriteCSVData(string fileName, CsvRow[] rows)
        {
            var rtn = false;

            if (!(String.IsNullOrWhiteSpace(fileName)))
            {
                try
                {
                    using (StreamWriter sw = new(fileName))
                    {
                        foreach (var row in rows)
                        {
                            sw.WriteLine(row.CSVString());
                        }
                    }

                    rtn = true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"ERROR: Failed to write to CSV file!\n{ex.Message}");
                }
            }

            return rtn;
        }

        /// <summary>
        /// Contains data relevant to a single CSV row.
        /// </summary>
        /// <remarks>
        /// The information is specific to a single sorting algorithm, and the
        /// data collected over multiple test runs.
        /// <para/>
        /// The original idea for this class came from reading this site:<br/>
        /// https://www.codeproject.com/articles/415732/reading-and-writing-csv-files-in-csharp
        /// </remarks>
        /// <seealso cref="System.Collections.Generic.List&lt;System.Double&gt;" />
        private class CsvRow : List<double>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CsvRow"/> class.
            /// </summary>
            /// <param name="algorithm">The algorithm.</param>
            public CsvRow(string algorithm)
            {
                Algorithm = algorithm;
            }

            /// <summary>
            /// Gets or sets the algorithm.
            /// </summary>
            public string Algorithm { get; set; }

            /// <summary>
            /// Gets the average.
            /// </summary>
            /// <value>
            /// The average.
            /// </value>
            public double Avg
            {
                get
                {
                    double total = 0;

                    foreach (var item in this)
                    {
                        total += item;
                    }

                    return total / Count;
                }
            }

            /// <summary>
            /// Parses a CSV string.
            /// </summary>
            /// <param name="csvString">The CSV string.</param>
            /// <returns>A new <see cref="CsvRow"/>.</returns>
            public static CsvRow ParseCSV(string csvString)
            {
                CsvRow rtn = null;

                if (!String.IsNullOrWhiteSpace(csvString))
                {
                    var data = csvString.Split(',');

                    if (data.Length > 0)
                    {
                        try
                        {
                            rtn = new(data[0]);
                            rtn.Algorithm = data[0];

                            foreach (var item in data)
                            {
                                rtn.Add(double.Parse(item));
                            }
                        }
                        catch (Exception)
                        { // Quietly ignore it }
                            rtn = null;
                        }
                    }
                }

                return rtn;
            }

            /// <summary>
            /// Gets the CSV string for this row.
            /// </summary>
            /// <returns></returns>
            public string CSVString()
            {
                return ToString();
            }

            /// <summary>
            /// Converts to string.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                var sb = new StringBuilder(Algorithm);

                foreach (var item in this)
                {
                    sb.Append(',').Append(item);
                }

                return sb.ToString();
            }
        }
    }
}