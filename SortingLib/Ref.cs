/*
 *  File Name:   Ref.cs
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
 * Date: 22/08/2021
 * ****************************************************************
 */

namespace SortingLib
{
    /// <summary>
    /// Defines the <see cref="Ref" />.
    /// </summary>
    public class Ref<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ref{T}"/> class.
        /// </summary>
        /// <param name="val">The value.</param>
        public Ref(T val)
        {
            Val = val;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Val { get; set; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="T"/> to <see cref="Ref{T}"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Ref<T>(T value) => new(value);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Ref{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator T(Ref<T> t) => t.Val;
    }
}