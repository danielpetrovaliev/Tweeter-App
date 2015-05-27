namespace Tweeter.Web.Extensions
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;

    public static class LinkHellpersExtensions
    {
        public static MvcHtmlString RawAjaxActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, 
            string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repId = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repId, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repId, linkText));
        }

        public static MvcHtmlString RawActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
            string controllerName, object routeValues, object htmlAttributes)
        {
            var repId = Guid.NewGuid().ToString();
            var lnk = htmlHelper.ActionLink(repId, actionName, controllerName, routeValues, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repId, linkText));
        }

        public static MvcHtmlString RawActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
            string controllerName)
        {
            var repId = Guid.NewGuid().ToString();
            var lnk = htmlHelper.ActionLink(repId, actionName, controllerName);
            return MvcHtmlString.Create(lnk.ToString().Replace(repId, linkText));
        }
    }
}