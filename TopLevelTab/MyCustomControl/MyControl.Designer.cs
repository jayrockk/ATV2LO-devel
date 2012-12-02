namespace FTR2LO_Vail
{
    partial class MyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_Apply = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.label_tasklistxmlfilepath = new System.Windows.Forms.Label();
            this.textBox_tasklistxmlfilepath = new System.Windows.Forms.TextBox();
            this.numericUpDown_EarlyStart = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_ServerPort = new System.Windows.Forms.NumericUpDown();
            this.label_EarlyStart = new System.Windows.Forms.Label();
            this.textBox_ServerName = new System.Windows.Forms.TextBox();
            this.label_ServerPort = new System.Windows.Forms.Label();
            this.label_ServerName = new System.Windows.Forms.Label();
            this.button_Defaults = new System.Windows.Forms.Button();
            this.button_TasklistxmlFileDialog = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxPurgeOldItems = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox_line = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxFTR2LO = new System.Windows.Forms.PictureBox();
            this.pictureBoxFTR = new System.Windows.Forms.PictureBox();
            this.pictureBoxLO = new System.Windows.Forms.PictureBox();
            this.label_actual_LOstatus = new System.Windows.Forms.Label();
            this.label_LightsOut_service_status = new System.Windows.Forms.Label();
            this.label_actual_server_status = new System.Windows.Forms.Label();
            this.label_actual_FTRstatus = new System.Windows.Forms.Label();
            this.label_ForTheRecord_service_status = new System.Windows.Forms.Label();
            this.label_FTR2LO_service_status = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label_PurgeOldItems = new System.Windows.Forms.Label();
            this.loadingCircle_FTR2LO = new MRG.Controls.UI.LoadingCircle();
            this.loadingCircle_FTR = new MRG.Controls.UI.LoadingCircle();
            this.loadingCircle_LO = new MRG.Controls.UI.LoadingCircle();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.labelDebugInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EarlyStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ServerPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFTR2LO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFTR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLO)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Apply
            // 
            this.button_Apply.Location = new System.Drawing.Point(12, 342);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(168, 23);
            this.button_Apply.TabIndex = 0;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(452, 342);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(79, 23);
            this.Button_Cancel.TabIndex = 1;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click_1);
            // 
            // label_tasklistxmlfilepath
            // 
            this.label_tasklistxmlfilepath.AutoSize = true;
            this.label_tasklistxmlfilepath.BackColor = System.Drawing.Color.Transparent;
            this.label_tasklistxmlfilepath.Location = new System.Drawing.Point(6, 26);
            this.label_tasklistxmlfilepath.Name = "label_tasklistxmlfilepath";
            this.label_tasklistxmlfilepath.Size = new System.Drawing.Size(61, 13);
            this.label_tasklistxmlfilepath.TabIndex = 26;
            this.label_tasklistxmlfilepath.Text = "Tasklist.xml";
            // 
            // textBox_tasklistxmlfilepath
            // 
            this.textBox_tasklistxmlfilepath.Location = new System.Drawing.Point(91, 23);
            this.textBox_tasklistxmlfilepath.Name = "textBox_tasklistxmlfilepath";
            this.textBox_tasklistxmlfilepath.Size = new System.Drawing.Size(95, 20);
            this.textBox_tasklistxmlfilepath.TabIndex = 27;
            this.toolTip1.SetToolTip(this.textBox_tasklistxmlfilepath, "Path to LightsOut tasklist file. This can normally stay untouched.");
            this.textBox_tasklistxmlfilepath.Leave += new System.EventHandler(this.textBox_tasklistxmlfilepath_Leave_1);
            // 
            // numericUpDown_EarlyStart
            // 
            this.numericUpDown_EarlyStart.Location = new System.Drawing.Point(91, 22);
            this.numericUpDown_EarlyStart.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown_EarlyStart.Name = "numericUpDown_EarlyStart";
            this.numericUpDown_EarlyStart.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown_EarlyStart.TabIndex = 35;
            this.toolTip1.SetToolTip(this.numericUpDown_EarlyStart, "If you want the computer to wake up some minutes before the recording, set it her" +
                    "e.");
            this.numericUpDown_EarlyStart.Leave += new System.EventHandler(this.numericUpDown_EarlyStart_Leave);
            // 
            // numericUpDown_ServerPort
            // 
            this.numericUpDown_ServerPort.Location = new System.Drawing.Point(91, 49);
            this.numericUpDown_ServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_ServerPort.Name = "numericUpDown_ServerPort";
            this.numericUpDown_ServerPort.Size = new System.Drawing.Size(77, 20);
            this.numericUpDown_ServerPort.TabIndex = 34;
            this.toolTip1.SetToolTip(this.numericUpDown_ServerPort, "Port that For the Record server runs on. This can normally stay untouched.");
            this.numericUpDown_ServerPort.Leave += new System.EventHandler(this.numericUpDown_ServerPort_Leave);
            // 
            // label_EarlyStart
            // 
            this.label_EarlyStart.AutoSize = true;
            this.label_EarlyStart.BackColor = System.Drawing.Color.Transparent;
            this.label_EarlyStart.Location = new System.Drawing.Point(6, 26);
            this.label_EarlyStart.Name = "label_EarlyStart";
            this.label_EarlyStart.Size = new System.Drawing.Size(59, 13);
            this.label_EarlyStart.TabIndex = 30;
            this.label_EarlyStart.Text = "Early wake";
            // 
            // textBox_ServerName
            // 
            this.textBox_ServerName.Location = new System.Drawing.Point(91, 23);
            this.textBox_ServerName.Name = "textBox_ServerName";
            this.textBox_ServerName.Size = new System.Drawing.Size(55, 20);
            this.textBox_ServerName.TabIndex = 33;
            this.toolTip1.SetToolTip(this.textBox_ServerName, "Host name or IP address of the computer that runs For the Record. If FTR2LO is ru" +
                    "nning on the same computer, set this to \"localhost\". ");
            this.textBox_ServerName.Leave += new System.EventHandler(this.textBox_ServerName_Leave_1);
            // 
            // label_ServerPort
            // 
            this.label_ServerPort.AutoSize = true;
            this.label_ServerPort.BackColor = System.Drawing.Color.Transparent;
            this.label_ServerPort.Location = new System.Drawing.Point(6, 51);
            this.label_ServerPort.Name = "label_ServerPort";
            this.label_ServerPort.Size = new System.Drawing.Size(26, 13);
            this.label_ServerPort.TabIndex = 31;
            this.label_ServerPort.Text = "Port";
            // 
            // label_ServerName
            // 
            this.label_ServerName.AutoSize = true;
            this.label_ServerName.BackColor = System.Drawing.Color.Transparent;
            this.label_ServerName.Location = new System.Drawing.Point(6, 26);
            this.label_ServerName.Name = "label_ServerName";
            this.label_ServerName.Size = new System.Drawing.Size(38, 13);
            this.label_ServerName.TabIndex = 32;
            this.label_ServerName.Text = "Server";
            // 
            // button_Defaults
            // 
            this.button_Defaults.Location = new System.Drawing.Point(367, 342);
            this.button_Defaults.Name = "button_Defaults";
            this.button_Defaults.Size = new System.Drawing.Size(79, 23);
            this.button_Defaults.TabIndex = 38;
            this.button_Defaults.Text = "Defaults";
            this.button_Defaults.UseVisualStyleBackColor = true;
            this.button_Defaults.Click += new System.EventHandler(this.button_Defaults_Click);
            // 
            // button_TasklistxmlFileDialog
            // 
            this.button_TasklistxmlFileDialog.Location = new System.Drawing.Point(196, 21);
            this.button_TasklistxmlFileDialog.Name = "button_TasklistxmlFileDialog";
            this.button_TasklistxmlFileDialog.Size = new System.Drawing.Size(27, 23);
            this.button_TasklistxmlFileDialog.TabIndex = 39;
            this.button_TasklistxmlFileDialog.Text = "...";
            this.button_TasklistxmlFileDialog.UseVisualStyleBackColor = true;
            this.button_TasklistxmlFileDialog.Click += new System.EventHandler(this.button_TasklistxmlFileDialog_Click);
            // 
            // checkBoxPurgeOldItems
            // 
            this.checkBoxPurgeOldItems.AutoSize = true;
            this.checkBoxPurgeOldItems.Location = new System.Drawing.Point(91, 51);
            this.checkBoxPurgeOldItems.Name = "checkBoxPurgeOldItems";
            this.checkBoxPurgeOldItems.Size = new System.Drawing.Size(15, 14);
            this.checkBoxPurgeOldItems.TabIndex = 36;
            this.toolTip1.SetToolTip(this.checkBoxPurgeOldItems, "Check this to remove old calendar entries from LightsOut");
            this.checkBoxPurgeOldItems.UseVisualStyleBackColor = true;
            this.checkBoxPurgeOldItems.CheckedChanged += new System.EventHandler(this.checkBoxPurgeOldItems_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_ServerPort);
            this.groupBox2.Controls.Add(this.label_ServerName);
            this.groupBox2.Controls.Add(this.numericUpDown_ServerPort);
            this.groupBox2.Controls.Add(this.textBox_ServerName);
            this.groupBox2.Location = new System.Drawing.Point(12, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 80);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Argus TV Server";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_TasklistxmlFileDialog);
            this.groupBox3.Controls.Add(this.label_tasklistxmlfilepath);
            this.groupBox3.Controls.Add(this.textBox_tasklistxmlfilepath);
            this.groupBox3.Location = new System.Drawing.Point(12, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(519, 58);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lights Out";
            // 
            // groupBox_line
            // 
            this.groupBox_line.Location = new System.Drawing.Point(0, 98);
            this.groupBox_line.Name = "groupBox_line";
            this.groupBox_line.Size = new System.Drawing.Size(548, 2);
            this.groupBox_line.TabIndex = 45;
            this.groupBox_line.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(22, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxFTR2LO
            // 
            this.pictureBoxFTR2LO.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxFTR2LO.Location = new System.Drawing.Point(191, 29);
            this.pictureBoxFTR2LO.Name = "pictureBoxFTR2LO";
            this.pictureBoxFTR2LO.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxFTR2LO.TabIndex = 19;
            this.pictureBoxFTR2LO.TabStop = false;
            // 
            // pictureBoxFTR
            // 
            this.pictureBoxFTR.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxFTR.Location = new System.Drawing.Point(191, 48);
            this.pictureBoxFTR.Name = "pictureBoxFTR";
            this.pictureBoxFTR.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxFTR.TabIndex = 19;
            this.pictureBoxFTR.TabStop = false;
            // 
            // pictureBoxLO
            // 
            this.pictureBoxLO.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLO.Location = new System.Drawing.Point(191, 67);
            this.pictureBoxLO.Name = "pictureBoxLO";
            this.pictureBoxLO.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxLO.TabIndex = 19;
            this.pictureBoxLO.TabStop = false;
            // 
            // label_actual_LOstatus
            // 
            this.label_actual_LOstatus.AutoSize = true;
            this.label_actual_LOstatus.BackColor = System.Drawing.Color.Transparent;
            this.label_actual_LOstatus.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label_actual_LOstatus.Location = new System.Drawing.Point(312, 70);
            this.label_actual_LOstatus.Name = "label_actual_LOstatus";
            this.label_actual_LOstatus.Size = new System.Drawing.Size(23, 13);
            this.label_actual_LOstatus.TabIndex = 25;
            this.label_actual_LOstatus.Text = "....";
            // 
            // label_LightsOut_service_status
            // 
            this.label_LightsOut_service_status.AutoSize = true;
            this.label_LightsOut_service_status.BackColor = System.Drawing.Color.Transparent;
            this.label_LightsOut_service_status.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label_LightsOut_service_status.Location = new System.Drawing.Point(221, 70);
            this.label_LightsOut_service_status.Name = "label_LightsOut_service_status";
            this.label_LightsOut_service_status.Size = new System.Drawing.Size(57, 13);
            this.label_LightsOut_service_status.TabIndex = 24;
            this.label_LightsOut_service_status.Text = "LightsOut:";
            // 
            // label_actual_server_status
            // 
            this.label_actual_server_status.AutoSize = true;
            this.label_actual_server_status.BackColor = System.Drawing.Color.Transparent;
            this.label_actual_server_status.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label_actual_server_status.Location = new System.Drawing.Point(312, 32);
            this.label_actual_server_status.Name = "label_actual_server_status";
            this.label_actual_server_status.Size = new System.Drawing.Size(23, 13);
            this.label_actual_server_status.TabIndex = 21;
            this.label_actual_server_status.Text = "....";
            // 
            // label_actual_FTRstatus
            // 
            this.label_actual_FTRstatus.AutoSize = true;
            this.label_actual_FTRstatus.BackColor = System.Drawing.Color.Transparent;
            this.label_actual_FTRstatus.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label_actual_FTRstatus.Location = new System.Drawing.Point(312, 51);
            this.label_actual_FTRstatus.Name = "label_actual_FTRstatus";
            this.label_actual_FTRstatus.Size = new System.Drawing.Size(23, 13);
            this.label_actual_FTRstatus.TabIndex = 23;
            this.label_actual_FTRstatus.Text = "....";
            // 
            // label_ForTheRecord_service_status
            // 
            this.label_ForTheRecord_service_status.AutoSize = true;
            this.label_ForTheRecord_service_status.BackColor = System.Drawing.Color.Transparent;
            this.label_ForTheRecord_service_status.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label_ForTheRecord_service_status.Location = new System.Drawing.Point(221, 51);
            this.label_ForTheRecord_service_status.Name = "label_ForTheRecord_service_status";
            this.label_ForTheRecord_service_status.Size = new System.Drawing.Size(50, 13);
            this.label_ForTheRecord_service_status.TabIndex = 22;
            this.label_ForTheRecord_service_status.Text = "Argus TV";
            // 
            // label_FTR2LO_service_status
            // 
            this.label_FTR2LO_service_status.AutoSize = true;
            this.label_FTR2LO_service_status.BackColor = System.Drawing.Color.Transparent;
            this.label_FTR2LO_service_status.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label_FTR2LO_service_status.Location = new System.Drawing.Point(221, 32);
            this.label_FTR2LO_service_status.Name = "label_FTR2LO_service_status";
            this.label_FTR2LO_service_status.Size = new System.Drawing.Size(49, 13);
            this.label_FTR2LO_service_status.TabIndex = 20;
            this.label_FTR2LO_service_status.Text = "ATV2LO:";
            this.label_FTR2LO_service_status.Click += new System.EventHandler(this.label_FTR2LO_service_status_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 2);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(0, 371);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(548, 2);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label_PurgeOldItems);
            this.groupBox6.Controls.Add(this.checkBoxPurgeOldItems);
            this.groupBox6.Controls.Add(this.label_EarlyStart);
            this.groupBox6.Controls.Add(this.numericUpDown_EarlyStart);
            this.groupBox6.Location = new System.Drawing.Point(12, 106);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(519, 80);
            this.groupBox6.TabIndex = 42;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "ATV2LO settings";
            // 
            // label_PurgeOldItems
            // 
            this.label_PurgeOldItems.AutoSize = true;
            this.label_PurgeOldItems.BackColor = System.Drawing.Color.Transparent;
            this.label_PurgeOldItems.Location = new System.Drawing.Point(6, 51);
            this.label_PurgeOldItems.Name = "label_PurgeOldItems";
            this.label_PurgeOldItems.Size = new System.Drawing.Size(79, 13);
            this.label_PurgeOldItems.TabIndex = 37;
            this.label_PurgeOldItems.Text = "Purge old items";
            // 
            // loadingCircle_FTR2LO
            // 
            this.loadingCircle_FTR2LO.Active = false;
            this.loadingCircle_FTR2LO.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle_FTR2LO.InnerCircleRadius = 5;
            this.loadingCircle_FTR2LO.Location = new System.Drawing.Point(191, 29);
            this.loadingCircle_FTR2LO.Name = "loadingCircle_FTR2LO";
            this.loadingCircle_FTR2LO.NumberSpoke = 12;
            this.loadingCircle_FTR2LO.OuterCircleRadius = 11;
            this.loadingCircle_FTR2LO.RotationSpeed = 100;
            this.loadingCircle_FTR2LO.Size = new System.Drawing.Size(16, 16);
            this.loadingCircle_FTR2LO.SpokeThickness = 2;
            this.loadingCircle_FTR2LO.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle_FTR2LO.TabIndex = 38;
            this.loadingCircle_FTR2LO.Text = "loadingCircle1";
            // 
            // loadingCircle_FTR
            // 
            this.loadingCircle_FTR.Active = false;
            this.loadingCircle_FTR.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle_FTR.InnerCircleRadius = 5;
            this.loadingCircle_FTR.Location = new System.Drawing.Point(191, 48);
            this.loadingCircle_FTR.Name = "loadingCircle_FTR";
            this.loadingCircle_FTR.NumberSpoke = 12;
            this.loadingCircle_FTR.OuterCircleRadius = 11;
            this.loadingCircle_FTR.RotationSpeed = 100;
            this.loadingCircle_FTR.Size = new System.Drawing.Size(16, 16);
            this.loadingCircle_FTR.SpokeThickness = 2;
            this.loadingCircle_FTR.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle_FTR.TabIndex = 46;
            this.loadingCircle_FTR.Text = "loadingCircle1";
            // 
            // loadingCircle_LO
            // 
            this.loadingCircle_LO.Active = false;
            this.loadingCircle_LO.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle_LO.InnerCircleRadius = 5;
            this.loadingCircle_LO.Location = new System.Drawing.Point(191, 67);
            this.loadingCircle_LO.Name = "loadingCircle_LO";
            this.loadingCircle_LO.NumberSpoke = 12;
            this.loadingCircle_LO.OuterCircleRadius = 11;
            this.loadingCircle_LO.RotationSpeed = 100;
            this.loadingCircle_LO.Size = new System.Drawing.Size(16, 16);
            this.loadingCircle_LO.SpokeThickness = 2;
            this.loadingCircle_LO.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle_LO.TabIndex = 47;
            this.loadingCircle_LO.Text = "loadingCircle2";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(22, 29);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(48, 48);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 48;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // labelDebugInfo
            // 
            this.labelDebugInfo.AutoSize = true;
            this.labelDebugInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelDebugInfo.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelDebugInfo.Location = new System.Drawing.Point(9, -3);
            this.labelDebugInfo.Name = "labelDebugInfo";
            this.labelDebugInfo.Size = new System.Drawing.Size(0, 13);
            this.labelDebugInfo.TabIndex = 21;
            // 
            // MyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.loadingCircle_LO);
            this.Controls.Add(this.loadingCircle_FTR);
            this.Controls.Add(this.loadingCircle_FTR2LO);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox_line);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.label_FTR2LO_service_status);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.button_Defaults);
            this.Controls.Add(this.label_ForTheRecord_service_status);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_actual_FTRstatus);
            this.Controls.Add(this.pictureBoxLO);
            this.Controls.Add(this.pictureBoxFTR);
            this.Controls.Add(this.labelDebugInfo);
            this.Controls.Add(this.label_actual_server_status);
            this.Controls.Add(this.label_actual_LOstatus);
            this.Controls.Add(this.pictureBoxFTR2LO);
            this.Controls.Add(this.label_LightsOut_service_status);
            this.Name = "MyControl";
            this.Size = new System.Drawing.Size(548, 410);
            this.Load += new System.EventHandler(this.MyControl_Load);
            this.Leave += new System.EventHandler(this.MyControl_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EarlyStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ServerPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFTR2LO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFTR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLO)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label label_tasklistxmlfilepath;
        private System.Windows.Forms.TextBox textBox_tasklistxmlfilepath;
        private System.Windows.Forms.NumericUpDown numericUpDown_EarlyStart;
        private System.Windows.Forms.NumericUpDown numericUpDown_ServerPort;
        private System.Windows.Forms.Label label_EarlyStart;
        private System.Windows.Forms.TextBox textBox_ServerName;
        private System.Windows.Forms.Label label_ServerPort;
        private System.Windows.Forms.Label label_ServerName;
        private System.Windows.Forms.Button button_Defaults;
        private System.Windows.Forms.Button button_TasklistxmlFileDialog;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox_line;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBoxFTR2LO;
        private System.Windows.Forms.PictureBox pictureBoxFTR;
        private System.Windows.Forms.PictureBox pictureBoxLO;
        private System.Windows.Forms.Label label_actual_LOstatus;
        private System.Windows.Forms.Label label_LightsOut_service_status;
        private System.Windows.Forms.Label label_actual_server_status;
        private System.Windows.Forms.Label label_actual_FTRstatus;
        private System.Windows.Forms.Label label_ForTheRecord_service_status;
        private System.Windows.Forms.Label label_FTR2LO_service_status;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBoxPurgeOldItems;
        private System.Windows.Forms.Label label_PurgeOldItems;
        private MRG.Controls.UI.LoadingCircle loadingCircle_FTR2LO;
        private MRG.Controls.UI.LoadingCircle loadingCircle_FTR;
        private MRG.Controls.UI.LoadingCircle loadingCircle_LO;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Label labelDebugInfo;

    }
}
