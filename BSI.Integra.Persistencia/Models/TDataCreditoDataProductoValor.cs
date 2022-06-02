﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataProductoValor
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string Producto { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public string Valor3 { get; set; }
        public string Valor4 { get; set; }
        public string Valor5 { get; set; }
        public string Valor6 { get; set; }
        public string Valor7 { get; set; }
        public string Valor8 { get; set; }
        public string Valor9 { get; set; }
        public string Valor10 { get; set; }
        public string Valor1smlv { get; set; }
        public string Valor2smlv { get; set; }
        public string Valor3smlv { get; set; }
        public string Valor4smlv { get; set; }
        public string Valor5smlv { get; set; }
        public string Valor6smlv { get; set; }
        public string Valor7smlv { get; set; }
        public string Valor8smlv { get; set; }
        public string Valor9smlv { get; set; }
        public string Valor10smlv { get; set; }
        public string Razon1 { get; set; }
        public string Razon2 { get; set; }
        public string Razon3 { get; set; }
        public string Razon4 { get; set; }
        public string Razon5 { get; set; }
        public string Razon6 { get; set; }
        public string Razon7 { get; set; }
        public string Razon8 { get; set; }
        public string Razon9 { get; set; }
        public string Razon10 { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusqueda IdDataCreditoBusquedaNavigation { get; set; }
    }
}
