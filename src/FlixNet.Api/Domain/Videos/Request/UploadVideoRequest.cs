using System.ComponentModel.DataAnnotations;

namespace FlixNet.Api.Domain.Videos.Request;

public class UploadVideoRequest
{
    [Required]
    public IFormFile File { get; set; } = null!;
}
