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

        public static string Plural(Value value)
        {
            if (value == Value.Six)
            {
                return "Sixes";
            }
            else
            {
                return value.ToString() + "s";
            }
        }
    }
}