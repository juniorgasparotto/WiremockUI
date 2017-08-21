using com.github.tomakehurst.wiremock.http;
using System;
using System.Windows.Forms;
using java.net;
using java.nio;
using System.Collections.Generic;
using System.IO;
using com.github.tomakehurst.wiremock.verification;

namespace WiremockUI
{
    public class GridViewLogRequestResponse : ILogTableRequestResponse
    {
        private DataGridView gridView;
        private Action<LogRow> onAddRequest;
        private FormMaster master;

        public bool EnableAutoScroll { get; set; }
        public bool Enabled { get; set; } = true;
        private long count;

        delegate void SetTextCallback();

        public GridViewLogRequestResponse(DataGridView gridView, FormMaster master, Action<LogRow> onAddRequest = null)
        {
            this.gridView = gridView;
            this.onAddRequest = onAddRequest;
            this.master = master;
        }

        public void AddServerIncoming(string incoming, DateTime date)
        {
            AddRowInMultiThread(call);
            void call()
            {
                count++;
                var headers = GetHeaders(incoming);
                var row = new LogRow
                {
                    Number = count,
                    Type = "NET.IN",
                    TypeDesc = "NETWORK INCOMING LOG",
                    Raw = incoming,
                    Method = GetHeaderValue(headers, "method"),
                    Url = GetHeaderValue(headers, "url"),
                    UrlAbsolute = GetUrlAbsolute(headers),
                    RequestDate = DateTime.Now,
                    ResponseDate = null,
                    Status = 0,
                    StatusMessage = "",
                    Request = null,
                    Response = null
                };

                AddNewRow(row);
            }
        }

        public void AddServerOutgoing(string outcoming, DateTime date)
        {
            AddRowInMultiThread(call);
            void call()
            {
                count++;
                var headers = GetHeaders(outcoming, true);
                int.TryParse(GetHeaderValue(headers, "status"), out int status);

                var row = new LogRow
                {
                    Number = count,
                    Type = "NET.OUT",
                    TypeDesc = "NETWORK OUTGOING LOG",
                    Raw = outcoming,
                    Method = "",
                    Url = "",
                    UrlAbsolute = "",
                    RequestDate = null,
                    ResponseDate = DateTime.Now,
                    Status = status,
                    StatusMessage = GetHeaderValue(headers, "statusMessage"),
                    Request = null,
                    Response = null
                };

                AddNewRow(row);
            }
        }

        public void AddRequestResponse(Request request, Response response)
        {
            AddRowInMultiThread(call);

            void call()
            {
                //var strResponseDate = response.getHeaders()?.getHeader("Date")?.ToString().Replace("Date: ", "");
                //var converted = DateTime.TryParse(strResponseDate, out var responseDate);

                count++;
                var row = new LogRow
                {
                    Number = count,
                    Type = "LISTENER",
                    TypeDesc = "WIREMOCK LISTENER",
                    Raw = null,
                    Method = request.getMethod().ToString(),
                    Url = request.getUrl(),
                    UrlAbsolute = request.getAbsoluteUrl(),
                    RequestDate = null,
                    ResponseDate = DateTime.Now,
                    Status = response.getStatus(),
                    StatusMessage = response.getStatusMessage(),
                    Request = request,
                    RequestLog = LoggedRequest.createFrom(request),
                    ResponseLog = LoggedResponse.from(response),
                    Response = response
                };
                
                AddNewRow(row);
            };
        }

        private void AddNewRow(LogRow logRow)
        {
            this.onAddRequest?.Invoke(logRow);

            var row = new DataGridViewRow
            {
                Tag = logRow
            };

            row.ContextMenuStrip = GetMenuTripRow(logRow);

            foreach (DataGridViewColumn col in gridView.Columns)
            {
                switch (col.Name)
                {
                    case "Number":
                        row.Cells.Add(GetCell(logRow.Number));
                        break;
                    case "TypeLog":
                        row.Cells.Add(GetCell(logRow.Type, logRow.TypeDesc));
                        break;
                    case "Raw":
                        row.Cells.Add(GetCell(logRow.Raw));
                        break;
                    case "Url":
                        row.Cells.Add(GetCell(logRow.Url, logRow.UrlAbsolute));
                        break;
                    case "RequestTime":
                        row.Cells.Add(GetCell(logRow.RequestDate?.TimeOfDay, logRow.RequestDate?.ToLongDateString()));
                        break; 
                    case "ResponseTime":
                        row.Cells.Add(GetCell(logRow.ResponseDate?.TimeOfDay, logRow.ResponseDate?.ToLongDateString()));
                        break; 
                    case "Method":
                        row.Cells.Add(GetCell(logRow.Method));
                        break;
                    case "Status":
                        row.Cells.Add(GetCell(logRow.Status, logRow.StatusMessage));
                        break;
                }
            }

            this.gridView.Rows.Add(row);

            if (EnableAutoScroll)
            {
                this.gridView.ClearSelection();

                var last = this.gridView.Rows.Count - 1;
                this.gridView.FirstDisplayedScrollingRowIndex = last;
                this.gridView.Rows[last].Selected = true;
            }
        }

