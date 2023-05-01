namespace Domain.Core
{
    public class IdNumerico
    {
        public int Valor { get; private set; }

        public IdNumerico(int valor)
        {
            Valor = valor;
        }
    }
}
