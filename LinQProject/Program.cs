using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQProject
{
    class Program
    {
        static void Main(string[] args)
        {


            Category categoryy = new Category();
            Product productt = new Product();


            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1 , CategoryName="Bilgisayar"},
                new Category{CategoryId=2 , CategoryName="Telefon"}
            };

            List<Product> products = new List<Product>
            {
                new Product{ProductId=1 , CategoryId =1 ,ProductName="Apple Laptop" , UnitPrice = 10000 , UnitInStock = 5},
                new Product{ProductId=2 , CategoryId =1 ,ProductName="Hp Laptop" , UnitPrice = 10000 , UnitInStock = 3},
                new Product{ProductId=3 , CategoryId =1 ,ProductName="Asus Laptop" , UnitPrice = 4000 , UnitInStock = 2},
                new Product{ProductId=4 , CategoryId =2 ,ProductName="Apple Telefon" , UnitPrice = 8000 , UnitInStock = 1},
                new Product{ProductId=5 , CategoryId =2 ,ProductName="Samsung Telefon" , UnitPrice = 7000 , UnitInStock = 4},

            };
            //METOTLAR//
            //LinQTest(products);

            //Test(products);

            //FindTest(products);

            //FindAllTest(products);

            //AscDescTest(products);

            //ClassicLinqTest(products);

            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         where p.UnitPrice>5000
                         orderby p.UnitPrice descending
                         select new ProductDto {ProductId = p.ProductId, CategoryName=c.CategoryName, ProductName=p.ProductName, UnitPrice=p.UnitPrice };

            foreach (var productDto in result)
            {
                Console.WriteLine("{0} --- {1} ", productDto.ProductName, productDto.CategoryName);
            }
        }

        private static void ClassicLinqTest(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 6000
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }


        private static void AscDescTest(List<Product> products)
        {
            //Single Line Query
            var result = products.Where(p => p.ProductName.Contains("top")).OrderBy(p => p.UnitPrice).ThenByDescending(p => p.ProductName);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("top"));
            Console.WriteLine(result);
        }

        private static void FindTest(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 4);
            Console.WriteLine(result.ProductName);
        }

        private static void LinQTest(List<Product> products)
        {
            // Linq olmadan uzun olarak filtreleme yapma
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitInStock > 3)
                {
                    Console.WriteLine(product.ProductName);
                }

            }

            Console.WriteLine("------------------------------------------");
            // Linq ile filtreleme yapma

            var results = products.Where(p => p.UnitPrice > 5000 && p.UnitInStock > 3);

            foreach (var result in results)
            {
                Console.WriteLine(result.ProductName);
            }
        }

        private static void Test(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Apple Laptop");
            Console.WriteLine(result);
        }
    }

    class ProductDto
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
