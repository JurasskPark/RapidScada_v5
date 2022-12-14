namespace Scada.Comm.Devices.DbImportPlus.UI
{
    partial class FrmConfig
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
            this.cbDataSourceType = new System.Windows.Forms.ComboBox();
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.txtOptionalOptions = new System.Windows.Forms.TextBox();
            this.lblOptionalOptions = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pageDatabase = new System.Windows.Forms.TabPage();
            this.gbDataSourceType = new System.Windows.Forms.GroupBox();
            this.btnConnectionTest = new System.Windows.Forms.Button();
            this.pageQuery = new System.Windows.Forms.TabPage();
            this.lblSelectQuery = new System.Windows.Forms.Label();
            this.txtSelectQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            this.pageData = new System.Windows.Forms.TabPage();
            this.tlpPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExecuteSQLQuery = new System.Windows.Forms.Button();
            this.pageCommands = new System.Windows.Forms.TabPage();
            this.gbCommandParams = new System.Windows.Forms.GroupBox();
            this.txtCmdQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            this.lblCmdQuery = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.numCmdNum = new System.Windows.Forms.NumericUpDown();
            this.lblCmdNum = new System.Windows.Forms.Label();
            this.gbCommand = new System.Windows.Forms.GroupBox();
            this.cbCommand = new System.Windows.Forms.ComboBox();
            this.btnDeleteCommand = new System.Windows.Forms.Button();
            this.btnCreateCommand = new System.Windows.Forms.Button();
            this.pageSettings = new System.Windows.Forms.TabPage();
            this.gpbTagsCount = new System.Windows.Forms.GroupBox();
            this.numTagCount = new System.Windows.Forms.NumericUpDown();
            this.chkAutoTagCount = new System.Windows.Forms.CheckBox();
            this.gpbTagFormatDatabase = new System.Windows.Forms.GroupBox();
            this.rdbKPTagsBasedRequestedTableRows = new System.Windows.Forms.RadioButton();
            this.rdbKPTagsBasedRequestedTableColumns = new System.Windows.Forms.RadioButton();
            this.pageHelp = new System.Windows.Forms.TabPage();
            this.txtHelp = new FastColoredTextBoxNS.FastColoredTextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbConnection.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.pageDatabase.SuspendLayout();
            this.gbDataSourceType.SuspendLayout();
            this.pageQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectQuery)).BeginInit();
            this.pageData.SuspendLayout();
            this.tlpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pageCommands.SuspendLayout();
            this.gbCommandParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCmdQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdNum)).BeginInit();
            this.gbCommand.SuspendLayout();
            this.pageSettings.SuspendLayout();
            this.gpbTagsCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTagCount)).BeginInit();
            this.gpbTagFormatDatabase.SuspendLayout();
            this.pageHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelp)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbDataSourceType
            // 
            this.cbDataSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSourceType.FormattingEnabled = true;
            this.cbDataSourceType.Items.AddRange(new object[] {
            "<Choose database type>",
            "Microsoft SQL Server",
            "Oracle",
            "PostgreSQL",
            "MySQL",
            "OLE DB",
            "ODBC",
            "Firebird"});
            this.cbDataSourceType.Location = new System.Drawing.Point(13, 19);
            this.cbDataSourceType.Name = "cbDataSourceType";
            this.cbDataSourceType.Size = new System.Drawing.Size(234, 21);
            this.cbDataSourceType.TabIndex = 0;
            this.cbDataSourceType.SelectedIndexChanged += new System.EventHandler(this.cbDataSourceType_SelectedIndexChanged);
            // 
            // gbConnection
            // 
            this.gbConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConnection.Controls.Add(this.txtOptionalOptions);
            this.gbConnection.Controls.Add(this.lblOptionalOptions);
            this.gbConnection.Controls.Add(this.txtPort);
            this.gbConnection.Controls.Add(this.lblPort);
            this.gbConnection.Controls.Add(this.txtConnectionString);
            this.gbConnection.Controls.Add(this.lblConnectionString);
            this.gbConnection.Controls.Add(this.txtPassword);
            this.gbConnection.Controls.Add(this.lblPassword);
            this.gbConnection.Controls.Add(this.txtUser);
            this.gbConnection.Controls.Add(this.lblUser);
            this.gbConnection.Controls.Add(this.txtDatabase);
            this.gbConnection.Controls.Add(this.lblDatabase);
            this.gbConnection.Controls.Add(this.txtServer);
            this.gbConnection.Controls.Add(this.lblServer);
            this.gbConnection.Location = new System.Drawing.Point(6, 65);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbConnection.Size = new System.Drawing.Size(414, 283);
            this.gbConnection.TabIndex = 1;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Connection";
            // 
            // txtOptionalOptions
            // 
            this.txtOptionalOptions.Location = new System.Drawing.Point(13, 149);
            this.txtOptionalOptions.Name = "txtOptionalOptions";
            this.txtOptionalOptions.Size = new System.Drawing.Size(388, 20);
            this.txtOptionalOptions.TabIndex = 13;
            this.txtOptionalOptions.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblOptionalOptions
            // 
            this.lblOptionalOptions.AutoSize = true;
            this.lblOptionalOptions.Location = new System.Drawing.Point(10, 133);
            this.lblOptionalOptions.Name = "lblOptionalOptions";
            this.lblOptionalOptions.Size = new System.Drawing.Size(85, 13);
            this.lblOptionalOptions.TabIndex = 12;
            this.lblOptionalOptions.Text = "Optional Options";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(210, 32);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(191, 20);
            this.txtPort.TabIndex = 11;
            this.txtPort.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(207, 16);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 10;
            this.lblPort.Text = "Port";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(13, 188);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(388, 82);
            this.txtConnectionString.TabIndex = 9;
            this.txtConnectionString.TextChanged += new System.EventHandler(this.txtConnectionString_TextChanged);
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(10, 172);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(89, 13);
            this.lblConnectionString.TabIndex = 8;
            this.lblConnectionString.Text = "Connection string";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(210, 110);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(191, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(207, 94);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(13, 110);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(191, 20);
            this.txtUser.TabIndex = 5;
            this.txtUser.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(10, 94);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(29, 13);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "User";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(13, 71);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(388, 20);
            this.txtDatabase.TabIndex = 3;
            this.txtDatabase.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(10, 55);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Database";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(13, 32);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(191, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblServer
            // 
            this.lblServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(10, 16);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(38, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.pageDatabase);
            this.tabControl.Controls.Add(this.pageQuery);
            this.tabControl.Controls.Add(this.pageData);
            this.tabControl.Controls.Add(this.pageCommands);
            this.tabControl.Controls.Add(this.pageSettings);
            this.tabControl.Controls.Add(this.pageHelp);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(434, 380);
            this.tabControl.TabIndex = 0;
            // 
            // pageDatabase
            // 
            this.pageDatabase.Controls.Add(this.gbDataSourceType);
            this.pageDatabase.Controls.Add(this.gbConnection);
            this.pageDatabase.Location = new System.Drawing.Point(4, 22);
            this.pageDatabase.Name = "pageDatabase";
            this.pageDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.pageDatabase.Size = new System.Drawing.Size(426, 354);
            this.pageDatabase.TabIndex = 0;
            this.pageDatabase.Text = "Database";
            this.pageDatabase.UseVisualStyleBackColor = true;
            // 
            // gbDataSourceType
            // 
            this.gbDataSourceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDataSourceType.Controls.Add(this.btnConnectionTest);
            this.gbDataSourceType.Controls.Add(this.cbDataSourceType);
            this.gbDataSourceType.Location = new System.Drawing.Point(6, 6);
            this.gbDataSourceType.Name = "gbDataSourceType";
            this.gbDataSourceType.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbDataSourceType.Size = new System.Drawing.Size(414, 53);
            this.gbDataSourceType.TabIndex = 0;
            this.gbDataSourceType.TabStop = false;
            this.gbDataSourceType.Text = "Data Source Type";
            // 
            // btnConnectionTest
            // 
            this.btnConnectionTest.Location = new System.Drawing.Point(255, 18);
            this.btnConnectionTest.Name = "btnConnectionTest";
            this.btnConnectionTest.Size = new System.Drawing.Size(146, 23);
            this.btnConnectionTest.TabIndex = 3;
            this.btnConnectionTest.Text = "Testing connection...";
            this.btnConnectionTest.UseVisualStyleBackColor = true;
            this.btnConnectionTest.Click += new System.EventHandler(this.btnConnectionTest_Click);
            // 
            // pageQuery
            // 
            this.pageQuery.Controls.Add(this.lblSelectQuery);
            this.pageQuery.Controls.Add(this.txtSelectQuery);
            this.pageQuery.Location = new System.Drawing.Point(4, 22);
            this.pageQuery.Name = "pageQuery";
            this.pageQuery.Padding = new System.Windows.Forms.Padding(3);
            this.pageQuery.Size = new System.Drawing.Size(426, 354);
            this.pageQuery.TabIndex = 1;
            this.pageQuery.Text = "Data Retrieval";
            this.pageQuery.UseVisualStyleBackColor = true;
            // 
            // lblSelectQuery
            // 
            this.lblSelectQuery.AutoSize = true;
            this.lblSelectQuery.Location = new System.Drawing.Point(3, 4);
            this.lblSelectQuery.Name = "lblSelectQuery";
            this.lblSelectQuery.Size = new System.Drawing.Size(28, 13);
            this.lblSelectQuery.TabIndex = 0;
            this.lblSelectQuery.Text = "SQL";
            // 
            // txtSelectQuery
            // 
            this.txtSelectQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectQuery.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtSelectQuery.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.txtSelectQuery.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.txtSelectQuery.BackBrush = null;
            this.txtSelectQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSelectQuery.CharHeight = 14;
            this.txtSelectQuery.CharWidth = 8;
            this.txtSelectQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSelectQuery.DefaultMarkerSize = 8;
            this.txtSelectQuery.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtSelectQuery.IsReplaceMode = false;
            this.txtSelectQuery.Location = new System.Drawing.Point(8, 20);
            this.txtSelectQuery.Name = "txtSelectQuery";
            this.txtSelectQuery.Paddings = new System.Windows.Forms.Padding(0);
            this.txtSelectQuery.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtSelectQuery.ServiceColors = null;
            this.txtSelectQuery.Size = new System.Drawing.Size(410, 328);
            this.txtSelectQuery.TabIndex = 5;
            this.txtSelectQuery.WordWrap = true;
            this.txtSelectQuery.Zoom = 100;
            this.txtSelectQuery.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtSelectQuery_TextChanged);
            // 
            // pageData
            // 
            this.pageData.Controls.Add(this.tlpPanel);
            this.pageData.Location = new System.Drawing.Point(4, 22);
            this.pageData.Name = "pageData";
            this.pageData.Size = new System.Drawing.Size(426, 354);
            this.pageData.TabIndex = 4;
            this.pageData.Text = "Data";
            this.pageData.UseVisualStyleBackColor = true;
            // 
            // tlpPanel
            // 
            this.tlpPanel.ColumnCount = 1;
            this.tlpPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPanel.Controls.Add(this.dgvData, 0, 0);
            this.tlpPanel.Controls.Add(this.btnExecuteSQLQuery, 0, 1);
            this.tlpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpPanel.Name = "tlpPanel";
            this.tlpPanel.RowCount = 2;
            this.tlpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpPanel.Size = new System.Drawing.Size(426, 354);
            this.tlpPanel.TabIndex = 10;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(420, 318);
            this.dgvData.TabIndex = 8;
            // 
            // btnExecuteSQLQuery
            // 
            this.btnExecuteSQLQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteSQLQuery.Location = new System.Drawing.Point(3, 327);
            this.btnExecuteSQLQuery.Name = "btnExecuteSQLQuery";
            this.btnExecuteSQLQuery.Size = new System.Drawing.Size(420, 23);
            this.btnExecuteSQLQuery.TabIndex = 9;
            this.btnExecuteSQLQuery.Text = "Execute SQL query";
            this.btnExecuteSQLQuery.UseVisualStyleBackColor = true;
            this.btnExecuteSQLQuery.Click += new System.EventHandler(this.btnExecuteSQLQuery_Click);
            // 
            // pageCommands
            // 
            this.pageCommands.Controls.Add(this.gbCommandParams);
            this.pageCommands.Controls.Add(this.gbCommand);
            this.pageCommands.Location = new System.Drawing.Point(4, 22);
            this.pageCommands.Name = "pageCommands";
            this.pageCommands.Padding = new System.Windows.Forms.Padding(3);
            this.pageCommands.Size = new System.Drawing.Size(426, 354);
            this.pageCommands.TabIndex = 2;
            this.pageCommands.Text = "Commands";
            this.pageCommands.UseVisualStyleBackColor = true;
            // 
            // gbCommandParams
            // 
            this.gbCommandParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCommandParams.Controls.Add(this.txtCmdQuery);
            this.gbCommandParams.Controls.Add(this.lblCmdQuery);
            this.gbCommandParams.Controls.Add(this.txtName);
            this.gbCommandParams.Controls.Add(this.lblName);
            this.gbCommandParams.Controls.Add(this.numCmdNum);
            this.gbCommandParams.Controls.Add(this.lblCmdNum);
            this.gbCommandParams.Location = new System.Drawing.Point(6, 67);
            this.gbCommandParams.Name = "gbCommandParams";
            this.gbCommandParams.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbCommandParams.Size = new System.Drawing.Size(414, 281);
            this.gbCommandParams.TabIndex = 1;
            this.gbCommandParams.TabStop = false;
            this.gbCommandParams.Text = "Command Parameters";
            // 
            // txtCmdQuery
            // 
            this.txtCmdQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmdQuery.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtCmdQuery.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.txtCmdQuery.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.txtCmdQuery.BackBrush = null;
            this.txtCmdQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCmdQuery.CharHeight = 14;
            this.txtCmdQuery.CharWidth = 8;
            this.txtCmdQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCmdQuery.DefaultMarkerSize = 8;
            this.txtCmdQuery.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCmdQuery.IsReplaceMode = false;
            this.txtCmdQuery.Location = new System.Drawing.Point(13, 71);
            this.txtCmdQuery.Name = "txtCmdQuery";
            this.txtCmdQuery.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCmdQuery.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCmdQuery.ServiceColors = null;
            this.txtCmdQuery.Size = new System.Drawing.Size(388, 197);
            this.txtCmdQuery.TabIndex = 6;
            this.txtCmdQuery.WordWrap = true;
            this.txtCmdQuery.Zoom = 100;
            this.txtCmdQuery.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtCmdQuery_TextChanged);
            // 
            // lblCmdQuery
            // 
            this.lblCmdQuery.AutoSize = true;
            this.lblCmdQuery.Location = new System.Drawing.Point(10, 55);
            this.lblCmdQuery.Name = "lblCmdQuery";
            this.lblCmdQuery.Size = new System.Drawing.Size(28, 13);
            this.lblCmdQuery.TabIndex = 4;
            this.lblCmdQuery.Text = "SQL";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(119, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(282, 20);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(116, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // numCmdNum
            // 
            this.numCmdNum.Location = new System.Drawing.Point(13, 32);
            this.numCmdNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numCmdNum.Name = "numCmdNum";
            this.numCmdNum.Size = new System.Drawing.Size(100, 20);
            this.numCmdNum.TabIndex = 1;
            this.numCmdNum.ValueChanged += new System.EventHandler(this.numCmdNum_ValueChanged);
            // 
            // lblCmdNum
            // 
            this.lblCmdNum.AutoSize = true;
            this.lblCmdNum.Location = new System.Drawing.Point(10, 16);
            this.lblCmdNum.Name = "lblCmdNum";
            this.lblCmdNum.Size = new System.Drawing.Size(92, 13);
            this.lblCmdNum.TabIndex = 0;
            this.lblCmdNum.Text = "Command number";
            // 
            // gbCommand
            // 
            this.gbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCommand.Controls.Add(this.cbCommand);
            this.gbCommand.Controls.Add(this.btnDeleteCommand);
            this.gbCommand.Controls.Add(this.btnCreateCommand);
            this.gbCommand.Location = new System.Drawing.Point(6, 6);
            this.gbCommand.Name = "gbCommand";
            this.gbCommand.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gbCommand.Size = new System.Drawing.Size(414, 55);
            this.gbCommand.TabIndex = 0;
            this.gbCommand.TabStop = false;
            this.gbCommand.Text = "Command";
            // 
            // cbCommand
            // 
            this.cbCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommand.FormattingEnabled = true;
            this.cbCommand.Location = new System.Drawing.Point(13, 20);
            this.cbCommand.Name = "cbCommand";
            this.cbCommand.Size = new System.Drawing.Size(226, 21);
            this.cbCommand.TabIndex = 0;
            this.cbCommand.SelectedIndexChanged += new System.EventHandler(this.cbCommand_SelectedIndexChanged);
            // 
            // btnDeleteCommand
            // 
            this.btnDeleteCommand.Location = new System.Drawing.Point(326, 19);
            this.btnDeleteCommand.Name = "btnDeleteCommand";
            this.btnDeleteCommand.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteCommand.TabIndex = 2;
            this.btnDeleteCommand.Text = "Delete";
            this.btnDeleteCommand.UseVisualStyleBackColor = true;
            this.btnDeleteCommand.Click += new System.EventHandler(this.btnDeleteCommand_Click);
            // 
            // btnCreateCommand
            // 
            this.btnCreateCommand.Location = new System.Drawing.Point(245, 19);
            this.btnCreateCommand.Name = "btnCreateCommand";
            this.btnCreateCommand.Size = new System.Drawing.Size(75, 23);
            this.btnCreateCommand.TabIndex = 1;
            this.btnCreateCommand.Text = "Create";
            this.btnCreateCommand.UseVisualStyleBackColor = true;
            this.btnCreateCommand.Click += new System.EventHandler(this.btnCreateCommand_Click);
            // 
            // pageSettings
            // 
            this.pageSettings.Controls.Add(this.gpbTagsCount);
            this.pageSettings.Controls.Add(this.gpbTagFormatDatabase);
            this.pageSettings.Location = new System.Drawing.Point(4, 22);
            this.pageSettings.Name = "pageSettings";
            this.pageSettings.Size = new System.Drawing.Size(426, 354);
            this.pageSettings.TabIndex = 3;
            this.pageSettings.Text = "Settings";
            this.pageSettings.UseVisualStyleBackColor = true;
            // 
            // gpbTagsCount
            // 
            this.gpbTagsCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbTagsCount.Controls.Add(this.numTagCount);
            this.gpbTagsCount.Controls.Add(this.chkAutoTagCount);
            this.gpbTagsCount.Location = new System.Drawing.Point(6, 79);
            this.gpbTagsCount.Name = "gpbTagsCount";
            this.gpbTagsCount.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gpbTagsCount.Size = new System.Drawing.Size(412, 50);
            this.gpbTagsCount.TabIndex = 11;
            this.gpbTagsCount.TabStop = false;
            this.gpbTagsCount.Text = "Tags Count";
            // 
            // numTagCount
            // 
            this.numTagCount.Location = new System.Drawing.Point(12, 19);
            this.numTagCount.Name = "numTagCount";
            this.numTagCount.Size = new System.Drawing.Size(100, 20);
            this.numTagCount.TabIndex = 8;
            this.numTagCount.ValueChanged += new System.EventHandler(this.numTagCount_ValueChanged);
            // 
            // chkAutoTagCount
            // 
            this.chkAutoTagCount.AutoSize = true;
            this.chkAutoTagCount.Location = new System.Drawing.Point(118, 20);
            this.chkAutoTagCount.Name = "chkAutoTagCount";
            this.chkAutoTagCount.Size = new System.Drawing.Size(48, 17);
            this.chkAutoTagCount.TabIndex = 9;
            this.chkAutoTagCount.Text = "Auto";
            this.chkAutoTagCount.UseVisualStyleBackColor = true;
            this.chkAutoTagCount.CheckedChanged += new System.EventHandler(this.chkAutoTagCount_CheckedChanged);
            // 
            // gpbTagFormatDatabase
            // 
            this.gpbTagFormatDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbTagFormatDatabase.Controls.Add(this.rdbKPTagsBasedRequestedTableRows);
            this.gpbTagFormatDatabase.Controls.Add(this.rdbKPTagsBasedRequestedTableColumns);
            this.gpbTagFormatDatabase.Location = new System.Drawing.Point(6, 6);
            this.gpbTagFormatDatabase.Name = "gpbTagFormatDatabase";
            this.gpbTagFormatDatabase.Padding = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.gpbTagFormatDatabase.Size = new System.Drawing.Size(412, 67);
            this.gpbTagFormatDatabase.TabIndex = 7;
            this.gpbTagFormatDatabase.TabStop = false;
            this.gpbTagFormatDatabase.Text = "Format of Tags from the Database";
            // 
            // rdbKPTagsBasedRequestedTableRows
            // 
            this.rdbKPTagsBasedRequestedTableRows.AutoSize = true;
            this.rdbKPTagsBasedRequestedTableRows.Location = new System.Drawing.Point(13, 42);
            this.rdbKPTagsBasedRequestedTableRows.Name = "rdbKPTagsBasedRequestedTableRows";
            this.rdbKPTagsBasedRequestedTableRows.Size = new System.Drawing.Size(278, 17);
            this.rdbKPTagsBasedRequestedTableRows.TabIndex = 1;
            this.rdbKPTagsBasedRequestedTableRows.TabStop = true;
            this.rdbKPTagsBasedRequestedTableRows.Text = "KP Tags Based on the List of Requested Table Rows";
            this.rdbKPTagsBasedRequestedTableRows.UseVisualStyleBackColor = true;
            this.rdbKPTagsBasedRequestedTableRows.CheckedChanged += new System.EventHandler(this.rdbKPTagsBasedRequestedTableRows_CheckedChanged);
            // 
            // rdbKPTagsBasedRequestedTableColumns
            // 
            this.rdbKPTagsBasedRequestedTableColumns.AutoSize = true;
            this.rdbKPTagsBasedRequestedTableColumns.Location = new System.Drawing.Point(13, 19);
            this.rdbKPTagsBasedRequestedTableColumns.Name = "rdbKPTagsBasedRequestedTableColumns";
            this.rdbKPTagsBasedRequestedTableColumns.Size = new System.Drawing.Size(291, 17);
            this.rdbKPTagsBasedRequestedTableColumns.TabIndex = 0;
            this.rdbKPTagsBasedRequestedTableColumns.TabStop = true;
            this.rdbKPTagsBasedRequestedTableColumns.Text = "KP Tags Based on the List of Requested Table Columns";
            this.rdbKPTagsBasedRequestedTableColumns.UseVisualStyleBackColor = true;
            this.rdbKPTagsBasedRequestedTableColumns.CheckedChanged += new System.EventHandler(this.rdbKPTagsBasedRequestedTableColumns_CheckedChanged);
            // 
            // pageHelp
            // 
            this.pageHelp.Controls.Add(this.txtHelp);
            this.pageHelp.Location = new System.Drawing.Point(4, 22);
            this.pageHelp.Name = "pageHelp";
            this.pageHelp.Size = new System.Drawing.Size(426, 354);
            this.pageHelp.TabIndex = 5;
            this.pageHelp.Text = "Help";
            this.pageHelp.UseVisualStyleBackColor = true;
            // 
            // txtHelp
            // 
            this.txtHelp.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtHelp.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.txtHelp.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.txtHelp.BackBrush = null;
            this.txtHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHelp.CharHeight = 14;
            this.txtHelp.CharWidth = 8;
            this.txtHelp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHelp.DefaultMarkerSize = 8;
            this.txtHelp.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelp.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtHelp.IsReplaceMode = false;
            this.txtHelp.Location = new System.Drawing.Point(0, 0);
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.Paddings = new System.Windows.Forms.Padding(0);
            this.txtHelp.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtHelp.ServiceColors = null;
            this.txtHelp.Size = new System.Drawing.Size(426, 354);
            this.txtHelp.TabIndex = 7;
            this.txtHelp.WordWrap = true;
            this.txtHelp.Zoom = 100;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 380);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(434, 41);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(347, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(266, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(434, 421);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlBottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 460);
            this.Name = "FrmConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DB Import Plus - Device {0} Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.pageDatabase.ResumeLayout(false);
            this.gbDataSourceType.ResumeLayout(false);
            this.pageQuery.ResumeLayout(false);
            this.pageQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectQuery)).EndInit();
            this.pageData.ResumeLayout(false);
            this.tlpPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pageCommands.ResumeLayout(false);
            this.gbCommandParams.ResumeLayout(false);
            this.gbCommandParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCmdQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdNum)).EndInit();
            this.gbCommand.ResumeLayout(false);
            this.pageSettings.ResumeLayout(false);
            this.gpbTagsCount.ResumeLayout(false);
            this.gpbTagsCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTagCount)).EndInit();
            this.gpbTagFormatDatabase.ResumeLayout(false);
            this.gpbTagFormatDatabase.PerformLayout();
            this.pageHelp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtHelp)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbDataSourceType;
        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage pageDatabase;
        private System.Windows.Forms.TabPage pageQuery;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbDataSourceType;
        private System.Windows.Forms.Label lblSelectQuery;
        private System.Windows.Forms.TabPage pageCommands;
        private System.Windows.Forms.ComboBox cbCommand;
        private System.Windows.Forms.GroupBox gbCommand;
        private System.Windows.Forms.Button btnDeleteCommand;
        private System.Windows.Forms.Button btnCreateCommand;
        private System.Windows.Forms.GroupBox gbCommandParams;
        private System.Windows.Forms.Label lblCmdQuery;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.NumericUpDown numCmdNum;
        private System.Windows.Forms.Label lblCmdNum;
        private FastColoredTextBoxNS.FastColoredTextBox txtSelectQuery;
        private System.Windows.Forms.TextBox txtOptionalOptions;
        private System.Windows.Forms.Label lblOptionalOptions;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnConnectionTest;
        private FastColoredTextBoxNS.FastColoredTextBox txtCmdQuery;
        private System.Windows.Forms.TabPage pageSettings;
        private System.Windows.Forms.CheckBox chkAutoTagCount;
        private System.Windows.Forms.NumericUpDown numTagCount;
        private System.Windows.Forms.GroupBox gpbTagFormatDatabase;
        private System.Windows.Forms.RadioButton rdbKPTagsBasedRequestedTableRows;
        private System.Windows.Forms.RadioButton rdbKPTagsBasedRequestedTableColumns;
        private System.Windows.Forms.TabPage pageData;
        private System.Windows.Forms.TableLayoutPanel tlpPanel;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExecuteSQLQuery;
        private System.Windows.Forms.GroupBox gpbTagsCount;
        private System.Windows.Forms.TabPage pageHelp;
        private FastColoredTextBoxNS.FastColoredTextBox txtHelp;
    }
}