using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class CronogramaCabeceraCambioBO : BaseBO
    {
        public int IdCronogramaTipoModificacion { get; set; }
        public int SolicitadoPor { get; set; }
        public int? AprobadoPor { get; set; }
        public bool Aprobado { get; set; }
        public bool Cancelado { get; set; }
        public string Observacion { get; set; }
        public Guid? IdMigracion { get; set; }


        public string GenerarMensajeRespuestaCambioCronograma(string codigoMatricula, string alumno, string pEspecifico, string centroCosto, int version, string solicitadoPorNombre, string cambios, bool aprobar, string usuario)
        {
            var msmEstado = "";
            var UMsm = "";
            if (aprobar == true) { msmEstado = "Se aprobo el cambio"; UMsm = "Aprobado"; }
            else { msmEstado = "Se Rechazo el cambio"; UMsm = "Rechazado"; }
            string mensaje = string.Empty;
            mensaje += "<p>Estimad@ " + solicitadoPorNombre + " </p>";
            mensaje += "<p style='font-size:10pt;'><b>CODIGO DE MATRICULA :</b> " + codigoMatricula + "</p>";
            mensaje += "<p><b>Datos de la Matricula</b></p>";
            mensaje += "<ul>";
            mensaje += "<li><b>Alumno :</b> " + alumno + "</li>";
            mensaje += "<li><b>Programa:</b> " + pEspecifico + "</li>";
            mensaje += "<li><b>Centro Costo:</b> " + centroCosto + "</li>";
            mensaje += "</ul>";
            mensaje += "<p><b>Datos del Cambio Solicitado</b></p>";
            mensaje += "<ul>";
            mensaje += "<li><b>Estado: </b> " + msmEstado + "</li>";
            mensaje += "<li><b>" + UMsm + " Por: </b>" + usuario + "";
            mensaje += "<li><b>Version:</b> " + version + "</li>";
            mensaje += "<li><b>Cambios:</b> <br/>" + cambios + "</li>";
            mensaje += "</ul>";
            if (aprobar == true) { mensaje += "<p style='font-size:10pt;'><b>Ya se puede generar el CREP</b>" + "</p>"; }
            mensaje += "<br/>";
            mensaje += "<p>Saludos,</p>";
            return mensaje;
        }
    }
}
