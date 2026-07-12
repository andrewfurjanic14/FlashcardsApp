using Dapper;
using FlashcardsApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal class StudySessionRepository : IStudySessionRepository
    {
        private readonly string _connectionString;

        public StudySessionRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "DefaultConnection not found");
        }
        public async Task<int> AddAsync(StudySession studySession)
        {
            string sql =
                """
                INSERT INTO StudySessions (DeckId, TotalPoints, Score)
                VALUES (@DeckId, @TotalPoints, @Score);
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.ExecuteAsync(sql, studySession);
        }

        public async Task<IEnumerable<StudySession>> GetAllAsync()
        {
            string sql =
                """
                SELECT * FROM StudySessions;
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.QueryAsync<StudySession>(sql);
        }
    }
}
