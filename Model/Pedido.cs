using Dapper;
using Entidades;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Model
{
    public interface IPedido
    {
        Task<List<Result>> Post(Entidades.Pedido pedido);
        Task<Result> Patch(int Estado, int numeroPedido);
        Task<List<DetallePedido>> Get(int pedido);
    }
    public class Pedido : IPedido
    {
        private IConfiguration _configuration;
        public Pedido(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Result> Patch(int Estado, int numeroPedido)
        {
            try
            {
                using (var con =new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(new { Estado, numeroPedido });
                    con.Open();
                    var result =await con.QuerySingleOrDefaultAsync<Result>("SP_UPDATE_STATE_PEDIDO", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Agrega pedido mas el detalle del pedido,
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<List<Result>> Post(Entidades.Pedido pedido)
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(new { pedido.idVendedor, pedido.idRepartidor });
                    con.Open();
                    var result = await con.QuerySingleOrDefaultAsync<Result>("SP_ADD_PEDIDO", parameters, commandType: CommandType.StoredProcedure);
                    List<Result> resultados = new List<Result>();
                    resultados.Add(result);
                    int ultimopedido =int.Parse(UltimoPedido());

                    foreach (var item in pedido.productosPedidos)
                    {
                        var resultDetallePedido = PostDetallePedido(ultimopedido, item.sku, item.cant);
                        resultados.Add(resultDetallePedido);
                    } 
                    return resultados;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UltimoPedido()
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    con.Open();
                    var result = con.QueryFirstOrDefault<string>("SELECT top 1 numeroPedido from Pedido order by 1 desc");

                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        } 
        
        public Result PostDetallePedido(int numeroPedido, int idproducto, int cantidad)
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(new { numeroPedido, idproducto, cantidad });
                    con.Open();
                    var result = con.QuerySingleOrDefault<Result>("SP_ADD_DETALLE_PEDIDO", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DetallePedido>> Get(int pedido)
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    List<DetallePedido> detallePedido = new List<DetallePedido>();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(new { pedido });
                    con.Open();
                    detallePedido =( await con.QueryAsync<DetallePedido>("SP_LISTAR_DETALLE_PEDIDO", parameters, commandType: CommandType.StoredProcedure)).ToList();
                    return detallePedido;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
