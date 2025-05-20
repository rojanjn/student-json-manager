
using System.Text.Json;

namespace StudentJsonManager
{
    internal class Program
    {
        static List<Student> students = new List<Student>();

        static void save()
        {
            // saves this list to a file
            string JsonString = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("student.json", JsonString);
        }

        static void load()
        {
            // loads the list from a file
            if (!File.Exists("student.json")) { Console.WriteLine("File cannot be loaded!"); return; }

            string JsonString = File.ReadAllText("student.json");
            students = JsonSerializer.Deserialize<List<Student>>(JsonString);

            Console.WriteLine("File is loaded!");
        }

        static string Menu()
        {
            Console.WriteLine("=-=-=-=-=-=-=-=- APPLICATION =-=-=-=-=-=-=-=-");
            Console.WriteLine(" 1 - Add a student to the list");
            Console.WriteLine(" 2 - View all the students from the list");
            Console.WriteLine(" 3 - View specific student with ID");
            Console.WriteLine(" 4 - Delete a student record");
            Console.WriteLine(" 5 - Exit");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("\tChoose 1, 2, 3, 4, or 5: ");

            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            bool programEnd = false;
            load();

            while (!programEnd) // is true
            {
                var choice = Menu();

                switch (choice)
                {
                    case "1": AddStudentToList(); break;
                    case "2": ViewAllStudentInList(); break;
                    case "3": FindSpecificStudent(); break;
                    case "4": DeleteARecord(); break;
                    case "5": programEnd = true; break; // to stop the loop/program
                    default: Console.WriteLine("Invalid input! Try again!"); break;
                }
            }
        }

        private static void DeleteARecord()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Enter the student's ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }

            Student studentToDelete = students.Find(s => s.Id == id);

            if (studentToDelete == null)
            {
                Console.WriteLine("Student not found!");
            }
            else
            {
                students.Remove(studentToDelete);
                Console.WriteLine("Student record deleted!");
                save();
            }
        }

        private static void FindSpecificStudent()
        {
            Console.Clear();
            Console.WriteLine("Enter the student's ID: ");
            var id = int.Parse(Console.ReadLine());

            foreach (var student in students)
            {
                if (student.Id == id)
                {
                    Console.WriteLine("Student Found.");
                    Console.WriteLine(student);
                    return;
                }
                else
                {
                    Console.WriteLine("Student Not Found!");
                }
            }
        }

        private static void ViewAllStudentInList()
        {
            Console.Clear();

            foreach (Student student in students)
            {
                Console.WriteLine(student);
                Console.WriteLine();
            }
        }

        private static void AddStudentToList()
        {
            Student student = new Student();
            Console.WriteLine("\nEnter the name of the student: ");
            student.Name = Console.ReadLine();
            Console.WriteLine("How many courses would you like to add: ");
            var nCourses = int.Parse(Console.ReadLine());

            if (nCourses > 0)
            {
                string[] courses = new string[nCourses];
                double[] marks = new double[nCourses];

                for (int i = 0; i < nCourses; i++)
                {
                    Console.WriteLine($"Name of the course {i + 1} : ");
                    courses[i] = Console.ReadLine();
                    Console.WriteLine($"Enter {courses[i]} Marks : ");
                    marks[i] = double.Parse(Console.ReadLine());
                }

                student.Courses = courses;
                student.Marks = marks;
            }

            students.Add(student);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tStudent Added to the list.");

            save();
        }
    }
}
