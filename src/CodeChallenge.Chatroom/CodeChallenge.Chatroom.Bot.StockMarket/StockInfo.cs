namespace CodeChallenge.Chatroom.Bot.StockMarket
{
    public class StockInfo
    {
        public string? Symbol { get; set; }
        public string? Open { get; set; }
        public string? Close { get; set; }

        public override string ToString()
        {
            if (Symbol == null)
            {
                return $"Not a stock";
            }

            return $"{Symbol} quote is ${Close ?? Open ?? "--.--"} per share";
        }
    }
}
