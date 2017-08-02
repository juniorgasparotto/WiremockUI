using Newtonsoft.Json;
using System.IO;
using WiremockUI.Data;
using System.Collections.Generic;
using System;

namespace WiremockUI
{
    public class MappingModel
    {
        [JsonIgnore]
        public Proxy Proxy { get; private set; }
        [JsonIgnore]
        public Mock Mock { get; private set; }

        public string Id { get; set; }
        public string Uuid { get; set; }
        public RequestModel Request { get; set; }
        public ResponseModel Response { get; set; }
        
        private MappingModel() { }

        public static MappingModel Create(Proxy proxy, Mock mock, string content, out Exception exception)
        {
            exception = null;
            try
            {
                var mapping = JsonConvert.DeserializeObject<MappingModel>(content);
                if (mapping.Id != null)
                { 
                    mapping.Proxy = proxy;
                    mapping.Mock = mock;
                    return mapping;
                }
                return null;
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        public bool HasBodyFile()
        {
            if (!string.IsNullOrWhiteSpace(Response.BodyFileName) && File.Exists(GetBodyFullPath()))
                return true;
            return false;
        }

        public string GetBodyFullPath()
        {
            return Path.Combine(Proxy.GetFullPath(Mock), "__files", Response.BodyFileName);
        }

        public string GetFormattedName(string fileName)
        {
            return fileName + " (" + Request?.Url + ")";
        }

        public class RequestModel
        {
            public string Url { get; set; }
            public string Method { get; set; }
        }

        public class ResponseModel
        {
            public int Status { get; set; }
            public string BodyFileName { get; set; }
            public Dictionary<string, string> Headers { get; set; }

        }
    }
}
