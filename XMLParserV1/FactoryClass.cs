using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1
{
    public static class FactoryClass
    {
        // Get Local CSV File Reader Object
        public static CSVLocalFileReader GetLocalReader(string path, int RecordCount)
        {
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.filePath = path;
            reader.Counter = RecordCount;
            return reader;
        }
        // GET Xml Reader Object
        public static XMLParserFromURL GetXMLParser(string url)
        {
            XMLParserFromURL Parser = new XMLParserFromURL(url);
            return Parser;
        }
        // 
        public static MemberOfParliament GetMP(string name, string ID, List<string> PaymentDateReceived, List<int> PaymentsReceived, List<string> DonnorNames)
        {
            MemberOfParliament MP = new MemberOfParliament(name, ID, PaymentDateReceived, PaymentsReceived, DonnorNames);
            return MP;
        }

    }
}
