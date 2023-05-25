using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThesisApi.Core.Interfaces;
using ThesisApi.DTO;
using ThesisApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThesisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PointsController : ControllerBase
    {
        private IPointsService _pointsService;
        public PointsController(IPointsService pointsService)
        {
            _pointsService = pointsService;
        }

        // GET: api/<PointsController>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<PointDto> Get()
        {
            List<PointDto> points = new List<PointDto>();
            var getallPoints = _pointsService.GetPoints();
            foreach (var item in getallPoints)
            {
                points.Add(new PointDto()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Name = item.Name,
                });
            }
            return points;
        }

        // GET api/<PointsController>/5
        [HttpGet("{id}")]
        public PointModel Get(int id)
        {
            var getallPoints = _pointsService.GetPoints();
            return _pointsService.GetPoints().Where(pr => pr.Id == id).FirstOrDefault();
        }

        // POST api/<PointsController>
        [HttpPost]
        public async Task Post([FromBody] PointDto point)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string fdfaf = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst("sub")?.Value;
            var usId = User.FindFirst("userId");
            try {
                await _pointsService.AddNewPoint(new PointModel { Name = point.Name, Latitude = point.Latitude, Longitude = point.Longitude }, Convert.ToInt32(usId.Value));
            }
            catch(Exception ex) { }
            
            
            }

        // PUT api/<PointsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PointsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
