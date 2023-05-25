using ThesisApi.Core.Context;
using ThesisApi.Core.Interfaces;
using ThesisApi.Models;

namespace ThesisApi.Services
{
    public class PointService : IPointsService
    {
        private EvServiceContext _context;
        public PointService(EvServiceContext evServiceContext) => _context = evServiceContext;
        public async Task AddNewPoint(PointModel point,int userId)
        {
            point.UserId = userId;
            var currentUser = _context.Users.Where(pr=>pr.Id == userId).FirstOrDefault();
            point.UserName = currentUser;
            try
            {
                await _context.Points.AddAsync(point);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) { }
        }

        public IEnumerable<PointModel> GetPoints()
        {
            return _context.Points.AsEnumerable();

        }

        public async Task RemovePoint(int pointId)
        {
            var findCurrent = _context.Points.Where(pr=>pr.Id == pointId).FirstOrDefault(); 
            if(findCurrent != null) { 
                _context.Remove(findCurrent);
                await _context.SaveChangesAsync();
            
            }
        }
    }
}
