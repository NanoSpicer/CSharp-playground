using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryPractice1 {
    public class PlayerScore : IComparable {
        public int Score { get; private set; }
        public int PlayerID { private set; get; }
        public int DayNumber { private set; get; }

        public PlayerScore(int DayNumber, int PlayerID, int Score) {
            this.DayNumber = DayNumber;
            this.PlayerID = PlayerID;
            this.Score = Score;
        }

        public int CompareTo(object obj) {
            PlayerScore p = obj as PlayerScore;
            if (p != null) return this.Score - p.Score;
            return -99999;
        }

        public override string ToString() {
            return "[" + this.PlayerID + "] scored " + this.Score + " at day " + this.DayNumber;
        }
    }
}
