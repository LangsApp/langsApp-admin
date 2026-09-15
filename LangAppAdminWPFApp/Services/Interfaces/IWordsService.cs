using LangApp.Admin.WPF.DTOs.Requests;
using LangApp.Admin.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangApp.Admin.WPF.Services.Interfaces
{
    public interface IWordsService
    {
        public Task<List<BaseWord>> GetBaseWordsAsync(CancellationToken cancellationToken);
        public Task<string> AddNewBaseWordsByCategoryAsync
            (CreateBaseWordsByCategoryDTO newBaseWordsByCategory, CancellationToken cancellationToken);
        public Task<string> AddNewBaseWordAsync(CreateBaseWordDTO newBaseWord, CancellationToken cancellationToken);
    }
}
