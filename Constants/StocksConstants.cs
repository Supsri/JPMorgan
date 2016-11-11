using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    public class StocksConstants
    {
        public const string StocksAPI = "http://localhost:55846/";
        public const string StocksClient = "https://localhost:50539/";
        
        public const string IdSrv = "https://localhost:44305/identity";
        public const string IdSrvToken = IdSrv + "/connect/token";
        public const string IdSrvAuthorize = IdSrv + "/connect/authorize";
        public const string IdSrvUserInfo = IdSrv + "/connect/userinfo";

    }
}
