using Microsoft.EntityFrameworkCore;
using Siace.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Siace.Data.Repository
{    public class Question
    {
        public int PRE_ID { get; set; }
        public string PRE_PREGUNTA { get; set; }
        public bool PRE_ACTIVO { get; set; }
        public int PRE_TIC_ID { get; set; }
        public int PRE_ENC_ID { get; set; }
        public string PRE_TIPO_DATO { get; set; }
        public string PRE_RANGO_INI { get; set; }
        public string PRE_RANGO_FIN { get; set; }
        public int? PRE_PRE_ID_TRIGGER { get; set; }
        public int? PRE_RES_ID_TRIGGER { get; set; }
        public string PRE_CLAVE_COMPUESTA { get; set; }
        public int? PRE_NO_SABE { get; set; }
        public bool? PRE_VALORACION { get; set; }
        public int? PRE_TIP_ID { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }
    }

    public class Respuesta
    {
        public int RES_ID { get; set; }
        public int RES_PRE_ID { get; set; }
        public string RES_VALOR { get; set; }
        public int? RES_VALOR_EVALUACION { get; set; }

        public Pregunta Pregunta { get; set; }
    }

    public class Respuestaa
    {
        public int resId { get; set; }
        public string resValor { get; set; }
    }

    public class ContestacionRespuesta
    {
        public int corId { get; set; }
        public int corResId { get; set; }
        public int corPreId { get; set; }
        public string corValor { get; set; }
        public string corImagen { get; set; }
        public bool corNoContesto { get; set; }

        public int corConId { get; set; }
    }

    public class Contestacion
    {
        public List<ContestacionRespuesta> contestacionesRespuestas { get; set; }
    }

    public class Preguntaa
    {
        public int preId { get; set; }
        public string prePregunta { get; set; }
        public List<Respuestaa> respuesta { get; set; }
        public List<ContestacionRespuesta> contestaciones { get; set; }
        public int? prePreIdTrigger { get; set; }
        public int? preResIdTrigger { get; set; }
        public int preTipId { get; set; }
        public int prePilId { get; set; }
    }


    public class PreguntasRepository:IPreguntasRepository
    {
        private SiaceDbContext context; 

        public PreguntasRepository(SiaceDbContext context)
        {
            this.context = context;     
        }

 
        public async Task<Pregunta> Get(int id)
        {
            var query = from pregunta in context.Preguntas
                        where pregunta.PreId == id
                        select new Pregunta
                        {
                            PreId = pregunta.PreId,
                            PrePregunta = pregunta.PrePregunta,
                            Respuesta = (from r in context.Respuestas
                                         where r.ResPreId == pregunta.PreId
                                         select r).ToList(),
                            PrePreIdTrigger = pregunta.PrePreIdTrigger,
                            PreResIdTrigger = pregunta.PreResIdTrigger,
                            PreTipId = pregunta.PreTipId,
                            PrePilId = pregunta.PrePilId,
                        };  

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<dynamic>> GetAllQuestions(int conId)
        {
            var query = from pregunta in context.Preguntas
                        select new
                        {
                            pregunta.PreId,
                            pregunta.PrePregunta,
                            Respuesta = (from r in context.Respuestas
                                         where r.ResPreId == pregunta.PreId
                                         select new
                                         {
                                             ResId = r.ResId,
                                             ResPreId = r.ResPreId,
                                             ResValor = r.ResValor,
                                             ResValorEvaluacion = r.ResValorEvaluacion,
                                             ContestacionesRespuesta = (from c in context.Contestaciones
                                                                        join cr in context.ContestacionesRespuestas
                                                                        on c.ConId equals cr.CorConId
                                                                        where c.ConEncId == pregunta.PreEncId && c.ConUsrId == 1 && pregunta.PreId == cr.CorPreId
                                                                           
                                                                        select new
                                                                        {
                                                                            cr.CorId,
                                                                            cr.CorResId,
                                                                            cr.CorPreId,
                                                                            cr.CorValor,
                                                                            cr.CorImagen,
                                                                            cr.CorNoContesto
                                                                        }).ToList(),
                                             ResPre = r.ResPre
                                         }).ToList(),
                            Contestaciones = (from c in context.Contestaciones
                                              join cr in context.ContestacionesRespuestas
                                              on c.ConId equals cr.CorConId
                                              where c.ConEncId == pregunta.PreEncId && c.ConUsrId == 1 && pregunta.PreId == cr.CorPreId
                                                 && c.ConId == conId
                                              select new
                                              {
                                                  cr.CorId,
                                                  cr.CorResId,
                                                  cr.CorPreId,
                                                  cr.CorValor,
                                                  cr.CorImagen,
                                                  cr.CorNoContesto
                                              }).ToList(),
                            pregunta.PrePreIdTrigger,
                            pregunta.PreResIdTrigger,
                            pregunta.PreTipId,
                            pregunta.PrePilId
                        };

            return await query.ToListAsync();
        }

        public async Task<int?> CreateUpdate(IEnumerable<Preguntaa> preguntas, int corConId)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (corConId == 0)
                    {
                        var nuevaContestacion = new Contestacione
                        {
                            ConUsrId = 1,
                            ConEncId = 1,
                            ConFecha = DateTime.Now,
                            ConNombre = "Contestacion",
                            ConFechaFin = DateTime.Now,
                        };
                        context.Contestaciones.Add(nuevaContestacion);
                        context.SaveChanges();

                        corConId = nuevaContestacion.ConId; 
                    }

              
                        var enviadosCorIds = preguntas.SelectMany(p => p.contestaciones.Select(c => c.corId)).ToList();

                        // Eliminar contestaciones que no están en la lista de enviados
                        var contestacionesParaEliminar = context.ContestacionesRespuestas
                            .Where(cr => !enviadosCorIds.Contains(cr.CorId) && cr.CorConId == corConId)
                            .ToList();

                        context.ContestacionesRespuestas.RemoveRange(contestacionesParaEliminar);
                    

                    foreach (var pregunta in preguntas)
                    {
                        foreach (var contestacionData in pregunta.contestaciones)
                        {
                            try
                            {
                                // Buscar la contestación respuesta por CorId
                                var contestacionRespuesta = context.ContestacionesRespuestas
                                    .FirstOrDefault(cr => cr.CorId == contestacionData.corId && cr.CorConId == corConId);

                                // Si no se encuentra la contestación respuesta, se crea un nuevo objeto
                                if (contestacionRespuesta == null)
                                {
                                    contestacionRespuesta = new ContestacionesRespuesta
                                    {
                                        CorResId = contestacionData.corResId,
                                        CorPreId = contestacionData.corPreId,
                                        CorValor = contestacionData.corValor,
                                        CorConId = corConId, // Usar el valor recibido como parámetro
                                        CorImagen = Encoding.UTF8.GetBytes("Imagen en formato de texto"),
                                        CorNoContesto = false
                                    };
                                    context.ContestacionesRespuestas.Add(contestacionRespuesta);
                                }
                                else
                                {
                                    contestacionRespuesta.CorResId = contestacionData.corResId;
                                    contestacionRespuesta.CorValor = contestacionData.corValor;

                                    context.ContestacionesRespuestas.Update(contestacionRespuesta);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error al buscar o manipular la contestación respuesta: {ex.Message}");
                                throw;
                            }
                        }
                    }

                    // Guardar todos los cambios en la base de datos
                    context.SaveChanges();
                    transactionScope.Complete(); // Completar la transacción si todo fue exitoso

                    return corConId;
                }
                catch (Exception ex)
                {
                    // Manejar excepción general del método SaveUpdate
                    Console.WriteLine($"Error al guardar los datos: {ex.Message}");
                    throw; // Relanzar la excepción para manejarla en un nivel superior si es necesario
                }
            }
        }

    }
}
