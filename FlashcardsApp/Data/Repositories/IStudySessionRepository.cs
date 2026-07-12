using FlashcardsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal interface IStudySessionRepository
    {
        Task<IEnumerable<StudySession>> GetAllAsync();
        Task<int> AddAsync(StudySession studySession);
    }
}
