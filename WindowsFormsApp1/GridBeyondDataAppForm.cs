using GridBeyondDataApp;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class GridBeyondForm : Form
    {
        public GridBeyondForm()
        {
            InitializeComponent();
            ReadCSVFile();
            PopulateChart();
        }

        // Uses TextFieldParser to read the CSV file and Insert the data to the Database
        public void ReadCSVFile()
        {
            using (TextFieldParser parser = new TextFieldParser(@"ExportFile.csv"))
            {
                bool firstLine = true;
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    // Skip header row
                    if (firstLine)
                    {
                        firstLine = false;
                        continue;
                    }

                    DatabaseAccess.InsertValues(parser.ReadFields());
                }
            }
        }

        // Populates the chart with values for MAX, MIN, AVG and Highest Consecutive values
        public void PopulateChart()
        {
            var maxQuery = "SELECT MAX(Market_Price_EX1) FROM dbo.MarketPrice";
            var minQuery = "SELECT MIN(Market_Price_EX1) FROM dbo.MarketPrice";
            var avgQuery = "SELECT AVG(Market_Price_EX1) FROM dbo.MarketPrice";

            // Ideally wanted to get this to just return a sum of total MarketPrice where the dates exist within an hour of each other
            // as well as the hour in question for the values
            // Couldn't get it working correctly. I've tried quite a few different queries,but unfortunately was unable to resolve it
            var highestValuesQuery = @"SELECT date, Market_Price_EX1
                                        FROM(SELECT MarketPrice.*,
                                                     (row_number() over(ORDER BY date) -
                                                      row_number() over(PARTITION BY Market_Price_EX1 ORDER BY date)
                                                     ) AS grp
                                              FROM MarketPrice
                                             ) AS mp
                                        GROUP BY Market_Price_EX1, date
                                        ORDER BY Market_Price_EX1 DESC; ";

            var mostExpensiveTimeDataTable = DatabaseAccess.GetData(highestValuesQuery);

            var maxDataTable = DatabaseAccess.GetData(maxQuery);
            var minDataTable = DatabaseAccess.GetData(minQuery);
            var avgDataTable = DatabaseAccess.GetData(avgQuery);

            double.TryParse(maxDataTable.Rows[0].ItemArray[0].ToString(), out double maxValue);
            double.TryParse(minDataTable.Rows[0].ItemArray[0].ToString(), out double minValue);
            double.TryParse(avgDataTable.Rows[0].ItemArray[0].ToString(), out double avgValue);

            // This is a hack 
            double.TryParse(mostExpensiveTimeDataTable.Rows[1].ItemArray[1].ToString(), out double firstValue);
            double.TryParse(mostExpensiveTimeDataTable.Rows[2].ItemArray[1].ToString(), out double secondValue);

            DateTime.TryParse(mostExpensiveTimeDataTable.Rows[1].ItemArray[0].ToString(), out DateTime firstTimeSlot);

            var dataTable = new DataTable();
            _ = new DataColumn();
            dataTable.Columns.Add("Value", typeof(double));

            DataColumn dataColumn = new DataColumn();
            dataColumn.ColumnName = "Type";
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();
            dataRow["Value"] = minValue;
            dataRow["Type"] = "MIN";
            dataTable.Rows.Add(dataRow);

            dataRow = dataTable.NewRow();
            dataRow["Value"] = avgValue;
            dataRow["Type"] = "AVG";
            dataTable.Rows.Add(dataRow);

            dataRow = dataTable.NewRow();
            dataRow["Value"] = maxValue;
            dataRow["Type"] = "MAX";
            dataTable.Rows.Add(dataRow);

            dataRow = dataTable.NewRow();
            dataRow["Value"] = firstValue + secondValue;
            dataRow["Type"] = firstTimeSlot.TimeOfDay.ToString() + " TO " + firstTimeSlot.AddHours(1).TimeOfDay.ToString();
            dataTable.Rows.Add(dataRow);

            var dataView = dataTable.DefaultView;
            dataView.Sort = "Value ASC";

            var IEtable = (dataTable as IListSource).GetList();
            marketChart.DataBindTable(IEtable, "Type");

            // Only one series in the chart, no need to show a legend of 1 series
            marketChart.Legends.Clear();

            // Make it so each item in the barchart has a separate colour
            Color[] colors = new Color[] { Color.Green, Color.Red, Color.Blue, Color.Purple };
            foreach (Series series in marketChart.Series)
            {
                foreach (DataPoint point in series.Points)
                {
                    point.Color = colors[series.Points.IndexOf(point)];
                }
            }
        }
    }
}
