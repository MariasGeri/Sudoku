using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        private List<Sudoku> _sudokus = new List<Sudoku>();
        private Random _rng = new Random();
        private Sudoku _currentQuiz = null;
        public Form1()
        {
            InitializeComponent();
            CreatePlayField();
            LoadSudokus();
            _currentQuiz = GetRandomQuiz();
            NewGame();
        }

        private void CreatePlayField()
        {
            int lineWidth = 5;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuField sf = new SudokuField();
                    sf.Left = j * sf.Width + (int)(Math.Floor((double)(j / 3))) * lineWidth;
                    sf.Top = i * sf.Height + (int)(Math.Floor((double)(i / 3))) * lineWidth;
                    panel1.Controls.Add(sf);
                }
            }
        }
        private void LoadSudokus()
        {

            {
                _sudokus.Clear();
                using (StreamReader sr = new StreamReader("sudoku.csv", Encoding.Default))
                {
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');

                        Sudoku s = new Sudoku();
                        s.Quiz = line[0];
                        s.Solution = line[1];
                        _sudokus.Add(s);
                    }
                }
            }

        }
        private Sudoku GetRandomQuiz()
        {
            int randomNumber = _rng.Next(_sudokus.Count);
            return _sudokus[randomNumber];
            
        }
        private void NewGame()
        {
            _currentQuiz = GetRandomQuiz();

            int counter = 0;
            foreach (var sf in panel1.Controls.OfType<SudokuField>())
            {
                sf.Value = int.Parse(_currentQuiz.Quiz[counter].ToString());
                sf.Active = sf.Value == 0;
                counter++;
            }
        }
    }
}
