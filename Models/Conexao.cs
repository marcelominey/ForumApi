using System.Data.SqlClient;

namespace ForumApi.Models
{
    public abstract class Conexao
    {
        /// <summary>
        /// Objeto utilizado para estabelecer a conexão com o 
        /// servidor de banco de dados SQLExpress
        /// </summary>
        protected SqlConnection con = null;
        /// <summary>
        /// Objeto utilizado para executar comandos de SQL, tais como:
        /// SELECT, UPDATE, DELETE, INSERT, etc
        /// </summary>
        protected SqlCommand cmd = null;
        /// <summary>
        /// Objeto utilizado para guardar os retornos do SELECT realizados
        /// nas tabelas do banco de dados 
        /// </summary>
        protected SqlDataReader sdr = null;
        
        /// <summary>
        /// O método Caminho retorna o local do banco de dados.
        /// </summary>
        ///<returns>Retorna uma string de conexão com o banco</returns>
        protected static string Caminho(){
            //return @"Data Source=.\sqlexpress;initial catalog=Forum/user id=sa;password=senai@132";
            return @"Data Source=.\sqlexpress;Initial Catalog=Forum;user id=sa;password=senai@123"; 
        }

    }
}