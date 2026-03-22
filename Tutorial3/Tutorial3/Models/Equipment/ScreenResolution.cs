namespace Tutorial3.Models.Equipment;

public record ScreenResolution(int Width, int Height) {
    public override string ToString()
    {
        return $"{Width}x{Height}";
    }
};