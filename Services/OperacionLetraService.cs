using Finanzas.Domain.Models;
using Finanzas.Domain.Persistence.Repositories;
using Finanzas.Domain.Services;
using Finanzas.Domain.Services.Communications;
using Finanzas.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Services
{
    public class OperacionLetraService : IOperacionLetraService
    {
        private readonly IOperacionLetraRepository _operacionLetraRepository;
        private readonly ILetraRepository _letraRepository;
        private readonly IOperacionRepository _operacionRepository;
        private readonly ICostosOperacionRepository _costosOperacionRepository;
        private readonly ITasaRepository _tasaRepository;
        private readonly IPeriodoRepository _periodoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OperacionLetraService(IOperacionLetraRepository operacionLetraRepository, IUnitOfWork unitOfWork, ILetraRepository letraRepository, IOperacionRepository operacionRepository, ICostosOperacionRepository costosOperacionRepository, ITasaRepository tasaRepository, IPeriodoRepository periodoRepository)
        {
            _operacionLetraRepository = operacionLetraRepository;
            _unitOfWork = unitOfWork;
            _letraRepository = letraRepository;
            _operacionRepository = operacionRepository;
            _costosOperacionRepository = costosOperacionRepository;
            _tasaRepository = tasaRepository;
            _periodoRepository = periodoRepository;
        }
        public async Task<IEnumerable<ResultOperacionLetraResource>> ListLetraOperacionesByCarteraId(int operacionId)
        {
            IEnumerable<OperacionLetra> operacionLetras = await _operacionLetraRepository.ListByOperacionId(operacionId);
            Operacion operacion = await _operacionRepository.FindByIdAsync(operacionId);
            if (operacionLetras == null)
            {
                return null;
            }
            List<OperacionLetra> operacionLetrasList = operacionLetras.ToList();
            List<ResultOperacionLetraResource> resultOperacionLetraResources = new List<ResultOperacionLetraResource>();

            operacionLetrasList.ForEach(async ol =>
            {
                Letra letra = await _letraRepository.FindByIdAsync(ol.LetraId);
                ResultOperacionLetraResource toAdd = new ResultOperacionLetraResource();
                toAdd.LetraId = ol.LetraId;
                toAdd.CostosFinales = ol.CostosFinales;
                toAdd.CostosIniciales = ol.CostosIniciales;
                toAdd.D = ol.D * 100;
                toAdd.Descuento = ol.Descuento;
                toAdd.FechaDescuento = operacion.FechaDescuento;
                toAdd.FechaGiro = letra.FechaGiro;
                toAdd.FechaVencimiento = letra.FechaVencimiento;
                toAdd.Flujo = ol.Flujo;
                toAdd.NDias = ol.NDias;
                toAdd.NombreGirador = letra.NombreGirador;
                toAdd.Retencion = ol.Retencion;
                toAdd.TCEA = ol.TCEA * 100;
                toAdd.TEP = ol.TEP*100;
                toAdd.ValorEntregado = ol.ValorEntregado;
                toAdd.ValorNeto = ol.ValorNeto;
                toAdd.ValorNominal = letra.ValorNominal;
                toAdd.ValorRecibido = ol.ValorRecibido;

                resultOperacionLetraResources.Add(toAdd);
            });
            IEnumerable<ResultOperacionLetraResource> resultOperacionLetraResourcesIEnumerable = resultOperacionLetraResources.AsEnumerable();
            return resultOperacionLetraResourcesIEnumerable;
        }

        public async Task<OperacionLetraResponse> AssignOperacionLetraAsync(int operacionId, int letraId)
        {
            var operacion = await _operacionRepository.FindByIdAsync(operacionId);
            var letra = await _letraRepository.FindByIdAsync(letraId);
            var tasa = await _tasaRepository.FindByIdAsync(operacion.TasaId);
            try
            {
                //Por calcular
                int nDias = Convert.ToInt32((letra.FechaVencimiento - operacion.FechaDescuento).TotalDays);

                float tep = 0;
                if (tasa.Nominal)
                {
                    var periodo = await _periodoRepository.FindByIdAsync(tasa.PeriodoId);
                    var periodoC = await _periodoRepository.FindByIdAsync(tasa.PeriodoCapitalizacionId);
                    tep = TEP(tasa, periodo,periodoC,nDias, operacion.AñoCalendario);
                }
                else
                {
                    var periodo = await _periodoRepository.FindByIdAsync(tasa.PeriodoId);
                    tep = TEP(tasa, periodo, periodo, nDias, operacion.AñoCalendario);
                }
                tep = (float)Decimal.Round((decimal)tep, 7);
                float d = (tep) / (1 + tep);
                d = (float)Decimal.Round((decimal)d, 7);
                float descuento = letra.ValorNominal * (float)d;
                descuento = (float)Decimal.Round((decimal)descuento, 2);

                IEnumerable<CostosOperacion> costosOperacions = await _costosOperacionRepository.ListByOperacionId(operacionId);
                List<CostosOperacion> costos = costosOperacions.ToList();

                float costosIniciales = GetCostos(costos,true,letra);
                float costosFinales = GetCostos(costos, false, letra);
                
                costosIniciales=(float)Decimal.Round((decimal)costosIniciales, 2);
                costosFinales =(float)Decimal.Round((decimal)costosFinales, 2);

                float retencion = 0;
                
                if (operacion.RetencionPorcentaje)
                {
                    retencion = letra.ValorNominal * operacion.Retencion;
                    retencion = (float)Decimal.Round((decimal)retencion, 2);
                }
                else
                {
                    retencion = operacion.Retencion;
                }
                
                float valorNeto = letra.ValorNominal-descuento;

                float valorEntregado = letra.ValorNominal + costosFinales - retencion;

                float valorRecibido = valorNeto-costosIniciales-retencion;

                float flujo = -letra.ValorNominal - costosFinales;

                float division = ((float)valorEntregado / valorRecibido);

                float tcea = 0;
                if (operacion.AñoCalendario)
                {
                    double exponente = (365.00 / (double)nDias);
                    tcea = (float)Math.Pow(division, exponente) - 1;
                }
                else
                {
                    double exponente = (360.00 / (double)nDias);
                    tcea = (float)Math.Pow(division, exponente) - 1;
                }
                tcea = (float)Decimal.Round((decimal)tcea, 7);


                await _operacionLetraRepository.AssingOperacionLetra(operacionId, letraId, tep, nDias,retencion, d,costosIniciales,costosFinales, descuento, valorNeto, valorEntregado, valorRecibido, tcea,flujo);
                await _unitOfWork.CompleteAsync();

                OperacionLetra operacionLetra = await _operacionLetraRepository.FindByLetraIdAndOperacionId(operacionId, letraId);

                return new OperacionLetraResponse(operacionLetra);

            }
            catch(Exception ex)
            {
                return new OperacionLetraResponse($"Error al asignar operacion con letra: {ex.Message}");
            }
        }

        private float TEP(Tasa tasa,Periodo periodo, Periodo periodoCapitalizacion, int nDias, bool calendario)
        {
            float tep = 0;
            if (tasa.Nominal)
            {
                double m = periodo.Cantidad/ periodoCapitalizacion.Cantidad;
                double n = nDias/periodoCapitalizacion.Cantidad;
                if (calendario && periodoCapitalizacion.Cantidad == 360)
                {
                    m = periodo.Cantidad / 365;
                    n = nDias / 365;
                }
                double parentesis = (1 + ((double)tasa.Monto)/((double)m));
                tep = (float)(Math.Pow(parentesis, n) - 1);
                return tep;
            }
            else
            {
                int n = periodo.Cantidad;
                if (calendario && periodoCapitalizacion.Cantidad == 360)
                {
                    n = 365;
                }
                double division = ((double)nDias / (double)n);
                tep = (float)(Math.Pow((1 + tasa.Monto), division) - 1);
                return tep;
            }
        }

        private float GetCostos(List<CostosOperacion> Costos, bool iniciales, Letra letra)
        {
            float costos = 0;
            Costos.ForEach(c =>
            { 
                if (iniciales == c.CostoInicial)
                {
                    if (c.Porcentaje)
                    {
                        costos = costos + (letra.ValorNominal) * c.Monto;
                    }
                    else
                    {
                        costos = costos + c.Monto;
                    }
                }   
            }
            );

            return costos;
        }

        
        public async Task<IEnumerable<OperacionLetra>> ListAsync()
        {
            return await _operacionLetraRepository.ListAsync();
        }

        public async Task<IEnumerable<OperacionLetra>> ListByLetraIdAsync(int letraId)
        {
            return await _operacionLetraRepository.ListByLetraId(letraId);
        }

        public async Task<IEnumerable<OperacionLetra>> ListByOperacionIdAsync(int operacionId)
        {
            return await _operacionLetraRepository.ListByOperacionId(operacionId);
        }

        public async Task<OperacionLetraResponse> UnassignOperacionLetraAsync(int operacionId, int letraId)
        {
            try
            {
                OperacionLetra operacionLetra = await _operacionLetraRepository.FindByLetraIdAndOperacionId(operacionId, letraId);

                _operacionLetraRepository.UnassingOperacionLetra(operacionId, letraId);
                await _unitOfWork.CompleteAsync();


                return new OperacionLetraResponse(operacionLetra);

            }
            catch (Exception ex)
            {
                return new OperacionLetraResponse($"Error al desasignar operacion con letra: {ex.Message}");
            }
        }

        public async Task<OperacionLetraResponse> GetByLetraIdAndOperacionAsync(int letraId, int operacionId)
        {
            var existingOperacionLetra = await _operacionLetraRepository.FindByLetraIdAndOperacionId(operacionId, letraId);
            if (existingOperacionLetra == null)
            {
                return new OperacionLetraResponse("Operacion Letra no encontrada");
            }
            return new OperacionLetraResponse(existingOperacionLetra);
        }

    }
}
