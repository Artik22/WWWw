#pragma checksum "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2cf0b4dffbd9dce344e130a40cbc9f951650c104"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MyTest_Details), @"mvc.1.0.view", @"/Views/MyTest/Details.cshtml")]
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
#line 1 "D:\проект\WWW\WWWw\Views\_ViewImports.cshtml"
using web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml"
using web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2cf0b4dffbd9dce344e130a40cbc9f951650c104", @"/Views/MyTest/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"47e2989be7935e1a8b5e6466c593fab35ffacecd", @"/Views/_ViewImports.cshtml")]
    public class Views_MyTest_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 5 "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml"
  
    ViewData["Title"] = "Test";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n\r\n\r\n    <fieldset>\r\n        <legend>Информация о сотруднике</legend>\r\n\r\n        <div class=\"display-label\"><b>Имя</b></div>\r\n        <div class=\"display-field\">\r\n            ");
#nullable restore
#line 18 "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml"
       Write(Html.DisplayFor(model => model.FirsName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"display-label\"><b>Фамилия</b></div>\r\n        <div class=\"display-field\">\r\n            ");
#nullable restore
#line 23 "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml"
       Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"display-label\"><b>Курсы</b></div>\r\n        <ul>\r\n");
#nullable restore
#line 28 "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml"
             foreach (Article c in Model.Articles)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>");
#nullable restore
#line 30 "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml"
               Write(c.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 31 "D:\проект\WWW\WWWw\Views\MyTest\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n\r\n    </fieldset>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<User> Html { get; private set; }
    }
}
#pragma warning restore 1591
