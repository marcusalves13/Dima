using MudBlazor;
using MudBlazor.Utilities;

namespace Dima.Web;

public static class Configuration
{
    public static string BackendUrl { get; set; } = "http://localhost:5218";
    public const string HttpClientName = "dima"; 
    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new DefaultTypography()
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        },
        PaletteDark = new PaletteDark()
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            AppbarBackground = Colors.LightGreen.Accent3,
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = new MudColor("#000000")
        },
        PaletteLight = new PaletteLight()
        {
            Primary = "#1EFA2D",
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Gray.Lighten4,
            AppbarBackground = new MudColor("#1EFA2D"),
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            PrimaryContrastText = new MudColor("#000000"),
            DrawerText = Colors.Shades.White,
            DrawerBackground = Colors.LightGreen.Darken4
        }
    };
}
