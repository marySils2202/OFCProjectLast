using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoDS;

namespace ProyectoED.ProyectoDS
{
    public class Stacks
    {
        public int topePila = 0;
        public int maximo;
        public Employee[] employees;
        
        public bool EstaVacia()
        {

            return topePila == 0;

        }

        public bool Estallena()
        {
            return topePila == maximo;
        }
    }
}
