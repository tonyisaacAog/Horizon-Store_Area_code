using AutoMapper;
using Data.Services;
using Horizon.Areas.Sales.Models;
using Horizon.Areas.Sales.ViewModel;
using Horizon.Data;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Horizon.Areas.Sales.Services
{
    public class ClientManager : GenericSettingsManager<Client, ClientVM>
    {
        public ClientManager(ApplicationDbContext db, IMapper mapper, SaveManager<ClientVM> saveManager) : base(db, mapper, saveManager)
        {
        }
        public async Task<(List<ClientVM> items, int totalCount)> SearchClients(string term, int page, int pageSize)
        {
            var query = _db.Clients
                   .Where(x => x.ClientName.Contains(term))
                   .OrderBy(x => x.ClientName);

            var totalCount = query.Count();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ClientVM { Id = x.Id, ClientName = x.ClientName, Phone1=x.Phone1,Phone2=x.Phone2,Phone3 =x.Phone3,Email=x.Email })
                .ToListAsync();
            return (items, totalCount);
        }
    }
}
