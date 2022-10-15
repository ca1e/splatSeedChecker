namespace splatSeedChecker;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.gearlistBox = new System.Windows.Forms.ListBox();
            this.gearItemCtrl1 = new splatoon3Tester.GearItemCtrl();
            this.gearTypeControl = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.buttonSeedWrite = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.labelIp = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gearTypeControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // gearlistBox
            // 
            this.gearlistBox.FormattingEnabled = true;
            this.gearlistBox.ItemHeight = 17;
            this.gearlistBox.Location = new System.Drawing.Point(12, 80);
            this.gearlistBox.Name = "gearlistBox";
            this.gearlistBox.Size = new System.Drawing.Size(223, 259);
            this.gearlistBox.TabIndex = 51;
            this.gearlistBox.SelectedIndexChanged += new System.EventHandler(this.gearlistBox_SelectedIndexChanged);
            // 
            // gearItemCtrl1
            // 
            this.gearItemCtrl1.Location = new System.Drawing.Point(245, 86);
            this.gearItemCtrl1.Name = "gearItemCtrl1";
            this.gearItemCtrl1.Size = new System.Drawing.Size(247, 194);
            this.gearItemCtrl1.TabIndex = 50;
            // 
            // gearTypeControl
            // 
            this.gearTypeControl.Controls.Add(this.tabPage7);
            this.gearTypeControl.Controls.Add(this.tabPage8);
            this.gearTypeControl.Controls.Add(this.tabPage9);
            this.gearTypeControl.Location = new System.Drawing.Point(12, 53);
            this.gearTypeControl.Name = "gearTypeControl";
            this.gearTypeControl.SelectedIndex = 0;
            this.gearTypeControl.Size = new System.Drawing.Size(223, 27);
            this.gearTypeControl.TabIndex = 49;
            this.gearTypeControl.SelectedIndexChanged += new System.EventHandler(this.gearTypeControl_SelectedIndexChanged);
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 26);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(215, 0);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "头部装备";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 26);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(215, 0);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "服装";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(4, 26);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(215, 0);
            this.tabPage9.TabIndex = 2;
            this.tabPage9.Text = "鞋子";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // buttonSeedWrite
            // 
            this.buttonSeedWrite.Location = new System.Drawing.Point(326, 302);
            this.buttonSeedWrite.Name = "buttonSeedWrite";
            this.buttonSeedWrite.Size = new System.Drawing.Size(106, 41);
            this.buttonSeedWrite.TabIndex = 48;
            this.buttonSeedWrite.Text = "写入种子";
            this.buttonSeedWrite.UseVisualStyleBackColor = true;
            this.buttonSeedWrite.Click += new System.EventHandler(this.buttonSeedWrite_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Enabled = false;
            this.buttonRefresh.Location = new System.Drawing.Point(353, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 56;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(272, 12);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 55;
            this.buttonDisconnect.Text = "断开";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // labelIp
            // 
            this.labelIp.AutoSize = true;
            this.labelIp.Location = new System.Drawing.Point(13, 15);
            this.labelIp.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(43, 17);
            this.labelIp.TabIndex = 54;
            this.labelIp.Text = "IP地址";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(85, 12);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(100, 23);
            this.ipTextBox.TabIndex = 53;
            this.ipTextBox.Text = "192.168.1.115";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(191, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 52;
            this.buttonConnect.Text = "连接";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "总数: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 357);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.labelIp);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.gearlistBox);
            this.Controls.Add(this.gearItemCtrl1);
            this.Controls.Add(this.gearTypeControl);
            this.Controls.Add(this.buttonSeedWrite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Splatoon Seed Checker";
            this.gearTypeControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ListBox gearlistBox;
    private splatoon3Tester.GearItemCtrl gearItemCtrl1;
    private TabControl gearTypeControl;
    private TabPage tabPage7;
    private TabPage tabPage8;
    private TabPage tabPage9;
    private Button buttonSeedWrite;
    private Button buttonRefresh;
    private Button buttonDisconnect;
    private Label labelIp;
    private TextBox ipTextBox;
    private Button buttonConnect;
    private Label label2;
}
