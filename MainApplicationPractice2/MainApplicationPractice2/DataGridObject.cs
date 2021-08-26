using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplicationPractice2 {
    public class DataGridObject {
        public long ID { get; set; }
        public string PlayerName { get; set; }
        public string PlayerTeam { get; set; }
        public long TotalScore { get; set; }

        public DataGridObject(long ID, string PName, string TName, long Score) { }

    }
}
