using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data.DTOs
{
    internal class StudySessionDto
    {
        public required string DeckName { get; set; }
        public DateOnly SessionDate { get; set; }
        public int TotalPoints { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return $"{SessionDate} - {DeckName}: {Score} / {TotalPoints}";
        }
    }
}
