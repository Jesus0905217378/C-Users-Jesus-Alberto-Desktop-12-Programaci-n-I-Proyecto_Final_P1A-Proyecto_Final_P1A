﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_P1A.Clases.Entidades
{
    public class MdAlumnos
    {
        public int idAlumno { get; set; }
        public string? carnet { get; set; }
        public string? nombre { get; set; }
        public string? correo { get; set; }
        public string? clase { get; set; }
        public string? seccion { get; set; }
        public int parcial1 { get; set; }
        public int parcial2 { get; set; }
        public int parcial3 { get; set; }


        public override string ToString()
        {
            return $"id: {idAlumno} Nombre {nombre} seccion {seccion} parcial1{parcial1} parcial2{parcial2}parcial3{parcial3}";
        } 
    }
}
