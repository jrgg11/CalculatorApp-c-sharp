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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
