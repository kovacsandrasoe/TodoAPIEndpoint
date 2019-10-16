using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtAuthentication.Data;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext context;

        public ValuesController
            (UserManager<IdentityUser> userManager, IConfiguration configuration,
            ApplicationDbContext context)
        {
            this.context = context;
            _userManager = userManager;
            _configuration = configuration;
        }



        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            return context.Todos;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value1";
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        [HttpGet]
        public ActionResult<string> MySelf()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var myself = _userManager.GetUserAsync(this.User);
            return userId;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
