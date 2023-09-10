using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message=null)
        {
            StatusCode=statusCode;
            Message=message ??  GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode{get;set;}
        public string Message{get;set;}
        public string GetDefaultMessageForStatusCode( int statusCode)
        {
            return statusCode switch
            {
                400=>"A Bad Request, you have made",
                401=>"Authorized,you are not",
                404=>"resource found,it was not",
                500=>"Errors are the path to dark side,errors lead to anger,Anger leads to hate,hates lead to creer change",
                _=>null
            };
        }
        
    }
}