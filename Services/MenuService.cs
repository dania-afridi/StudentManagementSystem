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
            Console.WriteLine("3. Exit");
            Console.Write("Select option: ");
        }

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

        private bool IsDuplicateId(int id)
        {
            return students.Any(s => s.Id == id);
        }


        private void AddStudent()
        {
            Student student = GetStudentFromInput();
            students.Add(student);
            Console.WriteLine("Student added successfully!");
        }

        private void ListStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }
        }
    }
}
