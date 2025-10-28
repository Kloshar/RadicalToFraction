using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace RadicalToFraction
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listview.Height = main.Height - canvas.Height - calculate_btn.Height - 100;
        }
        class myItem
        {
            public double val {  get; set; }
            public string key { get; set; }
        }

        private void calculate_btn_Click(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(valueNumber.Text);
            int p = Convert.ToInt32(radicalPower.Text);
            int max = Convert.ToInt32(maxValue.Text);
            double result = Math.Pow(x, 1.0 / p);
            Dictionary<string, double> lst = new Dictionary<string, double>(); //словарь: дробь, значение
            for (double i = 1; i <= max; i++)
            {
                for (double j = 1; j <= max; j++)
                {
                    string key = $"{i}/{j}";
                    lst[key] = i / j;
                }
            }
            var sortedDict = from s in lst orderby s.Value select s; //сортируем список
            var nearest = sortedDict.MinBy(x => Math.Abs(x.Value - result)); //ищем ближайшее
            numerator.Content = nearest.Key.Split('/')[0];
            denominator.Content = nearest.Key.Split('/')[1];

            foreach (KeyValuePair<string, double> ent in sortedDict)
            {
                listview.Items.Add(new myItem { val = ent.Value, key = ent.Key});
            }
            Debug.WriteLine($"Результат: {nearest}");
        }
        private void main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine("window size changed!");
            listview.Height = main.ActualHeight - canvas.ActualHeight - calculate_btn.ActualHeight - 100;
        }
    }
}