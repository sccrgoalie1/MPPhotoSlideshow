using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MPPhotoSlideshowCommon
{
  [Serializable]
  public class Templates
  {
    /// <summary>
    /// The version of the template (used for updating templates)
    /// </summary>
    public ModuleVersion TemplateVersion { get; set; }
    /// <summary>
    /// The list of templates
    /// </summary>
    public BindingList<PhotoTemplate> TemplatesList { get; set; }
    public Templates() { }
  }
}
