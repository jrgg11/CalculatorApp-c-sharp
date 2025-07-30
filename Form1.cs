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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string currentCalculation = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            currentCalculation += ((Button)sender).Text;
            textOut.Text = currentCalculation;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            currentCalculation = "";
            textOut.Text = currentCalculation;
        }

        private void buttonEquals_Click(object sender, EventArgs e)
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

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            if (currentCalculation.Length > 0)
            {
                currentCalculation = currentCalculation.Substring(0, currentCalculation.Length - 1);
                textOut.Text = currentCalculation;
            }
        }

        private void buttonSignChange_Click(object sender, EventArgs e)
        {
            if (currentCalculation.Length == 0) return;

            for (int i = currentCalculation.Length - 1; i >= 0; i--)
            {
                ChangeSignAtIndex(i);
            }
            textOut.Text = currentCalculation;

        }

        private void buttonParantheses_Click(object sender, EventArgs e)
        {
            int openParanthesesCount = currentCalculation.Count(c => c == '(');
            int closedParanthesesCount = currentCalculation.Count(c => c == ')');

            if (checkOpenParantheses(openParanthesesCount, closedParanthesesCount))
            {
                try
                {
                    if (checkLastChar(currentCalculation[currentCalculation.Length - 1], currentCalculation))
                        currentCalculation += "(";
                    else
                        currentCalculation += ")";
                }
                catch { currentCalculation += "("; }
            }
            else 
            {
                try
                {
                    if (checkLastChar(currentCalculation[currentCalculation.Length - 1], currentCalculation))
                        currentCalculation += "(";
                    else
                        currentCalculation += "*(";
                }
                catch { currentCalculation += "("; }
            }

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
        private bool checkOpenParantheses(int open, int closed)
        {
            if (open > closed)
                return true;
            else return false;
        }
        private bool checkLastChar(char c, string str)
        {
            if (  str.Length > 0 && (c == Symbols.PLUS_SIGN ||
                    c == Symbols.MINUS_SIGN ||
                    c == Symbols.MULTIPLY_SIGN ||
                    c == Symbols.DIVIDE_SIGN ||
                    c == Symbols.MODULO_SIGN ||
                    c == Symbols.LEFT_PARENTHESIS))
                return true;
            else return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
