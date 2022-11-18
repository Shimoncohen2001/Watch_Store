using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrderForList : IOrderForList
{
    private IDal Dal = new DalList();
}
