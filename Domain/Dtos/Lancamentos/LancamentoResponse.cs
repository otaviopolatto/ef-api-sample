using FinanceControl.Domain.Entities;
using static FinanceControl.Domain.Dtos.Tag.TagResponseDto;
using static FinanceControl.Domain.Entities.Lancamento;

namespace FinanceControl.Domain.Dtos.Lancamentos
{
    //public class LancamentoResponse
    //{
    //    public int Id { get; set; }
    //    public string Descricao { get; set; }
    //    public decimal Valor { get; set; }
    //    public DateTime Data { get; set; }
    //    public TipoLancamento Tipo { get; set; } 
    //    public ICollection<TagResponse> Tags { get; set; } = new List<TagResponse>();
    //}

    public record LancamentoResponse(
        int Id,
        string Descricao,
        decimal Valor,
        TipoLancamento Tipo,
        DateTime DataOperacao,
        List<TagResponse> Tags
    );


}

