using ProyectoDS;
using System;
using System.Windows.Forms;

namespace ProyectoED
{
    public class ListasDobles
    {
        private NodoDoble cabeza;
        private NodoDoble cola;

        public class NodoDoble
        {
            public ProyectoED.ProyectoDS.Employee Empleado;
            public NodoDoble Anterior;
            public NodoDoble Siguiente;

            public NodoDoble(ProyectoED.ProyectoDS.Employee empleado)
            {
                Empleado = empleado;
                Anterior = null;
                Siguiente = null;
            }

           
        }
        public NodoDoble ObtenerCabeza()
        {
            return cabeza;
        }
        public ListasDobles()
        {
            cabeza = null;
            cola = null;
        }

        public bool EstaVacia()
        {
            return cabeza == null;
        }

        public void AgregarInicio(ProyectoED.ProyectoDS.Employee empleado)
        {
            NodoDoble nuevoNodo = new NodoDoble(empleado);
            if (EstaVacia())
            {
                cabeza = nuevoNodo;
                cola = nuevoNodo;
            }
            else
            {
                nuevoNodo.Siguiente = cabeza;
                cabeza.Anterior = nuevoNodo;
                cabeza = nuevoNodo;
            }
        }

        public void AgregarFinal(ProyectoED.ProyectoDS.Employee empleado)
        {
            NodoDoble nuevoNodo = new NodoDoble(empleado);
            if (EstaVacia())
            {
                cabeza = nuevoNodo;
                cola = nuevoNodo;
            }
            else
            {
                cola.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = cola;
                cola = nuevoNodo;
            }
        }

        public void EliminarInicio()
        {
            if (!EstaVacia())
            {
                if (cabeza == cola)
                {
                    cabeza = null;
                    cola = null;
                }
                else
                {
                    cabeza = cabeza.Siguiente;
                    cabeza.Anterior = null;
                }
            }
        }

        public void EliminarFinal()
        {
            if (!EstaVacia())
            {
                if (cabeza == cola)
                {
                    cabeza = null;
                    cola = null;
                }
                else
                {
                    cola = cola.Anterior;
                    cola.Siguiente = null;
                }
            }
        }

        public void AgregarAntes(ProyectoED.ProyectoDS.Employee empleado, string referencia)
        {
            if (EstaVacia()) return;

            NodoDoble actual = cabeza;
            while (actual != null && actual.Empleado.nombreE != referencia)
            {
                actual = actual.Siguiente;
            }

            if (actual != null)
            {
                NodoDoble nuevoNodo = new NodoDoble(empleado);
                nuevoNodo.Siguiente = actual;
                nuevoNodo.Anterior = actual.Anterior;

                if (actual.Anterior != null)
                {
                    actual.Anterior.Siguiente = nuevoNodo;
                }
                else
                {
                    cabeza = nuevoNodo;
                }
                actual.Anterior = nuevoNodo;
            }
        }

        public void AgregarDespues(ProyectoED.ProyectoDS.Employee empleado, string referencia)
        {
            if (EstaVacia()) return;

            NodoDoble actual = cabeza;
            while (actual != null && actual.Empleado.nombreE != referencia)
            {
                actual = actual.Siguiente;
            }

            if (actual != null)
            {
                NodoDoble nuevoNodo = new NodoDoble(empleado);
                nuevoNodo.Anterior = actual;
                nuevoNodo.Siguiente = actual.Siguiente;

                if (actual.Siguiente != null)
                {
                    actual.Siguiente.Anterior = nuevoNodo;
                }
                else
                {
                    cola = nuevoNodo;
                }
                actual.Siguiente = nuevoNodo;
            }
        }

        public void Eliminar_X(string referencia)
        {
            if (EstaVacia()) return;

            NodoDoble actual = cabeza;
            while (actual != null && actual.Empleado.nombreE != referencia)
            {
                actual = actual.Siguiente;
            }

            if (actual != null)
            {
                if (actual.Anterior != null)
                {
                    actual.Anterior.Siguiente = actual.Siguiente;
                }
                else
                {
                    cabeza = actual.Siguiente;
                }

                if (actual.Siguiente != null)
                {
                    actual.Siguiente.Anterior = actual.Anterior;
                }
                else
                {
                    cola = actual.Anterior;
                }
            }
        }

        public void EliminarAntes(string referencia)
        {
            if (EstaVacia() || cabeza.Siguiente == null) return;

            NodoDoble actual = cabeza;
            while (actual != null && actual.Empleado.nombreE != referencia)
            {
                actual = actual.Siguiente;
            }

            if (actual != null && actual.Anterior != null)
            {
                NodoDoble nodoAEliminar = actual.Anterior;

                if (nodoAEliminar.Anterior != null)
                {
                    nodoAEliminar.Anterior.Siguiente = actual;
                    actual.Anterior = nodoAEliminar.Anterior;
                }
                else
                {
                    cabeza = actual;
                    actual.Anterior = null;
                }
            }
        }

        public void EliminarDespues(string referencia)
        {
            if (EstaVacia()) return;

            NodoDoble actual = cabeza;
            while (actual != null && actual.Empleado.nombreE != referencia)
            {
                actual = actual.Siguiente;
            }

            if (actual != null && actual.Siguiente != null)
            {
                NodoDoble nodoAEliminar = actual.Siguiente;

                if (nodoAEliminar.Siguiente != null)
                {
                    nodoAEliminar.Siguiente.Anterior = actual;
                    actual.Siguiente = nodoAEliminar.Siguiente;
                }
                else
                {
                    actual.Siguiente = null;
                    cola = actual;
                }
            }
        }

        public void MostrarLista(DataGridView dgEmpleados)
        {
            dgEmpleados.Rows.Clear();
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                dgEmpleados.Rows.Add(actual.Empleado.nombreE, actual.Empleado.apellidoE, actual.Empleado.telefonoE, actual.Empleado.direccionE, actual.Empleado.edadE);
                actual = actual.Siguiente;
            }
        }

        public NodoDoble Busqueda_Desordenada(string referencia)
        {
            NodoDoble actual = cabeza;

            
            while (actual != null && actual.Empleado.nombreE != referencia)
            {
                actual = actual.Siguiente;
            }

         
            return actual;
        }

    }
}
