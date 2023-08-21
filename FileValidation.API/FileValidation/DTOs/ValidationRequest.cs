namespace FileValidation.API.FileValidation.DTOs
{
    public record ValidationRequest(string FileName, string ContentAsBase64);
}
