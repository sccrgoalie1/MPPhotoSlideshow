namespace MPPhotoSlideshowCommon
{
  partial class SetupForm
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
      this.buildCacheButton = new System.Windows.Forms.Button();
      this.addNewTemplateButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.button1 = new System.Windows.Forms.Button();
      this.photosInYourCollectionLabel = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.aspectRatioLabel = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.addPictureButton = new System.Windows.Forms.Button();
      this.labelColorToggleButton = new System.Windows.Forms.Button();
      this.borderToggleButton = new System.Windows.Forms.Button();
      this.exifRotateCheckBox = new System.Windows.Forms.CheckBox();
      this.templateEnabledCheckBox = new System.Windows.Forms.CheckBox();
      this.borderFilePathTextBox = new System.Windows.Forms.TextBox();
      this.borderBottomTextBox = new System.Windows.Forms.TextBox();
      this.borderRightTextBox = new System.Windows.Forms.TextBox();
      this.borderTopTextBox = new System.Windows.Forms.TextBox();
      this.borderLeftTextBox = new System.Windows.Forms.TextBox();
      this.label20 = new System.Windows.Forms.Label();
      this.label19 = new System.Windows.Forms.Label();
      this.label18 = new System.Windows.Forms.Label();
      this.label17 = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.backgroundImageTextBox = new System.Windows.Forms.TextBox();
      this.label16 = new System.Windows.Forms.Label();
      this.cancelCacheLoadButton = new System.Windows.Forms.Button();
      this.saveButton = new System.Windows.Forms.Button();
      this.label14 = new System.Windows.Forms.Label();
      this.labelFontLabel = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.labelFontTextBox = new System.Windows.Forms.TextBox();
      this.labelTextColorTextBox = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.rotateAngleLabel = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.labelXPosTextBox = new System.Windows.Forms.TextBox();
      this.labelYPosTextBox = new System.Windows.Forms.TextBox();
      this.labelHeightTextBox = new System.Windows.Forms.TextBox();
      this.photoHeightTextBox = new System.Windows.Forms.TextBox();
      this.photoWidthTextBox = new System.Windows.Forms.TextBox();
      this.photoYPosTextBox = new System.Windows.Forms.TextBox();
      this.photoXPosTextBox = new System.Windows.Forms.TextBox();
      this.pictureSelectorComboBox = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.timerTextBox = new System.Windows.Forms.TextBox();
      this.photoFoldersListBox = new System.Windows.Forms.ListBox();
      this.addNewFolderTextBox = new System.Windows.Forms.TextBox();
      this.deleteFolderButton = new System.Windows.Forms.Button();
      this.addFolderButton = new System.Windows.Forms.Button();
      this.templateComboBox = new System.Windows.Forms.ComboBox();
      this.cacheProgressBar = new System.Windows.Forms.ProgressBar();
      this.label1 = new System.Windows.Forms.Label();
      this.pictureEnabledTextBox = new System.Windows.Forms.CheckBox();
      this.progressLabel = new System.Windows.Forms.Label();
      this.rotateAngleNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.photoCacheWorker = new System.ComponentModel.BackgroundWorker();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.watcherService = new System.ServiceProcess.ServiceController();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rotateAngleNumericUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // buildCacheButton
      // 
      this.buildCacheButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.buildCacheButton.Location = new System.Drawing.Point(203, 243);
      this.buildCacheButton.Name = "buildCacheButton";
      this.buildCacheButton.Size = new System.Drawing.Size(94, 24);
      this.buildCacheButton.TabIndex = 7;
      this.buildCacheButton.Text = "Build Cache";
      this.buildCacheButton.UseVisualStyleBackColor = true;
      this.buildCacheButton.Click += new System.EventHandler(this.buildCacheButton_Click);
      // 
      // addNewTemplateButton
      // 
      this.addNewTemplateButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.addNewTemplateButton.Location = new System.Drawing.Point(303, 303);
      this.addNewTemplateButton.Name = "addNewTemplateButton";
      this.addNewTemplateButton.Size = new System.Drawing.Size(94, 24);
      this.addNewTemplateButton.TabIndex = 9;
      this.addNewTemplateButton.Text = "New";
      this.addNewTemplateButton.UseVisualStyleBackColor = true;
      this.addNewTemplateButton.Click += new System.EventHandler(this.addNewTemplateButton_Click);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.AutoScroll = true;
      this.tableLayoutPanel1.ColumnCount = 6;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.Controls.Add(this.button1, 0, 21);
      this.tableLayoutPanel1.Controls.Add(this.photosInYourCollectionLabel, 4, 15);
      this.tableLayoutPanel1.Controls.Add(this.label13, 4, 14);
      this.tableLayoutPanel1.Controls.Add(this.aspectRatioLabel, 4, 13);
      this.tableLayoutPanel1.Controls.Add(this.label9, 4, 12);
      this.tableLayoutPanel1.Controls.Add(this.addPictureButton, 4, 11);
      this.tableLayoutPanel1.Controls.Add(this.labelColorToggleButton, 2, 9);
      this.tableLayoutPanel1.Controls.Add(this.borderToggleButton, 3, 9);
      this.tableLayoutPanel1.Controls.Add(this.exifRotateCheckBox, 4, 9);
      this.tableLayoutPanel1.Controls.Add(this.templateEnabledCheckBox, 4, 10);
      this.tableLayoutPanel1.Controls.Add(this.borderFilePathTextBox, 1, 19);
      this.tableLayoutPanel1.Controls.Add(this.borderBottomTextBox, 3, 18);
      this.tableLayoutPanel1.Controls.Add(this.borderRightTextBox, 3, 17);
      this.tableLayoutPanel1.Controls.Add(this.borderTopTextBox, 1, 18);
      this.tableLayoutPanel1.Controls.Add(this.borderLeftTextBox, 1, 17);
      this.tableLayoutPanel1.Controls.Add(this.label20, 0, 19);
      this.tableLayoutPanel1.Controls.Add(this.label19, 2, 18);
      this.tableLayoutPanel1.Controls.Add(this.label18, 2, 17);
      this.tableLayoutPanel1.Controls.Add(this.label17, 0, 18);
      this.tableLayoutPanel1.Controls.Add(this.label15, 0, 17);
      this.tableLayoutPanel1.Controls.Add(this.backgroundImageTextBox, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.label16, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.cancelCacheLoadButton, 3, 8);
      this.tableLayoutPanel1.Controls.Add(this.saveButton, 2, 20);
      this.tableLayoutPanel1.Controls.Add(this.label14, 0, 10);
      this.tableLayoutPanel1.Controls.Add(this.labelFontLabel, 0, 16);
      this.tableLayoutPanel1.Controls.Add(this.label12, 2, 16);
      this.tableLayoutPanel1.Controls.Add(this.labelFontTextBox, 1, 16);
      this.tableLayoutPanel1.Controls.Add(this.labelTextColorTextBox, 3, 16);
      this.tableLayoutPanel1.Controls.Add(this.label11, 2, 15);
      this.tableLayoutPanel1.Controls.Add(this.label10, 0, 15);
      this.tableLayoutPanel1.Controls.Add(this.rotateAngleLabel, 0, 14);
      this.tableLayoutPanel1.Controls.Add(this.label8, 2, 14);
      this.tableLayoutPanel1.Controls.Add(this.label7, 0, 13);
      this.tableLayoutPanel1.Controls.Add(this.label6, 2, 13);
      this.tableLayoutPanel1.Controls.Add(this.label5, 0, 12);
      this.tableLayoutPanel1.Controls.Add(this.label4, 0, 11);
      this.tableLayoutPanel1.Controls.Add(this.label3, 2, 12);
      this.tableLayoutPanel1.Controls.Add(this.labelXPosTextBox, 1, 15);
      this.tableLayoutPanel1.Controls.Add(this.labelYPosTextBox, 3, 15);
      this.tableLayoutPanel1.Controls.Add(this.labelHeightTextBox, 3, 14);
      this.tableLayoutPanel1.Controls.Add(this.photoHeightTextBox, 3, 12);
      this.tableLayoutPanel1.Controls.Add(this.photoWidthTextBox, 1, 12);
      this.tableLayoutPanel1.Controls.Add(this.photoYPosTextBox, 3, 13);
      this.tableLayoutPanel1.Controls.Add(this.photoXPosTextBox, 1, 13);
      this.tableLayoutPanel1.Controls.Add(this.pictureSelectorComboBox, 1, 11);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.timerTextBox, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.buildCacheButton, 2, 8);
      this.tableLayoutPanel1.Controls.Add(this.addNewTemplateButton, 3, 10);
      this.tableLayoutPanel1.Controls.Add(this.photoFoldersListBox, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.addNewFolderTextBox, 1, 3);
      this.tableLayoutPanel1.Controls.Add(this.deleteFolderButton, 3, 4);
      this.tableLayoutPanel1.Controls.Add(this.addFolderButton, 3, 3);
      this.tableLayoutPanel1.Controls.Add(this.templateComboBox, 1, 10);
      this.tableLayoutPanel1.Controls.Add(this.cacheProgressBar, 0, 8);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.pictureEnabledTextBox, 3, 11);
      this.tableLayoutPanel1.Controls.Add(this.progressLabel, 0, 9);
      this.tableLayoutPanel1.Controls.Add(this.rotateAngleNumericUpDown, 1, 14);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 22;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(516, 667);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // button1
      // 
      this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.button1.Location = new System.Drawing.Point(3, 633);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(94, 31);
      this.button1.TabIndex = 54;
      this.button1.Text = "View Template";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Visible = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // photosInYourCollectionLabel
      // 
      this.photosInYourCollectionLabel.AutoSize = true;
      this.photosInYourCollectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.photosInYourCollectionLabel.Location = new System.Drawing.Point(403, 450);
      this.photosInYourCollectionLabel.Name = "photosInYourCollectionLabel";
      this.photosInYourCollectionLabel.Size = new System.Drawing.Size(94, 30);
      this.photosInYourCollectionLabel.TabIndex = 53;
      this.photosInYourCollectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.toolTip1.SetToolTip(this.photosInYourCollectionLabel, "Number of photos in your collection that will fit this template slot");
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label13.Location = new System.Drawing.Point(403, 420);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(94, 30);
      this.label13.TabIndex = 52;
      this.label13.Text = "Photos Count in Your Collection";
      this.label13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      // 
      // aspectRatioLabel
      // 
      this.aspectRatioLabel.AutoSize = true;
      this.aspectRatioLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.aspectRatioLabel.Location = new System.Drawing.Point(403, 390);
      this.aspectRatioLabel.Name = "aspectRatioLabel";
      this.aspectRatioLabel.Size = new System.Drawing.Size(94, 30);
      this.aspectRatioLabel.TabIndex = 51;
      this.aspectRatioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.toolTip1.SetToolTip(this.aspectRatioLabel, "The current pictures aspect ratio");
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Location = new System.Drawing.Point(403, 360);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(94, 30);
      this.label9.TabIndex = 50;
      this.label9.Text = "Calculated Aspect Ratio";
      this.label9.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
      // 
      // addPictureButton
      // 
      this.addPictureButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.addPictureButton.Location = new System.Drawing.Point(403, 333);
      this.addPictureButton.Name = "addPictureButton";
      this.addPictureButton.Size = new System.Drawing.Size(94, 24);
      this.addPictureButton.TabIndex = 49;
      this.addPictureButton.Text = "Add Picture";
      this.toolTip1.SetToolTip(this.addPictureButton, "Add an additional picture from the standard 8");
      this.addPictureButton.UseVisualStyleBackColor = true;
      this.addPictureButton.Click += new System.EventHandler(this.addPictureButton_Click);
      // 
      // labelColorToggleButton
      // 
      this.labelColorToggleButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelColorToggleButton.Location = new System.Drawing.Point(203, 273);
      this.labelColorToggleButton.Name = "labelColorToggleButton";
      this.labelColorToggleButton.Size = new System.Drawing.Size(94, 24);
      this.labelColorToggleButton.TabIndex = 48;
      this.labelColorToggleButton.Text = "Label Clr Tgl";
      this.toolTip1.SetToolTip(this.labelColorToggleButton, "Changes ALL pictures for all templates label color at once");
      this.labelColorToggleButton.UseVisualStyleBackColor = true;
      this.labelColorToggleButton.Click += new System.EventHandler(this.labelColorToggleButton_Click);
      // 
      // borderToggleButton
      // 
      this.borderToggleButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.borderToggleButton.Location = new System.Drawing.Point(303, 273);
      this.borderToggleButton.Name = "borderToggleButton";
      this.borderToggleButton.Size = new System.Drawing.Size(94, 24);
      this.borderToggleButton.TabIndex = 47;
      this.borderToggleButton.Text = "Border Toggle";
      this.toolTip1.SetToolTip(this.borderToggleButton, "Changes ALL pictures for all templates border values at once");
      this.borderToggleButton.UseVisualStyleBackColor = true;
      this.borderToggleButton.Click += new System.EventHandler(this.borderToggleButton_Click);
      // 
      // exifRotateCheckBox
      // 
      this.exifRotateCheckBox.AutoSize = true;
      this.exifRotateCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.exifRotateCheckBox.Location = new System.Drawing.Point(403, 273);
      this.exifRotateCheckBox.Name = "exifRotateCheckBox";
      this.exifRotateCheckBox.Size = new System.Drawing.Size(94, 24);
      this.exifRotateCheckBox.TabIndex = 46;
      this.exifRotateCheckBox.Text = "EXIF Rotate";
      this.toolTip1.SetToolTip(this.exifRotateCheckBox, "Photos will be rotated on the fly during the slideshow based on their EXIF inform" +
        "ation");
      this.exifRotateCheckBox.UseVisualStyleBackColor = true;
      // 
      // templateEnabledCheckBox
      // 
      this.templateEnabledCheckBox.AutoSize = true;
      this.templateEnabledCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.templateEnabledCheckBox.Location = new System.Drawing.Point(403, 303);
      this.templateEnabledCheckBox.Name = "templateEnabledCheckBox";
      this.templateEnabledCheckBox.Size = new System.Drawing.Size(94, 24);
      this.templateEnabledCheckBox.TabIndex = 44;
      this.templateEnabledCheckBox.Text = "Enabled";
      this.toolTip1.SetToolTip(this.templateEnabledCheckBox, "Whether this template is enabled for your slideshow");
      this.templateEnabledCheckBox.UseVisualStyleBackColor = true;
      // 
      // borderFilePathTextBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.borderFilePathTextBox, 2);
      this.borderFilePathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.borderFilePathTextBox.Location = new System.Drawing.Point(103, 573);
      this.borderFilePathTextBox.Name = "borderFilePathTextBox";
      this.borderFilePathTextBox.Size = new System.Drawing.Size(194, 20);
      this.borderFilePathTextBox.TabIndex = 26;
      this.toolTip1.SetToolTip(this.borderFilePathTextBox, "The image that will act as the border for your photos");
      this.borderFilePathTextBox.DoubleClick += new System.EventHandler(this.borderFilePathTextBox_DoubleClick);
      // 
      // borderBottomTextBox
      // 
      this.borderBottomTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.borderBottomTextBox.Location = new System.Drawing.Point(303, 543);
      this.borderBottomTextBox.Name = "borderBottomTextBox";
      this.borderBottomTextBox.Size = new System.Drawing.Size(94, 20);
      this.borderBottomTextBox.TabIndex = 25;
      // 
      // borderRightTextBox
      // 
      this.borderRightTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.borderRightTextBox.Location = new System.Drawing.Point(303, 513);
      this.borderRightTextBox.Name = "borderRightTextBox";
      this.borderRightTextBox.Size = new System.Drawing.Size(94, 20);
      this.borderRightTextBox.TabIndex = 23;
      // 
      // borderTopTextBox
      // 
      this.borderTopTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.borderTopTextBox.Location = new System.Drawing.Point(103, 543);
      this.borderTopTextBox.Name = "borderTopTextBox";
      this.borderTopTextBox.Size = new System.Drawing.Size(94, 20);
      this.borderTopTextBox.TabIndex = 24;
      // 
      // borderLeftTextBox
      // 
      this.borderLeftTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.borderLeftTextBox.Location = new System.Drawing.Point(103, 513);
      this.borderLeftTextBox.Name = "borderLeftTextBox";
      this.borderLeftTextBox.Size = new System.Drawing.Size(94, 20);
      this.borderLeftTextBox.TabIndex = 22;
      // 
      // label20
      // 
      this.label20.AutoSize = true;
      this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label20.Location = new System.Drawing.Point(3, 570);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(94, 30);
      this.label20.TabIndex = 43;
      this.label20.Text = "Border File Path";
      this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label19
      // 
      this.label19.AutoSize = true;
      this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label19.Location = new System.Drawing.Point(203, 540);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(94, 30);
      this.label19.TabIndex = 42;
      this.label19.Text = "Border Bottom";
      this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label18
      // 
      this.label18.AutoSize = true;
      this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label18.Location = new System.Drawing.Point(203, 510);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(94, 30);
      this.label18.TabIndex = 41;
      this.label18.Text = "Border Right";
      this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label17.Location = new System.Drawing.Point(3, 540);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(94, 30);
      this.label17.TabIndex = 40;
      this.label17.Text = "Border Top";
      this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label15.Location = new System.Drawing.Point(3, 510);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(94, 30);
      this.label15.TabIndex = 39;
      this.label15.Text = "Border Left";
      this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // backgroundImageTextBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.backgroundImageTextBox, 2);
      this.backgroundImageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.backgroundImageTextBox.Location = new System.Drawing.Point(103, 33);
      this.backgroundImageTextBox.Name = "backgroundImageTextBox";
      this.backgroundImageTextBox.Size = new System.Drawing.Size(194, 20);
      this.backgroundImageTextBox.TabIndex = 38;
      this.toolTip1.SetToolTip(this.backgroundImageTextBox, "The image that will appear in the background when playing a slideshow");
      this.backgroundImageTextBox.DoubleClick += new System.EventHandler(this.backgroundImageTextBox_DoubleClick);
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label16.Location = new System.Drawing.Point(3, 30);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(94, 30);
      this.label16.TabIndex = 37;
      this.label16.Text = "Background Image";
      this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cancelCacheLoadButton
      // 
      this.cancelCacheLoadButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cancelCacheLoadButton.Enabled = false;
      this.cancelCacheLoadButton.Location = new System.Drawing.Point(303, 243);
      this.cancelCacheLoadButton.Name = "cancelCacheLoadButton";
      this.cancelCacheLoadButton.Size = new System.Drawing.Size(94, 24);
      this.cancelCacheLoadButton.TabIndex = 34;
      this.cancelCacheLoadButton.Text = "Cancel";
      this.cancelCacheLoadButton.UseVisualStyleBackColor = true;
      this.cancelCacheLoadButton.Click += new System.EventHandler(this.cancelCacheLoadButton_Click);
      // 
      // saveButton
      // 
      this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.saveButton.Location = new System.Drawing.Point(203, 603);
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(94, 24);
      this.saveButton.TabIndex = 27;
      this.saveButton.Text = "Save";
      this.saveButton.UseVisualStyleBackColor = true;
      this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label14.Location = new System.Drawing.Point(3, 300);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(94, 30);
      this.label14.TabIndex = 33;
      this.label14.Text = "Template Selector";
      this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelFontLabel
      // 
      this.labelFontLabel.AutoSize = true;
      this.labelFontLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelFontLabel.Location = new System.Drawing.Point(3, 480);
      this.labelFontLabel.Name = "labelFontLabel";
      this.labelFontLabel.Size = new System.Drawing.Size(94, 30);
      this.labelFontLabel.TabIndex = 32;
      this.labelFontLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label12.Location = new System.Drawing.Point(203, 480);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(94, 30);
      this.label12.TabIndex = 31;
      this.label12.Text = "Label Text Color (HEX)";
      this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelFontTextBox
      // 
      this.labelFontTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelFontTextBox.Location = new System.Drawing.Point(103, 483);
      this.labelFontTextBox.Name = "labelFontTextBox";
      this.labelFontTextBox.Size = new System.Drawing.Size(94, 20);
      this.labelFontTextBox.TabIndex = 20;
      // 
      // labelTextColorTextBox
      // 
      this.labelTextColorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelTextColorTextBox.Location = new System.Drawing.Point(303, 483);
      this.labelTextColorTextBox.Name = "labelTextColorTextBox";
      this.labelTextColorTextBox.Size = new System.Drawing.Size(94, 20);
      this.labelTextColorTextBox.TabIndex = 21;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label11.Location = new System.Drawing.Point(203, 450);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(94, 30);
      this.label11.TabIndex = 28;
      this.label11.Text = "Label YPos";
      this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.label11.Visible = false;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label10.Location = new System.Drawing.Point(3, 450);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(94, 30);
      this.label10.TabIndex = 27;
      this.label10.Text = "Label XPos";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.label10.Visible = false;
      // 
      // rotateAngleLabel
      // 
      this.rotateAngleLabel.AutoSize = true;
      this.rotateAngleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rotateAngleLabel.Location = new System.Drawing.Point(3, 420);
      this.rotateAngleLabel.Name = "rotateAngleLabel";
      this.rotateAngleLabel.Size = new System.Drawing.Size(94, 30);
      this.rotateAngleLabel.TabIndex = 26;
      this.rotateAngleLabel.Text = "Rotate Angle";
      this.rotateAngleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label8.Location = new System.Drawing.Point(203, 420);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(94, 30);
      this.label8.TabIndex = 25;
      this.label8.Text = "Label Height";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.label8.Visible = false;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label7.Location = new System.Drawing.Point(3, 390);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(94, 30);
      this.label7.TabIndex = 24;
      this.label7.Text = "Photo XPos";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(203, 390);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(94, 30);
      this.label6.TabIndex = 23;
      this.label6.Text = "PhotoYPos";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label5.Location = new System.Drawing.Point(3, 360);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(94, 30);
      this.label5.TabIndex = 22;
      this.label5.Text = "Photo Width";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Location = new System.Drawing.Point(3, 330);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(94, 30);
      this.label4.TabIndex = 21;
      this.label4.Text = "Picture Selector";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Location = new System.Drawing.Point(203, 360);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(94, 30);
      this.label3.TabIndex = 20;
      this.label3.Text = "Photo Height";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // labelXPosTextBox
      // 
      this.labelXPosTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelXPosTextBox.Enabled = false;
      this.labelXPosTextBox.Location = new System.Drawing.Point(103, 453);
      this.labelXPosTextBox.Name = "labelXPosTextBox";
      this.labelXPosTextBox.Size = new System.Drawing.Size(94, 20);
      this.labelXPosTextBox.TabIndex = 18;
      this.labelXPosTextBox.Visible = false;
      // 
      // labelYPosTextBox
      // 
      this.labelYPosTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelYPosTextBox.Enabled = false;
      this.labelYPosTextBox.Location = new System.Drawing.Point(303, 453);
      this.labelYPosTextBox.Name = "labelYPosTextBox";
      this.labelYPosTextBox.Size = new System.Drawing.Size(94, 20);
      this.labelYPosTextBox.TabIndex = 19;
      this.labelYPosTextBox.Visible = false;
      // 
      // labelHeightTextBox
      // 
      this.labelHeightTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.labelHeightTextBox.Enabled = false;
      this.labelHeightTextBox.Location = new System.Drawing.Point(303, 423);
      this.labelHeightTextBox.Name = "labelHeightTextBox";
      this.labelHeightTextBox.Size = new System.Drawing.Size(94, 20);
      this.labelHeightTextBox.TabIndex = 17;
      this.labelHeightTextBox.Visible = false;
      this.labelHeightTextBox.TextChanged += new System.EventHandler(this.labelHeightTextBox_TextChanged);
      // 
      // photoHeightTextBox
      // 
      this.photoHeightTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.photoHeightTextBox.Location = new System.Drawing.Point(303, 363);
      this.photoHeightTextBox.Name = "photoHeightTextBox";
      this.photoHeightTextBox.Size = new System.Drawing.Size(94, 20);
      this.photoHeightTextBox.TabIndex = 13;
      this.photoHeightTextBox.TextChanged += new System.EventHandler(this.photoHeightTextBox_TextChanged);
      // 
      // photoWidthTextBox
      // 
      this.photoWidthTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.photoWidthTextBox.Location = new System.Drawing.Point(103, 363);
      this.photoWidthTextBox.Name = "photoWidthTextBox";
      this.photoWidthTextBox.Size = new System.Drawing.Size(94, 20);
      this.photoWidthTextBox.TabIndex = 12;
      this.photoWidthTextBox.TextChanged += new System.EventHandler(this.photoWidthTextBox_TextChanged);
      // 
      // photoYPosTextBox
      // 
      this.photoYPosTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.photoYPosTextBox.Location = new System.Drawing.Point(303, 393);
      this.photoYPosTextBox.Name = "photoYPosTextBox";
      this.photoYPosTextBox.Size = new System.Drawing.Size(94, 20);
      this.photoYPosTextBox.TabIndex = 15;
      this.photoYPosTextBox.TextChanged += new System.EventHandler(this.photoYPosTextBox_TextChanged);
      // 
      // photoXPosTextBox
      // 
      this.photoXPosTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.photoXPosTextBox.Location = new System.Drawing.Point(103, 393);
      this.photoXPosTextBox.Name = "photoXPosTextBox";
      this.photoXPosTextBox.Size = new System.Drawing.Size(94, 20);
      this.photoXPosTextBox.TabIndex = 14;
      this.photoXPosTextBox.TextChanged += new System.EventHandler(this.photoXPosTextBox_TextChanged);
      // 
      // pictureSelectorComboBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.pictureSelectorComboBox, 2);
      this.pictureSelectorComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureSelectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.pictureSelectorComboBox.FormattingEnabled = true;
      this.pictureSelectorComboBox.Location = new System.Drawing.Point(103, 333);
      this.pictureSelectorComboBox.Name = "pictureSelectorComboBox";
      this.pictureSelectorComboBox.Size = new System.Drawing.Size(194, 21);
      this.pictureSelectorComboBox.TabIndex = 10;
      this.pictureSelectorComboBox.SelectionChangeCommitted += new System.EventHandler(this.pictureSelectorComboBox_SelectionChangeCommitted);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(3, 90);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(94, 30);
      this.label2.TabIndex = 10;
      this.label2.Text = "Folder Path";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // timerTextBox
      // 
      this.timerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.timerTextBox.Location = new System.Drawing.Point(103, 63);
      this.timerTextBox.Name = "timerTextBox";
      this.timerTextBox.Size = new System.Drawing.Size(94, 20);
      this.timerTextBox.TabIndex = 1;
      this.toolTip1.SetToolTip(this.timerTextBox, "The time between slide templates");
      // 
      // photoFoldersListBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.photoFoldersListBox, 3);
      this.photoFoldersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.photoFoldersListBox.FormattingEnabled = true;
      this.photoFoldersListBox.Location = new System.Drawing.Point(3, 123);
      this.photoFoldersListBox.Name = "photoFoldersListBox";
      this.tableLayoutPanel1.SetRowSpan(this.photoFoldersListBox, 4);
      this.photoFoldersListBox.Size = new System.Drawing.Size(294, 114);
      this.photoFoldersListBox.TabIndex = 4;
      // 
      // addNewFolderTextBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.addNewFolderTextBox, 2);
      this.addNewFolderTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.addNewFolderTextBox.Location = new System.Drawing.Point(103, 93);
      this.addNewFolderTextBox.Name = "addNewFolderTextBox";
      this.addNewFolderTextBox.Size = new System.Drawing.Size(194, 20);
      this.addNewFolderTextBox.TabIndex = 2;
      this.toolTip1.SetToolTip(this.addNewFolderTextBox, "Double click to select a path");
      this.addNewFolderTextBox.DoubleClick += new System.EventHandler(this.addNewFolderTextBox_DoubleClick);
      // 
      // deleteFolderButton
      // 
      this.deleteFolderButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.deleteFolderButton.Location = new System.Drawing.Point(303, 123);
      this.deleteFolderButton.Name = "deleteFolderButton";
      this.deleteFolderButton.Size = new System.Drawing.Size(94, 24);
      this.deleteFolderButton.TabIndex = 5;
      this.deleteFolderButton.Text = "Delete";
      this.deleteFolderButton.UseVisualStyleBackColor = true;
      this.deleteFolderButton.Click += new System.EventHandler(this.deleteFolderButton_Click);
      // 
      // addFolderButton
      // 
      this.addFolderButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.addFolderButton.Location = new System.Drawing.Point(303, 93);
      this.addFolderButton.Name = "addFolderButton";
      this.addFolderButton.Size = new System.Drawing.Size(94, 24);
      this.addFolderButton.TabIndex = 3;
      this.addFolderButton.Text = "Add";
      this.toolTip1.SetToolTip(this.addFolderButton, "Add the path to the list of photo folders");
      this.addFolderButton.UseVisualStyleBackColor = true;
      this.addFolderButton.Click += new System.EventHandler(this.addFolderButton_Click);
      // 
      // templateComboBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.templateComboBox, 2);
      this.templateComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.templateComboBox.FormattingEnabled = true;
      this.templateComboBox.Location = new System.Drawing.Point(103, 303);
      this.templateComboBox.Name = "templateComboBox";
      this.templateComboBox.Size = new System.Drawing.Size(194, 21);
      this.templateComboBox.TabIndex = 8;
      this.templateComboBox.SelectionChangeCommitted += new System.EventHandler(this.templateComboBox_SelectionChangeCommitted);
      // 
      // cacheProgressBar
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.cacheProgressBar, 2);
      this.cacheProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cacheProgressBar.Location = new System.Drawing.Point(3, 243);
      this.cacheProgressBar.Name = "cacheProgressBar";
      this.cacheProgressBar.Size = new System.Drawing.Size(194, 24);
      this.cacheProgressBar.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 60);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(94, 30);
      this.label1.TabIndex = 9;
      this.label1.Text = "Slide Interval (milli-seconds)";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // pictureEnabledTextBox
      // 
      this.pictureEnabledTextBox.AutoSize = true;
      this.pictureEnabledTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureEnabledTextBox.Location = new System.Drawing.Point(303, 333);
      this.pictureEnabledTextBox.Name = "pictureEnabledTextBox";
      this.pictureEnabledTextBox.Size = new System.Drawing.Size(94, 24);
      this.pictureEnabledTextBox.TabIndex = 11;
      this.pictureEnabledTextBox.Text = "Pic Enabled";
      this.toolTip1.SetToolTip(this.pictureEnabledTextBox, "Whether this picture will attempt to be filled during your slideshow");
      this.pictureEnabledTextBox.UseVisualStyleBackColor = true;
      // 
      // progressLabel
      // 
      this.progressLabel.AutoSize = true;
      this.tableLayoutPanel1.SetColumnSpan(this.progressLabel, 2);
      this.progressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.progressLabel.Location = new System.Drawing.Point(3, 270);
      this.progressLabel.Name = "progressLabel";
      this.progressLabel.Size = new System.Drawing.Size(194, 30);
      this.progressLabel.TabIndex = 35;
      this.progressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // rotateAngleNumericUpDown
      // 
      this.rotateAngleNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rotateAngleNumericUpDown.Location = new System.Drawing.Point(103, 423);
      this.rotateAngleNumericUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
      this.rotateAngleNumericUpDown.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
      this.rotateAngleNumericUpDown.Name = "rotateAngleNumericUpDown";
      this.rotateAngleNumericUpDown.Size = new System.Drawing.Size(94, 20);
      this.rotateAngleNumericUpDown.TabIndex = 45;
      // 
      // photoCacheWorker
      // 
      this.photoCacheWorker.WorkerReportsProgress = true;
      this.photoCacheWorker.WorkerSupportsCancellation = true;
      this.photoCacheWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.photoCacheWorker_DoWork);
      this.photoCacheWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.photoCacheWorker_ProgressChanged);
      this.photoCacheWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.photoCacheWorker_RunWorkerCompleted);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // watcherService
      // 
      this.watcherService.ServiceName = "MPPhotoSlideshowWatcher";
      // 
      // SetupForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(516, 667);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "SetupForm";
      this.Text = "MPSlideshow Configuration";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetupForm_FormClosing);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.rotateAngleNumericUpDown)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buildCacheButton;
    private System.Windows.Forms.Button addNewTemplateButton;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.ListBox photoFoldersListBox;
    private System.Windows.Forms.TextBox addNewFolderTextBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox timerTextBox;
    private System.Windows.Forms.Button deleteFolderButton;
    private System.Windows.Forms.Button addFolderButton;
    private System.Windows.Forms.ComboBox templateComboBox;
    private System.Windows.Forms.ProgressBar cacheProgressBar;
    private System.Windows.Forms.Label label1;
    private System.ComponentModel.BackgroundWorker photoCacheWorker;
    private System.Windows.Forms.ComboBox pictureSelectorComboBox;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox labelXPosTextBox;
    private System.Windows.Forms.TextBox labelYPosTextBox;
    private System.Windows.Forms.TextBox labelHeightTextBox;
    private System.Windows.Forms.TextBox photoHeightTextBox;
    private System.Windows.Forms.TextBox photoWidthTextBox;
    private System.Windows.Forms.TextBox photoYPosTextBox;
    private System.Windows.Forms.TextBox photoXPosTextBox;
    private System.Windows.Forms.Label labelFontLabel;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox labelFontTextBox;
    private System.Windows.Forms.TextBox labelTextColorTextBox;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label rotateAngleLabel;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.CheckBox pictureEnabledTextBox;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button cancelCacheLoadButton;
    private System.Windows.Forms.Label progressLabel;
    private System.Windows.Forms.TextBox backgroundImageTextBox;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.TextBox borderFilePathTextBox;
    private System.Windows.Forms.TextBox borderBottomTextBox;
    private System.Windows.Forms.TextBox borderRightTextBox;
    private System.Windows.Forms.TextBox borderTopTextBox;
    private System.Windows.Forms.TextBox borderLeftTextBox;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Label label15;
    private System.ServiceProcess.ServiceController watcherService;
    private System.Windows.Forms.CheckBox templateEnabledCheckBox;
    private System.Windows.Forms.NumericUpDown rotateAngleNumericUpDown;
    private System.Windows.Forms.CheckBox exifRotateCheckBox;
    private System.Windows.Forms.Button borderToggleButton;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button labelColorToggleButton;
    private System.Windows.Forms.Button addPictureButton;
    private System.Windows.Forms.Label photosInYourCollectionLabel;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label aspectRatioLabel;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Button button1;
  }
}

