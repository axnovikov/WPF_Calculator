using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        protected string mainOperation = String.Empty;
        protected string secOperation = String.Empty;
        protected bool continueInput = false;
        protected string memoryA = String.Empty;
        protected string memoryB = String.Empty;
        protected string memoryM = String.Empty;

        public MainWindow()
        {
            InitializeComponent();
            Screen.IsReadOnly = true;
        }

        private void Button_Num_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (!string.IsNullOrEmpty(mainOperation) && continueInput == false)
            {
                Screen.Clear();
                continueInput = true;
            }
            Screen.Text += button.Content;
            Screen.Text = RemoveLeadingZeros(Screen.Text);
        }

        private void Button_Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            mainOperation = button.Content as string;
            memoryA = Screen.Text;
            Screen.Clear();
        }

        private void Button_Zero_Click(object sender, RoutedEventArgs e)
        {
            if (Screen.Text != "0")
                Screen.Text += "0";
        }

        private void Button_Coma_Click(object sender, RoutedEventArgs e)
        {
            Screen.Text += ",";
        }

        private void Button_Solve_Click(object sender, RoutedEventArgs e)
        {
            memoryB = Screen.Text;
            switch (mainOperation)
            {
                case "+":
                    Screen.Text = AddTwoNumbers(memoryA, memoryB);
                    break;
                case "-":
                    Screen.Text = SubtractTwoNumbers(memoryA, memoryB);
                    break;
                case "x":
                    Screen.Text = MultiplyTwoNumbers(memoryA, memoryB);
                    break;
                case "/":
                    double numB = double.Parse(memoryB);
                    if (numB == 0)
                        Screen.Text = "Error";
                    else
                        Screen.Text = DivideTwoNumbers(memoryA, memoryB);
                    break;
            }

            memoryA = Screen.Text;
            memoryB = String.Empty;
            mainOperation = String.Empty;
        }

        // Cleaning buttons
        private void Button_Clean_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            secOperation = button.Content as string;
            switch (secOperation)
            {
                case "Del":
                    Screen.Text = RemoveLastDigit(Screen.Text);
                    break;
                case "C":
                    Screen.Text = "0";
                    memoryA = String.Empty;
                    memoryB = String.Empty;
                    break;
                case "CE":
                    Screen.Text = "0";
                    break;
            }
        }

        // Percent
        private void Button_Percent_Click(object sender, RoutedEventArgs e)
        {
            memoryB = Screen.Text;
            Button button = sender as Button;
            secOperation = button.Content as string;
            switch (secOperation)
            {
                case "+":
                    Screen.Text = SumPercent(memoryA, memoryB);
                    break;
                case "0":
                    Screen.Text = MinusPercent(memoryA, memoryB);
                    break;
                case "x":
                    Screen.Text = MultiplyPercent(memoryA, memoryB);
                    break;
                case "/":
                    Screen.Text = DividePercent(memoryA, memoryB);
                    break;
                default:
                    Screen.Text = "0";
                    break;
            }
        }

        // One actions buttons
        private void Button_Action_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            secOperation = button.Content as string;
            switch (secOperation)
            {
                case "x^2":
                    if (memoryA == null)
                        Screen.Text = PowDigit(memoryA);
                    else
                    {
                        memoryB = Screen.Text;
                        Screen.Text = PowDigit(memoryB);
                    }
                    break;
                case "1/x":
                    Screen.Text = DivideByOne(Screen.Text);
                    break;
                case "√":
                    Screen.Text = SqrtDigit(Screen.Text);
                    break;
                case "+/-":
                    Screen.Text = ChangeSign(Screen.Text);
                    break;
            }
        }

        // Memory buttons
        private void Button_Memory_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            secOperation = button.Content as string;
            switch (secOperation)
            {
                case "MS":
                    memoryM = Screen.Text;
                    break;
                case "M+":
                    if (memoryB != String.Empty)
                        memoryM = MPlus(memoryM, Screen.Text);
                    break;
                case "M-":
                    if (memoryB != String.Empty)
                        memoryM = MMinus(memoryM, Screen.Text);
                    break;
                case "MR":
                    Screen.Text = memoryM;
                    break;
                case "MC":
                    memoryM = null;
                    break;
            }
        }

        // Main action
        private string AddTwoNumbers(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double result = numA + numB;
            return result.ToString();
        }

        private string SubtractTwoNumbers(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double result = numA - numB;
            return result.ToString();
        }

        private string MultiplyTwoNumbers(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double result = numA * numB;
            return result.ToString();
        }

        private string DivideTwoNumbers(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double result = numA / numB;
            return result.ToString();
        }

        // Percent 
        private string SumPercent(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double ProcentOfNumB = numB / 100;
            double result = numA * ProcentOfNumB;
            result += numA;
            return result.ToString();
        }

        private string MinusPercent(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double ProcentOfNumB = numB / 100;
            double result = numA * ProcentOfNumB;
            result -= numA;
            return result.ToString();
        }

        private string MultiplyPercent(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double ProcentOfNumB = numB / 100;
            double result = numA * ProcentOfNumB;
            return result.ToString();
        }

        private string DividePercent(string strA, string strB)
        {
            double numA = double.Parse(strA);
            double numB = double.Parse(strB);
            double ProcentOfNumB = numB / 100;
            double result = numA / ProcentOfNumB;
            return result.ToString();
        }

        // Memory buttons 
        private string MPlus(string StrA, string StrB)
        {
            double numA = double.Parse(StrA);
            double numB = double.Parse(StrB);
            double result = numA + numB;
            return result.ToString();
        }

        private string MMinus(string StrA, string StrB)
        {
            double numA = double.Parse(StrA);
            double numB = double.Parse(StrB);
            double result = numA - numB;
            return result.ToString();
        }

        // One action
        private string ChangeSign(string input)
        {
            double result = double.Parse(input);
            result *= -1;
            return result.ToString();
        }

        private string PowDigit(string input)
        {
            double num = double.Parse(input);
            num = Math.Pow(num,2);
            string result = num.ToString();
            return result;
        }

        private string DivideByOne(string input)
        {
            double num = double.Parse(input);
            num = 1 / num;
            string result = num.ToString();
            return result;
        }

        private string SqrtDigit(string input)
        {
            double num = double.Parse(input);
            num = Math.Sqrt(num);
            string result = num.ToString();
            return result;
        }



        private string RemoveLastDigit(string input)
        {
            string output = input.Remove(input.Length - 1, 1);

            if (output.Length == 0)
                output = "0";

            return output;
        }

        private string RemoveLeadingZeros(string input)
        {
            if (input[0] == '0' && input[1] == ',')
                return input;
            else
                return input.TrimStart(new Char[] { '0' });
        }
    }
}
