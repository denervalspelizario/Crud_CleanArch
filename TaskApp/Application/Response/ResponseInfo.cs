
namespace Application.Response
{
    public record ResponseInfo
    {
        public string? Title { get; set; }
        public string? ErrorDEscription { get; set; }
        public int? HTTPStatus { get; set; }
    }
}
