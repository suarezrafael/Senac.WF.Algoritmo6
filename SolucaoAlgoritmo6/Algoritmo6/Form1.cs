using System.Net.Http.Json;

namespace Algoritmo6
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        private List<Etiqueta>? etiquetas;

        public Form1(IHttpClientFactory httpClientFactory)
        {
            InitializeComponent();
            _httpClient = httpClientFactory.CreateClient("ApiClient");

            //_httpClient.DefaultRequestHeaders.Authorization =
            //new AuthenticationHeaderValue("Bearer", "seu_token_aqui");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await CarregarEtiquetasAsync("");
        }

        private async Task CarregarEtiquetasAsync(string filtro)
        {
            try
            {
                var request = new FiltroEtiqueta { Texto = filtro };

                var response = await _httpClient.PostAsJsonAsync("etiquetas/filtrar", request);

                if (response.IsSuccessStatusCode)
                {
                    etiquetas = await response.Content.ReadFromJsonAsync<List<Etiqueta>>();

                    if (etiquetas != null)
                    {
                        dataGridView1.DataSource = etiquetas;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.Columns["Id"].HeaderText = "Código";
                        dataGridView1.Columns["Nome"].HeaderText = "Etiqueta";
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao buscar etiquetas: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar etiquetas: {ex.Message}");
            }
        }
    }

    public class Etiqueta
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class FiltroEtiqueta
    {
        public string Texto { get; set; } = string.Empty;
    }
}
