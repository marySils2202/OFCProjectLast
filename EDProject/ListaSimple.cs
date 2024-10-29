using ProyectoDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoED
{
    public class Nodo
    {
        public Employee Empleado { get; set; }  
        public Nodo Siguiente { get; set; }  

        public Nodo(Employee empleado)
        {
            Empleado = empleado;
            Siguiente = null;
        }
    }

    internal class ListaSimple
    {
        public Nodo Primero { get; set; }

        public Nodo aux1, aux2 = null;

        public bool ListaVacia()
        {
            if (Primero== null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Nodo ObtenerCabeza()
        {
            return Primero;
        }

        public void agregarInicio(Employee emp)
        {
            if (Primero == null)
            {
                Primero = new Nodo(emp);
                Primero.Siguiente = null;
            }
            else
            {
                Nodo nuevo = new Nodo(emp);
                nuevo.Siguiente = Primero;
                Primero = nuevo;
            }
        }

        public void AgregarFinal(Employee empleado)
        {
            Nodo nuevoNodo = new Nodo(empleado);

            if (Primero == null)
            {
                Primero = nuevoNodo;
            }
            else
            {
                Nodo actual = Primero;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
        }

        public void EliminarInicio()
        {
            if (Primero == null)
            {
                MessageBox.Show("La lista esta vacia, no se puede eliminar elementos");
            }
            else
            {
                if (Primero.Siguiente == null)
                {
                    Primero = null;
                }
                else
                {
                    Nodo nodo = Primero;
                    Primero = nodo.Siguiente;
                    nodo = null;
                }
            }
        }

        public void AgregarAntes(Employee empleado, object referencia)
        {
            if (ListaVacia())
            {
                Primero = new Nodo(empleado);
                Primero.Siguiente = null;
            }
            else
            {
                Nodo aux1 = null;
                Nodo aux2 = Primero;
                int bandera = 1;

                while ((!aux2.Empleado.nombreE.Equals(referencia) && !aux2.Empleado.apellidoE.Equals(referencia) &&
                        !aux2.Empleado.direccionE.Equals(referencia) &&
                        !(int.TryParse(referencia.ToString(), out int refInt) &&
                          (aux2.Empleado.telefonoE == refInt || aux2.Empleado.edadE == refInt))) && bandera == 1)
                {
                    if (aux2.Siguiente != null)
                    {
                        aux1 = aux2;
                        aux2 = aux2.Siguiente;
                    }
                    else
                    {
                        bandera = 0;
                    }
                }

                if (bandera == 1)
                {
                    Nodo nuevo = new Nodo(empleado);

                    if (Primero == aux2)
                    {
                        nuevo.Siguiente = Primero;
                        Primero = nuevo;
                    }
                    else
                    {
                        aux1.Siguiente = nuevo;
                        nuevo.Siguiente = aux2;
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontró \"{referencia}\" en la lista");
                }
            }
        }


        public void AgregarDespues(Employee empleado, object referencia)
        {
            if (ListaVacia())
            {
                Primero = new Nodo(empleado);
                Primero.Siguiente = null;
            }
            else
            {
                Nodo aux = Primero;
                int bandera = 1;

                while ((!aux.Empleado.nombreE.Equals(referencia) && !aux.Empleado.apellidoE.Equals(referencia) &&
                        !aux.Empleado.direccionE.Equals(referencia) &&
                        !(int.TryParse(referencia.ToString(), out int refInt) &&
                          (aux.Empleado.telefonoE == refInt || aux.Empleado.edadE == refInt))) && bandera == 1)
                {
                    if (aux.Siguiente != null)
                    {
                        aux = aux.Siguiente;
                    }
                    else
                    {
                        bandera = 0;
                    }
                }

                if (bandera == 1)
                {
                    Nodo nuevo = new Nodo(empleado);
                    nuevo.Siguiente = aux.Siguiente;
                    aux.Siguiente = nuevo;
                }
                else
                {
                    MessageBox.Show($"No se encontró \"{referencia}\" en la lista");
                }
            }
        }



        public void EliminarFinal()
        {
            if (Primero == null)
            {
                MessageBox.Show("La lista esta vacia, no se puede eliminar elementos");
            }
            else
            {
                if (Primero.Siguiente == null)
                {
                    Primero = null;
                }
                Nodo aux = Primero;
                while (aux.Siguiente.Siguiente != null)
                {
                    aux = aux.Siguiente;
                }
                aux.Siguiente = null;
            }
        }

        public void Eliminar_X(string elementoaeliminar)
        {
            if (Primero == null)
            {
                MessageBox.Show("No se pueden eliminar más elementos. La lista está vacía.");
                return;
            }

            if (Primero.Empleado.nombreE == elementoaeliminar)
            {
                Primero = Primero.Siguiente;
                return;
            }

            Nodo actual = Primero;

            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Empleado.nombreE == elementoaeliminar)
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return;
                }

                actual = actual.Siguiente;
            }

            MessageBox.Show("El elemento no fue encontrado en la lista");
        }

        public Nodo Busqueda_Desordenada(string referencia)
        {
            Nodo nuevo = Primero;

            while (nuevo != null && nuevo.Empleado.nombreE != referencia)
                nuevo = nuevo.Siguiente;

            return nuevo;
        }

        public void EliminarAntes(object referencia)
        {
            if (ListaVacia())
            {
                MessageBox.Show("La lista está vacía");
            }
            else
            {
                if (Primero.Empleado.nombreE.Equals(referencia) || Primero.Empleado.apellidoE.Equals(referencia) ||
                    Primero.Empleado.direccionE.Equals(referencia) || Primero.Empleado.telefonoE.Equals(referencia) ||
                    Primero.Empleado.edadE.Equals(referencia))
                {
                    MessageBox.Show("No existe un nodo anterior al primero");
                }
                else
                {
                    Nodo aux1 = null;
                    Nodo aux2 = Primero;
                    Nodo nuevo = Primero.Siguiente;
                    int bandera = 1;

                    while (nuevo != null && bandera == 1 &&
                        !nuevo.Empleado.nombreE.Equals(referencia) && !nuevo.Empleado.apellidoE.Equals(referencia) &&
                        !nuevo.Empleado.direccionE.Equals(referencia) &&
                        !(int.TryParse(referencia.ToString(), out int refInt) &&
                          (nuevo.Empleado.telefonoE == refInt || nuevo.Empleado.edadE == refInt)))
                    {
                        aux1 = aux2;
                        aux2 = nuevo;
                        nuevo = nuevo.Siguiente;
                    }

                    if (nuevo == null || bandera == 0)
                    {
                        MessageBox.Show($"No se encontró \"{referencia}\" en la lista");
                    }
                    else
                    {
                        if (Primero.Siguiente == nuevo)
                        {
                            Primero = nuevo;
                        }
                        else
                        {
                            aux1.Siguiente = nuevo;
                        }
                    }
                }
            }
        }

        public void EliminarDespues(object referencia)
        {
            if (ListaVacia())
            {
                MessageBox.Show("La lista está vacía");
            }
            else
            {
                if ((Primero.Empleado.nombreE.Equals(referencia) || Primero.Empleado.apellidoE.Equals(referencia) ||
                     Primero.Empleado.direccionE.Equals(referencia) ||
                     (int.TryParse(referencia.ToString(), out int refInt) &&
                      (Primero.Empleado.telefonoE == refInt || Primero.Empleado.edadE == refInt)))
                    && Primero.Siguiente == null)
                {
                    MessageBox.Show("No existe un nodo posterior al primero");
                }
                else
                {
                    Nodo actual = Primero;
                    Nodo aux1 = null;
                    Nodo aux2 = null;

                    while (actual != null && !actual.Empleado.nombreE.Equals(referencia) &&
                           !actual.Empleado.apellidoE.Equals(referencia) && !actual.Empleado.direccionE.Equals(referencia) &&
                           !(int.TryParse(referencia.ToString(), out refInt) &&
                             (actual.Empleado.telefonoE == refInt || actual.Empleado.edadE == refInt)))
                    {
                        aux1 = actual;
                        actual = actual.Siguiente;
                    }

                    if (actual != null && actual.Siguiente != null)
                    {
                        aux2 = actual.Siguiente;
                        actual.Siguiente = aux2.Siguiente;
                    }
                    else if (actual != null && actual.Siguiente == null)
                    {
                        MessageBox.Show("No se puede establecer al último como referencia");
                    }
                    else
                    {
                        MessageBox.Show($"No se encontró \"{referencia}\" en la lista");
                    }
                }
            }
        }


    }
}
