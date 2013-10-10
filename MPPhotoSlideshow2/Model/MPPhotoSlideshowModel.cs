#region Copyright (C) 2007-2013 Team MediaPortal

/*
    Copyright (C) 2007-2013 Team MediaPortal
    http://www.team-mediaportal.com

    This file is part of MediaPortal 2

    MediaPortal 2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    MediaPortal 2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with MediaPortal 2. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

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
    #region Picture1
    protected readonly AbstractProperty _helloStringProperty;
    protected readonly AbstractProperty _picture1Property;
    protected readonly AbstractProperty _picture1PosX;
    protected readonly AbstractProperty _picture1PosY;
    protected readonly AbstractProperty _picture1Date;
    protected readonly AbstractProperty _picture1DateColor;
    protected readonly AbstractProperty _picture1Width;
    protected readonly AbstractProperty _picture1Height;
    #endregion

    #region Picture2
    protected readonly AbstractProperty _picture2Property;
    protected readonly AbstractProperty _picture2PosX;
    protected readonly AbstractProperty _picture2PosY;
    protected readonly AbstractProperty _picture2Date;
    protected readonly AbstractProperty _picture2DateColor;
    protected readonly AbstractProperty _picture2Width;
    protected readonly AbstractProperty _picture2Height;
    #endregion

    #region Picture3
    protected readonly AbstractProperty _picture3Property;
    protected readonly AbstractProperty _picture3PosX;
    protected readonly AbstractProperty _picture3PosY;
    protected readonly AbstractProperty _picture3Date;
    protected readonly AbstractProperty _picture3DateColor;
    protected readonly AbstractProperty _picture3Width;
    protected readonly AbstractProperty _picture3Height;
    #endregion

    #region Picture4
    protected readonly AbstractProperty _picture4Property;
    protected readonly AbstractProperty _picture4PosX;
    protected readonly AbstractProperty _picture4PosY;
    protected readonly AbstractProperty _picture4Date;
    protected readonly AbstractProperty _picture4DateColor;
    protected readonly AbstractProperty _picture4Width;
    protected readonly AbstractProperty _picture4Height;
    #endregion

    #region Picture5
    protected readonly AbstractProperty _picture5Property;
    protected readonly AbstractProperty _picture5PosX;
    protected readonly AbstractProperty _picture5PosY;
    protected readonly AbstractProperty _picture5Date;
    protected readonly AbstractProperty _picture5DateColor;
    protected readonly AbstractProperty _picture5Width;
    protected readonly AbstractProperty _picture5Height;
    #endregion

    #region Picture6
    protected readonly AbstractProperty _picture6Property;
    protected readonly AbstractProperty _picture6PosX;
    protected readonly AbstractProperty _picture6PosY;
    protected readonly AbstractProperty _picture6Date;
    protected readonly AbstractProperty _picture6DateColor;
    protected readonly AbstractProperty _picture6Width;
    protected readonly AbstractProperty _picture6Height;
    #endregion

    #region Picture7
    protected readonly AbstractProperty _picture7Property;
    protected readonly AbstractProperty _picture7PosX;
    protected readonly AbstractProperty _picture7PosY;
    protected readonly AbstractProperty _picture7Date;
    protected readonly AbstractProperty _picture7DateColor;
    protected readonly AbstractProperty _picture7Width;
    protected readonly AbstractProperty _picture7Height;
    #endregion

    #region Picture8
    protected readonly AbstractProperty _picture8Property;
    protected readonly AbstractProperty _picture8PosX;
    protected readonly AbstractProperty _picture8PosY;
    protected readonly AbstractProperty _picture8Date;
    protected readonly AbstractProperty _picture8DateColor;
    protected readonly AbstractProperty _picture8Width;
    protected readonly AbstractProperty _picture8Height;
    #endregion

    #endregion

    #region private variables
    private static int templateWidth = 1920;
    private static int templateHeight=1080;
    private string backgroundImagePath = "";
    private int timerInterval = 10000;
    private XMLSettings settings;
    private Timer timer = new Timer();
    private Random _verticalRnd = new Random();
    private Random _horizontalRnd = new Random();
    private Random _templateRnd = new Random();
    private List<Picture> _verticalPictures = new List<Picture>();
    private List<Picture> _horizontalPictures = new List<Picture>();
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
        _helloStringProperty = new WProperty(typeof(string), HELLOWORLD_RESOURCE);
        _picture1Property = new WProperty(typeof(object), null);
        _picture1Date = new WProperty(typeof(string), null);
        _picture1PosX = new WProperty(typeof(object), null);
        _picture1PosY = new WProperty(typeof(object), null);
        _picture1DateColor = new WProperty(typeof(object), null);
        _picture1Width = new WProperty(typeof(object), null);
        _picture1Height = new WProperty(typeof(object), null);

        _picture2Property = new WProperty(typeof(object), null);
        _picture2Date = new WProperty(typeof(string), null);
        _picture2PosX = new WProperty(typeof(object), null);
        _picture2PosY = new WProperty(typeof(object), null);
        _picture2DateColor = new WProperty(typeof(object), null);
        _picture2Width = new WProperty(typeof(object), null);
        _picture2Height = new WProperty(typeof(object), null);

        _picture3Property = new WProperty(typeof(object), null);
        _picture3Date = new WProperty(typeof(string), null);
        _picture3PosX = new WProperty(typeof(object), null);
        _picture3PosY = new WProperty(typeof(object), null);
        _picture3DateColor = new WProperty(typeof(object), null);
        _picture3Width = new WProperty(typeof(object), null);
        _picture3Height = new WProperty(typeof(object), null);

        _picture4Property = new WProperty(typeof(object), null);
        _picture4Date = new WProperty(typeof(string), null);
        _picture4PosX = new WProperty(typeof(object), null);
        _picture4PosY = new WProperty(typeof(object), null);
        _picture4DateColor = new WProperty(typeof(object), null);
        _picture4Width = new WProperty(typeof(object), null);
        _picture4Height = new WProperty(typeof(object), null);

        _picture5Property = new WProperty(typeof(object), null);
        _picture5Date = new WProperty(typeof(string), null);
        _picture5PosX = new WProperty(typeof(object), null);
        _picture5PosY = new WProperty(typeof(object), null);
        _picture5DateColor = new WProperty(typeof(object), null);
        _picture5Width = new WProperty(typeof(object), null);
        _picture5Height = new WProperty(typeof(object), null);

        _picture6Property = new WProperty(typeof(object), null);
        _picture6Date = new WProperty(typeof(string), null);
        _picture6PosX = new WProperty(typeof(object), null);
        _picture6PosY = new WProperty(typeof(object), null);
        _picture6DateColor = new WProperty(typeof(object), null);
        _picture6Width = new WProperty(typeof(object), null);
        _picture6Height = new WProperty(typeof(object), null);

        _picture7Property = new WProperty(typeof(object), null);
        _picture7Date = new WProperty(typeof(string), null);
        _picture7PosX = new WProperty(typeof(object), null);
        _picture7PosY = new WProperty(typeof(object), null);
        _picture7DateColor = new WProperty(typeof(object), null);
        _picture7Width = new WProperty(typeof(object), null);
        _picture7Height = new WProperty(typeof(object), null);

        _picture8Property = new WProperty(typeof(object), null);
        _picture8Date = new WProperty(typeof(string), null);
        _picture8PosX = new WProperty(typeof(object), null);
        _picture8PosY = new WProperty(typeof(object), null);
        _picture8DateColor = new WProperty(typeof(object), null);
        _picture8Width = new WProperty(typeof(object), null);
        _picture8Height = new WProperty(typeof(object), null);
        LoadSettings();
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
    public string HelloString
    {
      get { return (string) _helloStringProperty.GetValue(); }
      set { _helloStringProperty.SetValue(value); }
    }
    #region Picture1
    public string Picture1Date
    {
      get { return (string)_picture1Date.GetValue(); }
      set { _picture1Date.SetValue(value); }
    }
    public object Picture1
    {
      get { return (object)_picture1Property.GetValue(); }
      set { _picture1Property.SetValue(value); }
    }
    public object Picture1PosX
    {
      get { return (object)_picture1PosX.GetValue(); }
      set { _picture1PosX.SetValue(value); }
    }
    public object Picture1PosY
    {
      get { return (object)_picture1PosY.GetValue(); }
      set { _picture1PosY.SetValue(value); }
    }
    public object Picture1DateColor
    {
      get { return (object)_picture1DateColor.GetValue(); }
      set { _picture1DateColor.SetValue(value); }
    }
    public object Picture1Height
    {
      get { return (object)_picture1Height.GetValue(); }
      set { _picture1Height.SetValue(value); }
    }
    public object Picture1Width
    {
      get { return (object)_picture1Width.GetValue(); }
      set { _picture1Width.SetValue(value); }
    }
    #endregion

    #region Picture2
    public string Picture2Date
    {
      get { return (string)_picture2Date.GetValue(); }
      set { _picture2Date.SetValue(value); }
    }
    public object Picture2
    {
      get { return (object)_picture2Property.GetValue(); }
      set { _picture2Property.SetValue(value); }
    }
    public object Picture2PosX
    {
      get { return (object)_picture2PosX.GetValue(); }
      set { _picture2PosX.SetValue(value); }
    }
    public object Picture2PosY
    {
      get { return (object)_picture2PosY.GetValue(); }
      set { _picture2PosY.SetValue(value); }
    }
    public object Picture2DateColor
    {
      get { return (object)_picture2DateColor.GetValue(); }
      set { _picture2DateColor.SetValue(value); }
    }
    public object Picture2Height
    {
      get { return (object)_picture2Height.GetValue(); }
      set { _picture2Height.SetValue(value); }
    }
    public object Picture2Width
    {
      get { return (object)_picture2Width.GetValue(); }
      set { _picture2Width.SetValue(value); }
    }
    #endregion

    #region Picture3
    public string Picture3Date
    {
      get { return (string)_picture3Date.GetValue(); }
      set { _picture3Date.SetValue(value); }
    }
    public object Picture3
    {
      get { return (object)_picture3Property.GetValue(); }
      set { _picture3Property.SetValue(value); }
    }
    public object Picture3PosX
    {
      get { return (object)_picture3PosX.GetValue(); }
      set { _picture3PosX.SetValue(value); }
    }
    public object Picture3PosY
    {
      get { return (object)_picture3PosY.GetValue(); }
      set { _picture3PosY.SetValue(value); }
    }
    public object Picture3DateColor
    {
      get { return (object)_picture3DateColor.GetValue(); }
      set { _picture3DateColor.SetValue(value); }
    }
    public object Picture3Height
    {
      get { return (object)_picture3Height.GetValue(); }
      set { _picture3Height.SetValue(value); }
    }
    public object Picture3Width
    {
      get { return (object)_picture3Width.GetValue(); }
      set { _picture3Width.SetValue(value); }
    }
    #endregion

    #region Picture4
    public string Picture4Date
    {
      get { return (string)_picture4Date.GetValue(); }
      set { _picture4Date.SetValue(value); }
    }
    public object Picture4
    {
      get { return (object)_picture4Property.GetValue(); }
      set { _picture4Property.SetValue(value); }
    }
    public object Picture4PosX
    {
      get { return (object)_picture4PosX.GetValue(); }
      set { _picture4PosX.SetValue(value); }
    }
    public object Picture4PosY
    {
      get { return (object)_picture4PosY.GetValue(); }
      set { _picture4PosY.SetValue(value); }
    }
    public object Picture4DateColor
    {
      get { return (object)_picture4DateColor.GetValue(); }
      set { _picture4DateColor.SetValue(value); }
    }
    public object Picture4Height
    {
      get { return (object)_picture4Height.GetValue(); }
      set { _picture4Height.SetValue(value); }
    }
    public object Picture4Width
    {
      get { return (object)_picture4Width.GetValue(); }
      set { _picture4Width.SetValue(value); }
    }
    #endregion

    #region Picture5
    public string Picture5Date
    {
      get { return (string)_picture5Date.GetValue(); }
      set { _picture5Date.SetValue(value); }
    }
    public object Picture5
    {
      get { return (object)_picture5Property.GetValue(); }
      set { _picture5Property.SetValue(value); }
    }
    public object Picture5PosX
    {
      get { return (object)_picture5PosX.GetValue(); }
      set { _picture5PosX.SetValue(value); }
    }
    public object Picture5PosY
    {
      get { return (object)_picture5PosY.GetValue(); }
      set { _picture5PosY.SetValue(value); }
    }
    public object Picture5DateColor
    {
      get { return (object)_picture5DateColor.GetValue(); }
      set { _picture5DateColor.SetValue(value); }
    }
    public object Picture5Height
    {
      get { return (object)_picture5Height.GetValue(); }
      set { _picture5Height.SetValue(value); }
    }
    public object Picture5Width
    {
      get { return (object)_picture5Width.GetValue(); }
      set { _picture5Width.SetValue(value); }
    }
    #endregion

    #region Picture6
    public string Picture6Date
    {
      get { return (string)_picture6Date.GetValue(); }
      set { _picture6Date.SetValue(value); }
    }
    public object Picture6
    {
      get { return (object)_picture6Property.GetValue(); }
      set { _picture6Property.SetValue(value); }
    }
    public object Picture6PosX
    {
      get { return (object)_picture6PosX.GetValue(); }
      set { _picture6PosX.SetValue(value); }
    }
    public object Picture6PosY
    {
      get { return (object)_picture6PosY.GetValue(); }
      set { _picture6PosY.SetValue(value); }
    }
    public object Picture6DateColor
    {
      get { return (object)_picture6DateColor.GetValue(); }
      set { _picture6DateColor.SetValue(value); }
    }
    public object Picture6Height
    {
      get { return (object)_picture6Height.GetValue(); }
      set { _picture6Height.SetValue(value); }
    }
    public object Picture6Width
    {
      get { return (object)_picture6Width.GetValue(); }
      set { _picture6Width.SetValue(value); }
    }
    #endregion

    #region Picture7
    public string Picture7Date
    {
      get { return (string)_picture7Date.GetValue(); }
      set { _picture7Date.SetValue(value); }
    }
    public object Picture7
    {
      get { return (object)_picture7Property.GetValue(); }
      set { _picture7Property.SetValue(value); }
    }
    public object Picture7PosX
    {
      get { return (object)_picture7PosX.GetValue(); }
      set { _picture7PosX.SetValue(value); }
    }
    public object Picture7PosY
    {
      get { return (object)_picture7PosY.GetValue(); }
      set { _picture7PosY.SetValue(value); }
    }
    public object Picture7DateColor
    {
      get { return (object)_picture7DateColor.GetValue(); }
      set { _picture7DateColor.SetValue(value); }
    }
    public object Picture7Height
    {
      get { return (object)_picture7Height.GetValue(); }
      set { _picture7Height.SetValue(value); }
    }
    public object Picture7Width
    {
      get { return (object)_picture7Width.GetValue(); }
      set { _picture7Width.SetValue(value); }
    }
    #endregion

    #region Picture8
    public string Picture8Date
    {
      get { return (string)_picture8Date.GetValue(); }
      set { _picture8Date.SetValue(value); }
    }
    public object Picture8
    {
      get { return (object)_picture8Property.GetValue(); }
      set { _picture8Property.SetValue(value); }
    }
    public object Picture8PosX
    {
      get { return (object)_picture8PosX.GetValue(); }
      set { _picture8PosX.SetValue(value); }
    }
    public object Picture8PosY
    {
      get { return (object)_picture8PosY.GetValue(); }
      set { _picture8PosY.SetValue(value); }
    }
    public object Picture8DateColor
    {
      get { return (object)_picture8DateColor.GetValue(); }
      set { _picture8DateColor.SetValue(value); }
    }
    public object Picture8Height
    {
      get { return (object)_picture8Height.GetValue(); }
      set { _picture8Height.SetValue(value); }
    }
    public object Picture8Width
    {
      get { return (object)_picture8Width.GetValue(); }
      set { _picture8Width.SetValue(value); }
    }
    #endregion

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
    public AbstractProperty HelloStringProperty
    {
      get { return _helloStringProperty; }
    }
    #region Picture1
    public AbstractProperty Picture1Property
    {
      get { return _picture1Property; }
    }
    public AbstractProperty Picture1DateProperty
    {
      get { return _picture1Date; }
    }
    public AbstractProperty Picture1PosXProperty
    {
      get { return _picture1PosX; }
    }
    public AbstractProperty Picture1PosYProperty
    {
      get { return _picture1PosY; }
    }
    public AbstractProperty Picture1DateColorProperty
    {
      get { return _picture1DateColor; }
    }
    public AbstractProperty Picture1HeightProperty
    {
      get { return _picture1Height; }
    }
    public AbstractProperty Picture1WidthProperty
    {
      get { return _picture1Width; }
    }
    #endregion

    #region Picture2
    public AbstractProperty Picture2Property
    {
      get { return _picture2Property; }
    }
    public AbstractProperty Picture2DateProperty
    {
      get { return _picture2Date; }
    }
    public AbstractProperty Picture2PosXProperty
    {
      get { return _picture2PosX; }
    }
    public AbstractProperty Picture2PosYProperty
    {
      get { return _picture2PosY; }
    }
    public AbstractProperty Picture2DateColorProperty
    {
      get { return _picture2DateColor; }
    }
    public AbstractProperty Picture2HeightProperty
    {
      get { return _picture2Height; }
    }
    public AbstractProperty Picture2WidthProperty
    {
      get { return _picture2Width; }
    }
    #endregion

    #region Picture3
    public AbstractProperty Picture3Property
    {
      get { return _picture3Property; }
    }
    public AbstractProperty Picture3DateProperty
    {
      get { return _picture3Date; }
    }
    public AbstractProperty Picture3PosXProperty
    {
      get { return _picture3PosX; }
    }
    public AbstractProperty Picture3PosYProperty
    {
      get { return _picture3PosY; }
    }
    public AbstractProperty Picture3DateColorProperty
    {
      get { return _picture3DateColor; }
    }
    public AbstractProperty Picture3HeightProperty
    {
      get { return _picture3Height; }
    }
    public AbstractProperty Picture3WidthProperty
    {
      get { return _picture3Width; }
    }
    #endregion

    #region Picture4
    public AbstractProperty Picture4Property
    {
      get { return _picture4Property; }
    }
    public AbstractProperty Picture4DateProperty
    {
      get { return _picture4Date; }
    }
    public AbstractProperty Picture4PosXProperty
    {
      get { return _picture4PosX; }
    }
    public AbstractProperty Picture4PosYProperty
    {
      get { return _picture4PosY; }
    }
    public AbstractProperty Picture4DateColorProperty
    {
      get { return _picture4DateColor; }
    }
    public AbstractProperty Picture4HeightProperty
    {
      get { return _picture4Height; }
    }
    public AbstractProperty Picture4WidthProperty
    {
      get { return _picture4Width; }
    }
    #endregion

    #region Picture5
    public AbstractProperty Picture5Property
    {
      get { return _picture5Property; }
    }
    public AbstractProperty Picture5DateProperty
    {
      get { return _picture5Date; }
    }
    public AbstractProperty Picture5PosXProperty
    {
      get { return _picture5PosX; }
    }
    public AbstractProperty Picture5PosYProperty
    {
      get { return _picture5PosY; }
    }
    public AbstractProperty Picture5DateColorProperty
    {
      get { return _picture5DateColor; }
    }
    public AbstractProperty Picture5HeightProperty
    {
      get { return _picture5Height; }
    }
    public AbstractProperty Picture5WidthProperty
    {
      get { return _picture5Width; }
    }
    #endregion

    #region Picture6
    public AbstractProperty Picture6Property
    {
      get { return _picture6Property; }
    }
    public AbstractProperty Picture6DateProperty
    {
      get { return _picture6Date; }
    }
    public AbstractProperty Picture6PosXProperty
    {
      get { return _picture6PosX; }
    }
    public AbstractProperty Picture6PosYProperty
    {
      get { return _picture6PosY; }
    }
    public AbstractProperty Picture6DateColorProperty
    {
      get { return _picture6DateColor; }
    }
    public AbstractProperty Picture6HeightProperty
    {
      get { return _picture6Height; }
    }
    public AbstractProperty Picture6WidthProperty
    {
      get { return _picture6Width; }
    }
    #endregion

    #region Picture7
    public AbstractProperty Picture7Property
    {
      get { return _picture7Property; }
    }
    public AbstractProperty Picture7DateProperty
    {
      get { return _picture7Date; }
    }
    public AbstractProperty Picture7PosXProperty
    {
      get { return _picture7PosX; }
    }
    public AbstractProperty Picture7PosYProperty
    {
      get { return _picture7PosY; }
    }
    public AbstractProperty Picture7DateColorProperty
    {
      get { return _picture7DateColor; }
    }
    public AbstractProperty Picture7HeightProperty
    {
      get { return _picture7Height; }
    }
    public AbstractProperty Picture7WidthProperty
    {
      get { return _picture7Width; }
    }
    #endregion

    #region Picture8
    public AbstractProperty Picture8Property
    {
      get { return _picture8Property; }
    }
    public AbstractProperty Picture8DateProperty
    {
      get { return _picture8Date; }
    }
    public AbstractProperty Picture8PosXProperty
    {
      get { return _picture8PosX; }
    }
    public AbstractProperty Picture8PosYProperty
    {
      get { return _picture8PosY; }
    }
    public AbstractProperty Picture8DateColorProperty
    {
      get { return _picture8DateColor; }
    }
    public AbstractProperty Picture8HeightProperty
    {
      get { return _picture8Height; }
    }
    public AbstractProperty Picture8WidthProperty
    {
      get { return _picture8Width; }
    }
    #endregion

    #endregion
    /// <summary>
    /// Method which will be called from our screen. We will change the value of our HelloWorld string here.
    /// </summary>
    public void ChangeHelloWorldString()
    {
      // Localized resources in the form [Section.Name] can be used in each Label in screens. Labels automatically
      // request the localized string from the system if a text of that form is written into their Content property.
      HelloString = COMMAND_TRIGGERED_RESOURCE;
    }

    #endregion

    #region Private members
    private void LoadSettings()
    {
      try
      {
        settings = new XMLSettings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MPPhotoSlideshow2.xml");
        Int32.TryParse(settings.getXmlAttribute("Interval","10000"), out timerInterval);
        backgroundImagePath = settings.getXmlAttribute("BackgroundPath");
        List<Picture> _allPictures = new List<Picture>();
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
          Log.Debug("MPSlideshow - Just finished loading horizontal and vertical pictures");
          timer.Elapsed += new ElapsedEventHandler(timer_Tick);
          timer.Interval = timerInterval;
          timer.Start();
          Log.Debug("MPSlideshow.OnPageLoad() - Pictures count {0}", _allPictures.Count);
        }
        else
        {
          //ServiceRegistration.Get<IScreenManager>().ShowDialog(
        }
        _allPictures = null;
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
        PhotoTemplate template = _photoTemplates[_templateRnd.Next(_photoTemplates.Count)];
        if (template.Photos[0].Enabled)
        {
          Picture picture1FileName;
          if (template.Photos[0].Image.Height > template.Photos[0].Image.Width)
          {
            picture1FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture1FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture1FileName.FilePath), LogInfoType.Debug);
          Picture1 = picture1FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[0].Image.posY,template.Photos[0].Image.posX);
          Picture1PosX = ImageMargin.Width;
          Picture1PosY = ImageMargin.Height;
          //Picture1GridMargin = String.Format("{0},{1},{2},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture1Date = picture1FileName.DateTaken.ToString("d");
          Picture1DateColor = template.Photos[0].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[0].Image.Height, template.Photos[0].Image.Width);
          Picture1Width = ImageSize.Width;
          Picture1Height = ImageSize.Height;
        }
        else
        {
          Picture1=String.Empty;
          Picture1Date = String.Empty;
          Picture1Width = 0;
          Picture1Height = 0;
        }
        if (template.Photos[1].Enabled)
        {
          Picture picture2FileName;
          if (template.Photos[1].Image.Height > template.Photos[1].Image.Width)
          {
            picture2FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture2FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture2FileName.FilePath), LogInfoType.Debug);
          Picture2 = picture2FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[1].Image.posY, template.Photos[1].Image.posX);
          Picture2PosX = ImageMargin.Width;
          Picture2PosY = ImageMargin.Height;
          //Picture1GridMargin = String.Format("{0},{1},{2},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture2Date = picture2FileName.DateTaken.ToString("d");
          Picture2DateColor = template.Photos[1].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[1].Image.Height, template.Photos[1].Image.Width);
          Picture2Width = ImageSize.Width;
          Picture2Height = ImageSize.Height;
        }
        else
        {
          Picture2 = String.Empty;
          Picture2Date = String.Empty;
          Picture2Width = 0;
          Picture2Height = 0;
        }
        if (template.Photos[2].Enabled)
        {
          Picture picture3FileName;
          if (template.Photos[2].Image.Height > template.Photos[2].Image.Width)
          {
            picture3FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture3FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture3FileName.FilePath), LogInfoType.Debug);
          Picture3 = picture3FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[2].Image.posY, template.Photos[2].Image.posX);
          Picture3PosX = ImageMargin.Width;
          Picture3PosY = ImageMargin.Height;
          //Picture2GridMargin = String.Format("{0},{2},{3},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture3Date = picture3FileName.DateTaken.ToString("d");
          Picture3DateColor = template.Photos[2].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[2].Image.Height, template.Photos[2].Image.Width);
          Picture3Width = ImageSize.Width;
          Picture3Height = ImageSize.Height;
        }
        else
        {
          Picture3 = String.Empty;
          Picture3Date = String.Empty;
          Picture3Width = 0;
          Picture3Height = 0;
        }
        if (template.Photos[3].Enabled)
        {
          Picture picture4FileName;
          if (template.Photos[3].Image.Height > template.Photos[3].Image.Width)
          {
            picture4FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture4FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture4FileName.FilePath), LogInfoType.Debug);
          Picture4 = picture4FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[3].Image.posY, template.Photos[3].Image.posX);
          Picture4PosX = ImageMargin.Width;
          Picture4PosY = ImageMargin.Height;
          //Picture3GridMargin = String.Format("{0},{3},{4},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture4Date = picture4FileName.DateTaken.ToString("d");
          Picture4DateColor = template.Photos[3].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[3].Image.Height, template.Photos[3].Image.Width);
          Picture4Width = ImageSize.Width;
          Picture4Height = ImageSize.Height;
        }
        else
        {
          Picture4 = String.Empty;
          Picture4Date = String.Empty;
          Picture4Width = 0;
          Picture4Height = 0;
        }
        if (template.Photos[4].Enabled)
        {
          Picture picture5FileName;
          if (template.Photos[4].Image.Height > template.Photos[4].Image.Width)
          {
            picture5FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture5FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture5FileName.FilePath), LogInfoType.Debug);
          Picture5 = picture5FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[4].Image.posY, template.Photos[4].Image.posX);
          Picture5PosX = ImageMargin.Width;
          Picture5PosY = ImageMargin.Height;
          //Picture4GridMargin = String.Format("{0},{4},{5},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture5Date = picture5FileName.DateTaken.ToString("d");
          Picture5DateColor = template.Photos[4].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[4].Image.Height, template.Photos[4].Image.Width);
          Picture5Width = ImageSize.Width;
          Picture5Height = ImageSize.Height;
        }
        else
        {
          Picture5 = String.Empty;
          Picture5Date = String.Empty;
          Picture5Width = 0;
          Picture5Height = 0;
        }
        if (template.Photos[5].Enabled)
        {
          Picture picture6FileName;
          if (template.Photos[5].Image.Height > template.Photos[5].Image.Width)
          {
            picture6FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture6FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture6FileName.FilePath), LogInfoType.Debug);
          Picture6 = picture6FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[5].Image.posY, template.Photos[5].Image.posX);
          Picture6PosX = ImageMargin.Width;
          Picture6PosY = ImageMargin.Height;
          //Picture5GridMargin = String.Format("{0},{5},{6},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture6Date = picture6FileName.DateTaken.ToString("d");
          Picture6DateColor = template.Photos[5].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[5].Image.Height, template.Photos[5].Image.Width);
          Picture6Width = ImageSize.Width;
          Picture6Height = ImageSize.Height;
        }
        else
        {
          Picture6 = String.Empty;
          Picture6Date = String.Empty;
          Picture6Width = 0;
          Picture6Height = 0;
        }
        if (template.Photos[6].Enabled)
        {
          Picture picture7FileName;
          if (template.Photos[6].Image.Height > template.Photos[6].Image.Width)
          {
            picture7FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture7FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture7FileName.FilePath), LogInfoType.Debug);
          Picture7 = picture7FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[6].Image.posY, template.Photos[6].Image.posX);
          Picture7PosX = ImageMargin.Width;
          Picture7PosY = ImageMargin.Height;
          //Picture6GridMargin = String.Format("{0},{6},{7},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture7Date = picture7FileName.DateTaken.ToString("d");
          Picture7DateColor = template.Photos[6].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[6].Image.Height, template.Photos[6].Image.Width);
          Picture7Width = ImageSize.Width;
          Picture7Height = ImageSize.Height;
        }
        else
        {
          Picture7 = String.Empty;
          Picture7Date = String.Empty;
          Picture7Width = 0;
          Picture7Height = 0;
        }
        if (template.Photos[7].Enabled)
        {
          Picture picture8FileName;
          if (template.Photos[7].Image.Height > template.Photos[7].Image.Width)
          {
            picture8FileName = _verticalPictures[_verticalRnd.Next(_verticalPictures.Count)];
          }
          else
          {
            picture8FileName = _horizontalPictures[_horizontalRnd.Next(_horizontalPictures.Count)];
          }
          //log.writeLog(String.Format("MPSlideshow.LoadPage() - Setting new file name {0}",picture8FileName.FilePath), LogInfoType.Debug);
          Picture8 = picture8FileName.FilePath;
          Size ImageMargin = ScaleToScreen(template.Photos[7].Image.posY, template.Photos[7].Image.posX);
          Picture8PosX = ImageMargin.Width;
          Picture8PosY = ImageMargin.Height;
          //Picture7GridMargin = String.Format("{0},{7},{8},{3}", ImageMargin.Width, ImageMargin.Height, 0, 0);
          Picture8Date = picture8FileName.DateTaken.ToString("d");
          Picture8DateColor = template.Photos[7].Label.TextColor;
          Size ImageSize = ScaleToScreen(template.Photos[7].Image.Height, template.Photos[7].Image.Width);
          Picture8Width = ImageSize.Width;
          Picture8Height = ImageSize.Height;
        }
        else
        {
          Picture8 = String.Empty;
          Picture8Date = String.Empty;
          Picture8Width = 0;
          Picture8Height = 0;
        }
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
    private Size ScaleToScreen(int height, int width)
    {
      double screenheight = 720;//System.Windows.SystemParameters.WorkArea.Height;
      double screenwidth = 1280;//System.Windows.SystemParameters.WorkArea.Width;
      double heightMultiplier = screenheight/templateHeight;
      double widthMultiplier = screenwidth / templateWidth;
      int imageHeight = Convert.ToInt32(height * heightMultiplier);
      int imageWidth = Convert.ToInt32(width * widthMultiplier);
      //log.writeLog(String.Format("MPPhotoSlideshow.ScaleToScreen() - Incoming height {0}, width {1}, screen height {2}, screen width {3}, outgoing height {4}, outgoing width {5}",height,width,screenheight,screenwidth,imageHeight,imageWidth), LogInfoType.Debug);
      return new Size(imageWidth, imageHeight);

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
