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
                    students.Add(new Student(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2)
                    ));
                }
            }

            return students;
        }
        //******* Additional CRUD methods can be implemented here *********//
        
        //** ADD: Add a new student
        public void AddStudent(Student student)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query =
                    @"INSERT INTO Students (Name, Age)
                      VALUES (@Name, @Age)";

                using SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex) when(ex.Number == 2627 || ex.Number == 2601)
            {
                // Unique constraint violation
                Console.Write(ex.Message);
            }
        }
        
        //** UPDATE: Update an existing student
        public bool UpdateStudent(Student student)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = @"UPDATE Students
                             SET Name = @Name, Age = @Age
                             WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Age", student.Age);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        //** DELETE: Delete a student by ID
        public bool DeleteStudent(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

    }
}
