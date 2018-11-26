using System;
using System.Collections.Generic;

namespace GoFish
{
    public class Deck
    {
        private List<Card> _cards;
        private readonly Random _random = new Random();
        public int Count
        {
            get { return _cards.Count; }
        }

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

        public Deck(IEnumerable<Card> initialCards)
        {
            _cards = new List<Card>(initialCards);
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        public Card Deal(int index)
        {
            Card cardToDeal = _cards[index];
            _cards.RemoveAt(index);
            return cardToDeal;
        }

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

        public IEnumerable<string> GetCardNames()
        {
            var cardNames = new List<string>();
            foreach (var card in _cards)
            {
                cardNames.Add(card.Name);
            }

            return cardNames;
        }

        public void Sort()
        {
            _cards.Sort(new CardComparer_byValue());
        }
    }
}