using DeweyTrain.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using System.Xml.Linq;

namespace DeweyTrain.Views
{
    /// <summary>
    /// Interaction logic for FindingCallNumbersPage.xaml
    /// </summary>
    public partial class FindingCallNumbersPage : Page
    {
        //Object of the DeweySystem Class.
        static DeweySystem ds = new DeweySystem();

        //Tree data structure which contains all the data from the local CSV file.
        TreeNode<DeweySystem> root = new TreeNode<DeweySystem>(ds);

        //List that will contain the 4 random DeweySystem objects required for the question
        List<DeweySystem> FourTopLvlOptions = new List<DeweySystem>();

        DeweySystem correctObj = new DeweySystem();

        List<string> correctThirdLevelCallNumbers = new List<string>();



        public FindingCallNumbersPage()
        {
            InitializeComponent();

            //Adding the callnumbers from the file to the Tree data structure.
            foreach (var callNum in ReadCSV("DeweyDecimalDataFile"))
            {
                //If the callnumber objects dont contain null or empty values add them to the tree.
                if (!callNum.TopLevelCallNumber.Equals("") && !callNum.TopLevelCallNumber.Equals(null))
                {
                    root.AddChild(callNum);
                } 
            }

            //List to contain all the child objects of the Tree data structure (root).
            List<DeweySystem> dsObjList = new List<DeweySystem>();

            //Adding all the DeweySystem Objects to the list created.
            foreach (var item in root.Children)
            {
                dsObjList.Add(item.Value);
            } 

            

            //Retrieving random call numbers and their descriptors from the Tree of call numbers.
            //The following code refrences StackOverFlow:
            //Author: Tim
            //https://stackoverflow.com/questions/23543128/take-a-random-item-from-one-list-and-add-it-to-another-list
            Random r = new Random();
            int max = 4;
            int now;
            for (int i = 0; i < max; i++)
            {
                max++;
                now = r.Next(0, dsObjList.Count);
                //Making sure there are no duplicates of objects added to the FourTopLvlOptions List.
                if (!FourTopLvlOptions.Contains(dsObjList.ElementAt(now)))
                {
                    FourTopLvlOptions.Add(dsObjList.ElementAt(now));
                }
                //Exit the loop if the list contains the 4 objects required.
                if (FourTopLvlOptions.Count == 4)
                {
                    break;
                }
            }

            //Selecting a random object from the list of 4 that will serve as the correct answer
            //and will be stored in the correctObj.
            
            for (int i = 0; i < 1; i++)
            {
                now = r.Next(0, FourTopLvlOptions.Count);
                correctObj = FourTopLvlOptions.ElementAt(now);
            }

            //Removing all call numbers not associated with the top level call number.
            foreach (var item in correctObj.ThirdLevelCallNumbers)
            {
                if (!item.Equals(""))
                {
                    int topCallNumStart = int.Parse(correctObj.TopLevelCallNumber.Substring(0, 3));
                    int number = int.Parse(item.Substring(0, 3));
                    int topCallNumEnd = topCallNumStart + 100;
                    if (number < topCallNumEnd && number > topCallNumStart)
                    {
                        correctThirdLevelCallNumbers.Add(item);
                    }
                }
            }

            correctObj.ThirdLevelCallNumbers = correctThirdLevelCallNumbers;

            TestListBox.ItemsSource = correctThirdLevelCallNumbers;

            //Selecting 1 third-level-entry from the correctObj's list of thirdLevelCallNumbers.
            string thirdLvlEntry = null;
            int a = 2;
            for (int i = 0; i < a; i++)
            {
                now = r.Next(0, correctObj.ThirdLevelCallNumbers.Count);
                if (!correctObj.ThirdLevelCallNumbers.ElementAt(now).Equals(""))
                {
                    thirdLvlEntry = correctObj.ThirdLevelCallNumbers.ElementAt(now);
                    a++;
                }
            }

            //Adding the third-level-entry to the relevant listbox.
            ThirdLevelEntryListBox.Items.Add(thirdLvlEntry);
            
            //Adding the 4 top level options to the relevant listbox.
            foreach (var item in FourTopLvlOptions)
            {
                TopLevelOptionsListBox.Items.Add(item.TopLevelCallNumber);
            }
  

        }

        public IEnumerable<DeweySystem> ReadCSV(string fileName)
        {
            // We change file extension here to make sure it's a .csv file.
            // TODO: Error checking.
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));

            //List that contains the third level call numbers from the file.
            List<string> thirdLevelCallNumbers = new List<string>();

            // lines.Select allows me to project each line as a Call number level. 
            // This will give me an IEnumerable<DeweySystem> back.
            return lines.Select(line =>
            {
                string[] data = line.Split(';');
                
                //Add each third level call number to the list. 
                for (int i = 1; i < data.Length; i++)
                {
                    thirdLevelCallNumbers.Add(data[i]);
                }
                // We return the Dewey system level with it's data in order.
                return new DeweySystem(data[0], thirdLevelCallNumbers);
            });
        }

        private void checkAnswerBtn_Click(object sender, RoutedEventArgs e)
        {



        }
    }
}
