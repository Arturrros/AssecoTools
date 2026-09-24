namespace ClassMonitor
{
    partial class FormMemory
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChartSGA = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartPGA = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSGA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartPGA)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartSGA
            // 
            chartArea1.Name = "ChartArea1";
            this.ChartSGA.ChartAreas.Add(chartArea1);
            this.ChartSGA.Dock = System.Windows.Forms.DockStyle.Left;
            legend1.Name = "SGAINFOLegend";
            legend2.Name = "SGALegend";
            this.ChartSGA.Legends.Add(legend1);
            this.ChartSGA.Legends.Add(legend2);
            this.ChartSGA.Location = new System.Drawing.Point(0, 0);
            this.ChartSGA.Name = "ChartSGA";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "SGALegend";
            series1.Name = "SGA";
            this.ChartSGA.Series.Add(series1);
            this.ChartSGA.Size = new System.Drawing.Size(600, 341);
            this.ChartSGA.TabIndex = 0;
            this.ChartSGA.Text = "chart1";
            this.ChartSGA.Click += new System.EventHandler(this.ChartSGA_Click);
            // 
            // ChartPGA
            // 
            chartArea2.Name = "ChartArea1";
            this.ChartPGA.ChartAreas.Add(chartArea2);
            this.ChartPGA.Dock = System.Windows.Forms.DockStyle.Left;
            legend3.Name = "PGAINFOLegend";
            legend4.Name = "PGALegend";
            this.ChartPGA.Legends.Add(legend3);
            this.ChartPGA.Legends.Add(legend4);
            this.ChartPGA.Location = new System.Drawing.Point(600, 0);
            this.ChartPGA.Name = "ChartPGA";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "PGALegend";
            series2.Name = "PGA";
            this.ChartPGA.Series.Add(series2);
            this.ChartPGA.Size = new System.Drawing.Size(600, 341);
            this.ChartPGA.TabIndex = 1;
            this.ChartPGA.Text = "chart2";
            this.ChartPGA.Click += new System.EventHandler(this.ChartPGA_Click);
            // 
            // FormMemory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 341);
            this.Controls.Add(this.ChartPGA);
            this.Controls.Add(this.ChartSGA);
            this.Name = "FormMemory";
            this.Text = "Memory View";
            this.Load += new System.EventHandler(this.FormMemory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartSGA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartPGA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChartSGA;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartPGA;
    }
}