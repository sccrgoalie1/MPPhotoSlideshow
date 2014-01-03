using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MPPhotoSlideshowCommon;
using System.ServiceProcess;

namespace MPPhotoSlideshowCommon
{
  public partial class SetupForm : Form
  {
    private string _pictureFolders = "";
    private bool isMP2 = false;
    private XMLSettings settings;
    private List<Picture> _allPictures = new List<Picture>();
    private ModuleVersion currentTemplateVersion = new ModuleVersion(1,0,0,0);
    private int previousSelectedTemplateIndex = -1;
    private int previousSelectedPictureIndex = -1;
    private BindingList<PhotoTemplate> photoTemplates = new BindingList<PhotoTemplate>();
    private BindingList<PhotoControl> photoControls = new BindingList<PhotoControl>();
    private InputBox inputBox = new InputBox();
    public SetupForm()
    {
      InitializeComponent();
      Log.Init(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "SetupLog", "log", LogType.Debug);
      LoadSettings();
      //update and save templates
      if (BuiltInTemplates.NewTemplatesAvailable(currentTemplateVersion))
      {
        photoTemplates = BuiltInTemplates.UpdateTemplates(photoTemplates);
        currentTemplateVersion = BuiltInTemplates.BuiltInTemplateVersion;
        Save();
      }
      if (photoTemplates.Count > 0)
      {        
        photoControls = photoTemplates[0].Photos;       
      }
      templateComboBox.DataSource = photoTemplates;
      templateComboBox.DisplayMember = "TemplateName";
      templateComboBox.ValueMember = "TemplateName";
      pictureSelectorComboBox.DataSource = photoControls;
      pictureSelectorComboBox.DisplayMember = "PhotoName";
      pictureSelectorComboBox.ValueMember = "PhotoName";
      if (photoTemplates.Count > 0)
      {
        previousSelectedTemplateIndex = 0;
        templateComboBox.SelectedIndex = 0;
        templateEnabledCheckBox.Checked = photoTemplates[0].Enabled;
      }
      if (photoControls.Count > 0)
      {
        previousSelectedPictureIndex = 0;
        pictureSelectorComboBox.SelectedIndex = 0;
      }
      ToggleFieldsBasedOnMpSlideShowVersion();
      SetupGUI();
    }

    public void ToggleFieldsBasedOnMpSlideShowVersion()
    {
      string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      if (!File.Exists(dir + @"\plugin.xml"))
      {
        exifRotateCheckBox.Visible = false;
        rotateAngleNumericUpDown.Visible = false;
        rotateAngleLabel.Visible = false;
        addPictureButton.Visible = false;
        labelFontLabel.Text = "Label Font (fonts.xml)";
        isMP2 = false;
      }
      else
      {
        isMP2 = true;
        labelFontLabel.Text = "Label Font Size";
      }
    }

