using System.Text.Json;

namespace crm_new_app.Models;

public class BudgetModel
{
    public int IdBudgetModel { get; set; }
    public string CustomerName { get; set; }
    public double Montant { get; set; }
    public DateTime DateTime { get; set; }

    public List<BudgetModel> GetAllBudgetModels(string data)
    {
        List<BudgetModel> toReturn = new List<BudgetModel>();

        using (JsonDocument doc = JsonDocument.Parse(data))
        {
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                var budgetModel = new BudgetModel
                {
                    IdBudgetModel = item.GetProperty("idBudgetModel").GetInt32(),
                    CustomerName = item.GetProperty("customerName").GetString(),
                    Montant = item.GetProperty("montant").GetDouble(),
                    DateTime = item.GetProperty("dateTime").GetDateTime(),
                };
                toReturn.Add(budgetModel);
            }
        }
        return toReturn;
    }
}