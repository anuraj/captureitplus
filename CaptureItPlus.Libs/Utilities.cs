using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace CaptureItPlus.Libs
{
    public static class Utilities
    {
        public static string Serialize<T>(T o)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                xmlSerializer.Serialize(stringWriter, o, namespaces);
                return stringBuilder.ToString();
            }
        }

        public static T Deserialize<T>(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                using (StringReader stringWriter = new StringReader(s))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(stringWriter);
                }
            }
            else
            {
                return default(T);
            }
        }
    }
}
