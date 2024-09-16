using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Data.Validation;

public class Validator
{
    private readonly List<string> errorMessages = [];

    private Validator() { }

    public static Validator Create()
    {
        return new Validator();
    }

    public void ThrowIfInvalid()
    {
        if (errorMessages.Count > 0)
        {
            var combinedErrorMessage = string.Join(", ", errorMessages);
            throw new ValidationException(combinedErrorMessage);
        }
    }

    public Validator NotNull<T>(string paramName, T value)
        where T : class
    {
        if (value == null)
        {
            errorMessages.Add($"{paramName} can not be null");
        }
        return this;
    }

    public Validator MinValue(string paramName, int value, int minValue)
    {
        if (value < minValue)
        {
            errorMessages.Add($"{paramName} must be greater than or equal to {minValue}");
        }
        return this;
    }

    public Validator MinValue(string paramName, decimal value, decimal minValue)
    {
        if (value < minValue)
        {
            errorMessages.Add($"{paramName} must be greater than or equal to {minValue}");
        }
        return this;
    }

    public Validator NotEmptyString(string paramName, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            errorMessages.Add($"{paramName} cannot be empty");
        }
        return this;
    }
}
