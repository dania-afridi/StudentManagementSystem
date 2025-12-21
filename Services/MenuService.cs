using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace StudentManagementSystem.Services
{
    internal class MenuService
    {
        private List<Student> students = new List<Student>();

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
            Console.WriteLine("4. Exit");
            Console.Write("Select option: ");
        }

        //******* Get new student detail *********//
        private Student GetStudentFromInput()
        {
            int id;
            while(true)
            {
                Console.Write("Enter Student Id: ");
                if(int.TryParse(Console.ReadLine(), out id) && id > 0)
                {
                    if (!IsDuplicateId(id))
                        break;

                    Console.WriteLine("Id already exists. Try another.");
                }
                else
                {
                    Console.WriteLine("Invalid Id. Enter a positive number.");
                }
            }

            string name;
            while (true)
            {
                Console.Write("Enter Student Name: ");
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

            int age;
            while (true)
            {
                Console.Write("Enter Student Age: ");
                if (int.TryParse(Console.ReadLine(), out age) && age > 0)
                    break;

                Console.WriteLine("Invalid age. Enter a positive number.");
            }

            return new Student(id, name, age);
        }

        //******* Check student Id if already exist *********//
        private bool IsDuplicateId(int id)
        {
            return students.Any(s => s.Id == id);
        }

        //******* Add a new student *********//
        private void AddStudent()
        {
            Student student = GetStudentFromInput();
            students.Add(student);
            Console.WriteLine("Student added successfully!");
        }

        //******* List of students sort by Name *********//
        private void ListStudents()
        {
            if (!students.Any())
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
            return students.FirstOrDefault(s => s.Id == id);
        }

        //******* List of students filter by age *********//
        private void ListStudentsAboveAge()
        {
            int minAge;
            while (true)
            {
                Console.Write("Enter minimum age: ");
                if (!int.TryParse(Console.ReadLine(), out minAge))
                {
                    Console.WriteLine("Invalid age. Enter a number.");
                    continue;
                }

                if (minAge <= 0)
                {
                    Console.WriteLine("Age must be greater than zero.");
                    continue;
                }

                break;
            }


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

    }
}
