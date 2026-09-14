using System;
using System.Collections.Generic;
using System.Text;

namespace KiviPaber.Core
{
    public enum Move
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2    
    }

    public enum RoundResult
    {
        Win,
        Lose,
        Draw
    }  

    public struct GameRound
    {
        public int Number { get; set; }
        public Move PlayerMove { get; set; }
        public Move ComputerMove { get; set; }
        public RoundResult Result { get; set; }
    }


}
