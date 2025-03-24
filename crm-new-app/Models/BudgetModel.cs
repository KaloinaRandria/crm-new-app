using System.Text.Json;

namespace crm_new_app.Models;

public class BudgetModel
{
    public int IdBudgetModel { get; set; }
    public string CustomerName { get; set; }
    public double Montant { get; set; }
    public DateTime DateTime { get; set; }
    public int IdCustomer { get; set; }

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
                    IdCustomer = item.GetProperty("idCustomer").GetInt32(),
                };
                toReturn.Add(budgetModel);
            }
        }
        return toReturn;
    }

    public Dictionary<int, double> budgetByIdCustomer(List<BudgetModel> budgetModels)
    {
        var budgetDict = new Dictionary<int, double>();

        foreach (var budget in budgetModels)
        {
            if (budgetDict.ContainsKey(budget.IdCustomer))
            {
                budgetDict[budget.IdCustomer] += budget.Montant;
            }
            else
            {
                budgetDict[budget.IdCustomer] = budget.Montant;
            }
        }
        
        return budgetDict.Take(3).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);       
    }

    public double getTotalMontant(List<BudgetModel> budgetModels)
    {
        double total = 0;
        foreach (var item in budgetModels)
        {
            total += item.Montant;
        }
        return total;
    }
}