using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B17_Ex05
{
    internal class GuessPin : UserControl
    { 
        private int m_GuessesAmount;
        private const int k_Space = 10;
        private Size m_ButtonSize = new Size(40, 40);
        internal PinButton[] m_PinButtons;

        public GuessPin(int i_GuessesAmount) 
        {
            m_GuessesAmount = i_GuessesAmount;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size((m_ButtonSize.Width + k_Space) * m_GuessesAmount - k_Space , m_ButtonSize.Height);
            m_PinButtons = new PinButton[m_GuessesAmount];
            for (int i = 0; i < m_PinButtons.Length; i++)
            {
                m_PinButtons[i] = new PinButton(i);
                m_PinButtons[i].Size = m_ButtonSize;
                m_PinButtons[i].Location = new Point((m_PinButtons[i].Width + k_Space) * i, 0);
                m_PinButtons[i].Enabled = false;
                this.Controls.Add(m_PinButtons[i]);
            }
        }
    }
}
