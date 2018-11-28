using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GoFish
{
    public class Player
    {
        private readonly Random _random;
        private readonly Deck _cards;
        private readonly TextBox _textBoxOnForm;

        public string Name { get; }
        public int CardCount
        {
            get { return _cards.Count; }
        }

        /// <summary>
        /// Constructor that sets all private fields for each player object.
        /// </summary>
        /// <param name="name">Name of the player.</param>
        /// <param name="random">This should always be <c>new Random()</c>.</param>
        /// <param name="textBoxOnForm">The textBox you would like to display text.</param>
        /// <param name="cards">The player's hand</param>
        public Player(String name, Random random, TextBox textBoxOnForm, Deck cards)
        {
            Name = name;
            _random = random;
            _textBoxOnForm = textBoxOnForm;
            _cards = cards;
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

        /// <summary>
        /// Overloaded method for asking for a random card.
        /// </summary>
        /// <param name="players">A list of players.</param>
        /// <param name="myIndex">The asking player's index.</param>
        /// <param name="stock">The stock deck.</param>
        public void AskForCard(List<Player> players, int myIndex, Deck stock)
        {
            if (stock.Count > 0)
            {
                if (_cards.Count == 0)
                {
                    _cards.Add(stock.Deal());
                }

                Value randomValue = GetRandomValue();
                AskForCard(players, myIndex, stock, randomValue);
            }
        }

        /// <summary>
        /// Method for asking other players for a card value.
        /// </summary>
        /// <param name="players">A list of players.</param>
        /// <param name="myIndex">The asking player's index.</param>
        /// <param name="stock">The stock deck.</param>
        /// <param name="value">The card value being asked for</param>
        public void AskForCard(List<Player> players, int myIndex, Deck stock, Value value)
        {
            _textBoxOnForm.Text += $@"{Name} asks if anyone has a {value}. {Environment.NewLine}";
            var totalCardsGiven = 0;
            for (var i = 0; i < players.Count; i++)
                if (i != myIndex)
                {
                    var player = players[i];
                    var cardsGiven = player.DoYouHaveAny(value);
                    totalCardsGiven += cardsGiven.Count;
                    while (cardsGiven.Count > 0)
                        _cards.Add(cardsGiven.Deal());
                }

            if (totalCardsGiven == 0 && stock.Count > 0)
                _textBoxOnForm.Text += $@"{Name} must draw from the stock.{Environment.NewLine}";
            _cards.Add(stock.Deal());
        }

        public void TakeCard(Card card)
        {
            _cards.Add(card);
        }

        public IEnumerable<string> GetCardNames()
        {
            return _cards.GetCardNames();
        }

        public Card Peek(int cardNumber)
        {
            return _cards.Peek(cardNumber);
        }

        public void SortHand()
        {
            _cards.SortByValue();
        }
    }
}