        private DataGridViewTextBoxCell GetCell(object value, string toolTip = null)
        {
            var cell = new DataGridViewTextBoxCell();
            cell.Value = value;
            cell.ToolTipText = toolTip;
            cell.Tag = value;
            return cell;
        }

        private string GetHeaderValue(Dictionary<string, string> headers, string name)
        {
            if (headers.ContainsKey(name))
                return headers[name];
            return null;
        }

        private Dictionary<string, string> GetHeaders(string headers, bool response = false)
        {
            var dic = new Dictionary<string, string>();
            if (headers == null)
                return dic;

            string[] lines = headers.Split(new char[] { '\r', '\n' });
            var count = 0;
            
            foreach(var line in lines)
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
                else if (count == 0 && response) {
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

        private string GetUrlAbsolute(Dictionary<string, string> headers)
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

        public void AddRowInMultiThread(Action action)
        {
            if (!Enabled)
                return;

            if (gridView.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(() => action());
                gridView.Invoke(d);
            }
            else
            {
                action();
            }
        }

        private ContextMenuStrip GetMenuTripRow(LogRow row)
        {
            // context menu
            var menu = new ContextMenuStrip();
            var viewRequestMenu = new ToolStripMenuItem();
            var viewResponseMenu = new ToolStripMenuItem();
            var viewRawMenu = new ToolStripMenuItem();
            //menuResponse.ImageList = imageList1;
            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                viewRequestMenu,
                viewResponseMenu,
                viewRawMenu
            });

            menu.Opening += (a, b) =>
            {

                if (row.Request == null)
                {
                    viewRawMenu.Visible = true;
                    viewRequestMenu.Visible = false;
                    viewResponseMenu.Visible = false;
                }
                else
                {
                    viewRawMenu.Visible = false;
                    viewRequestMenu.Visible = true;
                    viewResponseMenu.Visible = true;
                }
            };

            // view request
            viewRequestMenu.Text = "Visualizar requisição";
            viewRequestMenu.Click += (a, b) =>
            {
                var frmStart = new FormTextView(master, row.UrlAbsolute, row.RequestLog.ToString());
                master.TabMaster.AddTab(frmStart, row, row.Url + " (Request)");
            };

            // view response
            viewResponseMenu.Text = "Visualizar resposta";
            viewResponseMenu.Click += (a, b) =>
            {
                var frmStart = new FormTextView(master, row.UrlAbsolute, row.Response.ToString());
                master.TabMaster.AddTab(frmStart, row, row.Url + " (Response)");
            };

            // view raw
            viewRawMenu.Text = "Visualizar conteúdo";
            viewRawMenu.Click += (a, b) =>
            {
                var frmStart = new FormTextView(master, row.UrlAbsolute, row.Raw);
                master.TabMaster.AddTab(frmStart, row, row.Url + " (Raw)");
            };

            return menu;
        }

        public class LogRow
        {
            public Request Request { get; set; }
            public LoggedRequest RequestLog { get; set; }
            public Response Response { get; set; }
            public LoggedResponse ResponseLog { get; set; }

            public long Number { get; set; }
            public string Type { get; set; }
            public string ClientIp { get; set; }
            public string Url { get; set; }
            public string UrlAbsolute { get; set; }
            public DateTime? RequestDate { get; set; }
            public DateTime? ResponseDate { get; set; }
            public string Method { get; set; }
            public int Status { get; set; }
            public string StatusMessage { get; set; }
            public string TypeDesc { get; set; }
            public string Raw { get; set; }
        }
    }
}
