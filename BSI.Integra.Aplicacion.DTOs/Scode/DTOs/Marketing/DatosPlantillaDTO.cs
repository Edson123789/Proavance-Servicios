using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosPlantillaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public int Documento { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
    }


    public class PlantillaDatoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public string NombrePlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public bool Estado { get; set; }
        public int Documento { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string NombrePersonalAreaTrabajo { get; set; }
        public List<PlantillaAsociacionModuloSistemaDTO> ListaPlantillaAsociacionModuloSistema { get; set; }

        public PlantillaDatoDTO() {
            ListaPlantillaAsociacionModuloSistema = new List<PlantillaAsociacionModuloSistemaDTO>();
        }
    }



}
