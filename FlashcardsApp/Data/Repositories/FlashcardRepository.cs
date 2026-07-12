using Dapper;
using FlashcardsApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal class FlashcardRepository : IFlashcardRepository
    {
        private readonly string _connectionString;

        public FlashcardRepository(IConfiguration configuration)
        {
            _connectionString = 
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "DefaultConnection not found");
        }
        public async Task<IEnumerable<Flashcard>> GetAllAsync()
        {
            string sql =
                """
                SELECT * FROM Flashcards;
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.QueryAsync<Flashcard>(sql);
        }

        public async Task<IEnumerable<Flashcard>> GetByDeckIdAsync(int deckId)
        {
            string sql =
                """
                SELECT * FROM Flashcards
                WHERE DeckId = @DeckId;
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.QueryAsync<Flashcard>(sql, new { DeckId = deckId });
        }
        public async Task<int> AddAsync(Flashcard flashcard)
        {
            string sql =
                """
                INSERT INTO Flashcards (DeckId, Question, Answer)
                VALUES (@DeckId, @Question, @Answer);
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.ExecuteAsync(sql, flashcard);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string sql =
                """
                DELETE FROM Flashcards
                WHERE Id = @Id;
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<int> UpdateAsync(Flashcard flashcard)
        {
            string sql =
                """
                UPDATE Flashcards
                SET
                    DeckId = @DeckId,
                    Question = @Question,
                    Answer = @Answer
                WHERE Id = @Id
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.ExecuteAsync(sql, flashcard);
        }

    }
}
