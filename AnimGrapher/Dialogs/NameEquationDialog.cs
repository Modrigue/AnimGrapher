using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnimGrapher
{
    public partial class NameEquationDialog : Form
    {
        public NameEquationDialog(string name)
        {
            InitializeComponent();

            //buttonOk.Enabled = false; // always enabled
            if (name != "<New>")
                textboxName.Text = name;

            textboxName.Focus();
        }

        /// <summary>
        /// Get name from text box
        /// </summary>
        public string GetName()
        {
            return textboxName.Text;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void textboxName_TextChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = (textboxName.Text.Length > 0);
        }

        private void textboxName_KeyDown(object sender, KeyEventArgs e)
        {
            // no newline at Enter
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }
    }
}
