using eic.middleware.idsrv_wrapper.Models;
using ew.common.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eic.middleware.idsrv_wrapper
{
    public class IdSrvManager : EwhEntityBase
    {
        public string IdSrvAPIBaseUrl = "http://api.easyadmincp.com/";
        public IdSrvUser UserAdded;
        public IdSrvUserDetail UserLoggedFullInfo;


        public bool RegisterUser(RegisterUserDto dto)
        {
            var _client = new RestClient(IdSrvAPIBaseUrl);
            var request = new RestRequest("auth/register", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(dto);
            var response = _client.Execute<IdSrvUser>(request);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                UserAdded = response.Data;
                return true;
            }
            else
            {
                this.XErrorMessage = response.ErrorMessage;
                this.XException = response.ErrorException;
            }
            return false;
        }

        public bool SignIn(SignInDto dto)
        {
            var _client = new RestClient(IdSrvAPIBaseUrl);
            var request = new RestRequest("auth/signin", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(dto);
            var response = _client.Execute<IdSrvUserDetail>(request);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                UserLoggedFullInfo = response.Data;
                return true;
            }
            else
            {
                this.XErrorMessage = response.ErrorMessage;
                this.XException = response.ErrorException;
            }
            return false;
        }
        
    }
}
