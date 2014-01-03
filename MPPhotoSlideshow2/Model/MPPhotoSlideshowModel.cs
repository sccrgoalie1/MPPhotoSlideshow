using MediaPortal.Common.General;
using MediaPortal.UI.Presentation.Models;
using MediaPortal.UI.Presentation.DataObjects;
using MediaPortal.UI.Presentation.Workflow;
using MediaPortal.UI.Presentation.SkinResources;
using System.Collections.Generic;
using System;
using MPPhotoSlideshowCommon;
using System.IO;
using System.Timers;
using System.Windows;
using MediaPortal.Common;
using MediaPortal.UI.Presentation.Screens;

namespace MPPhotoSlideshow.Models
{
  /// <summary>
  /// Example for a simple model.
  /// The screenfile to this model is located at:
  /// /Skins/default/screens/hello_world.xaml
  /// </summary>
  /// <remarks>
  /// <para>
  /// Models are used for providing data from the system to the skin and for executing actions (commands)
  /// which are triggered by the Skin, for example by clicking a button.
  /// </para>
  /// <para>
  /// All public properties can be data-bound by the skin, for example the <see cref="HelloString"/> property.
  /// Note that properties, which are updated by the model and whose new value should be propagated to the
  /// skin, must be backed by an instance of <see cref="AbstractProperty"/>. That instance must be made available
  /// to the skin engine by publishing it under the same name as the actual property plus "Property", see for example
  /// <see cref="HelloStringProperty"/>. In models, always <see cref="WProperty"/> instances are used.
  /// </para>
  /// <para>
  /// You can also consider to implement the interface <see cref="IWorkflowModel"/>, which makes it
  /// possible to attend the screenflow = workflow of the user session. When that interface is implemented and this
  /// model is registered in a workflow state as backing workflow model, the model will get notifications when the
  /// GUI navigation switches to or away from its workflow state.
  /// </para>
  /// <para>
  /// To make an UI model known by the system (and thus loadable by the skin), it is necessary to register it
  /// in the <c>plugin.xml</c> file.
  /// </para>
  /// </remarks>
  public class MPPhotoSlideshowModel : IWorkflowModel
  {
    #region Consts
    public const string MODEL_ID_STR = "C6B0FA5A-288E-405A-A5D5-AA66CC0AA327";
    /// <summary>
    /// This is a localized string resource. Localized string resources always look like this:
    /// <example>
    /// [Section.Name]
    /// </example>
    /// Localized resources must be present at least in the english language, as this is the default.
    /// In the english language file of this hello world plugin, you'll find the translation of this string.
    /// The language file is located at: /Language/strings_en.xml
    /// </summary>
    protected const string HELLOWORLD_RESOURCE = "[MPPhotoSlideshow.HelloWorldText]";

    /// <summary>
    /// Another localized string resource.
    /// </summary>
    protected const string COMMAND_TRIGGERED_RESOURCE = "[MPPhotoSlideshow.ButtonTextCommandExecuted]";

    #endregion

    #region Protected properties

    /// <summary>
    /// This property holds a string that we will modify in this tutorial.
    /// </summary>
    ////protected readonly AbstractProperty _helloStringProperty;
    protected readonly AbstractProperty _slideshowBackgroundProperty;
    //protected readonly AbstractProperty _pictureListProperty;
    protected readonly ItemsList _pictureList = new ItemsList();   

    #endregion

    #region private variables

    private bool exifRotate = false;
    private ModuleVersion currentTemplateVersion = new ModuleVersion(1,0,0,0);

    private int timerInterval = 10000;
    private XMLSettings settings;
    private Timer timer = new Timer();

    private Random _templateRnd = new Random();
  
    private List<PhotoTemplate> _photoTemplates = new List<PhotoTemplate>();
    #endregion

    #region Ctor & maintainance

