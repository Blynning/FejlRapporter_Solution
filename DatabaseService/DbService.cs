using Microsoft.Data.SqlClient;
using Models;
using System.Data;

namespace DatabaseService;

public class DbService
{
    private readonly string _connectionString;

    public DbService(string connectionString)
    {
        _connectionString = connectionString;
    }

    //hent alle FejlRapporter
    public async Task<List<ErrorReport>> GetAllAsync()
    {
        List<ErrorReport> reports = new();

        //opret connection
        using SqlConnection connection = new SqlConnection(_connectionString);

        //åben connection
        await connection.OpenAsync();

        //opret command
        using SqlCommand command = new SqlCommand("GetAll", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        try
        {
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            //læs data
            while (await reader.ReadAsync())
            {
                ErrorReport rapport = new ErrorReport
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phonenumber = reader.GetString(3),
                    ErrorDescription = reader.GetString(4),
                    Status = (ErrorStatus)reader.GetInt32(5)
                };

                reports.Add(rapport);
            }
            //retuner listen, sorteret på status
            return reports.OrderBy(x => (int)x.Status).ToList(); ;
        }
        catch (Exception)
        {
            return null;
        }
    }


    public async Task<bool> UpdateErrorReportStatusAsync(ErrorReport errorReport)
    {
        //opret connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        //åben connection
        await connection.OpenAsync();

        //opret command
        using SqlCommand command = new SqlCommand("UpdateStatus", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        // tilføj parameters
        command.Parameters.AddWithValue("@Id", errorReport.Id);
        command.Parameters.AddWithValue("@Status", (int)errorReport.Status);

       
        try
        {
            //lav Db kald
            return await command.ExecuteNonQueryAsync() == 1;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public async Task<bool> CreateErrorReportAsync(ErrorReport errorReport)
    {
        //opret connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        //åben connection
        await connection.OpenAsync();

        using SqlCommand command = new SqlCommand("CreateErrorReport", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        // tilføj parameters
        command.Parameters.AddWithValue("@Name", errorReport.Name);
        command.Parameters.AddWithValue("@Email", errorReport.Email);
        command.Parameters.AddWithValue("@Phonenumber", errorReport.Phonenumber);
        command.Parameters.AddWithValue("@ErrorDescription", errorReport.ErrorDescription);


        try
        {
            //lav Db kald
            return await command.ExecuteNonQueryAsync() == 1;
        }
        catch (Exception)
        {
            return false;
        }
    }

}

