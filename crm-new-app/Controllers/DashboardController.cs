using crm_new_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace crm_new_app.Controllers;

public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly HttpClient _httpClient;
    
    public DashboardController(ILogger<DashboardController> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080")
        };
    }

    public async Task<IActionResult> Budget()
    {
        try
        {
            // Obtenir la date actuelle formatée
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            // Construire l'URL avec la date du jour
            string apiUrl = $"api/budget/all?dateTime={currentDateTime}";

            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var budgets = new BudgetModel().GetAllBudgetModels(data); 
                ViewData["budgets"] = budgets;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return View();
    }
}