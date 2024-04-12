using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using BrickWells.Models.ViewModels;

namespace BrickWells.Infrastructure;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PaginationTagHelper : TagHelper
{
    private IUrlHelperFactory urlHelperFactory;
    
    public PaginationTagHelper(IUrlHelperFactory temp)
    {
        urlHelperFactory = temp;
    }
    
    [ViewContext] //information from the table
    [HtmlAttributeNotBound] // prevent users to not be able to access the infromation from the ViewCOntext
    public ViewContext? ViewContext { get; set; }
    public string? PageAction { get; set; } // what action are we on in out controller
    
    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")] // the prefix for the dictionary attribute
    public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
    public PaginationInfo PageModel { get; set; } // the information about the page
    
    public bool PageClassesEnabled { get; set; } = false;
    public string PageClass { get; set; } = String.Empty;
    public string PageClassNormal { get; set; } = String.Empty;
    public string PageClassSelected { get; set; } = String.Empty;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext != null && PageModel != null)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            
            TagBuilder result = new TagBuilder("div");
            
            for (int i = 1; i <= PageModel.totalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                
                PageUrlValues["pageNum"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                
                if(PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.currentPage ? PageClassSelected : PageClassNormal);
                }
                
                tag.InnerHtml.Append(i.ToString());
                
                result.InnerHtml.AppendHtml(tag);
            }
            
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
} 