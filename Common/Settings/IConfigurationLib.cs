using Microsoft.Extensions.Configuration;

namespace Common.Settings
{
    public interface IConfigurationLib
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string myconn { get; }

        //WicQCitas
        string UrlBaseWicQ { get; }
        string PrefixWicQ { get; }
        string GetAppointmentsController { get; }

        //WicConnectionAPI
        string UrlBaseWicConnection { get; }
        string PrefixWicConnection { get; }
        string GetAppointmentsByFilterController { get; }

        // SecurityAPI
        string UrlBaseSecurityAPI { get; }
        string PrefixSecurityAPI { get; }
        string ChangeWicIDController { get; }

        int SuccessCode { get; }
        string SuccessMsgES { get; }
        string SuccessMsgEN { get; }

        int DataExistsCode { get; }
        string DataExistsMsgEN { get; }
        string DataExistsMsgES { get; }

        int DataNotFoundCode { get; }
        string DataNotFoundMsgES { get; }
        string DataNotFoundMsgEN { get; }

        int InvalidParametersCode { get; }
        string InvalidParametersMsgEN { get; }
        string InvalidParametersMsgES { get; }

        int TimeOutErrorCode { get; }
        string TimeOutErrorMsgEN { get; }
        string TimeOutErrorMsgES { get; }

        int UnpecifiedErrorCode { get; }
        string UnpecifiedErrorMsgEN { get; }
        string UnpecifiedErrorMsgES { get; }

        int UnauthorizedErrorCode { get; }
        string UnauthorizedErrorCodeMsgEN { get; }
        string UnauthorizedErrorCodeMsgES { get; }

        int MissingParametersCode { get; }
        string MissingParametersMsgEN { get; }
        string MissingParametersMsgES { get; }

        int BadCredentialsCode { get; }
        string BadCredentialsMsgEN { get; }
        string BadCredentialsMsgES { get; }

        int SuspendedAccountCode { get; }
        string SuspendedAccountMsgEN { get; }
        string SuspendedAccountMsgES { get; }

        int UserNotExistCode { get; }
        string UserNotExistMsgEN { get; }
        string UserNotExistMsgES { get; }
    }
}
