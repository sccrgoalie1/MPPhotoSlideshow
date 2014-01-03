using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPPhotoSlideshowCommon
{
  public class ImageControl
  {
    public int posX { get; set; }
    public int posY { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int RotateAngle { get; set; }
    public string BorderLeft { get; set; }
    public string BorderRight { get; set; }
    public string BorderTop { get; set; }
    public string BorderBottom { get; set; }
    public string BorderPath { get; set; }
    public ImageControl() { }
  }
}
