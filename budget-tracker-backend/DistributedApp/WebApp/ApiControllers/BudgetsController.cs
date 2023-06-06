using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.APP;
using Domain;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BudgetsController : ControllerBase
    {
        private readonly BudgetMapper _mapper;
        private readonly IAppBLL _bll;
        private readonly IGetUserIdService _userIdService;


        public BudgetsController(IMapper mapper, IAppBLL bll, IGetUserIdService userIdService)
        {
            _bll = bll;
            _userIdService = userIdService;
            _mapper = new BudgetMapper(mapper);
        }
        
        // PUT: api/Budgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(Guid id, Public.DTO.v1.BudgetToEdit budget)
        {
            if (id != budget.Id)
            {
                return BadRequest();
            }

            if (!await _bll.BudgetService.IsOwnedByUserAsync(budget.Id, User.GetUserId()))

            {

                return BadRequest("No hacking (bad user id)!");

            }
            
            budget.DateFrom = budget.DateFrom.ToUniversalTime();
            budget.DateTo = budget.DateTo.ToUniversalTime();
            var bllBudget = _mapper.Map(budget);

            _bll.BudgetService.Update(bllBudget!);

            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Budgets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.BudgetToEdit>> PostBudget(Public.DTO.v1.BudgetToEdit budget)
        {
            var bllBudget = _mapper.Map(budget);
            var vm = _bll.BudgetService.Add(bllBudget!);
            await _bll.SaveChangesAsync();

            return Ok(vm);
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            if (!await _bll.BudgetService.IsOwnedByUserAsync(id, _userIdService.GetUserId()))
            {

                return BadRequest("No hacking (bad user id)!");

            }
            var budget = await _bll.BudgetService.RemoveAsync(id);

            if (budget == null) return NotFound();
            
            await _bll.SaveChangesAsync();
            
            return NoContent();
        } 

        // GET: api/v1/Budgets
        [HttpGet("simple")]
        public async Task<ActionResult<IEnumerable<SimpleBudgetDTO>>> GetSimpleBudgets()
        {
            var budgetDTO = await _bll.BudgetService.AllSimpleBudgetsAsync(User.GetUserId());

            if (budgetDTO == null)
            {
                return NotFound();
            }

            var res = budgetDTO.Select(e => _mapper.MapSimpleBudget(e)).ToList();
            
            return res;
        }
        
        // GET: api/Budgets/5
        [HttpGet("details/{id}")]
        public async Task<ActionResult<BudgetDetailsDTO>> GetBudgetDetails(Guid id)
        {
           
            var budget = await _bll.BudgetService.GetDetails(User.GetUserId(), id);

            if (budget == null)
            {
                return NotFound();
            }

            var res = _mapper.MapBudgetDetails(budget);
            
            return res;
        }
        
        // GET: api/Budgets/5
        [HttpGet("edit/{id}")]
        public async Task<ActionResult<Public.DTO.v1.BudgetToEdit>> GetBudgetToEdit(Guid id)
        {
           
            var budget = await _bll.BudgetService.GetBudgetToEdit(User.GetUserId(), id);

            if (budget == null)
            {
                return NotFound();
            }

            var res = _mapper.MapBudgetToEdit(budget);
            
            return res;
        }

    }
}
