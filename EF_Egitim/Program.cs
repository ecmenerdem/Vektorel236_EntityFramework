using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EF_Egitim
{
    internal class Program
    {

        static void Main(string[] args)
        {
            NORTHWINDEntities ne = new NORTHWINDEntities();
            int secilenEkran = 0;
            string secilenMusteriID = "";
            List<BasketDTO> basket = new List<BasketDTO>();
            Anasayfa();







            #region Ekranlar

            void Anasayfa()
            {
                secilenEkran = 0;

                while (secilenEkran > 4 || secilenEkran < 1)
                {
                    Console.Clear();
                    Console.WriteLine("*****Hepsi Şurada Sistemine Hoşgeldiniz*****");
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Lütfen Yapmak İstediğiniz İşlemi Seçiniz");
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("1-Çalışanlar");
                    Console.WriteLine("2-Müşteriler");
                    Console.WriteLine("3-Siparişler");
                    Console.WriteLine("4-Ürünler");
                    secilenEkran = Convert.ToInt32(Console.ReadLine());

                    if (secilenEkran > 4 || secilenEkran < 1)
                    {
                        Console.WriteLine("Yanlış İşlem Yaptınız.Sayfa Tekrar Yüklenecek");

                        Thread.Sleep(3000);
                    }
                }


                if (secilenEkran == 1)
                {
                    CalisanlarEkrani();
                }
                else if (secilenEkran == 2)
                {
                    MusterilerEkrani();
                }
                else if (secilenEkran == 3)
                {
                    SiparisEkrani();
                }

                else if (secilenEkran == 4)
                {
                    UrunlerEkrani();
                }

            }






            void CalisanlarEkrani()
            {
                Console.Clear();

                Console.WriteLine("*****Çalışanlar*****");
                Console.WriteLine(Environment.NewLine);
                var employeeList = ne.Employees.ToList();

                for (int i = 0; i < employeeList.Count; i++)
                {
                    Console.WriteLine($"{employeeList[i].EmployeeID} - {employeeList[i].FirstName} {employeeList[i].LastName}");
                }
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("Anasayfaya dönmek için '0'(sıfır) a basınız veya detay bilgisini öğrenmek istediğiniz çalışanın numarasını giriniz.");

                secilenEkran = Convert.ToInt32(Console.ReadLine());

                while (!employeeList.Exists(q => q.EmployeeID == secilenEkran))
                {
                    Console.Clear();

                    Console.WriteLine("*****Çalışanlar*****");
                    Console.WriteLine(Environment.NewLine);

                    for (int i = 0; i < employeeList.Count; i++)
                    {
                        Console.WriteLine($"{employeeList[i].EmployeeID} - {employeeList[i].FirstName} {employeeList[i].LastName}");
                    }
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Anasayfaya dönmek için '0'(sıfır) a basınız veya detay bilgisini öğrenmek istediğiniz çalışanın numarasını giriniz.");

                    secilenEkran = Convert.ToInt32(Console.ReadLine());
                    if (secilenEkran == 0)
                    {
                        break;
                    }
                }


                if (secilenEkran == 0)
                {
                    Anasayfa();
                }
                else
                {
                    CalisanDetayEkrani(secilenEkran);
                }

            }

            void CalisanDetayEkrani(int id)
            {
                Console.Clear();

                Console.WriteLine("*****Çalışan Detay*****");
                Console.WriteLine(Environment.NewLine);

                var employee = ne.Employees.Where(q => q.EmployeeID == id)
                    .Include("Employees1")
                    .Include("Employees2")


                    .SingleOrDefault();

                Console.WriteLine($"ID: {employee.EmployeeID}");
                Console.WriteLine($"Ad: {employee.FirstName}");
                Console.WriteLine($"Soyad: {employee.LastName}");
                Console.WriteLine($"Title: {employee.Title}");
                Console.WriteLine($"Doğum Tarihi: {employee.BirthDate.Value.ToShortDateString()}");
                Console.WriteLine($"İşe Başlama Tarihi: {employee.HireDate.Value.ToShortDateString()}");

                //var parentEmployee = ne.Employees.Where(q=>q.EmployeeID== employee.ReportsTo).SingleOrDefault();

                if (employee.Employees2 != null)
                {
                    Console.WriteLine($"Yöneticisi: {employee.Employees2.FirstName} {employee.Employees2.LastName}");
                }
                else
                {
                    Console.WriteLine($"Yöneticisi: Yöneticisi Yok");
                }


                Console.WriteLine($"Sorumlu Personelleri: ");

                employee.Employees1.ToList().ForEach(q => Console.WriteLine($"  -{q.FirstName} {q.LastName}"));


                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("1-Geri Dön");

                secilenEkran = Convert.ToInt32(Console.ReadLine());

                if (secilenEkran == 1)
                {
                    CalisanlarEkrani();
                }

            }

            void MusterilerEkrani()
            {
                Console.Clear();
                Console.WriteLine("*****Müşteriler*****");
                Console.WriteLine(Environment.NewLine);

                var customers = ne.Customers
                    .ToList();

                customers.ForEach(q => Console.WriteLine($"{q.CustomerID} -  {q.CompanyName} - {q.ContactName}"));

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1-Geri Dön");
                Console.WriteLine("2-Müşteri Detayını Görüntüle");
                secilenEkran = Convert.ToInt32(Console.ReadLine());
                if (secilenEkran == 1)
                {
                    Anasayfa();
                }
                else if (secilenEkran == 2)
                {
                    Console.Write("Müşteri ID sini Giriniz: ");
                    secilenMusteriID = Console.ReadLine();
                    MusteriDetayEkrani(secilenMusteriID);
                }
            }

            void MusteriDetayEkrani(string musteriID)
            {
                Console.Clear();
                var customer = ne.Customers.SingleOrDefault(q => q.CustomerID.ToLower() == musteriID.ToLower());

                Console.WriteLine($"Firma ID:  {customer.CustomerID}");
                Console.WriteLine($"Firma Adı: {customer.CompanyName}");
                Console.WriteLine($"İletişim Kişisi: {customer.ContactName}");
                Console.WriteLine($"İletişim Kişisi Ünvanı: {customer.ContactTitle}");
                Console.WriteLine($"Firma Adresi: {customer.Address} / {customer.City} / {customer.Country}");
                Console.WriteLine($"İrtibat Numarası: {customer.Phone}");

                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("1-Geri Dön");
                Console.WriteLine("2-Müşteriye Ait Siparişleri Görüntüle");
                Console.WriteLine("3-Müşteri Siparişi Oluştur");

                secilenEkran = Convert.ToInt32(Console.ReadLine());

                if (secilenEkran == 1)
                {
                    MusterilerEkrani();
                }
                else if (secilenEkran == 2)
                {
                    MusteriSiparisListesiEkrani(customer.CustomerID);
                }
                else {
                    MusteriSiparisOlusturmaUrunListesiEkrani();
                }

            }

            void MusteriSiparisListesiEkrani(string musterID)
            {
                var orders = ne.Orders.Where(q => q.CustomerID.ToLower() == musterID.ToLower()).ToList();

                foreach (var order in orders)
                {

                    Console.WriteLine($"Sipariş ID: {order.OrderID} - Sipariş Tarihi: {order.OrderDate.Value.ToShortDateString()} - Ülke: {order.ShipCountry}");
                }

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1-Geri Dön");
                Console.WriteLine("2-Sipariş Detay");

                secilenEkran = Convert.ToInt32(Console.ReadLine());
                if (secilenEkran == 1)
                {
                    MusterilerEkrani();
                }
                else if (secilenEkran == 2)
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.Write("Sipariş Numarasını Giriniz: ");
                    int siparisNo = Convert.ToInt32(Console.ReadLine());
                    var order = GetOrderDetailsByOrderID(siparisNo);

                    OrderDetailsEkrani(order, Ekranlar.MusteriDetayEkrani);
                }
            }

            void OrderDetailsEkrani(OrderDTO orderDTO, string SeciliEkran = "")

            {

                Console.Clear();
                Console.WriteLine($"Sipariş No: {orderDTO.OrderID}");
                Console.WriteLine($"Müşteri: {orderDTO.CustomerName}");
                Console.WriteLine($"İlgili Personel: {orderDTO.EmployeeName}");

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("***** ÜRÜNLER *****");

                foreach (var orderDetail in orderDTO.Details)
                {

                    Console.WriteLine($"Ürün Adı: {orderDetail.ProductName} - Birim Fiyatı: {orderDetail.UnitPrice} - Adedi: {orderDetail.Quantity} - Birim Toplamı: {orderDetail.UnitTotal}");
                }
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"Toplam Sipariş Tutarı: {orderDTO.GrandTotal}");

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1-Anasayfaya Dön");

                if (SeciliEkran == Ekranlar.MusteriDetayEkrani)
                {
                    Console.WriteLine("2-Müşteri Ekranına Dön");
                }


                secilenEkran = Convert.ToInt32(Console.ReadLine());

                if (secilenEkran == 1)
                {
                    Anasayfa();
                }
                else if (secilenEkran == 2)
                {
                    MusteriDetayEkrani(secilenMusteriID);
                }
            }

            void SiparisEkrani()
            {

                Console.Clear();

                Console.Write($"Sipariş Numarasını Giriniz: ");

                int siparisNo = Convert.ToInt32(Console.ReadLine());

                var order = GetOrderDetailsByOrderID(siparisNo);
                OrderDetailsEkrani(order);

            }


            void UrunlerEkrani()
            {
                Console.Clear();

                Console.WriteLine("***** ÜRÜNLER *****");
                Console.WriteLine(Environment.NewLine);

                var products = GetProducts();

                Console.WriteLine($"Toplam Ürün Sayısı: {products.Count}");

                products.ForEach(x => Console.WriteLine($"Ürün No: {x.ProductID} - Ürün Adı: {x.Name} - Kategorisi: {x.Category} - Ürün Birim Fiyatı: {x.UnitPrice} - Stok: {x.Stock}"));

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("1-Geri Dön");
                Console.WriteLine("2-Ürün Detaylarını Getir");

                secilenEkran = Convert.ToInt32(Console.ReadLine());

                if (secilenEkran == 1)
                {
                    Anasayfa();
                }
                else if (secilenEkran == 2)
                {
                    Console.Write("Ürün Numarasını Giriniz: ");

                    int secilenUrunID = Convert.ToInt32(Console.ReadLine());

                    var product = GetProductDetailFromProductList(secilenUrunID, products);

                    UrunDetayEkrani(product);

                }

            }

            void UrunDetayEkrani(ProductDTO product)
            {
                Console.Clear();

                Console.WriteLine("***** ÜRÜN BİLGİLERİ *****");
                Console.WriteLine(Environment.NewLine);

                Console.WriteLine($"Ürün No: {product.ProductID}");
                Console.WriteLine($"Ürün Adı: {product.Name}");
                Console.WriteLine($"Ürün Açıklaması: {product.Description}");
                Console.WriteLine($"Ürün Kategorisi: {product.Category}");
                Console.WriteLine($"Ürün Birim Fiyatı: {product.UnitPrice}");
                Console.WriteLine($"Ürün Stok Adedi: {product.Stock}");
                Console.WriteLine($"Tedarikçi: {product.Supplier}");
                Console.WriteLine($"Toplam Satılan Ürün Sayısı: {product.TotalSalesQuantityCount}");
                Console.WriteLine($"Bu Üründen Toplam Kazanılan Para: {product.TotalEarningFromProduct}");
            }

            void MusteriSiparisOlusturmaUrunListesiEkrani(int? ekranNo=null)
            {
                var products = GetProducts();
                Console.WriteLine($"Toplam Ürün Sayısı: {products.Count}");

                products.ForEach(x => Console.WriteLine($"Ürün No: {x.ProductID} - Ürün Adı: {x.Name} - Kategorisi: {x.Category} - Ürün Birim Fiyatı: {x.UnitPrice} - Stok: {x.Stock}"));

                if (ekranNo == null)
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("1-Geri Dön");
                    Console.WriteLine("2-Sipariş Oluştur");

                    secilenEkran = Convert.ToInt32(Console.ReadLine());
                }

                else
                {
                    secilenEkran = (int)ekranNo;
                }

                if ( secilenEkran == 1)
                {
                    MusteriDetayEkrani(secilenMusteriID);
                }
                else if( secilenEkran == 2)
                {
                    int musteriSiparisEkraniSecimi = 2;

                    while (musteriSiparisEkraniSecimi==2)
                    {
                        
                        Console.Write("Ürün No Giriniz:");

                        int basketItemID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Ürün Adedi Giriniz:");

                        int baskentItemQuantity = Convert.ToInt32(Console.ReadLine());


                        var product = products.SingleOrDefault(q => q.ProductID == basketItemID);


                        BasketDTO basketItem = new BasketDTO(ne);

                        basketItem.ProductID = basketItemID;
                        basketItem.Quantity = baskentItemQuantity;
                        basketItem.ProductName = product.Name;
                        basketItem.CustomerID = secilenMusteriID;

                        basket = AddItemToBasket(basket, basketItem);

                        Console.WriteLine("Sepeti Görüntülemek İçin 1'e Basınız");

                        Console.WriteLine("Yeni Ürün Eklemek İçin 2' ye Basınız. ");


                        musteriSiparisEkraniSecimi = Convert.ToInt32(Console.ReadLine());


                        if (musteriSiparisEkraniSecimi == 1)
                        {
                            SepetGoruntule(basket);
                        }
                    }

                   
                   

                    //Products p = new Products();
                    //p.ProductID = 10;
                    //p.ProductName = "Kalem";

                    //ne.Products.Add(p);


                    //Orders o = new Orders();

                    //o.OrderID = 102356;
                    //o.CustomerID = "savea";

                    //ne.Orders.Add(o); 


                }

            }

            void SepetGoruntule(List<BasketDTO> Basket)
            {
                Console.WriteLine("Sepetinizdeki Ürünler:");
                foreach (var item in Basket)
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("Ürün Adı: " + item.ProductName);
                    Console.WriteLine("Ürün Adedi: " + item.Quantity);
                    Console.WriteLine("Ürün Toplam Fiyatı: " + item.UnitTotal);
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("---------------------");
                    Console.WriteLine(Environment.NewLine);
                }
                Console.WriteLine("1-Sepete Ürün Ekle");
                Console.WriteLine("2-Ödeme Yap");


                int sepetGoruntuleEkranSecim =Convert.ToInt32(Console.ReadLine());

                if (sepetGoruntuleEkranSecim==1)
                {
                    MusteriSiparisOlusturmaUrunListesiEkrani(2);
                }
                else if(sepetGoruntuleEkranSecim==2)
                {
                    OdemeYap(basket);
                }

            }

            #endregion

            #region Metotlar

            OrderDTO GetOrderDetailsByOrderID(int orderID)
            {
                var orderDetais = ne.Order_Details
                    .Where(q => q.OrderID == orderID)
                    .Include(q => q.Products)
                    .Include("Orders.Customers")
                    .Include("Orders.Employees")
                    .ToList();


                OrderDTO orderDTO = new OrderDTO();

                orderDTO.OrderID = orderID;
                orderDTO.CustomerName = orderDetais[0].Orders.Customers.CompanyName + $" ({orderDetais[0].Orders.Customers.ContactName})";

                orderDTO.EmployeeName = orderDetais[0].Orders.Employees.FirstName + " " + orderDetais[0].Orders.Employees.LastName;

                orderDTO.Details = new List<OrderDetailResponseDTO>();

                foreach (var orderDetail in orderDetais)
                {

                    OrderDetailResponseDTO orderDetailResponseDTO = new OrderDetailResponseDTO();
                    orderDetailResponseDTO.ProductName = orderDetail.Products.ProductName;
                    orderDetailResponseDTO.UnitPrice = orderDetail.UnitPrice;
                    orderDetailResponseDTO.Quantity = orderDetail.Quantity;

                    orderDTO.Details.Add(orderDetailResponseDTO);

                }
                return orderDTO;
            }

            List<ProductDTO> GetProducts()
            {

                var products = ne.Products
                                .Include("Categories")
                                .Include("Suppliers")
                                .Include("Order_Details");

                List<ProductDTO> productDTOList = new List<ProductDTO>();

                foreach (var product in products)
                {
                    productDTOList.Add(new ProductDTO()
                    {
                        Category = product.Categories.CategoryName,
                        Description = product.QuantityPerUnit,
                        ProductID = product.ProductID,
                        Name = product.ProductName,
                        Stock = (int)product.UnitsInStock,
                        Supplier = product.Suppliers.CompanyName + $" ({product.Suppliers.ContactName})",
                        UnitPrice = product.UnitPrice,
                        TotalSalesQuantityCount = product.Order_Details
                        .Sum(x => x.Quantity),
                        TotalEarningFromProduct = product.Order_Details
                                    .Sum(x => x.Quantity * x.UnitPrice)
                    });
                }

                return productDTOList;
            }

            ProductDTO GetProductDetail(int productID)
            {
                var products = GetProducts();
                var product = products.Where(q => q.ProductID == productID).FirstOrDefault();
                return product;
            }

            ProductDTO GetProductDetailFromProductList(int productID, List<ProductDTO> products)
            {
                var product = products.Where(q => q.ProductID == productID).FirstOrDefault();
                return product;
            }

            List<BasketDTO> AddItemToBasket(List<BasketDTO>Basket, BasketDTO basketDTO)
            {
                Basket.Add(basketDTO);
                return Basket;
            }


            bool OdemeYap(List<BasketDTO> basket_Pay)
            {
                List<Order_Details> orderDetails = new List<Order_Details>();

                Orders order = new Orders();

                order.OrderDate = DateTime.Now;
                order.ShipCity = "Ankara";
                order.CustomerID = basket_Pay.FirstOrDefault().CustomerID;


                var orderID= order.OrderID;

                foreach (var item in basket_Pay)
                {
                    orderDetails.Add(new Order_Details()
                    {
                        //ProductID = item.ProductID,
                        Quantity=(short)item.Quantity,
                        UnitPrice=(short)item.UnitPrice,
                    });
                }

                order.Order_Details = orderDetails;
                ne.Orders.Add(order);
                ne.SaveChanges();
                return true;
               
            }

            #endregion

            Console.Read();
        }


    }

}
