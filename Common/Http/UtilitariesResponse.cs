using Common.Settings;
using Common.Http;
using System.Text.Json;

namespace NewMIS.Common.HttpHelpers
{
    public class UtilitariesResponse<T> where T : class, new()
    {
        private readonly IConfigurationLib ConfigurationLib;

        public UtilitariesResponse(IConfigurationLib _configurationLib)
        {
            ConfigurationLib = _configurationLib;
        }

        public EResponseBase<T> SetResponseBaseForExecuteSQLCommand(int result)
        {
            EResponseBase<T> response = new();
            if (result >= 0)
            {
                response.Code = ConfigurationLib.SuccessCode;
                response.MessageES = ConfigurationLib.SuccessMsgES;
                response.MessageEN = ConfigurationLib.SuccessMsgEN;
            }
            else
            {
                response.Code = ConfigurationLib.DataNotFoundCode;
                response.MessageES = ConfigurationLib.DataNotFoundMsgES;
                response.MessageEN = ConfigurationLib.DataNotFoundMsgEN;
            }
            return response;
        }
        public EResponseBase<T> SetResponseBaseForList(IQueryable<T> query)
        {
            EResponseBase<T> response = new();
            if (query == null)
            {
                response.Code = ConfigurationLib.DataNotFoundCode;
                response.MessageES = ConfigurationLib.DataNotFoundMsgES;
                response.MessageEN = ConfigurationLib.DataNotFoundMsgEN;
            }
            else
            {
                if (query.Any())
                {
                    response.Code = ConfigurationLib.SuccessCode;
                    response.MessageES = ConfigurationLib.SuccessMsgES;
                    response.MessageEN = ConfigurationLib.SuccessMsgEN;
                    response.List = query.ToList();
                    response.IsResultList = true;
                }
                else
                {
                    response.Code = ConfigurationLib.DataNotFoundCode;
                    response.MessageES = ConfigurationLib.DataNotFoundMsgES;
                    response.MessageEN = ConfigurationLib.DataNotFoundMsgEN;
                }
            }
            return response;
        }

        public EResponseBase<T> SetResponseBaseForList(List<T> query)
        {
            EResponseBase<T> response = new();
            if (query == null)
            {
                response.Code = ConfigurationLib.DataNotFoundCode;
                response.MessageES = ConfigurationLib.DataNotFoundMsgES;
                response.MessageEN = ConfigurationLib.DataNotFoundMsgEN;
            }
            else
            {
                if (query.Any())
                {
                    response.Code = ConfigurationLib.SuccessCode;
                    response.MessageES = ConfigurationLib.SuccessMsgES;
                    response.MessageEN = ConfigurationLib.SuccessMsgEN;
                    response.List = query;
                    response.IsResultList = true;
                }
                else
                {
                    response.Code = ConfigurationLib.DataNotFoundCode;
                    response.MessageES = ConfigurationLib.DataNotFoundMsgES;
                    response.MessageEN = ConfigurationLib.DataNotFoundMsgEN;
                }
            }
            return response;
        }

        public EResponseBase<T> SetResponseBaseForObj(T obj)
        {
            EResponseBase<T> response = new();
            var noDefault = JsonSerializer.Serialize(obj);
            var Default = JsonSerializer.Serialize(new T());

            if (obj != null && noDefault != Default)
            {
                response.Code = ConfigurationLib.SuccessCode;
                response.MessageES = ConfigurationLib.SuccessMsgES;
                response.MessageEN = ConfigurationLib.SuccessMsgEN;
                response.Object = obj;
            }
            else
            {
                response.Code = ConfigurationLib.DataNotFoundCode;
                response.MessageES = ConfigurationLib.DataNotFoundMsgES;
                response.MessageEN = ConfigurationLib.DataNotFoundMsgEN;
            }
            return response;
        }

        public EResponseBase<T> SetResponseBaseForObj(string obj)
        {
            EResponseBase<T> response = new();
            if (obj != null)
            {
                response.Code = ConfigurationLib.SuccessCode;
                response.MessageES = ConfigurationLib.SuccessMsgES;
                response.MessageEN = ConfigurationLib.SuccessMsgEN;
                response.Data = obj;
            }
            else
            {
                response.Code = ConfigurationLib.DataNotFoundCode;
                response.MessageES = ConfigurationLib.DataNotFoundMsgES;
                response.MessageEN = ConfigurationLib.DataNotFoundMsgEN;
            }
            return response;
        }

