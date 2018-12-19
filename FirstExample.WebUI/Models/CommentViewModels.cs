using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FirstExample.WebUI.Models
{
    public class CommentCreate
    {
        [Required]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Enter comment befor send it.")]
        [StringLength(500, ErrorMessage = "Wow. Too big, can't handl it.")]
        public string Text { get; set; }
    }

    public class CommentUpdate
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int PageNum { get; set; }

        [Required(ErrorMessage = "Enter comment befor send it.")]
        [StringLength(500, ErrorMessage = "Max length is 500 symbols.")]
        public string Text { get; set; }
    }
}