using System;
using System.Collections.Generic;

namespace FirstExample.Core.Entities.Dto
{
    public class PostWithComments
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Author { get; set; }
        public Pagination<EditableComment> Comments { get; set; }
    }
}
