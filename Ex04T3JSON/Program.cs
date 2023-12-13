using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace Ex04T3JSON
{
    struct Reserva
    {
        public string nome;
        public DateTime dataHora;

        public Reserva(string nome, DateTime dataHora)
        {
            this.nome = nome;
            this.dataHora = dataHora;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string nome, rsp;
            DateTime data, hora, dataEHora;

            List<Reserva> listaReservas = new List<Reserva>();
            string fichJson = "listaReservas.json";

            Console.WriteLine("Efectue as suas reservas:   ");
            do
            {
                Console.Write("Nome do cliente:   ");
                nome = Console.ReadLine();
                Console.Write("Data da reserva:   ");
                data = DateTime.Parse(Console.ReadLine());
                Console.Write("Hora da reserva:   ");
                hora = DateTime.Parse(Console.ReadLine());

                dataEHora = new DateTime(data.Year, data.Month, data.Day, hora.Hour, hora.Minute, hora.Second);

                Reserva reserva = new Reserva(nome, dataEHora);
                listaReservas.Add(reserva);

                Console.Write("Deseja efetuar outra reserva? s/n");
                rsp = Console.ReadLine();
            }
            while (rsp.ToUpper() == "S");

            GravarListaReservasNoFicheiro(fichJson, listaReservas);
            List<Reserva> listaReservasLidas = LerReservasDoFicheiroJson(fichJson);

            if (listaReservasLidas.Count > 0)
            {
                foreach (Reserva r in listaReservasLidas)
                    Console.WriteLine($"\n\nDados da reserva de {r.nome}" +
                        $"\n\t Nome do Cliente: {r.nome}" +
                        $"\n\t Data da reserva: {r.dataHora.ToLongDateString()}" +
                        $"\n\t Hora da reserva: {r.dataHora.ToLongTimeString()}");
            }
        }

        static void GravarListaReservasNoFicheiro(string camJson, List<Reserva> lista)
        {
            try
            {
                string textoJson = JsonConvert.SerializeObject(lista);
                File.WriteAllText(camJson, textoJson);
            }
            catch(IOException ex)
            {
                Console.WriteLine("Erro na gravação do ficheiro:   " + ex.Message );
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro:   "+ ex.Message);
            }
        }

        static List<Reserva> LerReservasDoFicheiroJson(string camjson)
        {
            if (File.Exists(camjson))
            {
                try
                {
                    string texto = File.ReadAllText(camjson);
                    List<Reserva> lista = JsonConvert.DeserializeObject<List<Reserva>>(texto);
                    return lista;
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Erro na gravação do ficheiro:  " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro:  " + ex.Message);
                }
            }
            else
                Console.WriteLine($"Erro: Ficheiro {camjson} inexistente!!");
            return null;

        }
    }
}