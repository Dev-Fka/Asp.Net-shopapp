using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.entity;
using shopapp.entity.obj;

namespace shopapp.dataAccess.Concrete.EFCore
{
    public class SeedDatabase
    {
        public static void Seed()
        {
            var dbcontext = new ShopContext();
            if (dbcontext.Database.GetPendingMigrations().Count() == 0)
            {
                if (dbcontext.Categories.Count() == 0)
                {
                    dbcontext.Categories.AddRange(categories);
                }
                if (dbcontext.Products.Count() == 0)
                {
                    dbcontext.Products.AddRange(products);
                    dbcontext.AddRange(productCategories);
                }
            }

            dbcontext.SaveChanges();
        }

        public static Category[] categories ={
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"},
            new Category(){Name="Tablet"}

        };

        public static Product[] products ={
            new Product(){Name="İPHONE 8",Url="iphone-8",Price=1500,Description="İYİ CİHAZ.KAMERASI GÜZEL",ImageUrl="iphone.jpg"},
            new Product(){Name="SAMSUNG S10",Url="samsung-s10",Price=1000,Description="ORTALAMA CİHAZ.EKRANI GÜZEL",ImageUrl="iphone.jpg"},
            new Product(){Name="OPPO RENO",Url="oppo-reno",Price=2000,Description="ORTALAMA CİHAZ.KALİTESİ GÜZEL",ImageUrl="iphone.jpg"},
            new Product(){Name="ASUS X10",Url="asus-x10",Price=6000,Description="İYİ İŞLEMCİ,HIZLI EKRAN.",ImageUrl="asus-laptop.jpg"},
            new Product(){Name="Mac Pro",Url="mac-pro",Price=5000,Description="ORTALAMA CİHAZ.KALİTESİ GÜZEL",ImageUrl="mac-tablet.jpg"}
        };

        public static ProductCategory[] productCategories ={
            new ProductCategory(){product=products[0],category =categories[0]},
            new ProductCategory(){product=products[1],category =categories[0]},
            new ProductCategory(){product=products[2],category =categories[0]},
            new ProductCategory(){product=products[3],category =categories[1]},
            new ProductCategory(){product=products[4],category =categories[2]}
        };
    }

}