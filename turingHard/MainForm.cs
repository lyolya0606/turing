using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
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
        private bool finish = false;
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
            dataGridView.ClearSelection();
            dataGridViewTape.ClearSelection();
            if (start) {
                GetTape();
                char firstSymbol = '_';
                foreach (char c in this.tape) {
                    if (c != '_') {
                        firstSymbol = c;
                        this.index = this.tape.IndexOf(c);
                        break;
                    }
                }
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

            while (!finish) {
                dataGridView.ClearSelection();
                dataGridViewTape.ClearSelection();
                DoSteps();
                Thread.CurrentThread.Join(Speed());
            }

            this.start = false;
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
                    finish = true;
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
                            if (dataGridView[(int)(nowCondition - '0') - 1, i].Value.ToString() == "") {
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                MessageBox.Show("Ошибка в состоянии", "Ошибка", buttons, MessageBoxIcon.Error);
                                return;
                            } else {
                                this.action = dataGridView[(int)(nowCondition - '0') - 1, i].Value.ToString();
                                this.nowCondition = this.action[2];
                            }

                            break;
                        }
                    }
                }
            }

        }


        private void ButtonStepClick(object sender, EventArgs e) {
            dataGridView.ClearSelection();
            dataGridViewTape.ClearSelection();

            if (start) {
                GetTape();
                char firstSymbol = '_';
                foreach (char c in this.tape) {
                    if (c != '_') {
                        firstSymbol = c;
                        this.index = this.tape.IndexOf(c);
                        break;
                    }
                }


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

        private void SaveToolStripMenuItemClick(object sender, EventArgs e) {
            GetTape();
            string tapeSave = "";
            foreach (char c in this.tape) {
                tapeSave += c;
            }

            SavingFiles savingFiles = new SavingFiles();
            savingFiles.SaveTape(tapeSave);
            buttonStep.Enabled = false;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show("Лента сохранена", "Сохранение", buttons, MessageBoxIcon.Information);
        }

        private void ReadToolStripMenuItemClick(object sender, EventArgs e) {
            SavingFiles savingFiles = new SavingFiles();
            string tapeRead = savingFiles.ReadTape();
            for (int i = 0; i < dataGridViewTape.Columns.Count; i++) {
                dataGridViewTape[i, 0].Value = tapeRead[i];
            }

        }

        private void ButtonAgainClick(object sender, EventArgs e) {
            start = true;
            finish = false;
            buttonStep.Enabled = true;
            lastCondition = '1';
        }

        private int Speed() {
            if (comboBox.Text.ToString() == "Быстрая") {
                return 250;
            } else if (comboBox.Text.ToString() == "Нормальная") {
                return 400;
            } else {
                return 500;
            }
        }
    
    }
}