using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructure
{
    public class DExecutionUpdateWicPID
    {
        private readonly string connectionString;

        public DExecutionUpdateWicPID(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int InsertExecution(ExecutionUpdateWicPID execution)
        {
            int insertedID = 0;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("InsertExecutionUpdateWicPID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ExecutionDate", execution.ExecutionDate);
                    command.Parameters.AddWithValue("@StartDate", execution.StartDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@EndDate", execution.EndDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TypeBatch", execution.TypeBatch);
                    command.Parameters.AddWithValue("@Error", execution.Error ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Successful", execution.Successful);
                    command.Parameters.AddWithValue("@NotFoundCount", execution.NotFoundCount);
                    command.Parameters.Add("@InsertedID", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                    insertedID = (int)command.Parameters["@InsertedID"].Value;
                    Console.WriteLine($"Inserted ID in InsertExecution: {insertedID}");
                    return insertedID;
                }
            }
        }

        public void UpdateExecution(int executionID, ExecutionUpdateWicPID execution)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateExecutionUpdateWicPID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ExecutionID", executionID);
                    command.Parameters.AddWithValue("@StartDate", execution.StartDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@EndDate", execution.EndDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Error", execution.Error ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Successful", execution.Successful);
                    command.Parameters.AddWithValue("@NotFoundCount", execution.NotFoundCount);

                    command.ExecuteNonQuery();
                    Console.WriteLine($"Updated Execution with ID: {executionID}");
                }
            }
        }

        public List<User> GetNewParticipantsForUpdate(DateTime insertDt)
        {
            var participants = new List<User>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetNewParticipantForUpdateWicPID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InsertDt", insertDt);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                ID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                FirstLastName = reader.GetString(2),
                                Birthdate = reader.GetDateTime(3)
                            };
                            participants.Add(user);
                            Console.WriteLine($"Fetched User: {JsonConvert.SerializeObject(user, Formatting.Indented)}");
                        }
                    }
                }
            }
            return participants;
        }

        public void UpdateWicID(string firstName, string firstLastName, DateTime birthdate, string wicID, int executionID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UpdateWicIDByDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@FirstLastName", firstLastName);
                    command.Parameters.AddWithValue("@Birthdate", birthdate);
                    command.Parameters.AddWithValue("@WicID", wicID);
                    command.Parameters.AddWithValue("@ExecutionID", executionID);

                    command.ExecuteNonQuery();
                    Console.WriteLine($"Updated WicID for User: {firstName} {firstLastName}");
                }
            }
        }

        public void MarkParticipantNotFound(string firstName, string firstLastName, DateTime birthdate, int executionID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("MarkParticipantNotFoundByDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@FirstLastName", firstLastName);
                    command.Parameters.AddWithValue("@Birthdate", birthdate);
                    command.Parameters.AddWithValue("@ExecutionID", executionID);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Marked as Not Found: {firstName} {firstLastName}");
                }
            }
        }
    }
}
