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

using System;
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
        /// Sorts the <paramref name="listArray"/> using the <see cref="Array.Sort(Array)"/> algorithm.
        /// </summary>
        /// <param name="listArray">The list.</param>
        /// <param name="bw">The bw.</param>
        public static void ArraySort(int[] listArray, BackgroundWorker bw)
        {
            Array.Sort<int>(listArray);
        }

        /// <summary>
        ///The Heap Sort algorithm.
        /// </summary>
        /// <param name="listArray">The list.</param>
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
        public static void HeapSort(int[] listArray, BackgroundWorker bw)
        {
            var numOfElements = listArray.Length;

            // Build heap (rearrange array)
            for (int i = numOfElements / 2 - 1; i >= 0 && (bw == null || !bw.CancellationPending); i--)
                Heapify(listArray, numOfElements, i, bw);

            // One by one extract an element from heap
            for (int i = numOfElements - 1; i > 0 && (bw == null || !bw.CancellationPending); i--)
            {
                // Move current root to end
                // (BW) replaced with method call
                Exchange(listArray, 0, i);

                // call max heapify on the reduced heap
                Heapify(listArray, i, 0, bw);
            }
        }

        /// <summary>
        /// The Merge Sort algorithm.
        /// </summary>
        /// <param name="listArray">The list.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// Merge sort is based on the divide-and-conquer paradigm.<br/>
        /// This code was copied from:<br/>
        /// https://www.csharpstar.com/merge-sort-csharp-program/
        /// <br/>and cleaned up.
        /// </remarks>
        public static void MergeSort(int[] listArray, BackgroundWorker bw)
        {
            MergeSort_Divide(listArray, 0, listArray.Length - 1, bw);
        }

        /// <summary>
        /// The Quick Sort algorithm.
        /// </summary>
        /// <param name="listArray">The list.</param>
        /// <param name="bw">Worker thread.</param>
        /// <remarks>
        /// Worst case: O(N²)<br/>
        /// Best case : O(N log N)<br/>
        /// This code was copied from:<br/>
        /// http://anh.cs.luc.edu/170/notes/CSharpHtml/sorting.html
        /// <br/>and cleaned up.
        /// </remarks>
        public static void QuickSort(int[] listArray, BackgroundWorker bw)
        {
            IntArrayQuickSort(listArray, 0, listArray.Length - 1, bw);
        }

        /// <summary>
        /// Top-down Merge Sort algorithm.
        /// </summary>
        /// <remarks>
        /// This is an implementation of the 'C-like code' here:<br/>
        /// https://en.wikipedia.org/wiki/Merge_sort
        /// <br/>under the title: <b>Top-down implementation</b>.
        /// <para/>
        /// I have made some logic changes, as I believe they have the sort from to arrays
        /// around the wrong way.
        /// </remarks>
        /// <param name="listArray">The list.</param>
        /// <param name="bw">Worker thread.</param>
        public static void TopDownMergeSort(int[] listArray, BackgroundWorker bw)
        {
            var B = new int[listArray.Length];
            var A = new int[listArray.Length];

            // One-time copy
            listArray.CopyTo(B, 0);
            listArray.CopyTo(A, 0);

            // Sort data from B[] to A[]
            TopDownSplitMerge(B, 0, B.Length, A, bw);

            for (int i = 0; i < listArray.Length; i++)
            {
                listArray[i] = A[i];
            }
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
        private static void Exchange(int[] list, int m, int n)
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
        private static void Heapify(int[] list, int sizeOfHeap,
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
        private static void IntArrayQuickSort(int[] list, int leftIndex,
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
        private static void MergeSort_Conquer(int[] list, int leftIndex, int midIndex,
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
        private static void MergeSort_Divide(int[] list, int leftIndex,
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

        /// <summary>
        /// Tops down merge.
        /// </summary>
        /// <param name="A">The working copy.</param>
        /// <param name="iBegin">The i begin.</param>
        /// <param name="iMiddle">The i middle.</param>
        /// <param name="iEnd">The i end.</param>
        /// <param name="B">The list.</param>
        /// <param name="bw">Worker thread.</param>
        // Left source half is A[ iBegin:iMiddle-1].
        // Right source half is A[iMiddle:iEnd-1   ].
        // Result is            B[ iBegin:iEnd-1   ].
        private static void TopDownMerge(int[] A, int iBegin, int iMiddle, int iEnd, int[] B, BackgroundWorker bw)
        {
            var i = iBegin;

            var j = iMiddle;

            for (int k = iBegin; k < iEnd && (bw == null || !bw.CancellationPending); k++)
            {
                if (i < iMiddle && (j >= iEnd || A[i] <= A[j]))
                {
                    B[k] = A[i];
                    i++;
                }
                else
                {
                    B[k] = A[j];
                    j++;
                }
            }
        }

        /// <summary>
        /// Tops down split merge.
        /// </summary>
        /// <param name="B">The working copy.</param>
        /// <param name="iBegin">The i begin.</param>
        /// <param name="iEnd">The i end.</param>
        /// <param name="A">The original data.</param>
        /// <param name="bw">Worker thread.</param>
        // Split A[] into 2 runs, sort both runs into B[], merge both runs from B[] to A[]
        // iBegin is inclusive; iEnd is exclusive (A[iEnd] is not in the set).
        private static void TopDownSplitMerge(int[] B, int iBegin, int iEnd, int[] A, BackgroundWorker bw)
        {
            if (iEnd - iBegin > 1 && (bw == null || !bw.CancellationPending))
            {
                var iMiddle = (iEnd + iBegin) / 2;

                TopDownSplitMerge(A, iBegin, iMiddle, B, bw);
                TopDownSplitMerge(A, iMiddle, iEnd, B, bw);

                TopDownMerge(B, iBegin, iMiddle, iEnd, A, bw);
            }
        }
    }
}