    public void LoadSettings()
    {
      try
      {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\";
        if (File.Exists(folder + @"\SlideshowTemplates.xml"))
        {
          using (StreamReader streamReader = new StreamReader(folder + @"\SlideshowTemplates.xml"))
          {
            string stream = streamReader.ReadToEnd();
            if (stream.Length > 0)
            {
              Templates template = XMLHelper.Deserialize<Templates>(stream);
              photoTemplates = template.TemplatesList;
              currentTemplateVersion = template.TemplateVersion;
            }
            streamReader.Close();
          }          
        }
        //reload from old cache location 
        if (photoTemplates.Count == 0)
        {
          if (File.Exists(folder + @"\MPSlideshowTemplates.xml"))
          {
            using (StreamReader streamReader = new StreamReader(folder + @"\MPSlideshowTemplates.xml"))
            {
              string stream = streamReader.ReadToEnd();
              if (stream.Length > 0)
              {
                photoTemplates = XMLHelper.Deserialize<BindingList<PhotoTemplate>>(stream);
              }
              streamReader.Close();
            }
            //write to the new file
            Templates templates = new Templates()
            {
              TemplatesList = photoTemplates,
              TemplateVersion = new ModuleVersion(1, 0, 0, 0)
            };
            using (StreamWriter streamWriter = new StreamWriter(folder + @"\SlideshowTemplates.xml"))
            {
              string serialized = XMLHelper.Serialize<Templates>(templates);
              streamWriter.Write(serialized);
              streamWriter.Close();
            }
            //cleanup the old
            File.Delete(folder + @"\MPSlideshowTemplates.xml");
          }
        }
        if (File.Exists(folder + @"\MPSlideshowCache.xml"))
        {
          using (StreamReader streamReader = new StreamReader(folder + @"\MPSlideshowCache.xml"))
          {
            string stream = streamReader.ReadToEnd();
            if (stream.Length > 0)
            {
              _allPictures = XMLHelper.Deserialize<List<Picture>>(stream);
            }
            streamReader.Close();
          }

        }
        int interval = 10000;
        DateTime lastPictureLoad = new DateTime(1901, 1, 1);
        settings = new XMLSettings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MPPhotoSlideshow2.xml");
        _pictureFolders = settings.getXmlAttribute("FolderPaths", "");
        Int32.TryParse(settings.getXmlAttribute("Interval", "10000"), out interval);
        backgroundImageTextBox.Text = settings.getXmlAttribute("BackgroundPath", "mpslideshow_background.png");
        exifRotateCheckBox.Checked = Convert.ToBoolean(settings.getXmlAttribute("EXIFRotate","false"));
        
        //using (Settings xmlreader = new Settings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPPhotoSlideshow.xml"))
        //{
        //  _pictureFolders = xmlreader.GetValue("MyConfig", "FolderPaths");
        //  Int32.TryParse(xmlreader.GetValue("MyConfig", "Interval"), out interval);
        //  if (xmlreader.GetValue("MyConfig", "LastLoadCache") != "")
        //  {
        //    lastPictureLoad = Convert.ToDateTime(xmlreader.GetValue("MyConfig", "LastLoadCache"));
        //  }
        //  backgroundImageTextBox.Text=xmlreader.GetValue("MyConfig", "BackgroundPath");
        //}
        //if (backgroundImageTextBox.Text == "")
        //{
        //  backgroundImageTextBox.Text = "mpslideshow_background.png";
        //}
        timerTextBox.Text = interval.ToString();
        foreach (string pictureFolder in _pictureFolders.Split(','))
        {
          if (pictureFolder != "")
          {
            photoFoldersListBox.Items.Add(pictureFolder);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.ToString());
      }
    }

    private void templateComboBox_SelectionChangeCommitted(object sender, EventArgs e)
    {
      try
      {
        bool saveMe = true;
        if (pictureEnabledTextBox.Checked)
        {
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelHeightTextBox.Text == "" | labelXPosTextBox.Text == "" | labelYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "" | borderBottomTextBox.Text == "" | borderFilePathTextBox.Text == "" | borderLeftTextBox.Text == "" | borderRightTextBox.Text == "" | borderTopTextBox.Text == "" | backgroundImageTextBox.Text == "")
          {
            templateComboBox.SelectedIndex = previousSelectedTemplateIndex;
            saveMe=false;
            MessageBox.Show("No picture field can be left blank");
          }
        }
        if (saveMe)
        {
          StoreCurrentTemplate();
          previousSelectedTemplateIndex = templateComboBox.SelectedIndex;
          ResetGUI();
          if (templateComboBox.SelectedIndex > -1)
          {
            photoControls = photoTemplates[templateComboBox.SelectedIndex].Photos;
            pictureSelectorComboBox.DataSource = photoControls;
            //templateComboBox.SelectedIndex = 0;
            previousSelectedPictureIndex = 0;
            pictureSelectorComboBox.SelectedIndex = 0;
            templateEnabledCheckBox.Checked = photoTemplates[templateComboBox.SelectedIndex].Enabled;
            SetupGUI();
          }
        }
       
      }
      catch (Exception ex)
      {
        Log.Debug("MPPhotoslideshow.SetupForm() - Error {0}", ex.ToString());
      }
    }
    
