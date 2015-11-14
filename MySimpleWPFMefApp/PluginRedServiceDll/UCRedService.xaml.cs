using System.ComponentModel;
using System.Windows.Controls;

namespace PluginRedServiceDll
{
    /// <summary>
    /// Interaktionslogik für UCRedService.xaml
    /// </summary>
    public partial class UCRedService : UserControl, INotifyPropertyChanged, PluginContractsDll.PluginService.IPluginService
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

        public UCRedService()
        {
            InitializeComponent();
            DataContext = this;
            PluginHeadLine = "The Red Plugin";
        }

        public string InitPlugin()
        {
            return "I am the Red Plugin";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }
}
