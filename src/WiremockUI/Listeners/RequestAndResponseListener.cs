using com.github.tomakehurst.wiremock.http;
using com.github.tomakehurst.wiremock.verification;

namespace WiremockUI
{
    public class RequestAndResponseListener : RequestListener
    {
        private ILogTableRequestResponse log;

        public RequestAndResponseListener(ILogTableRequestResponse log)
        {
            this.log = log;
        }

        public void requestReceived(Request r1, Response r2)
        {
            log.AddRequestResponse(r1, r2);
        }
    }
}
