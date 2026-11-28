using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.DTOs.Requests
{
    public class DataSetBaseWordsNCategoriesDTO
    {
        public Dictionary<string, List<string>> DataSet { get; set; } = [];
    }
}
