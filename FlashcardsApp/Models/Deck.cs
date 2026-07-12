using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Models
{
    internal class Deck
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
