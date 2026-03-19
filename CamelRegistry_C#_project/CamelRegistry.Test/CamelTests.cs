namespace CamelRegistry.Test
{
	using System.ComponentModel.DataAnnotations;
	using Xunit;

	public class CamelTests
	{
		[Fact]
		public void Camel_WithInvalidHumpCount_FailsValidation()
		{
			var camel = new Camel
			{
				Name = "Johnny",
				Color = "Brown",
				HumpCount = -10
			};
			Assert.False(IsValidHumpCount(camel));
		}

		private bool IsValidHumpCount(Camel c)
		{
			return c.HumpCount >= 1 && c.HumpCount <= 2;
		}

		[Fact]
		public void Camel_WithInvalidName_FailsValidation()
		{
			var camel = new Camel
			{
				Name = "",
				Color = "Red",
				HumpCount = 1
			};

			var results = Validate(camel);

			Assert.NotEmpty(results);
		}

		[Fact]
		public void Camel_WithCorrectProps_PassesValidation()
		{
			var camel = new Camel
			{
				Name = "Michael",
				Color = "Purple",
				HumpCount = 2
			};

			var results = Validate(camel);

			Assert.Empty(results);
		}

		private static List<ValidationResult> Validate(Camel camel)
		{
			var context = new ValidationContext(camel);
			var results = new List<ValidationResult>();

			Validator.TryValidateObject(camel, context, results, true);

			return results;
		}
	}
}
