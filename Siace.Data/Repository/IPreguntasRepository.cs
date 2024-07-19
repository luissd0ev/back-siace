using Siace.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siace.Data.Repository
{
    public interface IPreguntasRepository
    {
       
        public Task<Pregunta> Get(int id);
        public Task<IEnumerable<dynamic>> GetAllQuestions(int conId);
        public Task<int?> CreateUpdate(IEnumerable<Preguntaa> preguntas, int corConId);
    }
}
