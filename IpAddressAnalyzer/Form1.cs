using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool verify(Int64[] value)
        {
            for (int i = 0; i < 4; i++)
            {
                if ((value[i] <0) || (value[i]>255))
                {
                    return false;
                }
            }
            if ((value[4] < 1) || (value[4] > 30))
            {
                return false;
            }
            return true;
        }

        private String convert(Int64 value)
        {
            if (type.Text == "Decimal")
            {
                return "" + value;
            }
            else if (type.Text == "Hex")
            {
                return "" + value.ToString("X").PadLeft(2, '0'); ;
            }
            else if (type.Text == "Binary")
            {
                return "" + Convert.ToString(value, 2).PadLeft(8, '0'); ;
            }
            else
            {
                type.Text = "Decimal";
                return convert(value); 
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Int64 [] value = new Int64[5];
            try
            {
                value[0] = Int64.Parse(textSrc1.Text);
                value[1] = Int64.Parse(textSrc2.Text);
                value[2] = Int64.Parse(textSrc3.Text);
                value[3] = Int64.Parse(textSrc4.Text);
                value[4] = Int64.Parse(textSrc5.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("FormatException");
            }
            catch (OverflowException)
            {
                MessageBox.Show("OverflowException");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("ArgumentNullException");
            }

            if (!verify(value))
            {
                MessageBox.Show("Wrong input");
            }
            else
            {
                Int64 addressValue = 0;
                addressValue += value[0] * (Int64)(Math.Pow(2, 24));
                addressValue += value[1] * (Int64)(Math.Pow(2, 16));
                addressValue += value[2] * (Int64)(Math.Pow(2, 8));
                addressValue += value[3] * (Int64)(Math.Pow(2, 0));

                //broadcast
                Int64 allFFF = (Int64)(Math.Pow(2, 32) - 1);

                //network  
                Int64 addressNetwork = addressValue & (allFFF ^ (Int64)(Math.Pow(2, 32 - Int64.Parse(textSrc5.Text)) - 1));
                textNet1.Text = convert( ((addressNetwork / (Int64)(Math.Pow(2, 24))) & 0xff));
                textNet2.Text = convert( ((addressNetwork / (Int64)(Math.Pow(2, 16))) & 0xff));
                textNet3.Text = convert( ((addressNetwork / (Int64)(Math.Pow(2, 8))) & 0xff));
                textNet4.Text = convert( ((addressNetwork / (Int64)(Math.Pow(2, 0))) & 0xff));
            
                //host
                textHost1.Text = convert( ((addressValue / (Int64)(Math.Pow(2, 24))) & 0xff));
                textHost2.Text = convert( ((addressValue / (Int64)(Math.Pow(2, 16))) & 0xff));
                textHost3.Text = convert( ((addressValue / (Int64)(Math.Pow(2, 8))) & 0xff));
                textHost4.Text = convert( ((addressValue / (Int64)(Math.Pow(2, 0))) & 0xff));

                //first host
                Int64 addressFirst = addressNetwork + 1;
                textHostF1.Text = convert( ((addressFirst / (Int64)(Math.Pow(2, 24))) & 0xff));
                textHostF2.Text = convert( ((addressFirst / (Int64)(Math.Pow(2, 16))) & 0xff));
                textHostF3.Text = convert( ((addressFirst / (Int64)(Math.Pow(2, 8))) & 0xff));
                textHostF4.Text = convert( ((addressFirst / (Int64)(Math.Pow(2, 0))) & 0xff));


                //host num
                Int64 hostNum = (Int64)Math.Pow(2, (32 - value[4])) - 2;
                textHostNum.Text = "" + hostNum;

                //last host
                Int64 addressLast = addressFirst + hostNum - 1;
                textHostL1.Text = convert( ((addressLast / (Int64)(Math.Pow(2, 24))) & 0xff));
                textHostL2.Text = convert( ((addressLast / (Int64)(Math.Pow(2, 16))) & 0xff));
                textHostL3.Text = convert( ((addressLast / (Int64)(Math.Pow(2, 8))) & 0xff));
                textHostL4.Text = convert( ((addressLast / (Int64)(Math.Pow(2, 0))) & 0xff));

                Int64 addressBroadCast = addressLast + 1;
                textBroad1.Text = convert( ((addressBroadCast / (Int64)(Math.Pow(2, 24))) & 0xff));
                textBroad2.Text = convert( ((addressBroadCast / (Int64)(Math.Pow(2, 16))) & 0xff));
                textBroad3.Text = convert( ((addressBroadCast / (Int64)(Math.Pow(2, 8))) & 0xff));
                textBroad4.Text = convert( ((addressBroadCast / (Int64)(Math.Pow(2, 0))) & 0xff));

            }
        }



    }
}
