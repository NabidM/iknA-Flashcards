using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json; //idrk what this does

namespace iknA_Flashcards.Classes
{
    public static class DataStorage
    {
        public static void SaveDeckToFile(Deck deck, string filePath) //if were lucky, this will save the deck to JSON
        {
            // This is 'serializing' - apparantly this converts data structures into simple text.
            // The indentation bit apparantly allows for whitespace in the JSON file
            string json = JsonSerializer.Serialize(deck, new JsonSerializerOptions { WriteIndented =  true });
            File.WriteAllText(filePath, json); //this will write the above json string to the file
        }
        public static Deck LoadDeckFromFile(string filePath) //Fingers Crossed, this will load the deck from the JSON file
        {
            //First we must check if the file path actually exists, otherwise the program will not work
            if (!File.Exists(filePath))
            {
                return null;
            }
            string json = File.ReadAllText(filePath); //this is reading all the data in the JSON file
            return JsonSerializer.Deserialize<Deck>(json); //If we're lucky again, this will undo the afforemention serialization
        }
    }
}
