using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iknA_Flashcards.Classes
{
    public class Deck
    {
        public string Name { get; set; } //the name of the deck eg. "Computer Science"
        public List<Flashcard> Flashcards { get; private set; } = new List<Flashcard>(); // Initialise here
        public ObservableCollection<Deck> Subdecks { get; private set; } = new ObservableCollection<Deck>(); //This is a list of any subdecks within the deck

        public Deck() //constructor to create a new deck
        {
            Flashcards = new List<Flashcard>(); //this is an empty list of flashcards
            Subdecks = new ObservableCollection<Deck>(); // this is an empty list of subdecks

        }
        public Deck(string newName)
        {
            Name = newName;
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
