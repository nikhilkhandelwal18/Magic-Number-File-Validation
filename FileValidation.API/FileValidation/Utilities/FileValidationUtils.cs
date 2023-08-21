using FileValidation.API.FileValidation.DTOs;
using Microsoft.AspNetCore.StaticFiles;

namespace FileValidation.API.FileValidation.Utilities
{
    public class FileValidationUtils
    {
        private static string UnknownMime => "APPLICATION/OCTET-STREAM";

        public static bool MagicNumberValidation(List<string> supportedTypes, List<ValidationRequest> attachments)
        {
            foreach (ValidationRequest attachment in attachments)
            {
                string fileExtension = Path.GetExtension(attachment.FileName);

                if (supportedTypes == null || !supportedTypes.Any()) return false;

                if (supportedTypes.ConvertAll(s => s.ToUpper()).Contains(fileExtension.ToUpper()))
                {
                    new FileExtensionContentTypeProvider()
                        .TryGetContentType(attachment.FileName, out string contentType);

                    string mimeType = contentType?.ToUpper() ?? UnknownMime;



                    /* Each OLD Office file has different type of contentType:
                     * ".xls", "application/vnd.ms-excel"
                     * ".doc", "application/msword"                      
                     */
                    if ((new string[] { "APPLICATION/MSWORD", "APPLICATION/VND.MS-EXCEL" }).Contains(mimeType.ToUpper()))
                    {
                        mimeType = "APPLICATION/VND.MS";
                    }


                    //Each LATEST Office file has different type of contentType:
                    //eg: DOCX: APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.WORDPROCESSINGML.DOCUMENT
                    //replaced the string as generic contentType
                    if ((new string[] { ".DOCX", ".XLSX", ".PPTX" }).Contains(fileExtension.ToUpper()))
                    {
                        mimeType = "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT";
                    }

                    //To handle outlook .msg file 
                    if ((new string[] { ".MSG" }).Contains(fileExtension.ToUpper()))
                    {
                        mimeType = "APPLICATION/MSWORD";
                    }


                    string mimeTypeMagicNumber = MagicNumberSniffer(attachment.ContentAsBase64);


                    return mimeType.ToUpper() == mimeTypeMagicNumber.ToUpper();
                }

                return false;
            }

            return false;
        }

        public static string MagicNumberSniffer(string contentAsBase64)
        {
            string contentType = String.Empty;
            Byte[] chkbytes = Convert.FromBase64String(contentAsBase64.Split(",").Last());

            string data_as_hex = BitConverter.ToString(chkbytes);
            string magicCheck = data_as_hex.Substring(0, 11);

            //Set the contenttype based on File Extension
            switch (magicCheck)
            {
                case "89-50-4E-47":
                    contentType = "image/png";
                    break;
                case "FF-D8-FF-E1":
                    contentType = "image/jpg";
                    break;
                case "FF-D8-FF-E0":
                    contentType = "image/jpeg";
                    break;
                case "25-50-44-46":
                    contentType = "APPLICATION/PDF";
                    break;
                case "50-4B-03-04": //.docx, .xlsx
                    //actual contentType = "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT.WORDPROCESSINGML.DOCUMENT";
                    contentType = "APPLICATION/VND.OPENXMLFORMATS-OFFICEDOCUMENT";
                    break;
                case "D0-CF-11-E0": //.doc , .xls
                    contentType = "APPLICATION/VND.MS";
                    break;

            }

            return contentType;
        }
    }
}
