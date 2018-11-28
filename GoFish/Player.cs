using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GoFish
{
    public class Player
    {
        private Random _random;
        private Deck _cards;
        private TextBox _textBoxOnForm;

        public string Name { get; }

        /// <summary>
        /// Constructor that sets all private fields for each player object.
        /// </summary>
        /// <param name="name">Name of the player.</param>
        /// <param name="random">This should always be <c>new Random()</c>.</param>
        /// <param name="textBoxOnForm">The textBox you would like to display text.</param>
        public Player(String name, Random random, TextBox textBoxOnForm)
        {
            Name = name;
            _random = random;
            _textBoxOnForm = textBoxOnForm;
            _textBoxOnForm.Text += $@"{Name} has just joined the game.{Environment.NewLine}";
        }

        /// <summary>
        /// Loops through each of the 13 card values. For each of the values,
        /// it counts all of the cards in the player's cards field that match the value.
        /// If the player has all four cards with that value, that's a complete book. 
        /// It adds the value to the books variable to be returned,
        /// and it removes the book from the player's cards.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Value> PullOutBooks()
        {
            var books = new List<Value>();
            for (var i = 1; i <= 13; i++)
            {
                var value = (Value) i;
                var howMany = 0;
                for (var card = 0; card < _cards.Count; card++)
                    if (_cards.Peek(card).Value == value)
                        howMany++;

                if (howMany == 4)
                {
                    books.Add(value);
                    _cards.PullOutValues(value);
                }
            }

            return books;
        }

        /// <summary>
        /// Returns a random value that is in the player's deck.
        /// </summary>
        /// <returns>A random Value.</returns>
        public Value GetRandomValue()
        {
            Card randomCard = _cards.Peek(_random.Next(_cards.Count));
            return randomCard.Value;
        }

        /// <summary>
        /// Checks if a player has any cards of a certain value.
        /// Then displays a record on the results textBox and returns a deck of cards with the requested value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Deck DoYouHaveAny(Value value)
        {
            Deck cardsIHave = _cards.PullOutValues(value);
            _textBoxOnForm.Text = $@"{Name} has {cardsIHave.Count} {Card.Plural(value)}. {Environment.NewLine}";
            return cardsIHave;
        }


    }
}