namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob) //! static mora jer je extesnion
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dob.Year;

        if (dob > today.AddYears(-age)) age--;

        return age;

    }
}
