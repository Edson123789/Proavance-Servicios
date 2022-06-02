using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Persistencia.SCode.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private int _timeout = 20 * 60;
        private integraDBContext _context;

        public DapperRepository()
        {
            _context = new integraDBContext();
        }

        public DapperRepository(integraDBContext context)
        {
            _context = context;
        }


        public string QueryDapper(string sql, object parametros)
        {
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.Text, commandTimeout: _timeout).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QueryDapper(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.Text, commandTimeout: timeout).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string FirstOrDefault(string sql, object parametros)
        {
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, param: parametros,
                commandType: CommandType.Text, commandTimeout: _timeout).FirstOrDefault();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string FirstOrDefault(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql, param: parametros,
                commandType: CommandType.Text, commandTimeout: timeout).FirstOrDefault();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QuerySPDapper(string sql, object parametros)
        {
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).ToList();
            
            //IList<dynamic> result = _context.Database.GetDbConnection()
            //    .Query<dynamic>(sql, parametros, commandType: CommandType.StoredProcedure).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }
        public string QuerySPDapper(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            IList<dynamic> result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout).ToList();

            //IList<dynamic> result = _context.Database.GetDbConnection()
            //    .Query<dynamic>(sql, parametros, commandType: CommandType.StoredProcedure).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }

        public string QuerySPFirstOrDefault(string sql, object parametros)
        {
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: _timeout).FirstOrDefault();

            //IList<dynamic> result = _context.Database.GetDbConnection()
            //    .Query<dynamic>(sql, parametros, commandType: CommandType.StoredProcedure).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }
        public string QuerySPFirstOrDefault(string sql, object parametros, int timeoutMinutos)
        {
            int timeout = ConvertirTimeOutSegundos(timeoutMinutos);
            dynamic result = SqlMapper.Query<dynamic>(_context.Database.GetDbConnection(), sql,
                param: parametros, commandType: CommandType.StoredProcedure, commandTimeout: timeout).FirstOrDefault();

            //IList<dynamic> result = _context.Database.GetDbConnection()
            //    .Query<dynamic>(sql, parametros, commandType: CommandType.StoredProcedure).ToList();

            var jsonResultado = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return jsonResultado;
        }


        private int ConvertirTimeOutSegundos(int timeoutMinutos)
        {
            return timeoutMinutos * 60;
        }
    }
}
