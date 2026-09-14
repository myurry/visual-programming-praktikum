namespace KiviPaber.Core
{
    public static class GameLogic
    {
        public static Move GetComputerMove()
        {
            // Simple AI logic - randomly select a move
            Random random = new Random();
            int moveIndex = random.Next(0, 3);
            return (Move)moveIndex;
        }

        public static RoundResult GetResult(Move playerMove, Move computerMove)
        {
            if (playerMove == computerMove)
            {
                return RoundResult.Draw;
            }

            switch (playerMove)
            {
                case Move.Rock:
                    return computerMove == Move.Scissors ? RoundResult.Win : RoundResult.Lose;
                case Move.Paper:
                    return computerMove == Move.Rock ? RoundResult.Win : RoundResult.Lose;
                case Move.Scissors:
                    return computerMove == Move.Paper ? RoundResult.Win : RoundResult.Lose;
                default:
                    throw new ArgumentException("Invalid move");
            }
        }
    }
}
