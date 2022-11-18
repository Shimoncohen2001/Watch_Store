using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrderTracking : IOrderTracking
{
    private IDal Dal = new DalList();
}
