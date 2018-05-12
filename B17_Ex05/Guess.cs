using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B17_Ex05
{
    public delegate void WinningPinEventHandler(List<Color> i_WinningColorCombination);

    internal class Guess : UserControl
    {
        internal GuessPin m_GuessPin;
        internal GuessResult m_GuessResult;
        internal Button m_GenerateResultButton;
        private const int k_Space = 10;
        private int m_GuessesPinLength;

        public Guess(int i_GuessesPinLength)
        {
            m_GuessesPinLength = i_GuessesPinLength;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            initializeGuessPin();
            initializeGuessResult();
            initializeGenerateResultButton();

            this.Size = new Size(m_GuessPin.Width + m_GenerateResultButton.Width + m_GuessResult.Width + 4*k_Space , m_GuessResult.Height + 2*k_Space);
            m_GuessPin.Location = new Point(k_Space, this.Height / 2 - m_GuessResult.Height / 2);
            m_GenerateResultButton.Location = new Point(m_GuessPin.Right + k_Space, this.Height / 2 - m_GenerateResultButton.Height / 2);
            m_GuessResult.Location = new Point(m_GenerateResultButton.Right + k_Space, this.Height / 2 - m_GuessResult.Height / 2);

            this.Controls.Add(m_GuessPin);
            this.Controls.Add(m_GuessResult);
            this.Controls.Add(m_GenerateResultButton);
        }

        private void initializeGenerateResultButton()
        {
            m_GenerateResultButton = new Button();
            m_GenerateResultButton.Size = new Size(40, 20);
            m_GenerateResultButton.Enabled = false;
            m_GenerateResultButton.Text = "-->>";
        }

        private void initializeGuessResult()
        {
            m_GuessResult = new GuessResult(m_GuessesPinLength);
        }

        private void initializeGuessPin()
        {
            m_GuessPin = new GuessPin(m_GuessesPinLength);
        }
    }
}
