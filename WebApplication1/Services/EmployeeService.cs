using System.Text.Json;
using InMemoryCrudApp.Models;

namespace InMemoryCrudApp.Services
{
    public class EmployeeService
    {
        private readonly string _filePath = "employees.json";
        private List<Employee> _employees;
        private int _nextId;

        public EmployeeService()
        {
            if (File.Exists(_filePath))
            {
                var json =  File.ReadAllText(_filePath);
                _employees = JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
                _nextId = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
            }
            else
            {
                _employees = new List<Employee>();
                _nextId = 1;
            }
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_employees, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<Employee> GetAll() => _employees;

        public Employee GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public void Add(Employee employee, string plainPassword)
        {
            employee.Id = _nextId++;
            employee.PasswordHash = PasswordHasher.Hash(plainPassword);
            _employees.Add(employee);
            SaveToFile();
        }

        public void Update(Employee employee, string newPlainPassword = null)
        {
            var existing = GetById(employee.Id);
            if (existing != null) 
            {
                existing.Name = employee.Name;
                existing.Department = employee.Department;
                existing.Email = employee.Email;
                if (!string.IsNullOrEmpty(newPlainPassword))
                    existing.PasswordHash = PasswordHasher.Hash(newPlainPassword);
                SaveToFile();
            }
        }


        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
                SaveToFile();
            }
        }
    }
}
