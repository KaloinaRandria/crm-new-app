using System.Text.Json;

namespace crm_new_app.Models;

public class TicketModel
{
    public int IdTicket { get; set; }
    public string Subject { get; set; }
    public string CustomerName { get; set; }
    public DateTime DateTime { get; set; }
    public double MontantDepense { get; set; }

    public List<TicketModel> getALlTicketModels(string data)
    {
        List<TicketModel> toReturn = new List<TicketModel>();

        using (JsonDocument doc = JsonDocument.Parse(data))
        {
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                var ticketModel = new TicketModel
                {
                    IdTicket = item.GetProperty("idTicket").GetInt32(),
                    Subject = item.GetProperty("subject").GetString(),
                    CustomerName = item.GetProperty("customerName").GetString(),
                    DateTime = item.GetProperty("dateTime").GetDateTime(),
                    MontantDepense = item.GetProperty("montantDepense").GetDouble(),
                };
                toReturn.Add(ticketModel);
            }
        }
        return toReturn;
    }
    
}