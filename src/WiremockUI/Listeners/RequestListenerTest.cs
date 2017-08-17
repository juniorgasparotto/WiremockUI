using com.github.tomakehurst.wiremock.http;
using com.github.tomakehurst.wiremock.verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiremockUI
{
    public class RequestListenerTest : RequestListener
    {
        public void requestReceived(Request r1, Response r2)
        {
            var a = LoggedRequest.createFrom(r1);
            var b = LoggedResponse.from(r2);
        }
    }
}
