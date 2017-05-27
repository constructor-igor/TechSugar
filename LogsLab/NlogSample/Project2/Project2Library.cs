using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Project2
{
    public class Project2Library
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Project2Library()
        {
            logger.Trace("Project2Library created");
        }

        public void Run()
        {
            logger.Info("Project2Library.run()");
        }

    }
}
