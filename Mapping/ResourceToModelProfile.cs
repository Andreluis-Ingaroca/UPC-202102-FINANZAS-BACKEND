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
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCarteraResource, Cartera>();
            CreateMap<SaveCostoResource, Costo>();
            CreateMap<SaveCostosOperacionResource, CostosOperacion>();
            CreateMap<SaveLetraResource, Letra>();
            CreateMap<SaveOperacionResource, Operacion>();
            CreateMap<SavePerfilResource, Perfil>();
            CreateMap<SavePeriodoResource, Periodo>();
            CreateMap<SaveTasaResource, Tasa>();
            CreateMap<SaveUsuarioResource, Usuario>();
            CreateMap<RegisterRequest, Usuario>();
        }
    }
}
