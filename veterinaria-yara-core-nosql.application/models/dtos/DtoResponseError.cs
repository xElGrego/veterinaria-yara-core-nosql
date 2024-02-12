namespace veterinaria_yara_core_nosql.application.models.dtos
{
    public class DtoResponseError
    {
        public int code { get; set; }
        public string? message { get; set; }
        public bool error { get; set; }
    }
}
