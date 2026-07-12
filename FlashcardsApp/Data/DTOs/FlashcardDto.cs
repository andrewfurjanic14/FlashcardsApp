using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.DTOs
{
    internal class FlashcardDto
    {
        public required string DeckName { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }

        public override string ToString()
        {
            return $"Deck: {DeckName}\n\tQuestion: {Question}\n\tAnswer: {Answer}";
        }
    }
}
