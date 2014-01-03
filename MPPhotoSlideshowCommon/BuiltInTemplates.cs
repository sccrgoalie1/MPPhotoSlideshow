using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace MPPhotoSlideshowCommon
{
  public static class BuiltInTemplates
  {
    public static ModuleVersion BuiltInTemplateVersion = new ModuleVersion(1, 0, 0, 5);
    public static List<PhotoTemplate> BuiltInTemplatesList = new List<PhotoTemplate>();

    private static void BuildTemplates()
    {
      TwoTallOneWide();
      TwoWidePhotos();
      SevenPhotosOneTall();
      TwoTallPhotos();
      OneTallPhoto();
      OneWidePhoto();
      FourTallTwoWide();
      EightPhotosSixWideTwoTall();
      OnePanorama();
      OneSquareThreeWide();
    }

    /// <summary>
    /// Returns a bool as to whether there is a new template version available
    /// </summary>
    /// <param name="currentVersion">the current version of the templates on the system</param>
    /// <returns></returns>
    public static bool NewTemplatesAvailable(ModuleVersion currentVersion)
    {
      return BuiltInTemplateVersion > currentVersion;
    }
    /// <summary>
    /// Updates a Binding List of templates to the latest version
    /// </summary>
    /// <param name="exisitingTemplates">Your current template list to be updated</param>
    /// <returns>Returns an updated list of templates</returns>
    public static BindingList<PhotoTemplate> UpdateTemplates(BindingList<PhotoTemplate> exisitingTemplates)
    {
      BuildTemplates();
      BindingList<PhotoTemplate> updatedTemplates = new BindingList<PhotoTemplate>();
      foreach (PhotoTemplate template in exisitingTemplates)
      {
        PhotoTemplate latestTemplate = BuiltInTemplatesList.Find(t => t.Id == template.Id);
        //latest template will be null for any templates that are not built in
        if (latestTemplate != null)
        {
          if (template.Version < latestTemplate.Version)
          {
            updatedTemplates.Add(latestTemplate);
          }
          else
          {
            updatedTemplates.Add(template);
          }
        }
        else
        {
          updatedTemplates.Add(template);
        }
      }
      //add any new templates that don't currently exist
      foreach (PhotoTemplate template in BuiltInTemplatesList)
      {
        bool exists = false;
        foreach (PhotoTemplate uTemplate in updatedTemplates)
        {
          if (template.Id == uTemplate.Id)
          {
            exists = true;
            break;
          }
        }
        if (!exists)
        {
          updatedTemplates.Add(template);
        }
      }
      return updatedTemplates;
    }

    /// <summary>
    /// Updates a List of templates to the latest version
    /// </summary>
    /// <param name="exisitingTemplates">You current template list to be updated</param>
    /// <returns>Returns an updated list of templates</returns>
    public static List<PhotoTemplate> UpdateTemplates(List<PhotoTemplate> exisitingTemplates)
    {
      BuildTemplates();
      List<PhotoTemplate> updatedTemplates = new List<PhotoTemplate>();
      foreach (PhotoTemplate template in exisitingTemplates)
      {
        PhotoTemplate latestTemplate = BuiltInTemplatesList.Find(t => t.Id == template.Id);
        //latest template will be null for any templates that are not built in
        if (latestTemplate != null)
        {
          if (template.Version < latestTemplate.Version)
          {
            updatedTemplates.Add(latestTemplate);
          }
          else
          {
            updatedTemplates.Add(template);
          }
        }
        else
        {
          updatedTemplates.Add(template);
        }
      }
      //add any new templates that don't currently exist
      foreach (PhotoTemplate template in BuiltInTemplatesList)
      {
        bool exists = false;
        foreach (PhotoTemplate uTemplate in updatedTemplates)
        {
          if (template.Id == uTemplate.Id)
          {
            exists = true;
            break;
          }
        }
        if (!exists)
        {
          updatedTemplates.Add(template);
        }
      }
      return updatedTemplates;
    }

    private static void TwoTallOneWide()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX=38,
        posY=206,
        Width=500,
        Height=667,
        RotateAngle = -20,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 500,
        posX = 38,
        posY = 873
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 610,
        posY = 277,
        Width = 700,
        Height = 525,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 700,
        posX = 610,
        posY = 802
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = true,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 1382,
        posY = 206,
        Width = 500,
        Height = 667,
        RotateAngle = 20,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 500,
        posX = 1382,
        posY = 873
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = true,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = false,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = false,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id=new Guid("031F6947-BCC1-4199-8DF5-000455794758"),
        TemplateName = "TwoTallOneWide"
      });
    }

    private static void TwoWidePhotos()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 40,
        posY = 210,
        Width = 880,
        Height = 660,
        RotateAngle = -20,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 40,
        Width = 880,
        posX = 40,
        posY = 870
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 1000,
        posY = 210,
        Width = 880,
        Height = 660,
        RotateAngle = 20,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 880,
        posX = 210,
        posY = 870
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = true,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = false,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = false,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = false,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 1),
        Id = new Guid("37B9D988-A4B3-442C-A08B-27EC42A9A019"),
        TemplateName = "TwoWidePhotos"
      });
    }

    private static void SevenPhotosOneTall()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 150,
        posY = 45,
        Width = 400,
        Height = 300,
        RotateAngle = -15,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 150,
        posY = 345
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 150,
        posY = 390,
        Width = 400,
        Height = 300,
        RotateAngle = 15,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 150,
        posY = 690
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = true,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 150,
        posY = 735,
        Width = 400,
        Height = 300,
        RotateAngle = -15,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 150,
        posY = 1035
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = true,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 720,
        posY = 220,
        Width = 480,
        Height = 640,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 480,
        posX = 720,
        posY = 860
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = true,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 1370,
        posY = 45,
        Width = 400,
        Height = 300,
        RotateAngle = 15,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 1370,
        posY = 345
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = true,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 1370,
        posY = 390,
        Width = 400,
        Height = 300,
        RotateAngle = -15,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 1370,
        posY = 690
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = true,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 1370,
        posY = 735,
        Width = 400,
        Height = 300,
        RotateAngle = 15,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 1370,
        posY = 1035
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = true,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id = new Guid("8B6013B4-1293-4E86-85FE-F62AEBC74CD8"),
        TemplateName = "7Photos1Tall"
      });
    }

    private static void TwoTallPhotos()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 135,
        posY = 80,
        Width = 690,
        Height = 920,
        RotateAngle = -20,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 690,
        posX = 135,
        posY = 1000
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 1095,
        posY = 80,
        Width = 690,
        Height = 920,
        RotateAngle = 20,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 690,
        posX = 1095,
        posY = 1000
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = true,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = false,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = false,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = false,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id = new Guid("6BCD3422-4400-48D3-A406-AE89E0B70D2A"),
        TemplateName = "TwoTallPhotos"
      });
    }

    private static void OneTallPhoto()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 585,
        posY = 40,
        Width = 750,
        Height = 1000,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 750,
        posX = 585,
        posY = 1040
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 0,
        posY = 80,
        Width =0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = false,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = false,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = false,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = false,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id = new Guid("6300E660-989F-4AB7-B078-46C96DFB8D9E"),
        TemplateName = "OneTallPhoto"
      });
    }

    private static void OneWidePhoto()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 360,
        posY = 90,
        Width = 1200,
        Height = 900,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 50,
        Width = 1200,
        posX = 360,
        posY = 990
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 0,
        posY = 80,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = false,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = false,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = false,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = false,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id = new Guid("8293C3BE-BE75-4241-83D7-8E6C8F2ED34F"),
        TemplateName = "OneWidePhoto"
      });
    }

    private static void FourTallTwoWide()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 119,
        posY = 60,
        Width = 338,
        Height = 450,
        RotateAngle = -10,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 338,
        posX = 119,
        posY = 510
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 119,
        posY = 570,
        Width = 338,
        Height = 450,
        RotateAngle = 10,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 338,
        posX = 119,
        posY = 1020
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = true,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 616,
        posY = 40,
        Width = 640,
        Height = 480,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 640,
        posX = 616,
        posY = 520
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = true,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 616,
        posY = 560,
        Width = 640,
        Height = 480,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width =640,
        posX = 616,
        posY = 1040
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = true,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 1463,
        posY = 60,
        Width = 338,
        Height = 450,
        RotateAngle = 10,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 338,
        posX = 1463,
        posY = 510
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = true,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 1463,
        posY = 570,
        Width = 338,
        Height = 450,
        RotateAngle = -10,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 338,
        posX = 1463,
        posY = 1020
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = true,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id = new Guid("FE1A8512-8D59-416C-BD89-E29816755C0A"),
        TemplateName = "FourTallTwoWide"
      });
    }

    private static void EightPhotosSixWideTwoTall()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 120,
        posY = 120,
        Width = 480,
        Height = 360,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 480,
        posX = 120,
        posY = 480
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 720,
        posY = 120,
        Width = 480,
        Height = 360,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 480,
        posX = 720,
        posY = 480
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = true,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 1320,
        posY = 120,
        Width = 480,
        Height = 360,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 480,
        posX = 1320,
        posY = 480
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = true,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 500,
        posY = 400,
        Width = 210,
        Height = 280,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 210,
        posX = 500,
        posY = 680
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = false,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 1210,
        posY = 400,
        Width = 210,
        Height = 280,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 210,
        posX = 1210,
        posY = 680
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 120,
        posY = 600,
        Width = 480,
        Height = 360,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 480,
        posX = 120,
        posY = 960
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = true,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 720,
        posY = 600,
        Width = 480,
        Height = 360,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 480,
        posX = 720,
        posY = 960
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = true,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 1320,
        posY = 600,
        Width = 480,
        Height = 360,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 480,
        posX = 1320,
        posY = 960
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = true,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 1),
        Id = new Guid("31C47172-2FAC-4AEA-85E2-F50DD393E2AD"),
        TemplateName = "8Photos6Wide2Tall"
      });
    }

    private static void OnePanorama()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 60,
        posY = 180,
        Width = 1800,
        Height = 780,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 1800,
        posX = 60,
        posY = 960
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 0,
        posY = 80,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = false,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = false,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = false,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = false,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id = new Guid("C8B21038-BB43-433C-8348-E55E3A7B81C0"),
        TemplateName = "OnePanorama"
      });
    }

    private static void OneSquareThreeWide()
    {
      //Stores each of the photos for adding to the template
      BindingList<PhotoControl> photos = new BindingList<PhotoControl>();
      //The image
      ImageControl picture1 = new ImageControl()
      {
        posX = 147,
        posY = 40,
        Width = 1000,
        Height = 1000,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label1 = new LabelControl()
      {
        Font = "fontB12",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 40,
        Width = 1000,
        posX = 40,
        posY = 1040
      };
      //The entire photo
      PhotoControl photo1 = new PhotoControl()
      {
        Enabled = true,
        Image = picture1,
        Label = label1,
        PhotoName = "Picture1"
      };
      photos.Add(photo1);

      ImageControl picture2 = new ImageControl()
      {
        posX = 1294,
        posY = 45,
        Width = 400,
        Height = 300,
        RotateAngle = 10,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label2 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 1294,
        posY = 345
      };
      //The entire photo
      PhotoControl photo2 = new PhotoControl()
      {
        Enabled = true,
        Image = picture2,
        Label = label2,
        PhotoName = "Picture2"
      };
      photos.Add(photo2);

      ImageControl picture3 = new ImageControl()
      {
        posX = 1294,
        posY = 390,
        Width = 400,
        Height = 300,
        RotateAngle = -10,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label3 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 1294,
        posY = 435
      };
      //The entire photo
      PhotoControl photo3 = new PhotoControl()
      {
        Enabled = true,
        Image = picture3,
        Label = label3,
        PhotoName = "Picture3"
      };
      photos.Add(photo3);

      ImageControl picture4 = new ImageControl()
      {
        posX = 1294,
        posY = 735,
        Width = 400,
        Height = 300,
        RotateAngle = 10,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label4 = new LabelControl()
      {
        Font = "font10",
        FontSize = "12",
        TextColor = "#FFFFFF",
        Height = 20,
        Width = 400,
        posX = 1294,
        posY = 780
      };
      //The entire photo
      PhotoControl photo4 = new PhotoControl()
      {
        Enabled = true,
        Image = picture4,
        Label = label4,
        PhotoName = "Picture4"
      };
      photos.Add(photo4);

      ImageControl picture5 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label5 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo5 = new PhotoControl()
      {
        Enabled = false,
        Image = picture5,
        Label = label5,
        PhotoName = "Picture5"
      };
      photos.Add(photo5);

      ImageControl picture6 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label6 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo6 = new PhotoControl()
      {
        Enabled = false,
        Image = picture6,
        Label = label6,
        PhotoName = "Picture6"
      };
      photos.Add(photo6);

      ImageControl picture7 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label7 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo7 = new PhotoControl()
      {
        Enabled = false,
        Image = picture7,
        Label = label7,
        PhotoName = "Picture7"
      };
      photos.Add(photo7);

      ImageControl picture8 = new ImageControl()
      {
        posX = 0,
        posY = 0,
        Width = 0,
        Height = 0,
        RotateAngle = 0,
        BorderBottom = "0",
        BorderLeft = "0",
        BorderRight = "0",
        BorderPath = "mpslideshow_image_border.png",
        BorderTop = "0"
      };
      //The label
      LabelControl label8 = new LabelControl()
      {
        Font = "",
        FontSize = "",
        TextColor = "",
        Height = 0,
        Width = 0,
        posX = 0,
        posY = 0
      };
      //The entire photo
      PhotoControl photo8 = new PhotoControl()
      {
        Enabled = false,
        Image = picture8,
        Label = label8,
        PhotoName = "Picture8"
      };
      photos.Add(photo8);

      //Add all the photos to the template list
      BuiltInTemplatesList.Add(new PhotoTemplate()
      {
        Enabled = true,
        Photos = photos,
        Version = new ModuleVersion(1, 0, 0, 0),
        Id = new Guid("4BAA87E8-B661-4BF4-A3DD-D27C75A44369"),
        TemplateName = "OneSquareThreeWide"
      });
    }
  }
}
