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

namespace ToldEm.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WPFHost _host;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Action<string> doLog = message =>
            {
                txtLog.Text = message + "\r\n" + txtLog.Text;

                return;
            };

            _host = new WPFHost(cvsMain, doLog);

            var g = new ToldEm.Core.GameCore();
//            g.Setup(_host, new ToldEm.Core.Demo());
            g.Setup(_host, new BushRun.BushRunGame());
        }
    }
}
