namespace DSUI
{
    public interface IValidation
    {
        string ValidateString(string message);
        int ValidateInt(string message);
        double ValidateDouble(string message);
        string ValidateAddress(string message);
        long ValidatePhone(string message);
        string ValidateName(string message);
        char ValidateGender(string message);
    }
}