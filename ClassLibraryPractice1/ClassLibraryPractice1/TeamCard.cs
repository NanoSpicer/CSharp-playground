using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryPractice1{
    public class TeamCard : IComparable{
        public string Name { get; set; }

        public TeamCard(String name) {
            this.Name = name;
        }

        public int CompareTo(object obj) {
            TeamCard card = obj as TeamCard;
            if(card!=null) return String.Compare(this.Name, card.Name);
            return -9999;
        }

        public override string ToString() {
            return Name;
        }
    }
}
