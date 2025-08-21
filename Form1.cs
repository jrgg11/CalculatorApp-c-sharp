using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.BusinessLogic;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private string currentCalculation = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            currentCalculation += ((Button)sender).Text;
            textOut.Text = currentCalculation;
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            currentCalculation = "";
            textOut.Text = currentCalculation;
        }

        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            string formattedCalculation = currentCalculation.ToString();
            try
            {
                textOut.Text = new DataTable().Compute(formattedCalculation, null).ToString();
                currentCalculation = textOut.Text;
            }
            catch
            {
                textOut.Text = "Error!";
                currentCalculation = "";
            }
        }

        private void ButtonClearEntry_Click(object sender, EventArgs e)
        {
            if (currentCalculation.Length > 0)
            {
                currentCalculation = currentCalculation.Substring(0, currentCalculation.Length - 1);
                textOut.Text = currentCalculation;
            }
        }

        private void ButtonSignChange_Click(object sender, EventArgs e)
        {
            if (currentCalculation.Length == 0) return;

            for (int i = currentCalculation.Length - 1; i >= 0; i--)
            {
                ChangeSignAtIndex(i);
            }
            textOut.Text = currentCalculation;

        }

        private void ButtonParantheses_Click(object sender, EventArgs e)
        {
            currentCalculation = BracketHelpers.AddBracket(currentCalculation);

            textOut.Text = currentCalculation;
        }
        private void ChangeSignAtIndex(int i)
        {
            if ((currentCalculation[i] == Symbols.PLUS_SIGN || currentCalculation[i] == Symbols.MINUS_SIGN) && currentCalculation[i + 1] != 0)
            {
                // Change the sign of the last operator
                if (currentCalculation[i] == Symbols.PLUS_SIGN)
                    currentCalculation = currentCalculation.Remove(i, 1).Insert(i, Symbols.MINUS_SIGN.ToString());
                else
                    currentCalculation = currentCalculation.Remove(i, 1).Insert(i, Symbols.PLUS_SIGN.ToString());

            }
            else if ((currentCalculation[i] == '*' || currentCalculation[i] == '/' || currentCalculation[i] == '%') && currentCalculation[i + 1] != 0)
            {
                currentCalculation = currentCalculation.Insert(i + 1, "(-");
                currentCalculation += ")";

            }
            else 
                if (i == 0 && currentCalculation[i] != 0)
                    currentCalculation = currentCalculation.Insert(i, Symbols.MINUS_SIGN.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
