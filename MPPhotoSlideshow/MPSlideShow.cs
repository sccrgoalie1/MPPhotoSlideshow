using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.Dialogs;
using MediaPortal.Database;
using MediaPortal.Picture.Database;
using System.IO;
using MediaPortal.Profile;
using System.Drawing;
using MPPhotoSlideshowCommon;


namespace MPPhotoSlideshow
{
  public class MPSlideShow:GUIWindow, ISetupForm
  {
    //[SkinControlAttribute(2)]
    //protected GUIButtonControl picture1Button = null;
    [SkinControlAttribute(1)]
    protected GUIImage background = null;
    [SkinControlAttribute(67)]
    protected GUIImage picture1 = null;
    [SkinControlAttribute(671)]
    protected GUILabelControl picture1Label = null;
    [SkinControlAttribute(68)]
    protected GUIImage picture2 = null;
    [SkinControlAttribute(681)]
    protected GUILabelControl picture2Label = null;
    [SkinControlAttribute(69)]
    protected GUIImage picture3 = null;
    [SkinControlAttribute(691)]
    protected GUILabelControl picture3Label = null;
    [SkinControlAttribute(70)]
    protected GUIImage picture4 = null;
    [SkinControlAttribute(701)]
    protected GUILabelControl picture4Label = null;
    [SkinControlAttribute(71)]
    protected GUIImage picture5 = null;
    [SkinControlAttribute(711)]
    protected GUILabelControl picture5Label = null;
    [SkinControlAttribute(72)]
    protected GUIImage picture6 = null;
    [SkinControlAttribute(721)]
    protected GUILabelControl picture6Label = null;
    [SkinControlAttribute(73)]
    protected GUIImage picture7 = null;
    [SkinControlAttribute(731)]
    protected GUILabelControl picture7Label = null;
    [SkinControlAttribute(74)]
    protected GUIImage picture8 = null;
    [SkinControlAttribute(741)]
    protected GUILabelControl picture8Label = null;
    private ModuleVersion currentTemplateVersion = new ModuleVersion(1, 0, 0, 0);
    private Random _vertical4x3Rnd = new Random();
    private Random _horizontal4x3Rnd = new Random();
    private Random _verticalPanoramasRnd = new Random();
    private Random _horizontalPanoramasRnd = new Random();
    private Random _verticalSquareRnd = new Random();
    private Random _horizontalSquareRnd = new Random();
    private Random _vertical16x9Rnd = new Random();
    private Random _horizontal16x9Rnd = new Random();
    private Random _templateRnd = new Random();
    private XMLSettings settings;
    private Timer timer = new Timer();
    private string _backgroundImagePath;
    private List<Picture> _allPictures = new List<Picture>();
    private List<Picture> _vertical4x3Pictures = new List<Picture>();
    private List<Picture> _horizontal4x3Pictures = new List<Picture>();
    private List<Picture> _verticalPanoramasPictures = new List<Picture>();
    private List<Picture> _horizontalPanoramasPictures = new List<Picture>();
    private List<Picture> _verticalSquarePictures = new List<Picture>();
    private List<Picture> _horizontalSquarePictures = new List<Picture>();
    private List<Picture> _vertical16x9Pictures = new List<Picture>();
    private List<Picture> _horizontal16x9Pictures = new List<Picture>();
    private List<PhotoTemplate> _photoTemplates = new List<PhotoTemplate>();
    public MPSlideShow()
    {

    }



    #region ISetupForm Members
    //Returns the name of the plugin which is shown in the plugin menu
    public string PluginName()
    {
      return "MPPhotoSlideshow";
    }
    //Returns the author of the plugin which is shown in the plugin menu
    public string Author()
    {
      return "sccrgoalie1";
    }
    //Returns the description which is shown in the plugin menu
    public string Description()
    {
      return "A photo slideshow of all your photos";
    }
    //show the setup dialog
    public void ShowPlugin()
    {
      Form setupForm = new SetupForm();
      setupForm.Show();
      //MessageBox.Show("Nothing to configure...yet");
    }
    //Indicates whether the plugin can be enabled/disabled
    public bool CanEnable()
    {
      return true;
    }
    //Get Window-ID
    public int GetWindowId()
    {
      //window id of the window plugin beloinging to this setup
      return 58963;
    }
    //Indicates whether this plugin is enabled by default
    public bool DefaultEnabled()
    {
      return true;
    }
    //Indicates if a plugin has it's own setup screen
    public bool HasSetup()
    {
      return true;
    }

