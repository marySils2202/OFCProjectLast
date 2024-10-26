using ProyectoDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoED.ProyectoDS
{
    public class ColaSimple
    {
        public Employee[] employees;
        public int final;  
        public int frente; 

        public ColaSimple(int size)
        {
            employees = new Employee[size];
            final = -1;
            frente = -1;
        }

        public void Agregar(Employee empleado)
        {
            if (EstaLlena())
            {
                MessageBox.Show("La cola está llena");
                return;
            }

            final++;
            employees[final] = empleado;

            if (final == 0)
            {
                frente = 0;
            }
        }

        public void Eliminar()
        {
            if (EstaVacia())
            {
                MessageBox.Show("La cola esta vacia");
                return;
            }
            else
            {
                if (frente == final)
                {
                    employees.SetValue(null, frente);
                    frente = -1;
                    final = -1;
                }
                else 
                {
                    employees.SetValue(null, frente);                    
                    frente++;
                }
            }
        }

        public bool EstaLlena()
        {
            return final == employees.Length - 1;
        }

        public bool EstaVacia()
        {
            return frente == -1;
        }
    }

}
