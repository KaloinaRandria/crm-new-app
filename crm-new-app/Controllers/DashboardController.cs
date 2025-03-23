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
                double totalBudget = new BudgetModel().getTotalMontant(budgets);
                ViewData["TotalBudget"] = totalBudget;
                ViewData["budgets"] = budgets;
            }
            
            
            
            //ticket
            string apiTicket = $"api/ticket/all?dateTime={currentDateTime}";

            var responseTicket = await _httpClient.GetAsync(apiTicket);
            if (responseTicket.IsSuccessStatusCode)
            {
                var data = await responseTicket.Content.ReadAsStringAsync();
                var tickets = new TicketModel().getALlTicketModels(data);
                double totalTicket = new TicketModel().getTotalTicketMontant(tickets);
                ViewData["TotalTicket"] = totalTicket;
                ViewData["tickets"] = tickets;
            }
            
            
            //Lead
            string apiLead = $"api/lead/all?dateTime={currentDateTime}";
            
            var responseLead = await _httpClient.GetAsync(apiLead);
            if (responseLead.IsSuccessStatusCode)
            {
                var data = await responseLead.Content.ReadAsStringAsync();
                var leads = new LeadModel().getAllLeadsModels(data);
                double totalLead = new LeadModel().GetMontantDepense(leads);
                ViewData["TotalLead"] = totalLead;
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
    
    [HttpPost("/Ticket/Delete")]
    public async Task<IActionResult> Delete(int idTicket)
    {
        try
        {
            // Appel à l'API Java pour supprimer le ticket
            string apiDeleteTicket = $"api/ticket/delete?idTicket={idTicket}";

            var response = await _httpClient.PostAsync(apiDeleteTicket, null);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Ticket supprimé avec succès.";
            }
            else
            {
                TempData["ErrorMessage"] = "Erreur lors de la suppression du ticket.";
            }

            // Redirection vers la vue du Dashboard ou une autre page après suppression
            return RedirectToAction("Budget");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["ErrorMessage"] = "Erreur lors de la suppression du ticket.";
            return RedirectToAction("Budget");
        }
    }

    [HttpPost("/Lead/DeleteLead")]
    public async Task<IActionResult> DeleteLead(int idLead)
    {
        try
        {  
            string apiDeleteLead = $"api/lead/delete?idLead={idLead}";
            var response = await _httpClient.PostAsync(apiDeleteLead, null);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Lead supprimé avec succès.";
            }
            else
            {
                TempData["ErrorMessage"] = "Erreur lors de la suppression du Lead.";
            }
            return RedirectToAction("Budget");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["ErrorMessage"] = "Erreur lors de la suppression du Lead.";
            return RedirectToAction("Budget");
        }
    }

    [HttpPost("/Ticket/Update")]
    public async Task<IActionResult> Update(int idTicket, double newMontant)
    {
        try
        {
            // Construire l'URL de l'API Java
            string apiUpdateTicket = $"api/ticket/update?idTicket={idTicket}&newMontant={newMontant}";

            var response = await _httpClient.PostAsync(apiUpdateTicket, null);
        
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Montant mis à jour avec succès.";
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

        // Rediriger vers la page principale après mise à jour
        return RedirectToAction("Budget");
    }
    
    
    [HttpPost("/Lead/UpdateLead")]
    public async Task<IActionResult> UpdateLead(int idLead, double newMontant)
    {
        try
        {
            // Construire l'URL de l'API Java
            string apiUpdateLead = $"api/lead/update?idLead={idLead}&newMontant={newMontant}";

            var response = await _httpClient.PostAsync(apiUpdateLead, null);
        
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Montant mis à jour avec succès.";
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

        // Rediriger vers la page principale après mise à jour
        return RedirectToAction("Budget");
    }
    
    


}