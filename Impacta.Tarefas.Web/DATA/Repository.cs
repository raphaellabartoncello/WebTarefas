using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using Antlr.Runtime.Misc;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls;

namespace Impacta.Tarefas.Web
{
    public class Repository : IRepository
    {
        //Declarar os objetos que serão utilizados para conectar ao banco de dados e executar os comandos SQL Server
        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;

        //Criar um construtor e dentro dele vamos verificar se os objetos já existem em memória, caso não temos que instanciar
        //CODE SNIPPET para criar o construtor (CTOR)

        public Repository()
        {
            if (conn == null)
            {
                var stringConexao = ConfigurationManager.ConnectionStrings["ConexaoPessoalDB"].ConnectionString;

                conn = new SqlConnection(stringConexao);
            }

            if (comando == null)
            {
                comando = new SqlCommand();
            }
        }

        public bool Create(TarefasMOD tarefasMOD)
        {
            try
            {
                //Indica para o .Net que executará uma QUERY
                comando.CommandType = System.Data.CommandType.Text;

                //Informar o INSERT

                comando.CommandText = "INSERT INTO TAREFAS(NOME, PRIORIDADE, CONCLUIDA, OBSERVACOES) VALUES(@Nome, @Prioridade, @Concluida, @Observacoes)";

                //comando.Parameters.AddWithValue("@Nome", tarefasMOD.Nome.Trim().ToUpper());
                comando.Parameters.AddWithValue("@Nome", tarefasMOD.Nome);
                comando.Parameters.AddWithValue("@Prioridade", tarefasMOD.Prioridade);
                comando.Parameters.AddWithValue("@Concluida", tarefasMOD.Concluida);
                comando.Parameters.AddWithValue("@Observacoes", tarefasMOD.Observacoes);

                comando.Connection = conn;

                conn.Open();

                result = comando.ExecuteNonQuery() >= 1 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Para validadr se a conexão ainda continua aberta utilizamos o enumerador (verificar status da conexão)
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return result;
        }
        //public TarefasMOD Read(int id)
        //{
        //    TarefasMOD tarefa = null;

        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return 
        //}
        public bool Update(TarefasMOD tarefas)
        {
            int total = 0;

            try
            {
                string sql = @"UPDATE TAREFAS SET Nome=@Nome, Prioridade=@Prioridade, Concluida=@Concluida, Observacoes=@Observacoes WHERE Id=@Id";

                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoPessoalDB"].ConnectionString))
                {
                    comando.CommandText = sql;
                    comando.CommandType = CommandType.Text;

                    //SqlConnection
                    comando.Connection = conexao;

                    //parametros a serem trocados
                    comando.Parameters.AddWithValue("@Nome", tarefas.Nome);
                    comando.Parameters.AddWithValue("@Prioridade", tarefas.Prioridade);
                    comando.Parameters.AddWithValue("@Concluida", tarefas.Concluida);
                    comando.Parameters.AddWithValue("@Observacoes", tarefas.Observacoes);
                    comando.Parameters.AddWithValue("@Id", tarefas.Id);

                    conexao.Open();

                    //ID do registro afetado com a alteração
                    total = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }
        public int Delete(int id)
        {
            string sql = @"DELETE TAREFAS WHERE Id=@Id";

            int total = 0;

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoPessoalDB"].ConnectionString))
            {
                using (var cmd = new SqlCommand(sql, cn))
                {
                    //cmd.Parameters.AddWithValue("@Id, id");
                    cn.Open();
                    total = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }

            return total;
        }
        public List<TarefasMOD> ReadAll()
        {
            List<TarefasMOD> lista = new List<TarefasMOD>();
            try
            {
                //O using é uma boa prática para instanciar a SqlConnection - Automaticamente após a execução do bloco using a conexão com o banco de dados é encerrada.
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoPessoalDB"].ConnectionString))
                {
                    //Configuração do SqlCommand para execução do select
                    comando.Connection = conexao;

                    //Definir o tipo de comando a ser executado
                    comando.CommandType = CommandType.Text;

                    comando.CommandText = "SELECT * FROM TAREFAS ORDER BY PRIORIDADE";

                    //Abrir conexão com o banco de dados
                    conexao.Open();

                    using (var dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var tarefa = new TarefasMOD();
                            tarefa.Id = Convert.ToInt32(dr["Id"]);
                            tarefa.Nome = Convert.ToString(dr["Nome"].ToString());
                            tarefa.Prioridade = Convert.ToInt32(dr["Prioridade"]);
                            tarefa.Concluida = Convert.ToBoolean(dr["Concluida"]);
                            tarefa.Observacoes = Convert.ToString(dr["Observacoes"]);


                            lista.Add(tarefa);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lista;

        }

        bool IRepository.Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TarefasMOD Read(int id)
        {
            TarefasMOD tarefas = null;

            try
            {
                using (var conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoPessoalDB"].ConnectionString))
                {
                    comando.Connection = conexao;

                    //Define o tipo de comando a ser executado
                    comando.CommandType = CommandType.Text;

                    //Definição da query
                    comando.CommandText = @"SELECT Id, Nome, Prioridade, Concluida, Observacoes FROM TAREFAS WHERE ID=@ID";

                    comando.Parameters.AddWithValue("@ID", id);

                    conexao.Open();

                    using (var dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            tarefas = new TarefasMOD()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nome = Convert.ToString(dr["Nome"]),
                                Concluida = Convert.ToBoolean(dr["Concluida"]),
                                Prioridade = Convert.ToInt32(dr["Prioridade"]),
                                Observacoes = Convert.ToString(dr["Observacoes"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return tarefas;
        }
    }
}