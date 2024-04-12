using Microsoft.AspNetCore.Mvc;
using BrickWells.Models;
using BrickWells.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BrickWells.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private IAdminRepository _repo;
    private readonly InferenceSession _session;
    private readonly string _onnxModelPath;

    public AdminController(IAdminRepository temp, IHostEnvironment hostEnvironment)
    {
        _repo = temp;

        //ONNX path 
        _onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "PV4.onnx");
        _session = new InferenceSession(_onnxModelPath);
    }
    
    public IActionResult Index()
    {
        return View();
    }

    //product Information and Methods
    public IActionResult ProductList(int pageNum)
    {
        int pageSize = 5;

        var brickProducts = new ProductListViewModel
        {
            Products = _repo.Products
                .OrderBy(x => x.ProductId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                currentPage = pageNum,
                itemsPerPage = pageSize,
                totalItems = _repo.Products.Count()
            }
        };
        return View(brickProducts);
    }
    
    [HttpGet]
    public IActionResult AddProduct()
    {
        return View("ProductEntryForm", new Product());
    }

    [HttpPost]
    public IActionResult AddProduct(Product response)
    {
        if(ModelState.IsValid)
        {
            _repo.AddProduct(response);
            return View("Confrmation", response);
        }
        else
        {
            return View(response);
        }
    }

    [HttpGet]
    public IActionResult PEdit(int Id)
    {
        var updatedprodInfo = _repo.Products
            .Single(x => x.ProductId == Id);
        
        return View("ProductEntryForm",updatedprodInfo);
    }
    
    [HttpPost]
    public IActionResult PEdit(Product updatedprodInfo)
    {
        _repo.EditProduct(updatedprodInfo);

        return RedirectToAction("ProductList");
    }
    
    [HttpGet]
    public IActionResult PDelete(int Id)
    {
        var prodtoDelete = _repo.Products
            .Single(x => x.ProductId == Id);
        
        return View(prodtoDelete);
    }
    
    [HttpPost]
    public IActionResult PDelete(Product Delete)
    {
        _repo.DeleteProduct(Delete);

        return RedirectToAction("ProductList");
    }
    
    public IActionResult ProductDetails(int Id)
    {
        var productDetails = _repo.Products
            .Single(x => x.ProductId == Id);
        
        return View(productDetails);
    }
    
    
    //Customer Information and Methods
    public IActionResult CustomerInfo(int pageNum)
    {
        int pageSize = 25;

        var brickCustomers = new CustomerListViewModel()
        {
            Customers = _repo.Customers
                .OrderBy(x => x.CustomerId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                currentPage = pageNum,
                itemsPerPage = pageSize,
                totalItems = 500 // _repo.Orders.Count()
            }
        };

        return View(brickCustomers);
    }
    

    [HttpGet]
    public IActionResult CEdit(int Id)
    {
        var updatedcustInfo = _repo.Customers
            .Single(x => x.CustomerId == Id);
        
        return View("CustomerEntryForm",updatedcustInfo);
    }
    
    [HttpPost]
    public IActionResult CEdit(Customer updatedcustInfo)
    {
        _repo.EditCustomer(updatedcustInfo);

        return RedirectToAction("CustomerInfo");
    }
    
    [HttpGet]
    public IActionResult CDelete(int Id)
    {
        var prodtoDelete = _repo.Customers
            .Single(x => x.CustomerId == Id);
        
        return View(prodtoDelete);
    }
    
    [HttpPost]
    public IActionResult CDelete(Customer Delete)
    {
        _repo.DeleteCustomer(Delete);

        return RedirectToAction("CustomerInfo");
    }

    // public IActionResult OrderReview()
    // {
    //     var records = _repo.Orders.ToList();  // Fetch all records
    //     var predictions = new List<FraudPrediction>();  // Your ViewModel for the view
    //
    //     // Dictionary mapping the numeric prediction to an animal type
    //     var class_type_dict = new Dictionary<int, string>
    //     {
    //         { 0, "Not Fraud" },
    //         { 1, "Fraud" }
    //     };
    //
    //
    //
    //     foreach (var record in records)
    //     {  
    //         var input = new List<float>
    //         {
    //             (float) record.Time,
    //             (float) record.TransactionId,
    //             (float) record.CustomerId,
    //             (float)(record.Amount ?? 0),
    //             
    //
    //             //dummycode date
    //             record.DayOfWeek == "Mon" ? 1 : 0,
    //             record.DayOfWeek == "Sat" ? 1 : 0,
    //             record.DayOfWeek == "Sun" ? 1 : 0,
    //             record.DayOfWeek == "Thu" ? 1 : 0,
    //             record.DayOfWeek == "Tue" ? 1 : 0,
    //             record.DayOfWeek == "Wed" ? 1 : 0,
    //
    //             record.EntryMode == "PIN" ? 1 : 0,
    //             record.EntryMode == "Tap" ? 1 : 0,
    //
    //
    //             record.TypeOfTransaction == "Online" ? 1 : 0,
    //             record.TypeOfTransaction == "POS" ? 1 : 0,
    //
    //             record.CountryOfTransaction == "India" ? 1 : 0,
    //             record.CountryOfTransaction == "Russia" ? 1 : 0,
    //             record.CountryOfTransaction == "USA" ? 1 : 0,
    //             record.CountryOfTransaction == "UnitedKingdom" ? 1 : 0,
    //
    //             //Use CountryOfTransaction is Shipping Address is null
    //             (record.ShippingAddress ?? record.CountryOfTransaction) == "India" ? 1 : 0,
    //             (record.ShippingAddress ?? record.CountryOfTransaction) == "Russia" ? 1 : 0,
    //             (record.ShippingAddress ?? record.CountryOfTransaction) == "USA" ? 1 : 0,
    //             (record.ShippingAddress ?? record.CountryOfTransaction) == "UnitedKingdom" ? 1 : 0,
    //
    //             record.Bank == "HSBC" ? 1: 0,
    //             record.Bank == "Halifax" ? 1: 0,
    //             record.Bank == "Lloyds" ? 1: 0,
    //             record.Bank == "Metro" ? 1: 0,
    //             record.Bank == "Monzo" ? 1: 0,
    //             record.Bank == "RBS" ? 1: 0,
    //
    //             record.TypeOfCard == "Visa" ? 1 : 0,
    //
    //         };
    //         //converts them to an actuall type that passes it to the onnx
    //         var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });
    //
    //         var inputs = new List<NamedOnnxValue>
    //         {
    //             NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
    //         };
    //         //this will run it
    //         string predictionResult;
    //         using (var results = _session.Run(inputs))
    //         {
    //             var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
    //             predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
    //         }
    //         //this grabs the prediction and appends it to the originakl table
    //         predictions.Add(new FraudPrediction { Order = record, Prediction = predictionResult }); // Adds the animal information and prediction for that animal to AnimalPrediction viewmodel
    //     }
    //
    //     return View(predictions);
    // }

    //find a way to show new records fraud or not immediatly grab it from the record
    
    //Order Information and Methods
    public IActionResult OrderReview(int pageNum)
    {
        int pageSize = 25;

        var brickOrders = new OrderListViewModel()
        {
            Orders = _repo.Orders
                .OrderBy(x => x.TransactionId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                currentPage = pageNum,
                itemsPerPage = pageSize,
                totalItems = 500 // _repo.Orders.Count()
            }
        };
        
        return View(brickOrders);
    }
    
    public IActionResult OrderDetails(int Id)
    {
        var orderDetails = _repo.Orders
            .Single(x => x.TransactionId == Id);
        
        return View(orderDetails);
    }
    

}