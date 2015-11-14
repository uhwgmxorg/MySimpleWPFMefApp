using System.ComponentModel.Composition;

namespace PluginContractsDll
{
    public class PluginService
    {
        [InheritedExport(typeof(IPluginService))]
        public interface IPluginService
        {
            string InitPlugin();
        }
    }
}
