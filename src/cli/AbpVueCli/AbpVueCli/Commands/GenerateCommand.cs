using System;
using System.Collections.Generic;
using System.Text;

namespace AbpVueCli.Commands
{
    public class GenerateCommand : CommandBase
    {
        public GenerateCommand(IServiceProvider serviceProvider) : base(
            serviceProvider, "generate", "生成文件")
        {
            AddCommand<CrudCommand>();
            AddCommand<ApiCommand>();
            AddCommand<ModelCommand>();
            AddCommand<ListCommand>();
            AddCommand<CreateCommand>();
            AddCommand<EditCommand>();
        }
    }
}
