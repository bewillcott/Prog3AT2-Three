
# Prog3 AT2 Question 3
This is a TAFE assignment for the Diploma in Software Development, at the South Metropolitan TAFE,
Rockingham, Western Australia.

The project scenario is to make a list of different annual salaries for payroll
in whole numbers (integers) that will need to be sorted.  There should be multiple methods
of sorting, so that the payroll staff can decide on which to use.

An application needs to be developed that creates lists of integer values
between 10K and 10 million.  The application must have the ability to sort in three different
styles with timers to indicate the speed at which this happens.  There must
be at least 1 million items in the list as the **future business strategy** is
to employ at least this many staff.

Only one sorting technique may be the in-built method (`Array.Sort()`),
the rest must be written into the source code.


## Implementation
This project consists of four sub-projects within the one Visual Studio session:

- ConsoleApp
- SortingLib
- Prog3AT2-Two
- TestProject1

### ConsoleApp
This is a small console application that exercises all of the sorting algorithms,
for the purposes of testing and producing the timing data for documentary reference purposes.

### SortingLib
This is a Class Library that contains the `Sorting` and `Helper` classes.

### Prog3AT2-Three
This is the GUI program that provides the means to utilize the sorting algorithms,
in much the same way as the console application.

### TestProject1
A simple test to make sure my C# implementation of the Top-down Merge Sort routine C-like code from Wikipedia
actually worked properly.

