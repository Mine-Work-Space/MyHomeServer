using AntDesign;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyHomeServer.Client.Services
{
    public class CustomConfigService
    {
        private IJSRuntime _jS;

        public CustomConfigService(IJSRuntime js)
        {
            _jS = js;
        }

        public async Task ChangeDirection(string direction)
        {
            direction = direction?.ToLowerInvariant();
            await _jS.InvokeVoidAsync(JSInteropConstants.SetDomAttribute, "html", new Dictionary<string, string>
            {
                ["class"] = direction,
                ["data-direction"] = direction
            });
        }

        public async Task SetCustomTheme(string cssLinkPath, string csslinkSelector)
        {
            var js = @$"document.querySelector(""{csslinkSelector}"").setAttribute(""href"", ""{cssLinkPath}"");";
            await _jS.InvokeVoidAsync("eval", js);
        }

        public async Task SetTheme(CustomGlobalTheme theme)
        {
            var js = """
                    let item = Array.from(document.getElementsByTagName("link")).find((item) =>
                       item.getAttribute("href")?.match("_content/AntDesign/css/ant-design-blazor")
                    );
                    if (!item) {
                        item = document.createElement('link');
                        item.rel="stylesheet";
                        document.head.appendChild(item);
                    }
                    item.href = "{{theme}}";
                    """.Replace("{{theme}}", theme.Value);

            await _jS.InvokeVoidAsync("eval", js);
        }
    }
}
