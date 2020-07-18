using Gigabyte.ULightingEffects.Devices;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Aorus.X570.Pro.RgbController
{
    internal static class MemoryHelper
    {
        public static bool GetMemorySpdInfo(List<UleRamModuleInformation> lstInfo, string spddumpXml)
        {
            if (!File.Exists(spddumpXml))
                return false;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(spddumpXml);

            for (int index = 0; index < xmlDocument.SelectSingleNode("MemorySPDInfos").ChildNodes.Count; ++index)
            {
                string innerText1 = xmlDocument.SelectNodes("//Manufacture")[index].InnerText;
                string innerText2 = xmlDocument.SelectNodes("//PartNumber")[index].InnerText;
            
                lstInfo.Add(new UleRamModuleInformation(innerText1, innerText2));
            }

            return true;
        }
    }
}
