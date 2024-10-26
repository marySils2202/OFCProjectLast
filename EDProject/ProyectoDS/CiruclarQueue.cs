using ProyectoDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoED.ProyectoDS
{
    public class CircularQueue
    {

        public Employee?[] employees;
        public int frente;
        public int fin;
        int Tamaño;

        public CircularQueue(int tamaño)
        {

            Tamaño = tamaño;
            employees = new Employee?[tamaño];

            frente = -1;
            fin = -1;
        }

        public void Agregar(Employee employee)
        {
            if (EstaLlena())
            {
                MessageBox.Show("La cola esta llena");
                return;
            }
            else
            if (fin == Tamaño - 1)
            {
                fin = 0;
            }
            else
            {
                fin++;
            }

            employees[fin] = employee;

            if (frente == -1)
            {
                frente = 0;
            }
        }

        public void Eliminar()
        {
            if (EstaVacia())
            {
                MessageBox.Show("No hay elementos para eliminar");
                return;
            }

            employees[frente] = null;

            if (frente == fin)
            {
                frente = fin = -1;
            }
            else
            {
                if (frente == Tamaño - 1)
                {
                    frente = 0;
                }
                else
                {
                    frente++;
                }
            }
        }

        public bool EstaVacia()
        {
            return frente == -1;
        }

        public bool EstaLlena()
        {
            return (fin + 1) % Tamaño == frente;
        }
    }
}



