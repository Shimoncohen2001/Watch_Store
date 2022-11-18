using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlProduct : BlApi.IProduct 
{
    private IDal Dal = new DalList();  // Permet d'acceder au mofa de l'interface Products de la DAL
    
}
