using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLParserV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1.Tests
{
    [TestClass()]
    public class CSVLocalFileReaderTests
    {
        [TestMethod()]
        public void GetAllDataTest_NullCheck_Pass()
        {
            // Arrange
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.filePath = "mps.csv";
            reader.Counter = 0;
            // Act
            List<string> LocalOutput = reader.GetAllData();
            // Assert
            Assert.IsNotNull(LocalOutput);
        }
        [TestMethod()]
        public void GetAllDataTest_TypeCheck_Pass()
        {
            // Arrange
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.filePath = "mps.csv";
            reader.Counter = 0;
            // Act
            List<string> LocalOutput = reader.GetAllData();
            // Assert
            Assert.IsInstanceOfType(LocalOutput, typeof(List<string>));
        }
        [TestMethod()]
        public void GetAllDataTest_CountCheck_Pass()
        {
            // Arrange
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.filePath = "mps.csv";
            reader.Counter = 0;
            // Act
            List<string> LocalOutput = reader.GetAllData();
            System.Diagnostics.Debug.WriteLine(LocalOutput.Count);
            // Assert
            Assert.IsTrue(LocalOutput.Count == 650);
        }
    }
}