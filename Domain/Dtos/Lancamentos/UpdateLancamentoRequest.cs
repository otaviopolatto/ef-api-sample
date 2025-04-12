using static FinanceControl.Domain.Entities.Lancamento;

namespace FinanceControl.Domain.Dtos.Lancamentos
{
    public class UpdateLancamentoRequest
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoLancamento Tipo { get; set; } 
        public List<int> Tags { get; set; } = new List<int>();

    }

    public static class UpdateLancamentoExtension
    {
        public static FinanceControl.Domain.Entities.Lancamento MapToLancamento(this UpdateLancamentoRequest request, List<FinanceControl.Domain.Entities.Tag> tags)
        {
            return new FinanceControl.Domain.Entities.Lancamento
            {
                Id = request.Id,
                Descricao = request.Descricao,
                Valor = request.Valor,
                Data = request.Data,
                Tipo = request.Tipo,
                Tags = tags
            };
        }
    }

}
