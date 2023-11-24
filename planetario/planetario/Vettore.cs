using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace planetario
{
    internal class Vettore
    {
        public double x { get; }
        public double y { get; }

        public Vettore(double X, double Y)
        {
            this.x = X;
            this.y = Y;
        }

        public override string ToString() //possibile feature far vedere a video il valore vettore
        {
            return string.Format("({0};{1})", x, y);
        }

        public static Vettore operator + (Vettore a, Vettore b)
        {
            return new Vettore((a.x + b.x), (a.y + b.y));   
        }

        public static Vettore operator - (Vettore a, Vettore b)
        {
            return new Vettore((a.x - b.x), (a.y - b.y));
        }
        public static Vettore operator *(Vettore a, double b)
        {
            return new Vettore((a.x * b), (a.y * b));
        }
        public static Vettore operator *(double b, Vettore a)
        {
            return new Vettore((a.x * b), (a.y * b));
        }
        public static Vettore operator /(Vettore a, double b)
        {
            return new Vettore((a.x / b), (a.y / b));
        }
        public static Vettore operator /(double b, Vettore a)
        {
            return new Vettore((b/a.x), (b/a.y));
        }
        public double Modulo()
        {
            return Math.Sqrt( Math.Pow(2,x) + Math.Pow(2,y) );
        }
    }
}
