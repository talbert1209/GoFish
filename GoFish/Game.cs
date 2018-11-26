using System.Collections;
using System.Collections.Generic;

namespace GoFish
{
    internal class Game
    {
        public Game(string playerName, IEnumerable<string> otherPlayers, object progress)
        {
            
        }

        public IEnumerable GetPlayerCardNames()
        {
            throw new System.NotImplementedException();
        }

        public string DescribeBooks()
        {
            throw new System.NotImplementedException();
        }

        public string DescribePlayerHands()
        {
            throw new System.NotImplementedException();
        }

        public bool PlayOneRound(int listHandSelectedIndex)
        {
            throw new System.NotImplementedException();
        }

        public string GetWinnerName()
        {
            throw new System.NotImplementedException();
        }
    }
}