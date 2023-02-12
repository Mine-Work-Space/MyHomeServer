using AntDesign;

namespace MyHomeServer.Client.Services
{
    public sealed class CustomGlobalTheme : EnumValue<CustomGlobalTheme, string>
    {
        public static readonly CustomGlobalTheme Light = new(nameof(Light).ToLowerInvariant(), "_content/AntDesign/css/ant-design-blazor.css");
        public static readonly CustomGlobalTheme Dark = new(nameof(Dark).ToLowerInvariant(), "_content/AntDesign/css/ant-design-blazor.dark.css"); //"_content/AntDesign/css/ant-design-blazor.dark.css");
        public static readonly CustomGlobalTheme Compact = new(nameof(Compact).ToLowerInvariant(), "_content/AntDesign/css/ant-design-blazor.compact.css");
        public static readonly CustomGlobalTheme Aliyun = new(nameof(Aliyun).ToLowerInvariant(), "_content/AntDesign/css/ant-design-blazor.aliyun.css");

        private CustomGlobalTheme(string name, string value) : base(name, value)
        {
        }
    }
}
