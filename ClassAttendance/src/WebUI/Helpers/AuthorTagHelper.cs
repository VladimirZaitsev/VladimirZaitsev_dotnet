using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.Helpers
{
    [HtmlTargetElement("author")]
    public class AuthorTagHelper : TagHelper
    {
        private const string RepositoryAddress = "https://github.com/VladimirZaitsev/VladimirZaitsev_dotnet";
        private const string Author = "Vladimir Zaitsev";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", RepositoryAddress);
            output.Content.SetContent(Author);
        }
    }
}