        public EResponseBase<T> SetResponseBaseForValidationExceptionString(ICollection<string> errors)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.InvalidParametersCode,
                MessageES = ConfigurationLib.InvalidParametersMsgES,
                MessageEN = ConfigurationLib.InvalidParametersMsgEN,
                FunctionalErrors = errors.ToHashSet()
            };
            return response;
        }
        public EResponseBase<T> SetResponseBaseForOK()
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.SuccessCode,
                MessageES = ConfigurationLib.SuccessMsgES,
                MessageEN = ConfigurationLib.SuccessMsgEN
            };
            return response;
        }
        public EResponseBase<T> SetResponseBaseForOK(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.SuccessCode,
                MessageES = ConfigurationLib.SuccessMsgES,
                MessageEN = ConfigurationLib.SuccessMsgEN
            };
            if (obj != null) response.Object = obj;
            return response;
        }
        public EResponseBase<T> SetResponseBaseForOK(ICollection<T> obj)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.SuccessCode,
                MessageES = ConfigurationLib.SuccessMsgES,
                MessageEN = ConfigurationLib.SuccessMsgEN
            };
            if (obj.Any()) { response.List = obj; response.IsResultList = true; }
            return response;
        }
        public EResponseBase<T> SetResponseBaseForExceptionUnexpected()
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.UnpecifiedErrorCode,
                MessageES = ConfigurationLib.UnpecifiedErrorMsgES,
                MessageEN = ConfigurationLib.UnpecifiedErrorMsgEN
            };
            return response;
        }
        public EResponseBase<T> SetResponseBaseForException(Exception ex)
        {
            EResponseBase<T> response = new();
            if (ex is TimeoutException)
            {
                response.Code = ConfigurationLib.TimeOutErrorCode;
                response.MessageES = ConfigurationLib.TimeOutErrorMsgES;
                response.MessageEN = ConfigurationLib.TimeOutErrorMsgEN;
            }
            else if (ex is HttpRequestException)
            {
                response.Code = ConfigurationLib.TimeOutErrorCode;
                response.MessageES = ConfigurationLib.TimeOutErrorMsgES;
                response.MessageEN = ConfigurationLib.TimeOutErrorMsgEN;
            }
            //else if (ex is NotAuthorizeResourceException)
            //{
            //    response.Code = ConfigurationLib.UnauthorizedErrorCode;
            //    response.MessageES = ConfigurationLib.UnauthorizedErrorCodeMsgES;
            //    response.MessageEN = ConfigurationLib.UnauthorizedErrorCodeMsgEN;
            //}
            else
            {
                response.Code = ConfigurationLib.UnpecifiedErrorCode;
                response.MessageES = ConfigurationLib.UnpecifiedErrorMsgES;
                response.MessageEN = ConfigurationLib.UnpecifiedErrorMsgEN;
            }
            return response;
        }
        public EResponseBase<T> SetResponseBaseForNoAuthorized()
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.UnauthorizedErrorCode,
                MessageES = ConfigurationLib.UnauthorizedErrorCodeMsgES,
                MessageEN = ConfigurationLib.UnauthorizedErrorCodeMsgEN,
                List = new List<T>()
            };
            return response;
        }

        public EResponseBase<T> SetResponseBaseForNoDataFound()
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.DataNotFoundCode,
                MessageES = ConfigurationLib.DataNotFoundMsgES,
                MessageEN = ConfigurationLib.DataNotFoundMsgEN,
                List = new List<T>()
            };
            return response;
        }

        public EResponseBase<T> SetResponseBaseForParameterNoValid()
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.InvalidParametersCode,
                MessageES = ConfigurationLib.InvalidParametersMsgES,
                MessageEN = ConfigurationLib.InvalidParametersMsgEN,
                List = new List<T>()
            };
            return response;
        }

        public EResponseBase<T> SetResponseBaseForDuplicatedData(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.DataExistsCode,
                MessageES = ConfigurationLib.DataExistsMsgES,
                MessageEN = ConfigurationLib.DataExistsMsgEN
            };
            if (obj != null) response.Object = obj;
            return response;
        }

        public EResponseBase<T> SetResponseBaseForMissingParameters(T? obj)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.MissingParametersCode,
                MessageES = ConfigurationLib.MissingParametersMsgES,
                MessageEN = ConfigurationLib.MissingParametersMsgEN
            };
            if (obj != null) response.Object = obj;
            return response;
        }

        public EResponseBase<T> SetResponseBaseForSignatureError(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = -1001,
                MessageES = "Firma incorrecta",
                MessageEN = "Wrong signature"
            };
            if (obj != null) response.Object = obj;
            return response;
        }

        public EResponseBase<T> SetResponseBaseNotEistUserError(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = -1001,
                MessageES = "Usuario no existe",
                MessageEN = "Wrong signature"
            };
            if (obj != null) response.Object = obj;
            return response;
        }

        public EResponseBase<T> SetResponseBaseErrorInsertData(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = -1001,
                MessageES = "Error al insertar datos",
                MessageEN = "Error inserting data"
            };
            if (obj != null) response.Object = obj;
            return response;
        }

        public EResponseBase<T> SetResponseBaseErrorUpdateData(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = -1001,
                MessageES = "Error al actualizar datos",
                MessageEN = "Error updating data"
            };
            if (obj != null) response.Object = obj;
            return response;
        }

        public EResponseBase<T> SetResponseBaseForBadCredentials()
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.BadCredentialsCode,
                MessageES = ConfigurationLib.BadCredentialsMsgES,
                MessageEN = ConfigurationLib.BadCredentialsMsgEN
            };
            return response;
        }

        public EResponseBase<T> SetResponseBaseForBadCredentials(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.BadCredentialsCode,
                MessageES = ConfigurationLib.BadCredentialsMsgES,
                MessageEN = ConfigurationLib.BadCredentialsMsgEN,
                Object = obj
            };
            return response;
        }

        public EResponseBase<T> SetResponseBaseForAccountSuspended(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.SuspendedAccountCode,
                MessageES = ConfigurationLib.SuspendedAccountMsgES,
                MessageEN = ConfigurationLib.SuspendedAccountMsgEN
            };
            if (obj != null) response.Object = obj;
            return response;
        }

        public EResponseBase<T> SetResponseBaseForUserNoExist()
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.UserNotExistCode,
                MessageES = ConfigurationLib.UserNotExistMsgEN,
                MessageEN = ConfigurationLib.UserNotExistMsgES
            };
            return response;
        }

        public EResponseBase<T> SetResponseBaseForUserNoExist(T obj)
        {
            EResponseBase<T> response = new()
            {
                Code = ConfigurationLib.UserNotExistCode,
                MessageES = ConfigurationLib.UserNotExistMsgEN,
                MessageEN = ConfigurationLib.UserNotExistMsgES,
                Object = obj
            };
            return response;
        }
    }
}
