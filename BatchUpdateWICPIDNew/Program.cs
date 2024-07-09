using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Helpers;
using Common.Request;
using Common.Settings;
using Domain;
using Infraestructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class Program
{
    public static IConfigurationLib Configuration { get; set; }

    static async Task Main()
    {
        string assemblyLocation = Assembly.GetEntryAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        var builder = new ConfigurationBuilder()
            .SetBasePath(assemblyDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        Configuration = new ConfigurationLib(builder);
        var dExecutionUpdateWicPID = new DExecutionUpdateWicPID(Configuration.myconn);
        var participantHelper = new ParticipantHelper(Configuration);

        int executionID = InsertExecution(dExecutionUpdateWicPID);
        Console.WriteLine($"Execution ID: {executionID}");

        int notFoundCount = 0;

        try
        {
            var participants = dExecutionUpdateWicPID.GetNewParticipantsForUpdate(DateTime.Now);
            Console.WriteLine("Participants fetched:");
            foreach (var participant in participants)
            {
                Console.WriteLine(JsonConvert.SerializeObject(participant, Formatting.Indented));
            }

            foreach (var participant in participants)
            {
                var formattedBirthDate = participant.Birthdate.ToString("yyyy-MM-ddTHH:mm:ss");
                Console.WriteLine($"Formatted Birthdate for participant {participant.FirstName} {participant.FirstLastName}: {formattedBirthDate}");

                var request = new ParticipantGetWicPIDRequestV1
                {
                    FirstName = participant.FirstName,
                    LastName = participant.FirstLastName,
                    BirthDay = formattedBirthDate
                };

                string requestBody = JsonConvert.SerializeObject(request);
                Console.WriteLine($"Request Body for participant {participant.FirstName} {participant.FirstLastName}: {requestBody}");

                try
                {
                    var response = await participantHelper.GetListParticipantsAsync(request);
                    if (response != null && response.Any())
                    {
                        var newWicID = response.First().WICPID;
                        Console.WriteLine($"Request Body: {JsonConvert.SerializeObject(new REQChangeWicID { UserID = participant.ID, WicID = newWicID })}");
                        try
                        {
                            await participantHelper.ChangeWicIDAsync(participant.ID, newWicID);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error changing WicID for participant {participant.FirstName} {participant.FirstLastName}: {ex.Message}");
                            dExecutionUpdateWicPID.MarkParticipantNotFound(participant.FirstName, participant.FirstLastName, participant.Birthdate, executionID);
                            notFoundCount++;
                        }
                    }
                    else
                    {
                        dExecutionUpdateWicPID.MarkParticipantNotFound(participant.FirstName, participant.FirstLastName, participant.Birthdate, executionID);
                        notFoundCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching participant {participant.FirstName} {participant.FirstLastName}: {ex.Message}");
                    dExecutionUpdateWicPID.MarkParticipantNotFound(participant.FirstName, participant.FirstLastName, participant.Birthdate, executionID);
                    notFoundCount++;
                }
            }

            UpdateExecution(dExecutionUpdateWicPID, executionID, true, null, notFoundCount);
            Console.WriteLine("All data has been successfully processed.");
        }
        catch (Exception ex)
        {
            UpdateExecution(dExecutionUpdateWicPID, executionID, false, ex.Message, notFoundCount);
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("The application will close in 30 seconds...");
        System.Threading.Thread.Sleep(30000); // Espera 30 segundos antes de cerrar la consola
    }

    static int InsertExecution(DExecutionUpdateWicPID dExecutionUpdateWicPID)
    {
        var reqExecution = new ExecutionUpdateWicPID
        {
            ExecutionDate = DateTime.Now,
            StartDate = DateTime.Now.Date,
            EndDate = DateTime.Now.Date.AddDays(1),
            TypeBatch = "updateNewParticipant",
            Successful = false,
            Error = string.Empty, // Inicializamos el error como string vacío
            NotFoundCount = 0 // Inicializamos el contador de no encontrados en 0
        };
        int insertedID = dExecutionUpdateWicPID.InsertExecution(reqExecution);
        Console.WriteLine($"Inserted Execution ID: {insertedID}");
        return insertedID;
    }

    static void UpdateExecution(DExecutionUpdateWicPID dExecutionUpdateWicPID, int executionID, bool successful, string error, int notFoundCount)
    {
        var reqExecution = new ExecutionUpdateWicPID
        {
            ExecutionDate = DateTime.Now, // Especificamos los valores que quizás son necesarios
            StartDate = DateTime.Now.Date,
            EndDate = DateTime.Now.Date.AddDays(1),
            Successful = successful,
            Error = error ?? string.Empty, // Asegurarse de que el parámetro Error no sea nulo
            NotFoundCount = notFoundCount
        };

        Console.WriteLine($"Updating Execution with ID: {executionID}");
        Console.WriteLine($"StartDate: {reqExecution.StartDate}");
        Console.WriteLine($"EndDate: {reqExecution.EndDate}");
        Console.WriteLine($"Successful: {reqExecution.Successful}");
        Console.WriteLine($"Error: {reqExecution.Error}");
        Console.WriteLine($"NotFoundCount: {reqExecution.NotFoundCount}");

        dExecutionUpdateWicPID.UpdateExecution(executionID, reqExecution);

        if (!successful)
        {
            Console.WriteLine($"Update failed with error: {error}");
        }
    }
}
