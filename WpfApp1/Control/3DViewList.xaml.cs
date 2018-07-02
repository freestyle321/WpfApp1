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

namespace WpfApp1.Control
{
    /// <summary>
    /// _3DViewList.xaml 的交互逻辑
    /// </summary>
    public partial class _3DViewList : UserControl
    {
        public _3DViewList()
        {
            InitializeComponent();
            scroll.ScrollChanged += Scroll_ScrollChanged;
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var sc = (ScrollViewer)sender;
            FreshButtonStatus(sc);
        }

        private void Button_Up(object sender, RoutedEventArgs e)
        {
            scroll.LineUp();
        }
        private void Button_Down(object sender, RoutedEventArgs e)
        {
            scroll.LineDown();
        }

        private void FreshButtonStatus(ScrollViewer sc)
        {

            if (sc.VerticalOffset > 0)
            {
                btn_up.IsEnabled = true;
            }
            else
            {
                btn_up.IsEnabled = false;
            }


            if (sc.VerticalOffset != sc.ScrollableHeight)
            {
                btn_down.IsEnabled = true;
            }
            else
            {
                btn_down.IsEnabled = false;
            }


        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
