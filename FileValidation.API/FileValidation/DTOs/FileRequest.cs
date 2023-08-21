namespace FileValidation.API.FileValidation.DTOs
{
    //public class FileValidateDTO
    //{
    //    public List<string> SupportedTypes { get; set; }
    //    public List<AttachmentDTO> Attachments { get; set; }
    //}




    public record FileRequest(List<string> SupportedTypes, List<ValidationRequest> Attachments);
}
