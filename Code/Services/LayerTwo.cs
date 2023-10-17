namespace Code.Services;

public interface ILayerTwo
{
    public bool Add(GeneralDto dto);
}

public class LayerTwo : ILayerTwo
{
    public bool Add(GeneralDto dto)
    {
        // Do something
        return true;
    }
}
