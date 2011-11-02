using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;


public class NinjectControllerFactory : DefaultControllerFactory
{
    private readonly IKernel kernel;

    public NinjectControllerFactory(IKernel kernel)
    {
        this.kernel = kernel;
    }

    protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
    {
        if (controllerType != null)
            return (IController)kernel.Get(controllerType);
        else
            return null;
    }
}
