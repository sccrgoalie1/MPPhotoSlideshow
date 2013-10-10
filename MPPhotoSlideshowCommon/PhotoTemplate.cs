using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MPPhotoSlideshowCommon
{   
  public class PhotoTemplate
  {
    public string TemplateName { get; set; }
    public BindingList<PhotoControl> Photos { get; set; }
    public PhotoTemplate(){}
  }
}
