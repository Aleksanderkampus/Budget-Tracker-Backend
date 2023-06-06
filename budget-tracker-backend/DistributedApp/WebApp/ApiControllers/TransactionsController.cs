
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TransactionMapper _mapper;
        private readonly IGetUserIdService _userIdService;

        public TransactionsController(IMapper mapper, IAppBLL bll, IGetUserIdService userIdService)
        {
            _bll = bll;
            _mapper = new TransactionMapper(mapper);
            _userIdService = userIdService;
        }

        // GET: api/v1/Transactions
        [HttpGet]
        public async Task<IEnumerable<TransactionWithCategories?>> GetTransactions()
        {
            var vm = await _bll.TransactionService.GetTransactionsWithCategories(_userIdService.GetUserId());

            var res = vm.Select((e) => _mapper.MapTransactionWithCategories(e));
            
            return res;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionDetailsDTO>> PutTransaction(Guid id, Public.DTO.v1.TransactionDetailsDTO transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            if (!await _bll.TransactionService.IsOwnedByUserAsync(transaction.Id, _userIdService.GetUserId()))
            {

                return BadRequest("No hacking (bad user id)!");

            }
            
            var bllTransaction = _mapper.Map(transaction);

            _bll.TransactionService.Update(bllTransaction!);

            await _bll.SaveChangesAsync();
            
            return _mapper.Map(bllTransaction);
        }

        // POST: api/v1/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionDetailsDTO>> PostTransaction(TransactionDetailsDTO transaction)
        {
            var bllTransaction = _mapper.Map(transaction);
            var vm = _bll.TransactionService.Add(bllTransaction!);
            await _bll.SaveChangesAsync();

            return _mapper.Map(bllTransaction)!;
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            if (!await _bll.TransactionService.IsOwnedByUserAsync(id, _userIdService.GetUserId()))
            {

                return BadRequest("No hacking (bad user id)!");

            }
            var transAction = await _bll.TransactionService.RemoveAsync(id);

            if (transAction == null) return NotFound();
            
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        [HttpGet("details/{id}")]
        public async Task<ActionResult<TransactionDetailsDTO>> GetTransactionDetails(Guid id)
        {
          
            var transaction = await _bll.TransactionService.GetDetails(_userIdService.GetUserId(),id);

            if (transaction == null)
            {
                return NotFound();
            }

            var result = _mapper.MapDetails(transaction);

            return result;
        }

        [HttpGet("graphdata")]
        public async Task<ActionResult<IEnumerable<TransactionGraphData>>> GetTransactionGraphData()
        {
            var vm = await _bll.TransactionService.GetGraphData(_userIdService.GetUserId());

            var res = vm.Select((e) => _mapper.MapGraphData(e)).ToList();

            return res;
        }

    }
}
