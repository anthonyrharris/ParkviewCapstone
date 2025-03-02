#pragma checksum "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "44f1ace0c980d9a85a1f2dca9c5fab0fadaeedee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Chat_CoachIndex), @"mvc.1.0.view", @"/Views/Chat/CoachIndex.cshtml")]
namespace AspNetCore
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
#line 1 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/_ViewImports.cshtml"
using Pulse;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/_ViewImports.cshtml"
using Pulse.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
using Web.ViewModels.ChatViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
using Pulse.EntityFramework.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"44f1ace0c980d9a85a1f2dca9c5fab0fadaeedee", @"/Views/Chat/CoachIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc8f594ab4734867f415a78a6ed5a2bd419e7d40", @"/Views/_ViewImports.cshtml")]
    public class Views_Chat_CoachIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ChatIndexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Chat", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ViewChat", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            WriteLiteral("\n");
#nullable restore
#line 9 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
  
    ViewData["Title"] = "All Current Chats";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"card mb-4 mt-3\">\n    <div class=\"card-header\"><i class=\"fas fa-table mr-1\"></i>");
#nullable restore
#line 14 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                                                         Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
    <div class=""card-body"">
        <div class=""table-responsive"">
            <table class=""table table-bordered"" id=""dataTable"" cellspacing=""0"">
                <thead>
                    <tr>
                        <th>Patient</th>
                        <th>Messages</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 25 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                     if (Model.AssignedPatients.Count > 0)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                         foreach (var patient in Model.AssignedPatients)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\n                                <td>");
#nullable restore
#line 30 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                               Write(patient.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                                <td class=\"text-center\">\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "44f1ace0c980d9a85a1f2dca9c5fab0fadaeedee6110", async() => {
                WriteLiteral("\n                                        <button class=\"btn btn-primary\">View Messages</button>\n                                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-peerCoachId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                                                                                              WriteLiteral(Model.PeerCoach.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["peerCoachId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-peerCoachId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["peerCoachId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 32 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                                                                                                                                        WriteLiteral(patient.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["patientId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-patientId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["patientId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                                </td>\n                            </tr>\n");
#nullable restore
#line 37 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                         
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p>");
#nullable restore
#line 41 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                      Write(Model.PeerCoach.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" has no assigned patients.</p>\n");
#nullable restore
#line 42 "/Users/TonyHarris/Desktop/Capstone/Pulse/Pulse/Views/Chat/CoachIndex.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n            </table>\n        </div>\n    </div>\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<Account> userManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<Account> signInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChatIndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
