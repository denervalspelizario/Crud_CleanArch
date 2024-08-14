

namespace Application.Response
{
    // Estrutura de resposta
    public record ResponseBase<T>
    {
        public ResponseInfo? ResponseInfo { get; set; }
        public T? Value { get; set; }
    }
}
