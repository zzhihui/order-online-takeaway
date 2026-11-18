using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Takeway.Web.Application.Extension
{
    public class RedirectToActionAnchor: RedirectToRouteResult
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the anchor.
        /// </summary>
        public string Anchor { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectToActionAnchor"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="anchor">The anchor.</param>
        /// <param name="routeValues">The route values.</param>
        public RedirectToActionAnchor(string action, string anchor, object routeValues)
            : this(action, null, anchor, routeValues)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectToActionAnchor"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="anchor">The anchor.</param>
        /// <param name="routeValues">The route values.</param>
        public RedirectToActionAnchor(string action, string controller, string anchor, object routeValues)
            : base(new RouteValueDictionary(routeValues))
        {
            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentNullException(action);

            this.Action = action;
            this.Anchor = anchor;
            this.Controller = controller;
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="context"/> parameter is null.</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.RouteValues["action"] = this.Action;

            if (!string.IsNullOrWhiteSpace(this.Controller))
                this.RouteValues["controller"] = this.Controller;

            RequestContext requestContext = new RequestContext(context.HttpContext, RouteTable.Routes.GetRouteData(context.HttpContext));
            VirtualPathData vpd = RouteTable.Routes.GetVirtualPath(requestContext, RouteName, RouteValues);

            if (vpd == null || string.IsNullOrWhiteSpace(vpd.VirtualPath))
            {
                throw new InvalidOperationException("No route matched");
            }

            string target = vpd.VirtualPath;

            if (!string.IsNullOrWhiteSpace(this.Anchor))
            {
                // Add the anchor onto the end:
                target += string.Format("#{0}", Anchor);
            }

            context.HttpContext.Response.Redirect(target, false);
        }
    }
}