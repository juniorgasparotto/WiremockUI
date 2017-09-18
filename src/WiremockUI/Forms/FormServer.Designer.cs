namespace WiremockUI
{
    partial class FormServer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabServerBasic = new System.Windows.Forms.TabPage();
            this.lblServerNew = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblServerTargetUrl = new System.Windows.Forms.Label();
            this.txtUrlTarget = new System.Windows.Forms.TextBox();
            this.tabServerAdvance = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabServerBasic.SuspendLayout();
            this.tabServerAdvance.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 263);
            this.panel1.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabServerBasic);
            this.tabControl1.Controls.Add(this.tabServerAdvance);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(6, 10);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 253);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 15;
            // 
            // tabServerBasic
            // 
            this.tabServerBasic.Controls.Add(this.lblServerNew);
            this.tabServerBasic.Controls.Add(this.txtName);
            this.tabServerBasic.Controls.Add(this.label3);
            this.tabServerBasic.Controls.Add(this.lblServerPort);
            this.tabServerBasic.Controls.Add(this.txtPort);
            this.tabServerBasic.Controls.Add(this.lblServerTargetUrl);
            this.tabServerBasic.Controls.Add(this.txtUrlTarget);
            this.tabServerBasic.Location = new System.Drawing.Point(4, 36);
            this.tabServerBasic.Name = "tabServerBasic";
            this.tabServerBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tabServerBasic.Size = new System.Drawing.Size(547, 213);
            this.tabServerBasic.TabIndex = 0;
            this.tabServerBasic.Text = "Básico";
            this.tabServerBasic.UseVisualStyleBackColor = true;
            // 
            // lblServerNew
            // 
            this.lblServerNew.AutoSize = true;
            this.lblServerNew.Location = new System.Drawing.Point(6, 15);
            this.lblServerNew.Name = "lblServerNew";
            this.lblServerNew.Size = new System.Drawing.Size(81, 13);
            this.lblServerNew.TabIndex = 23;
            this.lblServerNew.Text = "Nome do servidor:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(9, 36);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(530, 20);
            this.txtName.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(6, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "http://localhost:";
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Location = new System.Drawing.Point(5, 124);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(78, 13);
            this.lblServerPort.TabIndex = 21;
            this.lblServerPort.Text = "Porta do servidor:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(89, 146);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(450, 20);
            this.txtPort.TabIndex = 19;
            this.txtPort.Text = "5500";
            // 
            // lblServerTargetUrl
            // 
            this.lblServerTargetUrl.AutoSize = true;
            this.lblServerTargetUrl.Location = new System.Drawing.Point(6, 69);
            this.lblServerTargetUrl.Name = "lblServerTargetUrl";
            this.lblServerTargetUrl.Size = new System.Drawing.Size(75, 13);
            this.lblServerTargetUrl.TabIndex = 20;
            this.lblServerTargetUrl.Text = "Url de destino:";
            // 
            // txtUrlTarget
            // 
            this.txtUrlTarget.Location = new System.Drawing.Point(9, 91);
            this.txtUrlTarget.Name = "txtUrlTarget";
            this.txtUrlTarget.Size = new System.Drawing.Size(530, 20);
            this.txtUrlTarget.TabIndex = 18;
            // 
            // tabServerAdvance
            // 
            this.tabServerAdvance.Controls.Add(this.propertyGrid1);
            this.tabServerAdvance.Location = new System.Drawing.Point(4, 36);
            this.tabServerAdvance.Name = "tabServerAdvance";
            this.tabServerAdvance.Padding = new System.Windows.Forms.Padding(3);
            this.tabServerAdvance.Size = new System.Drawing.Size(547, 213);
            this.tabServerAdvance.TabIndex = 1;
            this.tabServerAdvance.Text = "Avançado";
            this.tabServerAdvance.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ButtonFace;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.Size = new System.Drawing.Size(541, 207);
            this.propertyGrid1.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(555, 10);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 263);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(555, 64);
            this.panel2.TabIndex = 15;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(101, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 44);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(8, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 44);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Adicionar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FormServer
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(555, 327);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FormServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novo Server";
            this.Load += new System.EventHandler(this.FormServer_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabServerBasic.ResumeLayout(false);
            this.tabServerBasic.PerformLayout();
            this.tabServerAdvance.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabServerBasic;
        private System.Windows.Forms.Label lblServerNew;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblServerTargetUrl;
        private System.Windows.Forms.TextBox txtUrlTarget;
        private System.Windows.Forms.TabPage tabServerAdvance;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Panel panel3;
    }
}

