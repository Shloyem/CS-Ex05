using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B17_Ex05
{
    internal class LogicUnit
    {
        private const int k_GuessesPinLength = 4;
        private int m_GuessesAmount;
        private Color[] m_GameColors = new Color[] { Color.Pink, Color.Red, Color.Green, Color.LightBlue,
                                                    Color.Blue, Color.Yellow, Color.DarkRed, Color.White};
        private Color[] m_WinningPinCombination;
        private Color[] m_CurrentPinCombination;
        private readonly Dictionary<Color, bool> r_AvailableColors = new Dictionary<Color, bool>();
        private int m_ActiveGuessIndex = 0;
        private int m_AmountOfButtonsPickedOnCurrIndex = 0;
        public event EventHandler AllPinButtonsSelected;

        public LogicUnit()
        {
            m_WinningPinCombination = new Color[k_GuessesPinLength];
            m_CurrentPinCombination = new Color[k_GuessesPinLength];
            initializeAvailableColorDictionary();
        }

        public Color[] WinningPinCombination
        {
            get { return m_WinningPinCombination; }
        }

        public int ActiveGuessIndex
        {
            get { return m_ActiveGuessIndex; }
        }

        public Dictionary<Color, bool> AvailableColors
        {
            get { return r_AvailableColors; }
        }

        public int GuessesPinLength
        {
            get { return k_GuessesPinLength; }
        }

        public int GuessesAmount
        {
            get { return m_GuessesAmount; }
            set { m_GuessesAmount = value; }
        }

        internal void UpdateAColorWasPicked(Color i_PreviousColor, Color i_SelectedColor, int i_ButtonIndex)
        {
            bool isPreviousColorSelected = i_PreviousColor != Color.Empty;

            if (isPreviousColorSelected)
            {
                r_AvailableColors[i_PreviousColor] = true; // Make the Previous color available for selection
            }
            else
            {
                m_AmountOfButtonsPickedOnCurrIndex++;
            }

            m_CurrentPinCombination[i_ButtonIndex] = i_SelectedColor;

            r_AvailableColors[i_SelectedColor] = false; // Make the current color not available for selection

            respondIfGuessIsComplete();
        }

        private void respondIfGuessIsComplete()
        {
            if (m_AmountOfButtonsPickedOnCurrIndex == k_GuessesPinLength)
            {
                OnAllPinButtonsSelected();
            }
        }

        protected virtual void OnAllPinButtonsSelected()
        {
            if (AllPinButtonsSelected != null)
            {
                AllPinButtonsSelected.Invoke(this, EventArgs.Empty);
            }
        }

        internal void GenerateResultFromCurrPinAndCheckIfWon(out int o_SamePos, out int o_NotSamePos, out bool o_IsWinningPin)
        {
            o_SamePos = 0;
            o_NotSamePos = 0;

            for (int i = 0; i < m_CurrentPinCombination.Length; i++)
            {
                for (int j = 0; j < m_WinningPinCombination.Length; j++)
                {
                    if (m_CurrentPinCombination[j] == m_WinningPinCombination[i])
                    {
                        if (i == j)
                        {
                            o_SamePos++;
                            break;
                        }
                        else
                        {
                            o_NotSamePos++;
                            break;
                        }
                    }

                }
            }

            o_IsWinningPin = o_SamePos == k_GuessesPinLength;
        }

        internal bool IsLastGuess()
        {
            return m_ActiveGuessIndex == m_GuessesAmount - 1;
        }

        internal void ResetGuess()
        {
            m_ActiveGuessIndex++;
            m_CurrentPinCombination = new Color[k_GuessesPinLength];
            m_AmountOfButtonsPickedOnCurrIndex = 0;
            r_AvailableColors.Clear();
            initializeAvailableColorDictionary();
        }

        internal void GenerateWinningCombination()
        {
            Color[] gameColorsCopy = (Color[])m_GameColors.Clone(); //Data structure will be changed and therefore we work on its copy
            Random randomPicker = new Random();
            int randIndex;
            int minIndex = 0;
            int maxIndex = gameColorsCopy.Length - 1;

            for (int i = 0; i < m_WinningPinCombination.Length; i++)
            {
                randIndex = randomPicker.Next(minIndex, maxIndex);
                m_WinningPinCombination[i] = gameColorsCopy[randIndex];
                swapColors(ref gameColorsCopy[minIndex], ref gameColorsCopy[randIndex]);
                minIndex++;
            }
        }

        private void swapColors(ref Color i_FirstColor, ref Color i_SecondColor)
        {
            Color tempColor = i_FirstColor;

            i_FirstColor = i_SecondColor;
            i_SecondColor = tempColor;
        }

        private void initializeAvailableColorDictionary()
        {
            foreach (Color color in m_GameColors)
            {
                r_AvailableColors.Add(color, true);
            }
        }
    }
}
