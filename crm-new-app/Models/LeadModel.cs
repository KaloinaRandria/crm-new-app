using System.Text.Json;

namespace crm_new_app.Models;

public class LeadModel
{
    public int IdLead { get; set; }
    public string LeadName { get; set; }
    public string CustomerName { get; set; }
    public DateTime DateTime { get; set; }
    public double MontantDepense { get; set; }

    public List<LeadModel> getAllLeadsModels(string data)
    {
        List<LeadModel> leads = new List<LeadModel>();
        using (JsonDocument doc = JsonDocument.Parse(data))
        {
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                var leadModel = new LeadModel
                {
                    IdLead = item.GetProperty("idLead").GetInt32(),
                    LeadName = item.GetProperty("leadName").GetString(),
                    CustomerName = item.GetProperty("customerName").GetString(),
                    DateTime = item.GetProperty("dateTime").GetDateTime(),
                    MontantDepense = item.GetProperty("montantDepense").GetDouble(),
                };
                leads.Add(leadModel);
            }
        }
        return leads;
    }

    public double GetMontantDepense(List<LeadModel> leadsModels)
    {
        double totalLeads = 0;
        foreach (var item in leadsModels)
        {
            totalLeads += item.MontantDepense;
        }
        return totalLeads;
    }
}