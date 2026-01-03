using StudentManagementSystem.Models;
using System.Linq;
using System.Text.RegularExpressions;


namespace StudentManagementSystem.Services
{
    ///******* MenuService to handle user interactions *********//
    public class MenuService
    {
        //******* Fields *********//
        private readonly FileService fileService;
        private List<Student> students;

        //******* Test database read functionality *********//
        private void TestDbRead()
        {
            StudentDbService dbService = new StudentDbService();
            var students = dbService.GetAllStudents();

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Id} - {student.Name} - {student.Age}");
            }
        }

        //******* Constructor to initialize FileService and load students *********//
        public MenuService()
        {
             fileService = new FileService();
            // students = fileService.LoadStudents();
            StudentDbService db = new StudentDbService();
            var students = db.GetAllStudents();

        }

        //******* Start the menu loop *********//
        public void Start()
        {
            //TestDbRead();
           // StudentDbService db = new StudentDbService();

           // db.AddStudent(new Student(0, "Test User", 25));
            //db.UpdateStudent(new Student(1, "Updated Name", 30));
            //db.DeleteStudent(5);
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
        //******* Display menu options *********//
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

        //******* Get new student detail *********//
        private Student GetStudentFromInput()
        {
            /*
            int Id;
            Console.Write("Enter Student Id: ");
            ReadStudentId(out Id);
            while (IsDuplicateId(Id))
            {
                Console.WriteLine("Id already exists. Try another.");
                ReadStudentId(out Id);
            }
            */
            string Name;
            Console.Write("Enter Student Name: ");
            ReadStudentName(out Name);

            int Age;
            Console.Write("Enter Student Age: ");
            ReadStudentAge(out Age);

            return new Student(0, Name, Age);
        }
        //******* Read student Id *********//
        private void ReadStudentId(out int id)
        {
            while (true)
            {
                if(!(int.TryParse(Console.ReadLine(), out id) && id > 0))
                {
                    Console.WriteLine("Invalid Id. Enter a positive number."); 
                }
                else
                {
                    break;
                }
            }
        }
        //******* Read student Name *********//
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

                int letterCount = name.Replace(" ", "").Length;
                if (letterCount < 3)
                {
                    Console.WriteLine("Name must contain at least 3 letters.");
                    continue;
                }

                break;
            }
        }
        //******* Read student Age *********//
        private void ReadStudentAge(out int age)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out age) && age > 0)
                    break;

                Console.WriteLine("Invalid age. Enter a positive number.");
            }
        }
        //******* Add a new student *********//
        private void AddStudent()
        {
            Student student = GetStudentFromInput();
            StudentDbService db = new StudentDbService();
            db.AddStudent(student);
            Console.WriteLine("Student added successfully!");
        }

        //******* List of students sort by Name *********//
        private void ListStudents()
        {
            StudentDbService db = new StudentDbService();
            var students = db.GetAllStudents();

            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }
            var orderedStudents = students
           .OrderBy(s => s.Name)
           .ToList();

            Console.WriteLine("\n--- Student List ---");

            foreach (var student in orderedStudents)
            {
                Console.WriteLine($"Id: {student.Id} | Name: {student.Name} | Age: {student.Age}");
            }
        }
        //******* Find student by Id *********//
        private Student FindStudentById(int id)
        {
            StudentDbService db = new StudentDbService();
            var students = db.GetAllStudents();
            return students.FirstOrDefault(s => s.Id == id);
        }

        //******* List of students filter by age *********//
        private void ListStudentsAboveAge()
        {
            int minAge;
            while (true)
            {
                Console.Write("Enter minimum age: ");
                ReadStudentAge(out minAge);

                break;
            }

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

        //******* Update Student *********//
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

            fileService.SaveStudents(students);
            Console.WriteLine("Student updated successfully.");
        }

        //******* Remove Student *********//

        private void DeleteStudent()
        {
            Console.Write("Enter Student Id to delete: ");
            ReadStudentId(out int id);
            StudentDbService db = new StudentDbService();
            var students = db.GetAllStudents();
            Student student = FindStudentById(id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            students.Remove(student);
            fileService.SaveStudents(students);
            Console.WriteLine("Student deleted successfully.");
        }


    }
}
