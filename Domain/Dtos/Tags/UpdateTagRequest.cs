namespace FinanceControl.Domain.Dtos.Tag
{
    public class UpdateTagRequest
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public static explicit operator FinanceControl.Domain.Entities.Tag(UpdateTagRequest request)
        {
            return new FinanceControl.Domain.Entities.Tag
            {
                Id = request.Id,
                Descricao = request.Descricao,
            };
        }

    }

}
