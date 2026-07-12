using Dapper;
using FlashcardsApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace FlashcardsApp.Data.Repositories
{
    internal class DeckRepository : IDeckRepository
    {
        private readonly string _connectionString;

        public DeckRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "DefaultConnection not found");
        }

        public async Task<int> AddAsync(Deck deck)
        {
            string sql =
                """
                INSERT INTO Decks (Name)
                VALUES (@Name);
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.ExecuteAsync(sql, deck);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string sql =
                """
                DELETE FROM Decks
                WHERE Id = @Id;
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Deck>> GetAllAsync()
        {
            string sql =
                """
                SELECT * FROM Decks;
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.QueryAsync<Deck>(sql);
        }

        public async Task<int> UpdateAsync(Deck deck)
        {
            string sql =
                """
                UPDATE Decks
                SET Name = @Name;
                """;
            using var con = DataAccess.GetConnection(_connectionString);
            return await con.ExecuteAsync(sql, deck);
        }
    }
}
