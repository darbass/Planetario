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
        List<Pianeta> pianeti = new List<Pianeta>();
        double G = 6.67 * Math.Pow(10, -2);
        int secpertic = 1; //per ogni tic passano 3600 sec
        int i = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            tempo.Enabled = false;

            Pianeta b = new Pianeta(0, 500, 8000000, 30);

            b.velocita = new Vettore(10,0);
            b.Forza= new Vettore(0, 0);
            b.accellerazione= new Vettore(0,0);
            b.stampapianeta(this);
            pianeti.Add(b);

            Pianeta c = new Pianeta(700, 300, 300, 30);

            c.velocita = new Vettore(0, 0);
            c.Forza = new Vettore(0, 0);
            c.accellerazione = new Vettore(0, 0);
            c.stampapianeta(this);
            pianeti.Add(c);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (i==0)
            {
                tempo.Enabled = true;
                i++;
            }
            else
            {
                tempo.Enabled = false;
                i--;
            }
           
        }

        private void tempo_Tick(object sender, EventArgs e)
        {
            CalcolaForze();
            CalcolaAccellerazioni();
            CalcolaVelocita();
            foreach (var pianeta in pianeti)
            {
                //moltiplico per secpertic in quanto se tenessimo un secondo per tic la simulazione impiegherebbe troppo tempo
                pianeta.Left += (int)pianeta.velocita.x /** secpertic*/;
                pianeta.Top += (int)pianeta.velocita.y /** secpertic*/;

                pianeta.stampapianeta(this);

                //cancella forze e accellerazioni
                pianeta.Forza = new Vettore(0, 0);
                pianeta.accellerazione = new Vettore(0,0);
            }
        }
        private void CalcolaForze()
        {
            if (pianeti.Count > 1)
            {
                for (int i = 0; i < pianeti.Count; i++)
                {
                    for (int j = i + 1; j < pianeti.Count; j++)
                    {
                        //la forza che prova uno su laltro è la stessa viceversa
                        Gravitazione(pianeti[i], pianeti[j]);
                    }
                }
            }
            
        }
        private void Gravitazione(Pianeta pianeta1, Pianeta pianeta2)
        {
            //distanza
            double dx = pianeta1.X - pianeta2.X;
            double dy = pianeta1.Y - pianeta2.Y;

            //crazione vettore forza
            Vettore forza= new Vettore(G * ((pianeta1.massa + pianeta2.massa) / Math.Pow(dx, 2)), G * ((pianeta1.massa + pianeta2.massa) / Math.Pow(dy, 2)));

            //somma vettoriale con la forza precedente
            if (dx > 0)
            {
                pianeta1.Forza.x -= forza.x;
            }
            else
            { 
            
            }
            if (dy> 0)
            {

            }
            else 
            { 
            
            }
            pianeta1.Forza += forza;
            pianeta2.Forza += forza;
        }
        private void CalcolaAccellerazioni()
        {
            foreach (var pianeta in pianeti)
            {
                pianeta.accellerazione = new Vettore(pianeta.Forza.x / pianeta.massa, pianeta.Forza.y / pianeta.massa);
            }
        }
        private void CalcolaVelocita()
        {
            foreach (var pianeta in pianeti)
            {
                //moltiplico per secpertic in quanto se tenessimo un secondo per tic la simulazione impiegherebbe troppo tempo
                pianeta.velocita = new Vettore(pianeta.velocita.x + pianeta.accellerazione.x * secpertic, pianeta.velocita.y + pianeta.accellerazione.y * secpertic);
            }
        }    
    }
}
