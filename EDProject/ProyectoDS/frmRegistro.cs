using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoED.ProyectoDS;

namespace ProyectoDS
{
    public struct Employee
    {
        public string nombreE, apellidoE, direccionE;
        public int edadE, telefonoE;

    };
    public partial class frmRegistro : Form
    {
        private int cantidad, edad, telefono;
        private Stacks pilas;
        string nombre, apellido, direccion;
        
        public frmRegistro()
        {
            InitializeComponent();
            pilas = new Stacks();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (rbPilas.Checked == true)
            {
                rbColasSimples.Enabled = false;
                rbColasCirculares.Enabled = false;
            }
            if (pilas.Estallena())
            {
                txtCantidad.Text = string.Empty;
                txtCantidad.Enabled = true;
                MessageBox.Show("La pila esta llena", "Aviso", MessageBoxButtons.OK);

                LimpiarControles();
                return;
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
            //if (Regex.IsMatch(txtTelefono.Text.Trim(), @"^\d{8}$"))
            //{
            //    MessageBox.Show("Los valores ingresados deben ser números enteros ", "Aviso", MessageBoxButtons.OK);
            //    LimpiarControles();
            //    return;
            //}

            nombre = txtNombre.Text.Trim();
            apellido = txtApellido.Text.Trim();
            direccion = txtDireccion.Text.Trim();
            telefono = int.Parse(txtTelefono.Text.Trim());
            edad = int.Parse(txtEdad.Text.Trim());
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
                pilas.employees[pilas.topePila].telefonoE =0;
                pilas.employees[pilas.topePila].edadE = 0;
                dgEmpleados.Rows[pilas.topePila].Cells[0].Value = null;
                dgEmpleados.Rows[pilas.topePila].Cells[1].Value = null;
                dgEmpleados.Rows[pilas.topePila].Cells[2].Value = null;
                dgEmpleados.Rows[pilas.topePila].Cells[3].Value = null;
                dgEmpleados.Rows[pilas.topePila].Cells[4].Value = null;
                MessageBox.Show("Registro eliminado correctamenete");

            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
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


            pilas.maximo = cantidad;
            pilas.employees = new Employee[pilas.maximo];
            txtCantidad.Enabled = false;

            MessageBox.Show("Arreglo creado", "Aviso", MessageBoxButtons.OK);
        }
    }
}
