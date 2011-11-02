using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using NHibernate;
using NHibernate.Context;

namespace TheaterBooking.Infrastructure
{
    public class NHSessionManagementModule : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += delegate
            {
                ISession session = NHSessionFactory.GetFactory().OpenSession();
                CurrentSessionContext.Bind(session);
            };

            context.EndRequest += delegate
            {
                CurrentSessionContext.Unbind(NHSessionFactory.GetFactory());
            };
        }

        #endregion
    }
}
