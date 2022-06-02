using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class LocacionBO : BaseEntity
    {
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int IdRegion { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; }
        public byte[] RowVersion { get; set; }

        DapperRepository _dapperRepository;
        public List<LocacionDTO> Locacion;
        public List<LocacionFiltro> ListaLocacion;
        public LocacionBO()
        {
            _dapperRepository = new DapperRepository();
            Locacion = new List<LocacionDTO>();
            ListaLocacion = new List<LocacionFiltro>();

        }

        public void GetLocacion()
        {
            string _queryLocacion = "Select Id,Nombre from pla.V_LocacionForFiltro Where Estado=1";
            var queryLocacion = _dapperRepository.QueryDapper(_queryLocacion, null);
             Locacion = JsonConvert.DeserializeObject<List<LocacionDTO>>(queryLocacion);

        }
        public void ObtenerLocacionParaFiltro()
        {
            string _queryLocacion = "Select Id,Nombre,IdCiudad from pla.V_TLocacion_ParaFiltro Where Estado=1";
            var queryLocacion = _dapperRepository.QueryDapper(_queryLocacion, null);
             ListaLocacion = JsonConvert.DeserializeObject<List<LocacionFiltro>>(queryLocacion);

        }
    }

    public class LocacionDTO
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
    }

    public class LocacionFiltro
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public int? IdCiudad { get; set; } 
    }

    
}
