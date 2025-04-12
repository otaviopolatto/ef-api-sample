namespace FinanceControl.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Lancamento> Lancamentos { get; set; } = new List<Lancamento>();

    }

}
