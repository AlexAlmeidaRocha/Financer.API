using Microsoft.AspNetCore.Mvc;
using FinancerAPI.Abstractions.Interface.Repositories;
using FinancerAPI.Domain.Entities;
using FinancerAPI.Domain.Enums;
using FinancerAPI.Models;
using FinancerAPI.Data.Dtos;

namespace FinancerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtratoController : ControllerBase
    {
        private readonly ILogger<ExtratoController> _logger;
        private readonly IExtratoRepository _extractRepository;

        public ExtratoController(ILogger<ExtratoController> logger, IExtratoRepository extratoRepository)
        {
            _logger = logger;
            _extractRepository = extratoRepository;
        }

        [HttpPost()]
        public async Task<IActionResult> Post(ExtratoCreateModel extratoCreateModel)
        {
            await AddExtrato(extratoCreateModel, false);

            return Created("post", extratoCreateModel);
        }

        [HttpPost("avulso")]
        public async Task<IActionResult> PostAvulso(ExtratoCreateModel extratoCreateModel)
        {
            await AddExtrato(extratoCreateModel, true);

            return Created("post", extratoCreateModel);
        }

        private async Task AddExtrato(ExtratoCreateModel extratoCreateModel, bool avulso)
        {
            await _extractRepository.Add(new Extrato()
            {
                Description = extratoCreateModel.Description,
                Date = extratoCreateModel.Date.Date,
                Value = extratoCreateModel.Value,
                Avulso = avulso,
                Status = ExtratoStatus.Valido
            });
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch(int id, ExtratoPatchModel extratoPatchModel)
        {
            var extrato = await _extractRepository.GetById(id);

            if (extrato == null)
                return BadRequest("Lancamento não encontrado.");

            if (!extrato.Avulso)
                return BadRequest("Somente lancamentos avulsos podem ser alterados");

            if (extrato.Status == ExtratoStatus.Cancelado)
                return BadRequest("Não pode alterar lancamentos cancelados.");

            extrato.Description = extratoPatchModel.Description;
            extrato.Date = extratoPatchModel.Date;

            await _extractRepository.Update(extrato);

            return Accepted();
        }

        [HttpPatch("cancelar")]
        public async Task<IActionResult> PatchCancel(int id)
        {
            var extrato = await _extractRepository.GetById(id);

            if (extrato == null)
                return BadRequest("Lancamento não encontrado.");

            if (!extrato.Avulso)
                return BadRequest("Somente lancamentos avulsos podem ser alterados");

            if (extrato.Status == ExtratoStatus.Cancelado)
                return BadRequest("Não pode alterar lancamentos cancelados.");

            extrato.Status = ExtratoStatus.Cancelado;

            await _extractRepository.Update(extrato);

            return Accepted();
        }

        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get(DateTime? dtInitial, DateTime? dtEnd)
        {

            if (dtInitial == null || dtEnd == null)
            {
                dtInitial = DateTime.Now.AddDays(-2);
                dtEnd = DateTime.Now;
            }

            var result = await _extractRepository.GetAll(dtInitial.Value, dtEnd.Value);

            var balance = result.Where(x => x.Status == ExtratoStatus.Valido).Sum(x => x.Value);

            return Ok(new { extrato = result, balance });
        }

        [HttpGet("chart")]
        public async Task<IActionResult> GetDataChart()
        {
            var dashborad = await _extractRepository.GetChart();

            for (int i = 1; i != 12; i++)
            {
                if (!dashborad.Positive.Any(x => x.Month == i))
                    dashborad.Positive.Add(new ChartDto() { Month = i, Value = 0 });
                if (!dashborad.Negative.Any(x => x.Month == i))
                    dashborad.Negative.Add(new ChartDto() { Month = i, Value = 0 });
            }

            return Ok(new
            {
                dataPositive = dashborad.Positive.OrderBy(o => o.Month).Select(s => s.Value).ToArray(),
                dataNegative = dashborad.Negative.OrderBy(o => o.Month).Select(s => s.Value).ToArray(),
                currentYear = DateTime.Now.Year,
                revenue = dashborad.Positive.Sum(s => s.Value),
                expense = dashborad.Negative.Sum(s => s.Value),
                balance = dashborad.Positive.Sum(s => s.Value) + dashborad.Negative.Sum(s => s.Value)
            });
        }
    }
}
