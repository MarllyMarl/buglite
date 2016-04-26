using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.IO;
using System.Web.Mvc;

namespace Bug_Lite.HelperClasses
{
    public static class HtmlHelperExtensions
    {

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]

        public static MvcHtmlString RequiredFieldFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "")
        {

            return LabelHelper(html,

                ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                ExpressionHelper.GetExpressionText(expression), labelText);
        }

        private static MvcHtmlString LabelHelper(HtmlHelper html,
            ModelMetadata metadata, string htmlFieldName, string labelText)
        {
            if (string.IsNullOrEmpty(labelText))
            {
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            }

            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            bool isRequired = false;

            if (metadata.ContainerType != null)
            {
                isRequired = metadata.ContainerType.GetProperty(metadata.PropertyName)

                                .GetCustomAttributes(typeof(RequiredAttribute), false)
                                .Length == 1;
            }

            TagBuilder tag = new TagBuilder("label");
            tag.Attributes.Add("for",

                TagBuilder.CreateSanitizedId(
                    html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)
                )
            );

            if (isRequired)

             tag.Attributes.Add("class", "label-required");
             tag.SetInnerText("");
             //tag.SetInnerText(labelText);
             var output = tag.ToString(TagRenderMode.Normal);

            //if (isRequired)
            //{
            //    var asteriskTag = new TagBuilder("span");
            //    asteriskTag.Attributes.Add("class", "required");
            //    asteriskTag.SetInnerText("*");
            //    output += asteriskTag.ToString(TagRenderMode.Normal);
            //}
            if (isRequired)
            {
                var asterisktag = new TagBuilder("span");
                //asterisktag.AddCssClass("req-asterisk");
                //asterisktag.MergeAttributes(new Dictionary<string, object> { { "style", "color: red; margin-left: 3px; margin-right: 0px; vertical-align: middle; font:2.3em 'Trebuchet MS',helvetica,arial,verdana;" } });
                //asterisktag.SetInnerText("*");
                //output += asterisktag.ToString(TagRenderMode.Normal);
            }

            return MvcHtmlString.Create(output);
        }
    }
}