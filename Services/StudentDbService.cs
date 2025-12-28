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
        //******* Additional CRUD methods can be implemented here *********//
        
        //** ADD: Add a new student
        public bool AddStudent(Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query =
                            "INSERT INTO Students (Name, Age) VALUES (@Name, @Age)";

                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@Name", student.Name);
                        command.Parameters.AddWithValue("@Age", student.Age);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    return true;
            }
            catch (SqlException ex) when(ex.Number == 2627 || ex.Number == 2601)
            {
                    // Unique constraint violation
                    return false;
            }
        }
        /*
        //** UPDATE: Update an existing student
        public bool UpdateStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query =
                    "UPDATE Students SET Name = @Name, Age = @Age WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Age", student.Age);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
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
        */
    }
}
