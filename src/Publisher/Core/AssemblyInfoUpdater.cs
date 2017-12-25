using System;
using System.IO;
using System.Linq;

namespace Publisher.Core
{
    /// <summary>
    /// Utility class to update file assembly info files.
    /// </summary>
    public static class AssemblyInfoUpdater
    {
        /// <summary>
        /// Updates the specified assembly info file.
        /// </summary>
        /// <param name="file">File name.</param>
        /// <param name="attribute">Name of the attribute to update.</param>
        /// <param name="major">The major version number (empty, number or *).</param>
        /// <param name="minor">The minor version number (empty, number or *).</param>
        /// <param name="build">The build version number (empty, number or *).</param>
        /// <param name="revision">The revision version number (empty, number or *).</param>
        public static void Update(string file, string attribute, string major, string minor, string build, string revision)
        {
            string[] contents;
            string versionLine;
            int versionLineNumber;

            versionLine = GetAttrLine(file, attribute, out contents, out versionLineNumber);

            if (versionLine != null)
            {
                var version = versionLine.Substring(versionLine.IndexOf("(\"") + 2);
                version = version.Substring(0, version.LastIndexOf("\")"));
                var newVersion = ProcessVersionElements(version, major, minor, build, revision);
                var newVersionLine = versionLine.Replace(version, newVersion);
                contents[versionLineNumber] = newVersionLine;
                File.WriteAllLines(file, contents);
                Console.WriteLine("Updated version: {0}", newVersionLine);
            }
        }

        public static Version GetCurrentVersion(string file, string attribute)
        {
            string[] contents;
            string versionLine;
            int versionLineNumber;

            versionLine = GetAttrLine(file, attribute, out contents, out versionLineNumber);

            if (versionLine != null)
            {
                var version = versionLine.Substring(versionLine.IndexOf("(\"") + 2);
                version = version.Substring(0, version.LastIndexOf("\")"));
                return ProcessVersionElements(version);
            }

            return null;
        }

        public static string GetAttrLine(string file, string attribute, out string[] contents, out int versionLineNumber)
        {
            // open the file
            contents = File.ReadAllLines(file);

            // find the attribute
            versionLineNumber = 0;
            for (var i = 0; i < contents.Length; i++)
            {
                var content = contents[i];
                if (content.StartsWith("[assembly: " + attribute + "("))
                {
                    versionLineNumber = i;
                    return content;
                }
            }

            return null;
        }

        /// <summary>
        /// Processes the elements of a multipart version number and applies any updates required
        /// as indicated by the other arguments.
        /// </summary>
        /// <param name="version">The multipart version number.</param>
        /// <param name="major">The major version number (empty, number or *).</param>
        /// <param name="minor">The minor version number (empty, number or *).</param>
        /// <param name="build">The build version number (empty, number or *).</param>
        /// <param name="revision">The revision version number (empty, number or *).</param>
        /// <returns>An updated multipart version number.</returns>
        private static string ProcessVersionElements(string version, string major, string minor, string build, string revision)
        {
            // split version info
            var versionPartsList = version.Split('.').ToList();

            var majorPart = major;
            var minorPart = minor;
            var buildPart = build;
            var revisionPart = revision;

            // remove any blank version elements
            versionPartsList.RemoveAll(s => s.Length == 0);
            var versionParts = versionPartsList.ToArray();

            // update the major number?
            if (versionParts.Length >= 1)
                majorPart = ProcessVersionElement(versionParts[0], major);
            else
                majorPart = string.Empty;

            // update the minor number?
            if (versionParts.Length >= 2)
                minorPart = "." + ProcessVersionElement(versionParts[1], minor);
            else
                minorPart = string.Empty;

            // update the build number?
            if (versionParts.Length >= 3)
                buildPart = "." + ProcessVersionElement(versionParts[2], build);
            else
                buildPart = string.Empty;

            // update the revision number?
            if (versionParts.Length >= 4)
                revisionPart = "." + ProcessVersionElement(versionParts[3], revision);
            else
                revisionPart = string.Empty;

            // build the new version number
            var newVersion = string.Format("{0}{1}{2}{3}", majorPart, minorPart, buildPart, revisionPart);

            return newVersion;
        }

        private static Version ProcessVersionElements(string version)
        {
            // split version info
            var versionPartsList = version.Split('.').ToList();

            // remove any blank version elements
            versionPartsList.RemoveAll(s => s.Length == 0);
            var versionParts = versionPartsList.ToArray();

            int major, minor, build, revision = 0;

            int.TryParse(versionParts.ElementAtOrDefault(0), out major);
            int.TryParse(versionParts.ElementAtOrDefault(1), out minor);
            int.TryParse(versionParts.ElementAtOrDefault(2), out build);
            int.TryParse(versionParts.ElementAtOrDefault(3), out revision);

            return new Version(major, minor, build, revision);
        }

        /// <summary>
        /// Examines an existing multipart version element and either increments its value, leaves it
        /// as is or updates it with the specified new value.
        /// </summary>
        /// <param name="oldValue">The existing version element value (may be empty).</param>
        /// <param name="newValue">A value or * (indicates the old value should be incremented.</param>
        /// <returns>Updated version element.</returns>
        private static string ProcessVersionElement(string oldValue, string newValue)
        {
            if (newValue == "*" && (oldValue != null && oldValue != "*"))
                return (int.Parse(oldValue) + 1).ToString();
            if (newValue?.Length > 0)
                return newValue;
            if (oldValue == string.Empty)
                return "0";

            return oldValue;
        }
    }
}
