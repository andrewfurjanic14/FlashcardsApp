# FlashcardsApp
Split into two sides: 
  1. Deck Management
  2. Study
Deck Management is where the user can edit their decks by adding/updating/deleting flashcards, and adding new named decks.
Study is where the user can flip through the decks and check the answers of cards when they're ready and mark whether they got it right or wrong.

The application uses SQL Server with Dapper. 
It has three tables: Decks, Flashcards, StudySessions.
Flashcards and Study Sessions have DTOs to map their Deck Ids onto Deck Name instead for presenting to the user. 
