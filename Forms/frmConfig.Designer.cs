﻿namespace BMS
{
	partial class frmConfig
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numFontTitleAndon = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.numFontLabelPlan = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.btnSaveFontSize = new System.Windows.Forms.Button();
			this.numFontValuePlan = new System.Windows.Forms.NumericUpDown();
			this.numFontTitleCD = new System.Windows.Forms.NumericUpDown();
			this.numFontValueCD = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtTakt = new System.Windows.Forms.TextBox();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.txtTcpIp = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnSaveAreaPLC = new System.Windows.Forms.Button();
			this.grvAreaPLC = new System.Windows.Forms.DataGridView();
			this.colNameCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAreaDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAreaRisk = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.numValueTakt = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.numLabelTakt = new System.Windows.Forms.NumericUpDown();
			this.lblTakt = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numFontTitleAndon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontLabelPlan)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontValuePlan)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontTitleCD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontValueCD)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grvAreaPLC)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numValueTakt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numLabelTakt)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numValueTakt);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.numLabelTakt);
			this.groupBox1.Controls.Add(this.lblTakt);
			this.groupBox1.Controls.Add(this.numFontTitleAndon);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.numFontLabelPlan);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.btnSaveFontSize);
			this.groupBox1.Controls.Add(this.numFontValuePlan);
			this.groupBox1.Controls.Add(this.numFontTitleCD);
			this.groupBox1.Controls.Add(this.numFontValueCD);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.txtTakt);
			this.groupBox1.Controls.Add(this.txtPort);
			this.groupBox1.Controls.Add(this.txtTcpIp);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 14);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(555, 630);
			this.groupBox1.TabIndex = 66;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Config";
			// 
			// numFontTitleAndon
			// 
			this.numFontTitleAndon.DecimalPlaces = 2;
			this.numFontTitleAndon.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numFontTitleAndon.Location = new System.Drawing.Point(194, 401);
			this.numFontTitleAndon.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.numFontTitleAndon.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontTitleAndon.Name = "numFontTitleAndon";
			this.numFontTitleAndon.Size = new System.Drawing.Size(347, 40);
			this.numFontTitleAndon.TabIndex = 7;
			this.numFontTitleAndon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFontTitleAndon.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontTitleAndon.ValueChanged += new System.EventHandler(this.numFontTitleAndon_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(-2, 409);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(165, 32);
			this.label8.TabIndex = 31;
			this.label8.Text = "Title Andon:";
			// 
			// numFontLabelPlan
			// 
			this.numFontLabelPlan.DecimalPlaces = 2;
			this.numFontLabelPlan.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numFontLabelPlan.Location = new System.Drawing.Point(197, 349);
			this.numFontLabelPlan.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.numFontLabelPlan.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontLabelPlan.Name = "numFontLabelPlan";
			this.numFontLabelPlan.Size = new System.Drawing.Size(347, 40);
			this.numFontLabelPlan.TabIndex = 6;
			this.numFontLabelPlan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFontLabelPlan.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontLabelPlan.ValueChanged += new System.EventHandler(this.numFontLabelPlan_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(7, 357);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(156, 32);
			this.label7.TabIndex = 29;
			this.label7.Text = "Label Plan:";
			// 
			// btnSaveFontSize
			// 
			this.btnSaveFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveFontSize.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSaveFontSize.Location = new System.Drawing.Point(403, 583);
			this.btnSaveFontSize.Name = "btnSaveFontSize";
			this.btnSaveFontSize.Size = new System.Drawing.Size(141, 41);
			this.btnSaveFontSize.TabIndex = 28;
			this.btnSaveFontSize.Text = "Save";
			this.btnSaveFontSize.UseVisualStyleBackColor = true;
			this.btnSaveFontSize.Click += new System.EventHandler(this.btnSaveFontSize_Click);
			// 
			// numFontValuePlan
			// 
			this.numFontValuePlan.DecimalPlaces = 2;
			this.numFontValuePlan.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numFontValuePlan.Location = new System.Drawing.Point(197, 299);
			this.numFontValuePlan.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.numFontValuePlan.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontValuePlan.Name = "numFontValuePlan";
			this.numFontValuePlan.Size = new System.Drawing.Size(347, 40);
			this.numFontValuePlan.TabIndex = 5;
			this.numFontValuePlan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFontValuePlan.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontValuePlan.ValueChanged += new System.EventHandler(this.numFontValuePlan_ValueChanged);
			// 
			// numFontTitleCD
			// 
			this.numFontTitleCD.DecimalPlaces = 2;
			this.numFontTitleCD.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numFontTitleCD.Location = new System.Drawing.Point(197, 244);
			this.numFontTitleCD.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.numFontTitleCD.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontTitleCD.Name = "numFontTitleCD";
			this.numFontTitleCD.Size = new System.Drawing.Size(347, 40);
			this.numFontTitleCD.TabIndex = 4;
			this.numFontTitleCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFontTitleCD.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontTitleCD.ValueChanged += new System.EventHandler(this.numFontTitleCD_ValueChanged);
			// 
			// numFontValueCD
			// 
			this.numFontValueCD.DecimalPlaces = 2;
			this.numFontValueCD.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numFontValueCD.Location = new System.Drawing.Point(197, 190);
			this.numFontValueCD.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.numFontValueCD.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontValueCD.Name = "numFontValueCD";
			this.numFontValueCD.Size = new System.Drawing.Size(347, 40);
			this.numFontValueCD.TabIndex = 3;
			this.numFontValueCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numFontValueCD.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numFontValueCD.ValueChanged += new System.EventHandler(this.numFontValueCD_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(9, 307);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(154, 32);
			this.label5.TabIndex = 24;
			this.label5.Text = "Value Plan:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(23, 252);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(140, 32);
			this.label6.TabIndex = 23;
			this.label6.Text = "Label CD:";
			// 
			// txtTakt
			// 
			this.txtTakt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTakt.Location = new System.Drawing.Point(197, 127);
			this.txtTakt.Name = "txtTakt";
			this.txtTakt.Size = new System.Drawing.Size(347, 40);
			this.txtTakt.TabIndex = 2;
			this.txtTakt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtTakt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
			// 
			// txtPort
			// 
			this.txtPort.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPort.Location = new System.Drawing.Point(197, 76);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(347, 40);
			this.txtPort.TabIndex = 1;
			this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTcpIp
			// 
			this.txtTcpIp.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTcpIp.Location = new System.Drawing.Point(197, 27);
			this.txtTcpIp.Name = "txtTcpIp";
			this.txtTcpIp.Size = new System.Drawing.Size(347, 40);
			this.txtTcpIp.TabIndex = 0;
			this.txtTcpIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(25, 198);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(138, 32);
			this.label4.TabIndex = 22;
			this.label4.Text = "Value CD:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(86, 135);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 32);
			this.label3.TabIndex = 21;
			this.label3.Text = "Takt:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(86, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 32);
			this.label2.TabIndex = 19;
			this.label2.Text = "Port:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(39, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(124, 32);
			this.label1.TabIndex = 17;
			this.label1.Text = "TCP-IP: ";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnSaveAreaPLC);
			this.groupBox3.Controls.Add(this.grvAreaPLC);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(573, 14);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(633, 630);
			this.groupBox3.TabIndex = 68;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Config Address Area";
			// 
			// btnSaveAreaPLC
			// 
			this.btnSaveAreaPLC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveAreaPLC.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSaveAreaPLC.Location = new System.Drawing.Point(486, 582);
			this.btnSaveAreaPLC.Name = "btnSaveAreaPLC";
			this.btnSaveAreaPLC.Size = new System.Drawing.Size(141, 41);
			this.btnSaveAreaPLC.TabIndex = 62;
			this.btnSaveAreaPLC.Text = "Save";
			this.btnSaveAreaPLC.UseVisualStyleBackColor = true;
			this.btnSaveAreaPLC.Click += new System.EventHandler(this.btnSaveAreaPLC_Click);
			// 
			// grvAreaPLC
			// 
			this.grvAreaPLC.AllowUserToAddRows = false;
			this.grvAreaPLC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grvAreaPLC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.grvAreaPLC.BackgroundColor = System.Drawing.Color.White;
			this.grvAreaPLC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grvAreaPLC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNameCD,
            this.colAreaDelay,
            this.colAreaRisk});
			this.grvAreaPLC.Location = new System.Drawing.Point(6, 34);
			this.grvAreaPLC.Name = "grvAreaPLC";
			this.grvAreaPLC.RowHeadersWidth = 25;
			this.grvAreaPLC.RowTemplate.Height = 28;
			this.grvAreaPLC.Size = new System.Drawing.Size(615, 542);
			this.grvAreaPLC.TabIndex = 0;
			// 
			// colNameCD
			// 
			this.colNameCD.DataPropertyName = "NameCD";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colNameCD.DefaultCellStyle = dataGridViewCellStyle1;
			this.colNameCD.FillWeight = 20F;
			this.colNameCD.HeaderText = "Name CD";
			this.colNameCD.MinimumWidth = 8;
			this.colNameCD.Name = "colNameCD";
			this.colNameCD.ReadOnly = true;
			// 
			// colAreaDelay
			// 
			this.colAreaDelay.DataPropertyName = "AreaDelay";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colAreaDelay.DefaultCellStyle = dataGridViewCellStyle2;
			this.colAreaDelay.FillWeight = 35F;
			this.colAreaDelay.HeaderText = "Area Delay";
			this.colAreaDelay.MinimumWidth = 8;
			this.colAreaDelay.Name = "colAreaDelay";
			// 
			// colAreaRisk
			// 
			this.colAreaRisk.DataPropertyName = "AreaRisk";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colAreaRisk.DefaultCellStyle = dataGridViewCellStyle3;
			this.colAreaRisk.FillWeight = 35F;
			this.colAreaRisk.HeaderText = "Area Risk";
			this.colAreaRisk.MinimumWidth = 8;
			this.colAreaRisk.Name = "colAreaRisk";
			// 
			// numValueTakt
			// 
			this.numValueTakt.DecimalPlaces = 2;
			this.numValueTakt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numValueTakt.Location = new System.Drawing.Point(194, 503);
			this.numValueTakt.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.numValueTakt.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numValueTakt.Name = "numValueTakt";
			this.numValueTakt.Size = new System.Drawing.Size(347, 40);
			this.numValueTakt.TabIndex = 33;
			this.numValueTakt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numValueTakt.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numValueTakt.ValueChanged += new System.EventHandler(this.numValueTakt_ValueChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(13, 511);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(150, 32);
			this.label9.TabIndex = 35;
			this.label9.Text = "Value Takt:";
			// 
			// numLabelTakt
			// 
			this.numLabelTakt.DecimalPlaces = 2;
			this.numLabelTakt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numLabelTakt.Location = new System.Drawing.Point(197, 451);
			this.numLabelTakt.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
			this.numLabelTakt.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numLabelTakt.Name = "numLabelTakt";
			this.numLabelTakt.Size = new System.Drawing.Size(347, 40);
			this.numLabelTakt.TabIndex = 32;
			this.numLabelTakt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numLabelTakt.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numLabelTakt.ValueChanged += new System.EventHandler(this.numLabelTakt_ValueChanged);
			// 
			// lblTakt
			// 
			this.lblTakt.AutoSize = true;
			this.lblTakt.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTakt.Location = new System.Drawing.Point(11, 459);
			this.lblTakt.Name = "lblTakt";
			this.lblTakt.Size = new System.Drawing.Size(152, 32);
			this.lblTakt.TabIndex = 34;
			this.lblTakt.Text = "Label Takt:";
			// 
			// frmConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1223, 670);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmConfig";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CONFIG";
			this.Load += new System.EventHandler(this.frmConfig_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numFontTitleAndon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontLabelPlan)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontValuePlan)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontTitleCD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numFontValueCD)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grvAreaPLC)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numValueTakt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numLabelTakt)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown numFontValuePlan;
		private System.Windows.Forms.NumericUpDown numFontTitleCD;
		private System.Windows.Forms.NumericUpDown numFontValueCD;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtTakt;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.TextBox txtTcpIp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSaveFontSize;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView grvAreaPLC;
		private System.Windows.Forms.Button btnSaveAreaPLC;
		private System.Windows.Forms.DataGridViewTextBoxColumn colNameCD;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAreaDelay;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAreaRisk;
		private System.Windows.Forms.NumericUpDown numFontTitleAndon;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numFontLabelPlan;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numValueTakt;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown numLabelTakt;
		private System.Windows.Forms.Label lblTakt;
	}
}