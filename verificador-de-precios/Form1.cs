using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace verificador_de_precios
{
    public partial class Form1 : Form
    {
        private int segundos = 0;
        private string codigo = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width / 2 - pictureBox1.Width / 2, 0);
            label2.Location = new Point(this.Width / 2 - label2.Width /2 , this.Height/2 - 100);
            pictureBox2.Location = new Point(this.Width / 2 - pictureBox2.Width / 2, (this.Height / 2) + 50);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                try
                {
                    MySqlConnection servidor = new MySqlConnection("server = 127.0.0.1; user = toor; database = verificador_de_precios; SSL mode = none;");
                    servidor.Open();
                    string query = "SELECT producto_nombre, producto_precio, producto_cantidad, producto_imagen FROM productos WHERE producto_codigo =" + codigo + ";";
                    MySqlCommand consulta = new MySqlCommand(query, servidor);
                    MySqlDataReader resul = consulta.ExecuteReader();
                    if (resul.HasRows)
                    {
                        resul.Read();
                        pictureBox2.Visible = false;
                        pictureBox1.Visible = false;
                        pictureBox3.ImageLocation = resul.GetString(3);
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox3.Visible = true;
                        label2.Text = resul.GetString(0) + Environment.NewLine + "Precio:" + resul.GetString(1) +
                            Environment.NewLine + "Stock:" + resul.GetString(2);

                        timer1.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show("El producto no fue encontrado");
                    }
                }
                catch (Exception x){
                    MessageBox.Show(x.ToString(), "Titulo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                codigo = "";
            }
            else
            {
                codigo += e.KeyChar;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            segundos++;

            if (segundos == 4)
            {
                timer1.Enabled = false;
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                label2.Text = "";
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
