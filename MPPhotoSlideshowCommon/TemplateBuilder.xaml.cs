using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MPPhotoSlideshowCommon
{
  /// <summary>
  /// Interaction logic for TemplateBuilder.xaml
  /// </summary>
  public partial class TemplateBuilder : Window
  {
    private PhotoTemplate _template;
    public ObservableCollection<BindingPicture> PictureList = new ObservableCollection<BindingPicture>();

    public TemplateBuilder(PhotoTemplate template)
    {
      InitializeComponent();
      _template = template;
      ItemControl.ItemsSource = PictureList;
      BuildTemplate(720, 1280);
    }

    private void BuildTemplate(int windowheight, int windowwidth)
    {
      PictureList.Clear();
      for (int i = 0; i < _template.Photos.Count; i++)
      {
        if (_template.Photos[i].Enabled)
        {
          Picture pictureFileName = TemplateBuilderHelper.GetPicture(_template, i);
          Log.Debug(pictureFileName.FilePath);
          Size ImageMargin = TemplateBuilderHelper.ScaleToScreen(_template.Photos[i].Image.posY,
            _template.Photos[i].Image.posX, windowheight, windowwidth);
          Size ImageSize = TemplateBuilderHelper.ScaleToScreen(_template.Photos[i].Image.Height,
            _template.Photos[i].Image.Width, windowheight, windowwidth);
          int rotateAngle = _template.Photos[i].Image.RotateAngle;
          //string borderImage = _template.Photos[i].Image.BorderPath;
          string borderImage =
            @"C:\Program Files (x86)\Team MediaPortal\MP2-Client\Plugins\MPPhotoSlideshow2\Skin\default\images\mpslideshow_image_border.png";
          //if (_template.Photos[i].Image.BorderLeft == "0" & _template.Photos[i].Image.BorderTop == "0" &
          //    _template.Photos[i].Image.BorderRight == "0" & _template.Photos[i].Image.BorderBottom == "0")
          //{
          //  borderImage = "";
          //  //Log.Debug("Setting border image equal to the empty string");
          //}

          PictureList.Add(new BindingPicture()
          {

            BorderImage = borderImage,
            PictureBorderMargins =
              String.Format("{0},{1},{2},{3}", _template.Photos[i].Image.BorderLeft, _template.Photos[i].Image.BorderTop,
                _template.Photos[i].Image.BorderRight, _template.Photos[i].Image.BorderBottom),
            Left = Convert.ToInt32(ImageMargin.Width),
            Top = Convert.ToInt32(ImageMargin.Height),
            PictureDate = pictureFileName.DateTaken.ToString("d"),
            PictureDateColor = _template.Photos[i].Label.TextColor,
            PictureHeight = Convert.ToInt32(ImageSize.Height),
            PictureWidth = Convert.ToInt32(ImageSize.Width),
            PictureImage = pictureFileName.FilePath,
            PictureDateFontSize = _template.Photos[i].Label.FontSize,
            RotateAngle = rotateAngle,
            RotateX = Convert.ToInt32(ImageSize.Width/2),
            RotateY = Convert.ToInt32(ImageSize.Height/2)
          });
        }
      }
    }


    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      BuildTemplate(Convert.ToInt32(this.ActualHeight), Convert.ToInt32(this.ActualWidth));
    }

  }

  public class BindingPicture
  {
    public int PictureWidth { get; set; }
    public int PictureHeight { get; set; }
    public int Top { get; set; }
    public int Left { get; set; }
    public int RotateX { get; set; }
    public int RotateY { get; set; }
    public int RotateAngle { get; set; }
    public string BorderImage { get; set; }
    public string PictureBorderMargins { get; set; }
    public string PictureDate { get; set; }
    public string PictureDateColor { get; set; }
    public string PictureDateFontSize { get; set; }
    public string PictureImage { get; set; }
  }
}
