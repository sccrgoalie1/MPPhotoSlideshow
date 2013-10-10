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
    private Random _verticalRnd = new Random();
    private Random _horizontalRnd = new Random();
    private Random _templateRnd = new Random();
    private XMLSettings settings;
    private Timer timer = new Timer();
    private string _backgroundImagePath;
    private List<Picture> _allPictures = new List<Picture>();
    private List<Picture> _verticalPictures = new List<Picture>();
    private List<Picture> _horizontalPictures = new List<Picture>();
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

        }
      //}
        if (_allPictures.Count > 0)
        {
          foreach (Picture picture in _allPictures)
          {
            if (picture.Width > picture.Height)
            {
              _horizontalPictures.Add(picture);
            }
            else
            {
              _verticalPictures.Add(picture);
            }
          }
          //_horizontalPictures = _allPictures.Where(s => s.Width > s.Height).ToList<Picture>();
          //_verticalPictures = _allPictures.Where(s => s.Width < s.Height).ToList<Picture>();
          //_allPictures = null;
          background.SetFileName(_backgroundImagePath);
          MediaPortal.GUI.Library.Log.Debug("MPSlideshow - Just finished loading horizontal and vertical pictures");
          LoadPage();
          timer.Tick += new EventHandler(timer_Tick);
          timer.Interval = interval;
          timer.Start();
          MediaPortal.GUI.Library.Log.Debug("MPSlideshow.OnPageLoad() - Pictures count {0}", _allPictures.Count);
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
        //picture1.Dispose();
        Picture picture1FileName;
        if (template.Photos[0].Image.Height > template.Photos[0].Image.Width)
        {
          picture1FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture1FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture1.Height = template.Photos[0].Image.Height;
        picture1.Width = template.Photos[0].Image.Width;
        //VisualEffect effect = new VisualEffect();
        ////effect.AnimationType = AnimationType.VisibleChange;
        //effect.Effect = EffectType.RotateZ;
        //effect.Amount = 200;
        //effect.CenterX = 360;
        //effect.CenterY = 288;
        //effect.EndX = 30;
        //effect.Animate(200, true);
        //effect.AnimationType = AnimationType.WindowOpen;
        //effect.Effect = EffectType.Fade;
        //effect.StartX=100;
        //effect.StartY=100;
        //effect.EndX=100;
        //effect.EndY=105;
        //effect.Amount = 500;
        //List<VisualEffect> effects = new List<VisualEffect>();
        //effects.Add(effect);
        //picture1.SetAnimations(effects);
        picture1.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[0].Image.BorderLeft, template.Photos[0].Image.BorderRight, template.Photos[0].Image.BorderTop, template.Photos[0].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[0].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture1.SetPosition(template.Photos[0].Image.posX, template.Photos[0].Image.posY);
        picture1.SetFileName(picture1FileName.FilePath);
        //if (picture1FileName.RotateFromExifOrientation)
        //{
        //  //rotate the image in memory only
        //  picture1.MemoryImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
        //  picture1.set
        //}
        picture1.ScaleToScreenResolution();
        picture1.Refresh();
        picture1Label.Label = picture1FileName.DateTaken.ToString("d");
        picture1Label.Height = template.Photos[0].Label.Height;
        picture1Label.Width = template.Photos[0].Label.Width;
        picture1Label.SetPosition(template.Photos[0].Label.posX, template.Photos[0].Label.posY);
        picture1Label.FontName = template.Photos[0].Label.Font;
        picture1Label.TextColor = ColorTranslator.FromHtml(template.Photos[0].Label.TextColor).ToArgb();
        picture1Label.ScaleToScreenResolution();
      }
      else
      {
        picture1.SetFileName(String.Empty);
        picture1Label.Label = String.Empty;
      }
      if (template.Photos[1].Enabled)
      {
        //picture2.Dispose();
        Picture picture2FileName;
        if (template.Photos[1].Image.Height > template.Photos[1].Image.Width)
        {
          picture2FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture2FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture2.Height = template.Photos[1].Image.Height;
        picture2.Width = template.Photos[1].Image.Width;
        picture2.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[1].Image.BorderLeft, template.Photos[1].Image.BorderRight, template.Photos[1].Image.BorderTop, template.Photos[1].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[1].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture2.SetPosition(template.Photos[1].Image.posX, template.Photos[1].Image.posY);
        picture2.SetFileName(picture2FileName.FilePath);
        picture2.ScaleToScreenResolution();
        picture2.Refresh();
        picture2Label.Label = picture2FileName.DateTaken.ToString("d");
        picture2Label.Height = template.Photos[1].Label.Height;
        picture2Label.Width = template.Photos[1].Label.Width;
        picture2Label.SetPosition(template.Photos[1].Label.posX, template.Photos[1].Label.posY);
        picture2Label.FontName = template.Photos[1].Label.Font;
        picture2Label.TextColor = ColorTranslator.FromHtml(template.Photos[1].Label.TextColor).ToArgb(); ;
        picture2Label.ScaleToScreenResolution();
      }
      else
      {
        picture2.SetFileName(String.Empty);
        picture2Label.Label = String.Empty;
      }
      if (template.Photos[2].Enabled)
      {
        //picture3.Dispose();
        Picture picture3FileName;
        if (template.Photos[2].Image.Height > template.Photos[2].Image.Width)
        {
          picture3FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture3FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture3.Height = template.Photos[2].Image.Height;
        picture3.Width = template.Photos[2].Image.Width;
        picture3.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[2].Image.BorderLeft, template.Photos[2].Image.BorderRight, template.Photos[2].Image.BorderTop, template.Photos[2].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[2].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture3.SetPosition(template.Photos[2].Image.posX, template.Photos[2].Image.posY);
        picture3.SetFileName(picture3FileName.FilePath);
        picture3.ScaleToScreenResolution();
        picture3.Refresh();
        picture3Label.Label = picture3FileName.DateTaken.ToString("d");
        picture3Label.Height = template.Photos[2].Label.Height;
        picture3Label.Width = template.Photos[2].Label.Width;
        picture3Label.SetPosition(template.Photos[2].Label.posX, template.Photos[2].Label.posY);
        picture3Label.FontName = template.Photos[2].Label.Font;
        picture3Label.TextColor = ColorTranslator.FromHtml(template.Photos[2].Label.TextColor).ToArgb(); ;
        picture3Label.ScaleToScreenResolution();
      }
      else
      {
        picture3.SetFileName(String.Empty);
        picture3Label.Label = String.Empty;
      }
      if (template.Photos[3].Enabled)
      {
        //picture4.Dispose();
        Picture picture4FileName;
        if (template.Photos[3].Image.Height > template.Photos[3].Image.Width)
        {
          picture4FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture4FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture4.Height = template.Photos[3].Image.Height;
        picture4.Width = template.Photos[3].Image.Width;
        picture4.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[3].Image.BorderLeft, template.Photos[3].Image.BorderRight, template.Photos[3].Image.BorderTop, template.Photos[3].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[3].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture4.SetPosition(template.Photos[3].Image.posX, template.Photos[3].Image.posY);
        picture4.SetFileName(picture4FileName.FilePath);
        picture4.ScaleToScreenResolution();
        picture4.Refresh();
        picture4Label.Label = picture4FileName.DateTaken.ToString("d");
        picture4Label.Height = template.Photos[3].Label.Height;
        picture4Label.Width = template.Photos[3].Label.Width;
        picture4Label.SetPosition(template.Photos[3].Label.posX, template.Photos[3].Label.posY);
        picture4Label.FontName = template.Photos[3].Label.Font;
        picture4Label.TextColor = ColorTranslator.FromHtml(template.Photos[3].Label.TextColor).ToArgb(); ;
        picture4Label.ScaleToScreenResolution();
      }
      else
      {
        picture4.SetFileName(String.Empty);
        picture4Label.Label = String.Empty;
      }
      if (template.Photos[4].Enabled)
      {
        //picture5.Dispose();
        Picture picture5FileName;
        if (template.Photos[4].Image.Height > template.Photos[4].Image.Width)
        {
          picture5FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture5FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture5.Height = template.Photos[4].Image.Height;
        picture5.Width = template.Photos[4].Image.Width;
        picture5.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[4].Image.BorderLeft, template.Photos[4].Image.BorderRight, template.Photos[4].Image.BorderTop, template.Photos[4].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[4].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture5.SetPosition(template.Photos[4].Image.posX, template.Photos[4].Image.posY);
        picture5.SetFileName(picture5FileName.FilePath);
        picture5.ScaleToScreenResolution();
        picture5.Refresh();
        picture5Label.Label = picture5FileName.DateTaken.ToString("d");
        picture5Label.Height = template.Photos[4].Label.Height;
        picture5Label.Width = template.Photos[4].Label.Width;
        picture5Label.SetPosition(template.Photos[4].Label.posX, template.Photos[4].Label.posY);
        picture5Label.FontName = template.Photos[4].Label.Font;
        picture5Label.TextColor = ColorTranslator.FromHtml(template.Photos[4].Label.TextColor).ToArgb(); ;
        picture5Label.ScaleToScreenResolution();
      }
      else
      {
        picture5.SetFileName(String.Empty);
        picture5Label.Label = String.Empty;
      }
      if (template.Photos[5].Enabled)
      {
        //picture6.Dispose();
        Picture picture6FileName;
        if (template.Photos[5].Image.Height > template.Photos[5].Image.Width)
        {
          picture6FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture6FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture6.Height = template.Photos[5].Image.Height;
        picture6.Width = template.Photos[5].Image.Width;
        picture6.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[5].Image.BorderLeft, template.Photos[5].Image.BorderRight, template.Photos[5].Image.BorderTop, template.Photos[5].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[5].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture6.SetPosition(template.Photos[5].Image.posX, template.Photos[5].Image.posY);
        picture6.SetFileName(picture6FileName.FilePath);
        picture6.ScaleToScreenResolution();
        picture6.Refresh();
        picture6Label.Label = picture6FileName.DateTaken.ToString("d");
        picture6Label.Height = template.Photos[5].Label.Height;
        picture6Label.Width = template.Photos[5].Label.Width;
        picture6Label.SetPosition(template.Photos[5].Label.posX, template.Photos[5].Label.posY);
        picture6Label.FontName = template.Photos[5].Label.Font;
        picture6Label.TextColor = ColorTranslator.FromHtml(template.Photos[5].Label.TextColor).ToArgb(); ;
        picture6Label.ScaleToScreenResolution();
      }
      else
      {
        picture6.SetFileName(String.Empty);
        picture6Label.Label = String.Empty;
      }
      if (template.Photos[6].Enabled)
      {
        //picture7.Dispose();
        Picture picture7FileName;
        if (template.Photos[6].Image.Height > template.Photos[6].Image.Width)
        {
          picture7FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture7FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture7.Height = template.Photos[6].Image.Height;
        picture7.Width = template.Photos[6].Image.Width;
        picture7.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[6].Image.BorderLeft, template.Photos[6].Image.BorderRight, template.Photos[6].Image.BorderTop, template.Photos[6].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[6].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture7.SetPosition(template.Photos[6].Image.posX, template.Photos[6].Image.posY);
        picture7.SetFileName(picture7FileName.FilePath);
        picture7.ScaleToScreenResolution();
        picture7.Refresh();
        picture7Label.Label = picture7FileName.DateTaken.ToString("d");
        picture7Label.Height = template.Photos[6].Label.Height;
        picture7Label.Width = template.Photos[6].Label.Width;
        picture7Label.SetPosition(template.Photos[6].Label.posX, template.Photos[6].Label.posY);
        picture7Label.FontName = template.Photos[6].Label.Font;
        picture7Label.TextColor = ColorTranslator.FromHtml(template.Photos[6].Label.TextColor).ToArgb(); ;
        picture7Label.ScaleToScreenResolution();
      }
      else
      {
        picture7.SetFileName(String.Empty);
        picture7Label.Label = String.Empty;
      }
      if (template.Photos[7].Enabled)
      {
        //picture8.Dispose();
        Picture picture8FileName;
        if (template.Photos[7].Image.Height > template.Photos[7].Image.Width)
        {
          picture8FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
        }
        else
        {
          picture8FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
        }
        picture8.Height = template.Photos[7].Image.Height;
        picture8.Width = template.Photos[7].Image.Width;
        picture8.SetBorder(String.Format("{0},{1},{2},{3}", template.Photos[7].Image.BorderLeft, template.Photos[7].Image.BorderRight, template.Photos[7].Image.BorderTop, template.Photos[7].Image.BorderBottom), GUIImage.BorderPosition.OutsideImage, false, true, template.Photos[7].Image.BorderPath, 0xFFFFFFFF, false, false);
        picture8.SetPosition(template.Photos[7].Image.posX, template.Photos[7].Image.posY);
        picture8.SetFileName(picture8FileName.FilePath);
        picture8.ScaleToScreenResolution();
        picture8.Refresh();
        picture8Label.Label = picture8FileName.DateTaken.ToString("d");
        picture8Label.Height = template.Photos[7].Label.Height;
        picture8Label.Width = template.Photos[7].Label.Width;
        picture8Label.SetPosition(template.Photos[7].Label.posX, template.Photos[7].Label.posY);
        picture8Label.FontName = template.Photos[7].Label.Font;
        picture8Label.TextColor = ColorTranslator.FromHtml(template.Photos[7].Label.TextColor).ToArgb(); ;
        picture8Label.ScaleToScreenResolution();
      }
      else
      {
        picture8.SetFileName(String.Empty);
        picture8Label.Label = String.Empty;
      }
      timer.Start();
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
