using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Data;


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

                comando.Parameters.AddWithValue("@Nome", tarefasMOD.Nome.Trim().ToUpper());
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
        public TarefasMOD Read(int id)
        {
            throw new NotImplementedException();
        }
        public bool Update(TarefasMOD tarefasMOD)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public List<TarefasMOD> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}