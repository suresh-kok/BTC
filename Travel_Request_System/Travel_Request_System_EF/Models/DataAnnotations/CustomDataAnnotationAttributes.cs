using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Travel_Request_System_EF.Models.DataAnnotations
{
    public class CustomDataAnnotationAttributes
    {

    }

    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dt = (DateTime)value;
                if (dt.Date >= DateTime.UtcNow.Date)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(ErrorMessage ?? "Make sure your " + validationContext.MemberName + " is >= than today");
            }
            return ValidationResult.Success;
        }
    }

    public sealed class IsDateAfterAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string testedPropertyName;
        private readonly bool allowEqualDates;

        public IsDateAfterAttribute(string testedPropertyName, bool allowEqualDates = false)
        {
            this.testedPropertyName = testedPropertyName;
            this.allowEqualDates = allowEqualDates;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(testedPropertyName);
            if (propertyTestedInfo == null)
            {
                return new ValidationResult(string.Format("unknown property {0}", testedPropertyName));
            }

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || !(value is DateTime))
            {
                return ValidationResult.Success;
            }

            if (propertyTestedValue == null || !(propertyTestedValue is DateTime))
            {
                return ValidationResult.Success;
            }

            // Compare values
            if ((DateTime)value >= (DateTime)propertyTestedValue)
            {
                if (allowEqualDates && ((DateTime)value).Date == ((DateTime)propertyTestedValue).Date)
                {
                    return ValidationResult.Success;
                }
                else if ((DateTime)value > (DateTime)propertyTestedValue)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "isdateafter"
            };
            rule.ValidationParameters["propertytested"] = testedPropertyName;
            rule.ValidationParameters["allowequaldates"] = allowEqualDates;
            yield return rule;
        }
    }

    public class RequiredIfAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value != null)
            {
                Object instance = context.ObjectInstance;
                Type type = instance.GetType();
                Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
                if (proprtyvalue.ToString() == DesiredValue.ToString())
                {
                    ValidationResult result = base.IsValid(value, context);
                    return result;
                }
            }
            return ValidationResult.Success;
        }
    }
}