
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cisco.Runbook
{
    public static class Parameters
    {

        // For a Class Library
        static string exeConfigPath = System.Reflection.Assembly.GetAssembly(typeof(GetHXServer)).Location;

        public static string PathConfig { get; private set; } =
            exeConfigPath + ".config";
            
        public static string getParameter(string paramName)
        {
            string paramValue = string.Empty;
            using (Stream stream = File.OpenRead(PathConfig))
            {
                XDocument xdoc = XDocument.Load(stream);
                XElement element = xdoc.Element("configuration").Element("appSettings").Elements().First(a => a.Attribute("key").Value == paramName);
                paramValue = element.Attribute("value").Value;
            }
            return paramValue;
        }
    }

}
