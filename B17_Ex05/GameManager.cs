using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B17_Ex05
{
    internal class GameManager
    {
        private LogicUnit logicUnit = new LogicUnit();
        private GuessAmountSelectionWindow guessAmountSelectionWindow = new GuessAmountSelectionWindow();
        private GameForm gameForm;

        public GameManager()
        {
        }

        public void RunBullsAndCowsGame()
        {
            initializeLogicUnit();
            guessAmountSelectionWindow.GuessNumSetByUser += guessAmountSelectionWindow_GuessNumSetByUser;
            guessAmountSelectionWindow.ShowDialog();
        }

        private void initializeLogicUnit()
        {
            logicUnit.GenerateWinningCombination(); //Winning Combination resides in Logic Unit
            logicUnit.AllPinButtonsSelected += logicUnit_AllPinButtonsSelected;
        }

        private void guessAmountSelectionWindow_GuessNumSetByUser(int i_GuessesAmount)
        {
            logicUnit.GuessesAmount = i_GuessesAmount;
            startGameForm();
        }

        private void startGameForm()
        {
            gameForm = new GameForm(logicUnit.GuessesAmount, logicUnit.GuessesPinLength);
            activateNextGuess();
            gameForm.ShowDialog();
        }

        private void activateNextGuess()
        {
            foreach (PinButton pinButton in gameForm.m_Guesses[logicUnit.ActiveGuessIndex].m_GuessPin.m_PinButtons)
            {
                pinButton.Enabled = true;
                pinButton.Click += pinButton_Click;
                pinButton.UpdateAColorWasPicked += pinButton_UpdateAColorWasPicked;
            }
        }

        private void deactivateCurrentGuess()
        {
            gameForm.m_Guesses[logicUnit.ActiveGuessIndex].m_GenerateResultButton.Enabled = false;
            foreach (PinButton pinButton in gameForm.m_Guesses[logicUnit.ActiveGuessIndex].m_GuessPin.m_PinButtons)
            {
                pinButton.Enabled = false;
                pinButton.Click -= pinButton_Click;
                pinButton.UpdateAColorWasPicked -= pinButton_UpdateAColorWasPicked;
            }
        }

        private void pinButton_UpdateAColorWasPicked(Color i_PreviousColor, Color i_SelectedColor, int i_ButtonIndex)
        {
            logicUnit.UpdateAColorWasPicked(i_PreviousColor, i_SelectedColor, i_ButtonIndex);
        }

        private void logicUnit_AllPinButtonsSelected(object sender, EventArgs e)
        {
            bool isGenerateResultButtonEnabled = gameForm.m_Guesses[logicUnit.ActiveGuessIndex].m_GenerateResultButton.Enabled == true;

            if (!isGenerateResultButtonEnabled)
            {
                gameForm.m_Guesses[logicUnit.ActiveGuessIndex].m_GenerateResultButton.Click += m_GenerateResultButton_Click;
            }

            gameForm.m_Guesses[logicUnit.ActiveGuessIndex].m_GenerateResultButton.Enabled = true;
        }

        private void m_GenerateResultButton_Click(object sender, EventArgs e)
        {
            deactivateCurrentGuess();
            logicUnit.GenerateResultFromCurrPinAndCheckIfWon(out int samePos, out int notSamePos, out bool isWinningPin);
            gameForm.m_Guesses[logicUnit.ActiveGuessIndex].m_GuessResult.GenerateResultView(samePos, notSamePos);
            if (isWinningPin)
            {
                enteredPinIsCurrect();
            }
            else
            {
                enteredPinIsNotCurrect();
            }
        }

        private void enteredPinIsNotCurrect()
        {
            bool isLastGuess = logicUnit.IsLastGuess();

            if (isLastGuess)
            {
                printLosingMsg();
            }
            else
            {
                logicUnit.ResetGuess();
                activateNextGuess();
            }
        }

        private void printLosingMsg()
        {
            if (MessageBox.Show(
@"Game Finished, you lost :(
would you like to play again?"
                , "Restart Match"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                restartGame();
            }
            else
            {
                gameForm.Close();
            }
        }

        private void enteredPinIsCurrect()
        {
            gameForm.m_WinningGuess.DisplayWinningGuess(logicUnit.WinningPinCombination);
            printWinningMsg();
        }

        private void printWinningMsg()
        {
            if (MessageBox.Show(
@"Game Finished, you won :)
would you like to play again?"
                , "Restart Match"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                restartGame();
            }
            else
            {
                gameForm.Close();
            }
        }

        private void restartGame()
        {
            logicUnit = new LogicUnit();
            guessAmountSelectionWindow = new GuessAmountSelectionWindow();
            gameForm.Dispose();
            this.RunBullsAndCowsGame();
        }

        private void pinButton_Click(object sender, EventArgs e)
        {
            ColorSelectionWindow colorSelectionWindow = new ColorSelectionWindow(logicUnit.AvailableColors);

            colorSelectionWindow.ClickedColorButton += (sender as PinButton).colorSelectionWindow_ClickedColorButton;
            colorSelectionWindow.ShowDialog();
        }

    }
}
