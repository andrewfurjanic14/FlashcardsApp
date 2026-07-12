using FlashcardsApp.Data.Repositories;
using FlashcardsApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Controllers
{
    internal class FlashcardController
    {
        private IFlashcardRepository _repository;
        public FlashcardController(IConfiguration configuration)
        {
            _repository = new FlashcardRepository(configuration);
        }
        public async Task<IEnumerable<Flashcard>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<IEnumerable<Flashcard>> GetByDeckIdAsync(int deckId)
        {
            return await _repository.GetByDeckIdAsync(deckId);
        }
        public async Task<int> AddAsync(string question, string answer, int deckId)
        {
            var flashcard = new Flashcard() { Question = question, Answer = answer, DeckId = deckId };
            return await _repository.AddAsync(flashcard);
        }

        public async Task<int> EditAsync(int id, string question, string answer, int deckId)
        {
            var flashcard = new Flashcard() { Id = id, Question = question, Answer = answer, DeckId = deckId };
            return await _repository.UpdateAsync(flashcard);
        }
        public async Task<int> EditAsync(Flashcard flashcard)
        {
            return await _repository.UpdateAsync(flashcard);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
