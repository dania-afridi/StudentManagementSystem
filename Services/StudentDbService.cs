using Microsoft.Data.SqlClient;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class StudentDbService
    {
        private readonly string connectionString =
            "Server=localhost;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // READ: Get all students
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Age FROM Students";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                    );

                    students.Add(student);
                }
            }

            return students;
        }
    }
}
