using FinanceControl.Domain.Dtos;
using FinanceControl.Domain.Dtos.Lancamentos;
using FinanceControl.Domain.Entities;

namespace FinanceControl.Service.LancamentoService
{
    public interface ILancamentoService
    {
        Task<IEnumerable<LancamentoResponse>> GetLancamentosService();

        Task<Response<LancamentoResponse>> GetLancamentoByIdService(int idLancamento);

        Task<Response<Lancamento>> CreateLancamentoService(CreateLancamentoRequest request);

        Task<Response<Lancamento>> UpdateLancamentoService(UpdateLancamentoRequest request);

        Task<Response<Lancamento>> DeleteLancamentoService(int idLancamento);



    }
}