    private void Save()
    {
      try
      {
        StoreCurrentPhoto();
        StoreCurrentTemplate();
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow";
        File.Delete(folder + @"\SlideshowTemplates.xml");
        Templates template = new Templates() {TemplatesList = photoTemplates, TemplateVersion = currentTemplateVersion};
        using (StreamWriter streamWriter = new StreamWriter(folder + @"\SlideshowTemplates.xml"))
        {
          string serialized = XMLHelper.Serialize<Templates>(template);
          streamWriter.Write(serialized);
          streamWriter.Close();
        }
        string folderpaths = "";
        foreach (string path in photoFoldersListBox.Items)
        {
          if (folderpaths == "")
          {
            folderpaths = path;
          }
          else
          {
            folderpaths = folderpaths + "," + path;
          }
        }
        settings.writeToXMLFile("FolderPaths", folderpaths);
        int interval = 10000;
        Int32.TryParse(timerTextBox.Text, out interval);
        settings.writeToXMLFile("Interval", interval.ToString());
        settings.writeToXMLFile("BackgroundPath", backgroundImageTextBox.Text);
        settings.writeToXMLFile("EXIFRotate", exifRotateCheckBox.Checked.ToString());

        //restart the service for new settings
        TimeSpan timeout = TimeSpan.FromMilliseconds(20000);
        if (watcherService.Status == ServiceControllerStatus.Running)
        {
          int millisec1 = Environment.TickCount;
          watcherService.Stop();
          watcherService.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
          // count the rest of the timeout
          int millisec2 = Environment.TickCount;
          timeout = TimeSpan.FromMilliseconds(2000 - (millisec2 - millisec1));
        }
        watcherService.Start();
        watcherService.WaitForStatus(ServiceControllerStatus.Running, timeout);
        //using (Settings xmlreader = new Settings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPPhotoSlideshow.xml"))
        //{
        //  int interval=10000;          
        //  Int32.TryParse(timerTextBox.Text, out interval);
        //  xmlreader.SetValue("MyConfig", "Interval", interval);
        //  string folderpaths = "";
        //  foreach (string path in photoFoldersListBox.Items)
        //  {
        //    if (folderpaths == "")
        //    {
        //      folderpaths = path;
        //    }
        //    else
        //    {
        //      folderpaths = folderpaths + "," + path;
        //    }
        //  }
        //  xmlreader.SetValue("MyConfig", "FolderPaths", folderpaths);
        //  xmlreader.SetValue("MyConfig", "BackgroundPath", backgroundImageTextBox.Text);
        //  Settings.SaveCache();
        //}

      }
      catch (Exception ex)
      {
        Log.Debug("MPPhotoslideshow.SetupForm() - Error {0}", ex.ToString());
      }
    }

    public void ResetGUI()
    {
      try
      {
        photoWidthTextBox.Text = "";
        photoHeightTextBox.Text = "";
        photoXPosTextBox.Text = "";
        photoYPosTextBox.Text = "";
        labelTextColorTextBox.Text = "";
        labelFontTextBox.Text = "";
        labelHeightTextBox.Text = "";
        rotateAngleNumericUpDown.Value = 0;
        labelXPosTextBox.Text = "";
        labelYPosTextBox.Text = "";
        borderBottomTextBox.Text = "";
        borderFilePathTextBox.Text = "mpslideshow_image_border.png";
        borderLeftTextBox.Text = "";
        borderTopTextBox.Text = "";
        borderRightTextBox.Text = "";
        pictureEnabledTextBox.Checked = false;
        templateEnabledCheckBox.Checked = false;
      }
      catch (Exception ex)
      {
        Log.Debug("MPPhotoslideshow.SetupForm() - Error {0}", ex.ToString());
      }
    }

    private void StoreCurrentTemplate()
    {
      try
      {
        StoreCurrentPhoto();
        if (previousSelectedTemplateIndex > -1)
        {
          PhotoTemplate template = (PhotoTemplate)photoTemplates[previousSelectedTemplateIndex];
          template.Photos = photoControls;
          template.Enabled = templateEnabledCheckBox.Checked;
        }
        previousSelectedTemplateIndex = templateComboBox.SelectedIndex;
      }
      catch (Exception ex)
      {
        Log.Debug("MPPhotoslideshow.SetupForm() - Error {0}", ex.ToString());
      }
    }

