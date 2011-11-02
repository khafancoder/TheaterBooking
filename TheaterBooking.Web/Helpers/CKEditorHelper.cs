using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Globalization;

namespace TheaterBooking.Web
{
    public static class CKEditorHelper
    {

        public static MvcHtmlString CKTextBox(this HtmlHelper u, string name)
        {
            return u.CKTextBox(name, null);
        }

        public static MvcHtmlString CKTextBox(this HtmlHelper u, string name, object value)
        {
            return u.CKTextBox(name, value == null ? "" : value.ToString());
        }

        public static MvcHtmlString CKTextBox(this HtmlHelper u, string name, string value)
        {

            return u.CKTextBox(name, string.IsNullOrEmpty(value) ? "" : value.ToString(), null);
        }

        public static MvcHtmlString CKTextBox(this HtmlHelper u, string name, string value, string CKEditorAttributes)
        {
            if (value == null)
            {
                value = Convert.ToString(u.ViewDataContainer.ViewData[name], CultureInfo.InvariantCulture);
            }

            string langCode = "fa";
            string fileBrowserUrl = new UrlHelper(u.ViewContext.RequestContext).Action("GenericBrowser", "Resource");

            string ckAttributes = string.Format("{{language : '{0}',filebrowserBrowseUrl : '{1}', height: 400", langCode, fileBrowserUrl);

            if (!string.IsNullOrEmpty(CKEditorAttributes))
                ckAttributes += "," + CKEditorAttributes;

            ckAttributes += "}";


            string output = string.Format(@"
<textarea name=""{0}"" id=""{0}"" rows=""50"" cols=""80"" style=""display:none; width:100%; height: 400px"">{1}</textarea>
<script type=""text/javascript"">
    $(function()
    {{
        var editor = CKEDITOR.replace( '{0}' , {2});
        $(""#{0}"").show();
    }});	
</script>",
            name, value, ckAttributes);

            return new MvcHtmlString(output);

        }
    }
}