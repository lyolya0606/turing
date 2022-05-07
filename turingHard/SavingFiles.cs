using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace turingHard {
    class SavingFiles {
        public void SaveTape(string tape) {
            FileStream file = new FileStream("tape.txt", FileMode.Create);
            StreamWriter fileWriter = new StreamWriter(file);
            fileWriter.Write(tape);
            fileWriter.Close();
        }

        public string ReadTape() {
            string fileText = "";
            foreach (string line in File.ReadLines("tape.txt")) {
                fileText = line;
                break;
            }
            return fileText;
        }

    }
}
