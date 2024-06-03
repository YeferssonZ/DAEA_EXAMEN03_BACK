namespace backend.DTOs
{
    public class PeliculaResponse
    {
        public string? Id { get; set; }
        public string? Titulo { get; set; }
        public string? VideoUrl { get; set; }

        public List<GeneroDto>? Generos { get; set; }

    }
}