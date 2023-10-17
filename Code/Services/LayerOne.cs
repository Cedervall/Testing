namespace Code.Services;

public interface ILayerOne
{
    public void GenerateDto();

}

public class LayerOne : ILayerOne
{
    private readonly ILayerTwo _layerTwo;
    private readonly DataService _dataService;

    public LayerOne(ILayerTwo layerTwo, DataService dataService)
    {
        _layerTwo = layerTwo;
        _dataService = dataService;
    }

    public void GenerateDto()
    {
        var dto = _dataService.GetGeneralDto();
        dto.FieldDate = DateTime.Parse("2023-03-03 03:03");
        _layerTwo.Add(dto);
    }
}
