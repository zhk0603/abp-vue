using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VueTemplate.Permission.Samples
{
    public interface ISampleAppService : IApplicationService
    {
        Task<SampleDto> GetAsync();

        Task<SampleDto> GetAuthorizedAsync();
    }
}
