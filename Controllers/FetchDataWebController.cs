using EBD.API.Domain;
using EBD.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EBD.API.Controllers
{
    [Route("api/ebd-lessons")]
    public class FetchDataWebController : ControllerBase
    {
        private IFetchDataWeb _fetchDataWeb;
        public FetchDataWebController(IFetchDataWeb fetchDataWeb)
        {
            _fetchDataWeb = fetchDataWeb;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Lesson>), 200)]
        public async Task<IActionResult> FetchLessonsFromEBDAsync()
        {
            var lessons = await _fetchDataWeb.FetchLessonsFromEBD();
            return Ok(lessons);
        }
    }
}
