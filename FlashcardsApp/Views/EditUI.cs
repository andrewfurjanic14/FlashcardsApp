using FlashcardsApp.Controllers;
using FlashcardsApp.Models;
using FlashcardsApp.Views.Utility;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardsApp.Views
{
    internal class EditUI
    {
        /* 
         1. Add new Deck
            a. Input Name -> return
         2. Edit Existing Deck
            a. Add new flashcard
            b. Edit existing flashcard
                i. Input changed Question
                ii. Input changed Answer
            c. Delete flashcard
            d. Delete Deck
         */
        private DeckController _deckController;
        private FlashcardController _flashcardController;
        public EditUI(IConfiguration configuration)
        {
            _deckController = new DeckController(configuration);
            _flashcardController = new FlashcardController(configuration);
        }

        public async Task RunAsync()
        {
            await Menu();
        }

        private async Task Menu()
        {
            var options = PromptService.Edit_MenuOptions;
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
                        Console.WriteLine("You chose: Add Deck");
                        await AddDeck();
                        break;
                    case UIOptionKeyword.Edit:
                        Console.WriteLine("You chose: Edit Deck");
                        await EditDeck();
                        break;
                    case UIOptionKeyword.Exit:
                        exit = true;
                        break;
                }
            }
        }

        private async Task AddDeck()
        {
            var name = PromptService.RequestString("Input Name: ", ModelConstants.DeckName_CharLimit);
            await _deckController.AddAsync(name);
        }

        private async Task EditDeck()
        {
            var decks = await _deckController.GetAllAsync();
            int deckId = PromptService.ChooseFromDecks(decks).Id;

            var options = PromptService.Edit_DeckOptions;
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
                        Console.WriteLine("You chose: Add flashcard");
                        await AddFlashcard(deckId);
                        break;
                    case UIOptionKeyword.Edit:
                        Console.WriteLine("You chose: Edit flashcard");
                        await EditFlashcard(deckId);
                        break;
                    case UIOptionKeyword.DeleteCard:
                        Console.WriteLine("You chose: Delete flashcard");
                        await DeleteFlashcard(deckId);
                        break;
                    case UIOptionKeyword.DeleteDeck:
                        Console.WriteLine("You chose: Delete this deck");
                        await DeleteDeck(deckId);
                        break;
                    case UIOptionKeyword.Exit:
                        exit = true;
                        break;
                }
            }
        }

        private async Task DeleteDeck(int deckId)
        {
            await _deckController.DeleteAsync(deckId);
        }

        private async Task DeleteFlashcard(int deckId)
        {
            var flashcards = await _flashcardController.GetByDeckIdAsync(deckId);
            var flashcard = PromptService.ChooseFromFlashcards(flashcards);

            await _flashcardController.DeleteAsync(flashcard.Id);
        }

        private async Task EditFlashcard(int deckId)
        {
            var flashcards = await _flashcardController.GetByDeckIdAsync(deckId);
            var flashcard = PromptService.ChooseFromFlashcards(flashcards);

            string questionPrompt = "Enter New Question, or press ENTER to keep:\n)";
            string answerPrompt = "Enter New Answer, or press ENTER to keep:\n)";
            flashcard.Question =
                PromptService.RequestString(
                    questionPrompt, ModelConstants.Question_CharLimit, flashcard.Question);
            flashcard.Answer = PromptService.RequestString(
                    answerPrompt, ModelConstants.Answer_CharLimit, flashcard.Answer);

            await _flashcardController.EditAsync(flashcard);
        }

        private async Task AddFlashcard(int deckId)
        {
            var question = PromptService.RequestString("Enter Question: ", ModelConstants.Question_CharLimit);
            var answer = PromptService.RequestString("Enter Answer: ", ModelConstants.Answer_CharLimit);

            await _flashcardController.AddAsync(question, answer, deckId);
        }
    }
}
