#pragma checksum "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\Administration\ManageUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "30cea60514173adfacb2eebb44fbf59ae0d17dba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(RevenueCompanion.Presentation.Pages.Administration.Pages_Administration_ManageUser), @"mvc.1.0.razor-page", @"/Pages/Administration/ManageUser.cshtml")]
namespace RevenueCompanion.Presentation.Pages.Administration
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30cea60514173adfacb2eebb44fbf59ae0d17dba", @"/Pages/Administration/ManageUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"006187e71ba024b2aaaef0307acd878b84c50618", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Administration_ManageUser : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/Administration/Users", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("float:right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/appscripts/manageuser.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\Administration\ManageUser.cshtml"
  
    var selectedUserId = ViewData["selectedUserId"].ToString();
    var roleId = ViewData["roleId"].ToString();
    var userEmail = HttpContextAccessor.HttpContext.Session.GetString("userEmail");

#line default
#line hidden
#nullable disable
            WriteLiteral("<input type=\"hidden\" id=\"selectedUserId\"");
            BeginWriteAttribute("value", " value=\"", 328, "\"", 351, 1);
#nullable restore
#line 8 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\Administration\ManageUser.cshtml"
WriteAttributeValue("", 336, selectedUserId, 336, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n<input type=\"hidden\" id=\"roleId\"");
            BeginWriteAttribute("value", " value=\"", 389, "\"", 404, 1);
#nullable restore
#line 9 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\Administration\ManageUser.cshtml"
WriteAttributeValue("", 397, roleId, 397, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n<div class=\"page-title\">\r\n    <h3 class=\"title\">Users</h3>\r\n</div>\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n        <div class=\"panel panel-default\">\r\n            <div>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30cea60514173adfacb2eebb44fbf59ae0d17dba6537", async() => {
                WriteLiteral("<i class=\"fa-backward\"></i>&nbsp;Go Back");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n            <div class=\"panel-heading\">\r\n                <h3 class=\"panel-title\">Manage Access Rights Of: ");
#nullable restore
#line 20 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\Administration\ManageUser.cshtml"
                                                            Write(userEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                <br />
                <br />
            </div>
            <div class=""row"">
                <div class=""col-md-6"">
                    <div class=""panel panel-default"">
                        <h3 class=""panel-title"">Role Pages</h3>
                        <div class=""panel-body"">
                            <div class=""row"">
                                <div class=""col-md-12 col-sm-12 col-xs-12"">
                                    <table ");
            WriteLiteral(@" class=""table table-bordered"" id=""table_pages_by_roleId"">
                                        <thead>
                                            <tr>
                                                <th>S/N</th>
                                                <th>Menu Name</th>
                                                <th>Action &nbsp;&nbsp; <input type=""checkbox"" id=""rowPagesCheckAll"" class=""rolePagesOne"" name=""rolePagesOne"" /></th>
                                            </tr>
                                        </thead>
                                        <tbody id=""table_pages_by_roleId_body"">
                                        </tbody>
                                    </table>
                                    <div class=""text-center"">


                                        <button class=""btn  btn-primary m-t-n-xs"" type=""submit"" id=""btn-assignRowPages""><strong>Assign</strong></button>
                                    </div>
                          ");
            WriteLiteral(@"      </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""col-md-6"">
                    <div class=""panel panel-default"">
                        <h3 class=""panel-title"">User Assigned Pages</h3>
                        <div class=""panel-body"">
                            <div class=""row"">
                                <div class=""col-md-12 col-sm-12 col-xs-12"">
                                    <table class=""table table-bordered"" id=""table_pages_by_userId"">
                                        <thead>
                                            <tr>
                                                <th>S/N</th>
                                                <th>Menu Name</th>
                                                <th>Action &nbsp;&nbsp; <input type=""checkbox"" id=""userPagesCheckAll"" class=""userPagesOne"" name=""userPagesOne"" /></th>

                                            </t");
            WriteLiteral(@"r>
                                        </thead>
                                        <tbody id=""table_pages_by_userId_body"">
                                        </tbody>
                                    </table>
                                    <div class=""text-center"">


                                        <button class=""btn  btn-danger m-t-n-xs"" type=""submit"" id=""btn-removeAssignedPages""><strong>Remove Pages</strong></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>
");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "30cea60514173adfacb2eebb44fbf59ae0d17dba11871", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 86 "D:\Dev\C#\source\repos\Revenue\RevenueCompanion\RevenueCompanion\RevenueCompanion.Presentation\Pages\Administration\ManageUser.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RevenueCompanion.Presentation.Pages.Administration.ManageUserModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<RevenueCompanion.Presentation.Pages.Administration.ManageUserModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<RevenueCompanion.Presentation.Pages.Administration.ManageUserModel>)PageContext?.ViewData;
        public RevenueCompanion.Presentation.Pages.Administration.ManageUserModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
