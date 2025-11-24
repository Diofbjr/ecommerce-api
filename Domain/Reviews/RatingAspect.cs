namespace Ecommerce.Api.Domain.Reviews
{
    public class RatingAspect
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }   // Ex: "Entrega", "Atendimento", "Qualidade"
        public bool Active { get; set; } = true;
    }
}
