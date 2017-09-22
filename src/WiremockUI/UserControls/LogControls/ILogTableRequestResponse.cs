using com.github.tomakehurst.wiremock.http;
using java.net;
using java.nio;
using System;

namespace WiremockUI
{
    public interface ILogTableRequestResponse
    {
        void AddServerIncoming(string incoming, DateTime date);
        void AddServerOutgoing(string outcoming, DateTime date);
        void AddRequestResponse(Request request, Response response);
    }
}