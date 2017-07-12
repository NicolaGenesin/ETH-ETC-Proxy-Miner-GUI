using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Miner
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Boolean isMining = false;
        Process process = new Process();

        public MainWindow()
        {
            InitializeComponent();

            ethWalletAddress.Text = "0x76567212871cc11ef1be9b1f93e87d96a625d1co";
            etcWalletAddress.Text = "0x76567212871cc11ef1be9b1f93e87d96a625d1co";
            email.Text = "test@gmail.com";
            minerName.Text = "rig";
            isETC.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isMining)
            {
                isMining = false;
                ((Button)sender).Content = "Start";
                process.Kill();
            }
            else
            {
                isMining = true;
                ((Button)sender).Content = "Stop";

                Task.Delay(1000).ContinueWith(_ =>
                {
                    string cryptoTarget = isETC.IsChecked.Value ? "etc" : "eth";
                    string addressTarget = isETC.IsChecked.Value ? etcWalletAddress.Text : ethWalletAddress.Text;

                    process.StartInfo.FileName = @"Stub.exe";
                    process.StartInfo.Arguments = @"-X -epool " + cryptoTarget + "-eu1.nanopool.org:19999 -ewal " + addressTarget + "/" + minerName.Text + "/" + email.Text + " -mode 1";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    process.WaitForExit();
                });
            }
        }
    }
}
