using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class ReviewAndPublishDTO
    {
        public Guid Id { get; set; }
        public bool Reviewed { get; set; }
        public bool Published { get; set; }
    }
}
