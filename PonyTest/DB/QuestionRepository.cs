using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace PonyTest.DB;

public class QuestionRepository
{
    MySqlConnection connection;
    public QuestionRepository(IOptions<DataBaseConection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Question> GetQuestionsByTest(TestData test)
    {
        List<Question> questions = new List<Question>();
        string sql = "SELECT * FROM Questions WHERE test_id = " + test.Id;

        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    questions.Add(new Question
                    {
                        Id = dr.GetInt32("id"),
                        TestId = dr.GetInt32("test_id"),
                        QuestionText = dr.GetString("question_text"),
                        OptionA = dr.GetString("option_a"),
                        OptionB = dr.GetString("option_b"),
                        OptionC = dr.GetString("option_c"),
                        CorectOption = dr.GetChar("correct_option")
                    });
                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        return questions;
    }
    
}