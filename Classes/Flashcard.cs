using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iknA_Flashcards
{
    public class Flashcard
    {
        public string Title { get; set; } //Title of the flashcard
        public string Body { get; set; } //Body of the flaschard
        public Flashcard(string newTitle, string newBody) //constructor to create a new card
        {
            Title = newTitle;
            Body = newBody;
        }
    }
}
