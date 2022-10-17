using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BusinessManager.Model
{
	public class NotEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, "Field is required.")
				: new ValidationResult(true, "Field is qqq.");
		}

		
	}

	public class InputValidationRule : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{

			string input = value.ToString();

			bool rt = Regex.IsMatch(input, @"^\d+([.,]\d{1,2})?$");
			if (!rt)
			{
				return new ValidationResult(false, this.ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}

	
		
	}

	public class RegexValidationRule : ValidationRule
	{
		#region Data

		private string errorMessage;
		private RegexOptions regexOptions = RegexOptions.None;
		private string regexText;

		#endregion // Data

		#region Constructors

		public RegexValidationRule()
		{
		}

		public RegexValidationRule(string regexText)
		{
			this.RegexText = regexText;
		}

		public RegexValidationRule(string regexText, string errorMessage)
			: this(regexText)
		{
			this.RegexOptions = regexOptions;
		}


		public RegexValidationRule(string regexText, string errorMessage, RegexOptions regexOptions)
			: this(regexText)
		{
			this.RegexOptions = regexOptions;
		}

		#endregion // Constructors

		#region Properties


		public string ErrorMessage
		{
			get { return this.errorMessage; }
			set { this.errorMessage = value; }
		}


		public RegexOptions RegexOptions
		{
			get { return regexOptions; }
			set { regexOptions = value; }
		}

		public string RegexText
		{
			get { return regexText; }
			set { regexText = value; }
		}

		#endregion // Properties

		#region Validate


		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			ValidationResult result = ValidationResult.ValidResult;

	
			if (!String.IsNullOrEmpty(this.RegexText))
			{
				string text = value as string ?? String.Empty;
				if (!Regex.IsMatch(text, this.RegexText, this.RegexOptions))
					result = new ValidationResult(false, this.ErrorMessage);
			}

			return result;
		}

		#endregion // Validate
	}
}
