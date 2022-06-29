using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Avaliacao3BimLp3.Repositories;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);
var productRepository = new ProductRepository(databaseConfig);

// Routing 
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Product")
{
    if(modelAction == "List")
    {
        if (!(productRepository.GetAll().Count() == 0))
        {
            foreach (var product in productRepository.GetAll())
            {
                if(product.Active)
                {
                    Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");
                }
                else
                {
                    continue;
                }
                
            }
        }
        else
        {
            Console.WriteLine($"Nenhum produto cadastrado");
        }
    }

    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var name = args[3];
        var price = Convert.ToDouble(args[4]);
        var active = Convert.ToBoolean(args[5]);

        if (!(productRepository.ExistsByID(id)))
        {
            var product = new Product(id, name, price, active);
            productRepository.Save(product);   
            Console.WriteLine($"Produto {name} cadastrado com sucesso");
        }
        else
        {
            Console.WriteLine($"Produto com Id {id} já existe.");
        }
    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsByID(id))
        {
            productRepository.Delete(id);   
            Console.WriteLine($"Produto {id} removido com sucesso");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado.");
        }
    }

    if (modelAction == "Enable")
    {
        var id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsByID(id))
        {
            productRepository.Enable(id);   
            Console.WriteLine($"Produto {id} habilitado com sucesso");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado.");
        }
    }

    if (modelAction == "Disable")
    {
        var id = Convert.ToInt32(args[2]);

        if (productRepository.ExistsByID(id))
        {
            productRepository.Disable(id);   
            Console.WriteLine($"Produto {id} desabilitado com sucesso");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado.");
        }
    }

    if(modelAction == "PriceBetween")
    {
        var initialPrice = Convert.ToDouble(args[2]);
        var endPrice = Convert.ToDouble(args[3]);

        if(productRepository.GetAllWithPriceBetween(initialPrice, endPrice).Count() == 0)
        {
            Console.WriteLine($"Nenhum produto encontrado dentro do intervalo de preço R${initialPrice} e R${endPrice}");
        }
        else
        {
            foreach (var product in productRepository.GetAllWithPriceBetween(initialPrice, endPrice))
            {
                Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
    }

    if(modelAction == "PriceHigherThan")
    {
        var price = Convert.ToDouble(args[2]);

        if(productRepository.GetAllWithPriceHigherThan(price).Count() == 0)
        {
            Console.WriteLine($"Nenhum produto encontrado com preço maior que R${price}");
        }
        else
        {
            foreach (var product in productRepository.GetAllWithPriceHigherThan(price))
            {
                Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
    }

    if(modelAction == "PriceLowerThan")
    {
        var price = Convert.ToDouble(args[2]);

        if(productRepository.GetAllWithPriceLowerThan(price).Count() == 0)
        {
            Console.WriteLine($"Nenhum produto encontrado com preço menor que R${price}");
        }
        else
        {
            foreach (var product in productRepository.GetAllWithPriceLowerThan(price))
            {
                Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}");
            }
        }
    }

    if(modelAction == "AveragePrice")
    {
        if (!(productRepository.GetAll().Count() == 0))
        {
            Console.WriteLine($"Preço médio dos produtos: R${productRepository.GetAveragePrice()}");
        }
        else
        {
            Console.WriteLine($"Nenhum produto cadastrado");
        }
    }
}