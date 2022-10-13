using DeweyTrain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DeweyTrain.Views
{
    /// <summary>
    /// Interaction logic for IdentifyingAreasPage.xaml
    /// </summary>
    public partial class IdentifyingAreasPage : Page
    {
        //The dictionary that will be used to compare the users answers to.
        private Dictionary<int, string> questionsAnswer = new Dictionary<int, string>()
        {
            {000, "General Knowledge"},
            {100, "Psychology and Philosophy"},
            {200, "Religions and Mythology"},
            {300, "Social Sciences and Folklore"},
            {400, "Languages and Grammar"},
            {500, "Math and Science"},
            {600, "Medicine and Technology"},
            {700, "Arts and Recreation"},
            {800, "Literature"},
            {900, "Geography and History"}
        };

        private IList<CallNumber> _callNumbers = new ObservableCollection<CallNumber>();
        private IList<Description> _descriptions = new ObservableCollection<Description>();

        public IdentifyingAreasPage()
        {
            InitializeComponent();

            generateListItems();
            initializeCallNumbersBox();
            initializeDescriptionsBox();
        }

        //Method to generate and add the items needed for the callNumbers and descriptions lists.
        public void generateListItems()
        {
            
            _callNumbers.Add(new CallNumber(000));
            _callNumbers.Add(new CallNumber(100));
            _callNumbers.Add(new CallNumber(200));
            _callNumbers.Add(new CallNumber(300));

            _descriptions.Add(new Description("General Knowledge"));
            _descriptions.Add(new Description("Psychology and Philosophy"));
            _descriptions.Add(new Description("Religions and Mythology"));
            _descriptions.Add(new Description("Social Sciences and Folklore"));
            _descriptions.Add(new Description("Languages and Grammar"));
            _descriptions.Add(new Description("Math and Science"));
            _descriptions.Add(new Description("Medicine and Technology"));
        }

        //initializing events and styling for the callNumbersListBox
        public void initializeCallNumbersBox()
        {
            callNumbersListBox.ItemsSource = _callNumbers;

            callNumbersListBox.PreviewMouseMove += ListBox_PreviewMouseMove;

            var style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            style.Setters.Add(
                new EventSetter(
                    ListBoxItem.PreviewMouseLeftButtonDownEvent,
                    new MouseButtonEventHandler(ListBoxItem_PreviewMouseLeftButtonDown)));
            style.Setters.Add(
                    new EventSetter(
                        ListBoxItem.DropEvent,
                        new DragEventHandler(callNumberListBoxItem_Drop)));
            callNumbersListBox.ItemContainerStyle = style;
        }

        //initializing events and styling for the descriptionsListBox
        public void initializeDescriptionsBox()
        {
            descriptionsListBox.ItemsSource = _descriptions;

            descriptionsListBox.PreviewMouseMove += ListBox_PreviewMouseMove;

            var style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            style.Setters.Add(
                new EventSetter(
                    ListBoxItem.PreviewMouseLeftButtonDownEvent,
                    new MouseButtonEventHandler(ListBoxItem_PreviewMouseLeftButtonDown)));
            style.Setters.Add(
                    new EventSetter(
                        ListBoxItem.DropEvent,
                        new DragEventHandler(descriptionListBoxItem_Drop)));
            descriptionsListBox.ItemContainerStyle = style;
        }


        //The following method was taken from StackOverFlow:
        //Author: Wiesław Šoltés
        //Link: https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop
        private Point _dragStartPoint;
        private T FindVisualParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            return FindVisualParent<T>(parentObject);
        }

        //The following method was taken from StackOverFlow:
        //Author: Wiesław Šoltés
        //Link: https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop
        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(null);
            Vector diff = _dragStartPoint - point;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                var lb = sender as ListBox;
                var lbi = FindVisualParent<ListBoxItem>(((DependencyObject)e.OriginalSource));
                if (lbi != null)
                {
                    DragDrop.DoDragDrop(lbi, lbi.DataContext, DragDropEffects.Move);
                }
            }
        }

        //The following method was taken from StackOverFlow:
        //Author: Wiesław Šoltés
        //Link: https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop
        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        //CallNumber Listbox drop and move events
        //=========================================================================================================

        //The following method was taken from StackOverFlow:
        //Author: Wiesław Šoltés
        //Link: https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop
        private void callNumberListBoxItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                var source = e.Data.GetData(typeof(CallNumber)) as CallNumber;
                var target = ((ListBoxItem)(sender)).DataContext as CallNumber;

                int sourceIndex = callNumbersListBox.Items.IndexOf(source);
                int targetIndex = callNumbersListBox.Items.IndexOf(target);

                callNumbersMove(source, sourceIndex, targetIndex);
            }
        }

        //The following method was taken from StackOverFlow:
        //Author: Wiesław Šoltés
        //Link: https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop
        private void callNumbersMove(CallNumber source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                _callNumbers.Insert(targetIndex + 1, source);
                _callNumbers.RemoveAt(sourceIndex);
            }
            else
            {
                int removeIndex = sourceIndex + 1;
                if (_callNumbers.Count + 1 > removeIndex)
                {
                    _callNumbers.Insert(targetIndex, source);
                    _callNumbers.RemoveAt(removeIndex);
                }
            }
        }
        //=========================================================================================================


        //Description Listbox drop and move events
        //=========================================================================================================

        //The following method was taken from StackOverFlow:
        //Author: Wiesław Šoltés
        //Link: https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop
        private void descriptionListBoxItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                var source = e.Data.GetData(typeof(Description)) as Description;
                var target = ((ListBoxItem)(sender)).DataContext as Description;

                int sourceIndex = descriptionsListBox.Items.IndexOf(source);
                int targetIndex = descriptionsListBox.Items.IndexOf(target);

                descriptionsMove(source, sourceIndex, targetIndex);
            }
        }

        //The following method was taken from StackOverFlow:
        //Author: Wiesław Šoltés
        //Link: https://stackoverflow.com/questions/3350187/wpf-c-rearrange-items-in-listbox-via-drag-and-drop
        private void descriptionsMove(Description source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                _descriptions.Insert(targetIndex + 1, source);
                _descriptions.RemoveAt(sourceIndex);
            }
            else
            {
                int removeIndex = sourceIndex + 1;
                if (_descriptions.Count + 1 > removeIndex)
                {
                    _descriptions.Insert(targetIndex, source);
                    _descriptions.RemoveAt(removeIndex);
                }
            }
        }
        //=========================================================================================================

        private void checkAnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            testListBox.ItemsSource = _descriptions;
        }

    }
}
