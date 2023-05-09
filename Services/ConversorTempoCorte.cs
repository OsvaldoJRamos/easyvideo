namespace CortadorVideo.Services
{
    public static class ConversorTempoCorte
    {
        public static Corte ConverterValorTela(string descricaoExibicaoTela)
        {
            var tempoString = descricaoExibicaoTela.Split(" ");

            var nomeNovoCorte = tempoString[0];

            var tempoInicioTimeSpan = ObterTempoTimeSpan(tempoString[2]);
            var tempoFimTimeSpan = ObterTempoTimeSpan(tempoString[4]);
            var tempoDuracaoCorte = tempoFimTimeSpan - tempoInicioTimeSpan;


            return new Corte(tempoInicioTimeSpan, tempoDuracaoCorte, nomeNovoCorte);
        }

        private static TimeSpan ObterTempoTimeSpan(string tempoString)
        {
            var tempo = tempoString.Split(":");
            var tempoTimeSpan = TimeSpan.FromHours(Convert.ToDouble(tempo[0])) + TimeSpan.FromMinutes(Convert.ToDouble(tempo[1])) + TimeSpan.FromSeconds(Convert.ToDouble(tempo[2]));
            return tempoTimeSpan;
        }
    }

    public class Corte
    {
        public TimeSpan TempoInicio { get; private set; }
        public TimeSpan DuracaoEmSegundos { get; private set; }
        public string? Nome { get; private set; }

        public Corte(TimeSpan tempoInicio, TimeSpan duracaoEmSegundos, string? nomeNovoCorte)
        {
            TempoInicio = tempoInicio;
            DuracaoEmSegundos = duracaoEmSegundos;
            Nome = nomeNovoCorte;
        }
    }
}
