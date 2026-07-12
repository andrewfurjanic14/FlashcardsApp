using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Views.Utility
{
    enum UIOptionKeyword
    {
        Add,
        Edit,
        DeleteCard,
        DeleteDeck,
        View,
        Exit
    }
    internal class UserOption
    {
        public string Label { get; set; }
        public UIOptionKeyword Id { get; set; }
    }
}
