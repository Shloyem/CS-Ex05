using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B17_Ex05
{
    delegate void ButtonColorChangeEventHandler(Color i_PreviousColor, Color i_SelectedColor, int i_ButtonIndex);

    internal class PinButton : Button
    {
        private bool m_IsColorChanged = false;
        private int m_ButtonIndex;
        private Size m_ButtonSize = new Size(40, 40);
        public event ButtonColorChangeEventHandler UpdateAColorWasPicked;

        public PinButton(int i_ButtonIndex)
        {
            m_ButtonIndex = i_ButtonIndex;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = m_ButtonSize;
        }

        internal void colorSelectionWindow_ClickedColorButton(Color i_SelectedColor)
        {
            OnUpdateAColorWasPicked(i_SelectedColor);
            this.BackColor = i_SelectedColor;
            m_IsColorChanged = true;
        }

        protected virtual void OnUpdateAColorWasPicked(Color i_PickedColor)
        {
            if (UpdateAColorWasPicked != null)
            {
                if (!this.m_IsColorChanged)
                {
                    UpdateAColorWasPicked.Invoke(Color.Empty, i_PickedColor, m_ButtonIndex);
                }
                else
                {
                    UpdateAColorWasPicked.Invoke(this.BackColor, i_PickedColor, m_ButtonIndex);
                }
            }
        }
    }
}

