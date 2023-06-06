using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts.App;
using DAL.Contracts.App;
using DAL.DTO;
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
    public class FinancialCategoryController : ControllerBase
    {

        private readonly IAppBLL _bll;
        private readonly CategoryMapper _mapper;

        public FinancialCategoryController( IMapper mapper, IAppBLL bll)
        {
           
            _bll = bll;
            _mapper = new CategoryMapper(mapper);
        }

        // GET: api/v1/FinancialCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryWithTransactionAndBudgetDTO>>> GetFinancialCategories()
        {
            var vm = await

                _bll.CategoryService.AllCategoryWithTransactionAndBudgetAsync(User.GetUserId());

            var res = vm.Select((c) => _mapper.MapCategoryWithTransactionAndBudget(c)).ToList();
            
            return res;

        }

        [HttpGet("simple")]
        public async Task<ActionResult<IEnumerable<SimpleCategoryDTO>>> GetSimpleCategories()
        {
            var vm = await

                _bll.CategoryService.AllSimpleCategoriesAsync();

            var res = vm.Select((e) => _mapper.MapSimpleCategory(e)).ToList();
            
            return res;

        }
        
        [HttpGet("details/{id}")]
        public async Task<ActionResult<CategoryDetailsDTO>> GetCategoryDetails(Guid id)
        {
           
            var financialCategory = await _bll.CategoryService.GetCategoryDetails(User.GetUserId(), id);

            if (financialCategory == null)
            {
                return NotFound();
            }

            var res = _mapper.MapCategoryDetails(financialCategory);

            return res;
        }
        
        [HttpGet("piechart")]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.PieChartData>>> GetPieChartData()
        {
            var vm = await

                _bll.CategoryService.GetPieChartData(User.GetUserId());

            var res = vm.Select((e) => _mapper.MapPieChart(e)).ToList();
            
            return res;

        }
    }
}
