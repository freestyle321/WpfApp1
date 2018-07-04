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
using WpfApp1.Tool;

namespace WpfApp1.Control
{
    /// <summary>
    /// _3DViewList.xaml 的交互逻辑
    /// </summary>
    public partial class _3DViewList : UserControl
    {
        ScrollViewer scrollViewer;
        NotifyObject obj = new NotifyObject();
        public _3DViewList()
        {
            InitializeComponent();
            ViewList.SourceUpdated += ViewList_SourceUpdated;
            ViewList.SelectionChanged += ViewList_SelectionChanged;
            //   ViewList.ItemsSource = ModelList;

            ViewList.ItemsSource = ModelList;
        }

        private void ViewList_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            scrollViewer = GetDescendantByType(ViewList, typeof(ScrollViewer)) as ScrollViewer;
            if(scrollViewer!=null)
            {
                scrollViewer.ScrollChanged += Scroll_ScrollChanged;
            }
           
        }

        private void ViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedModel = ViewList.SelectedItem;
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var sc = (ScrollViewer)sender;
            FreshButtonStatus(sc);
        }

        private void Button_Up(object sender, RoutedEventArgs e)
        {
            if(scrollViewer!=null)
              scrollViewer.LineUp();

        }
        private void Button_Down(object sender, RoutedEventArgs e)
        {
            if (scrollViewer != null)
                scrollViewer.LineDown();
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



        public object SelectedModel
        {
            get { return (object)GetValue(SelectedModelProperty); }
            set { SetValue(SelectedModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedModelProperty =
            DependencyProperty.Register("SelectedModel", typeof(object), typeof(_3DViewList), new PropertyMetadata(0));


        private List<ModelClass> _ModelList { get; set; }
        public List<ModelClass> ModelList
        {
            set
            {
                _ModelList = value;

                obj.NotifyPropertyChanged("ModelList");

            }
            get { return this._ModelList; }
        }


   
        public  Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null)
            {
                return null;
            }
            if (element.GetType() == type)
            {
                return element;
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                {
                    break;
                }
            }
            return foundElement;
        }


    }
}
