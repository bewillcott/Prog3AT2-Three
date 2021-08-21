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

    using System.Collections.ObjectModel;
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
        /// The list.
        /// </summary>
        private static readonly int[] list = new int[ARRAY_SIZE];

        /// <summary>
        /// The helper.
        /// </summary>
        private Helper helper;

        /// <summary>
        /// The number of test runs.
        /// </summary>
        private const int NUM_OF_TEST_RUNS = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            helper = new Helper(list, RANDOM_SEED, MIN_SALARY, MAX_SALARY);
            var temp = Algorithms.List;
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
            MessageBox.Show($"You selected: {((sender as ComboBox).SelectedItem as Algorithms).Name}");
        }

        /// <summary>
        /// Defines the <see cref="Algorithms" />.
        /// </summary>
        public sealed class Algorithms : SmartEnum<Algorithms>
        {
            public static readonly Algorithms Unsorted = new(" Unsorted", "=Unsorted=", 0);
            public static readonly Algorithms ArraySort = new(nameof(ArraySort), "Array Sort", 1);
            public static readonly Algorithms HeapSort = new(nameof(HeapSort), "Heap Sort", 2);
            public static readonly Algorithms MergeSort = new(nameof(MergeSort), "Merge Sort", 3);
            public static readonly Algorithms QuickSort = new(nameof(QuickSort), "Quick Sort", 4);

            public string Label { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Algorithms"/> class.
            /// </summary>
            /// <param name="name">The name<see cref="string"/>.</param>
            /// <param name="value">The value<see cref="int"/>.</param>
            public Algorithms(string name, int value) : base(name, value)
            {
            }

            public Algorithms(string name, string label, int value) : base(name, value)
            {
                Label = label;
            }

            public override string ToString()
            {
                return Label;
            }
        }
    }
}