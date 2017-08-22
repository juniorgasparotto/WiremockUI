using com.github.tomakehurst.wiremock.http;
using System.Collections.Generic;

namespace WiremockUI
{
    public class HttpUtils
    {
        public static Dictionary<string, string> GetHeaders(Request request)
        {
            var dic = new Dictionary<string, string>();
            var all = request.getHeaders().all().iterator();
            HttpHeader header;
            
            while (all.hasNext())
            {
                header = (HttpHeader) all.next();
                dic.Add(header.key(), string.Join("\r\n", header.values().toArray()));
            }

            return dic;
        }

        public static Dictionary<string, string> GetHeaders(Response response)
        {
            var dic = new Dictionary<string, string>();
            var all = response.getHeaders().all().iterator();
            HttpHeader header;

            while (all.hasNext())
            {
                header = (HttpHeader)all.next();
                dic.Add(header.key(), string.Join("\r\n", header.values().toArray()));
            }

            return dic;
        }

        public static string GetHeaderValue(Dictionary<string, string> headers, string name)
        {
            if (headers.ContainsKey(name))
                return headers[name];
            return null;
        }

        public static Dictionary<string, string> GetHeaders(string headers, bool response = false)
        {
            var dic = new Dictionary<string, string>();
            if (headers == null)
                return dic;

            string[] lines = headers.Split(new char[] { '\r', '\n' });
            var count = 0;

            foreach (var line in lines)
            {
                if (count == 0 && !response)
                {
                    var split2 = line.Split(' ');
                    if (split2.Length > 0)
                    {
                        dic.Add("method", split2[0]);
                        if (split2.Length > 1)
                            dic.Add("url", split2[1]);
                        if (split2.Length > 2)
                        {
                            var httpsplit = split2[2].Split('/');
                            if (httpsplit.Length > 1)
                            {
                                dic.Add("protocol", httpsplit[0].ToLower());
                                dic.Add("protocol-version", httpsplit[1]);
                            }
                            else
                            {
                                dic.Add("protocol", split2[2].ToLower());
                            }
                        }
                    }
                }
                else if (count == 0 && response)
                {
                    var split2 = line.Split(' ');
                    if (split2.Length > 0)
                    {
                        var httpsplit = split2[0].Split('/');
                        if (httpsplit.Length > 1)
                        {
                            dic.Add("protocol", httpsplit[0].ToLower());
                            dic.Add("protocol-version", httpsplit[1]);
                        }
                        else
                        {
                            dic.Add("protocol", split2[0].ToLower());
                        }

                        if (split2.Length > 1)
                            dic.Add("status", split2[1]);
                        if (split2.Length > 2)
                            dic.Add("statusMessage", split2[2]);
                    }
                }
                else
                {
                    string[] split = line.Split(new char[] { ':' }, 2);
                    if (split.Length > 1)
                    {
                        var name = split[0].Trim().ToLower();
                        dic[name] = split[1].Trim();
                    }
                }
                count++;
            }

            return dic;
        }

        public static string GetUrlAbsolute(Dictionary<string, string> headers)
        {
            var protocol = GetHeaderValue(headers, "protocol");
            var host = GetHeaderValue(headers, "host");
            var url = GetHeaderValue(headers, "url");

            if (!string.IsNullOrEmpty(protocol)
                && !string.IsNullOrEmpty(host)
                && !string.IsNullOrEmpty(url))
            {
                var separator = "";

                if (host.EndsWith("/") && url.StartsWith("/"))
                    url = url.Remove(0, 1);

                if (!host.EndsWith("/") && !url.StartsWith("/"))
                    separator = "/";

                return $"{protocol}://{host}{separator}{url}";
            }

            return null;
        }
    }
}
