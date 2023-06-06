using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.APP;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;
using SubscriptionMapper = Public.DTO.Mappers.SubscriptionMapper;

namespace WebApp.ApiControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class SubscriptionTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly Public.DTO.Mappers.SubscriptionTypeMapper _mapper;

        public SubscriptionTypesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new SubscriptionTypeMapper(mapper);
            
        }

        // GET: api/SubscriptionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.SubscriptionType>>> GetSubscriptionTypes()
        {
           var vm = await _bll.SubscriptionTypeService.AllAsync();
          
          var res = vm.Select((e) => _mapper.Map(e)).ToList();
          
          return res;
        }
    }
}
