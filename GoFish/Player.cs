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

        public Player(String name, Random random, TextBox textBoxOnForm)
        {
            Name = name;
            _random = random;
            _textBoxOnForm = textBoxOnForm;
            _textBoxOnForm.Text += $@"{Name} has just joined the game.{Environment.NewLine}";
        }

        public IEnumerable<Value> PullOutBooks()
        {
            _cards.
        }
    }
}