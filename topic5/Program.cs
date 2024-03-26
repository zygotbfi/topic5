using System;
using System.Collections.Generic;

// Enum representing different departments
public enum Department
{
    IT,
    HR,
    Finance,
    Sales,
    Marketing
}

// Base class Person
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

// Employee class derived from Person
public class Employee : Person
{
    public int EmployeeId { get; }
    public double Salary { get; set; }
    public Department Department { get; set; }

    private static int totalEmployees = 0;

    public Employee(int employeeId, string firstName, string lastName, double salary, Department department)
        : base(firstName, lastName)
    {
        EmployeeId = employeeId;
        Salary = salary;
        Department = department;
        totalEmployees++;
    }

    ~Employee()
    {
        Console.WriteLine($"Employee {EmployeeId}: {FirstName} {LastName} has been destroyed.");
    }

    public static int GetTotalEmployees()
    {
        return totalEmployees;
    }
}

// Interface for manager
public interface IManager
{
    void AssignEmployeeToDepartment(Employee employee, Department department);
}

// EmployeeManager class
public class EmployeeManager : IManager
{
    private List<Employee> employees = new List<Employee>();

    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
    }

    public void RemoveEmployee(int employeeId)
    {
        Employee employeeToRemove = employees.Find(emp => emp.EmployeeId == employeeId);
        if (employeeToRemove != null)
        {
            employees.Remove(employeeToRemove);
            Console.WriteLine($"Employee {employeeToRemove.EmployeeId}: {employeeToRemove.FirstName} {employeeToRemove.LastName} removed successfully.");
        }
        else
        {
            Console.WriteLine($"Employee with ID {employeeId} not found.");
        }
    }

    public void DisplayEmployees()
    {
        Console.WriteLine("Employee Details:");
        foreach (var employee in employees)
        {
            Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, Salary: {employee.Salary}, Department: {employee.Department}");
        }
    }

    public double CalculateTotalSalary()
    {
        double totalSalary = 0;
        foreach (var employee in employees)
        {
            totalSalary += employee.Salary;
        }
        return totalSalary;
    }

    public void AssignEmployeeToDepartment(Employee employee, Department department)
    {
        employee.Department = department;
        Console.WriteLine($"Employee {employee.EmployeeId} assigned to {department} department.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating an instance of EmployeeManager
        EmployeeManager manager = new EmployeeManager();

        // Adding employees
        manager.AddEmployee(new Employee(1, "John", "Doe", 50000, Department.IT));
        manager.AddEmployee(new Employee(2, "Jane", "Smith", 60000, Department.HR));

        // Displaying employees
        manager.DisplayEmployees();

        // Removing an employee
        manager.RemoveEmployee(1);

        // Displaying total salary
        Console.WriteLine($"Total Salary: {manager.CalculateTotalSalary()}");

        // Demonstrating interface implementation
        Employee emp = new Employee(3, "Michael", "Johnson", 70000, Department.Finance);
        manager.AssignEmployeeToDepartment(emp, Department.Marketing);

        // Displaying total employees
        Console.WriteLine($"Total Employees: {Employee.GetTotalEmployees()}");
    }
}
