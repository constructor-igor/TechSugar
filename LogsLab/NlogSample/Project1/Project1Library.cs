using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Project1
{
    public class Project1Library
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Project1Library()
        {
            logger.Trace("Project1Library created");
        }

        public void Run()
        {
            logger.Info("Project1Library.run()");
        }

    }
}
