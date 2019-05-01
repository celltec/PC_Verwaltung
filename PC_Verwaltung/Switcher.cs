using System.Windows;
using System.Windows.Controls;

namespace PC_Verwaltung
{
  	public static class Switcher
  	{
    	public static Window Window;

    	public static void Navigate(Page content)
    	{
            Window.Content = content;
    	}
    }
}
