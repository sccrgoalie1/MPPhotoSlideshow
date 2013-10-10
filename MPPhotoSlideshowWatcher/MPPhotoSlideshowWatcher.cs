using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MPPhotoSlideshowWatcher
{
  public partial class MPPhotoSlideshowWatcher : ServiceBase
  {
    private FileWatcher fw;
    public MPPhotoSlideshowWatcher()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      fw = new FileWatcher();
      fw.Start();
    }

    protected override void OnStop()
    {
      fw.Stop();
    }
  }
}
