using Dapper;
using Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IProducto
    {
        Task<List<Entidades.Producto>> Get(int? sku);
    }
    public class Producto : IProducto
    {
        private readonly IConfiguration _configuration;
        public Producto(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Entidades.Producto>> Get(int? sku)
        {
            try
            {
                
                using (var con = new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    List<Entidades.Producto> Producto = new List<Entidades.Producto>();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(new { sku });
                    con.Open();
                    Producto = (await con.QueryAsync<Entidades.Producto>("SP_LISTAR_PRODUCTOS", parameters, commandType: CommandType.StoredProcedure)).ToList();
                    return Producto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
