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
            var  listBooks = App.db.Agent.ToList();
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
            var selAgent = (sender as Button).DataContext as Agent;
            NavigationService.Navigate(new AddEditPage(selAgent));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selAgent = (sender as Button).DataContext as Agent;
            if(MessageBox.Show("Вы точно хотите удалить эту запись", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var agentSaleToRemove = App.db.ProductSale.Where(a => a.AgentID == selAgent.ID);
                if(agentSaleToRemove == null)
                {
                    MessageBox.Show("Невозможно удалить", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    App.db.ProductSale.RemoveRange(agentSaleToRemove);
                    var shopToRemove = App.db.Shop.Where(a => a.AgentID == selAgent.ID);
                    App.db.Shop.RemoveRange(shopToRemove);
                    var priorityToRemove = App.db.AgentPriorityHistory.Where(a => a.AgentID == selAgent.ID);
                    App.db.AgentPriorityHistory.RemoveRange(priorityToRemove);

                    App.db.Agent.Remove(selAgent);
                    App.db.SaveChanges();
                    MessageBox.Show("Успешно удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    RefreshData();
                }
                
                
                
                    
            }


           
        }

        

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
            
        //    RefreshData();
        //}

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(new Agent()));
        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
