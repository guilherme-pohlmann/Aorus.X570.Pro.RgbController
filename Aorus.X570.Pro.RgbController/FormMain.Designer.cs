namespace Aorus.X570.Pro.RgbController
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.panelCurrentColor = new System.Windows.Forms.Panel();
            this.labelCurrentCoclor = new System.Windows.Forms.Label();
            this.buttonColor = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.DarkRed;
            // 
            // panelCurrentColor
            // 
            this.panelCurrentColor.BackColor = System.Drawing.Color.Red;
            this.panelCurrentColor.Location = new System.Drawing.Point(15, 25);
            this.panelCurrentColor.Name = "panelCurrentColor";
            this.panelCurrentColor.Size = new System.Drawing.Size(264, 25);
            this.panelCurrentColor.TabIndex = 0;
            // 
            // labelCurrentCoclor
            // 
            this.labelCurrentCoclor.AutoSize = true;
            this.labelCurrentCoclor.Location = new System.Drawing.Point(12, 9);
            this.labelCurrentCoclor.Name = "labelCurrentCoclor";
            this.labelCurrentCoclor.Size = new System.Drawing.Size(77, 13);
            this.labelCurrentCoclor.TabIndex = 1;
            this.labelCurrentCoclor.Text = "Current Color";
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(204, 65);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(75, 23);
            this.buttonColor.TabIndex = 2;
            this.buttonColor.Text = "Change";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Click to show";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Aorus X570 Pro Led Controller";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 99);
            this.Controls.Add(this.buttonColor);
            this.Controls.Add(this.labelCurrentCoclor);
            this.Controls.Add(this.panelCurrentColor);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aorus X570 Pro Led Controller";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Panel panelCurrentColor;
        private System.Windows.Forms.Label labelCurrentCoclor;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

