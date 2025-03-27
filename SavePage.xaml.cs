namespace RPG;

public partial class SavePage : ContentPage
{
	public SavePage()
	{
		InitializeComponent();
	}
    private async void NextPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SelectedSavePage());
    }
}