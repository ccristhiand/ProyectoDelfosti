using Dapper;
using Entidades;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Model
{
    public interface IUser
    {
        Task<Usuario> Login(string correo, string password);
        Task<List<UsuarioGet>> Get(string rol);
    }
    public class User : IUser
    {
        private readonly IConfiguration _configuration;
        public User(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<UsuarioGet>> Get(string rol)
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    List<UsuarioGet> usuario = new List<UsuarioGet>();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(new { rol });
                    con.Open();
                    usuario = (await con.QueryAsync<UsuarioGet>("SP_LISTAR_USUARIOS", parameters, commandType: CommandType.StoredProcedure)).ToList();
                    return usuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Usuario> Login(string correo, string password)
        {
            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("Dev")))
                {
                    Usuario usuario = new Usuario();
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.AddDynamicParams(new { correo, password });
                    con.Open();
                    usuario = await con.QuerySingleOrDefaultAsync<Usuario>("SP_LOGIN", parameters, commandType: CommandType.StoredProcedure);
                    return usuario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