    /// <summary>
    /// If the plugin show have it's own button on the main menu of Media Portal then it should return true
    /// </summary>
    /// <param name="strButtonText">text the button should have</param>
    /// <param name="strButtonImage">image for the button or empty for default</param>
    /// <param name="strButtonImageFocus">image for the button or empty for default</param>
    /// <param name="strPictureImage">subpicture for the button or empty for none</param>
    /// <returns></returns>
    public bool GetHome(out string strButtonText, out string strButtonImage, out string strButtonImageFocus, out string strPictureImage)
    {
      strButtonText = "Photo Slideshow";
      strButtonImage = String.Empty;
      strButtonImageFocus = String.Empty;
      strPictureImage = String.Empty;
      return true;
    }
    //With the getid will be a window-plugin / otherwise a process plugin
    public override int GetID
    {
      get
      {
        return 58963;
      }
      set
      {
      }
    }
    //load our skin file
    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\MPPhotoSlideshow.xml");
    }
    //Called before loading the page
    protected override void OnPageLoad()
    {
      MPPhotoSlideshowCommon.Log.Init(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MP1Log", "log", LogType.Debug);
      int interval = 10000;
      settings = new XMLSettings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MPPhotoSlideshow2.xml");
        Int32.TryParse(settings.getXmlAttribute("Interval"),out interval);
        _backgroundImagePath = settings.getXmlAttribute("BackgroundPath");
      //if (lastPictureLoad.AddDays(7) < DateTime.Now)
      //{
      //  Log.Debug("MPSlideshow.OnPageLoad() - Reloading cache as time has expired");
      //  LoadPicturesToCache();
      //}
      //else
      //{
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow";
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
        if (File.Exists(folder + @"\SlideshowTemplates.xml"))
        {
          using (StreamReader streamReader = new StreamReader(folder + @"\SlideshowTemplates.xml"))
          {
            string stream = streamReader.ReadToEnd();
            if (stream.Length > 0)
            {
              Templates template = XMLHelper.Deserialize<Templates>(stream);
              foreach (PhotoTemplate ptemp in template.TemplatesList)
              {
                _photoTemplates.Add(ptemp);
              }
              currentTemplateVersion = template.TemplateVersion;
            }
            streamReader.Close();
          }
          //only use enabled templates
          _photoTemplates = _photoTemplates.FindAll(p => p.Enabled);

        }
        else
        {
          if (File.Exists(folder + @"\MPSlideshowTemplates.xml"))
          {
            using (StreamReader streamReader = new StreamReader(folder + @"\MPSlideshowTemplates.xml"))
            {
              string stream = streamReader.ReadToEnd();
              if (stream.Length > 0)
              {
                _photoTemplates = XMLHelper.Deserialize<List<PhotoTemplate>>(stream);
              }
              streamReader.Close();
            }
            //only use enabled templates
            _photoTemplates = _photoTemplates.FindAll(p => p.Enabled);

          }
        }
        if (_allPictures.Count > 0)
        {
          foreach (Picture picture in _allPictures)
          {
            double aspectratio=0;
            double.TryParse(picture.AspectRatio, out aspectratio);
            if (picture.Width > picture.Height)
            {
              if (aspectratio == 1)
              {
                _horizontalSquarePictures.Add(picture);
              }
              else if (1 < aspectratio & aspectratio <= 1.5)
              {
                _horizontal4x3Pictures.Add(picture);
              }
              else if (1.5 < aspectratio & aspectratio < 2)
              {
                _horizontal16x9Pictures.Add(picture);
              }
              else if (aspectratio >= 2)
              {
                _horizontalPanoramasPictures.Add(picture);
              }
            }
            else
            {
              if (aspectratio == 1)
              {
                _verticalSquarePictures.Add(picture);
              }
              else if (1 < aspectratio & aspectratio <= 1.5)
              {
                _vertical4x3Pictures.Add(picture);
              }
              else if (1.5 < aspectratio & aspectratio < 2)
              {
                _vertical16x9Pictures.Add(picture);
              }
              else if (aspectratio >= 2)
              {
                _verticalPanoramasPictures.Add(picture);
              }
            }
          }
          //_horizontal4x3Pictures = _allPictures.Where(s => s.Width > s.Height).ToList<Picture>();
          //_vertical4x3Pictures = _allPictures.Where(s => s.Width < s.Height).ToList<Picture>();
          //_allPictures = null;
          background.SetFileName(_backgroundImagePath);
          MPPhotoSlideshowCommon.Log.Debug("MPSlideshow - Just finished loading horizontal and vertical pictures");
          LoadPage();
          timer.Tick += new EventHandler(timer_Tick);
          timer.Interval = interval;
          timer.Start();
          MPPhotoSlideshowCommon.Log.Debug("MPSlideshow.OnPageLoad() - Pictures count {0}", _allPictures.Count);
        }
        else
        {
          GUIDialogOK dlg = (GUIDialogOK)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_OK);
          dlg.SetHeading("Error");
          dlg.SetLine(1, "Please run the configuration tool and build a cache first");
          dlg.SetLine(2, String.Empty);
          dlg.SetLine(3, String.Empty);
          dlg.DoModal(GUIWindowManager.ActiveWindow);
        }
      base.OnPageLoad();
    }
    //public void RotateImageFromExif(string imagePath, string exifOrientation)
    //{
    //  Bitmap photoForRotation = (Bitmap)Bitmap.FromFile(imagePath);
    //  photoForRotation.
    //}

