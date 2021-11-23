using AutoMapper;
using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using Finanzas.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Cartera, CarteraResource>();
            CreateMap<Costo, CostoResource>();
            CreateMap<CostosOperacion, CostosOperacionResource>();
            CreateMap<Letra, LetraResource>();
            CreateMap<OperacionCartera, OperacionCarteraResource>();
            CreateMap<OperacionLetra, OperacionLetraResource>();
            CreateMap<Operacion, OperacionResource>();
            CreateMap<Perfil, PerfilResource>();
            CreateMap<Periodo, PeriodoResource>();
            CreateMap<Tasa, TasaResource>();
            CreateMap<Usuario, AuthenticationResponse>();
            CreateMap<Usuario, UsuarioResource>();
        }
    }
}
