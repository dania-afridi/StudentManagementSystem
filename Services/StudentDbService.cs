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
        //** TEST: Test the SQL operations */
        public void TestSQL()
        {
            // Insert
            bool inserted = AddStudent(new Student(0, "TestUser", 25));
            Console.WriteLine(inserted ? "Inserted" : "Duplicate");

            // Update
            bool updated = UpdateStudent(new Student(1, "UpdatedName", 30));
            Console.WriteLine(updated ? "Updated" : "Not found");

            // Delete
            bool deleted = DeleteStudent(1);
            Console.WriteLine(deleted ? "Deleted" : "Not found");
        }
        //** RUN: Run SQL tests */
        public void RunSqlTests()
        {
            Console.WriteLine("---- SQL TEST START ----");

            // 1. INSERT
            var student = new Student(0, "Sql Test User", 22);
            bool added = AddStudent(student);
            Console.WriteLine(added ? "Insert OK" : "Insert FAILED (duplicate)");

            // 2. READ
            var students = GetAllStudents();
            Console.WriteLine($"Total students in DB: {students.Count}");

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id} | {s.Name} | {s.Age}");
            }

            Console.WriteLine("---- SQL TEST END ----");
        }

    }
}
