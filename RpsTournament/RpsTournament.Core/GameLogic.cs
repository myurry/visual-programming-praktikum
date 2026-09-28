using System;
using static RpsTournament.Core.GameStates;

namespace RpsTournament.Core
{
    public static class GameLogic
    {
        // reuse a single Random instance to avoid repeated seeds
        private static readonly Random _random = new Random();

        public static Move GetComputerMove()
        {
            int moveIndex = _random.Next(0, 3);
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
                    throw new Exception(InvalidMoveException);
            }
        }
    }
}