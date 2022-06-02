using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: PersonalArchivoRepositorio
	/// Autor: Edgar Serruto
	/// Fecha: 16/08/2021
	/// <summary>
	/// Repositorio para de tabla T_PersonalArchivo
	/// </summary>
	public class PersonalArchivoRepositorio : BaseRepository<TPersonalArchivo, PersonalArchivoBO>
	{
		#region Metodos Base
		public PersonalArchivoRepositorio() : base()
		{
		}
		public PersonalArchivoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PersonalArchivoBO> GetBy(Expression<Func<TPersonalArchivo, bool>> filter)
		{
			IEnumerable<TPersonalArchivo> listado = base.GetBy(filter);
			List<PersonalArchivoBO> listadoBO = new List<PersonalArchivoBO>();
			foreach (var itemEntidad in listado)
			{
				PersonalArchivoBO objetoBO = Mapper.Map<TPersonalArchivo, PersonalArchivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PersonalArchivoBO FirstById(int id)
		{
			try
			{
				TPersonalArchivo entidad = base.FirstById(id);
				PersonalArchivoBO objetoBO = new PersonalArchivoBO();
				Mapper.Map<TPersonalArchivo, PersonalArchivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PersonalArchivoBO FirstBy(Expression<Func<TPersonalArchivo, bool>> filter)
		{
			try
			{
				TPersonalArchivo entidad = base.FirstBy(filter);
				PersonalArchivoBO objetoBO = Mapper.Map<TPersonalArchivo, PersonalArchivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PersonalArchivoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPersonalArchivo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PersonalArchivoBO> listadoBO)
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

		public bool Update(PersonalArchivoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPersonalArchivo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PersonalArchivoBO> listadoBO)
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
		private void AsignacionId(TPersonalArchivo entidad, PersonalArchivoBO objetoBO)
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

		private TPersonalArchivo MapeoEntidad(PersonalArchivoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPersonalArchivo entidad = new TPersonalArchivo();
				entidad = Mapper.Map<PersonalArchivoBO, TPersonalArchivo>(objetoBO,
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
		/// Repositorio: PersonalArchivoRepositorio
		/// Autor: Edgar Serruto
		/// Fecha: 16/08/2021
		/// <summary>
		/// Sube el archivo al blobstorage
		/// </summary>
		/// <param name="archivo">Byte de archivos</param>
		/// <param name="tipo">Tipo de archivo</param>
		/// <param name="nombreArchivo">Nombre de Archivo</param>
		/// <returns>string</returns>
		public string SubirDocumentosPersonal(byte[] archivo, string tipo, string nombreArchivo)
		{
			try
			{
				string nombreLink = string.Empty;

				try
				{
					string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
					string direccionBlob = @"repositorioweb/archivospersonal/";

					//Generar entrada al blob storage
					CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureStorageConnectionString);
					CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
					CloudBlobContainer container = blobClient.GetContainerReference(direccionBlob);

					CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
					blockBlob.Properties.ContentType = tipo;
					blockBlob.Metadata["filename"] = nombreArchivo;
					blockBlob.Metadata["filemime"] = tipo;
					Stream stream = new MemoryStream(archivo);
					var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

					objRegistrado.Wait();
					var correcto = objRegistrado.IsCompletedSuccessfully;

					if (correcto)
					{
						nombreLink = "https://repositorioweb.blob.core.windows.net/" + direccionBlob + nombreArchivo.Replace(" ", "%20");
					}
					else
					{
						nombreLink = "";
					}
					return nombreLink;
				}
				catch (Exception ex)
				{
					return "";
				}
			}
			catch (Exception e)
			{
				return "";
			}
		}
	}
}
