using FlashcardsApp.Controllers;
using FlashcardsApp.Models;
using FlashcardsApp.Views.Utility;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FlashcardsApp.Views
{
    internal class StudyUI
    {
        /*
         1. Start New Study Session
            a. Pick a deck -> card-by-card UI (keep track of points)
         2. View Study Sessions
         */
        private DeckController _deckController;
        private FlashcardController _flashcardController;
        private StudySessionController _studySessionController;
        public StudyUI(IConfiguration configuration)
        {
            _deckController = new DeckController(configuration);
            _flashcardController = new FlashcardController(configuration);
            _studySessionController = new StudySessionController(configuration);
        }

        public async Task RunAsync()
        {
            await Menu();
        }

        private async Task Menu()
        {
            var options = PromptService.Study_MenuOptions;
            var promptInput =
                new SelectionPrompt<UserOption>()
                        .Title("Choose an action: ")
                        .AddChoices(options)
                        .UseConverter(x => x.Label);

            bool exit = false;
            while (!exit)
            {
                var userChoice = AnsiConsole.Prompt(promptInput);

                switch (userChoice.Id)
                {
                    case UIOptionKeyword.Add:
                        Console.WriteLine("You chose: Begin new study session");
                        await BeginStudySession();
                        break;
                    case UIOptionKeyword.View:
                        Console.WriteLine("You chose: View study sessions");
                        await ViewStudySessions();
                        break;
                    case UIOptionKeyword.Exit:
                        exit = true;
                        break;
                }
            }
        }

        private async Task ViewStudySessions()
        {
            var studySessions = await _studySessionController.GetAllAsync();
            
            string[] columns =
            {
                "Date",
                "Deck",
                "Total Points Available",
                "Points Earned"
            };
            var table = new Table();
            table.AddColumns(columns);

            foreach (StudySession s in studySessions)
            {
                var dto = await _studySessionController.ToDtoAsync(s);
                table.AddRow(
                    dto.SessionDate.ToString(),
                    dto.DeckName,
                    dto.TotalPoints.ToString(),
                    dto.Score.ToString());
            }
            AnsiConsole.Write(table);
        }

        private async Task BeginStudySession()
        {
            var decks = await _deckController.GetAllAsync();
            var deck = PromptService.ChooseFromDecks(decks);
            var (score, totalPoints) = await StudyDeck(deck);
            await _studySessionController.AddAsync(deck.Id, score, totalPoints);
            AnsiConsole.WriteLine($"You're final score: {score} / {totalPoints}");
        }

        // return the (user's score, total points available)
        private async Task<(int, int)> StudyDeck(Deck deck)
        {
            AnsiConsole.WriteLine("Press any key to flip the card");

            int totalPoints = 0;
            int score = 0;

            var flashcards = await _flashcardController.GetByDeckIdAsync(deck.Id);
            foreach (var card in flashcards)
            {
                score += StudyFlashcard(card);
                totalPoints++;
            }

            return (score, totalPoints);
        }

        private int StudyFlashcard(Flashcard flashcard)
        {
            AnsiConsole.WriteLine("Question: " +  flashcard.Question);
            Console.ReadKey();
            AnsiConsole.WriteLine("Answer: " + flashcard.Answer);
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices("CORRECT", "INCORRECT"));
            return choice == "CORRECT" ? 1 : 0;
        }
    }
}
