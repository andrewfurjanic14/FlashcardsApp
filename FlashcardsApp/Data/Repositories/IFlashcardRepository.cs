using FlashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal interface IFlashcardRepository
    {
        Task<IEnumerable<Flashcard>> GetAllAsync();
        void AddAsync(Flashcard flashcard);
        void DeleteAsync(int id);
        void UpdateAsync(Flashcard flashcard);
    }
}
