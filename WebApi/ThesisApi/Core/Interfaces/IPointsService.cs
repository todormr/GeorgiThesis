using ThesisApi.Models;

namespace ThesisApi.Core.Interfaces
{
    public interface IPointsService
    {
        IEnumerable<PointModel> GetPoints();
        Task AddNewPoint(PointModel point, int userId);
        Task RemovePoint(int pointId);
    }
}
