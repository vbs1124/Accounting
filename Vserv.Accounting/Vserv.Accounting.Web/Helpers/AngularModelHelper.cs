﻿using System;
﻿using System.Collections.Generic;
﻿using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Vserv.Accounting.Web.Code.Utilities;
using HtmlTags;
using Humanizer;

namespace Vserv.Accounting.Web.Helpers
{
    public class AngularModelHelper<TModel>
    {
        protected readonly HtmlHelper Helper;
        private readonly string _expressionPrefix;

        public AngularModelHelper(HtmlHelper helper, string expressionPrefix)
        {
            Helper = helper;
            _expressionPrefix = expressionPrefix;
        }

        /// <summary>
        /// Converts an lambda expression into a camel-cased string, prefixed
        /// with the helper's configured prefix expression, ie:
        /// vm.model.parentProperty.childProperty
        /// </summary>
        public IHtmlString ExpressionFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var expressionText = ExpressionForInternal(property);
            return new MvcHtmlString(expressionText);
        }

        /// <summary>
        /// Converts a lambda expression into a camel-cased AngularJS binding expression, ie:
        /// {{vm.model.parentProperty.childProperty}} 
        /// </summary>
        public IHtmlString BindingFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            return MvcHtmlString.Create("{{" + ExpressionForInternal(property) + "}}");
        }

        /// <summary>
        /// Creates a div with an ng-repeat directive to enumerate the specified property,
        /// and returns a new helper you can use for strongly-typed bindings on the items
        /// in the enumerable property.
        /// </summary>
        public AngularNgRepeatHelper<TSubModel> Repeat<TSubModel>(
            Expression<Func<TModel, IEnumerable<TSubModel>>> property, string variableName)
        {
            var propertyExpression = ExpressionForInternal(property);
            return new AngularNgRepeatHelper<TSubModel>(
                Helper, variableName, propertyExpression);
        }

        private string ExpressionForInternal<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var camelCaseName = property.ToCamelCaseName();

            var expression = !string.IsNullOrEmpty(_expressionPrefix)
                ? _expressionPrefix + "." + camelCaseName
                : camelCaseName;

            return expression;
        }

        public HtmlTag FormGroupFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var metadata = ModelMetadata.FromLambdaExpression(property, new ViewDataDictionary<TModel>());

            var name = ExpressionHelper.GetExpressionText(property);

            var expression = ExpressionForInternal(property);

            //Creates <div class="form-group has-feedback"
            //				form-group-validation="Name">
            var formGroup = new HtmlTag("div")
                .AddClasses("form-group", "has-feedback")
                .Attr("form-group-validation", name);

            var labelText = metadata.DisplayName ?? name.Humanize(LetterCasing.Title);

            //Creates <label class="control-label" for="Name">Name</label>
            var label = new HtmlTag("label")
                .AddClass("control-label")
                .Attr("for", name)
                .Text(labelText);

            var tagName = String.Empty;

            if (metadata.DataTypeName == "MultilineText")
            {
                tagName = "textarea";
            }
            else if (metadata.DataTypeName == "DateTime")
            {

            }
            else
            {
                tagName = "input";
            }
            //var tagName = metadata.DataTypeName == "MultilineText"
            //    ? "textarea"
            //    : "input";

            //var placeholder = metadata.Watermark ??
            //                  (labelText + "...");
            //Creates <input ng-model="expression"
            //		   class="form-control" name="Name" type="text" >
            //var input = new HtmlTag(tagName)
            //    .AddClass("form-control")
            //    .Attr("ng-model", expression)
            //    .Attr("name", name)
            //    .Attr("type", "text");
            //.Attr("placeholder", placeholder); //TODO: Un comment to include placeholder.

            HtmlTag input = null;

            if (metadata.DataTypeName == "DateTime")
            {
                input = GetDateTimeHtmlTag(expression, name, metadata);
            }
            else if (metadata.DataTypeName == "Currency")
            {
                input = GetCurrencyHtmlTag(expression, name, metadata);
            }
            else
            {
                input = new HtmlTag(tagName)
              .AddClass("form-control")
              .Attr("ng-model", expression)
              .Attr("name", name)
              .Attr("type", "text");

                ApplyValidationToInput(input, metadata);
            }

            return formGroup
                .Append(label)
                .Append(input);
        }

        private HtmlTag GetDateTimeHtmlTag(string expression, string name, ModelMetadata metadata)
        {
            string uniqueName = String.IsNullOrEmpty(expression) ? String.Empty : expression.Replace(".", String.Empty);
            var inputGroup = new HtmlTag("div").AddClass("input-group");

            var span = new HtmlTag("span").AddClass("input-group-btn");

            var button = new HtmlTag("button")
            .Attr("type", "button")
            .Attr("ng-click", String.Format("open{0}()", uniqueName))
           .AddClasses("btn", "btn-default");

            var iconTag = new HtmlTag("i").AddClasses("glyphicon", "glyphicon-calendar");
            button.Append(iconTag);
            span.Append(button);

            var inputControl = new HtmlTag("input")
            .Attr("type", "text")
            .Attr("uib-datepicker-popup", "dd/MM/yyyy")
            //.Attr("uib-datepicker-popup", "")
            .Attr("ng-model", expression)
            .Attr("is-open", String.Format("popup{0}.opened", uniqueName))
            .Attr("datepicker-options", "dateOptions")
            .Attr("close-text", "Close")
            .Attr("name", name)
            .AddClass("form-control");

            ApplyValidationToInput(inputControl, metadata);

            inputGroup.Append(span).Append(inputControl);
            return inputGroup;
        }

        private HtmlTag GetCurrencyHtmlTag(string expression, string name, ModelMetadata metadata)
        {
            var inputGroup = new HtmlTag("div").AddClass("input-group");

            var span = new HtmlTag("span").AddClass("input-group-addon");
            var iconTag = new HtmlTag("i").AddClasses("fa", "fa-inr");

            var inputControl = new HtmlTag("input")
            .Attr("type", "text")
            .Attr("ng-model", expression)
            .Attr("name", name)
            .AddClass("form-control");

            ApplyValidationToInput(inputControl, metadata);

            span.Append(iconTag);
            inputGroup.Append(span).Append(inputControl);
            return inputGroup;
        }

        private void ApplyValidationToInput(HtmlTag input, ModelMetadata metadata)
        {
            if (metadata.IsRequired)
                input.Attr("required", "");

            if (metadata.DataTypeName == "EmailAddress")
                input.Attr("type", "email");

            if (metadata.DataTypeName == "PhoneNumber")
                input.Attr("pattern", @"[\ 0-9()-]+");

            if (metadata.DataTypeName == "Currency")
                input.Attr("pattern", @"^[1-9]\d*(\.\d+)?$");
        }
    }
}