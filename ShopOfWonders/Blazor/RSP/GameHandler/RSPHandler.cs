namespace SOW.ShopOfWonders.Blazor.RSP
{
    public class RSPHandler
    {
        public Elems Choose { get; set; }

        public Elems WinCondition { get; set; }

        public Elems LooseCondition { get; set; }

        public string Image { get; set; }

        public GameResults GameResult(RSPHandler opponent)
        {
            if (Choose == opponent.Choose)
                return GameResults.Draw;
            if(LooseCondition == opponent.Choose)
                return GameResults.Defeat;
            return GameResults.Victory;
        }

    }

}
