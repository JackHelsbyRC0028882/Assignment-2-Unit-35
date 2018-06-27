using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Assignment_2_Unit_35
{
    public partial class Form1 : Form
    {
        class row
        {
            public double time;
            public double altitude;
            public double velocity;
            public double acceleration;
        }

        List<row> table = new List<row>();

        public Form1()
        {
            InitializeComponent();
        }

        private void calculateCurrent()
        {
            for (int i = 1; i < table.Count; i++)
            {
                double dQ = table[i].altitude - table[i - 1].altitude;
                double dt = table[i].time - table[i - 1].time;
                table[i].velocity = dQ / dt;
            }
        }
        private void calculateDCurrent()
        {
            for (int i = 2; i < table.Count; i++)
            {
                double dI = table[i].velocity - table[i - 1].velocity;
                double dt = table[i].time - table[i - 1].time;
                table[i].acceleration = dI / dt;
            }
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            openFileDialog2.FileName = "";
            openFileDialog2.Filter = "csv Files|*.csv";
            DialogResult result = openFileDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog2.FileName))
                    {
                        string line = sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            table.Add(new row());
                            string[] r = sr.ReadLine().Split(',');
                            table.Last().time = double.Parse(r[0]);
                            table.Last().altitude = double.Parse(r[1]);
                        }
                    }
                    calculateCurrent();
                    calculateDCurrent();
                }
                catch (IOException)
                {
                    MessageBox.Show(openFileDialog2.FileName + "failed to open.");
                }
                catch (FormalException)
                {
                    MessageBox.Show(openFileDialog2.FileName + "is not in the required format.");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show(openFileDialog2.FileName + "is not in the required format.");
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series = new Series
            {
                Name = "Velocity",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2
            };
            chart2.Series.Add(series);
            foreach (row r in table.Skip(1))
            {
                series.Points.AddXY(r.time, r.velocity);
            }
            chart2.ChartAreas[0].AxisX.Title = "time (s)";
            chart2.ChartAreas[0].AxisY.Title = "velocity (m/s)";
            chart2.ChartAreas[0].RecalculateAxesScale();
        }

        private void savePSGToolStripMenuItem_Click(object sender, EventArgs e)
        {
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "png Files|*.png";
                DialogResult results = saveFileDialog1.ShowDialog();
                if (results == DialogResult.OK)
                {
                    try
                    {
                        chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
                    }
                    catch
                    {
                        MessageBox.Show(saveFileDialog1.FileName + " failed to save.");
                    }
                }
        }
            private void saveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "csv Files|*.csv";
            DialogResult results = saveFileDialog1.ShowDialog();
            if (results == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.WriteLine("Time /s, Charge /C, Current /A, dCurrent / A/s");
                        foreach (row r in table)
                        {
                            sw.WriteLine(r.time + "," + r.altitude + "," + r.velocity + "," + r.acceleration);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(saveFileDialog1.FileName + " failed to save.");
                }

            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series = new Series
            {
                Name = "Altitude",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2
            };
            chart2.Series.Add(series);
            foreach (row r in table.Skip(1))
            {
                series.Points.AddXY(r.time, r.altitude);
            }
            chart2.ChartAreas[0].AxisX.Title = "time (s)";
            chart2.ChartAreas[0].AxisY.Title = "altitude (m)";
            chart2.ChartAreas[0].RecalculateAxesScale();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series = new Series
            {
                Name = "Acceleration",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2
            };
            chart2.Series.Add(series);
            foreach (row r in table.Skip(1))
            {
                series.Points.AddXY(r.time, r.acceleration);
            }
            chart2.ChartAreas[0].AxisX.Title = "time (s)";
            chart2.ChartAreas[0].AxisY.Title = "acceleration (m/s²)";
            chart2.ChartAreas[0].RecalculateAxesScale();
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
   

