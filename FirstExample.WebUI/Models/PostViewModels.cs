using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using FirstExample.Core.Entities.Dto;

namespace FirstExample.WebUI.Models
{
    public class PostCreate
    {
        [Required(ErrorMessage = "Please, enter post name.")]
        [StringLength(25, ErrorMessage = "Max length 50 symbols.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter some text.")]
        [StringLength(500, ErrorMessage = "Max length 500")]
        public string Description { get; set; }

    }
}