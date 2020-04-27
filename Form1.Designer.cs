namespace DetectLaserApp
{
    partial class Form1
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
            this.DrawPictureBox = new System.Windows.Forms.PictureBox();
            this.comboBoxColor = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.BtnDraw = new System.Windows.Forms.Button();
            this.saveImageBTN = new System.Windows.Forms.Button();
            this.DrawGroupBox = new System.Windows.Forms.GroupBox();
            this.colorBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.setEraser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.penThickness = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.openImgBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DrawPictureBox)).BeginInit();
            this.DrawGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.penThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawPictureBox
            // 
            this.DrawPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DrawPictureBox.Location = new System.Drawing.Point(0, 0);
            this.DrawPictureBox.Name = "DrawPictureBox";
            this.DrawPictureBox.Size = new System.Drawing.Size(640, 480);
            this.DrawPictureBox.TabIndex = 0;
            this.DrawPictureBox.TabStop = false;
            // 
            // comboBoxColor
            // 
            this.comboBoxColor.DisplayMember = "Red";
            this.comboBoxColor.FormattingEnabled = true;
            this.comboBoxColor.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue"});
            this.comboBoxColor.Location = new System.Drawing.Point(717, 46);
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.Size = new System.Drawing.Size(121, 21);
            this.comboBoxColor.TabIndex = 1;
            this.comboBoxColor.Text = "Red";
            this.comboBoxColor.SelectedIndexChanged += new System.EventHandler(this.comboBoxColor_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(788, 439);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 37);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Stop";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // BtnDraw
            // 
            this.BtnDraw.BackColor = System.Drawing.Color.Transparent;
            this.BtnDraw.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDraw.ForeColor = System.Drawing.Color.Maroon;
            this.BtnDraw.Location = new System.Drawing.Point(705, 82);
            this.BtnDraw.Name = "BtnDraw";
            this.BtnDraw.Size = new System.Drawing.Size(151, 28);
            this.BtnDraw.TabIndex = 8;
            this.BtnDraw.Text = "Start Drawing";
            this.BtnDraw.UseVisualStyleBackColor = false;
            this.BtnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // saveImageBTN
            // 
            this.saveImageBTN.BackColor = System.Drawing.Color.Azure;
            this.saveImageBTN.Location = new System.Drawing.Point(93, 192);
            this.saveImageBTN.Name = "saveImageBTN";
            this.saveImageBTN.Size = new System.Drawing.Size(106, 23);
            this.saveImageBTN.TabIndex = 9;
            this.saveImageBTN.Text = "Save drawing";
            this.saveImageBTN.UseVisualStyleBackColor = false;
            this.saveImageBTN.Click += new System.EventHandler(this.saveImageBTN_Click);
            // 
            // DrawGroupBox
            // 
            this.DrawGroupBox.Controls.Add(this.label1);
            this.DrawGroupBox.Controls.Add(this.penThickness);
            this.DrawGroupBox.Controls.Add(this.colorBox);
            this.DrawGroupBox.Controls.Add(this.label3);
            this.DrawGroupBox.Controls.Add(this.setEraser);
            this.DrawGroupBox.Controls.Add(this.label2);
            this.DrawGroupBox.Controls.Add(this.saveImageBTN);
            this.DrawGroupBox.Location = new System.Drawing.Point(674, 140);
            this.DrawGroupBox.Name = "DrawGroupBox";
            this.DrawGroupBox.Size = new System.Drawing.Size(205, 225);
            this.DrawGroupBox.TabIndex = 10;
            this.DrawGroupBox.TabStop = false;
            this.DrawGroupBox.Visible = false;
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.Color.Black;
            this.colorBox.Location = new System.Drawing.Point(125, 53);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(57, 37);
            this.colorBox.TabIndex = 17;
            this.colorBox.TabStop = false;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Click to change";
            // 
            // setEraser
            // 
            this.setEraser.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.setEraser.Location = new System.Drawing.Point(54, 60);
            this.setEraser.Name = "setEraser";
            this.setEraser.Size = new System.Drawing.Size(49, 30);
            this.setEraser.TabIndex = 15;
            this.setEraser.Text = "Eraser";
            this.setEraser.UseVisualStyleBackColor = false;
            this.setEraser.Click += new System.EventHandler(this.setEraser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sitka Small", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 23);
            this.label2.TabIndex = 14;
            this.label2.Text = "Drawing color :";
            // 
            // penThickness
            // 
            this.penThickness.Cursor = System.Windows.Forms.Cursors.Hand;
            this.penThickness.Location = new System.Drawing.Point(24, 141);
            this.penThickness.Maximum = 5;
            this.penThickness.Minimum = 1;
            this.penThickness.Name = "penThickness";
            this.penThickness.Size = new System.Drawing.Size(140, 45);
            this.penThickness.TabIndex = 18;
            this.penThickness.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.penThickness.Value = 1;
            this.penThickness.Scroll += new System.EventHandler(this.penThickness_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Small", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Brush thickness:";
            // 
            // openImgBTN
            // 
            this.openImgBTN.Location = new System.Drawing.Point(674, 439);
            this.openImgBTN.Name = "openImgBTN";
            this.openImgBTN.Size = new System.Drawing.Size(103, 37);
            this.openImgBTN.TabIndex = 11;
            this.openImgBTN.Text = "Open an Image";
            this.openImgBTN.UseVisualStyleBackColor = true;
            this.openImgBTN.Click += new System.EventHandler(this.openImgBTN_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(902, 488);
            this.Controls.Add(this.openImgBTN);
            this.Controls.Add(this.DrawGroupBox);
            this.Controls.Add(this.BtnDraw);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.comboBoxColor);
            this.Controls.Add(this.DrawPictureBox);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DrawPictureBox)).EndInit();
            this.DrawGroupBox.ResumeLayout(false);
            this.DrawGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.penThickness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawPictureBox;
        private System.Windows.Forms.ComboBox comboBoxColor;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button BtnDraw;
        private System.Windows.Forms.Button saveImageBTN;
        private System.Windows.Forms.GroupBox DrawGroupBox;
        private System.Windows.Forms.PictureBox colorBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button setEraser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TrackBar penThickness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openImgBTN;
    }
}

