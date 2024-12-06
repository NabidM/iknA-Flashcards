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
            InitializeComponent(); // Initializes the UI components from XAML

            // Sample data: Creating a deck hierarchy
            RootDecks = new ObservableCollection<Deck>(); // Initialize the RootDecks collection

            // Setting the data context for the binding (links XAML to the C# data)
            DataContext = this;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Remove the placeholder text if the current text matches the placeholder
                if (textBox.Text == GetPlaceholderText(textBox.Name))
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = Brushes.Black; // Set the text colour to black
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Restore the placeholder text if the input is empty
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = GetPlaceholderText(textBox.Name);
                    textBox.Foreground = Brushes.Gray; // Set the text colour to grey
                }
            }
        }

        private string GetPlaceholderText(string textBoxName)
        {
            // Return the placeholder text based on the TextBox's name
            return textBoxName switch
            {
                "DeckNameInput" => "Enter Deck Name",
                "FlashcardTitleInput" => "Enter Flashcard Title",
                "FlashcardBodyInput" => "Enter Flashcard Body",
                _ => string.Empty // Fallback for unknown TextBox names
            };
        }


        // Method to create a deck dynamically based on user input
        private void CreateDeck_Click(object sender, RoutedEventArgs e)
        {
            string deckName = DeckNameInput.Text.Trim(); // Get the deck name from the TextBox

            if (!string.IsNullOrEmpty(deckName)) // Ensure the name isn't empty
            {
                var newDeck = new Deck(deckName); // Create a new deck with the given name
                RootDecks.Add(newDeck); // Add the new deck to the RootDecks collection
                DeckNameInput.Clear(); // Clear the input field
                MessageBox.Show($"Deck '{deckName}' created!"); // Show confirmation message
            }
            else
            {
                MessageBox.Show("Please enter a valid deck name."); // Show error if the name is empty
            }
        }

        // Method to create a flashcard dynamically based on user input
        private void CreateFlashcard_Click(object sender, RoutedEventArgs e)
        {
            string flashcardTitle = FlashcardTitleInput.Text.Trim(); // Get the title of the flashcard
            string flashcardBody = FlashcardBodyInput.Text.Trim(); // Get the body of the flashcard

            if (!string.IsNullOrEmpty(flashcardTitle) && !string.IsNullOrEmpty(flashcardBody)) // Ensure both inputs are not empty
            {
                var flashcard = new Flashcard(flashcardTitle, flashcardBody); // Create a new flashcard with the given title and body

                // Find the selected deck in the TreeView and add the flashcard to it
                if (DeckTreeView.SelectedItem is Deck selectedDeck)
                {
                    selectedDeck.AddFlashcard(flashcard); // Add the flashcard to the selected deck
                    FlashcardTitleInput.Clear(); // Clear the title input field
                    FlashcardBodyInput.Clear(); // Clear the body input field
                    MessageBox.Show($"Flashcard '{flashcardTitle}' created!"); // Show confirmation message
                }
                else
                {
                    MessageBox.Show("Please select a deck to add the flashcard to."); // Show error if no deck is selected
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid flashcard title and body."); // Show error if inputs are empty
            }
        }
    }
}