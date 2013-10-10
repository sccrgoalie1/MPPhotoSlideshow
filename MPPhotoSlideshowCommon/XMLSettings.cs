using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MPPhotoSlideshowCommon
{
    public class XMLSettings
    {
        private XmlDocument xmlDoc;
        private XmlTextWriter xmlWriter;
        private string fullname;
        public XMLSettings(string @directory, string filename)
        {
            openXMLSettingsFile(directory, filename);
         
           
        }
        private void openXMLSettingsFile(string @directory, string filename)
        {
            if (directory.EndsWith(@"\"))
            {
                fullname = directory + filename;
            }
            else
            {
                fullname = directory + @"\" + filename;
            }
            if (!new FileInfo(fullname).Exists)
            {
                createXMLSettingsFile(directory, filename);
            }
            else
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(@fullname);  
            }

        }
        private void createXMLSettingsFile(string @directory, string filename)
        {
            if (directory.EndsWith(@"\"))
            {
                fullname = directory + filename;
            }
            else
            {
                fullname = directory + @"\" + filename;
            }
            if (!Directory.Exists (directory))
            {
                Directory .CreateDirectory (directory);
            }
            xmlDoc = new XmlDocument();
            xmlWriter = new XmlTextWriter(@fullname, System.Text.Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='utf-8'");
            xmlWriter.WriteStartElement("Root");
            //If WriteProcessingInstruction is used as above,
            //Do not use WriteEndElement() here
            //xmlWriter.WriteEndElement();
            //it will cause the <Root></Root> to be <Root />
            xmlWriter.Close();
            xmlDoc.Load(@fullname);       
        }
        public void writeToXMLFile(string node, string text)
        {
            Boolean matched = false;
            try
            {
                XmlNode root = xmlDoc.DocumentElement;
                foreach (XmlNode s in xmlDoc )
                {
                    foreach (XmlNode d in s.ChildNodes)
                    {
                        if (d.Name == node)
                        {
                            d.InnerText = text;
                            matched = true;
                            break;
                        }
                    }
                }
                if (!matched)
                {
                    XmlElement childNode = xmlDoc.CreateElement(node);
                    root.AppendChild(childNode);
                    XmlText xmltext = xmlDoc.CreateTextNode(text);
                    childNode.AppendChild(xmltext);
                }


                //XmlElement childNode2 = xmlDoc.CreateElement("SecondChildNode");
                //XmlText textNode = xmlDoc.CreateTextNode("hello");
                //textNode.Value = "hello, world";


                //childNode.AppendChild(childNode2);
                //childNode2.SetAttribute("Name", "Value");
                //childNode2.AppendChild(textNode);

                //textNode.Value = "replacing hello world";
                xmlDoc.Save(@fullname);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public string getXmlAttribute(string tag)
        {
            string item = "";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@fullname);
            XmlNodeList xmlItem = xDoc.GetElementsByTagName(tag);
            if (xmlItem.Count>0)
            {
                item = xmlItem[0].InnerText;
            }
            //Console.WriteLine(item);
            return item;
        }
        public string getXmlAttribute(string tag, string defaultvalue)
        {
          string item = "";
          XmlDocument xDoc = new XmlDocument();
          xDoc.Load(@fullname);
          XmlNodeList xmlItem = xDoc.GetElementsByTagName(tag);
          if (xmlItem.Count > 0)
          {
            item = xmlItem[0].InnerText;
          }
          //Console.WriteLine(item);
          if (item != "")
          {
            return item;
          }
          else
          {
            return defaultvalue;
          }
        }


        
        
    
    }
}
