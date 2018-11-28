using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GoFish
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Game _game;

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textName.Text))
            {
                MessageBox.Show(@"Please enter your name", @"Can't start the game yet!");
                return;
            }
            _game = new Game(textName.Text, new List<string> {"Joe", "Bob"}, textProgress);
            buttonStart.Enabled = false;
            buttonAsk.Enabled = true;
            UpdateForm();
        }

        private void UpdateForm()
        {
            listHand.Items.Clear();
            foreach (string cardName in _game.GetPlayerCardNames())
                listHand.Items.Add(cardName);
            textBooks.Text = _game.DescribeBooks();
            textProgress.Text += _game.DescribePlayerHands();
            textProgress.SelectionStart = textProgress.Text.Length;
            textProgress.ScrollToCaret();
        }

        private void buttonAsk_Click(object sender, EventArgs e)
        {
            textProgress.Text += "";
            if (listHand.SelectedIndex < 0)
            {
                MessageBox.Show(@"Please select a card");
                return;
            }

            if (_game.PlayOneRound(listHand.SelectedIndex))
            {
                textProgress.Text += $@"The winner is... {_game.GetWinnerName()}";
                textBooks.Text = _game.DescribeBooks();
                buttonAsk.Enabled = false;
            }
            
            else
                UpdateForm();
        }
    }
}
