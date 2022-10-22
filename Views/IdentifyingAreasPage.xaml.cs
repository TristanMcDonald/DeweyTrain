using DeweyTrain.Models;
using System;
using System.Collections;
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
        private Dictionary<string, string> questionsAnswer = new Dictionary<string, string>()
        {
            {"000", "General Knowledge"},
            {"100", "Psychology and Philosophy"},
            {"200", "Religions and Mythology"},
            {"300", "Social Sciences and Folklore"},
            {"400", "Languages and Grammar"},
            {"500", "Math and Science"},
            {"600", "Medicine and Technology"},
            {"700", "Arts and Recreation"},
            {"800", "Literature"},
            {"900", "Geography and History"}
        };

        //The dictionary that will store all the users answers.
        private Dictionary<string, string> questions = new Dictionary<string, string>();

        //The ArrayLists that will display the call numbers and descriptions in the list boxes.
        public ArrayList _callNumbers = new ArrayList();
        public ArrayList _descriptions = new ArrayList();

        public IdentifyingAreasPage()
        {
            InitializeComponent();

            generateListItems();

            QuestionListBox1.ItemsSource = _callNumbers;
            QuestionListBox2.ItemsSource = _descriptions;
        }

        //Method to generate and add the items needed for the callNumbers and descriptions lists.
        public void generateListItems()
        {            
            Random r = new Random();
            int max = 4;
            int now;
            //Retrieving random call numbers and their descriptions from the dictionary of call numbers and descriptions.
            for (int i = 0; i < max; i++)
            {
                now = r.Next(0, questionsAnswer.Count);
                _callNumbers.Add(questionsAnswer.ElementAt(now).Key.ToString());
                _descriptions.Add(questionsAnswer.ElementAt(now).Value.ToString());
            }

        }

        //Check the user's answer.
        private void checkAnswerBtn_Click(object sender, RoutedEventArgs e)
        {

            bool isUserCorrect = true;

            //Checking each key value pair in the users answer
            //to ensure that the right key is paired with the right value
            //(the right call number is paired with the right description).
            foreach (var qKvp in questions)
            {
                var descriptionAnswer = from qaKvp in questionsAnswer
                                        where qaKvp.Key == qKvp.Key
                                        select qaKvp.Value;

                if (qKvp.Value != descriptionAnswer.ToString())
                {
                    isUserCorrect = false;
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
        public string? currentItemText;
        int currentItemIndex;

        private void AddButton1_Click(object sender, RoutedEventArgs e)
        {
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

        private void RemoveButton1_Click(object sender, RoutedEventArgs e)
        {
            // Find the right item and it's value and index  
            currentItemText = AnswerListBox1.SelectedValue.ToString();
            currentItemIndex = AnswerListBox1.SelectedIndex;
            // Add RightListBox item to the ArrayList  
            _callNumbers.Add(currentItemText);
            AnswerListBox1.Items.RemoveAt(AnswerListBox1.Items.IndexOf(AnswerListBox1.SelectedItem));
            // Refresh data binding  
            ApplyCallNumberDataBinding();
        }

        private void AddButton2_Click(object sender, RoutedEventArgs e)
        {
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
    

        private void RemoveButton2_Click(object sender, RoutedEventArgs e)
        {
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
}
