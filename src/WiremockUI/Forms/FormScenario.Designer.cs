namespace WiremockUI
{
    partial class FormScenario
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblScenarioDescription = new System.Windows.Forms.Label();
            this.txtDesc = new WiremockUI.EditorTextBox();
            this.lblScenarioName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 256);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 50);
            this.panel1.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(100, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 44);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 44);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Adicionar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblScenarioDescription);
            this.panel3.Controls.Add(this.txtDesc);
            this.panel3.Controls.Add(this.lblScenarioName);
            this.panel3.Controls.Add(this.txtName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(645, 256);
            this.panel3.TabIndex = 13;
            // 
            // lblScenarioDescription
            // 
            this.lblScenarioDescription.AutoSize = true;
            this.lblScenarioDescription.Location = new System.Drawing.Point(4, 60);
            this.lblScenarioDescription.Name = "lblScenarioDescription";
            this.lblScenarioDescription.Size = new System.Drawing.Size(55, 13);
            this.lblScenarioDescription.TabIndex = 14;
            this.lblScenarioDescription.Text = "Descrição";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.SystemColors.Control;
            this.txtDesc.Location = new System.Drawing.Point(6, 79);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(636, 171);
            this.txtDesc.TabIndex = 13;
            // 
            // lblScenarioName
            // 
            this.lblScenarioName.AutoSize = true;
            this.lblScenarioName.Location = new System.Drawing.Point(3, 9);
            this.lblScenarioName.Name = "lblScenarioName";
            this.lblScenarioName.Size = new System.Drawing.Size(38, 13);
            this.lblScenarioName.TabIndex = 11;
            this.lblScenarioName.Text = "Nome:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(304, 20);
            this.txtName.TabIndex = 10;
            // 
            // FormScenario
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(645, 306);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FormScenario";
            this.Text = "Novo cenário";
            this.Load += new System.EventHandler(this.FormAddScenario_Load);
            this.Resize += new System.EventHandler(this.FormAddScenario_Resize);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblScenarioName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblScenarioDescription;
        private EditorTextBox txtDesc;
    }
}

