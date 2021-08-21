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

using System;

namespace ConsoleApp
{
    /// <summary>
    /// Class contains a Console entry method.
    /// </summary>
    internal static class Input
    {
        /// <summary>
        /// Accepts an int from the Console.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        internal static int InputInt(string text)
        {
            var rtn = 0;
            var retry = false;

            do
            {
                Console.Write(text);
                var input = Console.ReadLine();

                if (input == string.Empty)
                {
                    retry = false;
                }
                else
                {
                    if (!int.TryParse(input, out rtn))
                    {
                        Console.WriteLine("Not a valid number.\nPlease try again.");
                        retry = true;
                    }
                }
            }
            while (retry);

            return rtn;
        }
    }
}