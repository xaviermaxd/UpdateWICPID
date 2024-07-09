using Microsoft.Extensions.Configuration;

namespace Common.Settings
{
    public class ConfigurationLib : IConfigurationLib
    {
        public IConfiguration Configuration { get; set; }

        public ConfigurationLib(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }


        // GenesysApiServices
        public string ClientId => Configuration.GetSection("Genesys")["clientId"];
        public string ClientSecret => Configuration.GetSection("Genesys")["clientSecret"];

        // DataBase
        public string myconn => Configuration.GetSection("ConnectionStrings")["conn"];

        //WicQCitas
        public string UrlBaseWicQ => Configuration.GetSection("WicQCitas")["UrlBase"];
        public string PrefixWicQ => Configuration.GetSection("WicQCitas")["Prefix"];
        public string GetAppointmentsController => Configuration.GetSection("WicQCitas")["GetAppointmentsController"];

        //WicConnectionAPI
        public string UrlBaseWicConnection => Configuration.GetSection("WIConnectionAPI")["UrlBase"];
        public string PrefixWicConnection => Configuration.GetSection("WIConnectionAPI")["Prefix"];
        public string GetAppointmentsByFilterController => Configuration.GetSection("WIConnectionAPI")["GetAppointmentsByFilterController"];

        // SecurityAPI
        public string UrlBaseSecurityAPI => Configuration.GetSection("SecurityAPI")["UrlBase"];
        public string PrefixSecurityAPI => Configuration.GetSection("SecurityAPI")["Prefix"];
        public string ChangeWicIDController => Configuration.GetSection("SecurityAPI")["ChangeWicIDController"];


        public int SuccessCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["SuccessCode"]);
        public string SuccessMsgES => Configuration.GetSection("AppConfiguration")["SuccessMsgES"];
        public string SuccessMsgEN => Configuration.GetSection("AppConfiguration")["SuccessMsgEN"];
        public int DataExistsCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["DataExistsCode"]);
        public string DataExistsMsgEN => Configuration.GetSection("AppConfiguration")["DataExistsMsgEN"];
        public string DataExistsMsgES => Configuration.GetSection("AppConfiguration")["DataExistsMsgES"];
        public int DataNotFoundCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["DataNotFoundCode"]);
        public string DataNotFoundMsgES => Configuration.GetSection("AppConfiguration")["DataNotFoundMsgES"];
        public string DataNotFoundMsgEN => Configuration.GetSection("AppConfiguration")["DataNotFoundMsgEN"];
        public int InvalidParametersCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["InvalidParametersCode"]);
        public string InvalidParametersMsgEN => Configuration.GetSection("AppConfiguration")["InvalidParametersMsgEN"];
        public string InvalidParametersMsgES => Configuration.GetSection("AppConfiguration")["InvalidParametersMsgES"];
        public int TimeOutErrorCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["TimeOutErrorCode"]);
        public string TimeOutErrorMsgEN => Configuration.GetSection("AppConfiguration")["TimeOutErrorMsgEN"];
        public string TimeOutErrorMsgES => Configuration.GetSection("AppConfiguration")["TimeOutErrorMsgES"];
        public int UnpecifiedErrorCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["UnpecifiedErrorCode"]);
        public string UnpecifiedErrorMsgEN => Configuration.GetSection("AppConfiguration")["UnpecifiedErrorMsgEN"];
        public string UnpecifiedErrorMsgES => Configuration.GetSection("AppConfiguration")["UnpecifiedErrorMsgES"];
        public int UnauthorizedErrorCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["UnauthorizedErrorCode"]);
        public string UnauthorizedErrorCodeMsgEN => Configuration.GetSection("AppConfiguration")["UnauthorizedErrorCodeMsgEN"];
        public string UnauthorizedErrorCodeMsgES => Configuration.GetSection("AppConfiguration")["UnauthorizedErrorCodeMsgES"];
        public int MissingParametersCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["MissingParametersCode"]);
        public string MissingParametersMsgEN => Configuration.GetSection("AppConfiguration")["MissingParametersMsgEN"];
        public string MissingParametersMsgES => Configuration.GetSection("AppConfiguration")["MissingParametersMsgES"];
        public int BadCredentialsCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["BadCredentialsCode"]);
        public string BadCredentialsMsgEN => Configuration.GetSection("AppConfiguration")["BadCredentialsMsgEN"];
        public string BadCredentialsMsgES => Configuration.GetSection("AppConfiguration")["BadCredentialsMsgES"];
        public int SuspendedAccountCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["SuspendedAccountCode"]);
        public string SuspendedAccountMsgEN => Configuration.GetSection("AppConfiguration")["SuspendedAccountMsgEN"];
        public string SuspendedAccountMsgES => Configuration.GetSection("AppConfiguration")["SuspendedAccountMsgES"];
        public int UserNotExistCode => Convert.ToInt32(Configuration.GetSection("AppConfiguration")["UserNotExistCode"]);
        public string UserNotExistMsgEN => Configuration.GetSection("AppConfiguration")["UserNotExistMsgEN"];
        public string UserNotExistMsgES => Configuration.GetSection("AppConfiguration")["UserNotExistMsgES"];
    }
}
