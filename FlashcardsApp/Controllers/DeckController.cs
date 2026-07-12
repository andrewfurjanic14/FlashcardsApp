using FlashcardsApp.Data.Repositories;
using FlashcardsApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlashcardsApp.Controllers
{
    internal class DeckController
    {
        IDeckRepository _repository;
        public DeckController(IConfiguration configuration)
        {
            _repository = new DeckRepository(configuration);
        }

        public async Task<int> AddAsync(string Name)
        {
            var deck = new Deck { Name = Name };
            return await _repository.AddAsync(deck);
        }

        public async Task<IEnumerable<Deck>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
