namespace WiremockUI
{
    partial class UcJsonView
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
            this.lblError = new System.Windows.Forms.Label();
            this.treeJson = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.White;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(0, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(276, 13);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "Ocorreu um erro ao tentar carregar o JSON: {0}";
            this.lblError.Visible = false;
            // 
            // treeJson
            // 
            this.treeJson.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeJson.Location = new System.Drawing.Point(0, 0);
            this.treeJson.Name = "treeJson";
            this.treeJson.Size = new System.Drawing.Size(150, 150);
            this.treeJson.TabIndex = 5;
            // 
            // UcJsonView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.treeJson);
            this.Name = "UcJsonView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TreeView treeJson;
    }
}
