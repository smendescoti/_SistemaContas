namespace SistemaContas.Presentation
{
    /// <summary>
    /// Classe para controle de cache do AspNet MVC
    /// </summary>
    public class CacheControl
    {
        /// <summary>
        /// Objeto que permite executar uma ação no sistema
        /// sempre que uma página/requisição for aberta
        /// </summary>
        private readonly RequestDelegate? _requestDelegate;

        /// <summary>
        /// Construtor para que o AspNet inicialize o atributo
        /// (Injeção de dependência)
        /// </summary>
        public CacheControl(RequestDelegate? requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        /// <summary>
        /// Método executado antes de qualquer página do sistema ser aberta
        /// </summary>
        public async Task Invoke(HttpContext httpContext)
        {
            //Limpar o cache do navegador
            httpContext.Response.OnStarting((state) => 
            {
                httpContext.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                httpContext.Response.Headers.Append("Pragma", "no-cache");
                httpContext.Response.Headers.Append("Expires", "0");

                return Task.FromResult(0);
            }, null);

            await _requestDelegate.Invoke(httpContext);
        }
    }
}
