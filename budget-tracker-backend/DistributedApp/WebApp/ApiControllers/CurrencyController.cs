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
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {

        private readonly CurrencyMapper _mapper;
        private readonly IAppBLL _bll;

        public CurrencyController(IMapper mapper, IAppBLL bll)
        {
           
            _bll = bll;
            _mapper = new CurrencyMapper(mapper);
        }

        // GET: api/v1/Currency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyDTO>>> GetCurrencies()
        {
            var vm = await

                _bll.CurrencyService.AllSimpleCurrencyAsync();

            var res = vm.Select((c) => _mapper.MapSimpleCurrency(c)).ToList();
            
            return res;
        }
        
    }
}
