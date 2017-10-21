[
![Inglês](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/en-us.png)
](https://github.com/juniorgasparotto/WiremockUI)
[
![Português](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/pt-br.png)
](https://github.com/juniorgasparotto/WiremockUI/blob/master/readme-pt-br.md)

# Wiremock UI

Is a project done in `.NET Framework 4.5` that creates mock servers using the famous `Wiremock` (http://wiremock.org).

The tool is completely visual and features of Wiremock have been enhanced:

* Easy to create and run a server Wiremock
* Create and manage more than one server Wiremock in one place
* Create multiple scenarios for the same API or website with the intention of switching them as needed.
* Viewing the maps with your corresponding answer in the form of`TreeView`
* Manage maps from Wiremock with the options: _create_, _Edit_, _Remove_, _duplicate_, _disable_ and _visualization in the form of JSON_
* Advanced text editor with the following options:
  * Highlight to the object: `JSON` `HTML` `JavaScript` `C#` `XML` `PHP` `LUA``VB.NET`
  * JSON and XML formatters
  * WordWrap: AutoWrap
  * Search and replacement
  * Go to line
* The management of maps (inside the tool) dispenses with the restart of the server.
* Logs in text and table views with the options:
  * Analysis of time
  * Re-run the request with the internal tool`Web Request`
  * Compare requests that did not match with any `TreeView` map.
* Built-in tools:
  * `Web Request`: Is a simple executor of HTTP calls that can help debug
  * `Text Compare`: Is a simple text comparer
  * `Text editor`: Text Editor with formatting options to JSON or XML
  * `JSON Viewer`: JSON Viewer in the form of a tree.

# Demo

[
  ![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/youtube.png)
](https://www.youtube.com/watch?v=6d7QQqbNKhk)

## Installation

1. Download the .zip file [by clicking here](https://github.com/juniorgasparotto/WiremockUI/raw/master/download/WiremockUI.zip)
2. Extract the .zip anywhere
3. Open the file`Wiremock.exe`

_No need for installation_

**Requirements:**

* Windows
* .NET Framework 4.5
  * chocolatey:`choco install dotnet4.5`
  * Microsoft: https://www.microsoft.com/en-us/download/details.aspx?id=30653

# <a name="documentation" />Documentation

* [Tutorial](#doc)
  * [Creating a mock server](#create-server)
    * [Advanced settings](#create-server-advanced)
    * [Options menu](#server-menu)
  * [Creating a new scenario](#new-scenario)
    * [Options menu](#scenario-menu)
  * [Creating a new map](#new-map)
    * [The menu map](#map-menu)
    * [Map file in the editor](#map-editor)
    * [Map file in JSON Viewer](#map-jsonview)
    * [Response options menu](#response-menu)
    * [File in the editor](#response-editor)
  * [Text editing](#text-editor)
  * [Logs/Debugging](#log)
    * [Re-run requests](#reexecute-request)
    * [Compare requests with existing maps](#compare-request)
    * [Check the time of a request](#compare-time)
  * [Creating a server to write mode](#create-server-recording)
  * [Creating a server just as a proxy](#start-as-proxy)
  * [Menu](#menu)
* [Wiremock-Overview](#wiremock)
  * [Run as mock server](#wiremock-start-as-mock)
  * [Execute as a proxy, but recording the data](#wiremock-start-as-record)
  * [Run only as a proxy](#wiremock-start-as-proxy)
* [How does it work?](#wiremock-how-work)
* [How to contribute](#how-to-contribute)
* [License](#license)

# <a name="doc" />Tutorial

## <a name="create-server" />Creating a mock server

When creating a new server, a scenario will also be created, you can have more than one scenario for a same server, switching them as needed using the option `Set as Default` that exists in the options of the scenarios.

* Click with the right side of the mouse over the item `Servers` and click`Add Server`
* The `Server Port` field will be automatically generated, but you can change it at any time.
* You do not need to fill in the `Target URL` field, because the idea is to create a server from scratch. If you want to generate an initial mass with an existing API, use this field and run the server in write mode `Start and Record` .

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server.png)

### <a name="create-server-advanced" />Advanced settings

* In the creation of the server, you can configure the implementation of wiremock. Click on the tab`Advanced`
* For more information about each one: http://wiremock.org/docs/running-standalone/

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server-advanced.png)

### <a name="server-menu" />Options menu

* `Add scenario`: Adds a new scenario. Only one scenario may be active at a time.
* `Start`: Starts a server using the physical `mappings` folders and data`__files`
* `Start (Only Proxy)`: Starts a server just as proxy bypassing the saved files if any.
* `Start and Record`: Starts a server as a proxy in write mode
* `Restart`: Restarts the server while maintaining the kind of execution that was started
* `Stop`: Stop the server
* `Open Server folder`: Opens the folder where are all the scenarios
* `Open Targer URL in browser`: Opens the original URL in the Browser
* `Open Server URL in browser`: Open the wiremock server URL in the Browser
* `Duplicate`: Duplicates the entire server, including the scenarios and all files
* `Edit`: Edit Server information
* `Remove`: Removes the server

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-menu.png)

## <a name="new-scenario" />Creating a new scenario

* Click with the right side of the mouse on the desired server and click`Add scenario`
* You can have more than one scenario for a same server, this is useful for situations where you don't want to waste time creating advanced matches using match options of wiremock.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-scenario.png)

### <a name="scenario-menu" />Options menu

* `Add map`: Adds a new map, this map will be the basics of a map of the wiremock.
* `Set as Default`: Indicates that the files of this scenario will be used when the server is started.
* `Open scenario folder`: Opens the folder that contains the files for this scenario
* `Duplicate`: Duplicate this scenario and all your files
* `Edit`: Edit scenario
* `Remove`: Removes the scenario
* `Show URL`: When active, displays the URL of tree maps
* `Show Name`: When active, displays the name of the file in the tree

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/scenario-menu.png)

## <a name="new-map" />Creating a new map

* To add a new map, click with the mouse on the direct side item `Scenario1` .
* A map file will be created with the basics of the Wiremock settings. For more information about how to configure a map go to: http://wiremock.org/docs/request-matching/.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-map.png)

### <a name="map-menu" />The menu map

* `Rename`: Renames the file, when this occurs, the answer file is also renamed and will stay with the same name, but keeping your original extension.
* `Duplicate`: Duplicates this map
* `Remove`: Removes the map
* `Enable`: When disabled, this map will be ignored
* `View in Web Request`: Opens the map in `WebRequest` allowing executes it.
* `View in explorer`: Opens the file manager with the selected file.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/map-menu.png)

### <a name="map-editor" />Map file in the editor

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/map-editor.png)

### <a name="map-jsonview" />Map file in JSON Viewer

* When opening a map file or any other file JSON, you can view it with the `JSON Viewer` tool.
* Click with the right side of the mouse over the desired attribute for more options:
  * `View text editor`: Displays the content in a new window
  * `View as Json`: Displays the content in a new window JSON Viewer
  * `Expand all`: Opens all the children of the node
  * `Close all`: Closes all children of the node
* This viewer is also available in`Tools -> JSON Viewer`

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/map-json.png)

### <a name="response-menu" />Response options menu

* `View in explorer`: Opens the file manager with the selected file.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/response-menu.png)

### <a name="response-editor" />File in the editor

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/response-editor.png)

## <a name="text-editor" />Text editing

To open the text edit options, click with the right side of the mouse over the desired field. The following options are displayed:

* `Undo`: Undo a change
* `Redo`: Redo a change
* `Edit`
  * `Word Wrap`: Turn on or off the automatic line break
  * `Select all`: Select all text
  * `Copy`: Copies the selected text
  * `Cut`: Cut selected text
  * `Paste`: Copy what is on the Clipboard to the text field
  * `Remove`: Removes the selected text
* `Find`: Opens a new window with the search options or text replacement.
* `Json`: JSON formatting options
  * `Format`: Let the JSON in a more readable form
  * `Escape`: "Escapes" the JSON so that he can be used as the value of other JSON
  * `Unescape`: Back to normal state when JSON this "escaped"
  * `Minify`: Remove the unnecessary spaces from JSON
  * `Edit value`: This option only appears when a text is selected, it is used to edit (in separate window) a value of an attribute that contains a "JSON escaped".
* `XML`: Has the same options from JSON, however for the XML format
* `Languages`: Change the file `Highlight` being edited according to the language selected.
* A new text editor is also available in`Tools -> Text Editor`

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/editor-edit-value.png)

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/editor-edit-value-window.png)

## Starting the server

* Click with the right mouse button on the desired server
* Click`Start`

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/start-server.png)
* To start the server, a window containing the logs in the form of text and table are displayed.
* The first text of the "log" displays the command line (in green) that would be the equivalent to the Java command for that particular stock.
* One of the advantages of using WiremockUI is that you can edit the map files and your responses without the need to restart the service.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-started.png)

**Open the server in the browser**

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/open-server-in-browser.png)

## <a name="log" />Logs/Debugging

* The log of the grid is more complete than the log in text form, in addition to showing the a more easy, there's still some debug options, such as:
  * Re-run the requests.
  * Compare the requests with the existing maps.
* These debug options only work with `LISTENER` type, `NET.IN` and types `NET.OUT` are low level calls made by wiremock and that are also displayed here.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid.png)

### <a name="reexecute-request" />Re-run requests

* By clicking with the right button on the `LISTENER` type, click the `Open in WebRequest` option.
* The tool allows you to edit the request data and displays in the status bar the return code with the time that the call took. This tool is also available through the menu`Tools -> Web Request`

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-webrequest.png)

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-webrequest-window.png)

### <a name="compare-request" />Compare requests with existing maps

* By clicking with the right button on the `LISTENER` type, click the `Compare` option.
* On the left side will open the contents of the log. Select the file you want to compare by using the button with the downward arrow on the right side of the comparator.
* This tool is also available in the menu`Tools -> Text Compare`

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-compare.png)

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-compare-window.png)

### <a name="compare-time" />Check the time of a request

* To have a higher accuracy over time, compare the time `NET.IN` type (column `RequestTime` ) with the `NET.OUT` type (column `ResponseTime` ) of the desired URL. Unfortunately, there is no option needs to get this information, it would be a desire for the next versions of Wiremock (in Java).
* The `NET.OUT` type does not return the URL in the field waited, therefore, the location of this line must be manual, i.e. clean the logs and make the call only the URL that you want to measure the time.
* This option only makes sense when this running as a proxy, it makes no sense to measure the time a mock server.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/log-grid-time.png)

## <a name="create-server-recording" />Creating a server to write mode

Add a new server by filling out the option `Target URL` , so the execution options and recording will appear in the Server menu.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server-record.png)

**Starting the server**

When running in record mode, you will see the log `match-headers` Options, this means that when generating a map of the route, the headers `Content-Type` and `SOAPAction` should be part of the filter if any, i.e. `URL` , `BODY` and these `headers` must be the same to be an answer.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/start-server-record.png)

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-started-record.png)
* After recording, the maps are displayed in the tree and your answers will be available by clicking on `+` each map.
* To use the saved files, stop the server with the option `Stop` and start with the option `Start` .

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-recorded-files.png)

## <a name="start-as-proxy" />Creating a server just as a proxy

Add a new server by filling out the option `Target URL` , so the execution options and recording will appear in the Server menu.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/add-server-proxy.png)

**Starting the server**

* Click with the right side of the mouse on the server
* Click the option`Start (Only Proxy)`

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-proxy-menu.png)

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/server-started-proxy.png)

## <a name="menu" />Menu

* `File`
  * `Refresh`: Refreshes the screen to return to the initial state.
  * `Open files folder`: Opens the folder where are all the files.
  * `Find in Files`: Opens the search tool.
  * `Languages`: Supports two languages: English and Portuguese
  * `Quit`: Exits the application
* `Execute`:
  * `Add Server`: Adds a new server
  * `Start All`: Start all servers
  * `Start and record all`: Start all servers in write mode
  * `Stop all`: Stop all servers
* `Tools`:
  * `Web Request`: Opens the `WebRequest` tool that makes web requests. This tool is very simple, several settings of the HTTP protocol has not been implemented, it was created for rerun requests or maps.
  * `Text Compare`: Opens the tool that compares text. This tool is very simple, it is only to assist in the comparison of requests with maps that did not match.
  * `Text Editor`: Opens the text editing tool. The tool is very simple and is designed to help you format some value in JSON or XML format.
  * `JSON Viewer`: Opens the JSON Viewer tool which helps in viewing the JSON display in form of TreeView
* `About`: Opens the about screen.

![](https://github.com/juniorgasparotto/WiremockUI/blob/master/doc/img/menus.png)

# <a name="wiremock" />Wiremock-Overview

Is a project built in java that simulates a web service. Technically it was designed to work in two ways:

* **Standalone Mode**: is when it is run from the command prompt with the purpose of creating web servers storing the request and responses in the form of files. It can work with 3 types of servers.
  * Run as mock server
  * Execute as a proxy, but recording the data (useful for initial load)
  * Run only as a proxy
* **Testing Framework**: is out of our scope, but it can also be used as mock framework API to Java unit tests. In .NET we have `mock4net` that has inspired Wiremock.

For more information, visit the official website of the tool: http://wiremock.org/

## <a name="wiremock-start-as-mock" />Run as mock server

Within the context of tests it is useful to simulate APIs or anything about the HTTP protocol. The server uses, basically, of two folders to work:

* **mappings**: this folder contains the files `.json` , where each file represents a route with your respective answer. There is a lot of settings within each map, all are available in the documentation of the wiremock.
* **__ files**: in this folder are the answer files that are configured on a map.

**Example of map-GET**

This map creates a route that will be hearing the `http://[SERVER]:5500/user/all` route using the verb `GET` . When a request is within these rules the contents of the file `response.txt` will be returned:

```json
{
  "request": {
    "url": "/user/all",
    "method": "GET"
  },
  "response": {
    "status": 200,
    "bodyFileName": "response.txt",
    "headers": {
      "Content-Type": "application/json"
    }
  }
}
```

**Example of map-POST**

This map creates a route that will be hearing the `http://[SERVER]:5500/user/insert”all` route using the verb `POST` and when the body of the request is the same `{\"Name\":\"User3\",\"Age\":100}` . When a request is within these rules the contents of the file `response.txt` will be returned:

```json
{
  "request": {
    "url": "/user/insert",
    "method": "POST",
    "bodyPatterns" : [
      {
        "equalToJson" : "{\"Name\":\"User3\",\"Age\":100}"
      }
    ]
  },
  "response": {
    "status": 200,
    "bodyFileName": "response.txt",
    "headers": {
      "Content-Type": "application/json"
    }
  }
}
```

**Response:**

The answer is always "raw", without any encapsulation. As in earlier maps, we saw that the response is a file `application/json` , then that file will have the JSON content, if it was an image, this answer file would have the image extension, example: `response.jpg` and your content would be a binary.

```json
{
  "key1": "value"
}
```

**Testing:**

* Up the server
  * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5500 --root-dir "D:\wm\server1" --verbose`
* GET scenario:
  * **Url**:`http://localhost:5500/user/all`
  * **Method**:`GET`
* POST scenario:
  * **Url**:`http://localhost:5500/user/insert`
  * **Method**:`POST`
  * **Body**:`{ "Name": "User3", "Age": 100}`

## <a name="wiremock-start-as-record" />Execute as a proxy, but recording the data

It is very useful to load the first mass, after that, you can edit the generated files and can create several scenarios. To use the generated files need to change the form of execution for mock server.

**Testing:**

* Up the server in write mode:
  * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5502 --proxy-all "https://www.w3schools.com/" --record-mappings --root-dir "D:\wm\server2" --verbose --match-headers Content-Type`
* Open the URL in your browser:`http://localhost:5502`
* Stop the server
* Edit any file
* Restart the server in order to use the saved files
  * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5502 --root-dir "D:\wm\server2" --verbose`
* Clear the browser cache
* Rerun:`http://localhost:5502`

## <a name="wiremock-start-as-proxy" />Run only as a proxy

In our context, it can be useful when you need to use the original service without having to change the URL in the client.

**Running:**

* Up the server in proxy mode (ignoring the maps saved):
  * `java -jar "D:\wm\wiremock-standalone-2.8.0.jar" --port 5502 --proxy-all "https://www.w3schools.com/" --verbose`
* Open the URL in your browser:`http://localhost:5502`

# <a name="wiremock-how-work" />How does it work?

The `.JAR` of Wiremock does not run using processes. The `.JAR` last version of Wiremock was converted to .NET using the "IKVM". With this, it was possible to increase the use of the tool, having direct access to the main classes.

* It uses Windows Forms as a paradigm, so you need to have the .NET Framework 4.5 installed.
* All saved files will be saved in a folder named `.app` which is at the root of where's the `.exe` .

# <a name="how-to-contribute" />How to contribute

At the moment, I will not add new features due to lack of time, I will be available only for bugs and minor improvements. If you want to contribute with new ideas or fixes, just contact us or access the board of the project.

1. I see that the main point of improvement would be in the form "FormMaster". He's in a lot of lines and little componentised.
2. Another important point is to improve the persistence layer, at the moment, the calls are not centered leaving the dangerous situation for future improvements and this aggravates by being on a database in the form of a single file.

**Important links for the project:**

_IKVM_:

Tool that converts the Java Wiremock for .NET.

https://www.ikvm.net/

_PocDatabase_:

Framework to facilitate data persistence

https://github.com/juniorgasparotto/PocDatabase

_Board_:

https://github.com/juniorgasparotto/WiremockUI/projects/1?fullscreen=true

# <a name="license" />License

The MIT License (MIT)

Copyright (c) 2017 Glauber Donizeti Gasparotto Junior

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute , sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN THE EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

* * *

<sub>This text was translated by a machine</sub>

https://github.com/juniorgasparotto/MarkdownGenerator