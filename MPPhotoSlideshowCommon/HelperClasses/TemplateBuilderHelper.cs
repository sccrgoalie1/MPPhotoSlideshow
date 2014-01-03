using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace MPPhotoSlideshowCommon
{
  public static class TemplateBuilderHelper
  {
    private static List<Picture> _vertical4x3Pictures = new List<Picture>();
    private static List<Picture> _horizontal4x3Pictures = new List<Picture>();
    private static List<Picture> _verticalPanoramasPictures = new List<Picture>();
    private static List<Picture> _horizontalPanoramasPictures = new List<Picture>();
    private static List<Picture> _verticalSquarePictures = new List<Picture>();
    private static List<Picture> _horizontalSquarePictures = new List<Picture>();
    private static List<Picture> _vertical16x9Pictures = new List<Picture>();
    private static List<Picture> _horizontal16x9Pictures = new List<Picture>();
    private static Random _vertical4x3Rnd = new Random();
    private static Random _horizontal4x3Rnd = new Random();
    private static Random _verticalPanoramasRnd = new Random();
    private static Random _horizontalPanoramasRnd = new Random();
    private static Random _verticalSquareRnd = new Random();
    private static Random _horizontalSquareRnd = new Random();
    private static Random _vertical16x9Rnd = new Random();
    private static Random _horizontal16x9Rnd = new Random();
    private static bool _initialized = false;
    private static int templateWidth = 1920;
    private static int templateHeight = 1080;

    public static Picture GetPicture(PhotoTemplate template, int index)
    {
      Init();
      Picture imageFileName = new Picture();

      try
      {
        if (template.Photos[index].Image.Height > template.Photos[index].Image.Width)
        {
          double value = (double)template.Photos[index].Image.Height / template.Photos[index].Image.Width;
          double aspectratio = Math.Truncate(10 * (value)) / 10;
          if (aspectratio == 1)
          {
            if (_verticalSquarePictures.Count > 0)
            {
              imageFileName = _verticalSquarePictures[_verticalSquareRnd.Next(_verticalSquarePictures.Count)];
            }
          }
          else if (1.3 <= aspectratio & aspectratio <= 1.5)
          {
            if (_vertical4x3Pictures.Count > 0)
            {
              imageFileName = _vertical4x3Pictures[_vertical4x3Rnd.Next(_vertical4x3Pictures.Count)];
            }
          }
          else if (1.5 < aspectratio & aspectratio < 2)
          {
            if (_vertical16x9Pictures.Count > 0)
            {
              imageFileName = _vertical16x9Pictures[_vertical16x9Rnd.Next(_vertical16x9Pictures.Count)];
            }
          }
          else if (aspectratio >= 2)
          {
            if (_verticalPanoramasPictures.Count > 0)
            {
              imageFileName = _verticalPanoramasPictures[_verticalPanoramasRnd.Next(_verticalPanoramasPictures.Count)];
            }
          }
        }
        else
        {
          double value = (double)template.Photos[index].Image.Width / template.Photos[index].Image.Height;
          double aspectratio = Math.Truncate(10 * (value)) / 10;
          if (aspectratio == 1)
          {
            if (_horizontalSquarePictures.Count > 0)
            {
              imageFileName = _horizontalSquarePictures[_horizontalSquareRnd.Next(_horizontalSquarePictures.Count)];
            }
          }
          else if (1.3 <= aspectratio & aspectratio <= 1.5)
          {
            if (_horizontal4x3Pictures.Count > 0)
            {
              imageFileName = _horizontal4x3Pictures[_horizontal4x3Rnd.Next(_horizontal4x3Pictures.Count)];
            }
          }
          else if (1.5 < aspectratio & aspectratio < 2)
          {
            if (_horizontal16x9Pictures.Count > 0)
            {
              imageFileName = _horizontal16x9Pictures[_horizontal16x9Rnd.Next(_horizontal16x9Pictures.Count)];
            }
          }
          else if (aspectratio >= 2)
          {
            if (_horizontalPanoramasPictures.Count > 0)
            {
              imageFileName = _horizontalPanoramasPictures[_horizontalPanoramasRnd.Next(_horizontalPanoramasPictures.Count)];
            }
          }
        }
        return imageFileName;
      }
      catch (Exception ex)
      {
        Log.Error(ex.ToString());
        return imageFileName;
      }

    }

    public static Size ScaleToScreen(int height, int width, double screenheight = 720, double screenwidth = 1280)
    {
      Init();
      double heightMultiplier = screenheight / templateHeight;
      double widthMultiplier = screenwidth / templateWidth;
      int imageHeight = Convert.ToInt32(height * heightMultiplier);
      int imageWidth = Convert.ToInt32(width * widthMultiplier);
      //log.writeLog(String.Format("MPPhotoSlideshow.ScaleToScreen() - Incoming height {0}, width {1}, screen height {2}, screen width {3}, outgoing height {4}, outgoing width {5}",height,width,screenheight,screenwidth,imageHeight,imageWidth), LogInfoType.Debug);
      return new Size(imageWidth, imageHeight);

    }

    public static void Init()
    {
      if (!_initialized)
      {
        _initialized = true;
        List<Picture> allPictures = new List<Picture>();
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow";
        if (File.Exists(folder + @"\MPSlideshowCache.xml"))
        {
          using (StreamReader streamReader = new StreamReader(folder + @"\MPSlideshowCache.xml"))
          {
            string stream = streamReader.ReadToEnd();
            if (stream.Length > 0)
            {
              allPictures = XMLHelper.Deserialize<List<Picture>>(stream);
            }
            streamReader.Close();
          }

        }
        if (allPictures.Count > 0)
        {
          foreach (Picture picture in allPictures)
          {
            double aspectratio = 0;
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
          allPictures = null;
        }
      }
    }
  }
}
