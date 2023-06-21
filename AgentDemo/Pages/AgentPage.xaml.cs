using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using AgentDemo.Models;
namespace AgentDemo.Pages
{
    /// <summary>
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        AgentDemo2Entities _content = new AgentDemo2Entities();
        public AgentPage()
        {
            InitializeComponent();
            List<AgentType> listGenres = _content.AgentType.ToList();
            listGenres.Insert(0, new AgentType { Title = "Все типы" });
            TypeCb.ItemsSource = listGenres;
            TypeCb.SelectedIndex = 0;
            RefreshData();

        }

        private void RefreshData()
        {
            List<Agent> listBooks = _content.Agent.ToList();
            if (TypeCb.SelectedIndex != 0)
            {
                AgentType selectedGenre = (AgentType)TypeCb.SelectedItem;
                listBooks = listBooks.Where(x => x.AgentTypeID == selectedGenre.ID).ToList();
            }

            if (SortCb.SelectedItem != null)
            {
                switch ((SortCb.SelectedItem as ComboBoxItem).Tag)
                {

                    case "1":

                        listBooks = listBooks.OrderBy(x => x.Title).ToList();
                        listBooks = listBooks.OrderBy(x => x.Procent).ToList();
                        listBooks = listBooks.OrderBy(x => x.Priority).ToList();
                        break;
                    case "2":

                        listBooks = listBooks.OrderByDescending(x => x.Title).ToList();
                        listBooks = listBooks.OrderByDescending(x => x.Procent).ToList();
                        listBooks = listBooks.OrderByDescending(x => x.Priority).ToList();

                        break;


                }

            }
            var searchString = SearchTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listBooks = listBooks.Where(x => x.Title.ToLower().Contains(searchString.ToLower()) || x.Email.ToLower().Contains(searchString.ToLower()) || x.Phone.ToLower().Contains(searchString.ToLower())).ToList();
            }
            //listDishes = listDishes.OrderByDescending(x => x.ServingPrice).ToList();
            AgentLv.ItemsSource = listBooks;
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }

        

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           RefreshData();
        }

        private void TypeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           RefreshData();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
            
        //    RefreshData();
        //}

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
