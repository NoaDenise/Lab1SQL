using System;
using System.Data;
using System.Data.SqlClient;
//Noa Denise Ishac NET23
namespace Lab1SQL
{
    internal class Program
    {
        // Connection string to connect to the database
        static string connectionString = @"Data Source=(localdb)\.;Initial Catalog=School;Integrated Security=True";
        static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            // Open database connection
            connection.Open();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose a function:");
                Console.WriteLine("1. View all students");
                Console.WriteLine("2. View students by class");
                Console.WriteLine("3. Add employee");
                Console.WriteLine("4. View all employees");
                Console.WriteLine("5. View grades for latest month");
                Console.WriteLine("6. Average grade per course");
                Console.WriteLine("7. Add student");
                Console.WriteLine("0. Exit program");

                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Students.GetStudents(connection);
                        break;
                    case "2":
                        Students.GetStudentsInClass(connection);
                        break;
                    case "3":
                        Staff.AddNewStaff(connection);
                        break;
                    case "4":
                        Staff.GetStaffByCategory(connection);
                        break;
                    case "5":
                        Grades.GetGradesLastMonth(connection);
                        break;
                    case "6":
                        Grades.GetAverageGradesPerCourse(connection);
                        break;
                    case "7":
                        Students.AddNewStudent(connection);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from the menu.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("Press Enter to return to menu.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            //Close database connection
            connection.Close();
        }
    }
}
    

    
