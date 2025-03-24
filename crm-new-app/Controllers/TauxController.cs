using Microsoft.AspNetCore.Mvc;

namespace crm_new_app.Controllers;

public class TauxController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly HttpClient _httpClient;

    public TauxController(ILogger<DashboardController> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080")
        };
    }

    public IActionResult Seuil()
    {
        return View();
    }

    [HttpPost("/Taux/UpdateTaux")]
    public async Task<IActionResult> UpdateTaux(double newTaux)
    {
        try
        {
            string apiUpdateTaux = $"api/taux/update?newTaux={newTaux}";
            
            var response = await _httpClient.PostAsync(apiUpdateTaux, null);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Taux mis à jour avec succès.";
            }
            else
            {
                TempData["ErrorMessage"] = "Erreur lors de la mise à jour du montant.";
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["ErrorMessage"] = "Erreur lors de la mise à jour du montant.";

        }
        return RedirectToAction("Seuil");
    }
}