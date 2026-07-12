using FlashcardsApp.Data.DTOs;
using FlashcardsApp.Data.Repositories;
using FlashcardsApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Controllers
{
    internal class StudySessionController
    {
        private IStudySessionRepository _studyRepository;
        private IDeckRepository _deckRepository;

        public StudySessionController(IConfiguration configuration)
        {
            _studyRepository = new StudySessionRepository(configuration);
            _deckRepository = new DeckRepository(configuration);
        }

        public async Task<IEnumerable<StudySession>> GetAllAsync()
        {
            return await _studyRepository.GetAllAsync();
        }
        public async Task<int> AddAsync(int deckId, int score, int totalPoints)
        {
            var studySession = new StudySession() { DeckId = deckId, Score = score, TotalPoints = totalPoints };
            return await _studyRepository.AddAsync(studySession);
        }

        public async Task<StudySessionDto> ToDtoAsync(StudySession studySession)
        {
            var deckName = await _deckRepository.GetNameAsync(studySession.DeckId);
            var dto = new StudySessionDto() { 
                SessionDate = studySession.SessionDate,
                TotalPoints = studySession.TotalPoints,
                Score = studySession.Score,
                DeckName = deckName
            };
            return dto;
        }
    }
}
