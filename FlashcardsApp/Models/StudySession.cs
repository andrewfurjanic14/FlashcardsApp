using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Models
{
    internal class StudySession
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public DateOnly SessionDate { get; set; }
        public int TotalPoints { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return $"{SessionDate} - {DeckId}: {Score} / {TotalPoints}";
        }
    }
}
