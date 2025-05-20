using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentJsonManager
{
    internal class Student
    {
        static int idGenerator = 0;
        public int Id { get; } // for autoincrement id, always only use get! 
        public string Name { get; set; }
        public string[] Courses { get; set; }
        public double[] Marks { get; set; }

        public Student() { Id = ++idGenerator; }
        public Student(string name, string[] courses, double[] marks)
        {
            if (courses.Length != marks.Length) throw new ArrayTypeMismatchException("The sizes are not the same!");

            Id = ++idGenerator;
            Name = name;
            Courses = courses;
            Marks = marks;
        }

        public double GetAvgMarks()
        {
            if (Marks == null) return 0; return Marks.Average();
        }

        public override string ToString()
        {
            string output = $"Student ID: {Id}, Name: {Name}";

            if (Marks == null || Marks.Length == 0) output += "==> no marks or courses are available!";
            else
            {
                for (int i = 0; i < Marks.Length; i++)
                {
                    output += $"\n{Courses[i]} = {Marks[i]}";
                }
                output += $"\nAverage = {GetAvgMarks():F2}";
            }
            return output;
        }
    }
}
