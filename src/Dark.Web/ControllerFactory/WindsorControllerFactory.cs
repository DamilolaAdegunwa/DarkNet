using Dark.Core.DI;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dark.Web.ControllerFactory
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private IIocManager _iocManger;
        public WindsorControllerFactory(IIocManager iocManager)
        {
            _iocManger = iocManager;
        }
        /// <summary>
        /// 创建控制器对象
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null == controllerType)
            {
                return null;
            }

            return _iocManger.Resolve(controllerType) as IController;
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            _iocManger.Release(controller);
        }
    }
}
