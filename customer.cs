using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Practicle_cw
{
    public partial class customer : Form
    {
        public customer()
        {
            InitializeComponent();
        }
        FilterInfoCollection filterinfo;
        VideoCaptureDevice videocapture;

        void startcamera()
        {
            try
            {
                filterinfo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                videocapture = new VideoCaptureDevice(filterinfo[0].MonikerString);
                videocapture.NewFrame += new NewFrameEventHandler(camera_on);
                videocapture.Start();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        private void camera_on(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            startcamera();
            panel2.Hide();
            panel1.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
            string filename = @"D:\picture\customer\" + txtfilename.Text + ".jpg";
            var bitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox2.DrawToBitmap(bitmap, pictureBox2.ClientRectangle);
            System.Drawing.Imaging.ImageFormat imageFormat = null;
            imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            bitmap.Save(filename,imageFormat);

            btnupload.Show();
        }

        private void customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               new customer().Close();
            }
            catch
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel1.Show();
            label8.Show();
            txtfilename.Show();
            btnstart.Show();
            button2.Show();
            btnupload.Hide();
        }

        private void customer_Load(object sender, EventArgs e)
        {
            panel2.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            panel1.Hide();
            label8.Hide();
            txtmobile.Hide();
            txtname.Hide();
            txtnic.Hide();
            txtpuchesprice.Hide();
            txtsearch.Hide();
            textBox1.Hide();
            txtfilename.Hide();
            btnstart.Hide();
            button2.Hide();
            btnpicupdate.Hide();
            btnsave.Hide();
            button1.Hide();
            btnsearch.Hide();
            btnupload.Hide();
            button4.Hide();
            button3.Hide();
           
        }

        private void btnnewpic_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            panel1.Show();
            label8.Show();
            txtfilename.Show();
            btnstart.Show();
            button2.Show();
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
            txtname.Clear();
            txtnic.Clear();
            txtpuchesprice.Clear();
            txtfilename.Clear();
            textBox1.Clear();
            txtmobile.Clear();
            panel1.Hide();
            panel2.Show();
            btnupload.Hide();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            txtmobile.Show();
            txtname.Show();
            txtnic.Show();
            txtpuchesprice.Show();
            txtsearch.Show();
            textBox1.Show();
            btnpicupdate.Show();
            btnsave.Show();
            button1.Show();
            btnsearch.Show();
            button4.Show();
            button3.Show();

        }

        private void btnreg_Click(object sender, EventArgs e)
        {
            txtsearch.Clear();
            txtname.Clear();
            txtnic.Clear();
            txtpuchesprice.Clear();
            txtfilename.Clear();
            textBox1 .Clear();
            txtmobile .Clear();
            label7.Hide();
            txtsearch.Hide();
            btnsearch.Hide();
            panel1.Show();
            panel2.Hide();
            btnupload.Hide();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();         
            txtmobile.Show();
            txtname.Show();
            txtnic.Show();
            txtpuchesprice.Show();
            textBox1.Show();
            btnpicupdate.Show();
            btnsave.Show();
            button1.Show();
        }
        public Image image
        {
            get { return pictureBox3.Image; }
            set { pictureBox3.Image = value; }
        }
        private byte[] getphoto()
        {
            MemoryStream stream = new MemoryStream();
            pictureBox3.Image.Save(stream, pictureBox3.Image.RawFormat);

            return stream.GetBuffer();
        }
        private void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK) ;
                {
                    pictureBox3.Image = new Bitmap(ofd.FileName);
                }
                panel2.Show();
                panel3.Hide();
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {

                //Difine command
                string sql = "INSERT INTO customer VALUES(@cname, @nic, @mobile,@purches,@saved,@picture)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cname", this.txtname.Text);
                cmd.Parameters.AddWithValue("@nic", this.txtnic.Text);
                cmd.Parameters.AddWithValue("@mobile", this.txtmobile.Text);
                cmd.Parameters.AddWithValue("@purches", this.txtpuchesprice.Text);
                cmd.Parameters.AddWithValue("@saved", this.textBox1.Text);
                cmd.Parameters.AddWithValue("@picture", getphoto());

                //excute command to insert data
                int retur = cmd.ExecuteNonQuery();

                MessageBox.Show("Customer Added" + retur, "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string y = "Customer Added Successful";
                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.Volume = 100;
                synth.Rate = 2;

                synth.Speak(y);

                conn.Close();
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SerialPort sp = new SerialPort();
            sp.PortName = "COM8";
            sp.Open();
            sp.WriteLine("AT" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("AT+CMGS=\"" + txtmobile.Text + "\"" + Environment.NewLine);
            Thread.Sleep(100);
            sp.WriteLine("Hi "+ txtname.Text+ "\nThank Your For Joining With Us... \n** YOUR ACCOUNT CREATED **\n... Come And Shop With Us..\n\n*SMS STOP ONLINE SHOPPING CENTER to "+94720840910+"" + Environment.NewLine);
            Thread.Sleep(100);
            sp.Write(new byte[] { 26 }, 0, 1);
            Thread.Sleep(100);

            var response = sp.ReadExisting();
            if (response.Contains("ERROR"))
            {
                MessageBox.Show("Sending faild");
            }
            /*else
            {
                MessageBox.Show("Message sent");
            }*/
            sp.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                try
                {
                    //Difine a Command
                    string sql = "UPDATE customer SET cname =@cname,nic=@nic,mobile= @mobile,purches =@purches,saved =@saved,picture =@picture WHERE cname =@cname";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@cname", this.txtname.Text);
                    cmd.Parameters.AddWithValue("@nic", this.txtnic.Text);
                    cmd.Parameters.AddWithValue("@mobile", this.txtmobile.Text);
                    cmd.Parameters.AddWithValue("@purches", this.txtpuchesprice.Text);
                    cmd.Parameters.AddWithValue("@saved", this.textBox1.Text);
                    cmd.Parameters.AddWithValue("@picture", getphoto());

                    //Excute Command to insert Data
                    int retur = cmd.ExecuteNonQuery();

                    MessageBox.Show("Your account Update successfully." + retur);
                    string y = "Customer details Update Successful";
                    SpeechSynthesizer synth = new SpeechSynthesizer();
                    synth.Volume = 100;
                    synth.Rate = 2;

                    synth.Speak(y);
                    conn.Close();
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex .Message);
                }
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                try
                {
                    string deleteQuery = "DELETE FROM customer WHERE mobile = '" + txtmobile.Text + "'";
                    SqlCommand cmd = new SqlCommand(deleteQuery, conn);

                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    DialogResult ms = MessageBox.Show("Are you Sure Remove Account", "Remove Item", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (ms == DialogResult.OK)
                    {
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customert successfully deleted.");

                        }
                        else
                        {
                            MessageBox.Show("Error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch(Exception ex2) 
                { 
                    MessageBox.Show(ex2.Message);
                }
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            DbConnection dbConnection = new DbConnection();
            SqlConnection conn = dbConnection.EstablishConnection();
            if (conn != null)
            {
                try
                {
                    string sql = "SELECT * FROM customer WHERE mobile =@mobile ";//methana table eke nama
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@mobile", this.txtsearch.Text);

                    //Access Data using DataReader Methord
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        //Bind Data with Control
                        this.txtname.Text = reader.GetValue(0).ToString();
                        this.txtnic.Text = reader.GetValue(1).ToString();
                        this.txtmobile.Text = reader.GetValue(2).ToString();
                        this.txtpuchesprice.Text = reader.GetValue(3).ToString();
                        this.textBox1.Text = reader.GetValue(4).ToString();

                        if (!reader.IsDBNull(reader.GetOrdinal("picture")))
                        {
                            byte[] imgByte = (byte[])reader["picture"];
                            using (MemoryStream ms = new MemoryStream(imgByte))
                            {
                                this.pictureBox3.Image = Image.FromStream(ms);
                            }
                        }
                    }


                    else
                    {
                        MessageBox.Show("No Records Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //Disconnect from server
                    conn.Close();
                }
                catch (Exception ex3)
                {
                    MessageBox.Show(ex3.Message);
                }
            }
            else
            {
                MessageBox.Show("Error establishing database connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
