using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PaistiGO
{

    public partial class MainWindow : Window
    {
        private Core applicationCore = null;
        private Task mainLoopTask = null;
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static CancellationToken cancelToken = tokenSource.Token;

        public MainWindow()
        {
            InitializeComponent();
            applicationCore = new Core(this);
            mainLoopTask = Task.Factory.StartNew(() => applicationCore.StartMainLoop(cancelToken));
        }

        public void AttachFailed()
        {
            tokenSource.Cancel();
            this.closeWindow();
        }

        public void closeWindow()
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                this.Close();
            }));
        }

        public void setDebugText(string text)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                textbox_debug.Text = text;
            }));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
