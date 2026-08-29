using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.ViewModels
{
    public class PaginateViewModel
    {
        public int PaginateNumber { get; set; } // number of current page

        public int TotalPaginate { get; set; } // total pages count

        public PaginateViewModel(int objectCount, int paginateNumber, int paginateSize)
        {
            PaginateNumber = paginateNumber;
            TotalPaginate = (int)Math.Ceiling((double)objectCount / paginateSize);
        }

        public bool HasPreviousPage 
        { 
            get { return PaginateNumber > 1; } 
        }
        public bool HasNextPage 
        { 
            get { return PaginateNumber < TotalPaginate; } 
        }
    }
}
