using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrderItem : BlApi.IOrderItem
{
    private IDal Dal = new DalList();  // Permet d'acceder au mofa de l'interface OrderItem de la DAL
}
