/*
 *  File Name:   UnitTest1.cs
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
 * Date: 24/08/2021
 * ****************************************************************
 */

namespace TestProject1
{
    using NUnit.Framework;

    using SortingLib;

    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="Tests" />.
    /// </summary>
    public class Tests
    {
        private static readonly int[] TEST_DATA = { 5, 10, 4, 1, 24, 13, 40, 23 };

        /// <summary>
        /// The Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// The Test1.
        /// </summary>
        [Test]
        public void Test1()
        {
            var list = new MyList(TEST_DATA);

            var helper = new Helper(list, -1, 10000, 1000000);
            helper.SortIt(Sorting.ArraySort, $"Array.Sort", null);
            var ls = list.ToArray();
            Console.Error.WriteLine(list);

            list = new MyList(TEST_DATA);
            helper = new Helper(list, -1, 10000, 1000000);
            helper.SortIt(Sorting.TopDownMergeSort, $"Top-down Merge Sort", null);
            Console.Error.WriteLine(list);

            for (int i = 0; i < ls.Length; i++)
            {
                Assert.IsTrue(ls[i] == list[i], "[{0}]: {1} - {2}", i, ls[i], list[i]);
            }
        }

        private class MyList : List<int>
        {
            public MyList()
            {
            }

            public MyList(IEnumerable<int> collection) : base(collection)
            {
            }

            public MyList(int capacity) : base(capacity)
            {
            }

            public override string ToString()
            {
                StringBuilder rtn = new StringBuilder();

                foreach (var item in this)
                {
                    rtn.Append($"{item}, ");
                }

                return rtn.ToString();
            }
        }
    }
}