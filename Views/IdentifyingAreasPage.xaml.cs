using DeweyTrain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private static Random rng = new Random();

        //The dictionary that will be used to compare the users answers to.
        private Dictionary<string, string> questions = new Dictionary<string, string>();

        //The dictionary that will store all the users answers.
        private Dictionary<string, string> qADict = new Dictionary<string, string>();
        private Dictionary<string, string> userAnswer = new Dictionary<string, string>();

        //The ArrayLists that will display the call numbers and descriptions in the list boxes.
        public ArrayList _callNumbers = new ArrayList();
        public ArrayList _descriptions = new ArrayList();

        public ArrayList _callNumbersAnswer = new ArrayList();
        public ArrayList _descriptionsAnswer = new ArrayList();

        public IdentifyingAreasPage()
        {
            InitializeComponents();
        }

        //Method to initialize the necessary components for the page.
        public void InitializeComponents()
        {
            InitializeComponent();
            generateListItems();
            QuestionListBox1.ItemsSource = _callNumbers;
            QuestionListBox2.ItemsSource = _descriptions;
        }

        //Method to poulate the questions dictionary
        public void populateQuestions()
        {
            questions.Clear();
            questions.Add("000", "General Knowledge");
            questions.Add("100", "Psychology and Philosophy");
            questions.Add("200", "Religions and Mythology");
            questions.Add("300", "Social Sciences and Folklore");
            questions.Add("400", "Languages and Grammar");
            questions.Add("500", "Math and Science");
            questions.Add("600", "Medicine and Technology");
            questions.Add("700", "Arts and Recreation");
            questions.Add("800", "Literature");
            questions.Add("900", "Geography and History");
        }

        //Method to generate and add the items needed for the callNumbers and descriptions lists.
        public void generateListItems()
        {
            //Calling the populate questions dictionary method
            populateQuestions();

            Random r = new Random();
            int max = 4;
            int now;
            //Retrieving random call numbers and their descriptions from the dictionary of call numbers and descriptions.
            //The following code refrences StackOverFlow:
            //Author: Tim
            //https://stackoverflow.com/questions/23543128/take-a-random-item-from-one-list-and-add-it-to-another-list
            for (int i = 0; i < max; i++)
            {
                now = r.Next(0, questions.Count);
                _callNumbers.Add(questions.ElementAt(now).Key.ToString());
                _descriptions.Add(questions.ElementAt(now).Value.ToString());
                qADict.Add(questions.ElementAt(now).Key.ToString(), questions.ElementAt(now).Value.ToString());
                questions.Remove(questions.ElementAt(now).Key); 
            }

            int now2;
            //Retrieving extra random descriptions from the dictionary of call numbers and descriptions.
            for (int i = 0; i < 3; i++)
            {
                now2 = r.Next(0, questions.Count);

                _descriptions.Add(questions.ElementAt(now2).Value.ToString());
                questions.Remove(questions.ElementAt(now2).Key);
            }

            //Shuffle the values in the descriptions column.
            _descriptions.Sort();
        }

        //Check the user's answer.
        private void checkAnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            //If the answerslist box does not contain any items display an error message.
            if (AnswerListBox1.Items.Count<1 || AnswerListBox2.Items.Count < 1)
            {
                errorEmptyAnswerLabel.Visibility = Visibility.Visible;
            }
            //If there is not a description for each call number or vice versa display an error message.
            if (AnswerListBox1.Items.Count != AnswerListBox2.Items.Count)
            {
                errorMatchingLabel.Visibility = Visibility.Visible;
            }
            else
            {
                errorMatchingLabel.Visibility = Visibility.Collapsed;
                errorEmptyAnswerLabel.Visibility = Visibility.Collapsed;

                //Clear out any old stored values in the user's Answers dictionary.
                userAnswer.Clear();

                //Getting the items form the users Answer list boxes and adding them to the userAnswers Dictionary.
                for (int i = 0; i < AnswerListBox1.Items.Count; i++)
                {
                    userAnswer.Add(AnswerListBox1.Items.GetItemAt(i).ToString(), AnswerListBox2.Items.GetItemAt(i).ToString());
                }

                bool isUserCorrect = false;

                //The following code refrences StackOverFlow:
                //Author: Jon Skeet
                //https://stackoverflow.com/questions/6753196/comparing-two-dictionaries-in-c-sharp

                //Sorting the dictionary that stores the users answers to the question as well as
                //the dictionary that stores the actual answers to the question and then comparing
                //their key value pairs with one another.
                var orderedqADict = qADict.OrderBy(kvp => kvp.Key);
                var orderedUserAnswer = userAnswer.OrderBy(kvp => kvp.Key);

                if (orderedqADict.SequenceEqual(orderedUserAnswer))
                {
                    isUserCorrect = true;
                }

                if (isUserCorrect.Equals(true))
                {
                    correctLabel.Visibility = Visibility.Visible;
                    incorrectLabel.Visibility = Visibility.Collapsed;
                    MessageBox.Show("You have received 5 points", "CONGRATULATIONS", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReplacingBooksPage.userPoints += 5;
                    userPointsLabel.Content = ReplacingBooksPage.userPoints.ToString();
                    if (ReplacingBooksPage.userPoints == 10)
                    {
                        BitmapImage badge = new BitmapImage(new Uri("pack://application:,,,/ImgAssets/accepted.png"));
                        MessageBox.Show("You have received a new Badge shown in the bottom right corner", "CONGRATULATIONS", MessageBoxButton.OK, MessageBoxImage.Information);
                        badgeImg.Source = badge;
                    }
                    if(ReplacingBooksPage.userPoints == 20)
                    {
                        BitmapImage badge = new BitmapImage(new Uri("pack://application:,,,/ImgAssets/badge2.jpg"));
                        MessageBox.Show("You have received a new Badge shown in the bottom right corner", "CONGRATULATIONS", MessageBoxButton.OK, MessageBoxImage.Information);
                        badgeImg.Source = badge;
                    }
                }
                else
                {
                    incorrectLabel.Visibility = Visibility.Visible;
                    correctLabel.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>  
        /// Refreshes data binding  
        /// </summary>  
        private void ApplyCallNumberDataBinding()
        {
            QuestionListBox1.ItemsSource = null;
            // Bind ArrayList with the ListBox  
            QuestionListBox1.ItemsSource = _callNumbers;
        }

        private void ApplyDescriptionDataBinding()
        {
            QuestionListBox2.ItemsSource = null;
            // Bind ArrayList with the ListBox  
            QuestionListBox2.ItemsSource = _descriptions;
        }

        //Declarations for variables used in button Add and Remove clicked events.
        //The following button click events refrences c-sharpcorner.com:
        //Author: Mahesh Chand
        //https://www.c-sharpcorner.com/uploadfile/mahesh/transferring-data-from-one-listbox-to-another-in-wpf/

        public string? currentItemText;
        int currentItemIndex;

        private void AddButton1_Click(object sender, RoutedEventArgs e)
        {
            //Error handling (Null reference exception)
            //(https://www.tutorialkart.com/c-sharp-tutorial/c-sharp-nullreferenceexception/#:~:text=How%20to%20Solve%20or%20Handle%20NullReferenceException%3F%201%20Using,is%20not%20null.%20Program.cs%20...%203%20Conclusion%20)
            if (QuestionListBox1.SelectedItem == null)
            {
                errorSelectItemAddLabel.Visibility = Visibility.Visible;
            }
            else
            {
                errorSelectItemAddLabel.Visibility = Visibility.Hidden;
                // Find the right item and it's value and index  
                currentItemText = QuestionListBox1.SelectedValue.ToString();
                currentItemIndex = QuestionListBox1.SelectedIndex;
                AnswerListBox1.Items.Add(currentItemText);
                if (_callNumbers != null)
                {
                    _callNumbers.RemoveAt(currentItemIndex);
                }
                // Refresh data binding  
                ApplyCallNumberDataBinding();
            }      
        }

        private void RemoveButton1_Click(object sender, RoutedEventArgs e)
        {
            //Error handling (Null reference exception)
            //(https://www.tutorialkart.com/c-sharp-tutorial/c-sharp-nullreferenceexception/#:~:text=How%20to%20Solve%20or%20Handle%20NullReferenceException%3F%201%20Using,is%20not%20null.%20Program.cs%20...%203%20Conclusion%20)
            if (AnswerListBox1.SelectedItem == null)
            {
                errorSelectItemRemoveLabel.Visibility = Visibility.Visible;
            }
            else
            {
                errorSelectItemRemoveLabel.Visibility = Visibility.Collapsed;
                // Find the right item and it's value and index  
                currentItemText = AnswerListBox1.SelectedValue.ToString();
                currentItemIndex = AnswerListBox1.SelectedIndex;
                // Add RightListBox item to the ArrayList  
                _callNumbers.Add(currentItemText);
                AnswerListBox1.Items.RemoveAt(AnswerListBox1.Items.IndexOf(AnswerListBox1.SelectedItem));
                // Refresh data binding  
                ApplyCallNumberDataBinding();
            }               
        }

        private void AddButton2_Click(object sender, RoutedEventArgs e)
        {
            //Error handling (Null reference exception)
            //(https://www.tutorialkart.com/c-sharp-tutorial/c-sharp-nullreferenceexception/#:~:text=How%20to%20Solve%20or%20Handle%20NullReferenceException%3F%201%20Using,is%20not%20null.%20Program.cs%20...%203%20Conclusion%20)
            if (QuestionListBox2.SelectedItem == null)
            {
                errorSelectItemAddLabel.Visibility = Visibility.Visible;
            }
            else
            {
                errorSelectItemAddLabel.Visibility = Visibility.Collapsed;
                // Find the right item and it's value and index  
                currentItemText = QuestionListBox2.SelectedValue.ToString();
                currentItemIndex = QuestionListBox2.SelectedIndex;
                AnswerListBox2.Items.Add(currentItemText);
                if (_descriptions != null)
                {
                    _descriptions.RemoveAt(currentItemIndex);
                }
                // Refresh data binding  
                ApplyDescriptionDataBinding();
            } 
        }
    

        private void RemoveButton2_Click(object sender, RoutedEventArgs e)
        {
            //Error handling (Null reference exception)
            //(https://www.tutorialkart.com/c-sharp-tutorial/c-sharp-nullreferenceexception/#:~:text=How%20to%20Solve%20or%20Handle%20NullReferenceException%3F%201%20Using,is%20not%20null.%20Program.cs%20...%203%20Conclusion%20)
            if (AnswerListBox2.SelectedItem == null)
            {
                errorSelectItemRemoveLabel.Visibility = Visibility.Visible;
            }
            else
            {
                errorSelectItemRemoveLabel.Visibility = Visibility.Collapsed;
                // Find the right item and it's value and index  
                currentItemText = AnswerListBox2.SelectedValue.ToString();
                currentItemIndex = AnswerListBox2.SelectedIndex;
                // Add RightListBox item to the ArrayList  
                _descriptions.Add(currentItemText);
                AnswerListBox2.Items.RemoveAt(AnswerListBox2.Items.IndexOf(AnswerListBox2.SelectedItem));
                // Refresh data binding  
                ApplyDescriptionDataBinding();
            }      
        }

        private void restartAreasBtn_Click(object sender, RoutedEventArgs e)
        {
            //Clear all items in all collections and list boxes
            _callNumbers.Clear();
            _descriptions.Clear();
            _callNumbersAnswer.Clear();
            _descriptionsAnswer.Clear();
            qADict.Clear();
            userAnswer.Clear();

            QuestionListBox1.ItemsSource = null;
            QuestionListBox2.ItemsSource = null;

            QuestionListBox1.Items.Clear();
            QuestionListBox2.Items.Clear();
            AnswerListBox1.Items.Clear();
            AnswerListBox2.Items.Clear();

            errorSelectItemAddLabel.Visibility = Visibility.Collapsed;
            errorMatchingLabel.Visibility = Visibility.Collapsed;
            errorEmptyAnswerLabel.Visibility = Visibility.Collapsed;
            errorSelectItemRemoveLabel.Visibility = Visibility.Collapsed;

            //Re-initialize all components
            InitializeComponents();
        }
    }
}
