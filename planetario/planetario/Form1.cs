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
        Planetario planetario1 = new Planetario(6.67 * Math.Pow(10, -7), 1, 1);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            tempo.Enabled = false;
            
           Pianeta a = new Pianeta(1000, 209, 65236, 5, Color.Blue);

            a.velocita = new Vettore(-1, 0);
            a.Forza = new Vettore(0, 0);
            a.accellerazione = new Vettore(0, 0);
            a.Image = Image.FromFile("Resources\\Terra.png");

            planetario1.pianeti.Add(a);

            Pianeta b = new Pianeta(500, 350, 1240000000, 7, Color.Red);

            b.velocita = new Vettore(1, 0);
            b.Forza = new Vettore(0, 0);
            b.accellerazione = new Vettore(0, 0);
            b.Image = Image.FromFile("Resources\\Sole.png");

            planetario1.pianeti.Add(b);

            Pianeta c = new Pianeta(500, 250, 1240000000, 7, Color.Orange);

            c.velocita = new Vettore(-1, 0);
            c.Forza = new Vettore(0, 0);
            c.accellerazione = new Vettore(0, 0);
            c.Image = Image.FromFile("Resources\\Marte.png");

            planetario1.pianeti.Add(c);

            Pianeta d = new Pianeta(100, 699, 65236, 5, Color.SandyBrown);

            d.velocita = new Vettore(2, -1);
            d.Forza = new Vettore(0, 0);
            d.accellerazione = new Vettore(0, 0);
            d.Image = Image.FromFile("Resources\\Giove.png");

            planetario1.pianeti.Add(d);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            planetario1.StampaPlanetario(g);
        }
        private void tempo_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            planetario1.MuoviPlanetario();
            
            if (b == 0)
            {
                planetario1.StampaPlanetario(g);
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
                planetario1.cancellapianetigr(g, this.BackColor);
                planetario1.StampaPlanetario(this);
                b++;
            }
            else
            {
                planetario1.cancellapianetipb();
                planetario1.StampaPlanetario(g);
                b--;
            }
        }
    }
}
