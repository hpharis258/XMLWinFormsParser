using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace XMLParserV1
{
    public partial class Form1 : Form
    {
        public string CSVPath = "mps.csv";
        public string XMLUrl = "https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml";
        public DataTable DisplayData; 
        private BackgroundWorker Worker;
        public List<MemberOfParliament> memberOfParliamentsFormList { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAndDisplayData(21, false);
            // Show Message
            string Message = "Clink on the Link to find out more about each member!";
            MessageBox.Show(Message);
            // Run Background Worker to Get the Rest of the Data
            this.Worker = SingletonBackgroundWorker.GetWorker();
            this.Worker.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.Worker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.Worker.WorkerReportsProgress = true;
            if(!this.Worker.IsBusy)
            {
                 this.Worker.RunWorkerAsync();
            }
        }

        private void bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            // Debug Message 
            System.Diagnostics.Debug.WriteLine("Background Worker Completed!!!");
        }

        private void bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            // Not needed
            throw new NotImplementedException();
        }

        // Background Worker Do work
        private void bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            GetAndDisplayData(0, true); 
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // If Selected Cell Contains HTTP it is a link, open in it in the Browser
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (SelectedValue.Contains("http"))
                {
                    //System.Diagnostics.Debug.WriteLine(SelectedValue);
                    OpenUrl(SelectedValue);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //// LINQ Querry
            var data = from thing in memberOfParliamentsFormList
                       where thing.Name.Contains(textBox1.Text) || thing.Name.ToUpper().Contains(textBox1.Text.ToUpper()) || thing.Name.ToLower().Contains(textBox1.Text.ToLower())
                       select thing;
            //
            DisplayData.Clear();
            var dataToShow = UIHelper.AddDataToDataTable(DisplayData, data.ToList<MemberOfParliament>());
            dataGridView1.Invoke((MethodInvoker)(() => { dataGridView1.DataSource = dataToShow; }));
        }
        // Open URL Method
        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        // Get And Display Data 
        public void GetAndDisplayData(int numRecord, bool anotherThread)
        {
            // Use Facotory Class To Initialise Local CSV Reader
            var LocalCSVReader = FactoryClass.GetLocalReader(CSVPath, numRecord);
            List<string> LocalReaderOutput = LocalCSVReader.GetAllData();
            // Use Facotory Class To Initialise XML Parser
            var XMLParser = FactoryClass.GetXMLParser(XMLUrl);
            XMLParser.GetRecordCount = numRecord;
            List<MemberOfParliament> List = XMLParser.GetAllData();
            // Merge Local Reader Data with XML Data
            UIHelper.MergeLocalCSVtoMemParliamentList(List, LocalReaderOutput);
            // Init Data Table 
            DataTable table = UIHelper.GetDataTable();
            // Add Data To Data Table
            UIHelper.AddDataToDataTable(table, List);
            // Set Data Table as the Source of Data Grid
            if (anotherThread == false)
            {
                dataGridView1.DataSource = table;
                DisplayData = table;
                // Allow to use List with data for Search
                memberOfParliamentsFormList = List;
            }
            else // Seperate Thread
            {
                dataGridView1.Invoke((MethodInvoker)(() => { dataGridView1.DataSource = table; }));
                DisplayData = table;
                memberOfParliamentsFormList = List;
            }
        }
    }
}