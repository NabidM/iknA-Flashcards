using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json; //idrk what this does
using System.Collections.ObjectModel;
using System.Windows;

namespace iknA_Flashcards.Classes
{
    public static class DataStorage
    {
        //a global variable refeerring to the file path of the json file
        private static readonly string FilePath = "flashcards.json";
        //if were lucky, this will save the decks to a json file
        //an observable collection is a collection that notifies the UI when items are added or removed
        public static void SaveDecksToFile(ObservableCollection<Deck> decks)
        {
            //Serializing is when you convert an object into a readable format to save to a JSON
            try
            {
                //Serializing the decks into a JSON file, and its gonna write indented so its easier to read
                string json = JsonSerializer.Serialize(decks, new JsonSerializerOptions { WriteIndented = true });
                //is now writing to the file
                File.WriteAllText(FilePath, json);
            }
            //the file was not saving, so I added this to check whether this was being called or not
            catch (Exception ex)
            {
                MessageBox.Show($"DATA DIDNT SAVE !!! : {ex.Message}");
            }
        }
        public static ObservableCollection<Deck> LoadDecksFromFile()
        {
            try
            {
                if (!File.Exists(FilePath)) return new ObservableCollection<Deck>(); //checking that the file exists
                                                                                     //otherwise, it will return an empty collection
            //Otherwise, it will read the file and deserialize it into a collection of decks
                string json = File.ReadAllText(FilePath);

                //if this returns null, return a new empty collcetion (i.e. there are no cards in the deck)

                return JsonSerializer.Deserialize<ObservableCollection<Deck>>(json) ?? new ObservableCollection<Deck>();
            }
            catch (Exception ex)
            {
                //if all else fails, show a message box shouting at the user and return an empty deck
                MessageBox.Show($"THE DATA DIDNT LOAD: {ex.Message}");
                return new ObservableCollection<Deck>();
            }
        }
    }
}
