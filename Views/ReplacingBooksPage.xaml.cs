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
    /// Interaction logic for ReplacingBooksPage.xaml
    /// </summary>
    /// 

    //Reference (Wiesław Šoltés, 2015)
    public partial class ReplacingBooksPage : Page
    {

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

        //Declaration of lists for storing the randomly generated call numbers
        //and the sorted call numbers. An IList used to allow the user to drag
        //and drop call numbers to reorder them.
        private List<Item> randomItems = new List<Item>();            
        private List<Item> sortedItems = new List<Item>();
        private IList<Item> _items = new ObservableCollection<Item>();

        //List which will contain the randomly generated call numbers.
        //List<double> List = new List<double>();

        public ReplacingBooksPage()
        {
            InitializeComponent();

            //Calling the generateListItems method to add the randomly generated
            //call numbers to the List instantiated above.
            generateListItems();

            listbox1.DisplayMemberPath = "CallNumber";
            listbox1.ItemsSource = _items;

            listbox1.PreviewMouseMove += ListBox_PreviewMouseMove;

            var style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            style.Setters.Add(
                new EventSetter(
                    ListBoxItem.PreviewMouseLeftButtonDownEvent,
                    new MouseButtonEventHandler(ListBoxItem_PreviewMouseLeftButtonDown)));
            style.Setters.Add(
                    new EventSetter(
                        ListBoxItem.DropEvent,
                        new DragEventHandler(ListBoxItem_Drop)));
            listbox1.ItemContainerStyle = style;
            

        }

        //Method to randomly generate call numbers.
        public void generateListItems()
        {
            //Random object for use with randomly generating call numbers.
            Random rd = new Random();

            //For loop to generate random numbers 10 times.
            for(int i = 0; i < 10; i++)
            {
                double rand_num = rd.Next(0, 999);

                //Generating a random decimal and using th etruncate method to
                //limit the decimal values to 3 decimal places (Skeet, 2010).
                double rand_decimal = rd.NextDouble();
                rand_decimal = Math.Truncate(rand_decimal * 1000) / 1000;

                double rand_call_num = rand_num + rand_decimal;

                string randSrtNum = rand_call_num.ToString();

                double randDecimal = Double.Parse(randSrtNum);

                //adding the randomly generated numbers to the IList for
                //use with the listbox drag and drop process.
                _items.Add(new Item(randDecimal));

            }

            //Storing the randomly generated call numbers in a List
            foreach (var item in _items)
            {
                randomItems.Add(item);
            }

            listbox1.ItemsSource = randomItems;

        }

        //Bubble sort algorithm to sort the call numbers in ascending order (Wade, 2020).
        public void BubbleSort(List<double> input)
        {
            var itemMoved = false;
            do
            {
                itemMoved = false;
                for (int i = 0; i < input.Count() - 1; i++)
                {
                    if (input[i] < input[i + 1])
                    {
                        var lowerValue = input[i + 1];
                        input[i + 1] = input[i];
                        input[i] = lowerValue;
                        itemMoved = true;
                    }
                }
            } while (itemMoved);
        }

        //Method to check if the user sorted the list items correctly.
        public void checkSorting()
        {
            //Calling the sort items method
            sortItems();

            //In this section of code 2 foreach loops are used to check if the 2 lists items match exactly.
            bool elementsMatch = true;

            List<double> temp_itemsList = new List<double>();

            List<double> tempSortedItemsList = new List<double>();

            foreach (var item in _items)
            {
                temp_itemsList.Add(item.CallNumber);
            }

            foreach (var item in sortedItems)
            {
                tempSortedItemsList.Add(item.CallNumber);
            }

            for (int i = 0; i < _items.Count; i++)
            {
                if (!temp_itemsList.ElementAt(i).Equals(tempSortedItemsList.ElementAt(i)))
                {
                    elementsMatch = false;
                }
            }

            //If all items in the users listbox, that they have sorted themselves, matches the items in the 
            //sortedItems list, which is sorted using the bubble sort method, then display the correct label
            //else display incorrect label.
            if (elementsMatch.Equals(true))
            {
                correctLabel.Visibility = Visibility.Visible;
                incorrectLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                incorrectLabel.Visibility = Visibility.Visible;
                correctLabel.Visibility = Visibility.Collapsed;
            }
        }

        //Method to sort the items within the sortedItems List in ascending order
        //Using the bubble sort method
        public void sortItems()
        {
            //storing the randomly generated call numbers into a different list
            //and using the bubble sort algorithm to sort that list in ascending order
            sortedItems = randomItems;

            List<double> itemsFromSorted = new List<double>();

            foreach (var item in sortedItems)
            {
                itemsFromSorted.Add(item.CallNumber);
            }

            //Clearing the sortedItems List to populate with newly sorted Items
            sortedItems.Clear();

            //calling the bubble sort method to sort the numbers
            BubbleSort(itemsFromSorted);

            //Adding the sorted numbers back to the sortedItems List. 
            foreach (var number in itemsFromSorted)
            {
                sortedItems.Add(new Item(number));
            }
        }

        //Event handler for check button clicked.
        private void checkBtn_Click(object sender, RoutedEventArgs e)
        {
            //Calling the checkSorting method.
            checkSorting();

            //Removing all potential items in listbox2 before adding sorted items to the listbox
            listbox2.Items.Clear();      

            //Iterating through each item in the list and adding them to the listbox.
            foreach (var item in sortedItems)
            {
                listbox2.Items.Add(item.CallNumber);
            }
        }

        
        
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
        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                var source = e.Data.GetData(typeof(Item)) as Item;
                var target = ((ListBoxItem)(sender)).DataContext as Item;

                int sourceIndex = listbox1.Items.IndexOf(source);
                int targetIndex = listbox1.Items.IndexOf(target);

                Move(source, sourceIndex, targetIndex);
            }
        }

        private void Move(Item source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                _items.Insert(targetIndex + 1, source);
                _items.RemoveAt(sourceIndex);
            }
            else
            {
                int removeIndex = sourceIndex + 1;
                if (_items.Count + 1 > removeIndex)
                {
                    _items.Insert(targetIndex, source);
                    _items.RemoveAt(removeIndex);
                }
            }
        }
    }
}