    private void SetupGUI()
    {
      if (pictureSelectorComboBox.SelectedIndex > -1)
      {
        PhotoControl control = (PhotoControl)photoControls[pictureSelectorComboBox.SelectedIndex];
        if (control.Image != null)
        {
          photoHeightTextBox.Text = control.Image.Height.ToString();
          photoWidthTextBox.Text = control.Image.Width.ToString();
          photoXPosTextBox.Text = control.Image.posX.ToString();
          photoYPosTextBox.Text = control.Image.posY.ToString();
          borderBottomTextBox.Text = control.Image.BorderBottom;
          borderFilePathTextBox.Text = control.Image.BorderPath;
          rotateAngleNumericUpDown.Value = control.Image.RotateAngle;
          if (borderFilePathTextBox.Text == "")
          {
            borderFilePathTextBox.Text = "mpslideshow_image_border.png";
          }
          borderLeftTextBox.Text = control.Image.BorderLeft;
          borderTopTextBox.Text = control.Image.BorderTop;
          borderRightTextBox.Text = control.Image.BorderRight;
        }
        if (control.Label != null)
        {

          labelHeightTextBox.Text = control.Label.Height.ToString();
          //labelWidthTextBox.Text = control.Label.Width.ToString();
          labelXPosTextBox.Text = control.Label.posX.ToString();
          labelYPosTextBox.Text = control.Label.posY.ToString();
          labelTextColorTextBox.Text = control.Label.TextColor;
          if (isMP2)
          {
            labelFontTextBox.Text = control.Label.FontSize;
          }
          else
          {
            labelFontTextBox.Text = control.Label.Font;
          }
        }
        pictureEnabledTextBox.Checked = control.Enabled;
        SetPhotoHelperLabels();
      }
    }

    private void StoreCurrentPhoto()
    {
      try
      {
        if (previousSelectedPictureIndex > -1)
        {
          PhotoControl control = (PhotoControl)photoControls[previousSelectedPictureIndex];
          int photoheight = 0;          
          Int32.TryParse(photoHeightTextBox.Text, out photoheight);
          control.Image.Height = photoheight;
          int photowidth = 0;          
          Int32.TryParse(photoWidthTextBox.Text, out photowidth);
          control.Image.Width = photowidth;
          int photoPosX = 0;          
          Int32.TryParse(photoXPosTextBox.Text, out photoPosX);
          control.Image.posX = photoPosX;
          int photoPosY = 0;
          Int32.TryParse(photoYPosTextBox.Text, out photoPosY);
          control.Image.posY = photoPosY;
          control.Image.BorderRight = borderRightTextBox.Text;
          control.Image.BorderLeft = borderLeftTextBox.Text;
          control.Image.BorderBottom = borderBottomTextBox.Text;
          control.Image.BorderTop = borderTopTextBox.Text;
          control.Image.BorderPath = borderFilePathTextBox.Text;
          int labelHeight = 0;
          Int32.TryParse(labelHeightTextBox.Text, out labelHeight);
          control.Label.Height = labelHeight;
          control.Image.RotateAngle = (int)rotateAngleNumericUpDown.Value;
          //int labelWidth = 0;
          //Int32.TryParse(labelWidthTextBox.Text, out labelWidth);
          //control.Label.Width = labelWidth;
          int labelPosX = 0;
          Int32.TryParse(labelXPosTextBox.Text, out labelPosX);
          control.Label.posX = labelPosX;
          int labelPosY = 0;
          Int32.TryParse(labelYPosTextBox.Text, out labelPosY);
          control.Label.posY = labelPosY;
          control.Label.TextColor = labelTextColorTextBox.Text;
          if (isMP2)
          {
            control.Label.FontSize = labelFontTextBox.Text;
          }
          else
          {
            control.Label.Font = labelFontTextBox.Text;
          }
          control.Enabled = pictureEnabledTextBox.Checked;
          photoControls[previousSelectedPictureIndex] = control;
        }
      }
      catch (Exception ex)
      {
        Log.Debug("MPPhotoslideshow.SetupForm() - Error {0}", ex.ToString());
      }
    }

    private void pictureSelectorComboBox_SelectionChangeCommitted(object sender, EventArgs e)
    {
      bool saveMe = true;
        if (pictureEnabledTextBox.Checked)
        {
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "" | borderBottomTextBox.Text == "" | borderFilePathTextBox.Text == "" | borderLeftTextBox.Text == "" | borderRightTextBox.Text == "" | borderTopTextBox.Text == "" | backgroundImageTextBox.Text == "")
          {
            pictureSelectorComboBox.SelectedIndex = previousSelectedPictureIndex;
            saveMe=false;
            MessageBox.Show("No picture field can be left blank");
          }
        }
        if (saveMe)
        {
          StoreCurrentPhoto();
          previousSelectedPictureIndex = pictureSelectorComboBox.SelectedIndex;         
          ResetGUI();
          SetupGUI();
          templateEnabledCheckBox.Checked = photoTemplates[templateComboBox.SelectedIndex].Enabled;
        }
    }

