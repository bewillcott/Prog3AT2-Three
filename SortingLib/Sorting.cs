/*
 *  File Name:   Sorting.cs
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

using System.Collections.Generic;
using System.ComponentModel;

namespace SortingLib
{
    /// <summary>
    /// Defines the <see cref="Sorting" />.
    /// </summary>
    public static class Sorting
    {
        /// <summary>
        ///The Heap Sort algorithm.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// O(n Log n)
        /// This code was copied from:<br/>
        /// https://www.geeksforgeeks.org/heap-sort/
        /// <br/>and cleaned up.<br/>
        /// - Renamed parameter<br/>
        /// - Renamed variables<br/>
        /// - Replaced some code with method call
        /// </remarks>
        public static void HeapSort(IList<int> list, BackgroundWorker bw)
        {
            var numOfElements = list.Count;

            // Build heap (rearrange array)
            for (int i = numOfElements / 2 - 1; i >= 0 && (bw == null || !bw.CancellationPending); i--)
                Heapify(list, numOfElements, i, bw);

            // One by one extract an element from heap
            for (int i = numOfElements - 1; i > 0 && (bw == null || !bw.CancellationPending); i--)
            {
                // Move current root to end
                // (BW) replaced with method call
                Exchange(list, 0, i);

                // call max heapify on the reduced heap
                Heapify(list, i, 0, bw);
            }
        }

        /// <summary>
        /// The Merge Sort algorithm.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// Merge sort is based on the divide-and-conquer paradigm.<br/>
        /// This code was copied from:<br/>
        /// https://www.csharpstar.com/merge-sort-csharp-program/
        /// <br/>and cleaned up.
        /// </remarks>
        public static void MergeSort(IList<int> list, BackgroundWorker bw)
        {
            MergeSort_Divide(list, 0, list.Count - 1, bw);
        }

        /// <summary>
        /// The Quick Sort algorithm.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// Worst case: O(N²)<br/>
        /// Best case : O(N log N)<br/>
        /// This code was copied from:<br/>
        /// http://anh.cs.luc.edu/170/notes/CSharpHtml/sorting.html
        /// <br/>and cleaned up.
        /// </remarks>
        public static void QuickSort(IList<int> list, BackgroundWorker bw)
        {
            IntArrayQuickSort(list, 0, list.Count - 1, bw);
        }

        /// <summary>
        /// Exchanges the specified data.
        /// </summary>
        /// <remarks>
        /// This code was copied from:<br/>
        /// http://anh.cs.luc.edu/170/notes/CSharpHtml/sorting.html
        /// <br/>and cleaned up.<br/>
        /// - Renamed parameter
        /// </remarks>
        /// <param name="list">The data.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        private static void Exchange(IList<int> list, int m, int n)
        {
            var temporary = list[m];
            list[m] = list[n];
            list[n] = temporary;
        }

        /// <summary>
        /// Heapifies the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="sizeOfHeap">The size of heap.</param>
        /// <param name="indexOfRoot">The index of root.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// This code was copied from:<br/>
        /// https://www.geeksforgeeks.org/heap-sort/
        /// <br/>and cleaned up.<br/>
        /// - Renamed parameters<br/>
        /// - Renamed variables<br/>
        /// - Replaced some code with method call
        /// </remarks>
        private static void Heapify(IList<int> list, int sizeOfHeap,
            int indexOfRoot, BackgroundWorker bw)
        {
            var indexOfLargest = indexOfRoot; // Initialize largest as root
            var leftIndex = 2 * indexOfRoot + 1; // left = 2*i + 1
            var rightIndex = 2 * indexOfRoot + 2; // right = 2*i + 2

            // If left child is larger than root
            if (leftIndex < sizeOfHeap && list[leftIndex] > list[indexOfLargest])
                indexOfLargest = leftIndex;

            // If right child is larger than largest so far
            if (rightIndex < sizeOfHeap && list[rightIndex] > list[indexOfLargest])
                indexOfLargest = rightIndex;

            // If largest is not root
            if (indexOfLargest != indexOfRoot && (bw == null || !bw.CancellationPending))
            {
                // (BW) Replaced code with method call
                Exchange(list, indexOfRoot, indexOfLargest);

                // Recursively heapify the affected sub-tree
                Heapify(list, sizeOfHeap, indexOfLargest, bw);
            }
        }

        /// <summary>
        /// Does a recursive quick sort.
        /// </summary>
        /// <param name="list">The list of integers.</param>
        /// <param name="leftIndex">The left index.</param>
        /// <param name="rightIndex">The right index.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// This code was copied from:<br/>
        /// http://anh.cs.luc.edu/170/notes/CSharpHtml/sorting.html
        /// <br/>and cleaned up.
        /// - Renamed parameters
        /// </remarks>
        private static void IntArrayQuickSort(IList<int> list, int leftIndex,
            int rightIndex, BackgroundWorker bw)
        {
            int i, j;
            int x;

            i = leftIndex;
            j = rightIndex;

            x = list[(leftIndex + rightIndex) / 2]; /* find pivot item */

            while (bw == null || !bw.CancellationPending)
            {
                while (list[i] < x)
                    i++;
                while (x < list[j])
                    j--;
                if (i <= j)
                {
                    Exchange(list, i, j);
                    i++;
                    j--;
                }
                if (i > j)
                    break;
            }

            if (leftIndex < j)
                IntArrayQuickSort(list, leftIndex, j, bw);
            if (i < rightIndex)
                IntArrayQuickSort(list, i, rightIndex, bw);
        }

        /// <summary>
        /// Sort each sub-array merging them back together.
        /// </summary>
        /// <param name="list">The integer array.</param>
        /// <param name="leftIndex">The left index.</param>
        /// <param name="midIndex">The middle index.</param>
        /// <param name="rightIndex">The right index.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// First sub-array is <paramref name="list"/>[<paramref name="leftIndex"/> ..
        /// <paramref name="midIndex"/>]<br/>
        /// Second sub-array is <paramref name="list"/>[<paramref name="midIndex"/> +
        /// <paramref name="leftIndex"/> .. <paramref name="rightIndex"/>]<br/>
        /// This code was copied from:<br/>
        /// https://www.geeksforgeeks.org/merge-sort/
        /// <br/>and cleaned up.
        /// </remarks>
        private static void MergeSort_Conquer(IList<int> list, int leftIndex, int midIndex,
            int rightIndex, BackgroundWorker bw)
        {
            // Find sizes of two
            // sub-arrays to be merged
            var leftArraySize = midIndex - leftIndex + 1;
            var rightArraySize = rightIndex - midIndex;

            // Create temp arrays
            var leftArray = new int[leftArraySize];
            var rightArray = new int[rightArraySize];
            int leftArrayIndex, rightArrayIndex;

            // Copy data to temp arrays
            for (leftArrayIndex = 0; leftArrayIndex < leftArraySize &&
                (bw == null || !bw.CancellationPending); ++leftArrayIndex)
            {
                leftArray[leftArrayIndex] = list[leftIndex + leftArrayIndex];
            }

            for (rightArrayIndex = 0; rightArrayIndex < rightArraySize &&
                (bw == null || !bw.CancellationPending); ++rightArrayIndex)
            {
                rightArray[rightArrayIndex] = list[midIndex + 1 + rightArrayIndex];
            }

            // Merge the temp arrays

            // Initial indexes of first
            // and second sub-arrays
            leftArrayIndex = 0;
            rightArrayIndex = 0;

            // Initial index of merged sub-array
            var listIndex = leftIndex;

            while (leftArrayIndex < leftArraySize && rightArrayIndex < rightArraySize &&
                (bw == null || !bw.CancellationPending))
            {
                // Sort back into original list array
                if (leftArray[leftArrayIndex] <= rightArray[rightArrayIndex])
                {
                    list[listIndex] = leftArray[leftArrayIndex];
                    leftArrayIndex++;
                }
                else
                {
                    list[listIndex] = rightArray[rightArrayIndex];
                    rightArrayIndex++;
                }

                listIndex++;
            }

            // Copy remaining elements of leftArray, if any
            while (leftArrayIndex < leftArraySize && (bw == null || !bw.CancellationPending))
            {
                list[listIndex] = leftArray[leftArrayIndex];
                leftArrayIndex++;
                listIndex++;
            }

            // Copy remaining elements of rightArray, if any
            while (rightArrayIndex < rightArraySize && (bw == null || !bw.CancellationPending))
            {
                list[listIndex] = rightArray[rightArrayIndex];
                rightArrayIndex++;
                listIndex++;
            }
        }

        /// <summary>
        /// Divide the list into working sub-lists.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="leftIndex">Index of the left.</param>
        /// <param name="rightIndex">Index of the right.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// First sub-array is arr[l..m]<br/>
        /// Second sub-array is arr[m+1..r]<br/>
        /// This code was copied from:<br/>
        /// https://www.geeksforgeeks.org/merge-sort/
        /// <br/>and cleaned up.
        /// </remarks>
        private static void MergeSort_Divide(IList<int> list, int leftIndex,
            int rightIndex, BackgroundWorker bw)
        {
            if (leftIndex < rightIndex)
            {
                // Find the middle
                // point
                var midIndex = leftIndex + (rightIndex - leftIndex) / 2;

                // Sort first and
                // second halves
                MergeSort_Divide(list, leftIndex, midIndex, bw);
                MergeSort_Divide(list, midIndex + 1, rightIndex, bw);

                // Merge the sorted halves
                MergeSort_Conquer(list, leftIndex, midIndex, rightIndex, bw);
            }
        }
    }
}