    public void UpdateCache()
    {
      //Directory.en
      //DirectoryInfo test = new DirectoryInfo("");
      //List<string> loadPictures = Directory.GetFiles("", "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)).ToList<string>();
      //var lastQuarter = DateTime.Now.AddMonths(-3);
      //list = UT.GetAllFiles(mask, (info) => info.CreationTime >= lastQuarter).ToList();
      //Assert.AreNotEqual(0, list.Count);
    }
    //public void LoadPicturesToCache()
    //{
    //  List<string> loadPictures = Directory.GetFiles(_pictureFolders, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)).ToList<string>();
    //  MediaPortal.GUI.Pictures.ExifMetadata exifMetaData = new MediaPortal.GUI.Pictures.ExifMetadata();
    //  foreach (string pictureFile in loadPictures)
    //  {
    //    MediaPortal.GUI.Pictures.ExifMetadata.Metadata metaData = exifMetaData.GetExifMetadata(pictureFile);
    //    DateTime pictureDate = new DateTime(1901,1,1);
    //    if (metaData.DatePictureTaken.DisplayValue !="")
    //    {
    //      //Log.Debug("Current date is {0}", metaData.DatePictureTaken.DisplayValue);
    //       pictureDate = Convert.ToDateTime(metaData.DatePictureTaken.DisplayValue);
    //    }
    //    int width = 0;
    //    int height = 0;
    //    using (FileStream stream = new FileStream(pictureFile, FileMode.Open, FileAccess.Read))
    //    {
    //      Bitmap pictureImage = new Bitmap(stream);
    //      width = pictureImage.Width;
    //      height = pictureImage.Height;
    //      stream.Close();
    //    }
    //    _allPictures.Add(new Picture() { FilePath = pictureFile, DateTaken = pictureDate, Height = height, Width = width });
    //  }
    //  using (Settings xmlreader = new Settings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPPhotoSlideshow.xml"))
    //  {
    //    xmlreader.SetValue("MyConfig", "LastLoadCache", DateTime.Now);
    //  }
    //  string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal\MPPhotoSlideshow.xml";
    //  File.Delete(folder + @"\MPSlideshowCache.xml");
    //  using (StreamWriter streamWriter = new StreamWriter(folder + @"\MPSlideshowCache.xml"))
    //  {
    //    string serialized = XMLHelper.Serialize<List<Picture>>(_allPictures);
    //    streamWriter.Write(serialized);
    //    streamWriter.Close();
    //  }
    //}

