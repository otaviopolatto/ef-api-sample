namespace FinanceControl.Domain.Entities
{
    public class Lancamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TipoLancamento Tipo { get; set; }  // Propriedade do tipo ENUM
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public enum TipoLancamento
        {
            Receita = 1,
            Despesa = 2
        }

    }

   

}
