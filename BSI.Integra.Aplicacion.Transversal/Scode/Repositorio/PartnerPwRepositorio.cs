using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/PartnerPw
    /// Autor: Johan Cayo - Wilber Choque - Ansoli Espinoza - Esthephany Tanco - Jorge Rivera - Gian Miranda.
    /// Fecha: 22/06/2021
    /// <summary>
    /// Gestión de Examenes tabla T_PartnerPw
    /// </summary>
    public class PartnerPwRepositorio : BaseRepository<TPartnerPw, PartnerPwBO>
    {
        #region Metodos Base
        public PartnerPwRepositorio() : base()
        {
        }
        public PartnerPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PartnerPwBO> GetBy(Expression<Func<TPartnerPw, bool>> filter)
        {
            IEnumerable<TPartnerPw> listado = base.GetBy(filter);
            List<PartnerPwBO> listadoBO = new List<PartnerPwBO>();
            foreach (var itemEntidad in listado)
            {
                PartnerPwBO objetoBO = Mapper.Map<TPartnerPw, PartnerPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PartnerPwBO FirstById(int id)
        {
            try
            {
                TPartnerPw entidad = base.FirstById(id);
                PartnerPwBO objetoBO = new PartnerPwBO();
                Mapper.Map<TPartnerPw, PartnerPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PartnerPwBO FirstBy(Expression<Func<TPartnerPw, bool>> filter)
        {
            try
            {
                TPartnerPw entidad = base.FirstBy(filter);
                PartnerPwBO objetoBO = Mapper.Map<TPartnerPw, PartnerPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PartnerPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPartnerPw entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<PartnerPwBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(PartnerPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPartnerPw entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<PartnerPwBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TPartnerPw entidad, PartnerPwBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TPartnerPw MapeoEntidad(PartnerPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPartnerPw entidad = new TPartnerPw();
                entidad = Mapper.Map<PartnerPwBO, TPartnerPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
        /// <summary>
        /// Obtiene una lista de partner con el Id y Nombre para los comboBox
        /// </summary>
        /// <returns></returns>
        public List<PartnerFiltroDTO> ObtenerPartnerFiltro()
        {
            try
            {
                List<PartnerFiltroDTO> partenerFiltro = new List<PartnerFiltroDTO>();
                string _queryPartner = string.Empty;
                _queryPartner = "SELECT Id,Nombre FROM pla.V_TPartnerPW_Filtro WHERE Estado=1";
                var queryPartner = _dapper.QueryDapper(_queryPartner, null);
                if (!string.IsNullOrEmpty(queryPartner) && !queryPartner.Contains("[]"))
                {
                    partenerFiltro = JsonConvert.DeserializeObject<List<PartnerFiltroDTO>>(queryPartner);
                }
                return partenerFiltro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Obtiene el partner  Anterior por Id Actual Partner
        /// </summary>
        /// <returns></returns>
        public int ObtenerPartnerAnterior(int idPartner)
        {
            try
            {
                int partenerAnterior = 0;
                string _queryPartner = string.Empty;
                _queryPartner = "SELECT IdActualPartner,IdAnteriorPartner FROM pla.V_ObtenerPartnerAnterior WHERE IdActualPartner= @idPartner";
                var queryPartner = _dapper.FirstOrDefault(_queryPartner, new { idPartner });
                if (!queryPartner.Equals("null"))
                {
                    var partner = JsonConvert.DeserializeObject<PartnerTroncalAnteriorDTO>(queryPartner);
                    partenerAnterior = partner.IdAnteriorPartner;
                }
                return partenerAnterior;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<PartnerPwDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new PartnerPwDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    ImgPrincipal = y.ImgPrincipal,
                    ImgPrincipalAlf = y.ImgPrincipalAlf,
                    ImgSecundaria = y.ImgSecundaria,
                    ImgSecundariaAlf = y.ImgSecundariaAlf,
                    Descripcion = y.Descripcion,
                    DescripcionCorta = y.DescripcionCorta,
                    Preguntas = y.Preguntas,
                    Posicion = y.Posicion,
                    IdPartner = y.IdPartner,
                    EncabezadoCorreoPartner = y.EncabezadoCorreoPartner,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public class PartnerBeneficioPwRepositorio : BaseRepository<TPartnerBeneficioPw, PartnerBeneficioPwBO>
    {
        #region Metodos Base
        public PartnerBeneficioPwRepositorio() : base()
        {
        }
        public PartnerBeneficioPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PartnerBeneficioPwBO> GetBy(Expression<Func<TPartnerBeneficioPw, bool>> filter)
        {
            IEnumerable<TPartnerBeneficioPw> listado = base.GetBy(filter);
            List<PartnerBeneficioPwBO> listadoBO = new List<PartnerBeneficioPwBO>();
            foreach (var itemEntidad in listado)
            {
                PartnerBeneficioPwBO objetoBO = Mapper.Map<TPartnerBeneficioPw, PartnerBeneficioPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PartnerBeneficioPwBO FirstById(int id)
        {
            try
            {
                TPartnerBeneficioPw entidad = base.FirstById(id);
                PartnerBeneficioPwBO objetoBO = new PartnerBeneficioPwBO();
                Mapper.Map<TPartnerBeneficioPw, PartnerBeneficioPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PartnerBeneficioPwBO FirstBy(Expression<Func<TPartnerBeneficioPw, bool>> filter)
        {
            try
            {
                TPartnerBeneficioPw entidad = base.FirstBy(filter);
                PartnerBeneficioPwBO objetoBO = Mapper.Map<TPartnerBeneficioPw, PartnerBeneficioPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PartnerBeneficioPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPartnerBeneficioPw entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<PartnerBeneficioPwBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(PartnerBeneficioPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPartnerBeneficioPw entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<PartnerBeneficioPwBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TPartnerBeneficioPw entidad, PartnerBeneficioPwBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TPartnerBeneficioPw MapeoEntidad(PartnerBeneficioPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPartnerBeneficioPw entidad = new TPartnerBeneficioPw();
                entidad = Mapper.Map<PartnerBeneficioPwBO, TPartnerBeneficioPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>

        public List<PartnerBeneficioPwDTO> ObtenerPartnerBeneficio(int idPartner)
        {
            try
            {
                List<PartnerBeneficioPwDTO> _registros = new List<PartnerBeneficioPwDTO>();
                string _queryPartner = string.Empty;
                _queryPartner = "SELECT Id,IdPartner,Descripcion,Estado FROM pla.T_PartnerBeneficio_PW WHERE Estado=1 AND IdPartner= @idPartner";
                var queryPartner = _dapper.QueryDapper(_queryPartner, new { idPartner });
                if (!queryPartner.Equals("null"))
                {
                    _registros = JsonConvert.DeserializeObject<List<PartnerBeneficioPwDTO>>(queryPartner);
                }
                return _registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

    public class PartnerContactoPwRepositorio : BaseRepository<TPartnerContactoPw, PartnerContactoPwBO>
    {
        #region Metodos Base
        public PartnerContactoPwRepositorio() : base()
        {
        }
        public PartnerContactoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PartnerContactoPwBO> GetBy(Expression<Func<TPartnerContactoPw, bool>> filter)
        {
            IEnumerable<TPartnerContactoPw> listado = base.GetBy(filter);
            List<PartnerContactoPwBO> listadoBO = new List<PartnerContactoPwBO>();
            foreach (var itemEntidad in listado)
            {
                PartnerContactoPwBO objetoBO = Mapper.Map<TPartnerContactoPw, PartnerContactoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PartnerContactoPwBO FirstById(int id)
        {
            try
            {
                TPartnerContactoPw entidad = base.FirstById(id);
                PartnerContactoPwBO objetoBO = new PartnerContactoPwBO();
                Mapper.Map<TPartnerContactoPw, PartnerContactoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PartnerContactoPwBO FirstBy(Expression<Func<TPartnerContactoPw, bool>> filter)
        {
            try
            {
                TPartnerContactoPw entidad = base.FirstBy(filter);
                PartnerContactoPwBO objetoBO = Mapper.Map<TPartnerContactoPw, PartnerContactoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PartnerContactoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPartnerContactoPw entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<PartnerContactoPwBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(PartnerContactoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPartnerContactoPw entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<PartnerContactoPwBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TPartnerContactoPw entidad, PartnerContactoPwBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TPartnerContactoPw MapeoEntidad(PartnerContactoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPartnerContactoPw entidad = new TPartnerContactoPw();
                entidad = Mapper.Map<PartnerContactoPwBO, TPartnerContactoPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        public List<PartnerContactoPwDTO> ObtenerPartnerContacto(int idPartner)
        {
            try
            {
                List<PartnerContactoPwDTO> _registros = new List<PartnerContactoPwDTO>();
                string _queryPartner = string.Empty;
                _queryPartner = "SELECT Id,IdPartner,Nombres,Apellidos,Email1,Email2,Telefono1,Telefono2,Estado FROM pla.T_PartnerContacto_PW WHERE Estado=1 AND IdPartner= @idPartner";
                var queryPartner = _dapper.QueryDapper(_queryPartner, new { idPartner });
                if (!queryPartner.Equals("null"))
                {
                    _registros = JsonConvert.DeserializeObject<List<PartnerContactoPwDTO>>(queryPartner);
                }
                return _registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
