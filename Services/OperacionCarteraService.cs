using Finanzas.Domain.Models;
using Finanzas.Domain.Persistence.Repositories;
using Finanzas.Domain.Services;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Services
{
    public class OperacionCarteraService : IOperacionCarteraService
    {
        private readonly IOperacionCarteraRepository _operacionCarteraRepository;
        private readonly ILetraRepository _letraRepository;
        private readonly IOperacionRepository _operacionRepository;
        private readonly IOperacionLetraRepository _operacionLetraRepository;
        private readonly IOperacionLetraService _operacionLetraService;
        private readonly IUnitOfWork _unitOfWork;

        public OperacionCarteraService(IOperacionCarteraRepository operacionCarteraRepository, IUnitOfWork unitOfWork, ILetraRepository letraRepository, IOperacionLetraRepository operacionLetraRepository, IOperacionLetraService operacionLetraService, IOperacionRepository operacionRepository)
        {
            _operacionCarteraRepository = operacionCarteraRepository;
            _unitOfWork = unitOfWork;
            _letraRepository = letraRepository;
            _operacionLetraRepository = operacionLetraRepository;
            _operacionLetraService = operacionLetraService;
            _operacionRepository = operacionRepository;
        }

        public async Task<OperacionCarteraResponse> AssignOperacionCarteraAsync(int operacionId, int carteraId)
        {
            var existingOperacion = await _operacionRepository.FindByIdAsync(operacionId);
            try
            {
                //Por Calcular
                float valorRecibidoTotal = 0;
                //TCEA Cartera
                float TCEACartera = 0;

                IEnumerable<Letra> letras = await _letraRepository.ListByCarteraId(carteraId);
                if (letras == null)
                {
                    await _operacionCarteraRepository.AssignOperacionCartera(operacionId, carteraId, valorRecibidoTotal, TCEACartera);
                    await _unitOfWork.CompleteAsync();

                    OperacionCartera operacionCartera1 = await _operacionCarteraRepository.FindByOperacionIdAndCarteraId(operacionId, carteraId);

                    return new OperacionCarteraResponse(operacionCartera1);

                }
                List<Letra> letrasList = letras.ToList();

                DateTime[] dates = new DateTime[letrasList.Count + 1];
                double[] flujos = new double[letrasList.Count + 1];

                letrasList.ForEach(async l =>
                {
                    await _operacionLetraService.AssignOperacionLetraAsync(operacionId, l.Id);
                });


                letrasList.ForEach(async l =>
                {
                    OperacionLetra operacionLetra=await _operacionLetraRepository.FindByLetraIdAndOperacionId(operacionId,l.Id);
                    valorRecibidoTotal += operacionLetra.ValorRecibido;
                });

                dates[0] = existingOperacion.FechaDescuento;
                flujos[0] = valorRecibidoTotal;
                int contador = 1;

                letrasList.ForEach(l =>
                {
                    OperacionLetra operacionLetra = _operacionLetraService.GetByLetraIdAndOperacionAsync(l.Id, operacionId).Result.Resource;
                    dates[contador] = l.FechaVencimiento;
                    flujos[contador] = operacionLetra.Flujo;
                    contador += 1;
                });
                
                if (existingOperacion.AñoCalendario)
                {
                    TCEACartera = (float)TIRNOPER(dates, flujos, letrasList.Count + 1, 365);
                }
                else
                {
                    TCEACartera = (float)TIRNOPER(dates, flujos, letrasList.Count + 1, 360);
                }
                TCEACartera = (float)Decimal.Round((decimal)TCEACartera, 7);

                await _operacionCarteraRepository.AssignOperacionCartera(operacionId, carteraId, valorRecibidoTotal, TCEACartera);
                await _unitOfWork.CompleteAsync();

                OperacionCartera operacionCartera = await _operacionCarteraRepository.FindByOperacionIdAndCarteraId(operacionId, carteraId);

                return new OperacionCarteraResponse(operacionCartera);
            }
            catch (Exception ex)
            {
                return new OperacionCarteraResponse($"Error al asignar la operacion con la cartera: {ex.Message}");
            }
        }
        private double TIRNOPER(DateTime[] Fecha, double[] Flujo, int N, double NDxA)
        {
            const double Delta = 0.00000001; //Número pequeño
            double TIR, //TIR = x
                VA, //VAN = 0
                Maximo = 11, //Esto es para tantear
                Minimo = -1; //Esto es para tantear
             do
            {
                VA = 0;
                TIR = (Maximo + Minimo) / 2;

                for (int NC = 1; NC < N; NC++)
                {
                    var diff = Convert.ToInt32((Fecha[NC] - Fecha[0]).TotalDays);
                    VA = VA + Flujo[NC] / Math.Pow((1 + TIR), (double)(diff / NDxA));
                }
                if (Math.Abs(VA) < Math.Abs(Flujo[0]))
                {
                    Maximo = TIR;
                }
                else
                {
                    Minimo = TIR;
                }

                //Esta es una medida de control para evitar bucles, nose si es necesario
                if (Maximo == Minimo)
                {
                    throw new Exception("La diferencia de decimales entre Minimo y Maximo es nula");
                }
                Console.Write($"\n\n TIR= {TIR}, Max={Maximo}, Min={Minimo} \n\n");
            } while (!(Math.Abs(VA + Flujo[0]) < Delta));

            return TIR;
        }

        public async Task<OperacionCarteraResponse> GetOperacionCarteraAsync(int operacionId, int carteraId)
        {
            var existingOperacionCartera = await _operacionCarteraRepository.FindByOperacionIdAndCarteraId(operacionId,carteraId);
            if (existingOperacionCartera == null)
            {
                return new OperacionCarteraResponse("Operacion Cartera no encontrada");
            }
            return new OperacionCarteraResponse(existingOperacionCartera);
        }




        public async Task<IEnumerable<OperacionCartera>> ListAsync()
        {
            return await _operacionCarteraRepository.ListAsync();
        }

        public async Task<IEnumerable<OperacionCartera>> ListByCarteraAsync(int carteraId)
        {
            return await _operacionCarteraRepository.ListByCarteraId(carteraId);
        }

        public async Task<OperacionCarteraResponse> UnassignOperacionCarteraAsync(int operacionId, int carteraId)
        {
            try
            {

                IEnumerable<Letra> letras = await _letraRepository.ListByCarteraId(carteraId);
                if (letras == null)
                {
                    OperacionCartera operacionCartera1 = await _operacionCarteraRepository.FindByOperacionIdAndCarteraId(operacionId, carteraId);

                    _operacionCarteraRepository.UnassignOperacionCartera(operacionId, carteraId);
                    await _unitOfWork.CompleteAsync();

                    return new OperacionCarteraResponse(operacionCartera1);

                }
                List<Letra> letrasList = letras.ToList();

                letrasList.ForEach(l =>
                {
                    _operacionLetraRepository.UnassingOperacionLetra(operacionId, l.Id);
                });


                OperacionCartera operacionCartera = await _operacionCarteraRepository.FindByOperacionIdAndCarteraId(operacionId, carteraId);

                _operacionCarteraRepository.UnassignOperacionCartera(operacionId,carteraId);
                await _unitOfWork.CompleteAsync();

                return new OperacionCarteraResponse(operacionCartera);
            }
            catch (Exception ex)
            {
                return new OperacionCarteraResponse($"Error al desasignar la operacion con la cartera: {ex.Message}");
            }
        }
    }
}
