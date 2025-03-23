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

            //budget
            string apiBudget = $"api/budget/all?dateTime={currentDateTime}";

            var responseBudget = await _httpClient.GetAsync(apiBudget);
            if (responseBudget.IsSuccessStatusCode)
            {
                var data = await responseBudget.Content.ReadAsStringAsync();
                var budgets = new BudgetModel().GetAllBudgetModels(data); 
                ViewData["budgets"] = budgets;
            }
            
            
            
            //ticket
            string apiTicket = $"api/ticket/all?dateTime={currentDateTime}";

            var responseTicket = await _httpClient.GetAsync(apiTicket);
            if (responseTicket.IsSuccessStatusCode)
            {
                var data = await responseTicket.Content.ReadAsStringAsync();
                var tickets = new TicketModel().getALlTicketModels(data); 
                ViewData["tickets"] = tickets;
            }
            
            
            //Lead
            string apiLead = $"api/lead/all?dateTime={currentDateTime}";
            
            var responseLead = await _httpClient.GetAsync(apiLead);
            if (responseLead.IsSuccessStatusCode)
            {
                var data = await responseLead.Content.ReadAsStringAsync();
                var leads = new LeadModel().getAllLeadsModels(data);
                ViewData["leads"] = leads;
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