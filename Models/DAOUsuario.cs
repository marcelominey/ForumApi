using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ForumApi.Models
{
    public class DAOUsuario:Conexao
    {
        public bool Cadastro(Usuario usuario){
            bool retorno = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                string inserir = "INSERT INTO tbl_usuario(nome,login,senha)" + 
                                 "values(@n,@l,@s)";
                cmd.Parameters.AddWithValue("@n",usuario.Nome);
                cmd.Parameters.AddWithValue("@l",usuario.Login);
                cmd.Parameters.AddWithValue("@s",usuario.Senha);

                con.ConnectionString = Caminho();
                con.Open();
                cmd.Connection = con;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = inserir;

                int rs = cmd.ExecuteNonQuery();

                if(rs > 0)
                    retorno = true;

            }
            catch(SqlException se){
                throw new Exception("Erro ao tentar cadastrar -> "+se.Message);
            }
            finally{
                con.Close();
            }
            return retorno;
        }
        public bool Atualizar(Usuario usuario){
            bool retorno = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                string atualizar = "UPDATE tbl_usuario SET (nome=@n,login=@l,senha=@s,data)" + 
                                   "values(@n,@l,@s)";
                cmd.Parameters.AddWithValue("@n",usuario.Nome);
                cmd.Parameters.AddWithValue("@l",usuario.Login);
                cmd.Parameters.AddWithValue("@s",usuario.Senha);
                cmd.Parameters.AddWithValue("@d",usuario.DataCadastro);
                cmd.Parameters.AddWithValue("@id",usuario.Idusuario);

                con.ConnectionString = Caminho();
                con.Open();
                cmd.Connection = con;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = atualizar;

                int rs = cmd.ExecuteNonQuery();

                if(rs > 0)
                    retorno = true;

            }
            catch(SqlException se){
                throw new Exception("Erro ao tentar atualizar -> "+se.Message);
            }
            finally{
                con.Close();
            }
            return retorno;
        }
        public bool Apagar(int id){
            bool retorno = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                string apagar = "DELETE FROM tbl_usuario WHERE id=@id";
                cmd.Parameters.AddWithValue("@id",id);
                
                con.ConnectionString = Caminho();
                con.Open();
                cmd.Connection = con;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = apagar;

                int rs = cmd.ExecuteNonQuery();

                if(rs > 0)
                    retorno = true;

            }
            catch(SqlException se){
                throw new Exception("Erro ao tentar inserir -> "+se.Message);
            }
            finally{
                con.Close();
            }
            return retorno;
        }
        public List<Usuario> ListarUsuario(){
            List<Usuario> lstUsuario = new List<Usuario>();
            try{
                //con = new SqlConnection(); //SqlConnection tem dois construtores. Vai fazer "por fora" msm
                //con.ConnectionString = conexao;
                con = new SqlConnection(Caminho()); //um dos construtores dele já é essa string, já posso passar direto aqui na instância
                con.Open();
                              
                //cmd = new SqlCommand();
                //cmd.Connection = con;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = " Select * from tbl_usuario"; 
                cmd = new SqlCommand("SELECT * FROM tbl_usuario", con);
                //como to passando ele como texto ali, ele faz uma verificação interna praq saber se é text, etc
                
                sdr = cmd.ExecuteReader();
                //Qual a diferença entre ExecuteNonQuery e ExecuteReader? 
                //Ele executa, mas não faz a consulta na base, não retorna, retorna um valor int ("x linhas foram afetadas").
                //Usamos no delete, insert, update, etc)
                //Lê os dados na tabela e traz pra cá, é um leitor. Pede pra executar a leitura dos dados, coloca dentro de
                //um SQL DataReader, um cara capaz de ler esses dados


                while(sdr.Read()){ //ENQUANTO TIVER LINHA/CONTEÚDO PARA LER EM sdr
                    
                    lstUsuario.Add(new Usuario(){
                        Idusuario=sdr.GetInt32(0), 
                        Nome=sdr.GetString(1), 
                        Login = sdr.GetString(2), 
                        Senha = sdr.GetString(3), 
                        DataCadastro = sdr.GetDateTime(4)
                    });
                    //CLASSE LSTUSUARIO: MEIO DE PASSAGEM DE DADOS
                    //ADICIONANDO NESSA LISTA UM NOVO USUARIO, GERADO ANONIMAMENTE(?)
                    //COLOCA OS DADOS DENTRO DELE
                    //ADICIONA NA LISTA (LISTA QUE SÓ RECEBE USUARIOS).
                }
            }
            catch(SqlException se){
                throw new Exception("Erro ao tentar ler a tabela tbl_usuario-> "+se.Message);
            }
            catch(Exception ex){
                throw new Exception("Erro inesperado-> "+ex.Message);
            }
            finally{
                con.Close();
            }
            return lstUsuario;
        }
    }
}