    /// <summary>
    /// Constructor... this one is called by the WorkflowManager when this model is loaded due to a screen reference.
    /// </summary>
    public MPPhotoSlideshowModel()
    {
      try
      {
        // In models, properties will always be WProperty instances. When using SProperties for screen databinding,
        // the system might run into memory leaks.
        Log.Init(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MP2Log", "log", LogType.Debug);
        ////_helloStringProperty = new WProperty(typeof(string), HELLOWORLD_RESOURCE);
        _slideshowBackgroundProperty = new WProperty(typeof(object), null);
        LoadSettings();

        //if there are no photos tell the user to run setup and exit
        //IDialogManager dialogManager = ServiceRegistration.Get<IDialogManager>();
        //dialogManager.ShowDialog("No Photo Cache","No photo cache built, please run the setup tool before launching the pluging",DialogType.OkDialog,false,null);
        if (BuiltInTemplates.NewTemplatesAvailable(currentTemplateVersion))
        {
          _photoTemplates = BuiltInTemplates.UpdateTemplates(_photoTemplates);
        }
        LoadPage();
      }
      catch (Exception ex)
      {
         Log.Error(ex.ToString());
      }
    }

    #endregion

    #region Public members
    #region Public Properties for changing Values
    /// <summary>
    /// This sample property will be accessed by the hello_world screen. Note that the data type must be the same
    /// as given in the instantiation of our backing property <see cref="_helloStringProperty"/>.
    /// </summary>
    ////public string HelloString
    ////{
    ////  get { return (string) _helloStringProperty.GetValue(); }
    ////  set { _helloStringProperty.SetValue(value); }
    ////}
    public string SlideshowBackground
    {
      get { return (string)_slideshowBackgroundProperty.GetValue(); }
      set { _slideshowBackgroundProperty.SetValue(value); }
    }
    /// <summary>
    /// Exposes the picture list to the skin
    /// </summary>
    public ItemsList PictureList
    {
      get { return _pictureList; }
    } 

    #endregion
    #region Abstract Properties for XAML Binding
    /// <summary>
    /// This is the dependency property for our sample string. It is needed to propagate changes to the skin.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the screen databinds to the <see cref="HelloString"/> property in a binding mode which will propagate data
    /// changes from the model to the skin (OneWay, TwoWay), the SkinEngine will attach a change handler to this property
    /// and react to changes.
    /// </para>
    /// <para>
    /// In other words: For each property <c>Xyz</c>, which should be able to be attached to, there must be an
    /// <see cref="AbstractProperty"/> with name <c>XyzProperty</c>.
    /// Only if <c>XyzProperty</c> is present in the model, value changes can be propagated to the skin.
    /// </para>
    /// </remarks>
    //public AbstractProperty HelloStringProperty
    //{
    //  get { return _helloStringProperty; }
    //}
    public AbstractProperty SlideshowBackgroundProperty
    {
      get { return _slideshowBackgroundProperty; }
    }   

    #endregion
    /// <summary>
    /// Method which will be called from our screen. We will change the value of our HelloWorld string here.
    /// </summary>
    //public void ChangeHelloWorldString()
    //{
    //  // Localized resources in the form [Section.Name] can be used in each Label in screens. Labels automatically
    //  // request the localized string from the system if a text of that form is written into their Content property.
    //  HelloString = COMMAND_TRIGGERED_RESOURCE;
    //}

    #endregion

    #region Private members
    private void LoadSettings()
    {
      try
      {
        settings = new XMLSettings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MPPhotoSlideshow2.xml");
        Int32.TryParse(settings.getXmlAttribute("Interval","10000"), out timerInterval);
        SlideshowBackground= settings.getXmlAttribute("BackgroundPath");
        exifRotate = Convert.ToBoolean(settings.getXmlAttribute("EXIFRotate", "false"));
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow";
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
        //}
        
          //_horizontalPictures = _allPictures.Where(s => s.Width > s.Height).ToList<Picture>();
          //_verticalPictures = _allPictures.Where(s => s.Width < s.Height).ToList<Picture>();
          //_allPictures = null;
          TemplateBuilderHelper.Init();
          Log.Debug("MPSlideshow - Just finished loading horizontal and vertical pictures");
          timer.Elapsed += new ElapsedEventHandler(timer_Tick);
          timer.Interval = timerInterval;
          timer.Start();
          //Log.Debug("MPSlideshow.OnPageLoad() - Pictures count {0}", _allPictures.Count);
        
   
      }
      catch (Exception ex)
      {
        Log.Error(ex.ToString());
      }
    }
    private void LoadPage()
    {
      try
      {
       timer.Stop();
       PictureList.Clear();
        PhotoTemplate template = _photoTemplates[_templateRnd.Next(_photoTemplates.Count)];
        for (int i = 0; i < template.Photos.Count; i++)
        {
          if (template.Photos[i].Enabled)
          {
            Picture pictureFileName = TemplateBuilderHelper.GetPicture(template, i);
            Log.Debug(pictureFileName.FilePath);
            Size ImageMargin = TemplateBuilderHelper.ScaleToScreen(template.Photos[i].Image.posY, template.Photos[i].Image.posX);
            Size ImageSize = TemplateBuilderHelper.ScaleToScreen(template.Photos[i].Image.Height, template.Photos[i].Image.Width);
            int rotateAngle = template.Photos[i].Image.RotateAngle;
            //only rotate photos based on exif info if requested
            if (exifRotate)
            {
              if (pictureFileName.ExifOrientation != "Normal")
              {
                switch (pictureFileName.ExifOrientation)
                {
                  case "Rotate 180":
                    rotateAngle = rotateAngle + 180;
                    break;
                  case "Flip Horizontal":
                    rotateAngle = rotateAngle + 180;
                    break;
                  case "Rotate 90":
                    rotateAngle = rotateAngle - 90;
                    break;
                  case "Flip Vertical":
                    rotateAngle = rotateAngle - 270;
                    break;
                  case "Rotate 270":
                    rotateAngle = rotateAngle + 180;
                    break;
                }
              }
            }
            string borderImage = template.Photos[i].Image.BorderPath;
            if (template.Photos[i].Image.BorderLeft == "0" & template.Photos[i].Image.BorderTop == "0" &
                template.Photos[i].Image.BorderRight == "0" & template.Photos[i].Image.BorderBottom == "0")
            {
              borderImage = "";
              //Log.Debug("Setting border image equal to the empty string");
            }

            PictureList.Add(new BindingPicture()
            {

              BorderImage = borderImage,
              PictureBorderMargins = String.Format("{0},{1},{2},{3}", template.Photos[i].Image.BorderLeft, template.Photos[i].Image.BorderTop, template.Photos[i].Image.BorderRight, template.Photos[i].Image.BorderBottom),
              Left = Convert.ToInt32(ImageMargin.Width),
              Top = Convert.ToInt32(ImageMargin.Height),
              PictureDate = pictureFileName.DateTaken.ToString("d"),
              PictureDateColor = template.Photos[i].Label.TextColor,
              PictureHeight = Convert.ToInt32(ImageSize.Height),
              PictureWidth = Convert.ToInt32(ImageSize.Width),
              PictureImage = pictureFileName.FilePath,
              PictureDateFontSize = template.Photos[i].Label.FontSize,
              RotateAngle=rotateAngle,
              RotateX = Convert.ToInt32(ImageSize.Width / 2),
              RotateY = Convert.ToInt32(ImageSize.Height / 2)
            });
          }
        }
        Log.Debug("Firing picture list changed with count of {0}", PictureList.Count);
        PictureList.FireChange();        
        timer.Start();
      }
      catch (Exception ex)
      {
        Log.Error(ex.ToString());
      }
    }
    

    private void timer_Tick(Object sender, EventArgs e)
    {
      //log.writeLog("MPSlideshow.timer_Tick() - Timer passed loading new page",LogInfoType.Debug); 
      LoadPage();
    }

    #endregion

    #region IWorkflowModel implementation

    public Guid ModelId
    {
      get { return new Guid(MODEL_ID_STR); }
    }

    public bool CanEnterState(NavigationContext oldContext, NavigationContext newContext)
    {
      return true;
    }

    public void EnterModelContext(NavigationContext oldContext, NavigationContext newContext)
    {
      // We could initialize some data here when entering the media navigation state
    }

    public void ExitModelContext(NavigationContext oldContext, NavigationContext newContext)
    {
      // We could dispose some data here when exiting media navigation context
    }

    public void ChangeModelContext(NavigationContext oldContext, NavigationContext newContext, bool push)
    {
      // We could initialize some data here when changing the media navigation state
    }

    public void Deactivate(NavigationContext oldContext, NavigationContext newContext)
    {
    }

    public void Reactivate(NavigationContext oldContext, NavigationContext newContext)
    {
    }

    public void UpdateMenuActions(NavigationContext context, IDictionary<Guid, WorkflowAction> actions)
    {
    }

    public ScreenUpdateMode UpdateScreen(NavigationContext context, ref string screen)
    {
      return ScreenUpdateMode.AutoWorkflowManager;
    }

    #endregion
  }
}
