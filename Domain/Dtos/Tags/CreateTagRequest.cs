namespace FinanceControl.Domain.Dtos.Tag
{
    public class CreateTagRequest
    {
        public string Descricao { get; set; } = string.Empty;

        public static explicit operator FinanceControl.Domain.Entities.Tag(CreateTagRequest request)
        {
            return new FinanceControl.Domain.Entities.Tag
            {
                Descricao = request.Descricao,
            };
        }

    }
}
