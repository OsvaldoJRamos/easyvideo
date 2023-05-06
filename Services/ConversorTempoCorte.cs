namespace CortadorVideo.Services
{
    public static class ConversorTempoCorte
    {
        public static TempoCorte Converter(string valorDaTela)
        {
            var tempoString = valorDaTela.Split(" ");

            var tempoInicioTimeSpan = ObterTempoTimeSpan(tempoString[0]);
            var tempoFimTimeSpan = ObterTempoTimeSpan(tempoString[2]);
            var tempoDuracaoCorte = tempoFimTimeSpan - tempoInicioTimeSpan;

            return new TempoCorte(tempoInicioTimeSpan, tempoDuracaoCorte);
        }

        private static TimeSpan ObterTempoTimeSpan(string tempoString)
        {
            var tempo = tempoString.Split(":");
            var tempoTimeSpan = TimeSpan.FromHours(Convert.ToDouble(tempo[0])) + TimeSpan.FromMinutes(Convert.ToDouble(tempo[1])) + TimeSpan.FromSeconds(Convert.ToDouble(tempo[2]));
            return tempoTimeSpan;
        }
    }

    public class TempoCorte
    {
        public TimeSpan TempoInicio { get; private set; }
        public TimeSpan DuracaoEmSegundos { get; private set; }

        public TempoCorte(TimeSpan tempoInicio, TimeSpan duracaoEmSegundos)
        {
            TempoInicio = tempoInicio;
            DuracaoEmSegundos = duracaoEmSegundos;
        }
    }
}
