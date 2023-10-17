namespace Code.Services;

public class DataService
{
    public GeneralDto GetGeneralDto() => new GeneralDto()
    {
        FieldOne = "Field one",
        FieldTwo = 2
    };
}