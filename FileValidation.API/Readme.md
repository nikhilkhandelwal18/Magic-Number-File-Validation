Swaggger 

1. Install-Package Swashbuckle.AspNetCore
2. builder.Services.AddSwaggerGen();
3. if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
4. launchSettings.json profile
    "launchUrl": "swagger"




    Validate upload file
    validate uploaded file's base64 string with actual file extension (MIME Type)

    "supportedTypes": [".jpg", ".jpeg", ".png", ".pdf", ".tiff", ".txt", ".xls", ".xlsx", ".doc", ".docx", ".msg"]