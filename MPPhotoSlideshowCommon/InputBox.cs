using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPPhotoSlideshowCommon
{
  public partial class InputBox : Form
  {
    public string InputText
    {
      get
      {
        return inputTextBox.Text;
      }
      set
      {
        inputTextBox.Text = value;
      }
    }
    public InputBox()
    {
      InitializeComponent();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.DialogResult = DialogResult.OK;
      }
    }
  }
}