    private void addNewTemplateButton_Click(object sender, EventArgs e)
    {
      try
      {
        inputBox.SetupInputBox("New Template", "Enter the new template name");
        DialogResult dialogResult = inputBox.ShowDialog(this);
        if (dialogResult == DialogResult.OK)
        {
          bool exist = false;
          foreach (PhotoTemplate template in photoTemplates)
          {
            if (template.TemplateName == inputBox.InputText)
            {
              exist = true;
              break;
            }
          }
          if (exist)
          {
            MessageBox.Show("Please try again, a template with this name already exists");
          }
          else
          {
            StoreCurrentTemplate();
            photoControls = new BindingList<PhotoControl>();
            pictureSelectorComboBox.DataSource = photoControls;
            for (int i = 1; i < 9; i++)
            {
              photoControls.Add(new PhotoControl() { PhotoName = String.Format("Picture{0}", i), Image = new ImageControl(), Label = new LabelControl(), Enabled=false });
            }
            photoTemplates.Add(new PhotoTemplate() { TemplateName = inputBox.InputText, Id = Guid.NewGuid(), Version=new ModuleVersion(1,0,0,0) });
            templateComboBox.SelectedIndex = photoTemplates.Count - 1;
            previousSelectedTemplateIndex = photoTemplates.Count - 1;
            previousSelectedPictureIndex = 0;
            inputBox.InputText = "";
            ResetGUI();
          }
        }
      }
      catch (Exception ex)
      {
        Log.Debug("MPPhotoslideshow.SetupForm() - Error {0}", ex.ToString());
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      bool saveMe = true;
        if (pictureEnabledTextBox.Checked)
        {
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "")
          {
            saveMe=false;
            MessageBox.Show("No picture field can be left blank");
          }
        }
        if (saveMe)
        {
          Save();
        }
    }

    private void addFolderButton_Click(object sender, EventArgs e)
    {
      if (Directory.Exists(addNewFolderTextBox.Text))
      {
        photoFoldersListBox.Items.Add(addNewFolderTextBox.Text);
      }
      else
      {
        MessageBox.Show("That directory does not exist, please try again");
      }
    }

