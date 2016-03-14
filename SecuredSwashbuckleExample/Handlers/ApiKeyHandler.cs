using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace SecuredSwashbuckleExample.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private const string REQUEST_HEADER = "X-ApiKey";

        public ApiKeyHandler(HttpConfiguration httpConfiguration)
        {
            InnerHandler = new HttpControllerDispatcher(httpConfiguration); 
        }


        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            bool isValidKey = false;
            if(request.Headers.Contains(REQUEST_HEADER))
            {
                string apiKey = request.Headers.GetValues(REQUEST_HEADER).First();
                //isValidKey = APIKeyService(apiKey);
                isValidKey = true;

            }
            if (!isValidKey)
            {
                
                return request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var httpResponse = await base.SendAsync(request, cancellationToken);

            return httpResponse;
        }

    }
}