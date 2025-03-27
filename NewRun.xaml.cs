namespace RPG;


public partial class NewRun : ContentPage
{
    public NewRun()
	{
		InitializeComponent();
    }

private async void NextPage(object sender, EventArgs e)
    {
        if (c1.IsChecked)
        {
            GlobalPlayer.player1.Class = "Knight";
        }
        else if (c2.IsChecked)
        {
            GlobalPlayer.player1.Class = "Archer";
        }
        else
        {
            GlobalPlayer.player1.Class = "Mage";
        }

        GlobalPlayer.player1.Nickname = Nickname.Text;

        if (!(c1.IsChecked || c2.IsChecked || c3.IsChecked))
        {
            DisplayAlert("Error", "Choose your class", "OK");
        }
        else if (Nickname.Text == "")
        {
            DisplayAlert("Error", "Enter your nickname", "OK");
        }
        else
        {
            await Navigation.PushAsync(new Travel());
        }
    }
}