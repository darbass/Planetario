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
        public int secpertic { get; set; }
        public int mtperpix { get; set; }

        public Planetario (double costanteG, int secondixtic, int metrixpix)
        {
            this.G = costanteG;
            this.secpertic = secondixtic;
            this.mtperpix = metrixpix;
        }
        
        public void StampaPlanetario(Graphics g)
        {
            CalcolaForze();
            CalcolaAccellerazioni();
            CalcolaVelocita();
            foreach (var pianeta in pianeti)
            {
                //moltiplico per secpertic in quanto se tenessimo un secondo per tic la simulazione impiegherebbe troppo temp0
                //utilizzo math.round perche il cast tronca il valore = meno preciso
                pianeta.Left += (int)Math.Round(pianeta.velocita.x * secpertic);
                pianeta.Top += (int)Math.Round(pianeta.velocita.y * secpertic);

                pianeta.stampapianeta(g);

                //cancella forze e accellerazioni
                pianeta.Forza = new Vettore(0, 0);
                pianeta.accellerazione = new Vettore(0, 0);
            }
        }
        public void StampaPlanetario(Form form)
        {
            CalcolaForze();
            CalcolaAccellerazioni();
            CalcolaVelocita();

            //spostamento pianeti
            foreach (var pianeta in pianeti)
            {
                //moltiplico per secpertic in quanto se tenessimo un secondo per tic la simulazione impiegherebbe troppo temp0
                //utilizzo math.round perche il cast tronca il valore = meno preciso
                pianeta.Left += (int) Math.Round(pianeta.velocita.x * secpertic);
                pianeta.Top += (int) Math.Round(pianeta.velocita.y * secpertic);
                
                pianeta.X += (int)Math.Round(pianeta.velocita.x * secpertic);
                pianeta.Y += (int)Math.Round(pianeta.velocita.y * secpertic);

                pianeta.stampapianeta(form);

                Console.WriteLine(pianeta.X + " " + pianeta.Y);

                //cancella forze e accellerazioni
                pianeta.Forza = new Vettore(0, 0);
                //pianeta.accellerazione = new Vettore(0, 0);
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

            double ModForza = G * (pianeta1.massa + pianeta2.massa) / (Math.Pow(dx, 2) + Math.Pow(dy, 2));

            Vettore cp1 = new Vettore(pianeta1.X, pianeta1.Y);
            Vettore cp2 = new Vettore(pianeta2.X, pianeta2.Y);
            Vettore forza1 = cp2 - cp1;
            Vettore forza2 = -1 * forza1;

            forza1 = (forza1 / forza1.Modulo()) * ModForza;
            forza2 = (forza2 / forza2.Modulo()) * ModForza;

            pianeta1.Forza += forza1;
            pianeta2.Forza += forza2;
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
