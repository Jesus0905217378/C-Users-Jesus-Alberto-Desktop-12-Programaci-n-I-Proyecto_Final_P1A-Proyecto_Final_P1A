using Proyecto_Final_P1A.Clases.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Proyecto_Final_P1A.Clases.Servicio
{
    public class ClsImportExport
    {
        private ClsConexion cone;

        public ClsImportExport()
        {
            cone = new();
        }


        public string importar(string archivo)
        {
            string texto = "";
            try
            {
                texto = File.ReadAllText(archivo);
                return $"Procesados:{cone.EjecutarSQLDirecto(texto)}";
            }
            catch (Exception ex)
            {
                return $"Hubo un error al importar {ex.Message}";
            }
        }

        
        public string exportar(string instruccion, string archivoDestino)
        {
            string textoSalida = "";
            DataTable respuesta = cone.consultaTablaDirecta(instruccion);


            foreach (DataRow dr in respuesta.Rows)
            {
                textoSalida += $"{dr["carnet"]};{dr["nombre"]};{dr["seccion"]};{dr["parcial1"]}\n";
            }


            if (!string.IsNullOrEmpty(textoSalida))
            {
                try
                {
                    File.WriteAllText(archivoDestino, textoSalida);
                    return $"Procesado o creado archivo {archivoDestino}";
                }
                catch (Exception ex)
                {
                    return $"hay clavo en el archivo {ex.Message}";
                }
            }

            return "no se encontraron registros";

        }


        
    }
}
