using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLParserV1
{
    public static class UIHelper
    {
        public static DataTable GetDataTable()
        {
            // Create new Data Table Instance
            DataTable table = new DataTable();
            // Add Table collumns
            table.Columns.Add("Index", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Party", typeof(string));
            table.Columns.Add("Constituency", typeof(string));
            table.Columns.Add("Donors", typeof(string));
            table.Columns.Add("Amount Received", typeof(int));
            table.Columns.Add("Image", typeof(Image));
            table.Columns.Add("Link", typeof(string));
            // 
            return table;
        }
        // Populate Datatable with data
        public static DataTable AddDataToDataTable(DataTable table, List<MemberOfParliament> List)
        {
            for (int i = 0; i < List.Count - 1; i++)
            {
                int index = i + 1;
                string Name = List[i].Name;
                string ID = List[i].Id;
                string Party = List[i].Party;
                string Constituency = List[i].Constituency;
                string DisplayDonor = List[i].DisplayDonnorNames;
                int Donations = List[i].FullAmountReceived;
                Image memPhoto = List[i].MemberPhoto;
                string Link = List[i].FullLink;
                // Clean Political Data
                Party = ReplaceNonAlhpaNumeric(Party);
                Constituency = ReplaceNonAlhpaNumeric(Constituency);
                table.Rows.Add(index, Name, ID, Party, Constituency, DisplayDonor, Donations, memPhoto, Link);
            }
                return table;
        }
        // Regex Helper Method
        public static string ReplaceNonAlhpaNumeric(string value)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            string str = rgx.Replace(value, "");
            return str;
        }
        public static List<MemberOfParliament> MergeLocalCSVtoMemParliamentList(List<MemberOfParliament> List, List<string> CSVOutput)
        {
            for (int i = 0; i < List.Count; i++)
            {
                string LocalCSVLine = CSVOutput[i + 1];
                var values = LocalCSVLine.Split(',');
                //Party
                List[i].Party = values[3];
                // Constituency 
                List[i].Constituency = values[4];
            }
            return List;
        }
        //
        public static string RemoveNonNumeric(string input)
        {
            string clean = Regex.Replace(input, "[^0-9]", "");
            return clean;
        }
    }
}
