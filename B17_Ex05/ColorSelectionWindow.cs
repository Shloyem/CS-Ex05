using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B17_Ex05
{
    public delegate void ColorPickEventHandler(Color selectedColor);

    internal class ColorSelectionWindow : Form
    {
        internal Button[] m_ColorChagingButtons;
        private Size m_ButtonSize = new Size(45, 30);
        private const int k_Space = 10;
        private Dictionary<Color, bool> m_AvailableColors; 
        public event ColorPickEventHandler ClickedColorButton;

        public ColorSelectionWindow(Dictionary<Color, bool> i_AvailableColors)
        {
            m_AvailableColors = i_AvailableColors;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            initializeButtons();
            this.Size = new Size((m_ButtonSize.Width + k_Space) * (m_AvailableColors.Count / 2) + 3 * k_Space,
                                    2 * (m_ButtonSize.Height + k_Space) + 5 * k_Space);
            this.Text = "Pick A Color";
            adjustButtonsProperties();
        }

        private void adjustButtonsProperties()
        {
            int initIndex = 0;

            foreach (KeyValuePair<Color, bool> buttonInfo in m_AvailableColors)
            {
                m_ColorChagingButtons[initIndex].BackColor = buttonInfo.Key;
                m_ColorChagingButtons[initIndex].Enabled = buttonInfo.Value;
                this.Controls.Add(m_ColorChagingButtons[initIndex]);
                m_ColorChagingButtons[initIndex].Click += button_Click;
                m_ColorChagingButtons[initIndex].Location = new Point((initIndex / 2) * (m_ButtonSize.Width + k_Space) + k_Space,
                                                                        (initIndex % 2) * (m_ButtonSize.Height + k_Space) + k_Space);
                initIndex++;
            }
        }

        private void initializeButtons()
        {
            int numOfButtons = m_AvailableColors.Count;

            m_ColorChagingButtons = new Button[numOfButtons];
            for (int i = 0; i < numOfButtons; i++)
            {
                m_ColorChagingButtons[i] = new Button();
                m_ColorChagingButtons[i].Size = m_ButtonSize;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Color selectedColor = (sender as Button).BackColor;
            OnClickedColorButton(selectedColor);
        }

        protected virtual void OnClickedColorButton(Color i_SelectedColor)
        {
            if (ClickedColorButton != null)
            {
                ClickedColorButton.Invoke(i_SelectedColor);
            }
            
            this.Close();
        }
    }
}
