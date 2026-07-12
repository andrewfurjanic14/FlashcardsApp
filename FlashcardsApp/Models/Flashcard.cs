using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Models
{
    internal class Flashcard
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }

        public override string ToString()
        {
            return $"Question: {Question}\n\tAnswer: {Answer}";
        }
    }
}
