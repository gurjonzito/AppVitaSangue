namespace AppVitaSangue.Services
{
    public static class ImageService
    {
        public static async Task<string> SelecionarImagem()
        {
            string diretorio = "";

            var imagemSelecionada = await MediaPicker.PickPhotoAsync();

            if (imagemSelecionada != null)
            {
                diretorio = imagemSelecionada.FullPath;
            }

            return diretorio;
        }

        public static string CopiarImagemDirApp(string sDiretorioImagem)
        {
            if (sDiretorioImagem.StartsWith(AppContext.BaseDirectory))
            {
                return sDiretorioImagem;
            }

            string diretorioDestino = "";

            if (!string.IsNullOrEmpty(sDiretorioImagem))
            {
                try
                {
                    string novoDiretorio = Path.Combine(AppContext.BaseDirectory, "Imagens");

                    if (!Directory.Exists(novoDiretorio))
                    {
                        Directory.CreateDirectory(novoDiretorio);
                    }

                    string nomeArquivo = Path.GetFileName(sDiretorioImagem);
                    diretorioDestino = Path.Combine(novoDiretorio, nomeArquivo);

                    if (!File.Exists(diretorioDestino) ||
                        !sDiretorioImagem.Equals(diretorioDestino, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Copy(sDiretorioImagem, diretorioDestino, true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao copiar imagem: {ex.Message}");
                    return "";
                }
            }

            return diretorioDestino;
        }
    }
}