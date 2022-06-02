using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteFurAprobadoDTO
    {
        public int  IdFur { get;set;}
        public string  Empresa{get;set;}
        public string  Sede{get;set;}
        public string  Area{get;set;}
        public string  TipoPedido{get;set;}
        public string  CodigoFur{get;set;}
        public int  Semana{get;set;}
        public string  RUC{get;set;}
        public string  Proveedor{get;set;}
        public string  ProductoServicio{get;set;}
        public decimal  Cantidad{get;set;}
        public string  Unidad{get;set;}
        public string  MonedaReal{get;set;}
        public decimal  PagoOrigen{get;set;}
        public decimal PagoDolares {get;set;}
        public string  Descripcion{get;set;}
        public DateTime  FechaLimite{get;set;}
        public string  CentroCosto{get;set;}
        public string  Observaciones{get;set;}
        public string  Rubro{get;set;}
        public long  NroCuenta{get;set;}
        public string  Cuenta{get;set;}
        public DateTime  FechaCreacion{get;set;}
        public string  UsuarioSolicitud{get;set;}
        public string  FaseAprobacion{get;set; }
        public DateTime FechaAprobacionJefeFinanzas { get; set; }
    }
}