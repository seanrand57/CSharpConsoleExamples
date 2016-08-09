using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    internal class GradeBook
    {
        private readonly List<float> _grades;
        private string _name;
        public NameChangeDelegate NameChange;
        private string v;

        public GradeBook()
        {
            _grades = new List<float>();
        }

        public GradeBook(string name)
        {
            _grades = new List<float>();
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Null or empty string");
                }

                if (_name != value)
                {
                    var oldValue = _name;
                    _name = value;
                    if (NameChange != null)
                    {
                        NameChange(oldValue, _name);
                    }
                }
            }
        }

        public void WriteGrades(TextWriter textWriter)
        {
            textWriter.WriteLine("**************");
            textWriter.WriteLine("Grades:");
            foreach (var grade in _grades)
            {
                textWriter.WriteLine(grade);
            }

            textWriter.WriteLine("**************");
        }

        public void AddGrade(float grade)
        {
            if (grade >= 0 && grade <= 100)
            {
                _grades.Add(grade);
            }
        }

        public GradeStatistics ComputeStatistics()
        {
            var stats = new GradeStatistics();
            // initalise sum as 0 as a float
            var sum = 0f;

            foreach (var grade in _grades)
            {
                stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                stats.LowestGrade = Math.Min(grade, stats.LowestGrade);

                // sum = sum + grade shorthand
                sum += grade;
            }

            stats.AverageGrade = sum/_grades.Count;
            return stats;
        }
    }
}