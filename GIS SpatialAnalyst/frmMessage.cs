using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Surveying
{
    public partial class frmMessage : Form
    {
        protected string m_Info = "";
        public string Info
        {
            set { m_Info = value; }
        }

        public frmMessage()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmMessage_Load);
        }

        void frmMessage_Load(object sender, EventArgs e)
        {
           
            this.TopMost = true;
            txtInfo.Text = m_Info;
        }
    }
}
