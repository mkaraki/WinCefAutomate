
namespace WinCefAutomate
{
    partial class AutomateWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_continue = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.pbar_dataset_current = new System.Windows.Forms.ProgressBar();
            this.lbox_proc = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_continue, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_status, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbar_dataset_current, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbox_proc, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(266, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_continue
            // 
            this.btn_continue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_continue.Location = new System.Drawing.Point(3, 23);
            this.btn_continue.Name = "btn_continue";
            this.btn_continue.Size = new System.Drawing.Size(260, 24);
            this.btn_continue.TabIndex = 0;
            this.btn_continue.Text = "Start";
            this.btn_continue.UseVisualStyleBackColor = true;
            this.btn_continue.Click += new System.EventHandler(this.btn_continue_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(3, 0);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(54, 13);
            this.lbl_status.TabIndex = 1;
            this.lbl_status.Text = "No Status";
            // 
            // pbar_dataset_current
            // 
            this.pbar_dataset_current.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbar_dataset_current.Location = new System.Drawing.Point(3, 433);
            this.pbar_dataset_current.Name = "pbar_dataset_current";
            this.pbar_dataset_current.Size = new System.Drawing.Size(260, 14);
            this.pbar_dataset_current.TabIndex = 2;
            // 
            // lbox_proc
            // 
            this.lbox_proc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbox_proc.Enabled = false;
            this.lbox_proc.FormattingEnabled = true;
            this.lbox_proc.Location = new System.Drawing.Point(3, 133);
            this.lbox_proc.Name = "lbox_proc";
            this.lbox_proc.Size = new System.Drawing.Size(260, 294);
            this.lbox_proc.TabIndex = 3;
            // 
            // AutomateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "AutomateWindow";
            this.Text = "AutomateWindow";
            this.Shown += new System.EventHandler(this.AutomateWindow_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_continue;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar pbar_dataset_current;
        private System.Windows.Forms.ListBox lbox_proc;
    }
}