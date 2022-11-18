using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    private IDal Dal = new DalList();  // Permet d'acceder au mofa de l'interface Order de la DAL
}
