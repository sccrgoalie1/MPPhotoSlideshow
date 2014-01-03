using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MPPhotoSlideshowCommon
{
  public class DesignerCanvas : Canvas
  {
    //public IEnumerable<DesignerItem> SelectedItems
    //{
    //  get
    //  {
    //    var selectedItems = from item in this.Children.OfType<DesignerItem>()
    //                        where item.IsSelected == true
    //                        select item;

    //    return selectedItems;
    //  }
    //}

    //public void DeselectAll()
    //{
    //  foreach (DesignerItem item in this.SelectedItems)
    //  {
    //    item.IsSelected = false;
    //  }
    //}
    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Source == this)
      {
        //this.dragStartPoint = new Point?(e.GetPosition(this));
        //DesignerCanvas designer = UIHelper.FindVisualParent<DesignerCanvas>(this, "DesignerCanvasRoot");
        ListView itemsList = UIHelper.FindChild<ListView>(this, "ItemControl");
        foreach (DesignerItem item in UIHelper.FindVisualChildren<DesignerItem>(itemsList))
        {
          if (item.IsSelected)
          {
            item.IsSelected = false;
          }
        }
        //this.DeselectAll();
        e.Handled = true;
      }
    }
    //protected override Size MeasureOverride(Size constraint)
    //{
    //  Size size = new Size();
    //  foreach (UIElement element in base.Children)
    //  {
    //    double left = Canvas.GetLeft(element);
    //    double top = Canvas.GetTop(element);
    //    left = double.IsNaN(left) ? 0 : left;
    //    top = double.IsNaN(top) ? 0 : top;

    //    //measure desired size for each child
    //    element.Measure(constraint);

    //    Size desiredSize = element.DesiredSize;
    //    if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
    //    {
    //      size.Width = Math.Max(size.Width, left + desiredSize.Width);
    //      size.Height = Math.Max(size.Height, top + desiredSize.Height);
    //    }
    //  }
    //  //for aesthetic reasons add extra points
    //  size.Width += 10;
    //  size.Height += 10;
    //  return size;
    //}
  }
}
