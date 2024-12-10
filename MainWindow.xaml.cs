using iknA_Flashcards.Classes;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iknA_Flashcards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ObservableCollection to hold the root decks
        public ObservableCollection<Deck> RootDecks { get; set; }

        public MainWindow()
        {
            InitializeComponent();  // Initializes the UI components from XAML

            // Sample data: Creating a deck hierarchy
            RootDecks = new ObservableCollection<Deck>();  // Initialize the RootDecks collection

            // Setting the data context for the binding (links XAML to the C# data)
            DataContext = null;
            DataContext = this;

        }
        // Load decks when the application starts
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loading decks...");
            var loadedDecks = DataStorage.LoadDecksFromFile();
            RootDecks.Clear();
            foreach (var deck in loadedDecks)
            {
                RootDecks.Add(deck);
            }

            MessageBox.Show($"Loaded {RootDecks.Count} root decks.");
        }

        // Save decks when the application closes
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Saving decks...");
            DataStorage.SaveDecksToFile(RootDecks);
            MessageBox.Show($"Saved {RootDecks.Count} root decks to {DataStorage.FilePath}");
        }


        // This method is called when a TextBox gains focus
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // If the current text is the placeholder text, clear it and set the text colour to black
                if (textBox.Text == GetPlaceholderText(textBox.Name))
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = Brushes.Black; // Set the text colour to black
                }
            }
        }

        // This method is called when a TextBox loses focus
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // If the TextBox is empty, restore the placeholder text and set the colour to grey
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = GetPlaceholderText(textBox.Name);
                    textBox.Foreground = Brushes.Gray; // Set the text colour to grey
                }
            }
        }

        // Returns the appropriate placeholder text based on the TextBox's name
        private string GetPlaceholderText(string textBoxName)
        {
            // Return the placeholder text based on the TextBox's name
            return textBoxName switch
            {
                "DeckNameInput" => "Enter Deck Name", // Placeholder for Deck Name input
                "FlashcardTitleInput" => "Enter Flashcard Title", // Placeholder for Flashcard Title input
                "FlashcardBodyInput" => "Enter Flashcard Body", // Placeholder for Flashcard Body input
                _ => string.Empty  // Fallback for unknown TextBox names
            };
        }

        // This method is called when the "Create Deck" button is clicked
        private void CreateDeck_Click(object sender, RoutedEventArgs e)
        {
            string deckName = DeckNameInput.Text.Trim();  // Get the deck name from the TextBox input

            if (!string.IsNullOrEmpty(deckName))  // Ensure the name isn't empty
            {
                var newDeck = new Deck(deckName);  // Create a new deck with the given name
                RootDecks.Add(newDeck);  // Add the new deck to the RootDecks collection
                DeckNameInput.Clear();  // Clear the input field after creation
                MessageBox.Show($"Deck '{deckName}' created!");  // Show a confirmation message
            }
            else
            {
                MessageBox.Show("Please enter a valid deck name.");  // Show an error message if the name is empty
            }
        }

        // This method is called when the "Create Flashcard" button is clicked
        private void CreateFlashcard_Click(object sender, RoutedEventArgs e)
        {
            string flashcardTitle = FlashcardTitleInput.Text.Trim();  // Get the title of the flashcard
            string flashcardBody = FlashcardBodyInput.Text.Trim();  // Get the body of the flashcard

            if (!string.IsNullOrEmpty(flashcardTitle) && !string.IsNullOrEmpty(flashcardBody))  // Ensure both inputs are not empty
            {
                var flashcard = new Flashcard(flashcardTitle, flashcardBody);  // Create a new flashcard with the given title and body

                // Find the selected deck in the TreeView and add the flashcard to it
                if (DeckTreeView.SelectedItem is Deck selectedDeck)
                {
                    selectedDeck.AddFlashcard(flashcard);  // Add the flashcard to the selected deck
                    FlashcardTitleInput.Clear();  // Clear the title input field
                    FlashcardBodyInput.Clear();  // Clear the body input field
                    MessageBox.Show($"Flashcard '{flashcardTitle}' created!");  // Show a confirmation message
                }
                else
                {
                    MessageBox.Show("Please select a deck to add the flashcard to.");  // Show error if no deck is selected
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid flashcard title and body.");  // Show error if inputs are empty
            }
        }

        // Method to save the decks to a file when the "Save" button is clicked
        private void SaveDecks_Click(object sender, RoutedEventArgs e)
        {
            DataStorage.SaveDecksToFile(RootDecks);  // Save the RootDecks ObservableCollection to a file
            MessageBox.Show("Decks saved successfully!");  // Show a success message
        }

        // Method to load the decks from a file when the "Load" button is clicked
        private void LoadDecks_Click(object sender, RoutedEventArgs e)
        {
            RootDecks = DataStorage.LoadDecksFromFile();  // Load the decks from a file and update the RootDecks collection
            MessageBox.Show("Decks loaded successfully!");  // Show a success message
        }
    }
}