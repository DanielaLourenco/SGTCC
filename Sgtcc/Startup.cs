﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sgtcc.Startup))]
namespace Sgtcc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
