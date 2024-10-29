using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoED;
using ProyectoED.ProyectoDS;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoDS
{

    public struct Employee
    {
        public string nombreE, apellidoE, direccionE;
        public int edadE, telefonoE;

    };
    public partial class frmRegistro : Form
    {
        string nombre, apellido, direccion;
        private int cantidad, edad, telefono;
        private Stacks pilas;
        private ColaSimple simples;
        private CircularQueue colascirculares;
        private ListaSimple ListaSimple;

        public frmRegistro()
        {
            InitializeComponent();
            pilas = new Stacks();
            simples = new ColaSimple(cantidad);
            ListaSimple = new ListaSimple();
        }

        public void Agregar(string nombre, string apellido, string direccion, int telefono, int edad)
        {
            if (rbPilas.Checked == true)
            {
                rbColasSimples.Enabled = false;
                rbColasCirculares.Enabled = false;

                if (pilas.Estallena())
                {
                    txtCantidad.Text = string.Empty;
                    txtCantidad.Enabled = true;
                    MessageBox.Show("La pila esta llena", "Aviso", MessageBoxButtons.OK);

                    LimpiarControles();
                    return;
                }
                else
                {
                    pilas.employees[pilas.topePila].nombreE = nombre;
                    pilas.employees[pilas.topePila].apellidoE = apellido;
                    pilas.employees[pilas.topePila].direccionE = direccion;
                    pilas.employees[pilas.topePila].telefonoE = telefono;
                    pilas.employees[pilas.topePila].edadE = edad;
                    dgEmpleados.Rows.Add(
                        (pilas.employees[pilas.topePila].nombreE), (pilas.employees[pilas.topePila].apellidoE), (pilas.employees[pilas.topePila].telefonoE),
                        (pilas.employees[pilas.topePila].direccionE), (pilas.employees[pilas.topePila].edadE)
                        );

                    pilas.topePila++;
                    LimpiarControles();
                }
            }
            else if (rbColasSimples.Checked == true)
            {
                rbPilas.Enabled = false;
                rbColasCirculares.Enabled = false;

                Employee empleado = new Employee();
                empleado.nombreE = nombre;
                empleado.telefonoE = telefono;
                empleado.apellidoE = apellido;
                empleado.edadE = edad;
                empleado.direccionE = direccion;

                if (simples.EstaLlena())
                {
                    MessageBox.Show("La cola está llena. No se puede agregar más empleados.");
                }
                else
                {
                    simples.Agregar(empleado);

                    dgEmpleados.Rows.Add(
                        simples.employees[simples.final].nombreE,
                        simples.employees[simples.final].apellidoE,
                        simples.employees[simples.final].telefonoE,
                        simples.employees[simples.final].direccionE,
                        simples.employees[simples.final].edadE
                    );

                    LimpiarControles();
                }

            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) || string.IsNullOrEmpty(txtEdad.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Debe completar los campos solicitados", "Aviso", MessageBoxButtons.OK);
                return;
            }
            if (int.Parse(txtEdad.Text.Trim()) <= 1)
            {
                MessageBox.Show("El empleado debe ser mayor de 18 años ", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return;

            }
            if (int.Parse(txtTelefono.Text.Trim()) <= 1)
            {
                MessageBox.Show("Los valores ingresados deben ser números enteros ", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return;
            }

            Agregar(txtNombre.Text, txtApellido.Text, txtDireccion.Text, int.Parse(txtTelefono.Text), int.Parse(txtEdad.Text));

        }
        public void LimpiarControles()
        {
            txtApellido.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (rbPilas.Checked == true)
            {
                if (pilas.EstaVacia())
                {
                    MessageBox.Show("No se puede eliminar, la pila está vacía.");
                    return;
                }
                else
                {
                    pilas.topePila--;
                    pilas.employees[pilas.topePila].nombreE = string.Empty;
                    pilas.employees[pilas.topePila].apellidoE = string.Empty;
                    pilas.employees[pilas.topePila].direccionE = string.Empty;
                    pilas.employees[pilas.topePila].telefonoE = 0;
                    pilas.employees[pilas.topePila].edadE = 0;
                    dgEmpleados.Rows[pilas.topePila].Cells[0].Value = null;
                    dgEmpleados.Rows[pilas.topePila].Cells[1].Value = null;
                    dgEmpleados.Rows[pilas.topePila].Cells[2].Value = null;
                    dgEmpleados.Rows[pilas.topePila].Cells[3].Value = null;
                    dgEmpleados.Rows[pilas.topePila].Cells[4].Value = null;
                    MessageBox.Show("Registro eliminado correctamenete");
                }
            }
            else if (rbColasSimples.Checked == true)
            {
                if (simples.EstaVacia())
                {
                    MessageBox.Show("La cola esta vacia, no se pueden eliminar más empleados");
                }
                else
                {
                    simples.Eliminar();

                    dgEmpleados.Rows.Clear();

                    for (int i = 0; i < simples.employees.Length; i++)
                    {
                        dgEmpleados.Rows.Add(simples.employees[i].nombreE, simples.employees[i].apellidoE, simples.employees[i].telefonoE, simples.employees[i].direccionE, simples.employees[i].edadE);
                    }

                    MessageBox.Show("Registro eliminado correctamente");
                }

            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            if (!rbPilas.Checked && !rbColasSimples.Checked && !rbColasCirculares.Checked)
            {
                MessageBox.Show("Por favor seleccione una estructura antes de crear el arreglo");
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar la cantidad de empleados", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
            }
            if (int.Parse(txtCantidad.Text.Trim()) <= 0)
            {
                MessageBox.Show("La cantidad de empleados debe ser mayor a 0", "Aviso", MessageBoxButtons.OK);
                txtCantidad.Focus();
            }
            cantidad = int.Parse(txtCantidad.Text.Trim());

            if (rbPilas.Checked == true)
            {
                pilas.maximo = cantidad;
                pilas.employees = new Employee[pilas.maximo];
                txtCantidad.Enabled = false;
            }
            else if (rbColasSimples.Checked == true)
            {
                simples = new ColaSimple(cantidad);
            }


            MessageBox.Show("Arreglo creado correctamente", "Aviso", MessageBoxButtons.OK);
        }

        public void ValidarFormulario()
        {
            if (rbPilas.Checked || rbColasSimples.Checked || rbColasCirculares.Checked)
            {
                if (string.IsNullOrEmpty(txtCantidad.Text.Trim()))
                {
                    MessageBox.Show("Debe indicar el tamaño del arreglo", "Aviso", MessageBoxButtons.OK);
                    txtCantidad.Focus();
                    return;
                }
            }
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) || string.IsNullOrEmpty(txtEdad.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Debe completar los campos solicitados", "Aviso", MessageBoxButtons.OK);
                return;
            }
            if (int.Parse(txtEdad.Text.Trim()) <= 1)
            {
                MessageBox.Show("El empleado debe ser mayor de 18 años ", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return;

            }
            if (int.Parse(txtTelefono.Text.Trim()) <= 1)
            {
                MessageBox.Show("Los valores ingresados deben ser números enteros ", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {


            ValidarFormulario();
            cantidad = int.Parse(txtCantidad.Text.Trim());
            colascirculares = new CircularQueue(cantidad);

            if (colascirculares.EstaLlena())
            {
                txtCantidad.Text = string.Empty;
                txtCantidad.Enabled = true;
                MessageBox.Show("La cola está llena", "Aviso", MessageBoxButtons.OK);
                LimpiarControles();
                return;
            }

            Employee empleado = new Employee
            {
                nombreE = txtNombre.Text.Trim(),
                apellidoE = txtApellido.Text.Trim(),
                direccionE = txtDireccion.Text.Trim(),
                telefonoE = int.Parse(txtTelefono.Text.Trim()),
                edadE = int.Parse(txtEdad.Text.Trim())
            };

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


        private void button1_Click(object sender, EventArgs e)
        {
            colascirculares.Eliminar();
            int fila = colascirculares.frente;
            if (fila >= 0 && fila < dgEmpleados.Rows.Count)
            {
                dgEmpleados.Rows[fila].Cells[0].Value = null;
                dgEmpleados.Rows[fila].Cells[1].Value = null;
                dgEmpleados.Rows[fila].Cells[2].Value = null;
                dgEmpleados.Rows[fila].Cells[3].Value = null;
                dgEmpleados.Rows[fila].Cells[4].Value = null;

            }
        }

        private void rbColasSimples_CheckedChanged(object sender, EventArgs e)
        {
            if (rbColasSimples.Checked)
            {
                rbColasCirculares.Enabled = false;
                rbPilas.Enabled = false;
            }
        }

        private void rbPilas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPilas.Checked == true)
            {
                rbColasSimples.Enabled = false;
                rbColasCirculares.Enabled = false;
            }
        }

        private void rbColasCirculares_CheckedChanged(object sender, EventArgs e)
        {
            if (rbColasCirculares.Checked)
            {
                rbColasSimples.Enabled = false;
                rbPilas.Enabled = false;
            }
        }

        private void rbListasSimples_CheckedChanged(object sender, EventArgs e)
        {
            txtCantidad.Enabled = false;
            btnIniciar.Enabled = false;
            btnAgregarInicio.Enabled = true;
            btnAgregarFinal.Enabled = true;
            btnAgregarAntes.Enabled = true;
            btnAgregarDespues.Enabled = true;
            txtReferenciaAg.Enabled = true;
            btnEliminarInicio.Enabled = true;
            btnEliminarFinal.Enabled = true;
            btnEliminarX.Enabled = true;
            btnEliminarAntes.Enabled = true;
            btnEliminarDespues.Enabled = true;
            txtReferencia.Enabled = true;
        }

        private void MostrarListaSimple()
        {
            dgEmpleados.Rows.Clear();
            Nodo actual = ListaSimple.ObtenerCabeza();
            while (actual != null)
            {
                dgEmpleados.Rows.Add(actual.Empleado.nombreE, actual.Empleado.apellidoE, actual.Empleado.telefonoE, actual.Empleado.direccionE, actual.Empleado.edadE);
                actual = actual.Siguiente;
            }
        }

        private void btnAgregarInicio_Click(object sender, EventArgs e)
        {
            ValidarFormulario();
            Employee empleado = new Employee
            {
                nombreE = txtNombre.Text.Trim(),
                apellidoE = txtApellido.Text.Trim(),
                direccionE = txtDireccion.Text.Trim(),
                telefonoE = int.Parse(txtTelefono.Text.Trim()),
                edadE = int.Parse(txtEdad.Text.Trim())
            };

            ListaSimple.agregarInicio(empleado);
            MostrarListaSimple();
            LimpiarControles();
        }

        private void btnAgregarFinal_Click(object sender, EventArgs e)
        {
            ValidarFormulario();
            Employee empleado = new Employee
            {
                nombreE = txtNombre.Text.Trim(),
                apellidoE = txtApellido.Text.Trim(),
                direccionE = txtDireccion.Text.Trim(),
                telefonoE = int.Parse(txtTelefono.Text.Trim()),
                edadE = int.Parse(txtEdad.Text.Trim())
            };

            ListaSimple.AgregarFinal(empleado);
            dgEmpleados.Rows.Add(empleado.nombreE, empleado.apellidoE, empleado.telefonoE, empleado.direccionE, empleado.edadE);
            LimpiarControles();
        }

        private void btnEliminarInicio_Click(object sender, EventArgs e)
        {
            if (ListaSimple.ListaVacia() == true)
            {
                MessageBox.Show("La lista esta vacía. No se puede eliminar");
            }
            else
            {
                ListaSimple.EliminarInicio();
                MostrarListaSimple();
            }
        }

        private void btnEliminarFinal_Click(object sender, EventArgs e)
        {
            if (ListaSimple.ListaVacia() == true)
            {
                MessageBox.Show("La lista esta vacía. No se puede eliminar");
            }
            else
            {
                ListaSimple.EliminarFinal();
                MostrarListaSimple();
            }
        }

        private void btnEliminarX_Click(object sender, EventArgs e)
        {
            if (ListaSimple.ListaVacia() == true)
            {
                MessageBox.Show("La lista esta vacía. No se puede eliminar");
                return;
            }
            if (string.IsNullOrEmpty(txtReferencia.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                txtReferencia.Focus();
                return;
            }
            ListaSimple.Eliminar_X(txtReferencia.Text);
            MostrarListaSimple();
            txtReferencia.Text = string.Empty;
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            Nodo resultado = ListaSimple.Busqueda_Desordenada(txtBuscar.Text);

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
                MessageBox.Show("No se encontró el elemento: " + txtBuscar.Text);
            }

            txtBuscar.Text = string.Empty;
        }

        private void btnAgregarAntes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaAg.Text))
            {
                MessageBox.Show("Ingrese una referencia");
                txtReferenciaAg.Focus();
                return;
            }
            Employee empleado = new Employee
            {
                nombreE = txtNombre.Text.Trim(),
                apellidoE = txtApellido.Text.Trim(),
                direccionE = txtDireccion.Text.Trim(),
                telefonoE = int.Parse(txtTelefono.Text.Trim()),
                edadE = int.Parse(txtEdad.Text.Trim())
            };

            ListaSimple.AgregarAntes(empleado, txtReferenciaAg.Text);
            MostrarListaSimple();
            txtReferenciaAg.Text = string.Empty;
            LimpiarControles();
        }

        private void btnAgregarDespues_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReferenciaAg.Text))
            {
                MessageBox.Show("Ingrese una referencia");
                txtReferenciaAg.Focus();
                return;
            }
            Employee empleado = new Employee
            {
                nombreE = txtNombre.Text.Trim(),
                apellidoE = txtApellido.Text.Trim(),
                direccionE = txtDireccion.Text.Trim(),
                telefonoE = int.Parse(txtTelefono.Text.Trim()),
                edadE = int.Parse(txtEdad.Text.Trim())
            };

            ListaSimple.AgregarDespues(empleado, txtReferenciaAg.Text);
            MostrarListaSimple();
            txtReferenciaAg.Text = string.Empty;
            LimpiarControles();
        }

        private void btnEliminarAntes_Click(object sender, EventArgs e)
        {
            if (ListaSimple.ListaVacia() == true)
            {
                MessageBox.Show("La lista esta vacía. No se puede eliminar");
                return;
            }
            if (string.IsNullOrEmpty(txtReferencia.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                txtReferencia.Focus();
                return;
            }
            ListaSimple.EliminarAntes(txtReferencia.Text);
            MostrarListaSimple();
            txtReferencia.Text = string.Empty;
        }

        private void btnEliminarDespues_Click(object sender, EventArgs e)
        {
            if (ListaSimple.ListaVacia() == true)
            {
                MessageBox.Show("La lista esta vacía. No se puede eliminar");
                return;
            }
            if (string.IsNullOrEmpty(txtReferencia.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del empleado como referencia para eliminar el registro");
                txtReferencia.Focus();
                return;
            }
            ListaSimple.EliminarDespues(txtReferencia.Text);
            MostrarListaSimple();
            txtReferencia.Text = string.Empty;
        }
    }

}
    

