using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MPPhotoSlideshowCommon;

namespace MPPhotoSlideshowCommon
{
  public partial class SetupForm : Form
  {
    private string _pictureFolders = "";
    private XMLSettings settings;
    private List<Picture> _allPictures = new List<Picture>();
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
      }
      if (photoControls.Count > 0)
      {
        previousSelectedPictureIndex = 0;
        pictureSelectorComboBox.SelectedIndex = 0;
      }
      SetupGUI();
    }
    public void LoadSettings()
    {
      try
      {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\";
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
        }
        //reload from old cache location 
        if (photoTemplates.Count == 0)
        {
          if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPSlideshowTemplates.xml"))
          {
            using (StreamReader streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPSlideshowTemplates.xml"))
            {
              string stream = streamReader.ReadToEnd();
              if (stream.Length > 0)
              {
                photoTemplates = XMLHelper.Deserialize<BindingList<PhotoTemplate>>(stream);
              }
              streamReader.Close();
            }
            //write to the new file
            using (StreamWriter streamWriter = new StreamWriter(folder + @"\MPSlideshowTemplates.xml"))
            {
              string serialized = XMLHelper.Serialize<BindingList<PhotoTemplate>>(photoTemplates);
              streamWriter.Write(serialized);
              streamWriter.Close();
            }
            //cleanup the old
            File.Delete(folder + @"Media Portal\MPSlideshowTemplates.xml");
          }
        }
        int interval = 10000;
        DateTime lastPictureLoad = new DateTime(1901, 1, 1);
        settings = new XMLSettings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MPPhotoSlideshow2.xml");
        _pictureFolders = settings.getXmlAttribute("FolderPaths", "");
        Int32.TryParse(settings.getXmlAttribute("Interval", "10000"), out interval);
        backgroundImageTextBox.Text = settings.getXmlAttribute("BackgroundPath", "mpslideshow_background.png");
        
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

    //private void button1_Click(object sender, EventArgs e)
    //{
    //  using (Settings xmlreader = new Settings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPPhotoSlideshow.xml"))
    //  {
    //    _pictureFolders = xmlreader.GetValue("MyConfig", "FolderPaths");
    //  }
    //  List<string> loadPictures = Directory.GetFiles(_pictureFolders, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)).ToList<string>();
    //  MediaPortal.GUI.Pictures.ExifMetadata exifMetaData = new MediaPortal.GUI.Pictures.ExifMetadata();
    //  foreach (string pictureFile in loadPictures)
    //  {
    //    MediaPortal.GUI.Pictures.ExifMetadata.Metadata metaData = exifMetaData.GetExifMetadata(pictureFile);
    //    DateTime pictureDate = new DateTime(1901,1,1);
    //    DateTime.TryParse(metaData.DatePictureTaken.DisplayValue,out pictureDate);
    //    int width = 0;
    //    int height = 0;
    //    string[] res = metaData.ImageDimensions.DisplayValue.Split('x');
    //    Int32.TryParse(res[0], out width);
    //    Int32.TryParse(res[1], out height);
    //    //using (FileStream stream = new FileStream(pictureFile, FileMode.Open, FileAccess.Read))
    //    //{
    //    //  Bitmap pictureImage = new Bitmap(stream);
    //    //  width = pictureImage.Width;
    //    //  height = pictureImage.Height;
    //    //  stream.Close();
    //    //}
    //    _allPictures.Add(new Picture() { FilePath = pictureFile, DateTaken = pictureDate, Height = height, Width = width });
    //  }
    //  using (Settings xmlreader = new Settings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPPhotoSlideshow.xml"))
    //  {
    //    xmlreader.SetValue("MyConfig", "LastLoadCache", DateTime.Now);
    //  }
    //  string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal";
    //  File.Delete(folder + @"\MPSlideshowCache.xml");
    //  using (StreamWriter streamWriter = new StreamWriter(folder + @"\MPSlideshowCache.xml"))
    //  {
    //    string serialized = XMLHelper.Serialize<List<Picture>>(_allPictures);
    //    streamWriter.Write(serialized);
    //    streamWriter.Close();
    //  }
    
    //}

    //private void button2_Click(object sender, EventArgs e)
    //{
    //  List<PhotoTemplate> templates = new List<PhotoTemplate>();
    //  PhotoTemplate template1 = new PhotoTemplate() 
    //  { 
    //    picture1 = new ImageControl() 
    //    { 
    //      Width = 500, Height = 667,posX=38,posY=206 
    //    }, 
    //    picture1Label = new LabelControl() 
    //    { 
    //      posX = 38, posY = 873, Font= "fontB12", Height=50, Width=500, TextColor=System.Drawing.ColorTranslator.FromHtml("#FFFFFF").ToArgb()
    //    },
    //    picture2 = new ImageControl() 
    //    { 
    //      Width = 700, Height = 525,posX=610,posY=277 
    //    }, 
    //    picture2Label = new LabelControl() 
    //    { 
    //      posX = 610, 
    //      posY = 802, 
    //      Font= "fontB12", 
    //      Height=50, 
    //      Width=700, 
    //      TextColor=System.Drawing.ColorTranslator.FromHtml("#FFFFFF").ToArgb()
    //    },
    //    picture3 = new ImageControl() 
    //    { 
    //      Width = 500, 
    //      Height = 667,
    //      posX=1382,
    //      posY=206 
    //    }, 
    //    picture3Label = new LabelControl() 
    //    { 
    //      posX = 1382, 
    //      posY = 873, 
    //      Font= "fontB12", 
    //      Height=50, 
    //      Width=500, 
    //      TextColor=System.Drawing.ColorTranslator.FromHtml("#FFFFFF").ToArgb()
    //    } 
    //  };
    //  templates.Add(template1);
    //  PhotoTemplate template2 = new PhotoTemplate()
    //  {
    //    picture1 = new ImageControl()
    //    {
    //      Width = 500,
    //      Height = 667,
    //      posX = 38,
    //      posY = 0
    //    },
    //    picture1Label = new LabelControl()
    //    {
    //      posX = 38,
    //      posY = 667,
    //      Font = "fontB12",
    //      Height = 50,
    //      Width = 500,
    //      TextColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF").ToArgb()
    //    },
    //    picture2 = new ImageControl()
    //    {
    //      Width = 700,
    //      Height = 525,
    //      posX = 610,
    //      posY = 277
    //    },
    //    picture2Label = new LabelControl()
    //    {
    //      posX = 610,
    //      posY = 802,
    //      Font = "fontB12",
    //      Height = 50,
    //      Width = 700,
    //      TextColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF").ToArgb()
    //    },
    //    picture3 = new ImageControl()
    //    {
    //      Width = 500,
    //      Height = 667,
    //      posX = 1382,
    //      posY = 206
    //    },
    //    picture3Label = new LabelControl()
    //    {
    //      posX = 1382,
    //      posY = 873,
    //      Font = "fontB12",
    //      Height = 50,
    //      Width = 500,
    //      TextColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF").ToArgb()
    //    }
    //  };
    //  templates.Add(template2);
    //  string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal";
    //  File.Delete(folder + @"\MPSlideshowTemplates.xml");
    //  using (StreamWriter streamWriter = new StreamWriter(folder + @"\MPSlideshowTemplates.xml"))
    //  {
    //    string serialized = XMLHelper.Serialize<List<PhotoTemplate>>(templates);
    //    streamWriter.Write(serialized);
    //    streamWriter.Close();
    //  }
    //}

    private void templateComboBox_SelectionChangeCommitted(object sender, EventArgs e)
    {
      try
      {
        bool saveMe = true;
        if (pictureEnabledTextBox.Checked)
        {
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelWidthTextBox.Text == "" | labelHeightTextBox.Text == "" | labelXPosTextBox.Text == "" | labelYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "" | borderBottomTextBox.Text == "" | borderFilePathTextBox.Text == "" | borderLeftTextBox.Text == "" | borderRightTextBox.Text == "" | borderTopTextBox.Text == "" | backgroundImageTextBox.Text == "")
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
        File.Delete(folder + @"\MPSlideshowTemplates.xml");
        using (StreamWriter streamWriter = new StreamWriter(folder + @"\MPSlideshowTemplates.xml"))
        {
          string serialized = XMLHelper.Serialize<BindingList<PhotoTemplate>>(photoTemplates);
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
        labelWidthTextBox.Text = "";
        labelXPosTextBox.Text = "";
        labelYPosTextBox.Text = "";
        borderBottomTextBox.Text = "";
        borderFilePathTextBox.Text = "mpslideshow_image_border.png";
        borderLeftTextBox.Text = "";
        borderTopTextBox.Text = "";
        borderRightTextBox.Text = "";
        pictureEnabledTextBox.Checked = false;
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
          labelWidthTextBox.Text = control.Label.Width.ToString();
          labelXPosTextBox.Text = control.Label.posX.ToString();
          labelYPosTextBox.Text = control.Label.posY.ToString();
          labelTextColorTextBox.Text = control.Label.TextColor;
          labelFontTextBox.Text = control.Label.Font;
        }
        pictureEnabledTextBox.Checked = control.Enabled;
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
          int labelWidth = 0;
          Int32.TryParse(labelWidthTextBox.Text, out labelWidth);
          control.Label.Width = labelWidth;
          int labelPosX = 0;
          Int32.TryParse(labelXPosTextBox.Text, out labelPosX);
          control.Label.posX = labelPosX;
          int labelPosY = 0;
          Int32.TryParse(labelYPosTextBox.Text, out labelPosY);
          control.Label.posY = labelPosY;
          control.Label.TextColor = labelTextColorTextBox.Text;
          control.Label.Font = labelFontTextBox.Text;
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
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelWidthTextBox.Text == "" | labelHeightTextBox.Text == "" | labelXPosTextBox.Text == "" | labelYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "" | borderBottomTextBox.Text == "" | borderFilePathTextBox.Text == "" | borderLeftTextBox.Text == "" | borderRightTextBox.Text == "" | borderTopTextBox.Text == "" | backgroundImageTextBox.Text == "")
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
        }
    }

    private void addNewTemplateButton_Click(object sender, EventArgs e)
    {
      try
      {
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
            photoTemplates.Add(new PhotoTemplate() { TemplateName = inputBox.InputText });
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
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelWidthTextBox.Text == "" | labelHeightTextBox.Text == "" | labelXPosTextBox.Text == "" | labelYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "")
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
          //bool rotateFromExifOrientation = false;
          string[] res = metaData.ImageDimensions.DisplayValue.Split('x');
          Int32.TryParse(res[0], out width);
          Int32.TryParse(res[1], out height);
          //string orientation = metaData.Orientation.DisplayValue;
          //if (orientation == "Rotate 90")
          //{
          //  int orientWidth = 0;
          //  int orientHeight = 0;
          //  using (FileStream stream = new FileStream(pictureFile, FileMode.Open, FileAccess.Read))
          //  {
          //    Bitmap pictureImage = new Bitmap(stream);
          //    orientWidth = pictureImage.Width;
          //    orientHeight = pictureImage.Height;
          //    stream.Close();
          //  }
          //  if (orientWidth > orientHeight)
          //  {
          //    //we need to flip the width and height so when we go to rotate it shows in the right template
          //    width = orientHeight;
          //    height = orientWidth;
          //    rotateFromExifOrientation = true;
          //  }
          //}
          progress++;
          photoCacheWorker.ReportProgress(progress);
          _allPictures.Add(new Picture() { FilePath = pictureFile, DateTaken = pictureDate, Height = height, Width = width });
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
      int YPos = 0;
      int height = 0;
      Int32.TryParse(photoYPosTextBox.Text, out YPos);
      Int32.TryParse(photoHeightTextBox.Text, out height);
      labelYPosTextBox.Text = (YPos + height).ToString();
    }

    private void photoWidthTextBox_TextChanged(object sender, EventArgs e)
    {
      labelWidthTextBox.Text = photoWidthTextBox.Text;
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
      borderBottomTextBox.Text = labelHeightTextBox.Text;
    }

    private void SetupForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (MessageBox.Show("Would you like to save your changes?","Save?",MessageBoxButtons.YesNo)==DialogResult.Yes)
      {
        if (pictureEnabledTextBox.Checked)
        {
          if (photoHeightTextBox.Text == "" | photoWidthTextBox.Text == "" | photoXPosTextBox.Text == "" | photoYPosTextBox.Text == "" | labelWidthTextBox.Text == "" | labelHeightTextBox.Text == "" | labelXPosTextBox.Text == "" | labelYPosTextBox.Text == "" | labelTextColorTextBox.Text == "" | labelFontTextBox.Text == "" | borderBottomTextBox.Text == "" | borderFilePathTextBox.Text == "" | borderLeftTextBox.Text == "" | borderRightTextBox.Text == "" | borderTopTextBox.Text == "" |backgroundImageTextBox.Text=="")
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

   
  }
}
