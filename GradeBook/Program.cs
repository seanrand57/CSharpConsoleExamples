using System;
using System.IO;
using System.Speech.Synthesis;

namespace GradeBook
{
    internal class Program
    {
        private static void GiveBookAName(GradeBook book)
        {
            book.Name = "The Gradebook";
        }

        private static void Main(string[] args)
        {
            var book = new GradeBook("Sean's Book");
            try
            {
                using (FileStream stream = File.Open("grades.txt", FileMode.Open))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        var grade = float.Parse(line);
                        book.AddGrade(grade);
                        line = reader.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Could not locate the file grades.txt");
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("You do not have access to the file grades.txt");
                return;
            }

            book.WriteGrades(Console.Out);

            try
            {
                Console.WriteLine("Please enter a name for the book");
                book.Name = Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid name");
            }

            var stats = book.ComputeStatistics();
            book.NameChange = OnNameChanged;
            book.Name = "The Gradebook";

            Console.WriteLine(book.Name);
            Console.WriteLine($"Highest Grade: {stats.HighestGrade}");
            Console.WriteLine($"Average Grade: {stats.AverageGrade}");
            Console.WriteLine($"Lowest Grade: {stats.LowestGrade}");
            Console.WriteLine($"Letter Grade: {stats.LetterGrade}");
            Console.WriteLine($"{stats.LetterGrade} = {stats.Description}");
        }

        private static void OnNameChanged(string oldValue, string newValue)
        {
            Console.WriteLine("Name Changed from {0} to {1}", oldValue, newValue);
        }

        private static void SayGoodBye()
        {
            var speech = new SpeechSynthesizer();
            speech.Speak("Goodbye");
        }


        private static void Arrays()
        {
            // storing grades in an array
            float[] grades;
            grades = new float[3];

            AddGrades(grades);

            foreach (var grade in grades)
            {
                Console.WriteLine($"Array Grade: {grade}");
            }
        }

        private static void AddGrades(float[] grades)
        {
            if (grades.Length >= 3)
            {
                grades[0] = 91f;
                grades[1] = 89.1f;
                grades[2] = 75f;
            }
        }

        private static void Immutable()
        {
            // Dates are immutable as an example
            var date = new DateTime(2016, 7, 11);

            // Updating the date object such as this will not yeild the current date + 1 day
            // due to being immutable. It must equal the reference type.

            // Notice the compile warning
            //date.AddDays(1);

            // correct way
            date = date.AddDays(1);

            // strings are exactly the same!
            var name = " Sean Rand ";
            name = name.Trim();

            Console.WriteLine($"Date: {date}");
            Console.WriteLine($"Name of Author: {name}");
        }
    }
}