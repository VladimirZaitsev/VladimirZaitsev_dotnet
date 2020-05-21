using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace WebUI.Helpers
{
    public static class HtmlHelpers
    {
        public static HtmlString CreateMenu(this IHtmlHelper html, string controllerName)
        {
            var lower = controllerName.ToLower();
            var sb = new StringBuilder();
            sb.Append("<li class=\"nav-item my-auto\">");
            sb.Append("<div class=\"dropdown\">");
            sb.Append("<button type =\"button\" class=\"dropdown-toggle\" style=\"background: none; border: none\"data-toggle=\"dropdown\">");
            sb.Append(controllerName);
            sb.Append("</button>");
            sb.Append("<div class=\"dropdown-menu\">");
            sb.Append($"<a class=\"nav-link text-dark\" href=\"/{controllerName}/List\">View {lower}</a>");
            sb.Append($"<a class=\"nav-link text-dark\" href=\"/{controllerName}/Add\">Add new {lower}</a>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</li>");

            return new HtmlString(sb.ToString());
        }
    }
}