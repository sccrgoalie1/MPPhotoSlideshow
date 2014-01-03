using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MPPhotoSlideshowCommon
{   
  public class PhotoTemplate
  {
    /// <summary>
    /// The name of the template
    /// </summary>
    public string TemplateName { get; set; }
    /// <summary>
    /// Whether the template is enabled for use in the Slideshow
    /// </summary>
    public bool Enabled { get; set; }
    /// <summary>
    /// The unique Id of the template
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// The version of the template (used for updating templates)
    /// </summary>
    public ModuleVersion Version { get; set; }
    /// <summary>
    /// The list of photos to be displayed when using this template
    /// </summary>
    public BindingList<PhotoControl> Photos { get; set; }
    public PhotoTemplate(){}
  }
}
