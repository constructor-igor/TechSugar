using System;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace UnityTests
{
    [TestFixture]
    public class UnityUseCase1
    {
        [Test]
        public void TestLegacy()
        {
            ILogService logService = new LogService();
            Executer executer1 = new Executer(logService, new FileService());
            executer1.Run(5.0);
            Executer executer2 = new Executer(logService, new FileService());
            executer2.Run(5.0);
        }

        [Test]
        public void TestUnity()
        {
            using (UnityContainer unity = new UnityContainer())
            {
                unity.RegisterInstance<ILogService>(new LogService());
                unity.RegisterType<IFileService, FileService>(new ContainerControlledLifetimeManager());

                Executer executer1 = unity.Resolve<Executer>();
                Executer executer2 = unity.Resolve<Executer>();
                Executer executer3 = new Executer(unity.Resolve<ILogService>(), unity.Resolve<IFileService>());

                Console.WriteLine("fileService: {0}", unity.Resolve<IFileService>().GetHashCode());
                Console.WriteLine("fileService: {0}", unity.Resolve<IFileService>().GetHashCode());

                Console.WriteLine(executer1.Run(5));
                Console.WriteLine(executer2.Run(10));
                Console.WriteLine(executer3.Run(20));
            }
        }
    }

    public interface ILogService
    {
        void Log(string message);
    }

    public interface IFileService
    {
        void DeleteFile(string filePath);
    }

    public class LogService : ILogService
    {
        #region ILogService
        public void Log(string message)
        {
        }
        #endregion
    }

    public class FileService : IFileService
    {
        #region IFileService
        public void DeleteFile(string filePath)
        {
        }
        #endregion
    }

    public class Executer
    {
        private readonly ILogService m_logService;
        private readonly IFileService m_fileService;

        public Executer(ILogService logService, IFileService fileService)
        {
            m_logService = logService;
            m_fileService = fileService;
        }

        public double Run(double factor)
        {
            Console.WriteLine("logService: {0}", m_logService.GetHashCode());
            Console.WriteLine("fileService: {0}", m_fileService.GetHashCode());
            m_logService.Log("start");

            m_fileService.DeleteFile("file.txt");

            return factor*5;
        }
    }
}