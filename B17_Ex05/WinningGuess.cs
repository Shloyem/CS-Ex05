using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B17_Ex05
{
    internal class WinningGuess : UserControl
    {
        private Size m_ButtonSize = new Size(40, 40);
        private const int k_Space = 10;
        private int m_GuessesAmount;
        private PinButton[] m_BlackButtonsOrWinningCombination;

        public WinningGuess(int i_GuessesAmount)
        {
            m_GuessesAmount = i_GuessesAmount;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            m_BlackButtonsOrWinningCombination = new PinButton[m_GuessesAmount];
            for (int i = 0; i < m_BlackButtonsOrWinningCombination.Length; i++)
            {
                m_BlackButtonsOrWinningCombination[i] = new PinButton(i);
                m_BlackButtonsOrWinningCombination[i].BackColor = Color.Black;
                m_BlackButtonsOrWinningCombination[i].Enabled = false;
                m_BlackButtonsOrWinningCombination[i].Location = new Point(k_Space + (m_BlackButtonsOrWinningCombination[0].Size.Width + k_Space) * i, k_Space);
                this.Controls.Add(m_BlackButtonsOrWinningCombination[i]);
            }

            this.Size = new Size ((m_BlackButtonsOrWinningCombination[0].Width + k_Space) * m_GuessesAmount, m_BlackButtonsOrWinningCombination[0].Height + 2 * k_Space);
        }

        internal void DisplayWinningGuess(Color[] i_WinningGuessCombination)
        {
            for (int i = 0; i < i_WinningGuessCombination.Length; i++)
            {
                m_BlackButtonsOrWinningCombination[i].BackColor = i_WinningGuessCombination[i];
            }
        }
    }
}
