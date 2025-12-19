using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagementSystem.Models;


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
            Console.Write("Enter Student Id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Student Age: ");
            int age = int.Parse(Console.ReadLine());

            return new Student(id, name, age);
        }

        private void AddStudent()
        {
            Console.WriteLine("AddStudent called");
        }

        private void ListStudents()
        {
            Console.WriteLine("ListStudents called");
        }
    }
}
