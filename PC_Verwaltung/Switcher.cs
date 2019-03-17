using System.Windows.Controls;

namespace PC_Verwaltung
{
  	public static class Switcher
  	{
    	public static MainWindow Window;

    	public static void Switch(UserControl content)
    	{
            Window.Navigate(content);
    	}
  	}
}
