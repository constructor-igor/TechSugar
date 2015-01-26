using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace AutoFixtureSamples
{
    public interface IFileData
    {
        string File { get; }
    }
    public class FileData : IFileData
    {
        public string File { get; private set; }
        public FileData(string file)
        {
            File = file;
        }
    }

    public interface IParameterDescriptor
    {
        string Id { get; }
        string Alias { get; }
    }
    public class FileParameterDescriptor: IParameterDescriptor
    {
        public int MaxSize { get; set; }
        public string Id { get; private set; }
        public string Alias { get; private set; }
        public FileTypeOptions FileType { get; private set; }

        public enum FileTypeOptions { Source  , Target  };
        public FileParameterDescriptor(string id, string alias, int maxSize, FileTypeOptions fileType)
        {
            MaxSize = maxSize;
            Id = id;
            Alias = alias;
            FileType = fileType;
        }
    }

    public class FileParameterDescriptorEx : FileParameterDescriptor
    {
        public IFileData FileData { get; private set; }

        public FileParameterDescriptorEx(string id, string alias, int maxSize, FileTypeOptions fileType, IFileData fileData)
            : base(id, alias, maxSize, fileType)
        {
            FileData = fileData;
        }
    }

    public class ParametersManager
    {
        readonly Dictionary<string, IParameterDescriptor> m_container = new Dictionary<string, IParameterDescriptor>(); 
        public void AddParameter(IParameterDescriptor parameterDescriptor)
        {
            m_container.Add(parameterDescriptor.Id, parameterDescriptor);
        }

        public IParameterDescriptor Find(string parameterId)
        {
            return m_container[parameterId];
        }

        public string GetAlias(string parameterId)
        {
            IParameterDescriptor found = m_container[parameterId];
            return found.Alias;
        }
    }

    [TestFixture]
    class TechSugarDemoTests
    {
        [Test]
        public void Manager_AddInstance_GetInstance_Simple()
        {
            ParametersManager parametersManager = new ParametersManager();

            FileParameterDescriptor fileParameterDescriptor = new FileParameterDescriptor("id", "alias", 10, FileParameterDescriptor.FileTypeOptions.Source);

            parametersManager.AddParameter(fileParameterDescriptor);

            Assert.That(parametersManager.Find("id"), Is.SameAs(fileParameterDescriptor));
        }

        [Test]
        public void Manager_AddInstance_GetInstance_Advanced()
        {
            ParametersManager parametersManager = new ParametersManager();

            FileParameterDescriptor fileParameterDescriptor = CreateFileParameterDescriptor();

            parametersManager.AddParameter(fileParameterDescriptor);

            Assert.That(parametersManager.Find("id"), Is.SameAs(fileParameterDescriptor));
        }

        [Test]
        public void Manager_AddInstance_GetInstance_AutoFixture_Version1()
        {
            ParametersManager parametersManager = new ParametersManager();

            Fixture fixture = new Fixture();
            string id = fixture.Create<string>();
            string alias = fixture.Create<string>();
            int maxSize = fixture.Create<int>();
            FileParameterDescriptor.FileTypeOptions fileTypeOptions = fixture.Create<FileParameterDescriptor.FileTypeOptions>();

            FileParameterDescriptor fileParameterDescriptor = new FileParameterDescriptor(id, alias, maxSize, fileTypeOptions);

            parametersManager.AddParameter(fileParameterDescriptor);

            Assert.That(parametersManager.Find(id), Is.SameAs(fileParameterDescriptor));
        }

        [Test]
        public void Manager_AddInstance_GetInstance_AutoFixture_Version2()
        {
            ParametersManager parametersManager = new ParametersManager();

            Fixture fixture = new Fixture();
            FileParameterDescriptor fileParameterDescriptor = fixture.Create<FileParameterDescriptor>();

            parametersManager.AddParameter(fileParameterDescriptor);

            Assert.That(parametersManager.Find(fileParameterDescriptor.Id), Is.SameAs(fileParameterDescriptor));
        }

        //DEMO add int parameter

        [Test] 
        public void Manager_AddInstance_GetInstance_AutoFixture_Alias()
        {
            // DEMO change .GetAlias
            ParametersManager parametersManager = new ParametersManager();

            Fixture fixture = new Fixture();
            FileParameterDescriptor fileParameterDescriptor = fixture.Create<FileParameterDescriptor>();

            parametersManager.AddParameter(fileParameterDescriptor);

            Assert.That(parametersManager.GetAlias(fileParameterDescriptor.Id), Is.SameAs(fileParameterDescriptor.Alias));
        }
        
        [Test]
        public void Manager_AddInstance_GetInstance_AutoFixture_AbstractType()
        {
            // DEMO add IFileData
            ParametersManager parametersManager = new ParametersManager();

            Fixture fixture = new Fixture();
            //..add Register
            FileParameterDescriptor fileParameterDescriptor = fixture.Create<FileParameterDescriptorEx>();

            parametersManager.AddParameter(fileParameterDescriptor);

            Assert.That(parametersManager.GetAlias(fileParameterDescriptor.Id), Is.SameAs(fileParameterDescriptor.Alias));
        }

        [Test]
        public void AutoFixture_CreateMany()
        {
            Fixture fixture = new Fixture();
            List<FileParameterDescriptor> list1 = new List<FileParameterDescriptor>(); 
            fixture.AddManyTo(list1, 10);
                                                                                //DEMO show list of items by OzCode
            Assert.That(list1.Count, Is.EqualTo(10));

            List<IParameterDescriptor> list2 = new List<IParameterDescriptor>();
                                                                                //DEMO fixture.Register<IParameterDescriptor>(fixture.Create<FileParameterDescriptor>);
            fixture.AddManyTo(list2, 10);
            Assert.That(list2.Count, Is.EqualTo(10));
        }

        [Test]
        public void SetPublicProperty()
        {
            Fixture fixture = new Fixture();
            FileParameterDescriptor descriptor1 = fixture.Create<FileParameterDescriptor>();

            var descriptor2 = fixture.Build<FileParameterDescriptor>()
                .With(x => x.MaxSize, 100)
                .Create();

            Assert.That(descriptor2.MaxSize, Is.EqualTo(100));
        }

        [Test]
        public void SetPrivateProperty()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Add(new DescriptorBuilder("alias", "alias"));
            //var descriptor1 = fixture.Create<FileParameterDescriptor>();
            var descriptor2 = fixture.Build<FileParameterDescriptor>()
                .With(x => x.Alias, "alias")
                .Create();
        }

        #region helper methods
        private static FileParameterDescriptor CreateFileParameterDescriptor()
        {
            FileParameterDescriptor fileParameterDescriptor = new FileParameterDescriptor("id", "alias", 10,
                FileParameterDescriptor.FileTypeOptions.Source);
            return fileParameterDescriptor;
        }
        #endregion
    }

    public class DescriptorBuilder : ISpecimenBuilder
    {
        private readonly string m_parameterName;
        private readonly string m_parameterValue;
        internal DescriptorBuilder(string parameterName, string parameterValue)
        {
            m_parameterName = parameterName;
            m_parameterValue = parameterValue;
        }
        public object Create(object request, ISpecimenContext context)
        {
            ParameterInfo pi = request as ParameterInfo;
            if (pi == null)
                return new NoSpecimen();

            if (pi.ParameterType != typeof(string) || pi.Name != m_parameterName)
                return new NoSpecimen();

            return m_parameterValue;
        }
    }
}
