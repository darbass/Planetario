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

            Random rand = new Random();

            // Generazione di variabili casuali nel range specificato
            int massa1 = rand.Next(1000, 200000);
            int massa2 = rand.Next(10000, 2000000000);
            int x1 = rand.Next(0, this.ClientSize.Height - 200);
            int x2 = rand.Next(0, this.ClientSize.Height - 200);
            int y1 = rand.Next(0, this.ClientSize.Height - 200);
            int y2 = rand.Next(0, this.ClientSize.Height - 200);
            int velocitax1 = rand.Next(0, 10);
            int velocitax2 = rand.Next(0, 10);
            int velocitay1 = rand.Next(0, 10);
            int velocitay2 = rand.Next(0, 10);

            Pianeta b = new Pianeta(x1, x2, massa1, 15);

            b.velocita = new Vettore(velocitax1, velocitay1);
            b.Forza = new Vettore(0, 0);
            b.accellerazione = new Vettore(0, 0);

            planetario1.pianeti.Add(b);

            Pianeta c = new Pianeta(x2, y2, massa2, 15);

            c.velocita = new Vettore(velocitax2, velocitay2);
            c.Forza = new Vettore(0, 0);
            c.accellerazione = new Vettore(0, 0);
            planetario1.pianeti.Add(c);

            Console.WriteLine("pianeta1 coordinate ({0},{1}), velocità {2}, massa {3}", x1, y1, b.velocita, massa1);
            Console.WriteLine("pianeta2 coordinate ({0},{1}), velocità {2}, massa {3}", x2, y2, c.velocita, massa2);

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
                cancellapianetigr(g);
                planetario1.StampaPlanetario(this);
                button1.Visible = true;
                BtnCG.Visible = true;
                this.BackColor = Color.Black;
                b++;
            }
            else
            {
                cancellapianetipb();
                planetario1.StampaPlanetario(g, this);
                button1.Visible = true;
                BtnCG.Visible = true;
                this.BackColor = Color.White;
                b--;
            }
        }
        private void cancellapianetipb()
        {
            foreach (var pianeta in planetario1.pianeti)
            {
                pianeta.Visible = false;
            }
        }
        private void cancellapianetigr(Graphics g)
        {
            foreach (var pianeta in planetario1.pianeti)
            {
                pianeta.cancellapianeta(g, this);
            }
        }
    }
}
