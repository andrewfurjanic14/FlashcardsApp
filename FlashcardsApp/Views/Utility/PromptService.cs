using FlashcardsApp.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Views.Utility
{
    internal static class PromptService
    {
        internal static readonly UserOption[] Edit_MenuOptions =
            {
                new() { Id = UIOptionKeyword.Add, Label = "[yellow]ADD[/] new deck" },
                new() { Id = UIOptionKeyword.Edit, Label = "[yellow]EDIT[/] deck" },
                new() { Id = UIOptionKeyword.Exit, Label = "[yellow]EXIT[/]" }
            };
        internal static readonly UserOption[] Edit_DeckOptions =
            {
                new() { Id = UIOptionKeyword.Add, Label = "[yellow]ADD[/] new flashcard" },
                new() { Id = UIOptionKeyword.Edit, Label = "[yellow]EDIT[/] flashcard" },
                new() { Id = UIOptionKeyword.DeleteCard, Label = "[yellow]DELETE[/] flashcard"},
                new() { Id = UIOptionKeyword.DeleteDeck, Label = "[yellow]DELETE[/] this deck"},
                new() { Id = UIOptionKeyword.Exit, Label = "[yellow]EXIT[/]" }
            };
        internal static readonly UserOption[] Study_MenuOptions =
            {
                new() { Id = UIOptionKeyword.Add, Label = "[yellow]BEGIN NEW[/] study session" },
                new() { Id = UIOptionKeyword.View, Label = "[yellow]VIEW[/] study sessions" },
                new() { Id = UIOptionKeyword.Exit, Label = "[yellow]EXIT[/]" }
            };

        internal static string RequestString(string instructions, int charLimit, string? defaultValue = null)
        {
            var promptInput =
                new TextPrompt<string>(instructions)
                    .Validate((x) => x.Length > charLimit
                        ? ValidationResult.Error("Exceeds {charLimit} character limit")
                        : ValidationResult.Success()
                    );

            if (defaultValue is not null)
            {
                promptInput.DefaultValue(defaultValue);
            }

            string output = AnsiConsole.Prompt(promptInput);
            return output;
        }

        internal static Deck ChooseFromDecks(IEnumerable<Deck> decks)
        {
            var deckChoice =
                AnsiConsole.Prompt(
                    new SelectionPrompt<Deck>()
                        .Title("Choose a deck")
                        .PageSize(5)
                        .MoreChoicesText("[grey]Move down to reveal more choices[/]")
                        .AddChoices(decks));
            return deckChoice;
        }

        internal static Flashcard ChooseFromFlashcards(IEnumerable<Flashcard> flashcards)
        {
            var flashcardChoice =
                AnsiConsole.Prompt(
                    new SelectionPrompt<Flashcard>()
                        .Title("Choose a flashcard")
                        .PageSize(5)
                        .MoreChoicesText("[grey]Move down to reveal more choices[/]")
                        .AddChoices(flashcards));
            return flashcardChoice;
        }
    }
}
