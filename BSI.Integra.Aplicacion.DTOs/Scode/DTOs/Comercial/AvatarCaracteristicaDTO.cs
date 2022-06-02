using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Comercial
{
    public class AvatarCaracteristicaDTO
    {
        public string TipoCaracteristica { get; set; }
        public string Etiqueta { get; set; }
        public string Valor { get; set; }
    }
    public class AvatarCaracteristicaAgrupadoDTO
    {
        public List<AvatarCaracteristicaDTO> Accesorios { get; set; }
        public List<AvatarCaracteristicaDTO> Ropa { get; set; }
        public List<AvatarCaracteristicaDTO> RopaColor { get; set; }
        public List<AvatarCaracteristicaDTO> Mirada { get; set; }
        public List<AvatarCaracteristicaDTO> Cejas { get; set; }
        public List<AvatarCaracteristicaDTO> Barba { get; set; }
        public List<AvatarCaracteristicaDTO> BarbaColor { get; set; }
        public List<AvatarCaracteristicaDTO> CabelloColor { get; set; }
        public List<AvatarCaracteristicaDTO> Boca { get; set; }
        public List<AvatarCaracteristicaDTO> PielColor { get; set; }
        public List<AvatarCaracteristicaDTO> Cabello { get; set; }
    }
}
