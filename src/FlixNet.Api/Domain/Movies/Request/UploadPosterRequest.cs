namespace FlixNet.Api.Domain.Movies.Request;

public class UploadPosterRequest
{
    public IFormFile File { get; set; } = default!;
}
