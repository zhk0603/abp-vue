using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Abp.VueTemplate.Permission.Samples
{
    [RemoteService]
    [Route("api/Permission/sample")]
    public class SampleController : PermissionController, ISampleAppService
    {
        private readonly ISampleAppService _sampleAppService;

        public SampleController(ISampleAppService sampleAppService)
        {
            _sampleAppService = sampleAppService;
        }

        /// <summary>
        /// ceshi...
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<SampleDto> GetAsync()
        {
            return await _sampleAppService.GetAsync();
        }

        /// <summary>
        /// 测试。。。。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("authorized")]
        [Authorize]
        public async Task<SampleDto> GetAuthorizedAsync()
        {
            return await _sampleAppService.GetAsync();
        }
    }
}
