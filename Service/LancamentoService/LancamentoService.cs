using FinanceControl.Domain.Entities;
using Financecontrol.Infrastructure.Persistence;
using FinanceControl.Domain.Dtos.Lancamentos;
using Microsoft.EntityFrameworkCore;
using static FinanceControl.Domain.Dtos.Tag.TagResponseDto;
using FinanceControl.Domain.Dtos;

namespace FinanceControl.Service.LancamentoService
{
    public class LancamentoService : ILancamentoService
    {
        private readonly AppDbContext _context;

        public LancamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LancamentoResponse>> GetLancamentosService()
        {
            /* Não recomendo por questões de performance 
            var lancamentosResponse = await _context.Lancamentos
                .Include(l => l.Tags)                
                .ToListAsync();
            */

            var lancamentosResponse = await _context.Lancamentos
            .Select(l => new LancamentoResponse(
                l.Id,
                l.Descricao,
                l.Valor,
                l.Tipo,
                l.Data,
                l.Tags.Select(t => new TagResponse(t.Id, t.Descricao)).ToList())
            )
            .AsNoTracking() // Melhora performance para consultas somente leitura
            .AsSplitQuery() // Evita produto cartesiano em relacionamentos
            .ToListAsync();

            return lancamentosResponse;

        }

        public async Task<Response<LancamentoResponse>> GetLancamentoByIdService(int idLancamento)
        {

            var lancamentoResponse = await _context.Lancamentos
                .Where(l => l.Id == idLancamento)
                .Select(l => new LancamentoResponse(
                l.Id,
                l.Descricao,
                l.Valor,
                l.Tipo,
                l.Data,
                l.Tags.Select(t => new TagResponse(t.Id, t.Descricao)).ToList()))
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (lancamentoResponse != null)
                return new Response<LancamentoResponse> { IsSuccess = true, Data = lancamentoResponse, Status = 200 };

            return new Response<LancamentoResponse> { IsSuccess = false, Message = "Lançamento não encontrado", Status = 404 };

        }

        public async Task<Response<Lancamento>> CreateLancamentoService(CreateLancamentoRequest request)
        {
            List<Tag> tags = new List<Tag>();

            if (request.Tags != null && request.Tags.Any())
            {
                tags = await _context.Tags.Where(t => request.Tags.Contains(t.Id)).ToListAsync();
            }

            Lancamento lancamento = CreateLancamentoExtension.MapToLancamento(request, tags);

            _context.Lancamentos.Add(lancamento);

            var saveResponse = await _context.SaveChangesAsync();

            if(saveResponse > 0)
                return new Response<Lancamento> { Status = 201, Message = "Lançamento registrado com sucesso", Data = lancamento, IsSuccess = true };


            return new Response<Lancamento> { Status = 400, Message = "Falha ao registrar lançamento", IsSuccess = false };

        }

        public async Task<Response<Lancamento>> UpdateLancamentoService(UpdateLancamentoRequest request)
        {
            List<Tag> tags = new List<Tag>();

            if (request.Tags != null && request.Tags.Any())
            {
                tags = await _context.Tags.Where(t => request.Tags.Contains(t.Id)).ToListAsync();
            }

            var existingLancamento = await _context.Lancamentos
                .Where(l => l.Id == request.Id)
                .Include(l => l.Tags)
                .FirstOrDefaultAsync();

            if (existingLancamento == null)
                return new Response<Lancamento> { Status = 404, IsSuccess = false, Message = "Lançamento não encontrado" };

            existingLancamento.Descricao = request.Descricao;
            existingLancamento.Valor = request.Valor;
            existingLancamento.Tipo = request.Tipo;
            existingLancamento.Data = request.Data;
            existingLancamento.Tags = tags;

            var saveResponse = await _context.SaveChangesAsync();
          
            if (saveResponse > 0)
                return new Response<Lancamento> { Status = 200, Message = "Lançamento atualizado com sucesso", Data = existingLancamento, IsSuccess = true };

            return new Response<Lancamento> { Status = 400, IsSuccess = false, Message = "Falha ao atualizar lançamento" };
        
        }

        public async Task<Response<Lancamento>> DeleteLancamentoService(int idLancamento)
        {
            var lancamentoResponse = await _context.Lancamentos.FindAsync(idLancamento);

            if (lancamentoResponse == null)
                return new Response<Lancamento> { Status = 404, IsSuccess = false, Message = "Lançamento não encontrado" };

            _context.Lancamentos.Remove(lancamentoResponse);

            await _context.SaveChangesAsync();

            return new Response<Lancamento> { Status = 204, IsSuccess = true, Message = "Lançemento removido com sucesso" };

        }


    }
}
