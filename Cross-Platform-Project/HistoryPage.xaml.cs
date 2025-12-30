using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Platform_Project
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(List<Movies> history)
        {
            InitializeComponent();
            HistoryList.ItemsSource = history;
        }
    }
}
