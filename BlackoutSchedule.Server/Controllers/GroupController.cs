using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace BlackoutSchedule.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<List<GroupModel>> GetGroups()
        {
            IEnumerable<Group> groups = await _groupService.GetListAcync();
            var result = new List<GroupModel>();

            foreach (var group in groups)
            {
                result.Add(
                    new GroupModel
                    {
                        Id = group.Id,
                        Name = group.Name
                    });
            }

            return result;
        }
    }
}
