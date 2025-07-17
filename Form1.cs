using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            if (currentCalculation.Length > 0)
            {
                for(int i = currentCalculation.Length - 1; i >= 0; i--)
                {
                    if ((currentCalculation[i] == '+' || currentCalculation[i] == '-') && currentCalculation[i+1]!=0)
                    {
                        // Change the sign of the last operator
                        if (currentCalculation[i] == '+')
                            currentCalculation = currentCalculation.Remove(i, 1).Insert(i, "-");
                        else
                            currentCalculation = currentCalculation.Remove(i, 1).Insert(i, "+");
                        break;
                    }
                    else if ((currentCalculation[i] == '*' || currentCalculation[i] == '/' || currentCalculation[i] == '%') && currentCalculation[i + 1] != 0)
                    {
                        currentCalculation = currentCalculation.Insert(i+1, "(-");
                        currentCalculation += ")";
                        break;
                    }
                    else if(i==0 && currentCalculation[i] != 0) currentCalculation = currentCalculation.Insert(i, "-");
                }
                textOut.Text = currentCalculation;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
