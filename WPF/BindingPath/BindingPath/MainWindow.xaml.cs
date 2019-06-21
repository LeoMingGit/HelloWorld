using System;
using System.Collections.Generic;
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

namespace BindingPath
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //后台代码绑定路径为属性或者索引（索引也可以理解为特殊的属性），当绑定源本身就为string，int类型数据时
            //XAML中可以省略路径，后台代码中用"."代替,注意与下面“/”的区别
            //string str = "123";
            //this.txtName.SetBinding(TextBox.TextProperty, new Binding(".") { Source = str });
            //this.txtLength.SetBinding(TextBox.TextProperty, new Binding("Length") { Source = str, Mode = BindingMode.OneWay });
            //this.txtSecondChar.SetBinding(TextBox.TextProperty, new Binding("[1]") { Source = str, Mode = BindingMode.OneWay });

            //让一个集合或者是DataView的第一个元素作为binding的路径。如果路径是集合的话可以继续"/"下去===============================
            List<string> strList = new List<string>() { "Tim", "Tom", "Blog" };
            this.txtName.SetBinding(TextBox.TextProperty, new Binding("/") { Source = strList });
            this.txtLength.SetBinding(TextBox.TextProperty, new Binding("/Length") { Source = strList, Mode = BindingMode.OneWay });
            this.txtSecondChar.SetBinding(TextBox.TextProperty, new Binding("/[1]") { Source = strList, Mode = BindingMode.OneWay });

            //实现多级默认集合作为binding的源=========================================================================================
            //定义一个国家列表
            List<Country> ListCountry = new List<Country>();
            //定义一个省份列表
            List<Province> ListProvince = new List<Province>();
            //定义一个城市列表
            List<City> ListCity1 = new List<City>();
            List<City> ListCity2 = new List<City>();
            //为城市列表中添加城市

            ListCity1.Add(new City() { Name = "郑州" });
            ListCity1.Add(new City() { Name = "许昌" });

            ListCity2.Add(new City() { Name = "福州" });
            ListCity2.Add(new City() { Name = "厦门" });

            //为省份列表添加省
            ListProvince.Add(new Province() { Name = "河南", CityList = ListCity1 });
            ListProvince.Add(new Province() { Name = "福建", CityList = ListCity2 });

            Country country = new Country() { Name = "中国", ProvinceList = ListProvince };
            ListCountry.Add(country);
            //当默认集合为对象的话，注意指明其属性，注意与上面的区分
            this.txtCountry.SetBinding(TextBox.TextProperty, new Binding("/Name") { Source = ListCountry });
            this.txtProvince.SetBinding(TextBox.TextProperty, new Binding("/ProvinceList/Name") { Source = ListCountry });
            this.txtCity.SetBinding(TextBox.TextProperty, new Binding("/ProvinceList/CityList/Name") { Source = ListCountry });
        }
        }

    #region 创建国家、省份、城市类
    class City
    {
        public string Name { get; set; }
    }
    class Province
    {
        public string Name { get; set; }
        public List<City> CityList { get; set; }
    }
    class Country
    {
        public string Name { get; set; }
        public List<Province> ProvinceList { get; set; }
    }
    #endregion

}
