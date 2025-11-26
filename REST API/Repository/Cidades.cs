using REST_API.Models;
using System.Text;
using System.Text.Json;

namespace REST_API.Repository {
    public static class CidadesRepository {

        private static List<Cidade> cidades;
        private const string EnderecoJson = "C:\\Users\\Kali\\source\\repos\\REST API\\REST API\\cidades.json";

        public static List<Cidade> Cidades {
            get {
                if (cidades == null)
                {
                    string jsonString = File.ReadAllText(EnderecoJson);

                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        cidades = JsonSerializer.Deserialize<List<Cidade>>(jsonString);
                    }
                    else
                    {
                        CarregarCidades();
                    }

                    return cidades;

                }
                else {
                    return cidades;
                }
            }
        }

        private static void CarregarCidades() {
            cidades = new List<Cidade>()
            {
                new Cidade() {
                    Id = 100,
                    Nome = "Camutanga",
                    IdEstado = 81,
                    IdPais = 55,
                    Populacao = 7000
                },
                new Cidade() {
                    Id = 200,
                    Nome = "Ferreiros",
                    IdEstado = 81,
                    IdPais = 55,
                    Populacao = 11000
                },
                new Cidade() {
                    Id = 300,
                    Nome = "Timbauba",
                    IdEstado = 81,
                    IdPais = 55,
                    Populacao = 52000
                },
                new Cidade() {
                    Id = 400,
                    Nome = "Juripiranga",
                    IdEstado = 83,
                    IdPais = 55,
                    Populacao = 10240
                }
            };
        }

        public static void Save()
        {
            string jsonString = JsonSerializer.Serialize(cidades);
            File.WriteAllText(EnderecoJson, jsonString, Encoding.UTF8);
        }
    }
}
