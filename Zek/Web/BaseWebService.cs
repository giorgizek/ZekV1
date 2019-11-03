using System;
using System.Web.Services;
using Zek.Data;
using Zek.Extensions;
using Zek.Security;

namespace Zek.Web
{
    public class BaseWebService : WebService
    {
        protected virtual string GetRequestHash(string requestID, DateTime? timestamp = null)
        {
            if (string.IsNullOrWhiteSpace(requestID))
                throw new ArgumentException(@"RequestID is required", nameof(requestID));

            if (timestamp == null)
                timestamp = DateTime.Now;
            return CryptoHelper.MD5Hex(requestID, timestamp.Value.ToUniversalDateString());
        }

        protected virtual bool IsValidRequest(string requestID, DateTime? timestamp = null, string hash = null)
        {
            if (string.IsNullOrWhiteSpace(requestID) || string.IsNullOrWhiteSpace(hash))
                return false;
            return hash.Equals(GetRequestHash(requestID, timestamp), StringComparison.InvariantCultureIgnoreCase);
        }

        protected virtual bool IsValidRequest(BaseRequest request)
        {
            return request != null && IsValidRequest(request.RequestID, request.Timestamp, request.RequestHash);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="errorCode">შეცდომის კოდი (0-შეცდომა არ მომხდარა, -1 ცარიელი ან არასწორი RequestID, RequestHash)</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        protected WSResponse<TResponse> CreateResponse<TResponse>(BaseRequest request, int errorCode = 0, string errorMessage = null)// where TResponse : class, new()
        {
            if (!IsValidRequest(request))
            {
                errorCode = -1;
                errorMessage = "Invalid Request";
            }

            return new WSResponse<TResponse>
            {
                RequestID = request.RequestID,
                RequestHash = GetRequestHash(request.RequestID, request.Timestamp),
                Timestamp = DateTime.Now,
                //Value = new TResponse(),
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
        }
    }
}
