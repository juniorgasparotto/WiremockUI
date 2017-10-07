using Newtonsoft.Json;
using System.IO;
using WiremockUI.Data;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace WiremockUI
{
    public class MappingModel
    {
        [JsonIgnore]
        public Server Server { get; private set; }
        [JsonIgnore]
        public Scenario Scenario { get; private set; }

        public string Id { get; set; }
        public string Uuid { get; set; }
        public RequestModel Request { get; set; }
        public ResponseModel Response { get; set; }
        
        private MappingModel() { }

        public static MappingModel Create(Server server, Scenario scenario, string content, out Exception exception)
        {
            exception = null;
            try
            {
                var mapping = JsonConvert.DeserializeObject<MappingModel>(content);
                if (mapping.Request != null)
                { 
                    mapping.Server = server;
                    mapping.Scenario = scenario;
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

        public static string RenameBodyName(string content, string bodyFileName)
        {
            var mapping = JsonConvert.DeserializeObject<JObject>(content);
            mapping.SelectToken("response")["bodyFileName"] = Path.GetFileName(bodyFileName);
            return Helper.FormatToJson(mapping.ToString(), false);
        }

        public bool HasBodyFile()
        {
            if (Response != null && !string.IsNullOrWhiteSpace(Response.BodyFileName) && File.Exists(GetBodyFullPath()))
                return true;
            return false;
        }

        public string GetBodyFullPath()
        {
            return Path.Combine(Server.GetFullPath(Scenario), "__files", Response.BodyFileName);
        }

        public string GetFormattedName(string fileName, bool showUrl, bool showName)
        {
            if (showUrl && showName)
                return Request?.Url + " (" + fileName + ")";
            else if (showUrl && !showName)
                return Request?.Url;
            else
                return fileName;
        }

        public enum ContentType
        {
            Other = 0,
            Json = 1,
            Xml = 2,
            Image = 3
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
            public Dictionary<string, object> Headers { get; set; }

            public bool IsJson()
            {

                return GetContentType() == ContentType.Json;
            }

            public bool IsImage()
            {
                return GetContentType() == ContentType.Image;
            }

            public bool IsXml()
            {
                return GetContentType() == ContentType.Xml;
            }

            public ContentType GetContentType()
            {
                var value = GetContentTypeValue();
                var contentType = ContentType.Other;
                if (value != null)
                {
                    if (value.Contains("json"))
                        contentType = ContentType.Json;
                    if (value.Contains("xml"))
                        contentType = ContentType.Xml;
                    if (value.Contains("soap"))
                        contentType = ContentType.Xml;
                    if (value.Contains("image"))
                        contentType = ContentType.Image;
                }
                return contentType;
            }

            public string GetContentTypeValue()
            {
                if (Headers != null)
                {
                    foreach (var header in Headers)
                        if (header.Key.ToLower().Trim() == "content-type")
                            return header.Value.ToString();
                }

                return null;
            }
        }
    }
}
