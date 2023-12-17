using System;
using System.Data.SqlClient;
using System.Data;

namespace Lab1SQL
{
    internal class Students
    {
        //Get students from student table
        public static void GetStudents(SqlConnection connection)
        {
            string query = "SELECT * FROM Students";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Students");

            if (dataSet.Tables["Students"].Rows.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                foreach (DataRow row in dataSet.Tables["Students"].Rows)
                {
                    Console.WriteLine($"{row["Name"]} (Age: {row["Age"]})");
                }
            }
        }

        public static void GetStudentsInClass(SqlConnection connection)
        {
            Console.WriteLine("Classes:");

            string query = "SELECT * FROM Classes";
            SqlCommand classCommand = new SqlCommand(query, connection);
            SqlDataReader classReader = classCommand.ExecuteReader();

            while (classReader.Read())
            {
                Console.WriteLine($"{classReader["ClassID"]}. {classReader["ClassName"]}");
            }

            classReader.Close();

            Console.Write("Choose a class by ID: ");
            int classID;
            while (!int.TryParse(Console.ReadLine(), out classID))
            {
                Console.WriteLine("Invalid ID. Try again.");
            }

            string studentsQuery = "SELECT * FROM Students WHERE ClassID = @ClassID";
            SqlCommand studentsCommand = new SqlCommand(studentsQuery, connection);
            studentsCommand.Parameters.AddWithValue("@ClassID", classID);

            SqlDataReader studentsReader = studentsCommand.ExecuteReader();

            Console.WriteLine("Students in chosen class:");
            while (studentsReader.Read())
            {
                Console.WriteLine($"{studentsReader["Name"]} (Age: {studentsReader["Age"]})");
            }

            studentsReader.Close();
        }
        //Adds student to database
        public static void AddNewStudent(SqlConnection connection)
        {
            Console.WriteLine("Enter the student's name:");
            string studentName = Console.ReadLine();

            Console.WriteLine("Enter the student's age:");
            int studentAge;
            while (!int.TryParse(Console.ReadLine(), out studentAge))
            {
                Console.WriteLine("Invalid age. Enter the student's age again:");
            }

            Console.WriteLine("Enter Class ID for the student:");
            int classID;
            while (!int.TryParse(Console.ReadLine(), out classID))
            {
                Console.WriteLine("Invalid Class ID. Enter Class ID again:");
            }

            // Check if the entered ClassID exists in the classes table
            string classQuery = "SELECT COUNT(*) FROM Classes WHERE ClassID = @ClassID";
            using (SqlCommand classCommand = new SqlCommand(classQuery, connection))
            {
                classCommand.Parameters.AddWithValue("@ClassID", classID);
                int classCount = (int)classCommand.ExecuteScalar();

                if (classCount == 0)
                {
                    Console.WriteLine("Class ID does not exist in the database. Please add the Class ID to the 'Classes' table.");
                    return; // Exit the method without adding the student
                }
            }

            string query = "INSERT INTO Students (Name, Age, ClassID) VALUES (@Name, @Age, @ClassID)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", studentName);
                command.Parameters.AddWithValue("@Age", studentAge);
                command.Parameters.AddWithValue("@ClassID", classID);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("New student added.");
                }
                else
                {
                    Console.WriteLine("Student not added. Try again");
                }
            }
        }
    }
}