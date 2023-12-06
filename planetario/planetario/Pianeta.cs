using planetario.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace planetario
{
    internal class Pianeta : PictureBox
    {
        //coordinate di riferimento (mutable)
        public int X { get; set; }
        public int Y { get; set; }

        //movimento
        public Vettore velocita { get; set; }
        public Vettore Forza { get; set; }
        public Vettore accellerazione { get; set; }

        //info fisiche pianeta
        public double massa { get; }
        public int raggio { get; }

        //caratteristiche visive
        public Color colore { get; set; }
        
        public Pianeta(int x, int y, double Massa, int Raggio, Color colore)
        {
            //-raggio perchè top e left sono coordinate angolo in alto a sinistra di picturebox
            this.Top = y - raggio;
            this.Left = x - raggio;

            // essendo picturebox rettangolo il raggio viene rappresentato da height e width
            this.Height = raggio * 2;
            this.Width = raggio * 2;

            this.X = x;
            this.Y = y;

            this.raggio = Raggio;
            this.massa = Massa;
            this.colore = colore;
        }
        
        //grafica
        //picturebox
        public void stampapianeta(Form form)
        {
            this.Location = new Point(this.Left, this.Top);
            this.BackColor = Color.Transparent;
            this.SizeMode= PictureBoxSizeMode.StretchImage;
            form.Controls.Add(this);
            this.Visible = true;
            this.Size = new Size(this.raggio * 6, this.raggio * 6);
        }
        //graphics
        public void stampapianeta(Graphics g)
        {
            SolidBrush pennelloCerchio = new SolidBrush(this.colore);
            g.FillEllipse(pennelloCerchio, this.X, this.Y, 2 * this.raggio, 2 * this.raggio);
        }   
        public void cancellapianeta(Graphics g, Color coloresfondo)
        { 
            SolidBrush pennelloCerchio = new SolidBrush(coloresfondo);
            g.FillEllipse(pennelloCerchio, this.X, this.Y, 2 * this.raggio, 2 * this.raggio);
        }

        public void sciapianeta(Graphics g) 
        {
            SolidBrush pennelloCerchio = new SolidBrush(this.colore);
            g.FillEllipse(pennelloCerchio, this.X, this.Y, 7, 7);
        }
    }
}
