using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlProductItem : IProductItem
{
    private IDal Dal = new DalList();
}
