using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace planetario
{
    internal class Planetario
    {
        public List<Pianeta> pianeti = new List<Pianeta>();
        public double G { get; set; }
        public double secpertic { get; set; }
        public double mtperpix { get; set; }

        public Planetario (double costanteG, double secondixtic, double metrixpix)
        {
            this.G = costanteG;
            this.secpertic = secondixtic;
            this.mtperpix = metrixpix;
        }
        
        //dinamica planetario
        public void MuoviPlanetario()
        {
            CalcolaForze();
            CalcolaAccellerazioni();
            CalcolaVelocita();

            //cambia coordinate
            foreach (var pianeta in pianeti)
            {       
                //moltiplico per secpertic in quanto se tenessimo un secondo per tic la simulazione impiegherebbe troppo tempo
                //utilizzo math.round perche il cast tronca il valore = meno preciso
                pianeta.Left += (int)Math.Round(pianeta.velocita.x * secpertic);
                pianeta.Top += (int)Math.Round(pianeta.velocita.y * secpertic);

                pianeta.X += (int)Math.Round(pianeta.velocita.x * secpertic);
                pianeta.Y += (int)Math.Round(pianeta.velocita.y * secpertic);

                //cancella forze e accellerazioni
                pianeta.Forza = new Vettore(0, 0);
                //pianeta.accellerazione = new Vettore(0, 0);
            }
        }

        //grafica planetario
        
        //picturebox
        public void StampaPlanetario(Form form)
        {
            foreach (var pianeta in pianeti)
            {
                pianeta.stampapianeta(form);
            }
        }
        public void cancellapianetipb()
        {
            foreach (var pianeta in pianeti)
            {
                pianeta.Visible = false;
            }
        }
        
        //graphics
        public void StampaPlanetario(Graphics g) 
        {
            foreach (var pianeta in pianeti)
            {
                pianeta.stampapianeta(g);
            }
        }
        public void cancellapianetigr(Graphics g, Color coloresfondo)
        {
            foreach (var pianeta in pianeti)
            {
                pianeta.cancellapianeta(g, coloresfondo);
            }
        }
        public void sciapianeti(Graphics g)
        {
            foreach (var pianeta in pianeti)
            {
                pianeta.sciapianeta(g);
            }
        }
        //fisica
        private void CalcolaForze()
        {
            if (pianeti.Count > 1)
            {
                for (int i = 0; i < pianeti.Count; i++)
                {
                    for (int j = i + 1; j < pianeti.Count; j++)
                    {
                        //la forza che prova uno su laltro è la stessa viceversa
                        double ModForza = G * (pianeti[i].massa * pianeti[j].massa) / (Math.Pow(pianeti[i].X - pianeti[j].X, 2) + Math.Pow(pianeti[i].Y - pianeti[j].Y, 2));

                        Vettore cp1 = new Vettore(pianeti[i].X, pianeti[i].Y);
                        Vettore cp2 = new Vettore(pianeti[j].X, pianeti[j].Y);
                        Vettore forza1 = cp2 - cp1;
                        Vettore forza2 = -1 * forza1;

                        forza1 = (forza1 / forza1.Modulo()) * ModForza;
                        forza2 = (forza2 / forza2.Modulo()) * ModForza;

                        pianeti[i].Forza += forza1;
                        pianeti[j].Forza += forza2;
                    }
                }
            }
        }
        private void CalcolaAccellerazioni()
        {
            foreach (var pianeta in pianeti)
            {
                pianeta.accellerazione = pianeta.Forza/pianeta.massa;
            }
        }
        private void CalcolaVelocita()
        {
            foreach (var pianeta in pianeti)
            {
                //moltiplico per secpertic in quanto se tenessimo un secondo per tic la simulazione impiegherebbe troppo tempo
                pianeta.velocita += pianeta.accellerazione * secpertic;
            }
        }
    }
}
