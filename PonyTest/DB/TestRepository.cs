using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace PonyTest.DB;

public class TestRepository
{
    MySqlConnection connection;
    public TestRepository(IOptions<DataBaseConection> connector)
    {
        connection = new MySqlConnection(connector.Value.ConnectionString);
    }

    public List<TestData> GetAll()
    {
        List<TestData> testData = new List<TestData>();
        try
        {
            connection.Open();
            string sql = "SELECT * FROM Tests;";
            using (var mc =  new MySqlCommand(sql, connection))
            using(var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    testData.Add(new TestData
                    {
                        Id = dr.GetInt32("id"),
                        Title = dr.GetString("title")
                    });
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }
        finally
        {
            if(connection.State == ConnectionState.Open)
                connection.Close();
        }
        return testData;
    }
    
    
}