using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace GoFish
{
    internal class Game
    {
        private List<Player> _players;
        private Dictionary<Value, Player> _books;
        private Deck _stock;
        private TextBox _textBoxOnForm;

        public Game(string playerName, IEnumerable<string> opponentNames, TextBox textBoxOnForm)
        {
            Random random = new Random();
            _textBoxOnForm = textBoxOnForm;
            _players = new List<Player>();
            _players.Add(new Player(playerName, random, textBoxOnForm));
            foreach (string player in opponentNames)
            {
                _players.Add(new Player(player, random, textBoxOnForm));
            }
            _books = new Dictionary<Value, Player>();
            _stock = new Deck();
            Deal();
            _players[0].SortHand();
        }

        private void Deal()
        {
            _stock.Shuffle();
            for (var i = 0; i < 5; i++)
                foreach (var player in _players)
                    player.TakeCard(_stock.Deal());

            foreach (var player in _players)
                PullOutBooks(player);
        }

        private bool PullOutBooks(Player player)
        {
            var booksPulled = player.PullOutBooks();
            foreach (var value in booksPulled) _books.Add(value, player);

            if (player.CardCount == 0)
                return true;

            return false;
        }

        public IEnumerable GetPlayerCardNames()
        {
            return _players[0].GetCardNames();
        }

        public string DescribeBooks()
        {
            var whoHasWhichBooks = "";
            foreach (var value in _books.Keys)
                whoHasWhichBooks += $"{_books[value].Name} has a book of {Card.Plural(value)}.{Environment.NewLine}";

            return whoHasWhichBooks;
        }

        public string DescribePlayerHands()
        {
            var description = "";
            foreach (var player in _players)
            {
                description += $@"{player.Name} has {player.CardCount}";
                if (player.CardCount == 1)
                    description += $" card.{Environment.NewLine}";
                else
                    description += $" cards.{Environment.NewLine}";
            }

            description += $"The stock has {_stock.Count} cards left.{Environment.NewLine}";
            return description;
        }

        public bool PlayOneRound(int selectedPlayerCard)
        {
            var cardToAskFor = _players[0].Peek(selectedPlayerCard).Value;
            for (var i = 0; i < _players.Count; i++)
            {
                if (i == 0)
                    _players[0].AskForCard(_players, 0, _stock, cardToAskFor);
                else
                    _players[i].AskForCard(_players, i, _stock);

                if (PullOutBooks(_players[i]))
                {
                    _textBoxOnForm.Text += $@"{_players[i].Name} drew a new hand. {Environment.NewLine}";
                    var card = 1;
                    while (card <= 5 && _stock.Count > 0)
                    {
                        _players[i].TakeCard(_stock.Deal());
                        card++;
                    }
                }

                _players[0].SortHand();
                if (_stock.Count == 0)
                {
                    _textBoxOnForm.Text += $@"The stock is out of cards. Game over!{Environment.NewLine}";
                    return true;
                }
            }

            return false;
        }

        public string GetWinnerName()
        {
            Dictionary<string, int> winners = new Dictionary<string, int>();
            foreach (Value value in _books.Keys)
            {
                string name = _books[value].Name;
                if (winners.ContainsKey(name))
                    winners[name]++;
                else
                    winners.Add(name, 1);
            }
            int mostBooks = 0;
            foreach (string name in winners.Keys)
                if (winners[name] > mostBooks)
                    mostBooks = winners[name];
            bool tie = false;
            string winnerList = "";
            foreach (string name in winners.Keys)
                if (winners[name] == mostBooks)
                {
                    if (!String.IsNullOrEmpty(winnerList))
                    {
                        winnerList += " and ";
                        tie = true;
                    }
                    winnerList += name;
                }
            winnerList += " with " + mostBooks + " books";
            if (tie)
                return "A tie between " + winnerList;
            else
                return winnerList;
        }
    }
}