using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPPhotoSlideshowCommon
{
  public class Picture
  {
    public string FilePath { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string AspectRatio { get; set; }
    public string ExifOrientation { get; set; }
    public DateTime DateTaken { get; set; }
    public Picture()
    {

    }
  }
}
