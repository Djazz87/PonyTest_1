using System;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace PonyTest.DB;

public class ResultRepository
{
    MySqlConnection connection;
    public ResultRepository(IOptions<DataBaseConection> option)
    {
        connection = new MySqlConnection(option.Value.ConnectionString);
        
    }

    public void InsertResult(Result result)
    {
        string sql = "insert into Results values (0, @user,@testid, @score, @grade, @date)";
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            {
                mc.Parameters.AddWithValue("user", result.user);
                mc.Parameters.AddWithValue("score", result.testid);
                mc.Parameters.AddWithValue("grade", result.score);
                mc.Parameters.AddWithValue("date", result.Date);
                mc.Parameters.AddWithValue("testid", result.testid);
                mc.ExecuteNonQuery();
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
    }
}