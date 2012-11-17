namespace __FTR2LOPlayground
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxClearEntries = new System.Windows.Forms.CheckBox();
            this.textBoxToAdd = new System.Windows.Forms.TextBox();
            this.textBoxToDelete = new System.Windows.Forms.TextBox();
            this.buttonCheckforupdates = new System.Windows.Forms.Button();
            this.buttonListInstalledPrograms = new System.Windows.Forms.Button();
            this.Ping = new System.Windows.Forms.Button();
            this.labelPing = new System.Windows.Forms.Label();
            this.labelAPIversion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(819, 117);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(436, 79);
            this.textBox1.TabIndex = 42;
            this.textBox1.WordWrap = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(458, 32);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(436, 79);
            this.textBox2.TabIndex = 42;
            this.textBox2.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "FTR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Tasklist.xml";
            // 
            // checkBoxClearEntries
            // 
            this.checkBoxClearEntries.AutoSize = true;
            this.checkBoxClearEntries.Location = new System.Drawing.Point(662, 118);
            this.checkBoxClearEntries.Name = "checkBoxClearEntries";
            this.checkBoxClearEntries.Size = new System.Drawing.Size(84, 17);
            this.checkBoxClearEntries.TabIndex = 44;
            this.checkBoxClearEntries.Text = "clear Entries";
            this.checkBoxClearEntries.UseVisualStyleBackColor = true;
            // 
            // textBoxToAdd
            // 
            this.textBoxToAdd.Location = new System.Drawing.Point(19, 154);
            this.textBoxToAdd.Multiline = true;
            this.textBoxToAdd.Name = "textBoxToAdd";
            this.textBoxToAdd.ReadOnly = true;
            this.textBoxToAdd.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxToAdd.Size = new System.Drawing.Size(436, 78);
            this.textBoxToAdd.TabIndex = 42;
            this.textBoxToAdd.WordWrap = false;
            // 
            // textBoxToDelete
            // 
            this.textBoxToDelete.Location = new System.Drawing.Point(461, 154);
            this.textBoxToDelete.Multiline = true;
            this.textBoxToDelete.Name = "textBoxToDelete";
            this.textBoxToDelete.ReadOnly = true;
            this.textBoxToDelete.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxToDelete.Size = new System.Drawing.Size(436, 78);
            this.textBoxToDelete.TabIndex = 42;
            this.textBoxToDelete.WordWrap = false;
            // 
            // buttonCheckforupdates
            // 
            this.buttonCheckforupdates.Location = new System.Drawing.Point(822, 265);
            this.buttonCheckforupdates.Name = "buttonCheckforupdates";
            this.buttonCheckforupdates.Size = new System.Drawing.Size(75, 23);
            this.buttonCheckforupdates.TabIndex = 46;
            this.buttonCheckforupdates.Text = "Check for Updates";
            this.buttonCheckforupdates.UseVisualStyleBackColor = true;
            this.buttonCheckforupdates.Click += new System.EventHandler(this.buttonCheckforupdates_Click);
            // 
            // buttonListInstalledPrograms
            // 
            this.buttonListInstalledPrograms.Location = new System.Drawing.Point(822, 293);
            this.buttonListInstalledPrograms.Name = "buttonListInstalledPrograms";
            this.buttonListInstalledPrograms.Size = new System.Drawing.Size(75, 23);
            this.buttonListInstalledPrograms.TabIndex = 47;
            this.buttonListInstalledPrograms.Text = "listInstalledPrograms";
            this.buttonListInstalledPrograms.UseVisualStyleBackColor = true;
            this.buttonListInstalledPrograms.Click += new System.EventHandler(this.buttonListInstalledPrograms_Click);
            // 
            // Ping
            // 
            this.Ping.Location = new System.Drawing.Point(50, 357);
            this.Ping.Name = "Ping";
            this.Ping.Size = new System.Drawing.Size(75, 41);
            this.Ping.TabIndex = 48;
            this.Ping.Text = "Ping";
            this.Ping.UseVisualStyleBackColor = true;
            this.Ping.Click += new System.EventHandler(this.Ping_Click);
            // 
            // labelPing
            // 
            this.labelPing.AutoSize = true;
            this.labelPing.Location = new System.Drawing.Point(132, 384);
            this.labelPing.Name = "labelPing";
            this.labelPing.Size = new System.Drawing.Size(50, 13);
            this.labelPing.TabIndex = 49;
            this.labelPing.Text = "labelPing";
            // 
            // labelAPIversion
            // 
            this.labelAPIversion.AutoSize = true;
            this.labelAPIversion.Location = new System.Drawing.Point(132, 357);
            this.labelAPIversion.Name = "labelAPIversion";
            this.labelAPIversion.Size = new System.Drawing.Size(80, 13);
            this.labelAPIversion.TabIndex = 49;
            this.labelAPIversion.Text = "labelAPIversion";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 444);
            this.Controls.Add(this.labelAPIversion);
            this.Controls.Add(this.labelPing);
            this.Controls.Add(this.Ping);
            this.Controls.Add(this.buttonListInstalledPrograms);
            this.Controls.Add(this.buttonCheckforupdates);
            this.Controls.Add(this.checkBoxClearEntries);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxToDelete);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBoxToAdd);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonRefresh);
            this.Name = "Form1";
            this.Text = "FTR2LOPlayground";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxClearEntries;
        private System.Windows.Forms.TextBox textBoxToAdd;
        private System.Windows.Forms.TextBox textBoxToDelete;
        private System.Windows.Forms.Button buttonCheckforupdates;
        private System.Windows.Forms.Button buttonListInstalledPrograms;
        private System.Windows.Forms.Button Ping;
        private System.Windows.Forms.Label labelPing;
        private System.Windows.Forms.Label labelAPIversion;
    }
}

