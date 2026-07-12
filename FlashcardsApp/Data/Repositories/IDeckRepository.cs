using FlashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal interface IDeckRepository
    {
        Task<IEnumerable<Deck>> GetAllAsync();
        Task<int> AddAsync(Deck deck);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(Deck deck);
    }
}
