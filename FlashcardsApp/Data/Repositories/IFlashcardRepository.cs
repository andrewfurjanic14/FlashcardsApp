using FlashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal interface IFlashcardRepository
    {
        Task<IEnumerable<Flashcard>> GetAllAsync();
        Task<IEnumerable<Flashcard>> GetByDeckIdAsync(int deckId);
        Task<int> AddAsync(Flashcard flashcard);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(Flashcard flashcard);
    }
}
