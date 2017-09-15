using Castle.Core.Logging;
using Dark.Core.DI;
using Dark.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Configuration;

namespace Dark.Core.Modules
{

    #region 1.0 接口 IModuleManager
    public interface IModuleManager
    {
        ModuleInfo StartModule { get; }

        void Initialize(Type startType);

        void StartModules();

        void Shutdown();
    }
    #endregion

    #region 2.0 具体实现
    public class ModuleManager : IModuleManager
    {

        private IIocManager _iocManager;

        public ILogger Logger { get; set; }

        private Type _startType;

        public ModuleInfo StartModule { get; set; }


        private List<ModuleInfo> modules;

        public ModuleManager(IIocManager iocManger)
        {
            _iocManager = iocManger;
            modules = new List<ModuleInfo>();
            Logger = NullLogger.Instance;
        }

        public void Initialize(Type startType)
        {
            //1:加载所有模块类型
            List<Type> types = GetModuleTypes(startType);

            //2:初始化所有模块信息
            _startType = startType;
            //3:注册模块
            RegisterModules(types);
            //4:实例化模块信息
            CreateModules(types);
            //5:给所有的模块设置依赖
            SetModuleDependencies();
        }



        /// <summary>
        /// 执行modules的所有 initialize方法和postinitialize 方发
        /// </summary>
        public void StartModules()
        {
            //1:获取所有module,并且进行排序,按照依赖的先后顺序排
            var sortModules = SortModules(StartModule.Type);
            //2:执行所有的modules 方法
            sortModules.ForEach(module => module.Instance.PreInitialize());
            sortModules.ForEach(module => module.Instance.Initialize());
            sortModules.ForEach(module => module.Instance.PostInitialize());
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有的模块依赖类型
        /// </summary>
        /// <returns></returns>
        private List<Type> GetModuleTypes(Type type)
        {
            List<Type> types = new List<Type>();
            List<Type> typeList = type.GetDependonTypes();
            //1：将自己加入到集合中
            types.AddIfNotContains(type);
            typeList.ForEach(u =>
            {
                types.AddRange(GetModuleTypes(u));
            });
            return types;
        }

        private void RegisterModules(List<Type> types)
        {
            types.ForEach(type =>
            {
                _iocManager.RegiserIfNot(type);
            });
        }

        /// <summary>
        /// 通过反射获取模块实例
        /// </summary>
        /// <param name="types"></param>
        private void CreateModules(List<Type> types)
        {
            //1:循环所有模块信息
            types.ForEach(type =>
            {
                //2:解析模块
                BaseModule instance = _iocManager.Resolve(type) as BaseModule;
                if (instance != null)
                {
                    instance.IocManager = _iocManager;
                    instance.Configuration = _iocManager.Resolve<IBaseConfiguration>();
                    //3:初始化模块
                    ModuleInfo moduleInfo = new ModuleInfo(type, instance);
                    
                    //4：递归查找模块的所有依赖
                    modules.Add(moduleInfo);
                    //5：检查该类型和启动类型是否一致,如果一致,那么设置启动模块
                    if (_startType == type)
                    {
                        StartModule = moduleInfo;
                    }

                    Logger.DebugFormat("module:{0} is loaded", type.FullName);
                }
            });
        }

        /// <summary>
        /// 给所有的模块设置依赖
        /// </summary>
        /// <param name="module"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        private void SetModuleDependencies()
        {
            modules.ForEach(module =>
            {
                //1:清空依赖
                module.Dependencies.Clear();
                //2:找到模块对应的依赖
                List<Type> dependonTypes = module.Type.GetDependonTypes();
                //3:检查如果
                dependonTypes.ForEach(type =>
                {
                    var dependModule = modules.FirstOrDefault(u => u.Type == type);
                    if (dependModule != null)
                    {
                        module.Dependencies.AddIfNotContains(dependModule);
                    }
                });
            });
        }


        /// <summary>
        /// 给模块排序
        /// </summary>
        /// <returns></returns>
        private List<ModuleInfo> SortModules(Type startType)
        {
            //1:将coremodule放在第一个
            var coreIndex = modules.FindIndex(u => u.Type == typeof(CoreModule));
            if (coreIndex < 0)
            {
                Logger.Fatal("未正确加载CoreModule模块");
                throw new Exception("未找到CoreModule");
            }
            var coreModule = modules[coreIndex];
            modules.RemoveAt(coreIndex);
            modules.Insert(0, coreModule);
            //2:将startmodule 放在最后一个

            var startIndex = modules.FindIndex(u => u.Type == startType);
            if (startIndex < 0)
            {
                Logger.Fatal("未正确加载CoreModule模块");
                throw new Exception("未找到CoreModule");
            }
            var startModule = modules[startIndex];
            modules.RemoveAt(startIndex);
            modules.Add(startModule);
            return modules;
        }
    } 
    #endregion
}