    //Called when Exiting
    protected override void OnPageDestroy(int newWindowId)
    {
      //string dbPath = FolderSettings.DatabaseName;
      //dbPath = MediaPortal.Picture.Database.PictureDatabase.DatabaseName;
      //if (!string.IsNullOrEmpty(dbPath) && PathIsNetworkPath(dbPath))
      //{
      //  Log.Info("Main: disposing PictureDatabase sqllite database.");
      //  MediaPortal.Picture.Database.PictureDatabase.Dispose();
      //}
      DisposePictures();
      timer.Stop();
      timer.Tick -= new EventHandler(timer_Tick);
      timer.Dispose();
      base.OnPageDestroy(newWindowId);
    }
    //Called when a button is pressed
    protected override void OnClicked(int controlId, GUIControl control, MediaPortal.GUI.Library.Action.ActionType actionType)
    {
      //if (control == picture1Button)
      //{
      //  OnPictureOneClicked();
      //}
      base.OnClicked(controlId, control, actionType);
    }

    private void OnPictureOneClicked()
    {
      GUIDialogOK dlg = (GUIDialogOK)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_OK);
      dlg.SetHeading("Picture 1 has been pressed");
      dlg.SetLine(1, "You pressed button 1");
      dlg.SetLine(2, String.Empty);
      dlg.SetLine(3, String.Empty);
      dlg.DoModal(GUIWindowManager.ActiveWindow);
    }
    private void LoadPage()
    {
      timer.Stop();
      DisposePictures();
      PhotoTemplate template = _photoTemplates[_templateRnd.Next(_photoTemplates.Count)];
      if (template.Photos[0].Enabled)
      {
        SetupTemplate(picture1, picture1Label, template, 0);
      }
      else
      {
        picture1.SetFileName(String.Empty);
        picture1Label.Label = String.Empty;
      }
      if (template.Photos[1].Enabled)
      {
        SetupTemplate(picture2, picture2Label, template, 1);
      }
      else
      {
        picture2.SetFileName(String.Empty);
        picture2Label.Label = String.Empty;
      }
      if (template.Photos[2].Enabled)
      {
        SetupTemplate(picture3, picture3Label, template, 2);
      }
      else
      {
        picture3.SetFileName(String.Empty);
        picture3Label.Label = String.Empty;
      }
      if (template.Photos[3].Enabled)
      {
        SetupTemplate(picture4, picture4Label, template, 3);
      }
      else
      {
        picture4.SetFileName(String.Empty);
        picture4Label.Label = String.Empty;
      }
      if (template.Photos[4].Enabled)
      {
        SetupTemplate(picture5, picture5Label, template, 4);
      }
      else
      {
        picture5.SetFileName(String.Empty);
        picture5Label.Label = String.Empty;
      }
      if (template.Photos[5].Enabled)
      {
        SetupTemplate(picture6, picture6Label, template, 5);
      }
      else
      {
        picture6.SetFileName(String.Empty);
        picture6Label.Label = String.Empty;
      }
      if (template.Photos[6].Enabled)
      {
        SetupTemplate(picture7, picture7Label, template, 6);
      }
      else
      {
        picture7.SetFileName(String.Empty);
        picture7Label.Label = String.Empty;
      }
      if (template.Photos[7].Enabled)
      {
        SetupTemplate(picture8, picture8Label, template, 7);
      }
      else
      {
        picture8.SetFileName(String.Empty);
        picture8Label.Label = String.Empty;
      }
      timer.Start();
    }

    private void SetupTemplate(GUIImage image, GUILabelControl imageLabel, PhotoTemplate template, int index)
    {
      Picture imageFileName = new Picture();
      double trueHeight = 0;
      if (template.Photos[index].Image.Height > template.Photos[index].Image.Width)
      {
        double value = (double)template.Photos[index].Image.Height / template.Photos[index].Image.Width;
        double aspectratio = Math.Truncate(10 * (value)) / 10;
        if (aspectratio == 1)
        {
          imageFileName= _verticalSquarePictures[_verticalSquareRnd.Next(_verticalSquarePictures.Count)];
        }
        else if (1.3 <= aspectratio & aspectratio <= 1.5)
        {
          imageFileName= _vertical4x3Pictures[_vertical4x3Rnd.Next(_vertical4x3Pictures.Count)];
        }
        else if (1.5 < aspectratio & aspectratio < 2)
        {
          imageFileName= _vertical16x9Pictures[_vertical16x9Rnd.Next(_vertical16x9Pictures.Count)];
        }
        else if (aspectratio >= 2)
        {
          imageFileName= _verticalPanoramasPictures[_verticalPanoramasRnd.Next(_verticalPanoramasPictures.Count)];
        }
        trueHeight=template.Photos[index].Image.Height;
      }
      else
      {
        double value = (double)template.Photos[index].Image.Width / template.Photos[index].Image.Height;
        double aspectratio = Math.Truncate(10 * (value)) / 10;
        if (aspectratio == 1)
        {
          imageFileName= _horizontalSquarePictures[_horizontalSquareRnd.Next(_horizontalSquarePictures.Count)];
        }
        else if (1.3 <= aspectratio & aspectratio <= 1.5)
        {
          imageFileName= _horizontal4x3Pictures[_horizontal4x3Rnd.Next(_horizontal4x3Pictures.Count)];
        }
        else if (1.5 < aspectratio & aspectratio < 2)
        {
          imageFileName= _horizontal16x9Pictures[_horizontal16x9Rnd.Next(_horizontal16x9Pictures.Count)];
        }
        else if (aspectratio >= 2)
        {
          imageFileName= _horizontalPanoramasPictures[_horizontalPanoramasRnd.Next(_horizontalPanoramasPictures.Count)];
        }
        trueHeight = (double)imageFileName.Height / (imageFileName.Width / template.Photos[index].Image.Width);
      }
      image.Height = template.Photos[index].Image.Height;
      image.Width = template.Photos[index].Image.Width;
      image.KeepAspectRatio = true;
      image.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[index].Image.BorderLeft, template.Photos[index].Image.BorderRight, template.Photos[index].Image.BorderTop, template.Photos[index].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[index].Image.BorderPath, 0xFFFFFFFF, false, false);
      image.SetPosition(template.Photos[index].Image.posX, template.Photos[index].Image.posY);
      image.SetFileName(imageFileName.FilePath);
      image.ScaleToScreenResolution();
      image.Refresh();
      imageLabel.Label = imageFileName.DateTaken.ToString("d");
      imageLabel.Height = 10;
      imageLabel.Width = image.Width + Convert.ToInt32(template.Photos[index].Image.BorderLeft) + Convert.ToInt32(template.Photos[index].Image.BorderRight);
      //MPPhotoSlideshowCommon.Log.Debug("Picture 1 Original X {0}, Updated X {1}, Original Y {2}, Updated Y {3}", template.Photos[0].Image.posX, image.XPosition, template.Photos[0].Image.posY, image.YPosition);
      //MPPhotoSlideshowCommon.Log.Debug("Picture 1 Original Width {0}, Updated Width {1}, Original Height {2}, Updated Height {3}", template.Photos[0].Image.Width, image.Width, template.Photos[0].Image.Height, image.Height);
      imageLabel.SetPosition(image.XPosition, image.YPosition + image.Height + Convert.ToInt32(template.Photos[index].Image.BorderBottom) + Convert.ToInt32(template.Photos[index].Image.BorderTop));
      imageLabel.FontName = template.Photos[index].Label.Font;
      imageLabel.TextColor = ColorTranslator.FromHtml(template.Photos[index].Label.TextColor).ToArgb();
    }
    private void DisposePictures()
    {
      picture1.Dispose();
      picture2.Dispose();
      picture3.Dispose();
      picture4.Dispose();
      picture5.Dispose();
      picture6.Dispose();
      picture7.Dispose();
      picture8.Dispose();
      picture1Label.Dispose();
      picture2Label.Dispose();
      picture3Label.Dispose();
      picture4Label.Dispose();
      picture5Label.Dispose();
      picture6Label.Dispose();
      picture7Label.Dispose();
      picture8Label.Dispose();
    }
    private void timer_Tick(Object sender, EventArgs e)
    {
      //Log.Debug("MPSlideshow.timer_Tick() - Timer passed loading new page");
      LoadPage();
    }
    //public override void Render(float timePassed)
    //{
    //  base.Render(timePassed);
    //  picture1.Render(timePassed);
    //  picture1Label.Render(timePassed);
    //  picture2.Render(timePassed);
    //  picture2Label.Render(timePassed);
    //  picture3.Render(timePassed);
    //  picture3Label.Render(timePassed);
    //}
    #endregion
  }

}
