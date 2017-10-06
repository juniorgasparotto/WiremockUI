using com.github.tomakehurst.wiremock.http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;

namespace WiremockUI
{
    public class HttpUtils
    {
        public static Dictionary<string, string> GetHeaders(HttpHeaders headers)
        {
            var dic = new Dictionary<string, string>();
            var all = headers.all().iterator();
            HttpHeader header;

            while (all.hasNext())
            {
                header = (HttpHeader)all.next();
                dic.Add(header.key(), string.Join("\r\n", header.values().toArray()));
            }

            return dic;
        }

        public static Dictionary<string, string> GetHeaders(Request request)
        {
            return GetHeaders(request.getHeaders());
        }

        public static Dictionary<string, string> GetHeaders(Response response)
        {
            return GetHeaders(response.getHeaders());
        }

        public static Dictionary<string, string> GetHeaders(WebResponse response)
        {
            var dic = new Dictionary<string, string>();
            foreach(var name in response.Headers.AllKeys)
                dic.Add(name, response.Headers[name]);
            return dic;
        }

        public static Dictionary<string, string> GetHeaders(WebRequest request)
        {
            var dic = new Dictionary<string, string>();
            foreach (var name in request.Headers.AllKeys)
                dic.Add(name, request.Headers[name]);
            return dic;
        }

        public static Dictionary<string, string> GetHeaders(string headers, bool isResponse = false, bool useLowerCaseInName = true)
        {
            var dic = new Dictionary<string, string>();
            if (headers == null)
                return dic;

            string[] lines = headers.Split(new char[] { '\r', '\n' });
            var count = 0;

            foreach (var line in lines)
            {
                var isNameValue = System.Text.RegularExpressions.Regex.IsMatch(line, @"^[^\s]*:.*$");
                if (count == 0 && !isNameValue && !isResponse)
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
                else if (count == 0 && !isNameValue && isResponse)
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
                        var name = split[0].Trim();
                        if (useLowerCaseInName)
                            name = name.ToLower();
                        dic[name] = split[1].Trim();
                    }
                }
                count++;
            }

            return dic;
        }

        public static string GetHeadersAsString(Dictionary<string, string> dic)
        {
            if (dic == null)
                return null;
            return string.Join("\r\n", dic.Select(x => x.Key + ": " + x.Value).ToArray());
        }

        public static string GetHeaderValue(Dictionary<string, string> headers, string name)
        {
            return headers?.FirstOrDefault(f => f.Key.ToLower() == name.ToLower()).Value;
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
