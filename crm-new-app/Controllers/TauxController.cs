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

    public async Task<IActionResult> Seuil()
    {
        //Check Session
        bool sessionStatus = await IsSessionValid(Request.Cookies["JSESSIONID"]);
        if (sessionStatus == false)
        {
            return View("ErrorSession");
        }
        return View();
    }

    [HttpPost("/Taux/UpdateTaux")]
    public async Task<IActionResult> UpdateTaux(double newTaux)
    {
        //Check Session
        bool sessionStatus = await IsSessionValid(Request.Cookies["JSESSIONID"]);
        if (sessionStatus == false)
        {
            return View("ErrorSession");
        }
        
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
    
    private async Task<bool> IsSessionValid(string jsessionId)
    {
        if (string.IsNullOrEmpty(jsessionId))
        {
            return false;
        }
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:8080/api/login/checkSession?sessionId={jsessionId}");
            requestMessage.Headers.Add("Cookie", $"JSESSIONID={jsessionId}");
            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content.Equals("true", StringComparison.OrdinalIgnoreCase);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la vérification de la session : {ex.Message}");
        }

        return false;
    }
}