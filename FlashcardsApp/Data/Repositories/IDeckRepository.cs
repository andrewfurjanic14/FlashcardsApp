using FlashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal interface IDeckRepository
    {
        Task<IEnumerable<Deck>> GetAllAsync();
        void AddAsync(Deck deck);
        void DeleteAsync(int id);
        void UpdateAsync(Deck deck);
    }
}
