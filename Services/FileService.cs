using System.Text.Json;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    internal class FileService
    {
        //***** Path to the JSON file *****//
        private const string FilePath = "students.json";

        //***** Load students from the JSON file *****//
        public List<Student> LoadStudents()
        {
            if (!File.Exists(FilePath))
                return new List<Student>();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Student>>(json)
                   ?? new List<Student>();
        }
        //***** Save students to the JSON file *****//
        public void SaveStudents(List<Student> students)
        {
            string json = JsonSerializer.Serialize(
                students,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, json);
        }
    }
}
