using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/MaterialPespecificoDetalle
    /// Autor: Gian Miranda
    /// Fecha: 11/07/2021
    /// <summary>
    /// BO para la logica del detalle del material de un PEspecifico
    /// </summary>
    public class MaterialPespecificoDetalleBO : BaseBO
    {
        /// Propiedades	                Significado
        /// -----------	                ------------
        ///IdMaterialPespecifico		Id del MaterialPEspecifico (PK de la tabla ope.T_MaterialPEspecifico)
        ///IdMaterialVersion	        Id de la version del material (PK de la tabla ope.T_MaterialVersion)
        ///IdMaterialEstado				Id del estado del material (PK de la tabla ope.T_MaterialEstado)
        ///NombreArchivo				Nombre del archivo
        ///UrlArchivo					Url del archivo
        ///FechaSubida					Fecha de subida
        ///ComentarioSubida				Comentario de subida
        ///IdMigracion					Id migracion de V3 (Campo nullable)
        ///IdFur					    Id del fur (PK de la tabla fin.T_Fur)
        ///FechaEntrega					Fecha de entrega
        ///DireccionEntrega			    Direccion de entrega
        ///UsuarioAprobacion			Usuario de aprobacion
        ///UsuarioSubida			    Usuario de subida
        ///FechaAprobacion				Fecha de aprobacion
        ///IdEstadoRegistroMaterial		Id del estado del registro del material (PK de la tabla ope.T_EstadoRegistroMaterial)
        ///UsuarioEnvio					Usuario del envio
        ///FechaEnvio					Fecha del envio
        ///ListaMaterialAccion			Lista del material de accion
        ///IdMaterialTipo				Id del tipo de material (PK de la tabla ope.T_MaterialTipo)
        /// IdMigracion                 Id migracion de V3 (Campo nullable)

        public int IdMaterialPespecifico { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdMaterialEstado { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string ComentarioSubida { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdFur { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioSubida { get; set; }
        public DateTime? FechaAprobacion { get; set; }
		public int? IdEstadoRegistroMaterial { get; set; }
        public string UsuarioEnvio { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public List<MaterialAccionBO> ListaMaterialAccion { get; set; }
        public int? IdMaterialTipo { get; set; }
        public MaterialPespecificoDetalleBO() {
            ListaMaterialAccion = new List<MaterialAccionBO>();
        }
    }
}
