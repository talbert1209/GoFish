using System;
using System.Collections.Generic;

namespace GoFish
{
    public class Deck
    {
        private List<Card> _cards;
        private readonly Random _random = new Random();

        ///<value>The number of cards in the deck</value>
        public int Count
        {
            get { return _cards.Count; }
        }

        /// <summary>
        /// Creates a 52 card deck.
        /// </summary>
        public Deck()
        {
            _cards = new List<Card>();
            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 1; value <= 13; value++)
                {
                    _cards.Add(new Card((Suit)suit, (Value)value));
                }
            }
        }

        /// <summary>
        /// Creates a new deck with a specific set of cards.
        /// </summary>
        /// <param name="initialCards">Cards you want added to the deck.</param>
        public Deck(IEnumerable<Card> initialCards)
        {
            _cards = new List<Card>(initialCards);
        }

        /// <summary>
        /// Adds a card to the deck of cards.
        /// </summary>
        /// <param name="card">Card you want to add.</param>
        public void Add(Card card)
        {
            _cards.Add(card);
        }

        /// <summary>
        /// Deals a specific card.
        /// </summary>
        /// <param name="index">The index of the card you want to deal.</param>
        /// <returns>The card that was dealt.</returns>
        public Card Deal(int index)
        {
            Card cardToDeal = _cards[index];
            _cards.RemoveAt(index);
            return cardToDeal;
        }

        /// <summary>
        /// Deals the card at the 0 index, or the "top" card.
        /// </summary>
        /// <returns>The card at the top of the deck (index 0) that was dealt.</returns>
        public Card Deal()
        {
            return Deal(0);
        }

        /// <summary>
        /// Changes the order of the deck.
        /// </summary>
        public void Shuffle()
        {
            var shuffledDeck = new List<Card>();
            while (_cards.Count > 0)
            {
                var randomCardIndex = _random.Next(_cards.Count);
                shuffledDeck.Add(_cards[randomCardIndex]);
                _cards.RemoveAt(randomCardIndex);
            }

            _cards = shuffledDeck;
        }

        /// <summary>
        /// Returns a list of all the cards in the deck.
        /// </summary>
        /// <returns>A list of cards represented as strings.</returns>
        public IEnumerable<string> GetCardNames()
        {
            var cardNames = new List<string>();
            foreach (var card in _cards)
            {
                cardNames.Add(card.Name);
            }

            return cardNames;
        }

        /// <summary>
        /// Sorts the cards in the deck first by value then by suit.
        /// </summary>
        public void Sort()
        {
            _cards.Sort(new CardComparer_byValue());
        }

        /// <summary>
        /// Lets you take a peek at one of the cards in the deck without dealing it.
        /// </summary>
        /// <param name="cardNumber">Card you want to peek at</param>
        /// <returns>A Card</returns>
        public Card Peek(int cardNumber)
        {
            return _cards[cardNumber];
        }

        /// <summary>
        /// Searches through the entire deck for cards with a certain value,
        /// and returns true i it finds any.
        /// </summary>
        /// <param name="value">Card value you want to search for.</param>
        /// <returns>True or False</returns>
        public bool ContainsValue(Value value)
        {
            foreach (var card in _cards)
            {
                if (card.Value == value)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Looks for any card that match the requested value,
        /// pulls them out of the deck and returns a new deck with those cards in it.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Deck PullOutValue(Value value)
        {
            Deck deckToReturn = new Deck(new Card[]{});
            for (int i = _cards.Count - 1; i >= 0; i--)
            {
                if (_cards[i].Value == value)
                {
                    deckToReturn.Add(Deal(i));
                }
            }

            return deckToReturn;
        }

        /// <summary>
        /// Checks a deck to see if it contains a book of four cards of whatever value was passed as the parameter.
        /// It returns true  if there a book in the deck, false if otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HasBook(Value value)
        {
            int numberOfCards = 0;
            foreach (var card in _cards)
            {
                if (card.Value == value)
                {
                    numberOfCards++;
                }
            }

            if (numberOfCards == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sorts the deck by the CardComparer_byValue() object.
        /// </summary>
        public void SortByValue()
        {
            _cards.Sort(new CardComparer_byValue());
        }
    }
}