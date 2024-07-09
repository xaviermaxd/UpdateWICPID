using Common.Request;
using Common.Response;
using Common.Settings;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class ParticipantHelper
    {
        private readonly IConfigurationLib configurationLib;
        private readonly string baseUrlWicConnection;
        private readonly string prefixWicConnection;
        private readonly string getListParticipantsController;
        private readonly string baseUrlSecurityAPI;
        private readonly string prefixSecurityAPI;
        private readonly string changeWicIDController;

        public ParticipantHelper(IConfigurationLib configurationLib)
        {
            this.configurationLib = configurationLib;
            baseUrlWicConnection = configurationLib.UrlBaseWicConnection;
            prefixWicConnection = configurationLib.PrefixWicConnection;
            getListParticipantsController = configurationLib.GetAppointmentsByFilterController;
            baseUrlSecurityAPI = configurationLib.UrlBaseSecurityAPI;
            prefixSecurityAPI = configurationLib.PrefixSecurityAPI;
            changeWicIDController = configurationLib.ChangeWicIDController;
        }

        public async Task<List<ParticipantGetWicPIDResponseV1>> GetListParticipantsAsync(ParticipantGetWicPIDRequestV1 request)
        {
            var client = new RestClient(baseUrlWicConnection);
            var restRequest = new RestRequest($"{prefixWicConnection}{getListParticipantsController}", Method.Post);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(request);
            restRequest.Timeout = TimeSpan.FromSeconds(60);

            Console.WriteLine($"Sending request to: {baseUrlWicConnection}{prefixWicConnection}{getListParticipantsController}");
            Console.WriteLine($"Request Body: {JsonConvert.SerializeObject(request)}");

            var response = await client.ExecuteAsync<ApiResponse<ParticipantGetWicPIDResponseV1>>(restRequest);

            if (response.StatusCode == HttpStatusCode.OK && response.Data != null)
            {
                return response.Data.listado;
            }

            throw new Exception("Error fetching participants: " + response.ErrorMessage);
        }

        public async Task ChangeWicIDAsync(int userID, string wicID)
        {
            var changeWicIDRequest = new REQChangeWicID
            {
                UserID = userID,
                WicID = wicID
            };

            var client = new RestClient(baseUrlSecurityAPI);
            var restRequest = new RestRequest($"{prefixSecurityAPI}{changeWicIDController}", Method.Post);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(changeWicIDRequest);
            restRequest.Timeout = TimeSpan.FromSeconds(60);

            Console.WriteLine($"Sending ChangeWicID request to: {baseUrlSecurityAPI}{prefixSecurityAPI}{changeWicIDController}");
            Console.WriteLine($"Request Body: {JsonConvert.SerializeObject(changeWicIDRequest)}");

            var response = await client.ExecuteAsync(restRequest);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Error changing WicID: {response.ErrorMessage}");
            }

            Console.WriteLine($"Successfully changed WicID for UserID: {userID}");
        }
    }

    public class REQChangeWicID
    {
        public int UserID { get; set; }
        public string? WicID { get; set; }
    }
}
