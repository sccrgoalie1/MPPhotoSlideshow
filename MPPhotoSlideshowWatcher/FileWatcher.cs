using MPPhotoSlideshowCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MPPhotoSlideshowWatcher
{
  public class FileWatcher
  {
    private List<FileSystemWatcher> _watchers;
    private XMLSettings settings;
    public FileWatcher() { }
    public void Start()
    {
      try
      {
        Log.Init(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "FileWatcher", "log", LogType.Debug);
        _watchers = new List<FileSystemWatcher>();
        string pictureFolders = "";
        settings = new XMLSettings(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MPPhotoSlideshow\", "MPPhotoSlideshow2.xml");
          pictureFolders = settings.getXmlAttribute("FolderPaths","");
          //Int32.TryParse(xmlreader.GetValue("MyConfig", "Interval"), out interval);
          //if (xmlreader.GetValue("MyConfig", "LastLoadCache") != "")
          //{
          //  lastPictureLoad = Convert.ToDateTime(xmlreader.GetValue("MyConfig", "LastLoadCache"));
          //}
          //backgroundImageTextBox.Text = xmlreader.GetValue("MyConfig", "BackgroundPath");
        foreach (string pDir in pictureFolders.Split(','))
        {
          if (Directory.Exists(pDir))
          {
            Log.Debug("Starting to watch {0} for new or deleted photos",pDir);
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Filter = "*.*";// fw.WatchFilter;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            _watchers.Add(watcher);

            watcher.EnableRaisingEvents = false;

            //if (!Directory.Exists(pDir))
            //{
            //    throw new DirectoryNotFoundException("Watched Directory must exist");
            //}

            watcher.Path = pDir;
            // Begin watching.
            watcher.EnableRaisingEvents = true;
          }
          else
          {
            //WriteInfo("WatchedFolder does not exist: {0}", pDir);
          }
        }
      }
      catch (Exception ex)
      {
         Log.Error("MPPhotoSlideshowWatcher.Start() - Error {0}", ex.ToString());
      }
    }
    private void OnChanged(object source, FileSystemEventArgs e)
    {
      try
      {
        if (e.FullPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || e.FullPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || e.FullPath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
        {
          Log.Debug("Found a photo that was changed {0}, checking to see if it is already in the cache", e.FullPath);
          List<Picture> cache = LoadCache();
          IEnumerable<bool> found = cache.Select(t => t.FilePath == e.FullPath);
          if (found.Count() == 0)
          {
            Log.Debug("Did not find in cache.  Adding it");
            Picture pic = BuildNewPicture(e.FullPath);
            cache.Add(pic);
            WriteCache(cache);
          }
          else
          {
            Log.Debug("Found in cache.  Removing and readding");
            cache.RemoveAll(t => t.FilePath == e.FullPath);
            Picture pic = BuildNewPicture(e.FullPath);
            cache.Add(pic);
            WriteCache(cache);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Error("MPPhotoSlideshowWatcher.OnDeleted() - Error {0}", ex.ToString());
      }
    }
    private void OnCreated(object source, FileSystemEventArgs e)
    {
      try
      {
        if (e.FullPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || e.FullPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || e.FullPath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
        {
          Log.Debug("Found a new photo {0}, beginning process to add to cache", e.FullPath);
          List<Picture> cache = LoadCache();
          Picture pic = BuildNewPicture(e.FullPath);
          cache.Add(pic);
          WriteCache(cache);
        }
        else
        {
          Log.Debug("Found a new photo but did not match a photo extension {0}",e.FullPath);
        }
      }
      catch (Exception ex)
      {
        Log.Debug("MPPhotoSlideshowWatcher.OnCreated() - Error {0}", ex.ToString());
      }
    }
    private void OnDeleted(object source, FileSystemEventArgs e)
    {
      try
      {
        if (e.FullPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || e.FullPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || e.FullPath.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
        {
          if (!File.Exists(e.FullPath))
          {
            Log.Debug("Found a photo that was deleted {0}", e.FullPath);
            List<Picture> cache = LoadCache();
            cache.RemoveAll(t => t.FilePath == e.FullPath);
            WriteCache(cache);
          }
          else
          {
            Log.Debug("Noticed a deleted event but the file still exist so don't do anything {0}",e.FullPath);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Error("MPPhotoSlideshowWatcher.OnDeleted() - Error {0}", ex.ToString());
      }
    }
    private Picture BuildNewPicture(string filepath)
    {
      try
      {
        ExifMetadata exifMetaData = new ExifMetadata();
        ExifMetadata.Metadata metaData = exifMetaData.GetExifMetadata(filepath);
        DateTime pictureDate = new DateTime(1901, 1, 1);
        DateTime.TryParse(metaData.DatePictureTaken.DisplayValue, out pictureDate);
        int width = 0;
        int height = 0;
        //bool rotateFromExifOrientation = false;
        string[] res = metaData.ImageDimensions.DisplayValue.Split('x');
        Int32.TryParse(res[0], out width);
        Int32.TryParse(res[1], out height);
        double aspectratio = 0;
          if (width > height)
          {
            double value = (double)width / height;
            aspectratio = Math.Truncate(10 * (value)) / 10;
          }
          else
          {
            double value = (double)height / width;
            aspectratio = Math.Truncate(10 * (value)) / 10;
          }
        string orientation = metaData.Orientation.DisplayValue;
        bool flipHeightAndWidth = false;
        if (orientation != null)
        {
          if (orientation != "Normal")
          {
            switch (orientation)
            {
              case "Rotate 90":
                flipHeightAndWidth = true;
                break;
              case "Rotate 270":
                flipHeightAndWidth = true;
                break;
            }
          }
        }
        if (flipHeightAndWidth)
        {
          return new Picture() { FilePath = filepath, DateTaken = pictureDate, Height = width, Width = height, AspectRatio = Convert.ToString(aspectratio), ExifOrientation = orientation };
        }
        else
        {
          return new Picture() { FilePath = filepath, DateTaken = pictureDate, Height = height, Width = width, AspectRatio = Convert.ToString(aspectratio), ExifOrientation = orientation };
        }
      }
      catch (Exception ex)
      {
        Log.Error("MPPhotoSlideshowWatcher.BuildPicture() - Error {0}", ex.ToString());
        return new Picture();
      }
    }
    private List<Picture> LoadCache()
    {
      try
      {
        Log.Debug("Loading existing photos");
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal";
        if (File.Exists(folder + @"\MPSlideshowCache.xml"))
        {
          using (StreamReader streamReader = new StreamReader(folder + @"\MPSlideshowCache.xml"))
          {
            string stream = streamReader.ReadToEnd();
            if (stream.Length > 0)
            {
              return XMLHelper.Deserialize<List<Picture>>(stream);
            }
            streamReader.Close();
          }

        }
        return new List<Picture>();
      }
      catch (Exception ex)
      {
        Log.Error("MPPhotoSlideshowWatcher.LoadTemplates() - Error {0}", ex.ToString());
        return new List<Picture>();
      }
    }

    private void WriteCache(List<Picture> photoTemplates)
    {
      try
      {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Team MediaPortal\MediaPortal";
        File.Delete(folder + @"\MPSlideshowCache.xml");
        using (StreamWriter streamWriter = new StreamWriter(folder + @"\MPSlideshowCache.xml"))
        {
          string serialized = XMLHelper.Serialize<List<Picture>>(photoTemplates);
          streamWriter.Write(serialized);
          streamWriter.Close();
        }
        Log.Debug("Finished writing cache");
      }
      catch (Exception ex)
      {
        Log.Error("MPPhotoSlideshowWatcher.WriteTemplates() - Error {0}", ex.ToString());
      }
    }
    public void Stop()
    {

      try
      {
        Log.Debug("The service is stopping");
        foreach (FileSystemWatcher watcher in _watchers)
        {
          watcher.EnableRaisingEvents = false;
          watcher.Dispose();
        }
      }
      catch (Exception ex)
      {
       Log.Error("MPPhotoSlideshowWatcher.Stop() - Error {0}", ex.ToString());
      }
    }
  }
}
