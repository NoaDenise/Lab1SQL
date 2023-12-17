using System;
using System.Data.SqlClient;


namespace Lab1SQL
{
    internal class Staff
    {
        public static void AddNewStaff(SqlConnection connection)
        {
            Console.WriteLine("Add new employee:");
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.WriteLine($"Please enter a profession for  {name}");
            string category = Console.ReadLine();

            string query = "INSERT INTO Staff (Name, Category) VALUES (@Name, @Category)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Category", category);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("New employee added.");
                }
                else
                {
                    Console.WriteLine("Employee not added. Try again");
                }
            }
        }
        public static void GetStaffByCategory(SqlConnection connection)
        {
            Console.WriteLine("Choose a category of employees: ");
            Console.WriteLine("Teacher\nAdmin\nPrincipal");
            string category = Console.ReadLine();

            string query = "SELECT * FROM Staff WHERE Category = @Category";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Category", category);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine($"Employees within the category '{category}':");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Name"]} - {reader["Category"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No employee was found within the category '{category}'.");
                    }
                }
            }
        }
    }
}
