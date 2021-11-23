using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class RegisterRequest
    {
        [Required]
        public string Correo { get; set; }

        [Required]
        public string Contraseña { get; set; }
    }
}
