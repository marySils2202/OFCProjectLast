using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProyectoED.ListasDobles;

namespace ProyectoED.ProyectoDS
{
    public struct Employee
    {
        public string nombreE, apellidoE, direccionE;
        public int edadE, telefonoE;
    };
    public partial class RegistroOFC : Form
    {
        string nombre, apellido, direccion;
        private int cantidad, edad, telefono;
        private Stacks pilas;
        private ColaSimple simples;
        private CircularQueue colascirculares;
        private ListaSimple ListaSimple;
        private ListasDobles listaDoble;
        public RegistroOFC()
        {
            InitializeComponent();
            pilas = new Stacks();
            simples = new ColaSimple(cantidad);
            ListaSimple = new ListaSimple();
            listaDoble = new ListasDobles();
            btnInicio.Enabled = true;
            ControlesFormulario(false);
        }
        #region Creación y Selección de la Estructura
        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (!rbStacks.Checked && !rbSimpleColas.Checked && !rbCircularesColas.Checked)
            {
                MessageBox.Show("Por favor seleccione una estructura antes de crear el arreglo");
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar la cantidad de empleados", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
                return;
            }
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad de empleados debe ser un número mayor a 0", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
                return;
            }
            MessageBox.Show("Arreglo creado correctamente", "Aviso", MessageBoxButtons.OK);
            ControlesFormulario(true);

        }
        private void rbStacks_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStacks.Checked)
            {
                pilas.maximo = cantidad;
                pilas.employees = new Employee[pilas.maximo];
                txtCantidad.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = false;
                rbListasDobles.Enabled = false;
                rbListasSimples.Enabled = false;


            }
        }
        private void rbSimpleColas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimpleColas.Checked)
            {
                simples = new ColaSimple(cantidad);
                btnEliminar.Enabled = false;
                btnAgregar.Enabled = false;
                rbListasDobles.Enabled = false;
                rbListasSimples.Enabled = false;

            }
        }
        private void rbListasSimples_CheckedChanged(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked)
            {
                ListaSimple = new ListaSimple();
                btnInicio.Enabled = false;
                txtCantidad.Enabled = false;
                ActivarControles(true);
                DesactivarRadioButton(false);
                rbListasDobles.Enabled = false;
                MessageBox.Show("Lista Simple creada correctamente", "Aviso", MessageBoxButtons.OK);
            }
        }
        private void rbListasDobles_CheckedChanged(object sender, EventArgs e)
        {
            if (rbListasDobles.Checked)
            {
                listaDoble = new ListasDobles();
                btnInicio.Enabled = false;
                txtCantidad.Enabled = false;
                ActivarControles(true);
                DesactivarRadioButton(false);
                rbListasSimples.Checked = false;
                MessageBox.Show("Lista Doble creada correctamente", "Aviso", MessageBoxButtons.OK);
            }
        }
        private void rbCircularesColas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCircularesColas.Checked)
            {
                rbSimpleColas.Enabled = false;
                rbStacks.Enabled = false;
                btnEliminar.Enabled = false;
                btnAgregar.Enabled = false;
                rbListasDobles.Enabled = false;
                rbListasSimples.Enabled = false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            rbCircularesColas.Enabled = true;
            rbSimpleColas.Enabled = true;
            rbListasDobles.Enabled = true;
            rbListasSimples.Enabled = true;
            rbStacks.Enabled = true;
            txtCantidad.Text = string.Empty;
            cantidad = 0;
            txtCantidad.Enabled = true;
            btnInicio.Enabled = true;
        }

        private Employee CrearEmpleado()
        {
            return new Employee
            {
                nombreE = txtNombre.Text.Trim(),
                apellidoE = txtApellido.Text.Trim(),
                direccionE = txtDireccion.Text.Trim(),
                telefonoE = int.Parse(txtTelefono.Text.Trim()),
                edadE = int.Parse(txtEdad.Text.Trim())
            };
        }

        #endregion
        #region Controles
        private void DesactivarRadioButton(bool activar)
        {
            rbCircularesColas.Enabled = activar;
            rbSimpleColas.Enabled = activar;
            rbStacks.Enabled = activar;
            btnAgregar.Enabled = activar;
            btnEliminar.Enabled = activar;
            button1.Enabled = activar;
            button2.Enabled = activar;
        }
        private void ActivarControles(bool Activar)
        {

            btnAntesAgregar.Enabled = Activar;
            btnDespuesAgregar.Enabled = Activar;
            btnInicioAgregar.Enabled = Activar;
            btnFinalAgregar.Enabled = Activar;
            btnAntesEliminar.Enabled = Activar;
            btnDespuesEliminar.Enabled = Activar;
            btnInicioEliminar.Enabled = Activar;
            btnEliminar_X.Enabled = Activar;
            btnFinalEliminar.Enabled = Activar;
            txtReferenciaAgregar.Enabled = Activar;
            txtReferenciaEliminar.Enabled = Activar;
        }
        private void LimpiarControles()
        {
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }
        private void ControlesFormulario(bool activar)
        {
            txtNombre.Enabled = activar;
            txtApellido.Enabled = activar;
            txtDireccion.Enabled = activar;
            txtEdad.Enabled = activar;
            txtTelefono.Enabled = activar;
            txtBusqueda.Enabled = activar;
        }
        #endregion

        #region Validaciones
        private bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) || string.IsNullOrEmpty(txtEdad.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Debe completar los campos solicitados", "Aviso", MessageBoxButtons.OK);
                return false;
            }

            if (!int.TryParse(txtEdad.Text.Trim(), out edad) || edad < 18)
            {
                MessageBox.Show("El empleado debe ser mayor de 18 años y la edad debe ser un número válido.", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return false;
            }

            if (!int.TryParse(txtTelefono.Text.Trim(), out telefono) || telefono <= 0)
            {
                MessageBox.Show("El teléfono debe ser un número entero positivo.", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return false;
            }
            return true;

        }
        #endregion
        #region Pilas y Colas
        public void Agregar(string nombre, string apellido, string direccion, int telefono, int edad)
        {
            if (rbStacks.Checked)
            {
                if (pilas.Estallena())
                {
                    MessageBox.Show("La pila está llena", "Aviso", MessageBoxButtons.OK);
                    LimpiarControles();
                    return;
                }
                else
                {
                    pilas.employees[pilas.topePila] = CrearEmpleado();
                    dgEmpleados.Rows.Add(nombre, apellido, telefono, direccion, edad);
                    pilas.topePila++;
                    LimpiarControles();
                }
            }

        }
        #endregion
        #region Pilas y Colas

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                int.TryParse(txtTelefono.Text, out telefono);
                int.TryParse(txtEdad.Text, out edad);
                Agregar(txtNombre.Text, txtApellido.Text, txtDireccion.Text, telefono, edad);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (rbStacks.Checked)
            {
                if (pilas.EstaVacia())
                {
                    MessageBox.Show("No se puede eliminar, la pila está vacía.");
                    return;
                }
                else
                {
                    pilas.topePila--;
                    dgEmpleados.Rows.RemoveAt(pilas.topePila);
                    MessageBox.Show("Registro eliminado correctamente");
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (rbSimpleColas.Checked)
            {
                if (!ValidarFormulario()) return;
                if (simples.EstaVacia())
                {
                    MessageBox.Show("La cola está vacía, no se pueden eliminar más empleados.");
                }
                else
                {
                    simples.Eliminar();
                    dgEmpleados.Rows.Clear();
                    for (int i = 0; i <= simples.final; i++)
                    {
                        dgEmpleados.Rows.Add(simples.employees[i].nombreE, simples.employees[i].apellidoE, simples.employees[i].telefonoE, simples.employees[i].direccionE, simples.employees[i].edadE);
                    }
                    MessageBox.Show("Registro eliminado correctamente");
                }
            }
            else
            if (rbCircularesColas.Checked)
            {
                if (colascirculares.EstaVacia())
                {
                    MessageBox.Show("La cola circular está vacía.");
                    return;
                }
                colascirculares.Eliminar();
                int fila = colascirculares.frente;

                if (fila >= 0 && fila < dgEmpleados.Rows.Count)
                {
                    dgEmpleados.Rows.RemoveAt(fila);
                    MessageBox.Show("Registro eliminado correctamente");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (rbSimpleColas.Checked)
            {
                if (simples.EstaLlena())
                {
                    MessageBox.Show("La cola está llena. No se puede agregar más empleados.");
                }
                else
                {
                    Employee empleado = CrearEmpleado();
                    simples.Agregar(empleado);
                    dgEmpleados.Rows.Clear();
                    for (int i = 0; i <= simples.final; i++)
                    {
                        dgEmpleados.Rows.Add(simples.employees[i].nombreE, simples.employees[i].apellidoE, simples.employees[i].telefonoE, simples.employees[i].direccionE, simples.employees[i].edadE);
                    }
                    LimpiarControles();
                }
            }
            else
            if (rbCircularesColas.Checked)
            {
                if (!ValidarFormulario()) return;

                cantidad = int.Parse(txtCantidad.Text.Trim());
                colascirculares = new CircularQueue(cantidad);

                if (colascirculares.EstaLlena())
                {
                    MessageBox.Show("La cola está llena", "Aviso", MessageBoxButtons.OK);
                    LimpiarControles();
                    return;
                }
                Employee empleado = CrearEmpleado();
                colascirculares.Agregar(empleado);
                dgEmpleados.Rows.Add(
                    empleado.nombreE,
                    empleado.apellidoE,
                    empleado.telefonoE,
                    empleado.direccionE,
                    empleado.edadE
                );
                LimpiarControles();
            }
        }
        #endregion
        #region Listas Dobles y Enlazadas
        //Métodos para mostrar listas
        private void MostrarListaDoble()
        {
            dgEmpleados.Rows.Clear();
            NodoDoble actual = listaDoble.ObtenerCabeza();

            if (actual == null)
            {
                MessageBox.Show("La lista doble está vacía.");
                return;
            }

            while (actual != null)
            {
                dgEmpleados.Rows.Add(
                    actual.Empleado.nombreE,
                    actual.Empleado.apellidoE,
                    actual.Empleado.telefonoE,
                    actual.Empleado.direccionE,
                    actual.Empleado.edadE
                );
                actual = actual.Siguiente;
            }
        }
        private void MostrarListaSimple()
        {
            dgEmpleados.Rows.Clear();
            Nodo actual = ListaSimple.ObtenerCabeza();

            if (actual == null)
            {
                MessageBox.Show("La lista simple está vacía.");
                return;
            }

            while (actual != null)
            {
                dgEmpleados.Rows.Add(
                    actual.Empleado.nombreE,
                    actual.Empleado.apellidoE,
                    actual.Empleado.telefonoE,
                    actual.Empleado.direccionE,
                    actual.Empleado.edadE
                );
                actual = actual.Siguiente;
            }
        }
        //Funciones para agregar elementos a las listas
        private void btnInicioAgregar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked)
            {
                if (!ValidarFormulario()) return;
                Employee empleado = CrearEmpleado();
                ListaSimple.agregarInicio(empleado);
                MostrarListaSimple();
                LimpiarControles();
                MessageBox.Show("Registro agregado exitosamente.");
            }
            else
                if (rbListasDobles.Checked)
            {
                if (!ValidarFormulario()) return;
                Employee empleado = CrearEmpleado();
                listaDoble.AgregarInicio(empleado);
                MostrarListaDoble();
                LimpiarControles();
                MessageBox.Show("Registro agregado exitosamente.");
            }

        }
        private void btnAntesAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaAgregar.Text))
            {
                MessageBox.Show("Ingrese una referencia");
                txtReferenciaAgregar.Focus();
                return;
            }
            if (!ValidarFormulario()) return;
            Employee empleado = CrearEmpleado();
            if (rbListasSimples.Checked)
            {
                ListaSimple.AgregarAntes(empleado, txtReferenciaAgregar.Text);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                listaDoble.AgregarAntes(empleado, txtReferenciaAgregar.Text);
                MostrarListaDoble();
            }
            txtReferenciaAgregar.Text = string.Empty;
            LimpiarControles();
            MessageBox.Show("Registro agregado exitosamente");
        }
        private void btnDespuesAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaAgregar.Text))
            {
                MessageBox.Show("Ingrese una referencia");
                txtReferenciaAgregar.Focus();
                return;
            }
            if (!ValidarFormulario()) return;
            Employee empleado = CrearEmpleado();
            if (rbListasSimples.Checked)
            {
                ListaSimple.AgregarDespues(empleado, txtReferenciaAgregar.Text);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                listaDoble.AgregarDespues(empleado, txtReferenciaAgregar.Text);
                MostrarListaDoble();
            }
            txtReferenciaAgregar.Text = string.Empty;
            LimpiarControles();
            MessageBox.Show("Registro agregado exitosamente");
        }
        private void btnFinalAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;
            Employee empleado = CrearEmpleado();
            if (rbListasSimples.Checked)
            {
                ListaSimple.AgregarFinal(empleado);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                listaDoble.AgregarFinal(empleado);
                MostrarListaDoble();
            }
            txtReferenciaAgregar.Text = string.Empty;
            LimpiarControles();
            MessageBox.Show("Registro agregado exitosamente");
        }

        //Funciones para eliminar objetos de las listas
        private void btnEliminar_X_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
            {
                MessageBox.Show("Ingrese el nombre del empleado como referencia para eliminar el registro");
                txtReferenciaEliminar.Focus();
                return;
            }
            if (rbListasSimples.Checked)
            {
                if (ListaSimple.ListaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar.");
                    return;
                }
                ListaSimple.Eliminar_X(txtReferenciaEliminar.Text);
                MostrarListaSimple();
            }
            else if (rbListasDobles.Checked)
            {
                if (listaDoble.EstaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar.");
                    return;
                }
                listaDoble.Eliminar_X(txtReferenciaEliminar.Text);
                MostrarListaDoble();
            }
            txtReferenciaEliminar.Text = string.Empty;
            MessageBox.Show("Registro eliminado exitosamente");
        }

        private void btnInicioEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                }
                else
                {
                    ListaSimple.EliminarInicio();
                    MostrarListaSimple();
                    MessageBox.Show("Registro Eliminado Exitosamente");

                }
            }
            else if (rbListasDobles.Checked == true)
            {
                listaDoble.EliminarInicio();
                MostrarListaDoble();
                MessageBox.Show("Registro Eliminado Exitosamente");

            }

        }
        private void btnFinalEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                }
                else
                {
                    ListaSimple.EliminarFinal();
                    MostrarListaSimple();
                    MessageBox.Show("Registro Eliminado Exitosamente");

                }
            }
            else if (rbListasDobles.Checked == true)
            {
                listaDoble.EliminarFinal();
                listaDoble.MostrarLista(dgEmpleados);
                MessageBox.Show("Registro Eliminado Exitosamente");

            }

        }
        private void btnAntesEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                ListaSimple.EliminarAntes(txtReferenciaEliminar.Text);
                MostrarListaSimple();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");

            }
            else
            if (rbListasDobles.Checked == true)
            {
                if (listaDoble.EstaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                listaDoble.EliminarAntes(txtReferenciaEliminar.Text);
                MostrarListaDoble();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");

            }
        }
        private void btnDespuesEliminar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                if (ListaSimple.ListaVacia() == true)
                {
                    MessageBox.Show("La lista esta vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                ListaSimple.EliminarDespues(txtReferenciaEliminar.Text);
                MostrarListaSimple();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");

            }
            else
              if (rbListasDobles.Checked == true)
            {
                if (listaDoble.EstaVacia())
                {
                    MessageBox.Show("La lista está vacía. No se puede eliminar");
                    return;
                }
                if (string.IsNullOrEmpty(txtReferenciaEliminar.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                    txtReferenciaEliminar.Focus();
                    return;
                }
                listaDoble.EliminarDespues(txtReferenciaEliminar.Text);
                MostrarListaDoble();
                txtReferenciaEliminar.Text = string.Empty;
                MessageBox.Show("Registro Eliminado Exitosamente");

            }

        }


        #endregion
        #region Búsqueda en la lista
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (rbListasSimples.Checked == true)
            {
                Nodo resultado = ListaSimple.Busqueda_Desordenada(txtBusqueda.Text);
                if (resultado != null)
                {
                    bool existe = false;
                    foreach (DataGridViewRow row in dgEmpleados.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(resultado.Empleado.nombreE, StringComparison.OrdinalIgnoreCase))
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe)
                    {
                        dgEmpleados.Rows.Clear();
                        dgEmpleados.Rows.Add(resultado.Empleado.nombreE, resultado.Empleado.apellidoE, resultado.Empleado.telefonoE, resultado.Empleado.direccionE, resultado.Empleado.edadE);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el elemento: " + txtBusqueda.Text);
                }
                txtBusqueda.Text = string.Empty;
            }
            else
               if (rbListasDobles.Checked == true)
            {
                NodoDoble resultado = listaDoble.Busqueda_Desordenada(txtBusqueda.Text);
                if (resultado != null)
                {
                    bool existe = false;
                    foreach (DataGridViewRow row in dgEmpleados.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(resultado.Empleado.nombreE, StringComparison.OrdinalIgnoreCase))
                        {
                            existe = true;
                            break;
                        }
                    }
                    if (existe)
                    {
                        dgEmpleados.Rows.Clear();
                        dgEmpleados.Rows.Add(resultado.Empleado.nombreE, resultado.Empleado.apellidoE, resultado.Empleado.telefonoE, resultado.Empleado.direccionE, resultado.Empleado.edadE);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el elemento: " + txtBusqueda.Text);
                }
                txtBusqueda.Text = string.Empty;

            }
             
        }
        #endregion



        private void RegistroOFC_Load(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click_1(object sender, EventArgs e)
        {
            if (!rbStacks.Checked && !rbSimpleColas.Checked && !rbCircularesColas.Checked)
            {
                MessageBox.Show("Por favor seleccione una estructura antes de crear el arreglo");
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar la cantidad de empleados", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
                return;
            }
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("La cantidad de empleados debe ser un número mayor a 0", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
                return;
            }
            MessageBox.Show("Arreglo creado correctamente", "Aviso", MessageBoxButtons.OK);
            ControlesFormulario(true);
        }
    }

}
