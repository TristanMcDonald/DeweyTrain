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

        //The dictionary that will store all the users answers.
        private Dictionary<int, string> questions = new Dictionary<int, string>();

        //The Lists that will display the call numbers and descriptions and allow drag and drop functions.
        private IList<CallNumber> _callNumbers = new ObservableCollection<CallNumber>();
        private IList<Description> _descriptions = new ObservableCollection<Description>();

        public IdentifyingAreasPage()
        {
            InitializeComponent();

            generateListItems();

            callNumbersListBox.ItemsSource = _callNumbers;
            descriptionsListBox.ItemsSource = _descriptions;
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
                _callNumbers.Add(new CallNumber(questionsAnswer.ElementAt(now).Key));
                _descriptions.Add(new Description(questionsAnswer.ElementAt(now).Value));
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

    }
}
