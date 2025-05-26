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
            string diretorioDestino = "";

            if (!string.IsNullOrEmpty(sDiretorioImagem))
            {
                string novoDiretorio = Path.Combine(AppContext.BaseDirectory, "Imagens");

                if (!Directory.Exists(novoDiretorio))
                {
                    Directory.CreateDirectory(novoDiretorio);
                }

                diretorioDestino = Path.Combine(novoDiretorio, Path.GetFileName(sDiretorioImagem));

                File.Copy(sDiretorioImagem, diretorioDestino, overwrite: true);
            }

            return diretorioDestino;
        }
    }
}