using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Overtime_Project.Models;
using Overtime_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplicationOvertime.Base;

namespace WebApplicationOvertime.Repository.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AccountRepository(Address address, string request = "Account/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<AccountUserVM>> GetUserData()
        {
            List<AccountUserVM> entities = new List<AccountUserVM>();

            using (var response = await httpClient.GetAsync(request + "userdata"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<AccountUserVM>>(apiResponse);
            }
            return entities;
        }

    }
}
