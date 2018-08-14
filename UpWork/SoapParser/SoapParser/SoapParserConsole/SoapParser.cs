using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SoapParserConsole
{
    public class SoapParser
    {
        private readonly XElement m_soapFromFile;
        public SoapParser(string fileName)
        {
            m_soapFromFile = XElement.Load(fileName);
        }

        public void Print()
        {
            PrintElemets(".", m_soapFromFile);
        }

        public string GetSessionID()
        {
            return Find_pcSessionID(m_soapFromFile);
        }

        private void PrintElemets(string prefix, XElement node)
        {
            foreach (XElement element in node.Elements())
            {
                Console.WriteLine(prefix + element.Name);
                foreach (XElement elementI in element.Elements())
                {
                    PrintElemets(prefix + ".", elementI);
                }
            }
        }

        private string Find_pcSessionID(XElement node)
        {
            foreach (XElement element in node.Elements())
            {
                if (element.Name.LocalName == "contextName" && element.Value == "pcSessionID")
                {
                    List<XElement> allParentElements = element.Parent.Elements().ToList();
                    string foundValue = allParentElements.Find(e => e.Name.LocalName == "contextValue").Value;
                    return foundValue;
                }
                foreach (XElement elementI in element.Elements())
                {
                    string value = Find_pcSessionID(elementI);
                    if (value != null)
                        return value;
                }
            }

            return null;
        }
    }
}