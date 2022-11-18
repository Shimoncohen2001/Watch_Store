using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlProductForList : IProductForList
{
    private IDal Dal = new DalList();
}
