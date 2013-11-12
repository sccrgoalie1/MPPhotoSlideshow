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
    /// <summary>
    /// Allows you to configure the Input Box Header Text and prompt text
    /// </summary>
    /// <param name="title">The header of the input box text</param>
    /// <param name="text">The text to be displayed above the text box</param>
    public void SetupInputBox(string title, string text)
    {
      this.Text = title;
      inputBoxLabel.Text = text;
      InputText = "";
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
