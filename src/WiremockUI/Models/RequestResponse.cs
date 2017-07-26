using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using WiremockUI.Data;
using System;
using System.Threading;

namespace WiremockUI
{
    public class RequestResponse
    {
        private JObject jMapping;

        public RequestFile Request { get; set; }
        public ResponseFile Response { get; set; }

        public class RequestFile
        {
            public string FileName { get; set; }
            public string Body { get; set; }
            public RequestResponse RequestResponse { get; set; }
        }

        public class ResponseFile
        {
            public string FileName { get; set; }
            public string Body { get; set; }
            public RequestResponse RequestResponse { get; set; }
        }


        public RequestResponse(Proxy proxy, Mock mock, string requestFileName)
        {
            this.Request = new RequestFile();
            Request.RequestResponse = this;
            Request.FileName = requestFileName;
            Request.Body = ReadAllText(requestFileName);

            this.Response = new ResponseFile();
            this.Response.RequestResponse = this;
            this.jMapping = JsonConvert.DeserializeObject<JObject>(Request.Body);
            if (jMapping != null)
            {
                var fileName = jMapping.SelectToken("response.bodyFileName").Value<string>();
                Response.FileName = Path.Combine(proxy.GetFullPath(mock), "__files", fileName);
                Response.Body = ReadAllText(Response.FileName);
            }
        }

        private string ReadAllText(string requestFileName)
        {
            while (Helper.IsFileLocked(requestFileName))
            { }

            using (var lockFileStream = File.Open(requestFileName, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (var reader = new StreamReader(lockFileStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string GetRequestName()
        {
            return Path.GetFileNameWithoutExtension(Request.FileName);
        }

        public string GetResponseName()
        {
            return Path.GetFileNameWithoutExtension(Response.FileName);
        }

        internal string GetFormattedName()
        {
            return GetRequestName() + GetUrlGet();
        }

        private string GetUrlGet()
        {
            if (jMapping != null)
            {
                var fileName = jMapping.SelectToken("request.url").Value<string>();
                return " (" + fileName + ")";
            }
            return "";
        }
    }
}
