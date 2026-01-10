using StudentManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.Services
{
    public class FakeDataService : IDataService
    {
        private readonly List<Student> students = new();

        public void AddStudent(Student student)
        {
            student.Id = students.Count + 1;
            students.Add(student);
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }

        public bool UpdateStudent(Student student)
        {
            var existing = students.FirstOrDefault(s => s.Id == student.Id);
            if (existing == null) return false;

            existing.Name = student.Name;
            existing.Age = student.Age;
            return true;
        }

        public bool DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return false;

            students.Remove(student);
            return true;
        }
    }
}
