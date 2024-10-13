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

namespace Unity3DDemo
{
    /// <summary>
    /// UnityContainer.xaml 的交互逻辑
    /// </summary>
    public partial class UnityContainer : UserControl
    {
        private UnityApp _unityApp;

        public UnityContainer()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _unityApp = new UnityApp();
            _host.Child = _unityApp;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _unityApp.Dispose();
        }
    }
}
