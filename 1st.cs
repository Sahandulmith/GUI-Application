using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Practicle_cw
{
    public partial class _1st : Form
    {
        public _1st()
        {
            InitializeComponent();
        }
        int startPoint = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 2;
            progressBar1.Value = startPoint;
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                this.Hide();
                new Form1().Show();
            }

        }

        private void _1st_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
