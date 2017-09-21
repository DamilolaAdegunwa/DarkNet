using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Modules;

namespace Dark.Core.Reflections
{

    public interface IAssemblyFinder
    {
        /// <summary>
        /// Gets all assemblies.
        /// </summary>
        /// <returns>List of assemblies</returns>
        List<Assembly> GetAllAssemblies();
    }


    public class AssemblyFinder : IAssemblyFinder
    {
        private readonly IModuleManager _moduleManager;

        public AssemblyFinder(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public List<Assembly> GetAllAssemblies()
        {
            //var assemblies = new List<Assembly>();

            //foreach (var module in _moduleManager.Modules)
            //{
            //    assemblies.Add(module.Assembly);
            //    //assemblies.AddRange(module.Instance.GetAdditionalAssemblies());
            //}
            return _moduleManager.Modules.Select(u => u.Assembly).Distinct().ToList();

            //return assemblies.Distinct().ToList();
        }
    }
}
