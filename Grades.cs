using System;
using System.Data.SqlClient;


namespace Lab1SQL
{
    internal class Grades
    {

        public static void GetGradesLastMonth(SqlConnection connection)
        {
            DateTime lastMonth = DateTime.Now.AddMonths(-1);

            string query = "SELECT Students.Name AS StudentName, Courses.CourseName, Grades.Grade " +
                           "FROM Grades " +
                           "INNER JOIN Students ON Grades.StudentID = Students.StudentID " +
                           "INNER JOIN Courses ON Grades.CourseID = Courses.CourseID " +
                           "WHERE MONTH(Grades.Date) = @Month AND YEAR(Grades.Date) = @Year";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Month", lastMonth.Month);
                command.Parameters.AddWithValue("@Year", lastMonth.Year);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Grades set in the latest month:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["StudentName"]} - {reader["CourseName"]} - {reader["Grade"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No grades have been set the latest month");
                    }
                }
            }
        }

        public static void GetAverageGradesPerCourse(SqlConnection connection)
        {
            string query = "SELECT Courses.CourseName, AVG(Grades.Grade) AS AverageGrade, MAX(Grades.Grade) AS MaxGrade, MIN(Grades.Grade) AS MinGrade " +
                           "FROM Courses " +
                           "LEFT JOIN Grades ON Courses.CourseID = Grades.CourseID " +
                           "GROUP BY Courses.CourseName";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Average grades per course:");
                        while (reader.Read())
                        {
                            string courseName = reader["CourseName"].ToString();
                            double averageGrade = 0; // Default value if DBNull
                            int maxGrade = 0; // Default value if DBNull
                            int minGrade = 0; // Default value if DBNull

                            if (!reader.IsDBNull(reader.GetOrdinal("AverageGrade")))
                            {
                                averageGrade = Convert.ToDouble(reader["AverageGrade"]);
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("MaxGrade")))
                            {
                                maxGrade = Convert.ToInt32(reader["MaxGrade"]);
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("MinGrade")))
                            {
                                minGrade = Convert.ToInt32(reader["MinGrade"]);
                            }

                            Console.WriteLine($"{courseName} - Average grade: {averageGrade}, Highest grade: {maxGrade}, Lowest grade: {minGrade}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No courses found.");
                    }
                }
            }
        }

    }
}
