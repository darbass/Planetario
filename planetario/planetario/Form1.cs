using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace planetario
{
    public partial class Form1 : Form
    {
        int a = 0;
        int b = 0;
        

        Planetario planetario1 = new Planetario(6.67 * Math.Pow(10, -2), 1, 1);
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            tempo.Enabled = false;
            Graphics g = this.CreateGraphics();

            Pianeta b = new Pianeta(900, 500, 800000000, 15);

            b.velocita = new Vettore(0,0);
            b.Forza= new Vettore(0, 0);
            b.accellerazione= new Vettore(0,0);

            planetario1.pianeti.Add(b);
            
            Pianeta c = new Pianeta(20, 20, 300, 15);

            c.velocita = new Vettore(0, 0);
            c.Forza = new Vettore(0, 0);
            c.accellerazione = new Vettore(0, 0);
            planetario1.pianeti.Add(c);

            //planetario1.StampaPlanetario(this);
            planetario1.StampaPlanetario(g, this);

        }
        private void tempo_Tick(object sender, EventArgs e)
        {
            
            if (b == 0)
            {
                Graphics g = this.CreateGraphics();
                planetario1.StampaPlanetario(g, this);
            }
            else
            {
                planetario1.StampaPlanetario(this);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (a == 0)
            {
                tempo.Enabled = true;
                a++;
            }
            else
            {
                tempo.Enabled = false;
                a--;
            }
        }

        private void BtnCG_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (b == 0)
            {
                this.Controls.Clear();
                planetario1.StampaPlanetario(this);
                button1.Visible = true;
                BtnCG.Visible = true;
                b++;
            }
            else
            {
                this.Controls.Clear();
                planetario1.StampaPlanetario(g,this);
                button1.Visible = true;
                BtnCG.Visible = true;
                b--;
            }
        }
    }
}
