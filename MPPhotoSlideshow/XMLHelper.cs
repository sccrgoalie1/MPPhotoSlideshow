using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Serialization;

namespace MPPhotoSlideshow
{
    public static class XMLHelper
    {

        public static T Deserialize<T>(string fromXML)
        {
            try
            {
                XmlSerializer xmls = new XmlSerializer(typeof(T));
                StringReader sr = new StringReader(fromXML);
                return (T)xmls.Deserialize(sr);
            }
            catch
            {
                return default(T);
            }
        }

        public static string Serialize<T>(T obj)
        {
            try
            {
                XmlSerializer xmls = new XmlSerializer(typeof(T));

                using (StringWriter stream = new StringWriter())
                {
                    xmls.Serialize(stream, obj);
                    stream.Flush();
                    return stream.ToString();
                }
            }
            catch
            {
                return null;
            }
        }



    }
}
