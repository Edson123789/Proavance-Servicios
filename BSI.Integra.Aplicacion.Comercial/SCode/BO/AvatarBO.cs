using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    /// BO: AvatarBO
    /// Autor: Jashin Salazar
    /// Fecha: 28/07/2021
    /// <summary>
    /// Gestion de modificacion del Avatar del personal
    /// </summary>
    public class AvatarBO: BaseBO
    {
        public int IdPersonal { get; set; }
        public string Top { get; set; }
        public string Accessories { get; set; }
        public string HairColor { get; set; }
        public string FacialHair { get; set; }
        public string FacialHairColor { get; set; }
        public string Clothes { get; set; }
        public string Eyes { get; set; }
        public string Eyesbrow { get; set; }
        public string Mouth { get; set; }
        public string Skin { get; set; }
        public string ClothesColor { get; set; }
        private readonly integraDBContext _integraDBContext;
        private readonly AvatarRepositorio _repAvatar;
        public AvatarBO()
        {
            _repAvatar = new AvatarRepositorio();
        }

        public AvatarBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repAvatar = new AvatarRepositorio(integraDBContext);
        }
        /// Autor: Jashin Salazar
        /// Fecha: 30/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las caracteristicas para la construccion del avatar
        /// </summary>
        /// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
        public AvatarCaracteristicaAgrupadoDTO ObtenerCaracteristicas()
        {
            try
            {
                var caracteristicasTotal = _repAvatar.ObtenerCaracteristicas();
                AvatarCaracteristicaAgrupadoDTO respuesta = new AvatarCaracteristicaAgrupadoDTO();
                respuesta.Cabello= (from caracterista in caracteristicasTotal
                             where caracterista.TipoCaracteristica== "TOP"
                             orderby caracterista.Etiqueta
                             select new AvatarCaracteristicaDTO
                             {
                                 TipoCaracteristica=caracterista.TipoCaracteristica,
                                 Etiqueta=caracterista.Etiqueta,
                                 Valor=caracterista.Valor
                             }).ToList();
                respuesta.CabelloColor= (from caracterista in caracteristicasTotal
                                         where caracterista.TipoCaracteristica == "HAIRCOLOR"
                                         orderby caracterista.Etiqueta
                                         select new AvatarCaracteristicaDTO
                                         {
                                             TipoCaracteristica = caracterista.TipoCaracteristica,
                                             Etiqueta = caracterista.Etiqueta,
                                             Valor = caracterista.Valor
                                         }).ToList();
                respuesta.Barba= (from caracterista in caracteristicasTotal
                                  where caracterista.TipoCaracteristica == "FACIALHAIR"
                                  orderby caracterista.Etiqueta
                                  select new AvatarCaracteristicaDTO
                                  {
                                      TipoCaracteristica = caracterista.TipoCaracteristica,
                                      Etiqueta = caracterista.Etiqueta,
                                      Valor = caracterista.Valor
                                  }).ToList();
                respuesta.BarbaColor= (from caracterista in caracteristicasTotal
                                       where caracterista.TipoCaracteristica == "FACIALHAIRCOLOR"
                                       orderby caracterista.Etiqueta
                                       select new AvatarCaracteristicaDTO
                                       {
                                           TipoCaracteristica = caracterista.TipoCaracteristica,
                                           Etiqueta = caracterista.Etiqueta,
                                           Valor = caracterista.Valor
                                       }).ToList();
                respuesta.Mirada= (from caracterista in caracteristicasTotal
                                   where caracterista.TipoCaracteristica == "EYE"
                                   orderby caracterista.Etiqueta
                                   select new AvatarCaracteristicaDTO
                                   {
                                       TipoCaracteristica = caracterista.TipoCaracteristica,
                                       Etiqueta = caracterista.Etiqueta,
                                       Valor = caracterista.Valor
                                   }).ToList();
                respuesta.Cejas= (from caracterista in caracteristicasTotal
                                  where caracterista.TipoCaracteristica == "EYEBROW"
                                  orderby caracterista.Etiqueta
                                  select new AvatarCaracteristicaDTO
                                  {
                                      TipoCaracteristica = caracterista.TipoCaracteristica,
                                      Etiqueta = caracterista.Etiqueta,
                                      Valor = caracterista.Valor
                                  }).ToList();
                respuesta.Boca= (from caracterista in caracteristicasTotal
                                 where caracterista.TipoCaracteristica == "MOUTH"
                                 orderby caracterista.Etiqueta
                                 select new AvatarCaracteristicaDTO
                                 {
                                     TipoCaracteristica = caracterista.TipoCaracteristica,
                                     Etiqueta = caracterista.Etiqueta,
                                     Valor = caracterista.Valor
                                 }).ToList();
                respuesta.PielColor= (from caracterista in caracteristicasTotal
                                      where caracterista.TipoCaracteristica == "SKINCOLOR"
                                      orderby caracterista.Etiqueta
                                      select new AvatarCaracteristicaDTO
                                      {
                                          TipoCaracteristica = caracterista.TipoCaracteristica,
                                          Etiqueta = caracterista.Etiqueta,
                                          Valor = caracterista.Valor
                                      }).ToList();
                respuesta.Ropa= (from caracterista in caracteristicasTotal
                                 where caracterista.TipoCaracteristica == "CLOTHE"
                                 orderby caracterista.Etiqueta
                                 select new AvatarCaracteristicaDTO
                                 {
                                     TipoCaracteristica = caracterista.TipoCaracteristica,
                                     Etiqueta = caracterista.Etiqueta,
                                     Valor = caracterista.Valor
                                 }).ToList();
                respuesta.RopaColor= (from caracterista in caracteristicasTotal
                                      where caracterista.TipoCaracteristica == "CLOTHECOLOR"
                                      orderby caracterista.Etiqueta
                                      select new AvatarCaracteristicaDTO
                                      {
                                          TipoCaracteristica = caracterista.TipoCaracteristica,
                                          Etiqueta = caracterista.Etiqueta,
                                          Valor = caracterista.Valor
                                      }).ToList();
                respuesta.Accesorios= (from caracterista in caracteristicasTotal
                                       where caracterista.TipoCaracteristica == "ACCESSORIES"
                                       orderby caracterista.Etiqueta
                                       select new AvatarCaracteristicaDTO
                                       {
                                           TipoCaracteristica = caracterista.TipoCaracteristica,
                                           Etiqueta = caracterista.Etiqueta,
                                           Valor = caracterista.Valor
                                       }).ToList();
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 30/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene avatar por usuario
        /// </summary>
        /// <param name="Usuario"> Nombre de usuario </param>
        /// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
        public AvatarDTO ObtenerAvatar(string Usuario)
        {
            try
            {
                AvatarDTO avatar = _repAvatar.ObtenerAvatar(Usuario);
                if(avatar.Id == null)
                {
                    if (avatar.IdSexo == 2)
                    {
                        avatar.Top = "LongHairStraight";
                        avatar.Accessories = "Blank";
                        avatar.HairColor = "Brown";
                        avatar.FacialHair = "Blank";
                        avatar.FacialHairColor = "Brown";
                        avatar.Clothes = "ShirtScoopNeck";
                        avatar.ClothesColor = "Pink";
                        avatar.Eyes = "Default";
                        avatar.Eyesbrow = "Default";
                        avatar.Mouth = "Default";
                        avatar.Skin = "Light";
                    }
                    else
                    {
                        avatar.Top = "ShortHairTheCaesar";
                        avatar.Accessories = "Blank";
                        avatar.HairColor = "Auburn";
                        avatar.FacialHair = "Blank";
                        avatar.FacialHairColor = "Auburn";
                        avatar.Clothes = "CollarSweater";
                        avatar.ClothesColor = "Blue02";
                        avatar.Eyes = "Default";
                        avatar.Eyesbrow = "Default";
                        avatar.Mouth = "Default";
                        avatar.Skin = "Tanned";
                    }
                }
                return avatar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 30/07/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza el avatar del usuario
        /// </summary>
        /// <param name="Avatar"> Objeto con las caracteristicas del avatar </param>
        /// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
        public bool ActualizarAvatar(AvatarDTO Avatar)
        {
            try
            {
                var resultado=false;
                //AvatarBO nuevoAvatar = new AvatarBO();
                if (Avatar.Id == null)
                {
                    this.IdPersonal = (int)Avatar.IdPersonal;
                    this.Top =Avatar.Top;
                    this.Accessories =Avatar.Accessories ;
                    this.HairColor=Avatar.HairColor ;
                    this.FacialHair =Avatar.FacialHair;
                    this.FacialHairColor =Avatar.FacialHairColor;
                    this.Clothes =Avatar.Clothes;
                    this.Eyes =Avatar.Eyes;
                    this.Eyesbrow = Avatar.Eyesbrow;
                    this.Mouth = Avatar.Mouth ;
                    this.Skin = Avatar.Skin;
                    this.ClothesColor=Avatar.ClothesColor;
                    this.Estado = true;
                    this.UsuarioCreacion = Avatar.Usuario;
                    this.UsuarioModificacion = Avatar.Usuario;
                    this.FechaCreacion = DateTime.Now;
                    this.FechaModificacion = DateTime.Now;
                    resultado = _repAvatar.Insert(this);
                }
                else
                {
                    AvatarBO nuevoAvatar = _repAvatar.FirstById((int)Avatar.Id,_integraDBContext);
                    nuevoAvatar.Top = Avatar.Top;
                    nuevoAvatar.Accessories = Avatar.Accessories;
                    nuevoAvatar.HairColor = Avatar.HairColor;
                    nuevoAvatar.FacialHair = Avatar.FacialHair;
                    nuevoAvatar.FacialHairColor = Avatar.FacialHairColor;
                    nuevoAvatar.Clothes = Avatar.Clothes;
                    nuevoAvatar.Eyes = Avatar.Eyes;
                    nuevoAvatar.Eyesbrow = Avatar.Eyesbrow;
                    nuevoAvatar.Mouth = Avatar.Mouth;
                    nuevoAvatar.Skin = Avatar.Skin;
                    nuevoAvatar.ClothesColor = Avatar.ClothesColor;
                    nuevoAvatar.UsuarioModificacion = Avatar.Usuario;
                    nuevoAvatar.FechaModificacion = DateTime.Now;
                    resultado = _repAvatar.Update(nuevoAvatar);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
