﻿using DO;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Category? Category { get; set; }
    public int Amount { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
     Product ID= {ID}: {Name},
     category - {Category}
     Price: {Price}
     Amount in Cart: {Amount}
     InStock: {InStock}
    ";
}
