namespace GoFish
{
    public class Card
    {
        public Suit Suit { get; }
        public Value Value { get; }
        public string Name
        {
            get { return $"{Value} of {Suit}"; }
        }

        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}