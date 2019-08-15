using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessForm
{
    public partial class Name_prompt : Form
    {
        public Name_prompt()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public string textBoxText
        {
            get
            {
                return name_tbx.Text;
            }
        }
    }
}
