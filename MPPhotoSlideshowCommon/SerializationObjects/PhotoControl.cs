using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPPhotoSlideshowCommon
{
  public class PhotoControl
  {
    public string PhotoName { get; set; }
    public bool Enabled { get; set; }
    public ImageControl Image { get; set; }
    public LabelControl Label { get; set; }
    public PhotoControl() { }
  }
}
