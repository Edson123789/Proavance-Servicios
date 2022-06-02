﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: ArticuloBO
    /// Autor: 
    /// Fecha: 24/02/2021
    /// <summary>
    /// Definicion Variables Articulo
    /// </summary>
    public class ArticuloBO : BaseBO
    {
        public int IdWeb { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string ImgPortada { get; set; }
        public string ImgPortadaAlt { get; set; }
        public string ImgSecundaria { get; set; }
        public string ImgSecundariaAlt { get; set; }
        public string Autor { get; set; }
        public int? IdTipoArticulo { get; set; }
        public string Contenido { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdCategoria { get; set; }
        public string UrlWeb { get; set; }
        public Guid? IdMigracion { get; set; }

        public string UrlDocumento { get; set; }
        public string DescripcionGeneral { get; set; }
    }
}
