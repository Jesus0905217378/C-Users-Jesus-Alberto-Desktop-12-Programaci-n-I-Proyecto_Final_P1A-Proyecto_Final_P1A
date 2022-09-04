using Proyecto_Final_P1A.Clases.Entidades;
using Proyecto_Final_P1A.Clases.Servicio;

namespace WF_ProyectoFinal
{
    public partial class Form1 : Form
    {
        ServicioAlumno srvAlumno = new();
        MdAlumnos oAlumnos = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void DesplegarGrid()
        {
            var respuesta = srvAlumno.ConsultaSQL("select * from tb_alumnos");
            dataGridViewAlumnos.DataSource = respuesta;
        }
        private void buttonObtenerDatos_Click(object sender, EventArgs e)
        {
            DesplegarGrid();
        }

        public void MapeoDatoFormulario(MdAlumnos _alumnos)
        {
            textBoxCarnet.Text = _alumnos.carnet;
            textBoxNombre.Text = _alumnos.nombre;
            textBoxCorreo.Text = _alumnos.correo;
            comboBoxClase.Text = _alumnos.clase;
            comboBoxSeccion.Text = _alumnos.seccion;
            textBoxP1.Text = Convert.ToString(_alumnos.parcial1);
            textBoxP2.Text = Convert.ToString(_alumnos.parcial2);
            textBoxP3.Text = Convert.ToString(_alumnos.parcial3);
        }

        private void LimpiarDatos()
        {
            oAlumnos = new();
            MapeoDatoFormulario(oAlumnos);
        }
        public void BuscarAlumno(string carnet)
        {
            oAlumnos = null;
            oAlumnos = srvAlumno.ObtenerAlumno(carnet);
            if (oAlumnos == null)
            {
                MessageBox.Show("El alumno buscado no existe");
                LimpiarDatos();
            }
            else
            {
                MapeoDatoFormulario(oAlumnos);
            }
        }

        private void buttonConsulta_Click(object sender, EventArgs e)
        {
            string carnet = textBoxCarnet.Text;
            BuscarAlumno(carnet);
        }

        public MdAlumnos DatosFormulario()
        {
            MdAlumnos _alumnos = new();
            _alumnos.carnet = textBoxCarnet.Text.Trim();
            _alumnos.nombre = textBoxNombre.Text.Trim();
            _alumnos.correo = textBoxCorreo.Text.Trim();
            _alumnos.clase = comboBoxClase.Text;
            _alumnos.seccion = comboBoxSeccion.Text;
            _alumnos.parcial1 = Convert.ToInt32(textBoxP1.Text);//conversion 
            _alumnos.parcial2 = Convert.ToInt32(textBoxP2.Text);
            _alumnos.parcial3 = Convert.ToInt32(textBoxP3.Text);
            return _alumnos;
        }
        public void Validacion()
        {
            MessageBox.Show("La nota ingresada no es valida");
            LimpiarDatos();
            oAlumnos = DatosFormulario();
        }
        private void buttonCrearAlumno_Click(object sender, EventArgs e)
        {
            oAlumnos = DatosFormulario();

            if (oAlumnos.parcial1 > 20)
            {
                Validacion();
            }
            else if (oAlumnos.parcial2 > 25)
            {
                Validacion();
            }
            else if (oAlumnos.parcial3 > 30)
            {
                Validacion();
            }
            else
            {
                int respuesta = srvAlumno.CrearAlumno(oAlumnos);//modificacion en ServicioAlumno
                if (respuesta > 0)
                {

                    MessageBox.Show("Se creo con exito el alumno");
                    LimpiarDatos();
                    DesplegarGrid();
                }
                else
                {
                    MessageBox.Show("ups hay un problema con la grabacion");
                }
            }

            
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            if (textBoxNombre.Text != "")
            {
                oAlumnos = DatosFormulario();
                int respuesta = srvAlumno.actualizarAlumno(oAlumnos);

                if (respuesta > 0)
                {
                    MessageBox.Show("Se compuso el Alumno");
                    LimpiarDatos();
                    DesplegarGrid();
                }
                else
                {
                    MessageBox.Show("Perdon hay un problema con la Grabacion");
                }

            }
            else
            {
                MessageBox.Show("No se puede dejar la casilla en Nombre en blanco");
            }

        }

        private void buttonImportar_Click(object sender, EventArgs e)
        {
            string archivo = @"C:/Users/Jesus Alberto/Desktop/12 Programación I/alunos.txt";
            ClsImportExport im = new();
            MessageBox.Show(im.importar(archivo));
        }

        private void buttonExportar_Click(object sender, EventArgs e)
        {
            string archivo = @"C:/Users/Jesus Alberto/Desktop/12 Programación I/salida.csv";
            ClsImportExport im = new();
            MessageBox.Show(im.exportar("select * from tb_alumnos where seccion='A'", archivo));
        }

        private void buttonBorrarAlum_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Estas seguro que quieres eliminar a este alumno?", "Eliminar Alumno", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                oAlumnos = DatosFormulario();
                int respuesta = srvAlumno.BorrarAlumno(oAlumnos);
                if (respuesta > 0)
                {
                    MessageBox.Show("Alumno eliminado exitosamente");
                    LimpiarDatos();
                    DesplegarGrid();
                }
                else
                {
                    MessageBox.Show("Hubo un problema al eliminar el alumno");
                }
            }
            if (r == DialogResult.No)
            {
                LimpiarDatos();
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
        }
    }
}