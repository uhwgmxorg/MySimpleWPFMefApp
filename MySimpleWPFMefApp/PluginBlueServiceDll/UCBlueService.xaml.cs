using System.ComponentModel;
using System.Windows.Controls;

namespace PluginBlueServiceDll
{
    /// <summary>
    /// Interaktionslogik für UCBlueService.xaml
    /// </summary>
    public partial class UCBlueService : UserControl, INotifyPropertyChanged, PluginContractsDll.PluginService.IPluginService
    {
        private string pluginHeadLine;
        public string PluginHeadLine
        {
            get { return pluginHeadLine; }
            set
            {
                if (value != PluginHeadLine)
                {
                    pluginHeadLine = value;
                    OnPropertyChanged("PluginHeadLine");
                };
            }
        }

        public UCBlueService()
        {
            InitializeComponent();
            DataContext = this;
            PluginHeadLine = "The Blue Plugin";
        }

        public string InitPlugin()
        {
            return "I am the Blue Plugin";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}
