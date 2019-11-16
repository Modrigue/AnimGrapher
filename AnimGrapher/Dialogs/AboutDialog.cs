using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AnimGrapher
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();

            labelVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void AboutDialog_Load(object sender, System.EventArgs e)
        {
            // add links to link labels

            LinkLabel.Link link1 = new LinkLabel.Link();
            link1.LinkData = "http://www.aleprojects.com/en/doc/parser";
            linkLabel1.Links.Add(link1);

            LinkLabel.Link link2 = new LinkLabel.Link();
            link2.LinkData = "http://www.flaticon.com/authors/pixel-buddha";
            linkLabel2.Links.Add(link2);

            LinkLabel.Link link3 = new LinkLabel.Link();
            link3.LinkData = "https://www.youtube.com/user/mburdis";
            linkLabel3.Links.Add(link3);

            LinkLabel.Link link4 = new LinkLabel.Link();
            link4.LinkData = "http://www.idius.net/about-ppp/";
            linkLabel4.Links.Add(link4);

            LinkLabel.Link link5 = new LinkLabel.Link();
            link5.LinkData = "https://fr.pinterest.com/peneloped63/";
            linkLabel5.Links.Add(link5);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // send the URL to the operating system
            Process.Start(e.Link.LinkData as string);
        }
    }
}
