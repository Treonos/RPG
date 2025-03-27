namespace RPG;

public partial class Travel : ContentPage
{
	public Travel()
	{
		InitializeComponent();
	}

    private async void NextPage(object sender, EventArgs e)
    {
        if (b1.IsChecked)
        {
            GlobalPlayer.player1.Biome = "Desert";
        }
        else if (b2.IsChecked)
        {
            GlobalPlayer.player1.Biome = "Mountains";
        }
        else if (b3.IsChecked)
        {
            GlobalPlayer.player1.Biome = "Castle";
        }
        else
        {
            GlobalPlayer.player1.Biome = "Swamp";
        }
        if (!(b1.IsChecked || b2.IsChecked || b3.IsChecked || b4.IsChecked))
        {
            DisplayAlert("Error", "Choose your destination", "OK");
        }
        else
        {
            await Navigation.PushAsync(new NewPage1());
        }
    }
}