using PluginContractsDll;
/******************************************************************************/
/*                                                                            */
/*   Program: MySimpleWPFMefAppExe                                            */
/*                                                                            */
/*   09.10.2015 1.0.0.0 uhwgmxorg Start                                       */
/*                                                                            */
/******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Windows;

namespace MySimpleWPFMefAppExe
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        PluginRepository PluginRepository { get; set; }

        const string PLUGIN_DIR = "\\Plugins";

        private PluginService.IPluginService pluginView;
        public PluginService.IPluginService PluginView
        {
            get
            {
                return pluginView;
            }
            set
            {
                pluginView = value;
                OnPropertyChanged("PluginView");
            }
        }

        private string pluginInitRetCode;
        public string PluginInitRetCode
        {
            get { return pluginInitRetCode; }
            set
            {
                if (value != PluginInitRetCode)
                {
                    pluginInitRetCode = value;
                    OnPropertyChanged("PluginInitRetCode");
                };
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadPlugins();
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button_Click_Blue_Service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Blue_Service(object sender, RoutedEventArgs e)
        {
            try
            {
                PluginView = PluginRepository.PluginServiceList.Find(v => v.ToString() == "PluginBlueServiceDll.UCBlueService");
                PluginInitRetCode = PluginView.InitPlugin();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                 PluginInitRetCode = "Plugin not found";
            }
        }

        /// <summary>
        /// Button_Click_Red_Service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Red_Service(object sender, RoutedEventArgs e)
        {
            try
            {
                PluginView = PluginRepository.PluginServiceList.Find(v => v.ToString() == "PluginRedServiceDll.UCRedService");
                PluginInitRetCode = PluginView.InitPlugin();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                 PluginInitRetCode = "Plugin not found";
            }
        }

        /// <summary>
        /// Button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events
        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events
        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// LoadPlugins
        /// </summary>
        private void LoadPlugins()
        {
            try
            {
                // We set the Plugin-Directory under the directory where the application is located
                string pluginDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + PLUGIN_DIR;
                var catalog = new DirectoryCatalog(pluginDir);
                var container = new CompositionContainer(catalog);
                PluginRepository = new PluginRepository(container.GetExportedValues<PluginContractsDll.PluginService.IPluginService>());

                foreach (var colorService in PluginRepository.PluginServiceList)
                {
                    string retCode = colorService.InitPlugin(); // <-- call the special plugin method
                    Debug.WriteLine("Load Plugin " + colorService.ToString() + " RetCode: " + retCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="p"></param>
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }

    #region Help classes
    public class PluginRepository
    {
        [ImportMany("IPluginService")]
        private IEnumerable<PluginContractsDll.PluginService.IPluginService> PluginServices { get; set; }
        // For more convenience we will copy the available plugins in a List<>
        public List<PluginContractsDll.PluginService.IPluginService> PluginServiceList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pluginServices"></param>
        public PluginRepository(IEnumerable<PluginContractsDll.PluginService.IPluginService> pluginServices)
        {
            PluginServiceList = new List<PluginService.IPluginService>();
            foreach (var service in pluginServices)
                PluginServiceList.Add(service);
        }
    }
    #endregion
}
