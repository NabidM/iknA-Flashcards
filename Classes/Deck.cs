using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iknA_Flashcards.Classes
{
    public class Deck
    {
        public string Name { get; set; } //the name of the deck eg. "Computer Science"
        public List<Flashcard> Flashcards { get; private set; } //This is a list of the flashcards within this whole Deck
        public List<Deck> Subdecks { get; private set; } //This is a list of any subdecks within the deck

        public Deck(string newName) //constructor to create a new deck
        {
            Name = newName; 
            Flashcards = new List<Flashcard>(); //this is an empty list of flashcards
            Subdecks = new List<Deck>(); // this is an empty list of subdecks
        }
        public void AddFlashcard(Flashcard card) //method to add flaschards to thee deck
        {
            Flashcards.Add(card);
        }
        public void AddSubdeck(Deck deck) //method to add subdecks to a deck
        {
            Subdecks.Add(deck);
        }
    }
}
