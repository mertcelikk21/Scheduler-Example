using SchedulerForTcmbData.Abstract;
using SchedulerForTcmbData.Models;
using System.Globalization;
using System.Xml;
namespace SchedulerForTcmbData.Services
{
    public class TcmbRequestService : ITcmbRequestService
    {
        public List<CurrencyExchangeDto> GetData()
        {
            List<CurrencyExchangeDto> list = new List<CurrencyExchangeDto>();
            XmlTextReader xmlDocument = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml");
            int type = 0;
            while (xmlDocument.Read())
            {
                if (xmlDocument.NodeType == XmlNodeType.Element)
                {

                    if (xmlDocument.GetAttribute("CurrencyCode") == "USD")
                    {
                        type = 1;
                    }

                    if ((type == 1) && (xmlDocument.Name == "BanknoteSelling"))
                    {
                        xmlDocument.Read();
                        type = 0;
                        list.Add(new CurrencyExchangeDto
                        {
                            CurrencyName = "USD",
                            Value = float.Parse(xmlDocument.Value, CultureInfo.InvariantCulture)
                        });
                    }
                }
                if (xmlDocument.NodeType == XmlNodeType.Element)
                {
                    if (xmlDocument.GetAttribute("CurrencyCode") == "EUR")
                    {
                        type = 2;
                    }
                    if ((type == 2) && (xmlDocument.Name == "BanknoteSelling"))
                    {
                        xmlDocument.Read();
                        type = 0;

                        list.Add(new CurrencyExchangeDto
                        {
                            CurrencyName = "EUR",
                            Value = float.Parse(xmlDocument.Value, CultureInfo.InvariantCulture)
                        });
                    }
                }
                if (xmlDocument.NodeType == XmlNodeType.Element)
                {
                    if (xmlDocument.GetAttribute("CurrencyCode") == "GBP")
                    {
                        type = 3;
                    }
                    if ((type == 3) && (xmlDocument.Name == "BanknoteSelling"))
                    {
                        xmlDocument.Read();
                        type = 0;

                        list.Add(new CurrencyExchangeDto
                        {
                            CurrencyName = "GBP",
                            Value = float.Parse(xmlDocument.Value, CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
            return list;
        }
    }
}
