using CoffeShop.Viewmodel.Base;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Viewmodel.Home
{
    public class HomeViewmodel : BindableBase
    {
        public SeriesCollection SeriesCollectionTop10 { get; set; }
        public SeriesCollection SeriesCollectionRevanuByWeek { get; set; }
        public string[] LabelsTop10 { get; set; }
        public string[] LabelsRevanuByWeek { get; set; }
        public Func<double, string> FormatterTop10 { get; set; }
        public Func<double, string> FormatterRevanuByWeek { get; set; }
        public List<PayMentHistory> PayMentHistory { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }

        public HomeViewmodel()
        {
            SeriesCollectionTop10 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 50, 39, 25,22,15,0,25,0,0,10 },
                    DataLabels = true,
                    Title = "Phần/Cái"
                }
            };
            FormatterTop10 = value => value.ToString("N0");
            LabelsTop10 = new string[] { "Capuchino", "Matcha", "Caramen", "Coffee", "Bánh Ngọt 1", "Bánh Flan", "Americano", " Coffe Sữa", "Capuchino Nóng", "Chocolate" };


            SeriesCollectionRevanuByWeek = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 400000, 190000, 500000,220000,550000,660000,100000 },
                    DataLabels = true,
                    Title = "Doanh Thu",
                    LineSmoothness = 0
                }
            };
            LabelsRevanuByWeek = GetListDateInWeekly(DateTime.Today).ToArray();
            FormatterRevanuByWeek = value => string.Format("{0} {1}",value.ToString("N0"), "(VND)");

            PayMentHistory = new List<PayMentHistory> 
            { 
                new PayMentHistory(), 
                new PayMentHistory(),
                new PayMentHistory(),
                new PayMentHistory(),
                new PayMentHistory(),
                new PayMentHistory()
            };
            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }
        public List<string> GetListDateInWeekly(DateTime dateTime)
        {
            DateTime startDate = dateTime.Date.AddDays(-(int)DateTime.Today.DayOfWeek);
            var listDate = new List<string>();          

            for (int i = 1; i < 8; i++)
                listDate.Add(startDate.AddDays(i).ToString("dd/MM/yyyy"));

            return listDate;
        }
    }
    public class PayMentHistory : BindableBase
    {
        private string _tabelName;
        private string _totalAmount;



        public string TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; OnPropertyChanged(); }
        }
        public string TabelName
        {
            get { return _tabelName; }
            set { _tabelName = value; OnPropertyChanged(); }
        }
    }
}
