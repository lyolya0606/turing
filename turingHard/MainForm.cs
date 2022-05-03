using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace turingHard {
    public partial class MainForm : Form {
        private char nowCondition;
        private char lastCondition = '1';
        private char nowSymbol;
        private string action;
        private string lastAction;
        private int index;
        private List<char> tape;
        private bool start = true;
        private int lastIndex;
        public MainForm() {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e) {
            List<char> alphabetList = new List<char> {
                '_'
            };
            foreach (char c in alphabet.Text) {
                alphabetList.Add(c);
            }

            dataGridView.RowCount = alphabetList.Count;
            for (int i = 0; i < dataGridView.Rows.Count; i++) {
                dataGridView.Rows[i].HeaderCell.Value = (alphabetList[i]).ToString();
            }

            for (int i = 0; i < dataGridViewTape.Columns.Count; i++) {
                dataGridViewTape[i, 0].Value = "_";
            }
            buttonStep.Enabled = true;
        }


        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void ButtonAddColClick(object sender, EventArgs e) {
            DataGridViewTextBoxColumn dgvAge = new DataGridViewTextBoxColumn();
            dgvAge.HeaderText = "Q" + (dataGridView.ColumnCount + 1).ToString();
            dataGridView.Columns.Add(dgvAge);

        }

        private void ButtonDeleteClick(object sender, EventArgs e) {
            dataGridView.Columns.RemoveAt(dataGridView.ColumnCount - 1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) {

        }

        private void DataGridView1CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            
        }

        public void GetTape() {
            List<char> tape = new List<char>();
            for (int i = 0; i < dataGridViewTape.Columns.Count; i++) {
                tape.Add(char.Parse(dataGridViewTape[i, 0].Value.ToString()));
            }
            this.tape = tape;
        }

        public void GetTable() {
        }

        private void ButtonRunClick(object sender, EventArgs e) {
            //int index = -1;
            //char firstSymbol = '_';
            //foreach (char c in tape) {
            //    if (c != '_') {
            //        firstSymbol = c;
            //        index = tape.IndexOf(c);
            //        break;
            //    }
            //}

            //string action = "";

            //for (int i = 0; i < dataGridView.Rows.Count; i++) {
            //    if (char.Parse(dataGridView.Rows[i].HeaderCell.Value.ToString()) == firstSymbol) {
            //        action = dataGridView[0, i].Value.ToString();
            //        break;
            //    }
            //}

            //char nowSymbol = firstSymbol;
            //char nowCondition = '1';

            //do {
            //    if (action[2] == nowCondition) {
            //        dataGridViewTape[index, 0].Selected = true;
            //        dataGridViewTape[index, 0].Value = action[0];
            //        if (action[1] == '>') {
            //            index++;
            //        }
            //        else {
            //            index--;
            //        }

            //        if (tape[index] != nowSymbol) {
            //            nowSymbol = tape[index];
            //            for (int i = 0; i < dataGridView.Rows.Count; i++) {
            //                if (char.Parse(dataGridView.Rows[i].HeaderCell.Value.ToString()) == nowSymbol) {
            //                    action = dataGridView[(int)(nowCondition - '0') - 1, i].Value.ToString();
            //                    nowCondition = action[2];
            //                    break;
            //                }
            //            }
            //        }

            //    }
            //} while (action[1] != '.' && action[2] != '0');

        }

        private void DoSteps() {
            if (this.action[2] == this.nowCondition) {
                this.lastIndex = this.index;
                this.lastAction = this.action;
                dataGridViewTape[this.lastIndex, 0].Selected = true;
                for (int i = 0; i < dataGridView.Rows.Count; i++) {
                    if (char.Parse(dataGridView.Rows[i].HeaderCell.Value.ToString()) == this.tape[this.index]) {
                        dataGridView[(int)(this.lastCondition - '0') - 1, i].Selected = true;
                        break;
                    }
                }

                this.lastCondition = this.nowCondition;

                if (this.action[1] == '.' && this.action[2] == '0') {
                    buttonStep.Enabled = false;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Программа завершена", "Завершение", buttons, MessageBoxIcon.Information);
                    return;
                }

                if (this.action[1] == '>') {
                    this.index++;
                }
                else {
                    this.index--;
                }

                dataGridViewTape[this.lastIndex, 0].Value = this.lastAction[0];
                if (this.tape[this.index] != this.nowSymbol) {
                    this.nowSymbol = this.tape[this.index];
                    for (int i = 0; i < dataGridView.Rows.Count; i++) {
                        if (char.Parse(dataGridView.Rows[i].HeaderCell.Value.ToString()) == this.nowSymbol) {
                            this.action = dataGridView[(int)(nowCondition - '0') - 1, i].Value.ToString();
                            this.nowCondition = this.action[2];
                            break;
                        }
                    }
                }
            }

        }


        private void ButtonStepClick(object sender, EventArgs e) {
            dataGridView.ClearSelection();
            dataGridViewTape.ClearSelection();
            GetTape();
            //int index = -1;
            if (start) {

                char firstSymbol = '_';
                foreach (char c in this.tape) {
                    if (c != '_') {
                        firstSymbol = c;
                        this.index = this.tape.IndexOf(c);
                        break;
                    }
                }

                //string action = "";

                for (int i = 0; i < dataGridView.Rows.Count; i++) {
                    if (char.Parse(dataGridView.Rows[i].HeaderCell.Value.ToString()) == firstSymbol) {
                        this.action = dataGridView[0, i].Value.ToString();
                        dataGridView[0, i].Selected = true;
                        break;
                    }
                }

                this.nowSymbol = firstSymbol;
                this.nowCondition = '1';
            } 
            DoSteps();

            this.start = false;
        }
    }
}