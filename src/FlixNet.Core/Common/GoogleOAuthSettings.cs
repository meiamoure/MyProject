using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixNet.Core.Common;

public class GoogleOAuthSettings
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string RedirectUri { get; set; } = default!;
    public string AuthEndpoint { get; set; } = default!;
    public string TokenEndpoint { get; set; } = default!;
    public string UserInfoEndpoint { get; set; } = default!;
}
