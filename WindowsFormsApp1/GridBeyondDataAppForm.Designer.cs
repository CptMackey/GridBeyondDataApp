namespace WindowsFormsApp1
{
    partial class GridBeyondForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.marketChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gridBeyondDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridBeyondDBDataSet = new GridBeyondDataApp.GridBeyondDBDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.marketChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBeyondDBDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBeyondDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // marketChart
            // 
            chartArea1.Name = "ChartArea1";
            this.marketChart.ChartAreas.Add(chartArea1);
            this.marketChart.DataSource = this.gridBeyondDBDataSetBindingSource;
            legend1.Name = "Legend";
            this.marketChart.Legends.Add(legend1);
            this.marketChart.Location = new System.Drawing.Point(13, 12);
            this.marketChart.Name = "marketChart";
            this.marketChart.Size = new System.Drawing.Size(775, 425);
            this.marketChart.TabIndex = 0;
            this.marketChart.Text = "MarketPrice";
            // 
            // gridBeyondDBDataSetBindingSource
            // 
            this.gridBeyondDBDataSetBindingSource.DataSource = this.gridBeyondDBDataSet;
            this.gridBeyondDBDataSetBindingSource.Position = 0;
            // 
            // gridBeyondDBDataSet
            // 
            this.gridBeyondDBDataSet.DataSetName = "GridBeyondDBDataSet";
            this.gridBeyondDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GridBeyondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.marketChart);
            this.Name = "GridBeyondForm";
            this.Text = "GridBeyondDataApp";
            ((System.ComponentModel.ISupportInitialize)(this.marketChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBeyondDBDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBeyondDBDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart marketChart;
        private System.Windows.Forms.BindingSource gridBeyondDBDataSetBindingSource;
        private GridBeyondDataApp.GridBeyondDBDataSet gridBeyondDBDataSet;
    }
}

