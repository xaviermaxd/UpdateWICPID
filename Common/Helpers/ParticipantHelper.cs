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
        private readonly string baseUrl;
        private readonly string prefix;
        private readonly string getListParticipantsController;

        public ParticipantHelper(IConfigurationLib configurationLib)
        {
            this.configurationLib = configurationLib;
            baseUrl = configurationLib.UrlBaseWicConnection;
            prefix = configurationLib.PrefixWicConnection;
            getListParticipantsController = configurationLib.GetAppointmentsByFilterController;
        }

        public async Task<List<ParticipantGetWicPIDResponseV1>> GetListParticipantsAsync(ParticipantGetWicPIDRequestV1 request)
        {
            var client = new RestClient(baseUrl);
            var restRequest = new RestRequest($"{prefix}{getListParticipantsController}", Method.Post);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddJsonBody(request);
            restRequest.Timeout = TimeSpan.FromSeconds(60);

            Console.WriteLine($"Sending request to: {baseUrl}{prefix}{getListParticipantsController}");
            Console.WriteLine($"Request Body: {JsonConvert.SerializeObject(request)}");

            var response = await client.ExecuteAsync<ApiResponse<ParticipantGetWicPIDResponseV1>>(restRequest);

            if (response.StatusCode == HttpStatusCode.OK && response.Data != null)
            {
                return response.Data.listado;
            }

            throw new Exception("Error fetching participants: " + response.ErrorMessage);
        }
    }
}
