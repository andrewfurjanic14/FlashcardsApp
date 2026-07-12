using FlashcardsApp.Data.DTOs;
using FlashcardsApp.Data.Repositories;
using FlashcardsApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FlashcardsApp.Controllers
{
    internal class FlashcardController
    {
        private IFlashcardRepository _flashcardRepository;
        private IDeckRepository _deckRepository;
        public FlashcardController(IConfiguration configuration)
        {
            _flashcardRepository = new FlashcardRepository(configuration);
            _deckRepository = new DeckRepository(configuration);
        }
        public async Task<IEnumerable<Flashcard>> GetAllAsync()
        {
            return await _flashcardRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Flashcard>> GetByDeckIdAsync(int deckId)
        {
            return await _flashcardRepository.GetByDeckIdAsync(deckId);
        }
        public async Task<int> AddAsync(string question, string answer, int deckId)
        {
            var flashcard = new Flashcard() { Question = question, Answer = answer, DeckId = deckId };
            return await _flashcardRepository.AddAsync(flashcard);
        }

        public async Task<int> EditAsync(int id, string question, string answer, int deckId)
        {
            var flashcard = new Flashcard() { Id = id, Question = question, Answer = answer, DeckId = deckId };
            return await _flashcardRepository.UpdateAsync(flashcard);
        }
        public async Task<int> EditAsync(Flashcard flashcard)
        {
            return await _flashcardRepository.UpdateAsync(flashcard);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _flashcardRepository.DeleteAsync(id);
        }
        public async Task<FlashcardDto> ToDtoAsync(Flashcard flashcard)
        {
            var deckName = await _deckRepository.GetNameAsync(flashcard.Id);
            var dto = new FlashcardDto()
            {
                Question = flashcard.Question,
                Answer = flashcard.Answer,
                DeckName = deckName
            };
            return dto;
        }
    }
}
