using com.github.tomakehurst.wiremock.http;
using System;
using System.Windows.Forms;
using com.github.tomakehurst.wiremock.verification;
using static WiremockUI.HttpUtils;
using System.Text;
using WiremockUI.Languages;
using System.Collections.Generic;

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
                    Headers = HttpUtils.GetHeaders(request.getHeaders()),
                    Raw = Helper.ResolveBreakLineInCompatibility(request.getHeaders().ToString()),
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
                        row.Cells.Add(GetCell(TruncateString(logRow.Raw)));
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
            var viewInComposerMenu = new ToolStripMenuItem();
            var compareMenu = new ToolStripMenuItem();

            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                viewRequestMenu,
                viewResponseMenu,
                viewRawMenu,
                viewInComposerMenu,
                compareMenu
            });

            menu.Opening += (a, b) =>
            {

                if (row.Request == null)
                {
                    viewRawMenu.Visible = true;
                    viewRequestMenu.Visible = false;
                    viewResponseMenu.Visible = false;
                    viewInComposerMenu.Visible = false;
                    compareMenu.Visible = false;
                }
                else
                {
                    viewRawMenu.Visible = false;
                    viewRequestMenu.Visible = true;
                    viewResponseMenu.Visible = true;
                    viewInComposerMenu.Visible = true;
                    compareMenu.Visible = true;
                }
            };

            // view request
            viewRequestMenu.Text = Resource.viewRequestMenu;
            viewRequestMenu.Click += (a, b) =>
            {
                var frmStart = new FormTextView(master, row.UrlAbsolute, row.RequestLog.ToString());
                master.TabMaster.AddTab(frmStart, row, row.Url + " (Request)");
            };

            // view response
            viewResponseMenu.Text = Resource.viewResponseMenu;
            viewResponseMenu.Click += (a, b) =>
            {
                var frmStart = new FormTextView(master, row.UrlAbsolute, row.Response.ToString());
                master.TabMaster.AddTab(frmStart, row, row.Url + " (Response)");
            };

            // view raw
            viewRawMenu.Text = Resource.viewRawMenu;
            viewRawMenu.Click += (a, b) =>
            {
                var frmStart = new FormTextView(master, row.UrlAbsolute, row.Raw);
                master.TabMaster.AddTab(frmStart, row, row.Url + " (Raw)");
            };

            // view in composer
            viewInComposerMenu.Text = Resource.viewInComposerMenu;
            viewInComposerMenu.Click += (a, b) =>
            {
                var requestHeaders = GetHeaders(row.RequestLog);
                var requestBody = row.RequestLog.getBodyAsString();
                var responseHeaders = GetHeaders(row.Response);
                var responseBody = row.Response.getBodyAsString();

                var frmComposer = new FormWebRequest(row.Method, row.UrlAbsolute, requestHeaders, requestBody, responseHeaders, responseBody);
                master.TabMaster.AddTab(frmComposer, row, row.Url);
            };

            // view in composer
            compareMenu.Text = Resource.compareMenu;
            compareMenu.Click += (a, b) =>
            {
                var strBuilder = new StringBuilder();
                StringBuilder strBuilderFormatted = null;
                var requestHeaders = GetHeaders(row.RequestLog);
                var requestBody = row.RequestLog.getBodyAsString();

                strBuilder.AppendLine($"{row.Method} {row.UrlAbsolute}");
                strBuilder.AppendLine();
                if (requestBody?.Length == 0)
                {
                    strBuilder.Append(GetHeadersAsString(requestHeaders));
                }
                else
                {
                    var strHeaders = GetHeadersAsString(requestHeaders);

                    strBuilder.AppendLine(strHeaders);
                    strBuilder.AppendLine();
                    strBuilder.Append(requestBody);

                    // formatted body
                    var contentType = HttpUtils.GetHeaderValue(row.Headers, "content-type");
                    string requestBodyFormatted = null;
                    if (contentType?.ToLower().Contains("json") == true)
                        requestBodyFormatted = Helper.FormatToJson(requestBody, false);
                    else if (contentType?.ToLower().Contains("xml") == true)
                        requestBodyFormatted = Helper.FormatToXml(requestBody, false);

                    if (requestBodyFormatted != null)
                    {
                        strBuilderFormatted = new StringBuilder();
                        strBuilderFormatted.AppendLine(strHeaders);
                        strBuilderFormatted.AppendLine();
                        strBuilderFormatted.Append(requestBodyFormatted);
                    }
                }

                var frmCompare = new FormCompare(this.master, strBuilderFormatted?.ToString() ?? strBuilder.ToString());
                master.TabMaster.AddTab(frmCompare, row, row.Url);
            };


            return menu;
        }

        private string TruncateString(string value, int maxLength = 400)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + " ......";
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
            public Dictionary<string, string> Headers { get; internal set; }
        }
    }
}
