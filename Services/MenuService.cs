using StudentManagementSystem.Models;
using System.Linq;
using System.Text.RegularExpressions;


namespace StudentManagementSystem.Services
{
    /// Handles all user interactions and menu operations
    public class MenuService
    {
        // Empty constructor – no state to initialize 
        public MenuService()
        {
        }

        //---- Entry point for menu loop ----//
        public void Start()
        {
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ListStudents();
                        break;
                    case "3":
                        ListStudentsAboveAge();
                        break;
                    case "4":
                        UpdateStudent();
                        break;
                    case "5":
                        DeleteStudent();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        private void ShowMenu()
        {
            Console.WriteLine("1. Add student");
            Console.WriteLine("2. List students");
            Console.WriteLine("3. List students above age");
            Console.WriteLine("4. Update student");
            Console.WriteLine("5. Delete student");
            Console.WriteLine("6. Exit");
            Console.Write("Select option: ");
        }

        // ---------------- INPUT HELPERS ----------------
        private Student GetStudentFromInput()
        {
            Console.Write("Enter Student Name: ");
            ReadStudentName(out string name);

            Console.Write("Enter Student Age: ");
            ReadStudentAge(out int age);

            return new Student(0, name, age);
        }
        //---- Read Id
        private void ReadStudentId(out int id)
        {
            while (true)
            {
                if(int.TryParse(Console.ReadLine(), out id) && id > 0)
                    return;

                Console.WriteLine("Invalid Id. Enter a positive number.");
            }
        }
        //---- Read Name 
        private void ReadStudentName(out string name)
        {
            while (true)
            {
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    continue;
                }

                if (!Regex.IsMatch(name, @"^[A-Za-z ]+$"))
                {
                    Console.WriteLine("Name can only contain letters and spaces.");
                    continue;
                }

                if(name.Replace(" ", "").Length < 3)
                {
                    Console.WriteLine("Name must contain at least 3 letters.");
                    continue;
                }

                return;
            }
        }
        //---- Read Age
        private void ReadStudentAge(out int age)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out age) && age > 0)
                    return;

                Console.WriteLine("Invalid age. Enter a positive number.");
            }
        }
        // ---------------- CRUD OPERATIONS ----------------
        private void AddStudent()
        {
            Student student = GetStudentFromInput();
            StudentDbService db = new StudentDbService();

            db.AddStudent(student);
            Console.WriteLine("Student added successfully!");
        }

        //---- List students by Name 

        private void ListStudents()
        {
            StudentDbService db = new StudentDbService();
            var students = db.GetAllStudents();

            if (students == null || !students.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            var orderedStudents = students.OrderBy(s => s.Name);

            Console.WriteLine("\n--- Student List ---");

            foreach (var student in orderedStudents)
            {
                Console.WriteLine($"Id: {student.Id} | Name: {student.Name} | Age: {student.Age}");
            }
        }
        
        //---- List of students filter by age
        private void ListStudentsAboveAge()
        {
            Console.Write("Enter minimum age: ");
            ReadStudentAge(out int minAge);

            StudentDbService db = new StudentDbService();
            var students = db.GetAllStudents();

            var result = students
                .Where(s => s.Age >= minAge)
                .OrderBy(s => s.Name)
                .ToList();

            if (!result.Any())
            {
                Console.WriteLine("No students match the criteria.");
                return;
            }

            foreach (var student in result)
            {
                Console.WriteLine(
                    $"Id: {student.Id} | Name: {student.Name} | Age: {student.Age}");
            }
        }
        //---- Find student by Id 
        private Student FindStudentById(int id)
        {
            StudentDbService db = new StudentDbService();
            var students = db.GetAllStudents();

            return students.FirstOrDefault(s => s.Id == id);
        }

        //---- Update Student 
        private void UpdateStudent()
        {
            Console.Write("Enter Student Id to update: ");
            ReadStudentId(out int id);

            Student student = FindStudentById(id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter new name: ");
            ReadStudentName(out string name);

            Console.Write("Enter new age: ");
            ReadStudentAge(out int age);

            student.Name = name;
            student.Age = age;

            StudentDbService db = new StudentDbService();
            bool updated = db.UpdateStudent(student);

            Console.WriteLine(updated 
                ? "Student updated successfully."
                : "Student not found.");
        }

        //---- Delete Student

        private void DeleteStudent()
        {
            Console.Write("Enter Student Id to delete: ");
            ReadStudentId(out int id);

            StudentDbService db = new StudentDbService();
            bool deleted = db.DeleteStudent(id);

            Console.WriteLine(deleted
                ? "Student not found."
                : "Student deleted successfully.");
        }

    }
}
