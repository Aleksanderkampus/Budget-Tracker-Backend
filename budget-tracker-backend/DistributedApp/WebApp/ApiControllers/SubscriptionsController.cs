using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts.App;
using DAL.Contracts.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.APP;
using Domain;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubscriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.SubscriptionMapper _mapper;
        private readonly IGetUserIdService _userIdService;

        public SubscriptionsController(AppDbContext context, IAppUOW uow, IAppBLL bll, IMapper mapper, IGetUserIdService userIdService)
        {
            _context = context;
            _bll = bll;
            _userIdService = userIdService;
            _mapper = new Public.DTO.Mappers.SubscriptionMapper(mapper);
        }
        

        // PUT: api/Subscriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscription(Guid id, Public.DTO.v1.SubscriptionDetails subscription)
        {
            if (id != subscription.Id)
            {
                return BadRequest();
            }

            if (!await _bll.SubscriptionService.IsOwnedByUserAsync(subscription.Id, User.GetUserId()))

            {

                return BadRequest("No hacking (bad user id)!");

            }
            
            subscription.DateStarted = subscription.DateStarted.ToUniversalTime();
            var bllSubscription = _mapper.Map(subscription);
            _bll.SubscriptionService.Update(bllSubscription!);

            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Subscriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.SubscriptionDetails>> PostSubscription(Public.DTO.v1.SubscriptionDetails subscription)
        {
            subscription.DateStarted = subscription.DateStarted.ToUniversalTime();
            var bllSubscription = _mapper.Map(subscription);
            var vm = _bll.SubscriptionService.Add(bllSubscription!);
            await _bll.SaveChangesAsync();

            return Ok(vm);
        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(Guid id)
        {
            if (!await _bll.SubscriptionService.IsOwnedByUserAsync(id, _userIdService.GetUserId()))
            {

                return BadRequest("No hacking (bad user id)!");

            }
            var transAction = await _bll.SubscriptionService.RemoveAsync(id);

            if (transAction == null) return NotFound();
            
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpGet("simple")]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.SimpleSubscription>>> GetSimpleSubscriptions()
        {
            var vm = await

                _bll.SubscriptionService.AllSimpleAsync(User.GetUserId());

            var res = vm.Select((e) => _mapper.MapSimple(e)).ToList();
            
            return res;
        }
        
        [HttpGet("details/{id}")]
        public async Task<ActionResult<Public.DTO.v1.SubscriptionDetails>> GetSubscriptionDetails(Guid id)
        {
           
            var subs = await _bll.SubscriptionService.GetSubscriptionDetails(User.GetUserId(), id);

            if (subs == null)
            {
                return NotFound();
            }

            var res = _mapper.MapDetails(subs);
            
            return res;
        }
    }
}