    private void addNewFolderTextBox_DoubleClick(object sender, EventArgs e)
    {
      DialogResult result = folderBrowserDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        addNewFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
      }
    }

    private void deleteFolderButton_Click(object sender, EventArgs e)
    {
      if (photoFoldersListBox.SelectedIndex > -1)
      {
        if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          photoFoldersListBox.Items.RemoveAt(photoFoldersListBox.SelectedIndex);
        }
      }
      else
      {
        MessageBox.Show("You must select an item to delete");
      }
    }

    private void photoCacheWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      foreach (string photofolder in photoFoldersListBox.Items)
      {        
        List<string> loadPictures = Directory.GetFiles(photofolder, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)).ToList<string>();
        ExifMetadata exifMetaData = new ExifMetadata();
        if (cacheProgressBar.InvokeRequired)
        {
          cacheProgressBar.Invoke(new MethodInvoker(delegate
          {
            cacheProgressBar.Maximum = loadPictures.Count();
          }));
        }
        if (progressLabel.InvokeRequired)
        {
          progressLabel.Invoke(new MethodInvoker(delegate
          {
            progressLabel.Text = String.Format("Currently processing {0}", photofolder);
          }));
        }
        int progress = 0;
        foreach (string pictureFile in loadPictures)
        {
          if ((photoCacheWorker.CancellationPending == true))
          {
            e.Cancel = true;
            break;
          }
          ExifMetadata.Metadata metaData = exifMetaData.GetExifMetadata(pictureFile);
          DateTime pictureDate = new DateTime(1901, 1, 1);
          DateTime.TryParse(metaData.DatePictureTaken.DisplayValue, out pictureDate);
          int width = 0;
          int height = 0;
          bool rotateFromExifOrientation = false;
          string[] res = metaData.ImageDimensions.DisplayValue.Split('x');
          Int32.TryParse(res[0], out width);
          Int32.TryParse(res[1], out height);
          double aspectratio = 0;
          if (width > height)
          {
            double value = (double)width / height;
            aspectratio = Math.Truncate(10 * (value)) / 10;
          }
          else
          {
            double value = (double)height / width;
            aspectratio = Math.Truncate(10 * (value)) / 10;
          }
          string orientation = metaData.Orientation.DisplayValue;
          bool flipHeightAndWidth = false;
          if (orientation != null)
          {
            if (orientation != "Normal")
            {
              switch (orientation)
              {
                case "Rotate 90":
                  flipHeightAndWidth = true;
                  break;
                case "Rotate 270":
                  flipHeightAndWidth = true;
                  break;
              }
            }
          }
          progress++;
          photoCacheWorker.ReportProgress(progress);
          if (flipHeightAndWidth)
          {
            _allPictures.Add(new Picture() { FilePath = pictureFile, DateTaken = pictureDate, Height = width, Width = height, AspectRatio = Convert.ToString(aspectratio), ExifOrientation = orientation });
          }
          else
          {
            _allPictures.Add(new Picture() { FilePath = pictureFile, DateTaken = pictureDate, Height = height, Width = width, AspectRatio = Convert.ToString(aspectratio), ExifOrientation = orientation });
          }
        }
        if ((photoCacheWorker.CancellationPending == true))
        {
          e.Cancel = true;
          break;
        }
      }
      if ((photoCacheWorker.CancellationPending == false))
      {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow";
        if (File.Exists(folder + @"\MPSlideshowCache.xml"))
        {
          File.Delete(folder + @"\MPSlideshowCache.xml");
        }
        using (StreamWriter streamWriter = new StreamWriter(folder + @"\MPSlideshowCache.xml"))
        {
          string serialized = XMLHelper.Serialize<List<Picture>>(_allPictures);
          streamWriter.Write(serialized);
          streamWriter.Close();
        }
      }
    }

    public decimal TruncateDecimal(decimal value, int precision)
    {
      decimal step = (decimal)Math.Pow(10, precision);
      int tmp = (int)Math.Truncate(step * value);
      return tmp / step;
    }

    private void buildCacheButton_Click(object sender, EventArgs e)
    {
      progressLabel.Text = "";
      cacheProgressBar.Style = ProgressBarStyle.Blocks;
      cacheProgressBar.Value = 0;
      cacheProgressBar.Minimum = 0;
      buildCacheButton.Enabled = false;
      cancelCacheLoadButton.Enabled = true;
      photoCacheWorker.RunWorkerAsync();
    }

    private void photoCacheWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      cacheProgressBar.Value=e.ProgressPercentage;
    }

    private void photoCacheWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if ((e.Cancelled == true))
      {
        progressLabel.Text="Canceled!";
      }

      else if (!(e.Error == null))
      {
        progressLabel.Text="Error: " + e.Error.Message;
      }

      else
      {
        progressLabel.Text="Done!";
      }
      buildCacheButton.Enabled = true;
      cancelCacheLoadButton.Enabled = false;
    }

    private void cancelCacheLoadButton_Click(object sender, EventArgs e)
    {
      photoCacheWorker.CancelAsync();
    }

    private void photoXPosTextBox_TextChanged(object sender, EventArgs e)
    {
      labelXPosTextBox.Text = photoXPosTextBox.Text;
    }

    private void photoYPosTextBox_TextChanged(object sender, EventArgs e)
    {
      //int YPos = 0;
      //int height = 0;
      //Int32.TryParse(photoYPosTextBox.Text, out YPos);
      //Int32.TryParse(photoHeightTextBox.Text, out height);
      //labelYPosTextBox.Text = (YPos + height).ToString();
    }

    private void photoWidthTextBox_TextChanged(object sender, EventArgs e)
    {
      //labelWidthTextBox.Text = photoWidthTextBox.Text;
      SetPhotoHelperLabels();
    }

    private void SetPhotoHelperLabels()
    {
      if (_allPictures.Count == 0)
      {
        photosInYourCollectionLabel.Text = "No cache built";
      }
      else
      {
        if (photoWidthTextBox.Text != "" & photoHeightTextBox.Text != "")
        {
          int height = 0;
          int width = 0;
          int.TryParse(photoWidthTextBox.Text, out width);
          int.TryParse(photoHeightTextBox.Text, out height);
          double value = 0;
          List<Picture> restrictedList = new List<Picture>();
          if (height > width)
          {
            value = (double) height/width;
            restrictedList = _allPictures.FindAll(t => t.Height > t.Width);
          }
          else
          {
            value = (double) width/height;
            restrictedList = _allPictures.FindAll(t => t.Width >= t.Height);
          }
          double aspectratio = Math.Truncate(10*(value))/10;
          aspectRatioLabel.Text = aspectratio.ToString();
          if (aspectratio == 1)
          {
            photosInYourCollectionLabel.Text = restrictedList.FindAll(t => t.AspectRatio == "1").Count.ToString();
          }
          else if (1.3 <= aspectratio & aspectratio <= 1.5)
          {
            photosInYourCollectionLabel.Text =
              restrictedList.FindAll(t => 1.3 <= Convert.ToDouble(t.AspectRatio) & Convert.ToDouble(t.AspectRatio) <= 1.5)
                .Count.ToString();
          }
          else if (1.5 < aspectratio & aspectratio < 2)
          {
            photosInYourCollectionLabel.Text =
              restrictedList.FindAll(t => 1.5 < Convert.ToDouble(t.AspectRatio) & Convert.ToDouble(t.AspectRatio) < 2)
                .Count.ToString();
          }
          else if (aspectratio >= 2)
          {
            photosInYourCollectionLabel.Text =
              restrictedList.FindAll(t => Convert.ToDouble(t.AspectRatio) >= 2).Count.ToString();
          }

        }
      }
    }

    private void backgroundImageTextBox_DoubleClick(object sender, EventArgs e)
    {
        DialogResult result = openFileDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
          backgroundImageTextBox.Text = openFileDialog1.FileName;
        }
    }

    private void borderFilePathTextBox_DoubleClick(object sender, EventArgs e)
    {
      DialogResult result = openFileDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        borderFilePathTextBox.Text = openFileDialog1.FileName;
      }
    }

    private void labelHeightTextBox_TextChanged(object sender, EventArgs e)
    {
      //borderBottomTextBox.Text = labelHeightTextBox.Text;
    }

    private void SetupForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (MessageBox.Show("Would you like to save your changes?","Save?",MessageBoxButtons.YesNo)==DialogResult.Yes)
      {
        if (pictureEnabledTextBox.Checked)
        {
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "" | borderBottomTextBox.Text == "" | borderFilePathTextBox.Text == "" | borderLeftTextBox.Text == "" | borderRightTextBox.Text == "" | borderTopTextBox.Text == "" |backgroundImageTextBox.Text=="")
          {
            MessageBox.Show("All fields must be filled in");
          }
          else
          {
            Save();
          }
        }
        else
        {
          Save();
        }
      }
    }

    private void borderToggleButton_Click(object sender, EventArgs e)
    {
      inputBox.SetupInputBox("New Border Value", "Enter the new border value");
        DialogResult dialogResult = inputBox.ShowDialog(this);
      if (dialogResult == DialogResult.OK)
      {
        int borderValue = 0;
        int.TryParse(inputBox.InputText, out borderValue);
        //Store any changes to the current photo before making the change
        StoreCurrentPhoto();
        foreach (PhotoTemplate template in photoTemplates)
        {
          foreach (PhotoControl photo in template.Photos)
          {
            photo.Image.BorderBottom = borderValue.ToString();
            photo.Image.BorderLeft = borderValue.ToString();
            photo.Image.BorderRight = borderValue.ToString();
            photo.Image.BorderTop = borderValue.ToString();
          }
        }
        //reload the GUI to reflect the change
        SetupGUI();
      }
    }

    private void labelColorToggleButton_Click(object sender, EventArgs e)
    {
      inputBox.SetupInputBox("New Label Color Value", "Enter the new label color value");
      DialogResult dialogResult = inputBox.ShowDialog(this);
      if (dialogResult == DialogResult.OK)
      {
        StoreCurrentPhoto();
        foreach (PhotoTemplate template in photoTemplates)
        {
          foreach (PhotoControl photo in template.Photos)
          {
            photo.Label.TextColor = inputBox.InputText;
          }
        }
        //reload the GUI to reflect the change
        SetupGUI();
      }
    }

    private void addPictureButton_Click(object sender, EventArgs e)
    {
      photoControls.Add(new PhotoControl() { PhotoName = String.Format("Picture{0}", photoControls.Count + 1), Image = new ImageControl(), Label = new LabelControl(), Enabled = false });
      StoreCurrentPhoto();
      previousSelectedPictureIndex = photoControls.Count -1;
      pictureSelectorComboBox.SelectedIndex = photoControls.Count - 1;
      ResetGUI();
    }

    private void photoHeightTextBox_TextChanged(object sender, EventArgs e)
    {
      SetPhotoHelperLabels();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      TemplateBuilder templateViewer = new TemplateBuilder(photoTemplates[templateComboBox.SelectedIndex]);
      templateViewer.Show();
    }
   

   
  }
}
