using System;
using System.Collections.Generic;
using Ninject;
using TheaterBooking.Infrastructure;
using TheaterBooking.Entities;

public class NinjectDefaultModule : Ninject.Modules.NinjectModule 
{
    public override void Load()
    {
        Bind(typeof(IRepository<>)).To(typeof(NHRepository<>)).InTransientScope();
    }
}