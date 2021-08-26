using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryPractice1 {
    public class PlayerCard : IComparable{
        
        public enum PositionType { Goalkeeper, Defender, Midfield, Forward }

        public int ID { private set; get; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Position;

        public PlayerCard(int ID, int Number, string Name, string Team, string pos) {
            this.ID = ID;
            this.Number = Number;
            this.Name = Name;
            this.Team = Team;
            this.Position = pos;
        }

        public int CompareTo(object obj) {
            PlayerCard p = obj as PlayerCard;
            if (p != null) return this.ID - p.ID;
            return -9999;
        }
        public override string ToString() {
            return "[" + this.ID+ "] " + this.Name+ " from " + this.Team+" on Position: "+this.Position;
        }
    }
}
