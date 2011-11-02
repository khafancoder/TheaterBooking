using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace TheaterBooking.Infrastructure
{
    public class NHSessionFactory
    {
        private static ISessionFactory cachedVersion = null;

        public static ISessionFactory GetFactory()
        {
            if (cachedVersion == null)
            {
                Configuration config = new Configuration();
                config.Configure();
                
                cachedVersion = config.BuildSessionFactory();
            }

            return cachedVersion;
        }
    }
}
