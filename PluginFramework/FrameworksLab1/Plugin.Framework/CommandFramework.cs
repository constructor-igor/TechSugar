using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Framework.Interfaces;

namespace Plugin.Framework
{
    public class CommandFramework
    {
        readonly List<DataFolder> pluginBinariesFolder = new List<DataFolder>();
        private CommandCompositionHelper commandCompositionHelper;
        private DataFramework dataFramework;

        public CommandFramework(DataFramework dataFramework)
        {
            this.dataFramework = dataFramework;
        }

        public void AddPluginsFolder(DataFolder pluginsFolder)
        {
            pluginBinariesFolder.Add(pluginsFolder);
        }

        public void Init()
        {
            commandCompositionHelper = new CommandCompositionHelper(pluginBinariesFolder);
        }

        public IDataEntity RunCommand(string commandUnique, string commandParameters)
        {
            Lazy<ICommand, IDictionary<string, object>> foundPlugin = commandCompositionHelper.Commands.First(command => command.Value.Descriptor.Unique.ToLower() == commandUnique.ToLower());
            ICommand foundCommand = foundPlugin.Value;

            ICommandContext commandContext = new CommandContext(dataFramework);
            return foundCommand.Run(commandContext);
        }
    }
}
