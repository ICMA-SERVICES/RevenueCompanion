#pragma checksum "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\PayeeDefaulter\DefaulterHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "77c8cfdeb3b151549822374f8df5301108ac2f44"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(RevenueCompanion.Presentation.Pages.PayeeDefaulter.Pages_PayeeDefaulter_DefaulterHistory), @"mvc.1.0.razor-page", @"/Pages/PayeeDefaulter/DefaulterHistory.cshtml")]
namespace RevenueCompanion.Presentation.Pages.PayeeDefaulter
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\_ViewImports.cshtml"
using RevenueCompanion.Presentation;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"77c8cfdeb3b151549822374f8df5301108ac2f44", @"/Pages/PayeeDefaulter/DefaulterHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"006187e71ba024b2aaaef0307acd878b84c50618", @"/Pages/_ViewImports.cshtml")]
    public class Pages_PayeeDefaulter_DefaulterHistory : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/appscripts/defaulterHistory.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""row"">
    <div class=""col-md-12"">
        <div class=""panel panel-default"">
            <div class=""panel-heading"">                
                <div class=""row"">
                    <div class=""col-md-8 col-sm-6 col-xl-6"">
                        <h3>Defaulter History</h3>
                    </div>
                    <div class=""col-md-4 col-sm-6 col-xl-6 row"">
                        <div class=""form-group col-md-4"">
                            <label>Year</label>
                            <select class=""form-control"" id=""yearField"">
                            </select>
                        </div>
                        <div class=""form-group col-md-6"">
                            <label for=""payerUtin"">Payer Utin</label>
                            <div class=""form-group default-select select2Style mt-3"">
                                <select class=""form-control select2"" data-placeholder=""Select"" id=""payerUtin"">
                                </select>
        ");
            WriteLiteral(@"                    </div>
                        </div>
                        <div class=""col-md-2"">
                            <button class=""btn btn-primary"" style=""margin-top: 28px;"" id=""filterTable"">Filter</button>
                        </div>
                    </div>
                </div>

            </div>
            <br />
            <div id=""RequestContainer""></div>

        </div>
    </div>
    <div id=""cover-spin""></div>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "77c8cfdeb3b151549822374f8df5301108ac2f445540", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 42 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\PayeeDefaulter\DefaulterHistory.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public RevenueCompanion.Presentation.Services.MenuSetup Menus { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RevenueCompanion.Presentation.Pages.SummaryReport.DefaulterHistoryModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<RevenueCompanion.Presentation.Pages.SummaryReport.DefaulterHistoryModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<RevenueCompanion.Presentation.Pages.SummaryReport.DefaulterHistoryModel>)PageContext?.ViewData;
        public RevenueCompanion.Presentation.Pages.SummaryReport.DefaulterHistoryModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591