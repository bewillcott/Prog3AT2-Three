/*
 *  File Name:   MainWindow.xaml.cs
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

namespace Prog3AT2_Three
{
    using Ardalis.SmartEnum;

    using SortingLib;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The array size.
        /// </summary>
        private const int ARRAY_SIZE = 10000000;

        /// <summary>
        /// The maximum salary.
        /// </summary>
        private const int MAX_SALARY = 10000000;

        /// <summary>
        /// The minimum salary.
        /// </summary>
        private const int MIN_SALARY = 10000;

        /// <summary>
        /// The random seed.
        /// </summary>
        private const int RANDOM_SEED = 1234;

        /// <summary>
        /// The helper.
        /// </summary>
        private readonly Helper helper;

        /// <summary>
        /// The list.
        /// </summary>
        private readonly List<int> list = new(ARRAY_SIZE);

        /// <summary>
        /// The worker
        /// </summary>
        private readonly BackgroundWorker worker = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            helper = new Helper(list, RANDOM_SEED, MIN_SALARY, MAX_SALARY);

            // Setup the worker
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;

            // Setup combobox
            AlgorithmComboBox.ItemsSource = Algorithms.List;
            AlgorithmComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// The AlgorithmComboBox_SelectionChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="SelectionChangedEventArgs"/>.</param>
        private void AlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ComboBox).SelectedItem as Algorithms;

            SalaryListBox.ItemsSource = null;
            DurationTextBox.Text = @"0.000 seconds";
            SortingProgressBar.IsIndeterminate = true;

            worker.RunWorkerAsync(item);

            e.Handled = true;
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

        /// <summary>
        /// Times the consuming operation.
        /// </summary>
        /// <param name="bw">The bw.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private object TimeConsumingOperation(BackgroundWorker bw, Algorithms item)
        {
            var rtn = 0.000;

            if (item == Algorithms.ListSort)
            {
                rtn = helper.SortIt(null, @"list.Sort", bw);
            }
            else if (item == Algorithms.HeapSort)
            {
                rtn = helper.SortIt(Sorting.HeapSort, $"Heap Sort", bw);
            }
            else if (item == Algorithms.MergeSort)
            {
                rtn = helper.SortIt(Sorting.MergeSort, $"Merge Sort", bw);
            }
            else if (item == Algorithms.QuickSort)
            {
                rtn = helper.SortIt(Sorting.QuickSort, $"Quick Sort", bw);
            }
            else if (item == Algorithms.Unsorted)
            {
                helper.GenerateIntArray();
            }

            return rtn;
        }

        /// <summary>
        /// Handles the DoWork event of the Worker control.
        /// </summary>
        /// <remarks>
        /// Initial code copied from:
        /// https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-run-an-operation-in-the-background?view=netframeworkdesktop-4.8
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            var bw = sender as BackgroundWorker;

            // Extract the argument.
            var item = e.Argument as Algorithms;

            // Start the time-consuming operation.
            e.Result = TimeConsumingOperation(bw, item);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the Worker control.
        /// </summary>
        /// <remarks>
        /// Initial code copied from:
        /// https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-run-an-operation-in-the-background?view=netframeworkdesktop-4.8
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SortingProgressBar.IsIndeterminate = false;

            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                var msg = $"An error occurred: {e.Error.Message}";
                MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                DurationTextBox.Text = e.Result != null ? ((double)e.Result).ToString("F3") + @" seconds" : @"0.000 seconds";
                SalaryListBox.ItemsSource = list;
            }
        }

        /// <summary>
        /// Defines the <see cref="Algorithms" />.
        /// </summary>
        public sealed class Algorithms : SmartEnum<Algorithms>
        {
            public static readonly Algorithms HeapSort = new(nameof(HeapSort), "Heap Sort", 2);
            public static readonly Algorithms ListSort = new(nameof(ListSort), "List Sort", 1);
            public static readonly Algorithms MergeSort = new(nameof(MergeSort), "Merge Sort", 3);
            public static readonly Algorithms QuickSort = new(nameof(QuickSort), "Quick Sort", 4);
            public static readonly Algorithms Unsorted = new(" Unsorted", "=Unsorted=", 0);

            /// <summary>
            /// Initializes a new instance of the <see cref="Algorithms"/> class.
            /// </summary>
            /// <param name="name">The name<see cref="string"/>.</param>
            /// <param name="value">The value<see cref="int"/>.</param>
            public Algorithms(string name, int value) : base(name, value)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Algorithms"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            /// <param name="label">The label.</param>
            /// <param name="value">The value.</param>
            public Algorithms(string name, string label, int value) : base(name, value)
            {
                Label = label;
            }

            /// <summary>
            /// Gets the label.
            /// </summary>
            /// <value>
            /// The label.
            /// </value>
            public string Label { get; }

            /// <summary>
            /// Converts to string.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Label;
            }
        }
    }
}