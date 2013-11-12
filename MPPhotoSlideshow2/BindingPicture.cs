using MediaPortal.Common.General;
using MediaPortal.UI.Presentation.DataObjects;
using System;
using System.Linq;
using System.Text;

namespace MPPhotoSlideshow
{
  public class BindingPicture : ListItem
  {
    protected readonly AbstractProperty _left = new WProperty(typeof(int), 0);
    protected readonly AbstractProperty _top = new WProperty(typeof(int), 0);
    protected readonly AbstractProperty _pictureImage = new WProperty(typeof(string), string.Empty);
    protected readonly AbstractProperty _pictureWidth = new WProperty(typeof(int), 0);
    protected readonly AbstractProperty _pictureHeight = new WProperty(typeof(int), 0);
    protected readonly AbstractProperty _rotateAngle = new WProperty(typeof(int), 0);
    protected readonly AbstractProperty _rotateX = new WProperty(typeof(int), 0);
    protected readonly AbstractProperty _rotateY = new WProperty(typeof(int), 0);
    protected readonly AbstractProperty _borderImage = new WProperty(typeof(string), string.Empty);
    protected readonly AbstractProperty _pictureBorderMargins = new WProperty(typeof(string), string.Empty);
    protected readonly AbstractProperty _pictureDate = new WProperty(typeof(string), string.Empty);
    protected readonly AbstractProperty _pictureDateColor = new WProperty(typeof(string), string.Empty);
    protected readonly AbstractProperty _pictureDateFontSize = new WProperty(typeof(string), string.Empty); 

    // Public properties
    public int Left
    {
      get { return (int)_left.GetValue(); }
      set { _left.SetValue(value); }
    }
    public int Top
    {
      get { return (int)_top.GetValue(); }
      set { _top.SetValue(value); }
    }
    public int PictureWidth
    {
      get { return (int)_pictureWidth.GetValue(); }
      set { _pictureWidth.SetValue(value); }
    }
    public int PictureHeight
    {
      get { return (int)_pictureHeight.GetValue(); }
      set { _pictureHeight.SetValue(value); }
    }
    public int RotateX
    {
      get { return (int)_rotateX.GetValue(); }
      set { _rotateX.SetValue(value); }
    }
    public int RotateY
    {
      get { return (int)_rotateY.GetValue(); }
      set { _rotateY.SetValue(value); }
    }
    public int RotateAngle
    {
      get { return (int)_rotateAngle.GetValue(); }
      set { _rotateAngle.SetValue(value); }
    }
    public string BorderImage
    {
      get { return (string)_borderImage.GetValue(); }
      set { _borderImage.SetValue(value); }
    }
    public string PictureBorderMargins
    {
      get { return (string)_pictureBorderMargins.GetValue(); }
      set { _pictureBorderMargins.SetValue(value); }
    }
    public string PictureDate
    {
      get { return (string)_pictureDate.GetValue(); }
      set { _pictureDate.SetValue(value); }
    }
    public string PictureDateColor
    {
      get { return (string)_pictureDateColor.GetValue(); }
      set { _pictureDateColor.SetValue(value); }
    }
    public string PictureDateFontSize
    {
      get { return (string)_pictureDateFontSize.GetValue(); }
      set { _pictureDateFontSize.SetValue(value); }
    }
    public string PictureImage
    {
      get { return (string)_pictureImage.GetValue(); }
      set { _pictureImage.SetValue(value); }
    }
    public AbstractProperty PictureWidthProperty
    {
      get { return _pictureWidth; }
    }
    public AbstractProperty PictureHeightProperty
    {
      get { return _pictureHeight; }
    }
    public AbstractProperty BorderImageProperty
    {
      get { return _borderImage; }
    }
    public AbstractProperty PictureBorderMarginsProperty
    {
      get { return _pictureBorderMargins; }
    }
    public AbstractProperty PictureDateProperty
    {
      get { return _pictureDate; }
    }
    public AbstractProperty PictureDateColorProperty
    {
      get { return _pictureDateColor; }
    }
    public AbstractProperty PictureDateFontSizeProperty
    {
      get { return _pictureDateFontSize; }
    }
    public AbstractProperty PictureImageProperty
    {
      get { return _pictureImage; }
    }
    public AbstractProperty TopProperty
    {
      get { return _top; }
    }
    public AbstractProperty LeftProperty
    {
      get { return _left; }
    }
    public AbstractProperty RotateAngleProperty
    {
      get { return _rotateAngle; }
    }
    public AbstractProperty RotateXProperty
    {
      get { return _rotateX; }
    }
    public AbstractProperty RotateYProperty
    {
      get { return _rotateY; }
    }
  }
}
