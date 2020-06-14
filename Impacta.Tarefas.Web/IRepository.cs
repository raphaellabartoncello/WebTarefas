using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impacta.Tarefas.Web
{
    interface IRepository
    {
        //CRUD
        bool Create(TarefasMOD tarefasMOD);
        TarefasMOD Read(int id);
        bool Update(TarefasMOD tarefasMOD);
        bool Delete(int id);

        //Devolve todas as tarefas
        List<TarefasMOD> ReadAll();
    }
}
