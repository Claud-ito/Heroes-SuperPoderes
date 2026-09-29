using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HeroesWeb.Models
{
    public class SuperPoderes
    {
    public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    public int HeroeId { get; set; }

        // El form solo envia HeroeId, no un objeto Heroes completo -> sin esto,
        // ASP.NET Core exige "Heroe" por ser una referencia no-nullable y el POST falla en silencio.
    [ValidateNever]
        public Heroes Heroe { get; set; } = null!;
    }
}
