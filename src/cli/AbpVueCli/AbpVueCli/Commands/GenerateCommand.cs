using System;
using System.Collections.Generic;
using System.Text;

namespace AbpVueCli.Commands
{
    public class GenerateCommand : CommandBase
    {
        public GenerateCommand(IServiceProvider serviceProvider) : base(
            serviceProvider, "generate", "generate")
        {
            AddCommand<CrudCommand>();
            AddCommand<ApiCommand>();
            AddCommand<ListCommand>();
            AddCommand<CreateCommand>();
            AddCommand<EditCommand>();
        }
    }

    public class ApiCommand : CommandBase
    {
        public ApiCommand(IServiceProvider serviceProvider) : base(serviceProvider, "api", "api")
        {
        }
    }

    public class ListCommand : CommandBase
    {
        public ListCommand(IServiceProvider serviceProvider) : base(serviceProvider, "list", "list")
        {
        }
    }

    public class CreateCommand : CommandBase
    {
        public CreateCommand(IServiceProvider serviceProvider) : base(serviceProvider, "create", "create")
        {
        }
    }

    public class EditCommand : CommandBase
    {
        public EditCommand(IServiceProvider serviceProvider) : base(serviceProvider, "edit", "edit")
        {
        }
    }

    public class InfoCommand : CommandBase
    {
        public InfoCommand(IServiceProvider serviceProvider) : base(serviceProvider, "info", "info")
        {
        }
    }
}
