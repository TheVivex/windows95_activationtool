using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace win95__keygenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    

    public partial class MainWindow : Window
    {
        public int version = 0;
        
        bool dig_check3(int x)
        {
            if(x == 333 || x == 444 || x == 555 || x == 666 || x == 777 ||
                x == 888 || x == 999)
            {
                return false;
            }
            return true;
        }
        
        bool dig_check7(int x)
        {
            int sum = 0;
            string test = x.ToString();
            for(int i = 0; i < test.Length; i++)
            {
                if (test[i] == '9')
                    return false;
            }

            for(int i = 0; i < 7; i++)
            {
                sum = sum + (x % 10);
                x = x / 10;
            }

            if(sum % 7 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        bool secOK(int x)
        {
            if(x >= 0 && x <= 2)
            {
                return true;
            }
            else if (x >= 95)
            {
                return true;
            }
            return false;
        }

        string Gen(int x)
        {
            Random r = new Random();

            int last_7_digit_int;
            string last_7_digit = "";

            do
            {
                last_7_digit_int = r.Next(0, 9999);
            } while (!dig_check7(last_7_digit_int));
            string temp_last_7_digit = "0000000";
            temp_last_7_digit = temp_last_7_digit + last_7_digit_int.ToString();
            string rev_last_7_digit = "";
            for (int i = temp_last_7_digit.Length - 1; rev_last_7_digit.Length < 7; i--)
            {
                rev_last_7_digit = rev_last_7_digit + temp_last_7_digit[i];
            }
            
            for(int i = 6; i > -1; i--)
            {
                last_7_digit = last_7_digit + rev_last_7_digit[i];
            }

            if (x == 0)
            {
                int first_3_digit_int;
                string first_3_digit;
                do
                {
                    first_3_digit_int = r.Next(0, 999);
                } while (!dig_check3(first_3_digit_int));

                if (first_3_digit_int < 100 && first_3_digit_int > 9)
                {
                    first_3_digit = string.Format("0{0}", first_3_digit_int);
                }
                else if (first_3_digit_int < 10)
                {
                    first_3_digit = string.Format("00{0}", first_3_digit_int);
                }
                else
                {
                    first_3_digit = first_3_digit_int.ToString();
                }
                return String.Format("{0}-{1}", first_3_digit, last_7_digit);
            }
            else
            {
                int first_3_digit_int;
                string first_3_digit;
                first_3_digit_int = r.Next(1, 366);
                string temp_first_3_digit = "00" + first_3_digit_int.ToString();

                if(temp_first_3_digit.Length == 4)
                {
                    first_3_digit = "0" + first_3_digit_int.ToString();
                }
                else if (temp_first_3_digit.Length == 5)
                {
                    first_3_digit = first_3_digit_int.ToString();
                }
                else
                {
                    first_3_digit = temp_first_3_digit;
                }

                int second_2_digit_int;
                string second_2_digit;
                do
                {
                    second_2_digit_int = r.Next(0, 99);
                } while (!secOK(second_2_digit_int));
                
                if(second_2_digit_int < 10)
                {
                    second_2_digit = "0" + second_2_digit_int.ToString();
                }
                else
                {
                    second_2_digit = second_2_digit_int.ToString();
                }

                int last_5_digit = r.Next(10000, 99999);

                return String.Format("{0}{1}-OEM-{2}-{3}",first_3_digit,second_2_digit,last_7_digit,last_5_digit.ToString());
            }

            
        }


        public MainWindow()
        {
            InitializeComponent();
            ver_cb.Items.Add("Retail");
            ver_cb.Items.Add("OEM");
            ver_cb.SelectedIndex = 1;
        }

        private void gen_bt_Click(object sender, RoutedEventArgs e)
        {
            key_tb.Text = Gen(version);
        }

        private void ver_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            version = ver_cb.SelectedIndex;
        }
    }
}
