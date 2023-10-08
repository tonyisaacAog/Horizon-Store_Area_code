using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("cardM")]
    public class CardMTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string AddationalClass { get; set; }
        public string TextColorClass { get; set; }
        public string HeaderClass { get; set; }
        public string BindingText { get; set; }
        public string IconButton { get; set; }
        public string ActionButton { get; set; }
        public string ColorButton { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            string preContent = @"
                <div class='card mt-3' {4}>
                        <div class='card-header {3} '>
                            <b class='{0} '>
                                <span class='fas {1} '> </span>&nbsp; 
                                <span>{2}</span>
                            </b>
                          
                        </div>
                        <div class='card-body'>
            ";
            string postContent = @"
            </div>                
            </div>
            ";
            output.TagName = "div";
            output.PreContent.SetHtmlContent(
                string.Format(preContent, TextColorClass,AddationalClass,Title,HeaderClass, BindingText,IconButton,ActionButton,ColorButton));
            output.PostContent.SetHtmlContent(postContent);
        }
    }
}
