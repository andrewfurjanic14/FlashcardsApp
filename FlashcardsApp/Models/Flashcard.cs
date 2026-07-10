using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Models
{
    internal class Flashcard
    {
        public int Id { get; set; }

        public int DeckId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
