using StudentManagementSystem.Models;
using System.Collections.Generic;

namespace StudentManagementSystem.Services
{
    public interface IDataService
    {
        void AddStudent(Student student);
        List<Student> GetAllStudents();
        bool UpdateStudent(Student student);
        bool DeleteStudent(int id);
    }
}
