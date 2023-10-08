using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("card-footer")]
    public class CardFooterTagHelper : TagHelper
    {
        public string AddationalClass { get; set; } = "text-center";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string preContent = @"
                <div class='card-footer {0}'>
                        
            ";
            string postContent = @"
            </div>                
            ";
            output.TagName = "div";
            output.PreContent.SetHtmlContent(
                string.Format(preContent,AddationalClass));
            output.PostContent.SetHtmlContent(postContent);
        }
    }
}
